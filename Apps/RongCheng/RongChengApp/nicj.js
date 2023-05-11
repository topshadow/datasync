$package("app.desktop")

$import("app.modules.common")

app.desktop.Module = function (config) {
    this.opener = null;
    this.mainApp = null
    this.openWinInTaskbar = true;
    this.autoRun = false
    this.autoInit = true
    this.type = "USER"
    Ext.apply(this, app.modules.common);
    Ext.apply(this, config);
    app.desktop.Module.superclass.constructor.call(this);
    this.addEvents({ "NotLogon": true })
    this.classId = this.script + "-" + this.id
    this.midiModules = {}
    this.midiWins = {}
    this.midiMenus = {}
    this.midiComponents = {}
    if (this.autoInit) {
        this.init();
    }
}

Ext.extend(app.desktop.Module, Ext.util.Observable, {
    init: function () { },
    getWin: function () { return null },
    setMainApp: function (mainApp) {
        this.mainApp = mainApp;
        if (!mainApp) {
            return
        }
        if (this.fullId) {
            var fid = this.ref || this.fullId
            var p = fid.indexOf(".")
            if (p > -1) {
                var domain = fid.substring(0, p);
                this.mainApp = mainApp.taskManager.loadLogonInfo(domain, mainApp)
            }
        }
        this.logger = mainApp.logger.createLogger(this.classId)
        this.renderToEl = mainApp.desktop.getDesktopEl()
    },
    getRenderToEl: function () {
        if (this.mainApp) {
            return this.mainApp.desktop.getDesktopEl();
        }
        if (this.opener && this.opener.getRenderToEl) {
            return this.opener.getRenderToEl();
        }
        return null;
    },
    destory: function () {
        var win = this.win
        if (win) {
            if (win.cmenu && win.cmenu.el) {
                win.cmenu.el.remove()
            }
            win.close()
            this.win = null
        }
        for (var id in this.midiModules) {
            this.midiModules[id].destory()
        }
        this.midiModules = {};
        for (var id in this.midiWins) {
            var win = this.midiWins[id]
            win.close()
        }
        this.midiWins = {};
        for (var id in this.midiMenus) {
            var m = this.midiMenus[id]
            m.destroy();
        }
        this.midiMenus = {};
        for (var id in this.midiComponents) {
            var c = this.midiComponents[id]
            if (c.destroy) {
                delete c.ownerCt
                c.destroy()
            }
        }
        this.midiComponents = {};
        this.fireEvent("destory", this)
    }

});
$package("app.desktop")
$import("util.rmi.miniJsonRequestSync")
app.desktop.TaskManager = function (mainApp) {
    this.addEvents({
        "beforeLoadModule": true,
        "loadStateChange": true,
        "afterloadModule": true,
        "loadModuleFalied": true,
        "afterDestoryModule": true
    })
    this.mainApp = mainApp
    this.modules = mainApp.modules
    this.logger = mainApp.logger.createLogger("app.desktop.TaskManager")
    this.tasks = new Ext.util.MixedCollection();

    mainApp.on("ready", function () {
        this.win = null;
        this.count = 0;
        if (!this.modules) {
            return
        }
        var modules = this.modules
        for (var i = 0; i < modules.length; i++) {
            var module = modules[i]
            module.pid = ++this.count;
            module.instance = null
            module.mainApp = this.mainApp
            this.tasks.add(module.fullId, module)
            if (module.autoRun) {
                this.loadInstance(module.fullId)
            }
        }

    }, this)
    app.desktop.TaskManager.superclass.constructor.call(this);
}

