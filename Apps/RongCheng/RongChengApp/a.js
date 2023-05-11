var canvas = require('canvas');
let slider = '';
let point = '';


// async function requestIndexPage() {
//     var res = await fetch("http://ph01.gd.xianyuyigongti.com:9002/chis/index.html", {
//         "headers": {
//             "accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
//             "accept-language": "zh-CN,zh;q=0.9",
//             "cache-control": "max-age=0",
//             "if-modified-since": "Thu, 30 Mar 2023 06:56:10 GMT",
//             "if-none-match": "W/\"7313-1680159370000\"",
//             "upgrade-insecure-requests": "1"
//         },
//         "referrerPolicy": "strict-origin-when-cross-origin",
//         "body": null,
//         "method": "GET",
//         // "mode": "cors",
//         "credentials": "include"
//     });
//     var cookie = res.headers.get('Set-Cookie');
//     console.log(cookie);


// }
// requestIndexPage()

function uuid() {
    // 初始话 uuid 
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4"; // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1); // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    slider = 'slider' + '-' + s.join("");
    point = 'point' + '-' + s.join("");



}
uuid();

async function getImage() {
    var res = await fetch("http://ph01.gd.xianyuyigongti.com:9002/chis/captcha/get", {
        "headers": {
            "accept": "*/*",
            "accept-language": "zh-CN,zh;q=0.9",
            "content-type": "application/json;charset=UTF-8"
        },
        "referrer": "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html",
        "referrerPolicy": "strict-origin-when-cross-origin",
        "body": JSON.stringify({ "captchaType": "blockPuzzle", "clientUid": slider, "ts": 1683512519001 }),
        "method": "POST",
        "mode": "cors",
        "credentials": "omit"
    });
    var data = await res.json();
    console.log(data);
}
getImage();


function getSlideWidth() {
    var cas = canvas.createCanvas();
    img.src = window.capchaSrc;
    img.onload = function () {
        let cas = document.createElement('canvas');
        cas.style.zIndex = 99999;
        cas.width = img.width;
        cas.height = img.height;
        cas.style.position = 'fixed';
        cas.style.top = 0;
        cas.style.left = 0;
        document.body.appendChild(cas);


        let ctx = cas.getContext('2d');
        ctx.drawImage(img, 0, 0);
        let imgdata = ctx.getImageData(0, 0, img.width, img.height);
        let len = imgdata.data.length;
        console.log(`宽${cas.width} 高:${cas.height} ,总${imgdata.data.length}`);
        let countWhite = 0;
        let whitePoints = []
        for (let x = 0; x < cas.width; x++) {
            for (let y = 0; y < cas.height; y++) {
                //  (0,0)   0,1,2,3,4
                //(1,1)  width*1*4
                let r = imgdata.data[y * cas.width * 4 + x * 4],
                    g = imgdata.data[1 + y * cas.width * 4 + x * 4],
                    b = imgdata.data[2 + y * cas.width * 4 + x * 4],
                    a = imgdata.data[3 + y * cas.width * 4 + x * 4];
                // console.log(r, g, b, a)
                if (r == 255 && g == 255 && b == 255) {
                    // countWhite++;
                    whitePoints.push({ x, y })
                    console.log('找到白色节点', x, y)
                    // if (countWhite >= 2) {

                    //     break;
                    // }

                } else {
                    // countWhite = 0;
                }

            }
        }
        console.log(whitePoints);
        let maxY = 0;
        let maxYLength = 0;
        for (let y = 0; y < cas.height; y++) {
            let yPoints = whitePoints.filter(p => p.y == y);
            let start = 0;
            let startX = 0;
            for (let p of yPoints) {
                if (whitePoints.find(po => po.x = p.x + 1)) {
                    start++;
                    if (start > 20) {
                        console.log('找到', y, p.x);
                        window['xy'] = { x: p.x, y };
                        break;
                    }
                }
                else {
                    startX = 0;
                }
            }
            if (window['xy']) {
                break;
            }
        }


    }


}


function autoLogin(moveLeftDistance, secretKey, token) {
    let now = new Date().getTime();
    //判断是否重合
    var vOffset = parseInt(5);

    moveLeftDistance = moveLeftDistance * 310 / parseInt(400)
    //图片滑动

    var data = {
        captchaType: "blockPuzzle",
        "pointJson": secretKey ? aesEncrypt(JSON.stringify({ x: moveLeftDistance, y: 5.0 }), secretKey) : JSON.stringify({ x: moveLeftDistance, y: 5.0 }),
        "token": token,
        clientUid: localStorage.getItem('slider'),
        ts: Date.now()
    }
    // var captchaVerification = this.secretKey ? aesEncrypt(this.backToken+'---'+JSON.stringify({x:this.moveLeftDistance,y:5.0}),this.secretKey):this.backToken+'---'+JSON.stringify({x:this.moveLeftDistance,y:5.0})
    checkPictrue(data, "http://ph01.gd.xianyuyigongti.com:9002/chis", function (res) {
        // 请求反正成功的判断
        if (res.repCode == "0000") {
            alert("s验证成功")
            _this.htmlDoms.move_block.css('background-color', '#5cb85c');
            _this.htmlDoms.left_bar.css({ 'border-color': '#5cb85c', 'background-color': '#fff' });
            _this.htmlDoms.icon.css('color', '#fff');
            _this.htmlDoms.icon.removeClass('icon-right');
            _this.htmlDoms.icon.addClass('icon-check');
            //提示框
            _this.htmlDoms.tips.addClass('suc-bg').removeClass('err-bg')
            // _this.htmlDoms.tips.css({"display":"block",animation:"move 1s cubic-bezier(0, 0, 0.39, 1.01)"});
            _this.htmlDoms.tips.animate({ "bottom": "0px" });
            _this.htmlDoms.tips.text(((_this.endMovetime - _this.startMoveTime) / 1000).toFixed(2) + 's验证成功');
            _this.isEnd = true;
            setTimeout(function () {
                _this.$element.find(".mask").css("display", "none");
                // _this.htmlDoms.tips.css({"display":"none",animation:"none"});
                _this.htmlDoms.tips.animate({ "bottom": "-35px" });
                _this.refresh();
            }, 1000)
            // _this.options.success({'captchaVerification':captchaVerification});
            _this.options.success({ 'captchaVerification': res.repData.captchaVerification });
        } else {
            _this.htmlDoms.move_block.css('background-color', '#d9534f');
            _this.htmlDoms.left_bar.css('border-color', '#d9534f');
            _this.htmlDoms.icon.css('color', '#fff');
            _this.htmlDoms.icon.removeClass('icon-right');
            _this.htmlDoms.icon.addClass('icon-close');

            _this.htmlDoms.tips.addClass('err-bg').removeClass('suc-bg')
            // _this.htmlDoms.tips.css({"display":"block",animation:"move 1.3s cubic-bezier(0, 0, 0.39, 1.01)"});
            _this.htmlDoms.tips.animate({ "bottom": "0px" });
            _this.htmlDoms.tips.text(res.repMsg)
            setTimeout(function () {
                _this.refresh();
                _this.htmlDoms.tips.animate({ "bottom": "-35px" });
            }, 1000);

            // setTimeout(function () {
            // 	// _this.htmlDoms.tips.css({"display":"none",animation:"none"});
            // },1300)
            _this.options.error(this);
        }
    })
    this.status = false;
}