Ext.extend(app.desktop.TaskManager, Ext.util.Observable, {
    addModuleCfg: function (module) {
        if (!this.tasks.item(module.fullId)) {
            var pid = this.count++
            module.pid = pid;
            module.title = module.name
            module.instance = null
            this.tasks.add(module.fullId, module);
            if (module.autoRun) {
                this.getModuleInstance(module.fullId)
            }
        }

    },

    getModule: function (id) {
        return this.tasks.item(id)
    },

    loadInstance: function (id, exCfg, fCallback, scope) {
        var module = this.tasks.item(id)
        if (!module) {
            var res = this.loadModuleCfg(id);
            if (res.code != 200) {
                if (res.msg == "NotLogon") {
                    this.mainApp.logon(this.loadInstance, this, [id, exCfg, fCallback, scope])
                } else {
                    if (typeof fCallback == "function") {
                        fCallback.apply(scope, []);
                    }
                    if (res.msg.indexOf("is not accessible") > 0) {
                        Ext.MessageBox.alert("错误", "当前角色没有权限,请联系管理员")
                    }
                }
                return
            }
            module = res.json.body
            if (module) {
                if (module.fullId != id) {
                    module.fullId = id
                }
                this.addModuleCfg(module)
            }
            else {
                this.fireEvent("loadModuleFalied", module);
                return;
            }
        }
        if (module) {
            Ext.apply(module, module.properties);
            Ext.apply(module, exCfg);
            var obj = module.instance
            if (obj == null) {
                var script = module.script
                this.fireEvent("beforeLoadModule", id, script);
                $require(script, [function () {
                    try {
                        var obj = eval("new " + script + "(module)");
                        obj.on("NotLogon", this.mainApp.logon, this.mainApp)
                        module.instance = obj
                        if (typeof fCallback == "function") {
                            if (typeof scope == "object") {
                                obj.opener = scope
                                obj.setMainApp(this.mainApp)
                            }
                            fCallback.apply(scope, [obj, 'remote'])
                        }
                        this.fireEvent("afterLoadModule", module)
                    }
                    catch (e) {
                        throw e
                        this.logger.error("new module instance [" + script + "] failed:" + e)
                        this.fireEvent("loadModuleFalied", module, e);
                    }
                }, this],
                    [function (state, cls, status, e) {
                        this.fireEvent("loadStateChange", module.title, state)
                        if (e || status > 300) {
                            this.logger.error("load module [" + module.id + "] failed:" + e)
                            this.fireEvent("loadModuleFalied", module, e);
                        }
                    }, this]
                )//$require
            }//if(obj == null)
            else {
                if (typeof fCallback == "function") {
                    if (typeof scope == "object")
                        obj.opener = scope
                    fCallback.apply(scope, [obj, "local"])

                    if (obj.win && obj.win.isVisible()) {
                        obj.win.toFront()
                    }
                }
            }
        }//if(modlue)
        else {
            this.logger.error("module [" + id + "] config not found")
            this.fireEvent("loadModuleFalied", { title: id }, null)
            return null
        }
    },

    destroyInstance: function (id) {
        var module = this.tasks.item(id)
        if (module && module.instance) {
            module.instance.destory();
            module.instance = null;
            this.tasks.removeKey(id)
            this.fireEvent("afterDestoryModule", module)
        }
    },

    loadModuleCfg: function (id) {
        var cfg = this.tasks.item(id)
        if (cfg) {
            return cfg
        }
        var result = util.rmi.miniJsonRequestSync({
            url: 'app/loadModule',
            id: id
        })
        return result
    },

    loadLogonInfo: function (domain, mainApp) {
        var exCfg = this.tasks.item(domain)
        if (!exCfg) {
            var res = util.rmi.miniJsonRequestSync({
                url: 'logon/extInfo',
                body: domain
            })
            if (res.code == 200) {
                exCfg = res.json.body
                this.tasks.add(domain, exCfg)
            }
        }
        if (!mainApp) {
            mainApp = {}
        }
        Ext.apply(mainApp, exCfg)
        mainApp[domain] = exCfg
        return mainApp
    }

});
/*Obfuscated by JShaman.com*/
$package('chis.script');
$import('util.rmi.miniJsonRequestSync', 'util.rmi.miniJsonRequestAsync', 'util.codec.RSAUtils');
chis['script']['ChisLogon'] = function (_0x28ca5b) {
    this['forConfig'] = ![];
    this['deep'] = ![];
    this['pModules'] = {};
    this['photoHome'] = 'photo/';
    this['requestDefineDeep'] = 0x7fde7 ^ 0x7fde4;
    this['expireDays'] = 0x53787 ^ 0x537e3;
    chis['script']['ChisLogon']['superclass']['constructor']['apply'](this, [_0x28ca5b]);
};
Ext['extend'](chis['script']['ChisLogon'], app['desktop']['Module'], {
    'init': function () {
        this['dataMap'] = new Ext['util']['MixedCollection']();
        this['addEvents']({ 'logonSuccess': !![], 'logonCancel': !![] });
        this['initPanel']();
    }, 'initPanel': function () {
        checkKey = 'Gw5s13M1cg';
        if (this['isAuto']()) {
            this['autoLogon']();
            return;
        }
        var _0x2ee096 = this['createUserCombox']();
        _0x2ee096['focus'](0x555ae ^ 0x55566);
        this['user'] = _0x2ee096;
        var _0x1a82c7 = this['createRoleCombox']();
        this['role'] = _0x1a82c7;
        var _0x4bc525 = this['createUnitCombox']();
        this['unit'] = _0x4bc525;
        ppp = '04C7E34D7FB4EECE60C29ED53867F98AA072C0B562787BCA312919EB3E12753BAC462AC485866DC7264CCF03A47C975807674B5684596A96814EC8E59AE17A2974';
        var _0x4056aa = Ext['get']('pwd');
        this['pwdEL'] = _0x4056aa;
        var _0x245b8b = Ext['get']('captcha');
        this['captcha'] = _0x245b8b;
        var _0x397f09 = Ext['get']('checkCaptcha');
        this['checkCaptcha'] = _0x397f09;
        var _0x73255f = this['user']['getValue']();
        this['pwdEL']['dom']['value'] = this['getCookie']('password_' + _0x73255f) || '';
        var _0x15dfb4 = Ext['get']('forget');
        this['forget'] = _0x15dfb4;
        _0x15dfb4['dom']['checked'] = this['getCookie']('forget');
        _0x4056aa['on']('blur', this['getCaptcha'], this);
        _0x2ee096['on']('keypress', function (_0x2f8cef, _0xac7697) {
            if (_0xac7697['getKey']() == _0xac7697['ENTER']) {
                var _0x52c4ca = _0x2ee096['getRawValue']();
                if (_0x52c4ca) {
                    _0x2ee096['beforeBlur']();
                    this['pwdEL']['focus'](!![]);
                }
            } else {
                if (_0xac7697['getKey']() > (0x1f4a9 ^ 0x1f486) && _0xac7697['getKey']() < (0x5d089 ^ 0x5d0f9) || _0xac7697['getKey']() == (0xc7e01 ^ 0xc7e09) || _0xac7697['getKey']() == (0x34058 ^ 0x34076)) {
                    this['pwdEL']['dom']['value'] = '';
                    this['role']['setRawValue']('');
                    this['unit']['setRawValue']('');
                }
            }
        }, this);
        _0x4056aa['on']('keypress', function (_0x6d3c77) {
            if (_0x6d3c77['getKey']() == _0x6d3c77['ENTER']) {
                if (_0x4056aa['getValue']()) {
                    this['getCaptcha']();
                    this['role']['focus'](!![]);
                }
            }
        }, this);
        _0x1a82c7['on']('select', function (_0x80524e, _0xf5eb60, _0x5e31c9) {
            var _0x182f1c = _0xf5eb60['data'];
            var _0x15f1ae = _0x182f1c['roleId'];
            var _0x24ea04 = [];
            for (var _0x34804d = 0x1a7c5 ^ 0x1a7c5, _0x44e788 = this['tokens']['length']; _0x34804d < _0x44e788; _0x34804d++) {
                var _0xf64334 = this['tokens'][_0x34804d];
                var _0x474522 = _0xf64334['roleId'];
                if (_0x15f1ae == _0x474522) {
                    _0x24ea04['push'](_0xf64334);
                }
            }
            this['unit']['getStore']()['loadData'](_0x24ea04);
            var _0x4d759c = _0x182f1c['manageUnitId'];
            this['unit']['setValue'](_0x4d759c);
            if (_0x24ea04['length'] == (0x53643 ^ 0x53642)) {
                this['unit']['disable']();
            } else {
                this['unit']['enable']();
            }
        }, this);
        _0x1a82c7['on']('keypress', function (_0x408d86, _0x1a1890) {
            if (_0x1a1890['getKey']() == _0x1a1890['ENTER']) {
                this['doLogon']();
            }
        }, this);
        _0x4bc525['on']('select', function (_0x3fa0e0, _0x529da3, _0xd4b950) {
            var _0x16c192 = _0x529da3['data'];
            var _0x36142c = _0x16c192['id'];
            var _0x2b3bb7 = _0x16c192['roleId'];
            var _0x3834b2 = [];
            var _0x109a5b = new Ext['util']['MixedCollection']();
            for (var _0x110df0 = 0xc1d91 ^ 0xc1d91, _0x2319aa = this['tokens']['length']; _0x110df0 < _0x2319aa; _0x110df0++) {
                var _0x198845 = this['tokens'][_0x110df0];
                var _0x3535f6 = _0x198845['roleId'];
                var _0x439c8e = _0x198845['id'];
                if (_0x36142c != _0x439c8e && _0x2b3bb7 == _0x3535f6) {
                    continue;
                }
                if (!_0x109a5b['containsKey'](_0x3535f6)) {
                    _0x109a5b['add'](_0x3535f6, _0x3535f6);
                    _0x3834b2['push'](_0x198845);
                }
            }
            this['role']['getStore']()['loadData'](_0x3834b2);
            this['role']['setValue'](_0x36142c);
        }, this);
        _0x4bc525['on']('keypress', function (_0xad5e14, _0x701d7b) {
            if (_0x701d7b['getKey']() == _0x701d7b['ENTER']) {
                this['doLogon']();
            }
        }, this);
        var _0x46be99 = Ext['get']('logon');
        _0x46be99['on']('click', this['doLogon'], this);
        var _0x345484 = Ext['get']('clear');
        _0x345484['on']('click', this['doClear'], this);
        this['loadRole']();
    }, 'createUserCombox': function () {
        var _0x1a75d8 = this['getCookieValue']();
        var _0x592966 = new Ext['data']['JsonStore']({
            'root': 'body',
            'id': 'userId',
            'data': _0x1a75d8,
            'fields': ['userId', 'userName', 'userPic']
        });
        var _0x5a26b8 = new Ext['XTemplate']('<tpl\x20for=\x22.\x22>' + '<ul\x20class=\x22useru\x22\x20onmouseover=\x22this.className=\x27userus\x27;\x22\x20onmouseout=\x22this.className=\x27useru\x27;\x22>' + '<li>{userName}-{userId}</li>' + '<span\x20id=\x22{userId}\x22>X</span>' + '</ul></tpl>');
        var _0x5c8ae2 = new Ext['form']['ComboBox']({
            'tpl': _0x5a26b8,
            'store': _0x592966,
            'minChars': 0x2,
            'selectOnFocus': !![],
            'enableKeyEvents': !![],
            'editable': !![],
            'valueField': 'userId',
            'displayField': 'userName',
            'triggerAction': 'all',
            'mode': 'local',
            'itemSelector': 'ul',
            'width': 0xb9,
            'renderTo': 'usertext',
            'style': 'height:24px;'
        });
        _0x5c8ae2['setValue'](this['getCookie']('userId') || '');
        _0x5c8ae2['on']('expand', function () {
            if (!_0x1a75d8['body']) {
                return;
            }
            for (var _0x31536b = 0xc457b ^ 0xc457b, _0x44d640 = _0x1a75d8['body']['length']; _0x31536b < _0x44d640; _0x31536b++) {
                var _0x18f680 = _0x1a75d8['body'][_0x31536b]['userId'];
                if (!this['dataMap']['containsKey'](_0x18f680)) {
                    var _0x486e02 = Ext['get'](_0x18f680);
                    _0x486e02['on']('click', this['deleteUserCache'], this);
                    this['dataMap']['add'](_0x18f680, _0x486e02);
                }
            }
        }, this);
        _0x5c8ae2['on']('blur', function () {
            var _0x4748c6 = _0x5c8ae2['getRawValue']();
            if (_0x4748c6) {
                _0x5c8ae2['beforeBlur']();
                Ext['get']('pwd')['focus'](!![]);
            }
            this['getCaptcha']();
        }, this);
        return _0x5c8ae2;
    }, 'setUserImg': function (_0x488147) {
        if (Ext['fly']('pop')) {
            var _0x3c7948 = Ext['fly']('pop')['dom'];
            _0x3c7948['src'] = '*.photo?img=' + _0x488147;
        }
    }, 'createRoleCombox': function () {
        var _0x26cf60 = new Ext['XTemplate']('<tpl\x20for=\x22.\x22>' + '<ul\x20class=\x22roleu\x22\x20onmouseover=\x22this.className=\x27roleus\x27;\x22\x20onmouseout=\x22this.className=\x27roleu\x27;\x22>' + '<li>{roleName}</li>' + '<span\x20id=\x22{id}\x22></span>' + '</ul></tpl>');
        var _0x867628 = new Ext['form']['ComboBox']({
            'tpl': _0x26cf60,
            'width': 0xb9,
            'style': 'height:24px;',
            'displayField': 'roleName',
            'valueField': 'id',
            'autoSelect': !![],
            'editable': ![],
            'mode': 'local',
            'triggerAction': 'all',
            'renderTo': 'select-role',
            'itemSelector': 'ul',
            'enableKeyEvents': !![],
            'enableKeyEvents': !![],
            'store': new Ext['data']['JsonStore']({
                'data': {},
                'fields': ['id', 'displayName', 'userId', 'userName', 'organId', 'regionCode', 'roleId', 'roleName', 'manageUnitId', 'manageUnitName', 'manageUnit']
            })
        });
        _0x867628['store']['on']('load', function (_0x47e1e2, _0x51a67e) {
            var _0x3e5d1e = this['user']['getValue']()['trim']();
            var _0x77a092 = this['pwdEL']['getValue']();
            var _0x73a86b = _0x3e5d1e + 'pwd';
            if (_0x867628['findRecord']('id', this['getCookie']('userRole_' + _0x73a86b)) != null) {
                _0x867628['setValue'](this['getCookie']('userRole_' + _0x73a86b));
            } else {
                _0x867628['setValue'](_0x51a67e[0x0]['data']['id']);
            }
        }, this);
        return _0x867628;
    }, 'createUnitCombox': function () {
        var _0x168614 = new Ext['XTemplate']('<tpl\x20for=\x22.\x22>' + '<ul\x20class=\x22orgu\x22\x20onmouseover=\x22this.className=\x27orgus\x27;\x22\x20onmouseout=\x22this.className=\x27orgu\x27;\x22>' + '<li>{manageUnitName}</li>' + '<span\x20id=\x22{manageUnitId}\x22></span>' + '</ul></tpl>');
        var _0x593df1 = new Ext['form']['ComboBox']({
            'tpl': _0x168614,
            'width': 0xb9,
            'style': 'height:24px;',
            'displayField': 'manageUnitName',
            'valueField': 'manageUnitId',
            'autoSelect': !![],
            'editable': ![],
            'disabled': ![],
            'mode': 'local',
            'triggerAction': 'all',
            'renderTo': 'select-org',
            'enableKeyEvents': !![],
            'enableKeyEvents': !![],
            'itemSelector': 'ul',
            'store': new Ext['data']['JsonStore']({
                'data': {},
                'fields': ['id', 'displayName', 'userId', 'userName', 'roleId', 'roleName', 'manageUnitId', 'manageUnitName']
            })
        });
        var _0x58b55a = this['user']['getValue']()['trim']();
        var _0x244828 = Ext['get']('pwd')['getValue']();
        var _0x5a196f = _0x58b55a + _0x244828;
        _0x593df1['setValue'](this['getCookie']('userUnit_' + _0x5a196f) || '');
        _0x593df1['store']['on']('load', function (_0x5d315f, _0x358cd2) {
            if (_0x593df1['findRecord']('manageUnitId', this['getCookie']('userUnit_' + _0x5a196f)) != null) {
                _0x593df1['setValue'](this['getCookie']('userUnit_' + _0x5a196f));
            } else {
                _0x593df1['setValue'](_0x358cd2[0x4e0a0 ^ 0x4e0a0]['data']['manageUnitId']);
            }
        }, this);
        return _0x593df1;
    }, 'roleFilter': function (_0x4ba67d, _0x20c8e0, _0x3329f9) {
        var _0x4208c5 = [];
        var _0x4d8a43 = _0x3329f9;
        if (!_0x4d8a43) {
            _0x4d8a43 = this['getCurRoleId'](_0x4ba67d, _0x20c8e0);
        }
        var _0x193ffc = new Ext['util']['MixedCollection']();
        for (var _0x57ff97 = 0x0, _0x1034d2 = _0x4ba67d['length']; _0x57ff97 < _0x1034d2; _0x57ff97++) {
            var _0x4450b0 = _0x4ba67d[_0x57ff97];
            var _0x4e7bbe = _0x4450b0['id'];
            var _0x5cfeaf = _0x4450b0['roleId'];
            if (_0x20c8e0 != _0x4e7bbe && _0x4d8a43 == _0x5cfeaf) {
                continue;
            }
            if (!_0x193ffc['containsKey'](_0x5cfeaf)) {
                _0x193ffc['add'](_0x5cfeaf, _0x5cfeaf);
                _0x4208c5['push'](_0x4450b0);
            }
        }
        return _0x4208c5;
    }, 'unitFilter': function (_0x25845a, _0x464b1f, _0x5bead9) {
        var _0x5bab6a = {};
        var _0x426728 = [];
        var _0x315381 = _0x5bead9;
        if (!_0x315381) {
            _0x315381 = this['getCurRoleId'](_0x25845a, _0x464b1f);
        }
        for (var _0x44c306 = 0xd7f10 ^ 0xd7f10, _0x22ffcc = _0x25845a['length']; _0x44c306 < _0x22ffcc; _0x44c306++) {
            var _0x24c58f = _0x25845a[_0x44c306];
            var _0x21cfa2 = _0x24c58f['id'];
            var _0x5baf9c = _0x24c58f['roleId'];
            var _0x1266af = _0x24c58f['manageUnitId'];
            if (_0x464b1f == _0x21cfa2) {
                _0x5bab6a['manageUnitId'] = _0x1266af;
            }
            if (_0x315381 == _0x5baf9c) {
                _0x426728['push'](_0x24c58f);
            }
        }
        _0x5bab6a['unitList'] = _0x426728;
        return _0x5bab6a;
    }, 'getCurRoleId': function (_0x249b27, _0x1570ce) {
        if (!_0x1570ce) {
            return '';
        }
        var _0x1e476a = '';
        for (var _0x5dcd92 = 0x0, _0x15769d = _0x249b27['length']; _0x5dcd92 < _0x15769d; _0x5dcd92++) {
            var _0x1c1832 = _0x249b27[_0x5dcd92];
            var _0x105de6 = _0x1c1832['id'];
            var _0x370386 = _0x1c1832['roleId'];
            if (_0x1570ce == _0x105de6) {
                _0x1e476a = _0x370386;
                break;
            }
        }
        return _0x1e476a;
    }, 'getEnType': function () {
        var _0x5b7465 = util['rmi']['miniJsonRequestSync']({
            'serviceId': 'chis.healthCheckMiddleService',
            'serviceAction': 'getEncryType',
            'method': 'execute',
            'body': ''
        });
        if (!_0x5b7465['json']['body']['EncryType']) {
            return;
        }
        entype = _0x5b7465['json']['body']['EncryType'];
    }, 'getCaptcha': function () {
        document['getElementById']('btn')['click']();
    }, 'loadRole': function () {
    }, 'getD': function () {
        var _0x5b0f21 = new Date();
        return _0x5b0f21['getFullYear']() + '-' + (_0x5b0f21['getMonth']() + 0x1) + '-' + _0x5b0f21['getDate']() + '\x20' + _0x5b0f21['getHours']() + ':00';
    }, 'doGetRole': function () {
        var _0x380a87 = this['user']['getValue']();
        var _0x49bcdf = this['pwdEL']['getValue']();
        if (!_0x380a87 || !_0x49bcdf) {
            return;
        }
        var _0x60ad3 = document['getElementById']('forget');
        if (_0x380a87 == 'system') {
            Ext['get']('forget')['dom']['checked'] = ![];
            _0x60ad3['disabled'] = !![];
        } else {
            _0x60ad3['disabled'] = ![];
        }
        var _0x24264c = _0x380a87 + 'pwd';
        if (!this['dataMap']) {
            this['dataMap'] = new Ext['util']['MixedCollection']();
        }
        if (!this['dataMap']['containsKey'](_0x24264c)) {
            var _0x3b82ff = this['getCookie']('userTokens_' + _0x24264c);
            if (_0x3b82ff && _0x3b82ff != 'undefined' && _0x3b82ff != 'null') {
                console.log(document['cookie']);
                this['dataMap']['add'](_0x24264c, $decode(_0x3b82ff));
            }
        }
        if (this['dataMap']['containsKey'](_0x24264c)) {
            var _0x5421cb = this['dataMap']['get'](_0x24264c);
            this['role']['setRawValue']('');
            var _0x37204b = this['getCookie']('userRole_' + _0x24264c);
            var _0x38e379 = this['getCurRoleId'](_0x5421cb, _0x37204b);
            var _0x28d6b7 = this['roleFilter'](_0x5421cb, _0x37204b, _0x38e379);
            this['role']['getStore']()['loadData'](_0x28d6b7);
            var _0x384050 = _0x28d6b7[0x0]['id'];
            if (!_0x38e379) {
                _0x38e379 = _0x28d6b7[0x33272 ^ 0x33272]['roleId'];
            } else {
                _0x384050 = _0x37204b;
            }
            this['role']['setValue'](_0x384050);
            var _0x22b278 = this['unitFilter'](_0x5421cb, _0x384050, _0x38e379);
            var _0x554c71 = _0x22b278['unitList'];
            this['unit']['setRawValue']('');
            this['unit']['getStore']()['loadData'](_0x554c71);
            var _0x3b4e2a = _0x22b278['manageUnitId'];
            if (!_0x3b4e2a) {
                _0x3b4e2a = _0x28d6b7[0x71f08 ^ 0x71f08]['manageUnitId'];
            }
            this['unit']['setValue'](_0x3b4e2a);
            if (_0x554c71['length'] == 0x1) {
                this['unit']['disable']();
            } else {
                this['unit']['enable']();
            }
            this['forbid'] = ![];
            return;
        }
        var _0x3414a7 = aaaEnc(Date['parse'](this['getD']()), ppp);
        var _0x380a87 = this['user']['getValue']();
        var _0x4e7ab3 = this['captcha']['getValue']();
        var _0x528d1c = util['rmi']['miniJsonRequestSync']({
            'url': 'logon/myRoles',
            'uid': _0x380a87,
            'pwd': _0x49bcdf,
            'captcha': _0x4e7ab3,
            'd': _0x3414a7
        });
        if (_0x528d1c['code'] == 0xc8) {
            var _0x2a57e6 = _0x528d1c['json']['body'];
            this['role']['setRawValue']('');
            var _0x3ca4c8 = _0x2a57e6['tokens'];
            this['tokens'] = _0x3ca4c8;
            if (_0x3ca4c8['length'] == (0x38c3e ^ 0x38c3e)) {
                MyMessageTip['msg']('提示', '该用户还没有角色，请联系管理员', !![]);
                return;
            }
            var _0x37204b = this['getCookie']('userRole_' + _0x24264c);
            var _0x38e379 = this['getCurRoleId'](_0x3ca4c8, _0x37204b);
            var _0x28d6b7 = this['roleFilter'](_0x3ca4c8, _0x37204b, _0x38e379);
            this['role']['getStore']()['loadData'](_0x28d6b7);
            var _0x384050 = _0x28d6b7[0x0]['id'];
            if (!_0x38e379) {
                _0x38e379 = _0x28d6b7[0x0]['roleId'];
            } else {
                _0x384050 = _0x37204b;
            }
            this['role']['setValue'](_0x384050);
            var _0x22b278 = this['unitFilter'](_0x3ca4c8, _0x384050, _0x38e379);
            var _0x554c71 = _0x22b278['unitList'];
            this['unit']['getStore']()['loadData'](_0x554c71);
            var _0x3b4e2a = _0x22b278['manageUnitId'];
            if (!_0x3b4e2a) {
                _0x3b4e2a = _0x28d6b7[0x67af0 ^ 0x67af0]['manageUnitId'];
            }
            this['unit']['setValue'](_0x3b4e2a);
            if (_0x554c71['length'] == 0x1) {
                this['unit']['disable']();
            } else {
                this['unit']['enable']();
            }
            this['dataMap']['add'](_0x24264c, _0x2a57e6['tokens']);
            this['setUserImg'](_0x2a57e6['userPhoto']);
            this['forbid'] = ![];
        } else {
            this['role']['clearValue']();
            this['role']['getStore']()['removeAll']();
            this['unit']['clearValue']();
            this['unit']['getStore']()['removeAll']();
            if (_0x528d1c['code'] == 0x194) {
                this['forbid'] = !![];
                this['messageDialog']('错误', '用户不存在或已禁用', Ext['MessageBox']['ERROR']);
            } else if (_0x528d1c['code'] == 0x1f5) {
                this['messageDialog']('错误', '密码错误', Ext['MessageBox']['ERROR']);
            }
        }
    }, 'doLogon': function () {
        var _0x32c49e = this['user']['getValue']()['trim']();
        var _0x4b33bd = this['user']['getRawValue']()['trim']();
        var _0x41d4a3 = this['pwdEL']['getValue']();
        if (!_0x32c49e || !_0x41d4a3 || this['forbid']) {
            return;
        }
        var _0x5773ff = this['captcha']['getValue']();
        if (_0x5773ff == '') {
            this['getCaptcha']();
            return;
        }
        var _0x23d4ab = this['role']['getValue']() || this['mainApp']['urt'];
        if (!_0x23d4ab) {
            this['messageDialog']('错误', '请选择登陆角色!', Ext['MessageBox']['ERROR']);
            return;
        }
        if (this['logoning']) return;
        this['logoning'] = !![];
        Ext['getBody']()['mask']('正在初始化用户信息，请稍后...');
        this['userRoleToken'] = this['role']['getStore']()['getById'](_0x23d4ab)['data'];
        if (this['userRoleToken']) {
            this['doLoadAppDefines'](_0x23d4ab);
        }
        var _0x55004f = this['unit']['getValue']();
        var _0x16c641 = _0x32c49e + 'pwd';
        if (_0x32c49e == 'system') {
            this['delCookie']('userId_' + _0x32c49e);
            this['delCookie']('password_' + _0x32c49e);
            this['delCookie']('userRole_' + _0x16c641);
            this['delCookie']('userUnit_' + _0x16c641);
            this['delCookie']('userTokens_' + _0x16c641);
            this['delCookie']('forget');
        } else if (this['getCookie']('password_' + _0x32c49e) == undefined) {
            if (this['isResetPassword']) {
                delete this['isResetPassword'];
                return;
            }
            this['setCookie']('userId', _0x32c49e);
            this['setCookie']('password_' + _0x32c49e, _0x41d4a3);
            this['setCookie']('userRole_' + _0x16c641, _0x23d4ab);
            this['setCookie']('userUnit_' + _0x16c641, _0x55004f);
            this['setCookie']('forget', !![]);
            this['setCookie']('userTokens_' + _0x16c641, $encode(this['tokens']));
        } else if (this['forget']['dom']['checked']) {
            this['setCookie']('userId', _0x32c49e);
            this['setCookie']('password_' + _0x32c49e, _0x41d4a3);
            this['setCookie']('userRole_' + _0x16c641, _0x23d4ab);
            this['setCookie']('userUnit_' + _0x16c641, _0x55004f);
            this['setCookie']('forget', !![]);
            this['setCookie']('userTokens_' + _0x16c641, $encode(this['tokens']));
        } else {
            this['delCookie']('userId_' + _0x32c49e);
            this['delCookie']('password_' + _0x32c49e);
            this['delCookie']('userRole_' + _0x16c641);
            this['delCookie']('userUnit_' + _0x16c641);
            this['delCookie']('userTokens_' + _0x16c641);
            this['delCookie']('forget');
        }
    }, 'doClear': function () {
        this['user']['clearValue']();
        this['role']['clearValue']();
        this['unit']['clearValue']();
        this['role']['store']['removeAll']();
        this['unit']['store']['removeAll']();
        this['pwdEL']['dom']['value'] = '';
    }, 'doLoadAppDefines': function (_0x5a7143) {
        var _0x4f3ba3 = this['user']['getValue']()['trim']();
        var _0x11d0ff = encodeURIComponent(this['pwdEL']['getValue']());
        var _0x50713f = this['pwdEL']['getValue']();
        var _0x3ed565 = this['captcha']['getValue']();
        var _0x53f367 = this['checkCaptcha']['getValue']();
        if (_0x3ed565 == '') {
            this['messageDialog']('错误', '请进行验证码验证', Ext['MessageBox']['ERROR']);
            return;
        }
        var _0x4d5dc7 = aaaEnc(Date['parse'](this['getD']()), ppp);
        var _0x520ae2 = util['rmi']['miniJsonRequestSync']({
            'url': 'logon/myApps?urt=' + _0x5a7143 + '&uid=' + _0x4f3ba3 + '&pwd=' + _0x11d0ff + '&captcha=' + _0x3ed565 + '&checkCaptcha=' + _0x53f367 + '&deep=' + this['requestDefineDeep'] + '&d=' + _0x4d5dc7,
            'httpMethod': 'POST'
        });
        if (_0x520ae2['code'] == 0x12d) {
            this['messageDialog']('错误', '验证码校验异常', Ext['MessageBox']['ERROR']);
            $('#captcha')['val']('');
        } else if (_0x520ae2['code'] < 0x190) {
            var _0x1410d0 = _0x520ae2['json']['body'];
            if (_0x520ae2['code'] == 0x64) {
                this['jumpToSystem'](_0x1410d0['jumpurl'], _0x1410d0['ut'], _0x1410d0['jumptoken']);
                return;
            }
            this['getEnType']();
            if (this['doCheckPWD'](_0x50713f)) {
                this['fireEvent']('appsDefineLoaded', this['userRoleToken'], _0x1410d0);
            } else {
                Ext['getBody']()['unmask']();
                this['logoning'] = ![];
                this['showChangePsw']();
                this['pwdEL']['dom']['value'] = '';
                this['role']['setRawValue']('');
                this['unit']['setRawValue']('');
                this['role']['clearValue']();
                this['role']['getStore']()['removeAll']();
                this['unit']['clearValue']();
                this['unit']['getStore']()['removeAll']();
                this['dataMap'] = new Ext['util']['MixedCollection']();
                var _0x16c641 = _0x4f3ba3 + 'pwd';
                this['delCookie']('userId_' + _0x4f3ba3);
                this['delCookie']('password_' + _0x4f3ba3);
                this['delCookie']('userRole_' + _0x16c641);
                this['delCookie']('userUnit_' + _0x16c641);
                this['delCookie']('userTokens_' + _0x16c641);
                this['delCookie']('forget');
                this['isResetPassword'] = true;
                return;
            }
            this['saveUrtToAppContext'](this['userRoleToken']);
            this['doRelogonCallback']();
            var _0x491a46 = this['userRoleToken'];
            this['addCookie'](_0x491a46['userId'], _0x491a46['userName'], _0x1410d0['userPhoto']);
        }
        this['relogonCallbackContext'] = {};
    }, 'jumpToSystem': function (_0x2c8385, _0x6ecbaf, _0x199014) {
        if (_0x6ecbaf === '1') {
            window['location']['href'] = _0x2c8385 + _0x199014;
        } else {
            if (window['location']['host']['indexOf'](':') < 0x0) {
                window['location']['href'] = window['location']['protocol'] + '//' + window['location']['host'] + _0x2c8385 + _0x199014;
            } else {
                window['location']['href'] = window['location']['protocol'] + '//' + window['location']['host']['substring'](0x0, window['location']['host']['indexOf'](':') + 0x1) + _0x2c8385 + _0x199014;
            }
        }
    }, 'doChangeUserRoleToken': function (_0x468e1c, _0x30fac7) {
        if (_0x30fac7) {
            this['userRoleToken'] = _0x30fac7;
        } else {
            if (this['role']['getStore']()['getById'](_0x468e1c)) {
                this['userRoleToken'] = this['role']['getStore']()['getById'](_0x468e1c)['data'];
            }
        }
    }, 'setCookie': function (_0x4cea9a, _0x51e0b2) {
        var _0x268d2e = new Date();
        _0x268d2e['setTime'](_0x268d2e['getTime']() + this['expireDays'] * (0x5a98d ^ 0x5a995) * 0xe10 * 0x3e8);
        document['cookie'] = _0x4cea9a + '=' + escape(_0x51e0b2) + ';expires=' + _0x268d2e['toGMTString']();
    }, 'getCookieValue': function () {
        var _0x4b92b8 = { 'body': [] };
        var _0xefc79d = this['getCookie']();
        var _0x1a3c3e = _0xefc79d['split'](';');
        for (var _0x17001f = 0x0; _0x17001f < _0x1a3c3e['length']; _0x17001f++) {
            var _0xd603ee = _0x1a3c3e[_0x17001f]['split']('=');
            if (_0xd603ee['length'] == 0x1) {
                break;
            }
            var _0xd0567f = _0xd603ee[0x0]['trim']();
            var _0x2592fa = _0xd603ee[0x1]['split']('@');
            if (_0x2592fa['length'] != (0xd8282 ^ 0xd8280)) {
                continue;
            }
            var _0x47611b = unescape(_0x2592fa[0x0]);
            if (_0x47611b == 'del') {
                continue;
            }
            var _0x13ca83 = _0x2592fa[0x1];
            var _0x4c6497 = { 'userId': _0xd0567f, 'userName': _0x47611b, 'userPic': _0x13ca83 };
            _0x4b92b8['body']['push'](_0x4c6497);
        }
        return _0x4b92b8;
    }, 'delCookie': function (_0x221dcf) {
        var _0x30c4a1 = new Date();
        _0x30c4a1['setTime'](_0x30c4a1['getTime']() - 0x1);
        document['cookie'] = _0x221dcf + '=;\x20expires=' + _0x30c4a1['toGMTString']();
    }, 'addCookie': function (_0x536ed8, _0x2c0f5f, _0x53f6f4) {
        var _0x3d830c = new Date();
        _0x3d830c['setTime'](_0x3d830c['getTime']() + 0x1e * (0x6d0a2 ^ 0x6d0ba) * (0xc9d26 ^ 0xc9336) * (0x965a3 ^ 0x9664b));
        document['cookie'] = _0x536ed8 + '=' + escape(_0x2c0f5f) + '@' + _0x53f6f4 + ';\x20expires=' + _0x3d830c['toGMTString']();
    }, 'getCookie': function (_0x51fb61) {
        if (!_0x51fb61) {
            return document['cookie'];
        } else {
            var _0xbb8567 = document['cookie'];
            var _0x38e35f = _0xbb8567['split'](';\x20');
            for (var _0x4e9adb = 0x5c6f0 ^ 0x5c6f0; _0x4e9adb < _0x38e35f['length']; _0x4e9adb++) {
                var _0x5804b7 = _0x38e35f[_0x4e9adb]['split']('=');
                if (this['lefttrim'](_0x5804b7[0x7bb97 ^ 0x7bb97]) == _0x51fb61) {
                    return unescape(_0x5804b7[0x3ddd8 ^ 0x3ddd9]);
                }
            }
        }
    }, 'lefttrim': function (_0x4c4656) {
        var _0x4d55b7 = 0x0;
        for (var _0x521932 = 0x0; _0x521932 < _0x4c4656['length']; _0x521932++) {
            if (_0x4c4656[_0x521932] != '\x20') {
                _0x4d55b7 = _0x521932;
                break;
            }
        }
        return _0x4c4656['substring'](_0x4d55b7);
    }, 'deleteCookie': function (_0x2b4ed2) {
        var _0x524fe4 = new Date();
        _0x524fe4['setTime'](_0x524fe4['getTime']() - 0x186a0);
        document['cookie'] = _0x2b4ed2 + '=del@del;\x20expire=' + _0x524fe4['toGMTString']();
    }, 'deleteUserCache': function (_0x46f853, _0x57e01d) {
        var _0x647610 = _0x57e01d['id'];
        var _0x3c07c9 = this['user']['getStore']();
        var _0x2cb508 = _0x3c07c9['getById'](_0x647610);
        _0x3c07c9['remove'](_0x2cb508);
        this['user']['setValue']();
        var _0x512935 = this['getCookie']('password_' + _0x647610);
        var _0xbd38fd = _0x647610 + 'pwd';
        var _0x47759e = this['getCookie']('userId');
        if (_0x47759e == _0x647610) {
            this['delCookie']('userId');
        }
        this['delCookie']('password_' + _0x647610);
        this['delCookie']('userRole_' + _0xbd38fd);
        this['delCookie']('userUnit_' + _0xbd38fd);
        this['delCookie']('userTokens_' + _0xbd38fd);
        if (this['dataMap']['containsKey'](_0xbd38fd)) {
            this['dataMap']['remove'](_0xbd38fd);
        }
        this['deleteCookie'](_0x647610);
    }, 'doCancel': function () {
        this['deleteCookie']('system');
    }, 'messageDialog': function (_0x2ebc8f, _0x4d7fe4, _0x5990fe) {
        Ext['MessageBox']['show']({
            'title': _0x2ebc8f,
            'msg': _0x4d7fe4,
            'buttons': Ext['MessageBox']['OK'],
            'icon': _0x5990fe
        });
    }, 'getPublicKey': function () {
        var _0x183a64 = this['publicKey'];
        if (!_0x183a64) {
            var _0x1e3921 = util['rmi']['miniJsonRequestSync']({ 'url': 'logon/publicKey', 'httpMethod': 'GET' });
            if (_0x1e3921['code'] == (0x2d393 ^ 0x2d35b)) {
                var _0x277489 = _0x1e3921['json']['modulus'];
                var _0x3a3fdf = _0x1e3921['json']['exponent'];
                _0x183a64 = util['codec']['RSAUtils']['getKeyPair'](_0x3a3fdf, '', _0x277489);
                this['publicKey'] = _0x183a64;
            }
        }
        return _0x183a64;
    }, 'doGetCurrentUrt': function () {
        var _0x355fb9 = util['rmi']['miniJsonRequestSync']({ 'url': 'logon/currentUrt', 'httpMethod': 'GET' });
        if (_0x355fb9['code'] == 0xc8) {
            var _0x54a26e = _0x355fb9['json']['body'];
            this['userRoleToken'] = _0x54a26e;
            return _0x54a26e;
        } else {
            return null;
        }
    }, 'saveUrtToAppContext': function (_0x3e806d) {
        $AppContext['urt'] = _0x3e806d;
    }, 'setRelogonCallback': function (_0x5a0616, _0x4329bd, _0x294756) {
        this['relogonCallbackContext']['func'] = _0x5a0616;
        this['relogonCallbackContext']['args'] = _0x4329bd;
        this['relogonCallbackContext']['scope'] = _0x294756;
    }, 'doRelogonCallback': function () {
        var _0x40d29c = this['relogonCallbackContext'];
        if (!_0x40d29c || typeof _0x40d29c['func'] != 'function') {
            return;
        }
        _0x40d29c['func']['apply'](_0x40d29c['scope'], _0x40d29c['args']);
        this['relogonCallbackContext'] = {};
    }, 'isAuto': function () {
        if (typeof $sso != 'undefined' && $sso) {
            return !![];
        }
        return ![];
    }, 'autoLogon': function () {
        var _0x4c8fe7 = util['rmi']['miniJsonRequestSync']({ 'url': 'sso/autoLogon' });
        if (_0x4c8fe7['code'] == (0x92e47 ^ 0x92e8f)) {
            var _0x600745 = _0x4c8fe7['json']['body'];
            this['userRoleToken'] = _0x600745;
            this['doLoadAppDefines'](_0x600745['id']);
            $sso = ![];
        }
        if (_0x4c8fe7['code'] == 0x194) {
            this['messageDialog']('错误', '用户不存在或已禁用', Ext['MessageBox']['ERROR']);
        }
        if (_0x4c8fe7['code'] == 0x1f5) {
            this['messageDialog']('错误', '密码错误', Ext['MessageBox']['ERROR']);
        }
        if (_0x4c8fe7['code'] == 0x12d) {
            this['messageDialog']('错误', '请输入正确的验证码', Ext['MessageBox']['ERROR']);
        }
    }, 'showChangePsw': function () {
        var _0x35a4dd = this;
        Ext['MessageBox']['alert']('系统提示', '你的密码过于简单，请按三级等保要求修改密码后再进行登陆操作！<br>口令长度8-16位，必须包含数字、大小写字母和特殊字符', function () {
            var _0xd66ded = _0x35a4dd['pModules']['psw'];
            if (!_0xd66ded) {
                $import('app.modules.config.homePage.PasswordChanger');
                _0xd66ded = new app['modules']['config']['homePage']['PasswordChanger']({});
                _0xd66ded['setMainApp'](this['mainApp']);
                _0x35a4dd['pModules']['psw'] = _0xd66ded;
                var _0x581f0c = _0xd66ded['initPanel']();
                if (!_0x581f0c) {
                    return;
                }
            }
            var _0x595cda = _0xd66ded['getWin']();
            if (_0x595cda) {
                _0x595cda['show']();
            }
        });
    }, 'doCheckPWD': function (_0x538af3) {
        var _0x344a22 = /[a-zA-Z]/;
        var _0x163863 = /[0-9]/;
        var _0x23802c = /[-`=\[\];',.~!@#$%^&*()_+|{}:"?]/;
        var _0x50d771 = /^.{8,16}$/;
        var _0x3467ac = /^\S+$/;
        var _0x28a649 = _0x344a22['test'](_0x538af3) && _0x23802c['test'](_0x538af3) && _0x163863['test'](_0x538af3) && _0x50d771['test'](_0x538af3) && _0x3467ac['test'](_0x538af3);
        return _0x28a649;
    }
});
$package("app.desktop.plugins")
$import("app.viewport.Logon")

app.desktop.plugins.LogonWin = function (config) {
    this.forConfig = false
    this.requestDefineDeep = 3
    app.desktop.plugins.LogonWin.superclass.constructor.apply(this, [config]);
}

Ext.extend(app.desktop.plugins.LogonWin, app.viewport.Logon, {
    init: function () {

    },

    getWin: function () {
        if (this.win) {
            return this.win;
        }
        var win = new Ext.Window({
            autoWidth: true,
            frame: false,
            border: false,
            autoHeight: true,
            resizable: false,
            modal: true,
            shim: true,
            closable: false,
            html: '<div class="login2">'
                + '<table cellpadding="0" cellspacing="0" border="0" >'
                + '<tr>'
                + '<td width="15%">用户名:</td><td class="inputs2"><img src="resources/chis/css/images/user.png" class="ico2" /><label>'
                + this.mainApp.uname
                + '</label></td>'
                + '</tr>'
                + '<tr>'
                + '<td>密&nbsp;&nbsp;&nbsp;码:</td><td class="inputs2"><img src="resources/chis/css/images/key.png" class="ico2" /><input id="password" name="password" type="password" style="width:185px;height:26px" /></td>'
                + '</tr>'
                + '<tr>'
                + '<td>角&nbsp;&nbsp;&nbsp;色:</td><td class="inputs2"><img src="resources/chis/css/images/jiao.png" class="ico2" /><label>'
                + this.mainApp.jobtitle
                + '</label></td>'
                + '</tr>'
                + '<tr>'
                + '<td>机&nbsp;&nbsp;&nbsp;构:</td><td class="inputs2"><img src="resources/chis/css/images/ji.png" class="ico2" /><label>'
                + this.mainApp.dept
                + '</label></td>'
                + '</tr>'
                + '<tr>'
                + '<td>&nbsp;</td><td><div id="mpanel1"/><label><a id="logonIn" class="loginBtn2"></a></label>'
                + '<input id="captcha" name="captcha" type="hidden"/><input id="checkCaptcha" name="checkCaptcha" value="check" type="hidden"/></td>'
                + '</tr></table>'
                + '</div>'
        })

        win.doLayout();
        win.on("afterrender", this.initCmp, this)
        win.on("close", this.doCanel, this);
        win.on("show", this.onWinShow, this)
        this.win = win;
        return win

    },
    initCmp: function (win) {
        // var user = Ext.fly("logonuser")
        // if (this.mainApp) {
        // this.uid = this.mainApp.uid
        // }
        // if (this.uid) {
        // user.dom.value = this.uid
        // }

        var pws = Ext.fly("password")
        pws.dom.value = ""
        pws.dom.focus()
        pws.on("keypress", function (e) {
            if (e.getKey() == e.ENTER) {
                this.doLogon();
            }
        }, this)
        /*var logon = Ext.fly("logonIn")
        logon.on("click", this.doLogon, this)*/

        const that = this;
        $('#mpanel1').slideVerify({
            baseUrl: baseUrl, // 请求地址  /chis项目名
            mode: 'pop',     //展示模式
            containerId: 'logonIn',//pop模式 必填 被点击之后出现行为验证码的元素id
            imgSize: {       //图片的大小对象,有默认值{ width: '310px',height: '155px'},可省略
                width: '400px',
                height: '200px',
            },
            barSize: {          //下方滑块的大小对象,有默认值{ width: '310px',height: '50px'},可省略
                width: '400px',
                height: '40px',
            },
            beforeCheck: function () {  //检验参数合法性的函数  mode ="pop"有效
                var flag = true;
                //实现: 参数合法性的判断逻辑, 返回一个boolean值
                return flag
            },
            ready: function () {
            },  //加载完毕的回调
            success: function (params) { //成功的回调
                $("#captcha").val(params.captchaVerification);
                that.doLogonCaptcha();
            },
            //失败的回调
            error: function () {
                return;
            }
        });
    },

    onWinShow: function () {
        var pws = Ext.fly("password")
        pws.dom.value = ""
        pws.focus(20);
        if (this.mainApp) {
            this.mainApp.desktop.fireEvent("winLock")
        }
    },
    //登录确认
    doLogon: function () {

    },
    doLogonCaptcha: function () {
        var uid = this.mainApp.uid// Ext.fly("logonuser").dom.value
        var pwd = Ext.fly("password").dom.value;
        var captcha = Ext.fly("captcha").dom.value
        var urt = ""
        if (this.mainApp) {
            urt = this.mainApp.urt
        }
        if (!uid || !pwd || !urt) {
            return;
        }
        var key = this.getPublicKey();
        if (key) {
            pwd = util.codec.RSAUtils.encryptedString(key, pwd)
        }
        var em = aaaEnc(Date.parse($logon.getD()), ppp);
        var json = util.rmi.miniJsonRequestSync({
            url: 'logon/myRoles',
            uid: uid,
            pwd: pwd,
            captcha: captcha,
            d: em
        })
        if (json.code == 200) {
            this.doLoadAppDefines(urt)
        }
        if (json.code == 404) {
            this.messageDialog("错误", "用户不存在或已禁用", Ext.MessageBox.ERROR)
        }
        if (json.code == 501) {
            this.messageDialog("错误", "密码错误", Ext.MessageBox.ERROR)
        }
    },
    doLoadAppDefines: function (urt) {
        util.rmi.miniJsonRequestAsync({
            url: "logon/myApps?urt=" + urt + "&deep=" + this.requestDefineDeep,
            httpMethod: "GET"
        }, function (code, msg, json) {
            if (code == 200) {
                this.win.hide()
                this.fireEvent("appsDefineLoaded")
            }
        }, this)
    },

    doCanel: function () {
        if (this.mainApp) {
            this.mainApp.desktop.fireEvent("winUnlock")
        }
        this.fireEvent("logonCancel")
        return true;
    }

})

