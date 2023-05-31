$package("chis.application.hy.script.visit");

$import("chis.script.BizHtmlFormView",
	"chis.application.hy.script.visit.HypertensionVisitMedicineHtml",
	"chis.application.hy.script.visit.HypertensionVisitHealthHtml",
	"chis.application.hy.script.visit.HypertensionVisitBaseInfoHtmlTemplate");
$styleSheet("chis.css.HypertensionVisitBaseInfo");

chis.application.hy.script.visit.HypertensionVisitBaseInfoFormHtml = function(cfg) {
	// cfg.autoLoadSchema = false;
	// cfg.isCombined = true;
	cfg.autoLoadData = false;
	cfg.needRadioCancel = true; //单选是否要取消选中
	// cfg.showButtonOnTop = true;
	// cfg.autoWidth = true;
	// cfg.colCount = 4;
	// cfg.labelWidth = 88;
	// cfg.fldDefaultWidth = 110;
	// cfg.autoFieldWidth = false;
	chis.application.hy.script.visit.HypertensionVisitBaseInfoFormHtml.superclass.constructor.apply(this, [cfg]);
	Ext.apply(this,chis.application.hy.script.visit.HypertensionVisitBaseInfoHtmlTemplate);
	this.initDataId = this.exContext.args.visitId;
	this.serviceId = "chis.hypertensionVisitService";
	this.serviceAction = "saveHypertensionVisit";
	this.createFields = ["visitDate", "nextDate", "visitDoctor"];
	// this.otherDisable = [
	// {fld:"currentSymptoms",type:"checkbox",control:[{key:"10",exp:'eq',field:["otherSymptoms"]}]},
	// {fld:"medicineBadEffect",type:"radio",control:[{key:"y",exp:'eq',field:["medicineBadEffectText"]}]}
	// ]
	this.on("aboutToSave", this.onAboutToSave, this);
	this.on("beforeLoadData", this.onBeforeLoadData, this);
	this.on("loadData", this.onLoadData, this);
	this.on("loadNoData", this.onLoadNoData, this);
	this.on("beforePrint", this.onBeforePrint, this);
	this.on("loadDataByLocal", this.onLoadDataByLocal, this);
	this.on("beforeSave", this.onBeforeSave, this);
	this.specificationReason2 = [];
	this.completeTypes = ["visitDate", "visitWay", "visitEffect", "currentSymptoms", "constriction", "diastolic", "weight", "targetWeight", "bmi", "targetBmi", "heartRate", "smokeCount", "targetSmokeCount", "drinkCount", "targetDrinkCount", "trainTimesWeek", "trainMinute", "targetTrainTimesWeek", "targetTrainMinute", "salt", "cure", "targetSalt", "psychologyChange", "obeyDoctor", "medicine", "medicineBadEffect", "visitEvaluate", "nextDate", "visitDoctor"];
	this.standardTypes = ["visitDate", "visitWay", "visitEffect", "currentSymptoms", "constriction", "diastolic", "weight", "targetWeight", "bmi", "targetBmi", "heartRate", "smokeCount", "targetSmokeCount", "drinkCount", "targetDrinkCount", "trainTimesWeek", "trainMinute", "targetTrainTimesWeek", "targetTrainMinute", "salt", "cure", "targetSalt", "psychologyChange", "obeyDoctor", "auxiliaryCheck", "medicine", "medicineBadEffect", "visitEvaluate", "nextDate", "visitDoctor", "agencyAndDept", "referralReason","medicineNot"];
};

var thisPanel = null;

Ext.extend(chis.application.hy.script.visit.HypertensionVisitBaseInfoFormHtml,
	chis.script.BizHtmlFormView, {
		onLoadData: function() {
			this.resetControlOtherFld();
			this.fieldValidate(this.schema)
			this.doFinish(1,false);
			this.doStandard(1,false);
            this.setImportJzInfoBtnState();
		},
        doUploadFile : function () {
            // var emType = "jpg,jpeg,png,bmp,rar,zip,docx,doc,pdf";
            var eduMaterialUploadWin = this.midiModules["eduMaterialUploadWin"];
            if (!eduMaterialUploadWin) {
                $import("chis.application.dbs.script.visit.VisitEvidenceUpload");
                var cfg = [];
                cfg.fileFilter = this.emType.split(',');
                cfg.dirType = this.dirType;
                eduMaterialUploadWin = new chis.application.dbs.script.visit.VisitEvidenceUpload(cfg);
                this.midiModules["eduMaterialUploadWin"] = eduMaterialUploadWin;
                eduMaterialUploadWin.on("upFieldSucess", this.afterUpLoad,this);
            }
            eduMaterialUploadWin.visitId = this.data.visitId;
            eduMaterialUploadWin.visitType = "1";
            eduMaterialUploadWin.mainApp = this.mainApp;
            eduMaterialUploadWin.show();
        },
        doFileView : function() {
            var module = this.createSimpleModule("fileViewListTwo",this.fileViewListTwo);
            module.visitId =  this.data.visitId;
            module.visitType = "1";
            module.mainApp = this.mainApp;
            this.showWin(module);
            module.loadData();
        },

		initData: function(initValues) {
			if (this.readOnly) {
				return;
			}
			if (initValues) {
				if (!this.data) {
					this.data = {};
				}
				if (this.exContext.args.JZXH) {
					initValues.visitWay = {
						key: "1",
						text: "门诊就诊"
					};
				}
				initValues.visitEffect = {
					key: "1",
					text: "继续随访"
				};
				initValues.smokeCount = 0;
				initValues.drinkCount = 0;
				// initValues.medicine = {
				// 	key: "1",
				// 	text: "规律"
				// };
				this.initFormData(initValues);

				//					for (var item in initValues) {
				//						var field = this.form.getForm().findField(item);
				//						if (field) {
				//							field.setValue(initValues[item]);
				//						} else {
				//							this.data[item] = initValues[item];
				//						}
				//					}
			}
			this.medicine = null;
			this.nextDateDisable = initValues['nextDateDisable'];
			this.planId = initValues["planId"];
			this.visitId = initValues["visitId"];
			this.empiId = initValues["empiId"];
			this.phrId = initValues["phrId"];
			this.planDate = initValues["planDate"];
			this.beginDate = Date.parseDate(initValues["beginDate"] ||
				this.mainApp.serverDate, "Y-m-d");
			this.endDate = Date.parseDate(initValues["endDate"] ||
				this.mainApp.serverDate, "Y-m-d");
			this.sn = initValues["sn"];
			this.planStatus = initValues['planStatus'];
			// 设置随访时间选择范围
			var nowDate = Date.parseDate(this.mainApp.serverDate, "Y-m-d");
			var visitDateObj = this.visitDate;
			if (this.planDate) {
				visitDateObj.setValue(this.planDate); // 默认值为计划时期
			}
			if (!this.beginDate || !this.endDate) {
				return;
			}
			//				if (nowDate < this.beginDate || nowDate > this.endDate) {
			//					visitDateObj.setMinValue(this.beginDate);
			//					visitDateObj.setMaxValue(this.endDate);
			//				} else if (nowDate >= this.beginDate && nowDate <= this.endDate) {
			//					visitDateObj.setMinValue(this.beginDate);
			//					visitDateObj.setMaxValue(nowDate);
			//				}
			var nextDateObj = this.nextDate;
			/*if (nowDate > this.endDate) {
				nextDateObj.setValue(nowDate);
			} else {
				var nextMinDate = new Date(this.endDate.getFullYear(),this.endDate.getMonth(), this.endDate.getDate() + 1);
				nextDateObj.setValue(nextMinDate);
			}*/
			var result = util.rmi.miniJsonRequestSync({
				serviceId: "chis.hypertensionVisitService",
				serviceAction: "visitInitialize",
				method: "execute",
				// cnd: ["eq", ["$", "empiId"], ["s", this.empiId]],
				body: {
					lifeStyleSchema: "chis.application.hr.schemas.EHR_LifeStyle",
					fixGroupSchema: "chis.application.hy.schemas.MDC_HypertensionFixGroup",
					empiSchema: "chis.application.mpi.schemas.MPI_DemographicInfo",
					empiId: this.empiId,
					phrId: this.phrId,
					visitId: this.visitId,
					lastEndDate: initValues["endDate"],
					lastBeginDate: initValues["beginDate"],
					planDate: this.planDate,
					planId: this.planId,
					occurDate: initValues["beginDate"],
					businessType: 1
				}
			});
			if (result.json.body) {
				var groupAlarm = result.json.body.groupAlarm;
				var nonArrivalDate = this.exContext.args.nonArrivalDate;
				if (groupAlarm == 2 && this.planStatus != "9" && !nonArrivalDate) {
					//				MyMessageTip
					//						.msg('提示', "下一次随访之前需要进行评估，请做好相关准备工作。", true);
				}
				var data = result.json.body.lifeStyle;
				if (data) {
					var smokeCount = data.smokeCount;
					var drinkCount = data.drinkCount;
					this.setValueById("smokeCount", smokeCount);
					this.setValueById("drinkCount", drinkCount);
				}
				data = result.json.body.minStepInfo;
				if (data) {
					this.nextRemindDate = data.nextRemindDate;
					this.nextPlanId = data.nextPlanId;
				} else {
					delete this.mextRemindDate;
					delete this.nextPlanId;
				}
				data = result.json.body.fixGroup;
				if (data) {
					this.manaUnitId = data.manaUnitId;
					this.height = data.height;
					this.weight = data.weight;
					window.fixGroupheight = data.height;
                    this.setValueById("weight", data.weight);
                    this.setValueById("bmi", data.bmi);
                    // this.setValueById("heartRate",data.heartRate);
                    this.setValueById("diastolic",data.diastolic);
                    this.setValueById("constriction",data.constriction);
					this.setRadioValue("riskLevel", data.riskLevel || '');
					this.clearCheckBoxValues("riskiness");
					this.setCheckBoxValues("riskiness", data.riskiness ||'');
					onRiskinessClick(data.riskiness);
					this.clearCheckBoxValues("targetHurt");
					this.setCheckBoxValues("targetHurt", data.targetHurt ||'');
					this.clearCheckBoxValues("complication");
					this.setCheckBoxValues("complication",data.complication || '');
				} else {
					delete this.manaUnitId;
					delete this.height;
					delete this.riskLevel;
					delete this.targetHurt;
					delete this.complication;
				}
				this.age = result.json.body.age;
				this.sex = result.json.body.sex;
				this.planMode = result.json.body.planMode;
				thisPanel.planMode = this.planMode;
				data = result.json.body.lastVisit;
				if (data) {
					/*if(data.noVisitReasonOtherNot){
						document.getElementById("noVisitReasonOtherNot" + thisPanel.idPostfix).disabled = false;
					}*/
					if (data.medicineIds) {
						this.medicineIds = data.medicineIds;
					} else {
						delete this.medicineIds;
					}
					this.medicines = data.medicines;
					if (this.medicines) {
						this.saveMedicine = this.medicines
						var YYQK = this.medicines.YYQK;
						document.getElementById("YYQK" + this.idPostfix).innerHTML = YYQK;
					}
					if(this.height <= 0 || this.height == null){
                        this.height = this.height;
                        window.fixGroupheight = this.height;
                    }
					// var targetWeight = data.targetWeight;
					var targetWeight = this.height - 105;
					var targetHeartRate = data.targetHeartRate;
					var targetSmokeCount = data.targetSmokeCount;
					var targetDrinkCount = data.targetDrinkCount;
					var targetTrainMinute = data.targetTrainMinute;
					var targetSalt = data.targetSalt || "";
					this.setValueById("targetWeight", targetWeight);
					var targetBmiDom = document.getElementById("targetBmi" + this.idPostfix);
					onWeightHyChange(targetWeight, targetBmiDom);
					this.setValueById("targetHeartRate", targetHeartRate || '');
					this.setValueById("targetSmokeCount", targetSmokeCount || '');
					this.setValueById("targetDrinkCount", targetDrinkCount || '');
					this.setValueById("targetTrainMinute", targetTrainMinute);
					this.setValueById("targetSalt", targetSalt);
					// this.form.getForm().findField("targetTrainTimesWeek").setValue(targetTrainTimesWeek);
					this.lastVisitId = data.visitId;
                    if(this.weight <= 0 || this.weight == null){
                        this.weight = data.weight;
                    }
				} else {
					this.lastVisitId = "0000000000000000";
				}
                //var dqWeight = data.weight;
				if (this.sex && this.height) {
					var targetWeight;
					if(this.weight){
                        targetWeight = this.weight;
                        this.setValueById("weight", this.weight || '');
                    }else{
                        if (this.sex == 1) {
                            targetWeight = this.height - 105;
                            this.setValueById("weight", targetWeight || '');
                        } else if (this.sex == 2) {
                            targetWeight = this.height - 110;
                            this.setValueById("weight", targetWeight || '');
                        }
                    }
					var targetBmiDom = document.getElementById("bmi" + this.idPostfix);
					onWeightHyChange(targetWeight, targetBmiDom);
				}

				// 去掉目标体重默认值
				// this.setValueById("targetWeight", '')

				if ((this.sex == 1 && this.age > 55) ||
					(this.sex == 2 && this.age > 65)) {
					this.setCheckBoxValues("riskiness", "1");
				}

				var body = result.json.body;
				if (body) {
					var visitInfo = body.visitInfo
					if (visitInfo) {
						this.medicines = visitInfo.medicines;
                        delete visitInfo.visitDate;
                        delete visitInfo.nextDate;
                        delete visitInfo.constriction;
                        delete visitInfo.diastolic;
                        delete visitInfo.weight;
                        delete visitInfo.bmi;
                        delete visitInfo.targetWeight;
                        delete visitInfo.targetBmi;
                        if(targetBmiDom){
							visitInfo.bmi = targetBmiDom.value;
						}
						// this.initFormData(visitInfo);
					}
				}
			}
			// 控制下次随访医生
			if (!this.visitId) {
				this.visitDoctor.setValue({
					"key": this.mainApp.uid,
					"text": this.mainApp.uname
				});
			}
			this.onLoadData();
			var noVisitReasonDom = document.getElementById("noVisitReason" + this.idPostfix);
			onVisitEffectHyChange(1, noVisitReasonDom);
			var otherSymptomsDom = document.getElementById("otherSymptoms" + this.idPostfix);
			otherSymptomsDom.disabled = true;
			this.setNextDate();
            this.setRadioDisabled("YYQKBtn",true);
            if (this.medicines) {
                //JWGDSXM-2124【责任医生】角色【高血压随访-用药情况】，新随访会显示上次随访的用药情况，但是不能编辑，建议把治疗方式和服药依从性也附上上次随访的选项
                var medicineBadEffectTextDom = document.getElementById("medicineBadEffectText" + this.idPostfix);
                if(data.cure){
                    //治疗方式
                    this.setCheckBoxValues("cure",data.cure);
                    noMedicineHyClick(data.cure,medicineBadEffectTextDom);
                    var div_cure = document.getElementById("div_cure" + thisPanel.idPostfix);
                    thisPanel.removeClass(div_cure, "x-form-invalid");
                    if(data.noCureReason){
                        var noCureReason = document.getElementById("noCureReason" + thisPanel.idPostfix);
                        noCureReason.value=data.noCureReason;
                    }
                }
                if(data.medicine) {
                    //服药依从性
                    this.setCheckBoxValues("medicine", data.medicine);
                    onMedicineClick(data.medicine,medicineBadEffectTextDom);
                    if(data.medicineNot) {
                        this.setCheckBoxValues("medicineNot", data.medicineNot);
                        onMedicineNotClick(data.medicineNot)
                        if(data.medicineOtherNot){
                            var medicineOtherNot = document.getElementById("medicineOtherNot" + thisPanel.idPostfix);
                            medicineOtherNot.value=data.medicineOtherNot;
                            thisPanel.removeClass(medicineOtherNot, "x-form-invalid");
                        }
                    }
                }
                if(data.medicineBadEffect) {
                    //药物不良反应
                    this.setCheckBoxValues("medicineBadEffect", data.medicineBadEffect);
                    onMedicineBadEffectChange(data.medicineBadEffect, medicineBadEffectTextDom);
                    if(data.medicineBadEffectText){
                        var medicineBadEffectText = document.getElementById("medicineBadEffectText" + thisPanel.idPostfix);
                        medicineBadEffectText.value=data.medicineBadEffectText;
                    }
                }
            }
		},

		onBeforeLoadData: function(entryName, initDataId) {
			this.phrId = this.exContext.args.phrId;
			this.empiId = this.exContext.args.empiId;
			this.visitId = this.exContext.args.visitId;
			this.planDate = this.exContext.args.planDate;
			this.planId = this.exContext.args.planId;
			this.sn = this.exContext.args.sn;
			return true;
		},

		loadData: function() {
			this.doNew();
			if (this.loading) {
				return;
			}
			if (!this.schema) {
				return;
			}
			if (!this.fireEvent("beforeLoadData", this.entryName,this.initDataId)) {
				return;
			}
			if (!this.exContext.args.visitId) {
				return;
			}
			// this.form = this.midiModules[this.actions[0].id].form;
			if (this.form && this.form.el) {
				this.form.el.mask("正在载入数据...", "x-mask-loading");
			}
			this.loading = true;
			this.age = 0;
			this.sex = 0;
			util.rmi.jsonRequest({
				serviceId: "chis.hypertensionVisitService",
				serviceAction: "getVisitInfo",
				method: "execute",
				body: {
					pkey: this.exContext.args.visitId,
					planId: this.exContext.args.planId,
					empiId: this.exContext.args.empiId
				}
			}, function(code, msg, json) {
				if (this.form && this.form.el) {
					this.form.el.unmask();
				}
				this.loading = false;
				if (code > 300) {
					if (code == 504) {
						msg = "对应的随访信息未找到!";
					}
					this.processReturnMsg(code, msg, this.loadData);
					return;
				}
				var visitEffect = "";
				this.medicine = "";
				var currentSymptoms = "";
				var body = json.body;
				if (body) {
					var visitInfo = body.visitInfo
					if (visitInfo) {
						this.medicines = visitInfo.medicines;
						this.clearCheckBoxValues("riskiness");
						this.clearCheckBoxValues("targetHurt");
						this.clearCheckBoxValues("complication");
						this.initFormData(visitInfo);
						this.fireEvent("loadData", this.entryName,visitInfo);
						visitEffect = visitInfo.visitEffect;
						var noVisitReasonDom = document.getElementById("noVisitReason" + this.idPostfix);
						var visitWayDom = thisPanel.getElementById("div_visitWay" + this.idPostfix);
						var medicineDom = thisPanel.getElementById("div_medicine" + this.idPostfix);
						onVisitEffectHyChange(visitEffect.key,noVisitReasonDom, visitWayDom,medicineDom,visitInfo.noVisitReason);
						var medicineBadEffect = visitInfo.medicineBadEffect;
						if (medicineBadEffect) {
							var medicineBadEffectTextDom = document.getElementById("medicineBadEffectText" + this.idPostfix);
							onMedicineBadEffectChange(medicineBadEffect.key,medicineBadEffectTextDom);
						}
						currentSymptoms = visitInfo.currentSymptoms;
						if (currentSymptoms) {
							var otherSymptomsDom = document.getElementById("otherSymptoms" + this.idPostfix);
							onCurrentSymptomsClick(null,otherSymptomsDom,currentSymptoms.key)
						}
						if (visitInfo.medicine) {
							this.medicine = visitInfo.medicine;
						}
						weight = visitInfo.weight;
						currentSymptoms = visitInfo.currentSymptoms;
						this.exContext.visitId = visitInfo.visitId;
						this.hypertensionGroup = visitInfo.hypertensionGroup;
						var medicineBadEffectTextDom = document
							.getElementById("medicineBadEffectText" +
								this.idPostfix);
						if (visitInfo.medicine) {
							onMedicineClick(visitInfo.medicine.key,
								medicineBadEffectTextDom)
						} else {
							onMedicineClick(null,
								medicineBadEffectTextDom)
						}
						if (visitInfo.medicineIds) {
							this.medicineIds = visitInfo.medicineIds;
						} else {
							delete this.medicineIds;
						}
						if(visitInfo.cure && visitInfo.cure.key == '2'){
							thisPanel.setRadioDisabled("medicine", true)
							thisPanel.setRadioDisabled("medicineNot", true)
							//thisPanel.setRadioDisabled("medicineOtherNot", true)
							thisPanel.setRadioDisabled("YYQKBtn", true)
							thisPanel.setRadioDisabled("medicineBadEffect", true)
						}else if(visitInfo.cure && visitInfo.cure.key == '1'){
                            thisPanel.setRadioDisabled("YYQKBtn", false)
						}else{
                            thisPanel.setRadioDisabled("YYQKBtn", true)
						}
					}
					delete this.manaUnitId;
					delete this.height;
					this.height = body.height;
					this.manaUnitId = body.manaUnitId;
					this.age = body.age;
					this.sex = body.sex;
					this.planMode = body.planMode;
					if (body.lastVisit) {
						this.lastVisitId = body.lastVisit.visitId;
					}
					var visitWay = this.getFormData().visitWay;
					if(visitWay && visitWay=='3'){
						var schema = this.schema;
						var re = util.schema
							.loadSync("chis.application.hy.schemas.MDC_HypertensionVisit_html")
						if (re.code == 200) {
							schema = re.schema;
						}
						var items = schema.items;
						for (var i = 0; i < items.length; i++) {
							var schemaItem = items[i];
							if (schemaItem.id == 'heartRate') {
								this.heartRateItem = schemaItem
								break
							}
						}
						var heartRateFld = document.getElementById("heartRate" + thisPanel.idPostfix);
						var heartRate_Label = document.getElementById("heartRate_Label" + thisPanel.idPostfix);
						thisPanel.removeClass(heartRateFld, "x-form-invalid");
						this.heartRateItem["not-null"] = false;
						heartRate_Label.style.color = "black";

					}
				}
				if (this.op == 'create') {
					this.op = "update";
				}
				//加载数据后触发方法
				this.doFinish(1,false);
				this.doStandard(1,false);
			}, this); // jsonRequest
			// 控制下次预约时间
			this.setNextDate();
		},
		setNextDate: function() {
			// 控制预约时间
			var nextDate = this.nextDate;
			// 按随访结果时 下次预约时间范围在下一计划的开始结果时间之间
			if (this.planMode == "1" && nextDate) {

				if (this.nextDateDisable || this.exContext.args.visitId) {} else {
					if (this.exContext.args.endDate < this.mainApp.serverDate) {
						var p = Date.parseDate(this.mainApp.serverDate,
							"Y-m-d");
						var nextMinDate = new Date(p.getFullYear(), p
							.getMonth(), p.getDate() + 1);
						nextDate.setMinValue(nextMinDate);
					} else {
						var p = Date.parseDate(this.exContext.args.endDate,
							"Y-m-d");
						var nextMinDate = new Date(p.getFullYear(), p
							.getMonth(), p.getDate() + 1);
						nextDate.setMinValue(nextMinDate);
					}
				}
			}
			// 控制nextDate时间
			if (this.planMode == "2") { // 按下次预约随访时
				thisPanel.nextDate = nextDate;
				nextDate.allowBlank = false;
				nextDate["not-null"] = true;
				var nextDate_Label = document.getElementById("nextDate_Label" + this.idPostfix);
				if (nextDate_Label) {
					nextDate_Label.style.color = "red";
				}
			}
			nextDate.validate();
		},

		getFormData: function() {
			var ac = util.Accredit;
			if (!this.schema) {
				return
			}
			var values = {};
			var items = this.schema.items
			var visitEffect = this.getRadioValue("visitEffect");
			if (items) {
				var n = items.length
				for (var i = 0; i < n; i++) {
					var it = items[i]
					if (this.op == "create" && !ac.canCreate(it.acValue)) {
						continue;
					}
					var v = this.data[it.id]
					if (v == undefined) {
						v = it.defaultValue
					}
					if (v != null && typeof v == "object") {
						v = v.key
					}
					if (this.isCreateField(it.id)) {
						v = eval("this." + it.id + ".getValue()");
						var xtype = eval("this." + it.id + ".getXType()");
						if (xtype == "treeField") {
							var rawVal = eval("this." + it.id +
								".getRawValue()");
							if (rawVal == null || rawVal == "")
								v = "";
						}
						if (xtype == "datefield" && v != null && v != "") {
							v = v.format('Y-m-d');
						}
					} else { // 不是EXT创建的控件获取值
						if (it.tag == "text") {
							v = this.getValueById(it.id);
						} else if (it.tag == "radioGroup") {
							v = this.getRadioValue(it.id);
						} else if (it.tag == "checkBox") {
							v = this.getCheckBoxValues(it.id);
						} else if (it.tag == "selectgroup") {
							v = this.getSelectValues();
						} else if (it.id == "visitDoctor") {
							v = eval("this." + it.id + ".getValue()");
						}
					}
					if (v) {
						values[it.id] = v;
					} else {
						values[it.id] = "";
					}
				}
			}
			return values;
		},
		// text类型的获取值
		getValueById: function(id) {
			var dom = document.getElementById(id + this.idPostfix);
			if (dom.value == dom.defaultValue) {
				return;
			}
			return dom.value
		},
		// radio类型的获取值
		getRadioValue: function(id) {
			var v = document.getElementsByName(id + this.idPostfix);
			var le = v.length;
			for (var i = 0; i < le; i++) {
				if (v[i].checked) {
					return v[i].value;
				}
			}
			return 0;
		},
		// checkBox类型的获取值
		getCheckBoxValues: function(id) {
			var v = document.getElementsByName(id + this.idPostfix);
			var le = v.length;
			var value = new Array();
			for (var i = 0; i < le; i++) {
				if (v[i].checked) {
					value.push(v[i].value);
				}
			}
			return value.toString();
		},
		// 取下拉框被选中的值,传入select的id
		getSelect: function(id) {
			var obj = document.getElementById(id + this.idPostfix); // selectid
			var index = obj.selectedIndex; // 选中索引
			var year = obj.options[index].value; // 选中值
			return year;
		},
		// text类型的赋值
		setValueById: function(id, v) {
			var dom = document.getElementById(id + this.idPostfix);
			if (!dom) {
				return;
			}
			if (v != null && (v != "" || v == "0")) {
				dom.value = v;
				dom.style.color = "#000";
			} else if (dom.defaultValue) {
				dom.value = dom.defaultValue
				dom.style.color = "#999";
			} else {
				dom.value = dom.defaultValue;
			}
		},
		// radio类型的赋值
		setRadioValue: function(id, v) {
			var dom = document.getElementsByName(id + this.idPostfix);
			if (!dom) {
				return;
			}
			var le = dom.length;
			for (var i = 0; i < le; i++) {
				if (v != null && dom[i].value == v.key) {
					dom[i].checked = true;
					eval("this." + id + this.idPostfix + "Value='" + dom[i].value + "'");
					break;
				}
			}
		},
		setRadioValue2: function(id, v) {
			var dom = document.getElementsByName(id + this.idPostfix);
			if (!dom) {
				return;
			}
			var le = dom.length;
			for (var i = 0; i < le; i++) {
				console.log(dom[i].value)
				if (v != null && dom[i].value == v) {
					dom[i].checked = true;
					eval("this." + id + this.idPostfix + "Value='" + dom[i].value + "'");
					break;
				}
			}
		},
		// 给下拉框赋值
		setSelectValue: function(id, v) {
			var dom = document.getElementById(id + this.idPostfix);
			if (!dom) {
				return;
			}
			// var le = dom.length;
			var le = dom.options.length;
			for (var i = 0; i < le; i++) {
				if (dom.options[i].value == v) {
					dom.options[i].selected = true;
					break;
				}
			}
		},

		// checkBox类型的赋值
		setCheckBoxValues: function(id, v) {
			var dom = document.getElementsByName(id + this.idPostfix);
			if (!dom) {
				return;
			}
			var k = dom.length;
			// 下面代码是后台传值list或者string, 二选一 确定后台传值类型后 将另一个判断删掉
			if (typeof v == "object" && v.key) { // 如果值是list类型
				v = v.key;
			}
			var value = [];
			if (v.indexOf(",") > -1) {
				value = v.split(",");
			} else {
				value[0] = v;
			}
			var l = value.length;
			for (var i = 0; i < l; i++) {
				for (var j = 0; j < k; j++) {
					if (value[i] == dom[j].value) {
						dom[j].checked = true;
						break;
					}
				}
			}
		},

		setMedicineDisabled: function(flag) {

		},

		setCheckBoxDisabled: function(id, flag) {
			var dom = document.getElementsByName(id + this.idPostfix);
			var k = dom.length;
			for (var i = 0; i < k; i++) {
				if (flag) {
					dom[i].checked = false;
				}
				dom[i].disabled = flag;
			}
		},

		setRadioDisabled: function(id, flag) {
			var dom = document.getElementsByName(id + this.idPostfix);
			var le = dom.length;
			for (var i = 0; i < le; i++) {
				dom[i].disabled = flag;
			}
		},
		setRadioDisabled2: function(id, flag) {
			var dom = document.getElementsByName(id + this.idPostfix);
			var le = dom.length;
			for (var i = 0; i < le; i++) {
				dom[i].disabled = flag;
				dom[i].checked = false;
			}
		},

		initFormData: function(data) {
			if(data.noVisitReasonOtherNot){
				document.getElementById("noVisitReasonOtherNot" + thisPanel.idPostfix).disabled = false;;
			}
			//屏蔽复制按钮
			if(this.btnAccessKeys[115]){
				let copyButtonLabelId = this.btnAccessKeys[115].id;
				if(data.visitId){
					$('#'+copyButtonLabelId).hide();
				}else{
					$('#'+copyButtonLabelId).show();
				}
			}
			Ext.apply(this.data, data)
			this.initDataId = this.data[this.schema.pkey]
			var items = this.schema.items
			var n = items.length
			for (var i = 0; i < n; i++) {
				var it = items[i]
				//管控级别不修改
				/*if(it.id=='riskLevel'){
					continue;
				}*/
				//症状
				if(it.id=='currentSymptoms'){
					this.clearCheckBoxValues(it.id);
				}
				if (this.isCreateField(it.id)) {
					var v = data[it.id]
					var f = eval("this." + it.id);
					// flag==true 表示要清空数据
					if (v != undefined || this.flag == true) {
						if (it.dic && v !== "" && v === 0) {
							// 解决字典类型值为0(int)时无法设置的BUG
							v = "0";
						}
						// 判断点击“保存旁边的新建的时候，不要清空管辖机构的数据”
						if (this.flag == true && it.id == "manaUnitId") {} else {
							f.setValue(v)
						}
						if (it.dic && v != "0" && f.getValue() != v) {
							f.counter = 1;
							// this.setValueAgain(f, v, it);
						}
					}
					if (it.update == "false" && this.initDataId) {
						f.disable();
					}
				} else {
					var v = data[it.id]
					if (v != undefined || this.flag == true) {
						if (undefined == v) {
							v = ""
						}
						if (it.tag == "text") {
							this.setValueById(it.id, v)
						} else if (it.tag == "radioGroup") {
							if (this.flag == true) {
								// 清空
								var vv = document.getElementsByName(it.id +
									this.idPostfix);
								var le = vv.length;
								for (var j = 0; j < le; j++) {
									if (vv[j].checked) {
										vv[j].checked = false;
										break;
									}
								}
							} else {
								this.setRadioValue(it.id, v)
							}
						} else if (it.tag == "checkBox") {
							if (this.flag == true) {
								// 清空
								this.clearCheckBoxValues(it.id);
							} else {
								this.setCheckBoxValues(it.id, v)
							}
						} else if (it.tag == "selectgroup") {
							if (this.flag == true) {
								// 单选框
								var v2 = document
									.getElementsByName("educationCode" +
										this.idPostfix);
								var le2 = v2.length;
								for (var j2 = 0; j2 < le2; j2++) {
									if (v2[j2].checked) {
										v2[j2].checked = false;
									}
								}
							} else {
								this.setSelectValues(it.id, v)
							}
						}
					}
				}
				this.setKeyReadOnly(true)
			}

			//不把上一份随访的参数带到下一个随访
			var noVisitReasonDom5 = document.getElementById("noVisitReason_5" + this.idPostfix);
			if(!noVisitReasonDom5.checked){
				var f = document.getElementById("noVisitReasonOtherNot" + this.idPostfix)
				if (f) {
					f.value = '';
				}
			}

			this.flag = false;
			if (this.medicines) {
				var YYQK = this.medicines.YYQK
				document.getElementById("YYQK" + this.idPostfix).innerHTML = YYQK;
			}
			if (data.healthProposal && data.healthProposal.text) {
				this.healths = {};
				this.healths.value = data.healthProposal.key;
				this.healths.JKCF = data.healthProposal.text;
				var JKCF = this.healths.JKCF
				document.getElementById("JKCF" + this.idPostfix).innerHTML = JKCF;
			}
            var flag = 0;
            var result = util.rmi.miniJsonRequestSync({
                serviceId : "chis.visitEvidenceService",
                serviceAction : "getParamsAuthority",
                method : "execute",
            });
            if (result.code == 200) {
                flag = result.json.body;
            } else {
                MyMessageTip.msg('错误提示', "公共参数获取错误",true);
            }
            if(this.form.getTopToolbar()){
                var hyStatus = this.exContext.ids["MDC_HypertensionRecord.phrId.status"];
                var btn = this.form.topToolbar.items.items;
                var planStatus = this.exContext.args["planStatus"];
                for(var i = 0;i < btn.length;i++){
                    if(flag == "0" && btn[i].cmd == "fileView"){
                        btn[i].hide();
                    }else{
                        if(btn[i].cmd == "fileView" && planStatus != '1'){
                            btn[i].disable();
                        }else{
                            if (hyStatus == "1" && (btn[i].cmd == "save" || btn[i].cmd == "importJzInfo" || btn[i].cmd == "synchronized")) {
                                btn[i].disable();
                            } else{
                                btn[i].enable();
                            }
                        }
                    }
                }
            }
		},
		calStandardPercent:function(e){
			var a = this.standardTypes.length;
			var b = this.warnWordCode.length;
			// this.warnWord
			var d = 100;
			var l = [];
			var flag = false;

			if(a!=b){
				d = Math.floor(b / a * 100);
			}
			var diffArry = this.getArrSameDifference(this.standardTypes, this.warnWordCode);
			var items = this.schema.items;
			for (var j = 0; j < diffArry.length; j++) {
				for(var j1 = 0; j1 < items.length; j1++) {
					if(diffArry[j] === items[j1].id) {
						if(items[j1].id == 'constriction' && this.specificationReason.indexOf("收缩压未填写") == 0){
							continue
						}
						if(items[j1].id == 'diastolic' && this.specificationReason.indexOf("舒张压未填写") == 0){
							continue
						}
						l.push(items[j1].alias);
					}
				}
			}
			var msg = "";
            var itemsLength = 0;
            if(this.form.topToolbar){
                var items = this.form.topToolbar.items.items;
                itemsLength = items.length;
            }
			var nbsp="<br>&nbsp;&nbsp;&nbsp;&nbsp;";

			if(l.length > 0) {
				this.nonStandard = "【"+ l+"】未填写";
				msg = msg+"<strong style='color:red'>不符合高血压随访规范，未规范原因为：</strong> "+nbsp+"【"+ l+"】未填写";
				flag = true;
			}
			if(this.specificationReason2&&this.specificationReason2.length>0) {
				//this.specificationReason.push(this.specificationReason2);
                if(this.specificationReason.join("")!="失访"){
                    this.specificationReason=this.specificationReason.concat(this.specificationReason2);
				}
			}


			if(flag && this.specificationReason&& this.specificationReason.length > 0) {
				msg = msg + nbsp + this.specificationReason.join(nbsp);
				this.nonStandard  += nbsp+ this.specificationReason.join(",");
			}
			if(!flag && this.specificationReason&&this.specificationReason.length>0) {
				this.nonStandard  = this.specificationReason.join(",");
				msg = msg+"<strong style='color:red'>不符合高血压随访规范，未规范原因为：</strong>"+nbsp+ this.specificationReason.join(nbsp);
			}

            for(var i = 0;i<itemsLength;i++){
                if(items[i].cmd == "standard"){
                    //修改规范按钮
                    //if(a!=b){
                    if(msg!=""){
                        items[i].setText("<strong style='color:#f56c6c;font-weight: bold;' >未规范</strong>");
                    }else{
                        items[i].setText("<strong style='color:#548dcc;font-weight: bold;' >规范</strong>");
                    }
                }
            }

			if(msg==""){
				msg = "<strong style='color:#548dcc'>当前业务数据符合规范</strong>";
			}
			if(e != 1) {
				MyMessageTip.msg("温馨提示",msg,true);
			}
            if(this.callBackSaveData){
                //保存回调
				this.saveToServerVerifyAfter(this.callBackSaveData);
			}
		},
		doStandard:function(type,flag){
			//规范按钮
 			// 1、空项检测（完善度）：【失访、终止管理】只判断必填项；同时【继续随访】也应该不包含【体征其他、辅助检查、转诊原因、转诊机构及科别】。
			// 2、规范性：【失访】不判断，就是【不规范】，不规范原因：失访；【终止管理】可以不判断，作为【规范】；【继续随访】按以下逻辑判断：
			// A、空项都需要填写（见第1条说明），未填写，则记录原因
			// B、【治疗方式=服药】且【服药依从性=规律/间断】时，【用药情况】未填写
			// C、【随访分类=控制不满意/不良反应/并发症】时，下次随访日期不在两周内
			// D、上次随访【随访分类=控制不满意/不良反应/并发症】时，本次随访未在两周内进行
			// E、存在危急情况（血压≥180/110mmHg）时，【转诊原因、转诊科别】未填写
			// F、连续2次的【随访分类=控制不满意/不良反应】时，【转诊原因、转诊科别】未填写
			// G、有新的并发症出现或原有并发症加重，【转诊原因、转诊科别】未填写 --该条可先不实现（因目前无地方填写具体【并发症】）

			var visitEffect = this.getHtmlFldValue("visitEffect");
			var cure = this.getHtmlFldValue("cure");
			var medicine = this.getHtmlFldValue("medicine");
			//var YYQK = document.getElementsByName("YYQK")[0].innerText;
            var YYQK = document.getElementById("YYQK" + this.idPostfix).innerText;
			var visitType = this.getHtmlFldValue("visitEvaluate");
			var visitDate= this.getHtmlFldValue("visitDate");
			var nextDate = this.getHtmlFldValue("nextDate");
            nextDate=nextDate.replace("下次随访日期","");
			//var fbs = this.getHtmlFldValue("fbs");
			var constriction = document.getElementById("constriction" + this.idPostfix).value;
			var diastolic = document.getElementById("diastolic" + this.idPostfix).value;
			this.specificationReason = [];

			// 规范性：【失访】不判断，就是【不规范】
			if(visitEffect == 2) {
				this.specificationReason.push("失访");
			}
			if(visitEffect){
				this.visitEffectVlue = visitEffect;
			}
			if(this.visitEffectVlue && (this.visitEffectVlue == 2||this.visitEffectVlue == 9)) {
				this.doVisiteff29(2,flag);
				return;
			}
			//【终止管理】可以不判断，作为【规范】
			if(visitEffect == 9) {
				// 终止管理规范
				var items = this.form.topToolbar.items.items;
				var itemsLength = items.length;
				for(var i = 0;i<itemsLength;i++){
					if(items[i].cmd == "standard"){
						items[i].setText("<strong style='color:#548dcc;font-weight: bold;' >规范</strong>");
					}
					if(items[i].cmd == "finish"){
						//修改完整率按钮
						items[i].setText(("完善度100%"));
					}
				}
				MyMessageTip.msg("温馨提示","<strong style='color:#548dcc'>当前业务数据符合规范</strong>",true);
				return;
			}

			if(visitEffect == 2 || visitEffect == 9) {
				this.standardTypes = ["noVisitReason", "nextDate", "visitDoctor"];
			}else if(visitEffect == 1){
				this.standardTypes = ["visitDate", "visitWay", "visitEffect", "currentSymptoms", "constriction", "diastolic", "weight", "targetWeight", "bmi", "targetBmi", "heartRate", "smokeCount", "targetSmokeCount", "drinkCount", "targetDrinkCount", "trainTimesWeek", "trainMinute", "targetTrainTimesWeek", "targetTrainMinute", "salt", "cure", "targetSalt", "psychologyChange", "obeyDoctor", "medicine", "medicineBadEffect", "visitEvaluate", "nextDate", "visitDoctor","medicineNot"];
                //无需服药的时候
                if(cure == 2) {
                    this.delCheckList(this.standardTypes,["medicine","medicineBadEffect"]);
                }
				// 治疗方式为：服药， 且服药依从性为：规律或间断时，【用药情况】未填写
				if(cure == 1 || medicine == 2) {
					if(YYQK == '') {
						this.specificationReason.push("【用药情况】未填写")
					}
				}
				// 随访分类为：控制不满意/不良反应/并发症时，下次随访日期不在两周内
				if((visitType == '2' || visitType == '3' || visitType == '4') && nextDate != '') {
					var operatingFormatter = this.operatingFormatter(visitDate,nextDate);
					if(operatingFormatter){
						this.specificationReason.push("下次随访日期不在两周内");
					}
				}
				// 存在危急情况（血压≥180/110mmHg）时，【转诊原因、转诊科别】未填写
				if(constriction >= 180 || diastolic >= 110) {
					var referralReason = document.getElementById("referralReason" + this.idPostfix).value;
					var referralOffice = document.getElementById("agencyAndDept" + this.idPostfix).value;
					if(referralReason=='' || referralOffice=='') {
						this.specificationReason.push("存在危急情况（血压≥180/110mmHg），【转诊原因】或【转诊科别】未填写");
					}
				}else if(isNaN(constriction)){
					this.specificationReason.push("收缩压未填写");
				}else if(isNaN(diastolic)){
					this.specificationReason.push("舒张压未填写");
				}

			}
            var e = this.getFormData();
            this.calPercent(e);
            this.createWarnWord(e);
			this.visitDatehandle(type);

		},
		visitDatehandle:function(type){
			const that= this;
			util.rmi.jsonRequest({
				serviceId: "chis.hypertensionVisitService",
				op: this.op,
				schema: this.entryName,
				serviceAction: "getLastVistRecordVisitId",
				method: "execute",
				empiId: this.exContext.ids.empiId,
				startDate: this.getDate(this.visitDate.getValue(), "Y-m-d"),
				visitId: this.exContext.args.visitId
			}, function (code, msg, json) {
				if (code > 300) {
                    that.calStandardPercent(type);
					return
				}
                that.specificationReason2=[];
				if (json.body) {
					var visitType = json.body.visitEvaluate;
					var referralReason = document.getElementById("referralReason" + that.idPostfix).value;
					var referralOffice = document.getElementById("agencyAndDept" + that.idPostfix).value;
					var nowVisitType =that.getHtmlFldValue("visitEvaluate");
					var visitDate = document.getElementsByName('visitDate'+that.idPostfix)[0].value

					// 上次随访【随访分类=控制不满意/不良反应/并发症】时，本次随访未在两周内进行
					if(visitType && visitType != 1) {
						var result = that.operatingFormatter(json.body.visitDate,visitDate);
						if(result){
							that.specificationReason2.push("上次随访为：控制不满意/不良反应/并发症，本次随访未在两周内进行");
						}
					}

					if(nowVisitType != ''){
						// 连续2次的【随访分类=控制不满意/不良反应】时，【转诊原因、转诊科别】未填写
						if(nowVisitType != 1){	// 本次随访分类不为满意时
							if(visitType && (visitType == 2 || visitType == 3) ) { // 上次随访分类不为满意时
								if (referralReason == '' || referralOffice == '') {
									that.specificationReason2.push("连续2次的【随访分类=控制不满意/不良反应】时，【转诊原因】或【转诊科别】未填写");
								}
							}
						}
					}

				}
                that.calStandardPercent(type);
			});
		},

		clearCheckBoxValues: function(id) {
			var vvv = document.getElementsByName(id + this.idPostfix);
			var lee = vvv.length;
			for (var j1 = 0; j1 < lee; j1++) {
				if (vvv[j1].checked) {
					vvv[j1].checked = false;
				}
			}
		},

		/**
		 * 高血压随访保存校验
		 * @param entryName
		 * @param op
		 * @param saveData
		 * @returns {*|boolean|boolean}
		 */
		onAboutToSave: function(entryName, op, saveData) {
            if(saveData.visitDoctor == ""){
                MyMessageTip.msg("提示", "随访医生不能为空", true);
                return false;
            }
            if("create" == op){
                saveData["visitId"] = "";
            }
			saveData["empiId"] = this.empiId;
			saveData["phrId"] = this.phrId;
			saveData["lastVisitId"] = this.lastVisitId;
			saveData["nextPlanId"] = this.nextPlanId;
			saveData["manaUnitId"] = this.manaUnitId;
			saveData["height"] = this.height;
			saveData["planDate"] = this.planDate;
			saveData["planId"] = this.planId;
			saveData["endDate"] = this.endDate;
			saveData["beginDate"] = this.beginDate;
			saveData["fixGroupDate"] = this.exContext.args.fixGroupDate
			saveData["sn"] = this.sn;
			if (this.planMode == "2" && saveData["visitEffect"] != "9") {
				var now = Date.parseDate(this.mainApp.serverDate, "Y-m-d");

				if (!this.nextDateDisable && saveData["nextDate"] <= now) {
					MyMessageTip.msg("提示", "预约日期必须大于当前日期", true);
					return false;
				}
			}
			if (this.hypertensionGroup) {
				saveData["hypertensionGroup"] = this.hypertensionGroup.key;
			}
			var constrictionF = document.getElementById("constriction" +
				this.idPostfix);
			var constriction = constrictionF.value;
			var diastolicF = document.getElementById("diastolic" +
				this.idPostfix);
			var diastolic = diastolicF.value;
			/*var updateReason = document.getElementById("updateReason" + this.idPostfix)
			var updateReasonVal = updateReason.value;
			saveData.updateReason = updateReasonVal;*/
			if (constriction && diastolic &&
				parseInt(constriction) <= parseInt(diastolic)) {
				this.addClass(constrictionF, "x-form-invalid");
				this.addClass(diastolicF, "x-form-invalid");
				MyMessageTip.msg("提示", "收缩压应大于舒张压", true);
				return false;
			}
			if (saveData["visitEffect"] && saveData["visitEffect"] != 1 &&
				!saveData["noVisitReason"]) {
				document
					.getElementsByName("noVisitReason" + this.idPostfix)[0]
					.focus();
				document
					.getElementsByName("noVisitReason" + this.idPostfix)[0]
					.select();
				MyMessageTip.msg("提示", "(暂时失访/终止管理)原因不能为空", true);
				return false;
			}

			// 收缩压 constriction  舒张压 diastolic  此次随访分类 div_visitEvaluate
			var visitEvaluate = this.getFormData().visitEvaluate;
			if(visitEvaluate == "1"){
				var ages = this.exContext.empiData.age;
				if(ages>=65){
					if(constriction>150 || diastolic>90){
						MyMessageTip.msg("提示", "老年人血压超过150/90 mmHg，本次随访分类为控制不满意", true);
						return false;
					}
				}else{
					if(constriction>140 || diastolic>90){
						MyMessageTip.msg("提示", "一般高血压患者血压超过140/90 mmHg，本次随访分类为控制不满意", true);
						return false;
					}
				}

			}

			/*if(op == 'update'){
				if(saveData["visitEffect"] == 1 && !saveData["riskUpdateReason"]){
					MyMessageTip.msg("提示", "修改原因不能为空", true);
					return false;
				}
			}*/
			//让不规律服药原因为空时可以正常保存
			/*if (saveData["medicine"] && saveData["medicine"] != 1
					&& !saveData["medicineNot"]) {
				document.getElementsByName("medicineNot" + this.idPostfix)[0]
						.focus();
				document.getElementsByName("medicineNot" + this.idPostfix)[0]
						.select();
				MyMessageTip.msg("提示", "不规律服药原因不能为空", true);
				return false;
			}*/
			if (saveData["medicineNot"] && saveData["medicineNot"] == 99 &&
				!saveData["medicineOtherNot"]) {
				document
					.getElementById("medicineOtherNot" + this.idPostfix)
					.focus();
				document
					.getElementById("medicineOtherNot" + this.idPostfix)
					.select();
				MyMessageTip.msg("提示", "其他不规律服药原因不能为空", true);
				return false;
			}
			if (saveData["noVisitReason"] && saveData["noVisitReason"] == 99 &&
				!saveData["noVisitReasonOtherNot"]) {
				document
					.getElementById("noVisitReasonOtherNot" + this.idPostfix)
					.focus();
				document
					.getElementById("noVisitReasonOtherNot" + this.idPostfix)
					.select();
				MyMessageTip.msg("提示", "转归其他原因不能为空", true);
				return false;
			}
			if (this.mainApp.exContext.hypertensionMode == 2 &&
				!this.nextDate.getValue()) {
				if (saveData["visitEffect"] == 9) {
					return true
				}
				MyMessageTip.msg("提示", "下次随访日期不能为空。", true);
				return false;
			}

			//zhucc,添加下次随访日期不能选当前随访日期之前的日期判断
			if (this.mainApp.exContext.hypertensionMode == 2 &&
				saveData["nextDate"] <= saveData["visitDate"]) {
				if (saveData["visitEffect"] == 9) {
					return true
				}
				MyMessageTip.msg("提示", "下次随访日期不能小于当前随访日期。", true);
				return false;
			}
			if (saveData["visitEffect"] != 2 &&
				saveData["visitEffect"] != 3) {
				return this.htmlFormSaveValidate(this.schema);
			}
		},

		htmlFormSaveValidate: function(schema) {
			if (!schema) {
				schema = this.schema;
			}
			var validatePass = true;
			var items = schema.items
			var n = items.length
			for (var i = 0; i < n; i++) {
				var it = items[i]
				if (this.isCreateField(it.id)) {
					var isLawful = true;
					isLawful = eval("this." + it.id + ".validate()");
					if (!isLawful) {
						validatePass = false;
						eval("this." + it.id + ".focus(true,200)");
						// console.log("-->this." + it.id + "验证未通过。");
						MyMessageTip.msg("提示", it.alias + "为必填项", true);
						this.createFldSaveValidate(it.id);
						break;
					}
					if (validatePass == false) {
						break;
					}
				} else {
					if (it.dic) {
						if (it['not-null'] == "1" ||
							it['not-null'] == "true") {
							var dfv = this.getHtmlFldValue(it.id);
							var divId = "div_" + it.id + this.idPostfix;
							var div = document.getElementById(divId);
							if (div) {
								if (dfv && dfv.length > 0) {
									this.removeClass(div, "x-form-invalid");
									div.title = "";
								} else {
									this.addClass(div, "x-form-invalid");
									div.title = it.alias + "为必填项";
									validatePass = false;
									document.getElementsByName(it.id +
										this.idPostfix)[0].focus();
									// console.log("-->"+it.id+"
									// "+it.alias+" 为必填项")
									MyMessageTip.msg("提示", div.title, true);
									break;
								}
							}
						}
					} else {
						var fld = document.getElementById(it.id +
							this.idPostfix);
						if (!fld) {
							continue;
						}
						if ((it['not-null'] == "1" || it['not-null'] == "true") &&
							!it["pkey"]) { // 跳过主键必填验证
							if (fld.value == "" ||
								(fld.value == fld.defaultValue && !fld.hidden)) {
								this.addClass(fld, "x-form-invalid");
								fld.title = it.alias + "为必填项";
								validatePass = false;
								if (!document.getElementById(it.id +
										this.idPostfix)) {
									continue;
								}
								document.getElementById(it.id +
									this.idPostfix).focus();
								document.getElementById(it.id +
									this.idPostfix).select();
								// console.log("-->"+it.id+" "+it.alias+"
								// 为必填项")
								MyMessageTip.msg("提示", fld.title, true);
								break;
							} else {
								this.removeClass(fld, "x-form-invalid");
								fld.title = it.alias
							}
						}
						var obj = fld;
						switch (it.type) {
							case "string":
								var maxLength = it.length;
								var fv = fld.value;
								var fvLen = this.getStrSize(fv);
								if (fvLen > maxLength) {
									this.addClass(obj, "x-form-invalid")
									obj.title = it.alias +
										"中输入的字符串超出定义的最大长度（" +
										maxLength + "）";
									validatePass = false;
									if (!document.getElementById(it.id +
											this.idPostfix)) {
										continue;
									}
									document.getElementById(it.id +
										this.idPostfix).focus();
									document.getElementById(it.id +
										this.idPostfix).select();
									// console.log("-->"+it.id+"
									// "+it.alias+"中输入的字符串超出定义的最大长度（"+maxLength+"）")
									MyMessageTip.msg("提示", obj.title, true);
								} else {
									this.removeClass(obj, "x-form-invalid");
									obj.title = it.alias
								}
								break;
							case 'int':
								var maxValue = it.maxValue;
								var minValue = it.minValue;
								var fv = obj.value;
								if (fv == obj.defaultValue) { // 跳过注释文字验证
									continue;
								}
								var reg = new RegExp("^[0-9]*$");
								var fid = it.id;
								if (!reg.test(fv)) {
									this.addClass(obj, "x-form-invalid")
									obj.title = it.alias + "中输入了非整数 数字或字符"
									validatePass = false;
									if (!document.getElementById(it.id +
											this.idPostfix)) {
										continue;
									}
									document.getElementById(it.id +
										this.idPostfix).focus();
									document.getElementById(it.id +
										this.idPostfix).select();
									// console.log("-->"+it.id+"
									// "+it.alias+"中输入了非整数 数字或字符")
									MyMessageTip.msg("提示", obj.title, true);
									continue;
								} else {
									this.removeClass(obj, "x-form-invalid");
									obj.title = it.alias
								}
								if (typeof(minValue) != 'undefined') {
									if (parseInt(fv) < minValue) {
										this
											.addClass(obj,
												"x-form-invalid")
										obj.title = it.alias +
											"中输入的值小于了定义的最小值（" +
											minValue + "）";
										validatePass = false;
										if (!document.getElementById(it.id +
												this.idPostfix)) {
											continue;
										}
										document.getElementById(it.id +
											this.idPostfix).focus();
										document.getElementById(it.id +
											this.idPostfix).select();
										// console.log("-->"+it.id+"
										// "+it.alias+"中输入的值小于了定义的最小值（"+minValue+"）")
										MyMessageTip.msg("提示", obj.title,
											true);
										continue;
									} else {
										this.removeClass(obj,
											"x-form-invalid");
										obj.title = it.alias
									}
								}
								if (typeof(maxValue) != 'undefined') {
									if (parseInt(fv) > maxValue) {
										this
											.addClass(obj,
												"x-form-invalid")
										obj.title = it.alias +
											"中输入的值大于了定义的最大值（" +
											maxValue + "）";
										validatePass = false;
										if (!document.getElementById(it.id +
												this.idPostfix)) {
											continue;
										}
										document.getElementById(it.id +
											this.idPostfix).focus();
										document.getElementById(it.id +
											this.idPostfix).select();
										// console.log("-->"+it.id+"
										// "+it.alias+"中输入的值大于了定义的最大值（"+maxValue+"）")
										MyMessageTip.msg("提示", obj.title,
											true);
										continue;
									} else {
										this.removeClass(obj,
											"x-form-invalid");
										obj.title = it.alias
									}
								}
								break;
							case "double":
								var length = it.length;
								var precision = it.precision;
								var maxValue = it.maxValue;
								var minValue = it.minValue;
								var dd = 0;
								if (typeof(precision) != 'undefined') {
									dd = parseInt(precision);
								}
								var iNum = length - dd;
								var regStr = "(^[0-9]{0," + iNum +
									"}$)|(^[0-9]{0," + iNum +
									"}(\\.[0-9]{0," + dd + "})?$)";
								if (dd == 0) {
									regStr = "(^[0-9]{0," + iNum +
										"}$)|(^[[0-9]*\\.[0-9]*]{0," +
										iNum + "}$)";
								}
								var reg = new RegExp(regStr);
								var fv = obj.value;
								if (fv == obj.defaultValue) { // 跳过注释文字验证
									continue;
								}
								if (typeof(minValue) != 'undefined') {
									if (fv < minValue) {
										this.addClass(obj, "x-form-invalid");
										obj.title = it.alias + "中输入的值小于了定义的最小值（" + minValue + "）"
										validatePass = false;
										if (!document.getElementById(it.id + this.idPostfix)) {
											continue;
										}
										document.getElementById(it.id + this.idPostfix).focus();
										document.getElementById(it.id + this.idPostfix).select();
										MyMessageTip.msg("提示", obj.title, true);
										continue;
									} else {
										this.removeClass(obj,
											"x-form-invalid");
										obj.title = it.alias
									}
								}
								if (typeof(maxValue) != 'undefined') {
									if (fv > maxValue) {
										this
											.addClass(obj,
												"x-form-invalid");
										obj.title = it.alias + "中输入的值大于了定义的最大值（" + maxValue + "）"
										validatePass = false;
										if (!document.getElementById(it.id + this.idPostfix)) {
											continue;
										}
										document.getElementById(it.id + this.idPostfix).focus();
										document.getElementById(it.id + this.idPostfix).select();
										MyMessageTip.msg("提示", obj.title, true);
										continue;
									} else {
										this.removeClass(obj,
											"x-form-invalid");
										obj.title = it.alias
									}
								}
								if (!reg.test(fv)) {
									this.addClass(obj, "x-form-invalid")
									obj.title = it.alias + "中输入了非浮点型数据或字符";
									if(precision && precision == 1){
										obj.title = it.alias + "只能保留一位小数"
									}
									if(precision && precision == 2){
										obj.title = it.alias + "只能保留两位小数"
									}
									validatePass = false;
									if (!document.getElementById(it.id + this.idPostfix)) {
										continue;
									}
									document.getElementById(it.id + this.idPostfix).focus();
									document.getElementById(it.id + this.idPostfix).select();
									MyMessageTip.msg("提示", obj.title, true);
									continue;
								} else {
									this.removeClass(obj, "x-form-invalid");
									obj.title = it.alias
								}
								break;
						}
						if (validatePass == false) {
							break;
						}
					}
				}
			}
			return validatePass;
		},
		onBeforeSave: function(entryName, op, saveData) {
			var isReturn = this.htmlFormSaveValidate(null);
			return isReturn;
		},
		saveToServer: function(saveData) {
			if(!this.fireEvent("beforeSave",this.entryName,this.op,saveData)){
				return;
			}
			if((saveData.drinkCount && isNaN(parseFloat(saveData.drinkCount))) || (saveData.targetDrinkCount && isNaN(parseFloat(saveData.targetDrinkCount)))) {
				MyMessageTip.msg('错误','日饮酒量（两）输入非数字类型',true);
				return
			}
			if(saveData.visitDate){
				this.nowDate = this.mainApp.serverDate
				if(saveData.visitDate>this.nowDate){
					MyMessageTip.msg('提示','随访日期不能大于当前日期',true)
					return
				}
			}
			//转归为继续随访的时候
			if(saveData.visitEffect && saveData.visitEffect == "1"){
				if ( saveData.cure && saveData.cure == "1") {
					if(!saveData.medicine) {
						MyMessageTip.msg("提示", "服药依从性不能为空", true);
						return;
					}
					if(saveData.medicine && (saveData.medicine == "2" || saveData.medicine == "3" )){
						if(!saveData.medicineNot) {
							MyMessageTip.msg("提示", "不规律服药原因不能为空", true);
							return;
						}
					}
					if(saveData.medicine && (saveData.medicine == "1" || saveData.medicine == "2" )){
						if(!saveData.medicineBadEffect) {
							MyMessageTip.msg("提示", "药物不良反应不能为空", true);
							return;
						}
					}
				}
			}
            //这个方法时用来获取完整率 不然保存会没有值
            this.handleCalPercent();
            this.nonStandard = "";
            this.callBackSaveData=saveData;
			this.doFinish(1);
			this.doStandard(1);
		},
        saveToServerVerifyAfter: function(saveData) {
            delete this.callBackSaveData;
			//回调
            if(!this.exContext.empiData.manaUnitId){
                this.exContext.empiData.manaUnitId=this.exContext.empiData.createUnit;
            }
            if(this.exContext.empiData.manaUnitId != this.mainApp.deptId){
                MyMessageTip.msg('提示','管辖机构非当前机构，无法操作此档案。',true)
                return
            }
            saveData.healthProposal = this.healths.value;
            //JCGWXTT-369 糖尿病随访跟高血压随访用药情况不应该删除
            /*if (this.medicine &&
                (this.medicine.key == "1" || this.medicine.key == "2") &&
                (saveData.medicine == "3" || saveData.medicine == "4")) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "当前操作会引起服药数据删除,是否继续?",
                    modal: true,
                    width: 300,
                    buttons: Ext.MessageBox.YESNO,
                    multiline: false,
                    fn: function(btn, text) {
                        if (btn == "no") {
                            return;
                        }
                        saveData.deleteMedicine = true;
                        this.executeSaveAction(saveData);
                    },
                    scope: this
                });
            } else {*/
                this.executeSaveAction(saveData);
            //}
        },

		executeSaveAction: function(saveData) {
			if (this.fireEvent("aboutToSave", this.entryName, this.op,
					saveData, this) == false) {
				this.form.el.unmask();
				return;
			}
			if (this.hypertensionGroup && this.hypertensionGroup.key) {
				saveData["hypertensionGroup"] = this.hypertensionGroup.key;
			}
			if (this.medicineIds) {
				saveData["medicineIds"] = this.medicineIds;
			}
			// 不规范
            if(this.nonStandard){
                if(this.nonStandard.length > 0) {
                    saveData["nonStandard"] = this.nonStandard;
                    saveData["standard"] = 0;
                    saveData["completeLevel"] = this.completeLevel;
                }else{
                    saveData["standard"] = 1;
                    saveData["completeLevel"] = this.completeLevel;
                    saveData["nonStandard"] = this.nonStandard;
                }
            }else{
                saveData["standard"] = 1;
                saveData["nonStandard"] = '';
				if(this.warnWord.length > 0){
					saveData["standard"] = 0;
					saveData["nonStandard"] = this.warnWord +"未填写";
				}
                if(this.specificationReason2.length > 0){
                    saveData["standard"] = 0;
                    saveData["nonStandard"] += this.specificationReason2;
                }
                if(this.specificationReason.length > 0){
                    saveData["standard"] = 0;
                    saveData["nonStandard"] += this.specificationReason;
                }
                saveData["completeLevel"] = this.completeLevel;
            }
			if (this.saveMedicine) {
				for (var i = 1; i < 6; i++) {
					if (!this.saveMedicine["drugNames" + i] ||
						this.saveMedicine["drugNames" + i] == "") {
						continue;
					}
					if (this.saveMedicine["medicineType" + i] &&
						this.saveMedicine["medicineType" + i].key) {
						this.saveMedicine["medicineType" + i] = this.saveMedicine["medicineType" +
							i].key
					}
					if (this.saveMedicine["medicineUnit" + i] &&
						this.saveMedicine["medicineUnit" + i].key) {
						this.saveMedicine["medicineUnit" + i] = this.saveMedicine["medicineUnit" +
							i].key
					}
					if (this.saveMedicine["everyDayTime" + i] &&
						this.saveMedicine["everyDayTime" + i].key) {
						this.saveMedicine["everyDayTime" + i] = this.saveMedicine["everyDayTime" +
							i].key
					}
				}
				Ext.apply(saveData, this.saveMedicine);
				this.saveMedicine = null;
			}
			if(this.loseWeight) {
				saveData["loseWeight"] = this.loseWeight.key;
			}
			saveData.planId = this.planId;
			this.fireEvent("saveToServer", this.op, saveData, this);
		},

		visitIdChange: function(visitId) {
			this.exContext.visitId = visitId;
			this.visitId = visitId;
		},

		doImport: function() {
			var module = this.midiModules["HyperClinicImportList"];
			var list = this.list;
			var cfg = {};
			cfg.empiId = this.exContext.args.empiId;
			if (!module) {
				var cls = "chis.application.hy.script.visit.HypertensionClinicList";
				$require(cls, [function() {
					var m = eval("new " + cls + "(cfg)");
					m.on("import", this.onImport, this);
					this.midiModules["HyperClinicImportList"] = m;
					list = m.initPanel();
					list.border = false;
					list.frame = false;
					this.list = list;

					var win = m.getWin();
					win.add(list);
					win.show();
				}, this]);
			} else {
				Ext.apply(module, cfg);
				module.requestData.cnd = ['eq', ['$', 'empiId'],
					['s', this.exContext.args.empiId]
				];
				module.loadData();
				var win = module.getWin();
				win.add(list);
				win.show();
			}
		},

		onImport: function(record) {
			if (!record) {
				return;
			}
			var height = document.getElementById("height" + this.idPostfix).value = record
				.get("height");
			this.height = record.get("height");
			var weightF = document
				.getElementById("weight" + this.idPostfix).value = record
				.get("weight");
			var weightF = document
				.getElementById("weight" + this.idPostfix)
			onWeightHyChange(record.get("weight"), document
				.getElementById("bmi" + this.idPostfix));
			var constriction = document.getElementById("constriction" +
				this.idPostfix).value = record.get("constriction");
			var diastolic = document.getElementById("diastolic" +
				this.idPostfix).value = record.get("diastolic");
		},

		addFieldAfterRender: function() {
			var curDate = Date.parseDate(this.mainApp.serverDate, "Y-m-d");
			this.visitDate = new Ext.form.DateField({
				name: 'visitDate' + this.idPostfix,
				width: 310,
				altFormats: 'Y-m-d',
				format: 'Y-m-d',
				altFormats: 'Ymd',
				emptyText: '随访日期',
				maxValue: curDate,
				allowBlank: false,
				invalidText: "必填字段",
				fieldLabel: "随访日期",
				renderTo: Ext
					.get("div_visitDate" + this.idPostfix)
			});
			this.nextDate = new Ext.form.DateField({
				name: 'nextDate' + this.idPostfix,
				width: 310,
				altFormats: 'Y-m-d',
				format: 'Y-m-d',
				altFormats: 'Ymd',
				emptyText: '下次随访日期',
				//minValue : curDate,
				// allowBlank:false,
				// invalidText : "必填字段",
				fieldLabel: "下次随访日期",
				renderTo: Ext.get("div_nextDate" + this.idPostfix)
			});
			this.visitDoctor = this.createDicField({
				"width": 310,
				"defaultIndex": 0,
				"id": "chis.dictionary.user",
				"render": "Tree",
				"selectOnFocus": true,
				"onlySelectLeaf": true,
				"parentKey": "%user.manageUnit.id",
				"defaultValue": {
					"key": this.mainApp.uid,
					"text": this.mainApp.uname
				}
			});
			this.visitDoctor.render(Ext.get("div_visitDoctor" +
				this.idPostfix));
			thisPanel = this;

			//				var serverDate = this.mainApp.serverDate;
			//				this.visitDate.maxValue = Date.parseDate(serverDate, "Y-m-d");
			//				this.nextDate.minValue = Date.parseDate(serverDate, "Y-m-d");
		},doSynchronized: function () {
			Ext.Msg.show({
				title: "确认同步",
				msg: "糖尿病随访的血压、体重、体质指数、日吸烟量、日饮酒量、运动到高血压随访（已建），确定是否继续？",
				modal: !0,
				width: 300,
				buttons: Ext.MessageBox.YESNO,
				multiline: !1,
				fn: function (e, t) {
					"yes" == e && this.synchronizedShowView()
				},
				scope: this
			})
		},synchronizedShowView: function () {
			var e = {};
			e.empiId = this.exContext.args.empiId, util.rmi.jsonRequest({
				serviceId: "chis.hypertensionVisitService",
				serviceAction: "synchronizedDiabetesVisitData",
				method: "execute",
				body: e
			}, function (e, t, i) {
				if (300 < e) this.processReturnMsg(e, t); else if (i.body) {
					var s = i.body;
					this.setValueById("constriction", s.constriction), this.setValueById("diastolic", s.diastolic), this.setValueById("weight", s.weight || ""), this.setValueById("bmi", s.bmi || ""), this.setValueById("targetWeight", s.targetWeight || ""), this.setValueById("targetBmi", s.targetBmi || ""), this.setValueById("smokeCount", s.smokeCount), this.setValueById("targetSmokeCount", s.targetSmokeCount || ""), this.setValueById("drinkCount", s.drinkCount), this.setValueById("targetDrinkCount", s.targetDrinkCount || ""), this.setValueById("trainTimesWeek", s.trainTimesWeek), this.setValueById("targetTrainTimesWeek", s.targetTrainTimesWeek || "0"), this.setValueById("trainMinute", s.trainMinute || "0"), this.setValueById("targetTrainMinute", s.targetTrainMinute || "0")
				}
			}, this)
		},
        delCheckList: function(commonField,delList) {
			//删除列表中用于检测必填的字段
            for(var i1=commonField.length-1;i1>=0;i1--){
                for(var j1=0;j1<delList.length;j1++){
                    if(commonField[i1]==delList[j1]){
                        delete commonField[i1];
                        break;
                    }
                }
            }
        },
		doCheckInput: function() {
			var e = this.commonFieldContent(),
				t = this.getFormData();
            var delList=[];
            if(t.visitEffect=="1"){
                //JWGDSXM-1787【责任医生】角色【高血压随访】，空项检测，应该不包括：辅助检查，转诊原因和科别字段的检测
				delList=["auxiliaryCheck","agencyAndDept","referralReason"];
            }
            if(t.cure=="2"){
                delList.push("medicine");
            }
            /*for(var i1=e.length-1;i1>=0;i1--){
                for(var j1=0;j1<delList.length;j1++){
                    if(e[i1]==delList[j1]){
                        delete e[i1];
                        break;
                    }
                }
            }*/
            this.delCheckList(e,delList);
            //JWGDSXM-1772 失访时应只检测必填项
			if(t.visitEffect=="2"||t.visitEffect=="9"){
				e=["noVisitReason","nextDate"];
			}

			var i = this.checkIsBlank(t, e),
				l = [],
				a = this.schema.items;
			if (null != i && 0 < i.length) {
				for (var s = 0; s < i.length; s++) for (var d = i[s], n = 0; n < a.length; n++) d == a[n].id && l.push(a[n].alias);
				//Ext.MessageBox.alert("提示以下信息空项", l)
                //空项检测提示
                MyMessageTip.msg("温馨提示","<strong style='color:red'>以下信息未填写：</strong><br>"+l,true);
			} else Ext.MessageBox.alert("提示", "未检测到有空项！")
		},commonFieldContent: function() {
			return this.completeTypes;
		},checkIsBlank: function (e, t) {
			for (var i = t, l = [], a = null, s = 0; s < i.length; s++) a = i[s], e.JY && ("string" == typeof e.JY[a] ? null != e.JY[a] && 0 != e.JY[a].length || l.push(a) : "object" == typeof e.JY[a] && (null != e.JY[a] && "" != e.JY[a].key || l.push(a))), e.jbxx && ("string" == typeof e.jbxx[a] ? null != e.jbxx[a] && 0 != e.jbxx[a].length || l.push(a) : "object" == typeof e.jbxx[a] && (null != e.jbxx[a] && "" != e.jbxx[a].key || l.push(a))), e.fsjl ? "string" == typeof e.fsjl[a] ? null != e.fsjl[a] && "0" != e.fsjl[a] && 0 != e.fsjl[a].length || l.push(a) : "object" == typeof e.fsjl[a] && (null != e.fsjl[a] && "" != e.fsjl[a] && "" != e.fsjl[a].key || l.push(a)) : e.SF ? "string" == typeof e.SF[a] ? null != e.SF[a] && 0 != e.SF[a].length || l.push(a) : "object" == typeof e.SF[a] && (null != e.SF[a] && "" != e.SF[a].key || l.push(a)) : "string" == typeof e[a] ? "chis.application.mhc.schemas.MHC_BabyVisitInfo_text" == this.entryName || "chis.application.mhc.schemas.MHC_BabyVisitRecord_html" == this.entryName ? null != e[a] && "0" != e[a] && 0 != e[a].length || l.push(a) : null != e[a] && 0 != e[a].length || l.push(a) : "object" == typeof e[a] && (null != e[a] && "" != e[a] && "" != e[a].key || l.push(a));
			return l
		},
		doCopy:function(){
			util.rmi.jsonRequest({
				serviceId: "chis.hypertensionVisitService",
				serviceAction: "getLastAleadyVisit",
				method: "execute",
				body: {
					empiId: this.exContext.ids['empiId']
				}
			}, function(code, msg, json) {
				if (code > 300) {
					this.processReturnMsg(code, msg, this.loadData)
					return
				}

				if(json.body){
                    let data = json.body.visitInfo;
                    delete data['visitDate'];
                    delete data['nextDate'];
                    //data['_actions'].update = false;
                    data['visitId'] = null;
                    this.initFormData(data)
                }
			}, this)
		},
		handleCalPercent: function() {
			var e = this.getFormData();
			if(!e){
				return;
			}
			this.calPercent(e),
				this.warnWord = [];
			var t = this.schema.items;
			if (0 < this.focusFld.length) {
				for (i = 0; i < this.focusFld.length; i++) {
					var s = this.focusFld[i];
					for (j = 0; j < t.length; j++) s == t[j].id && this.warnWord.push(t[j].alias + " ")
				}
			}
			this.completeLevel = e.completePercent + "%";
		},
		//失访专用方法。。  type 区分按钮进来 1-完善度进来  2-规范进来   提示不一样
		doVisiteff29:function(type,flag){
			var visitEffect = this.getHtmlFldValue("visitEffect");
			var noVisitReason = this.getHtmlFldValue("noVisitReason");
			var xcsfrq = this.getHtmlFldValue("nextDate");
			xcsfrq=xcsfrq.replace("下次随访日期","");
			// 失访or终止管理规范
			var items = this.form.topToolbar.items.items;
			var itemsLength = items.length;
			for(var i = 0;i<itemsLength;i++){
				if(items[i].cmd == "standard"){
					if(visitEffect == 2){
						items[i].setText("<strong style='color:#f56c6c;font-weight: bold;' >未规范</strong>");
					}else if(visitEffect == 9){
						items[i].setText("<strong style='color:#548dcc;font-weight: bold;' >规范</strong>");
					}
				}
				if(items[i].cmd == "finish"){
					var msg1 = "以下信息需要补充:"
					//判断完善度 按钮
					if((xcsfrq == "" && visitEffect != 9) && noVisitReason == ""){
						// this.completePercent = "94%";
						// items[i].setText(("完善度94%"));
						if(type == 1){
							msg1 += "转归原因,下次随访日期";
						}
					}else if(noVisitReason == ""){
						// this.completePercent = "96%";
						// items[i].setText(("完善度96%"));
						if(type == 1){
							msg1 += "转归原因";
						}
					}else if(xcsfrq == "" && visitEffect != 9){
						// this.completePercent = "96%";
						// items[i].setText(("完善度96%"));
						if(type == 1){
							msg1 += "下次随访日期";
						}
					}else {
						// items[i].setText(("完善度100%"));
						if(type == 1){
							msg1 = "基本信息完整，无需补充！";
						}
					}
                    if(flag != undefined && !flag){
                        return
                    }
					if(type == 1){
						MyMessageTip.msg("温馨提示", msg1,true);
						continue;
					}
					//提示
					if(type == 2){
						var msg = "<strong style='color:red'>不符合糖尿病随访规范，未规范原因为：</strong></br> ";// "未填写";
						//失访单独判断
						if(visitEffect == 2){
							msg += "失访";
							MyMessageTip.msg("温馨提示", msg,true);
						}
						//终止管理
						if(visitEffect == 9){
							//永远规范 不用提示
							MyMessageTip.msg("温馨提示", "<strong style='color:#548dcc'>当前业务数据符合规范</strong>",true);
						}
					}
				}
			}
		},
		doFinish: function(type,flag) {
			//完善度提示
			var e = this.getFormData();
			if(!e){
				return;
			}
            var visitEffect = this.getHtmlFldValue("visitEffect");
            var noVisitReason = this.getHtmlFldValue("noVisitReason");
            if(visitEffect){
                this.visitEffectVlue = visitEffect;
            }
            /*if(this.visitEffectVlue && (this.visitEffectVlue == 2||this.visitEffectVlue == 9)) {
				this.doVisiteff29(1,flag);
				return;
			}*/
			this.calPercent(e);
            this.createWarnWord(e);
			this.visitDatehandle(1);

			if(type != 1){
				if(this.warnWord.length == 0){
					MyMessageTip.msg("温馨提示","基本信息完整，无需补充！",true);
					// Ext.MessageBox.alert("提示", "基本信息完整，无需补充！");
				}else{
					MyMessageTip.msg("温馨提示","以下信息需要补充:" + this.warnWord + "!",true);
					// Ext.MessageBox.alert("提示", "以下信息需要补充:" + this.warnWord + "!");
				}
			}
		},
		createWarnWord: function(e){
            this.warnWord = [];
            this.warnWordCode = [];
            var cure = this.getHtmlFldValue("cure");
			var medicine = this.getHtmlFldValue("medicine");
            var t = this.schema.items;
            if (0 < this.focusFld.length) {
                for (var i = 0; i < this.focusFld.length; i++) {
                    var s = this.focusFld[i];
                    if(cure == 1 && s == "YYQK"){
                        this.warnWord.push("用药情况");
                        this.warnWordCode.push(this.focusFld[i]);
                    }
                    for (var j = 0; j < t.length; j++){
                        if(s == t[j].id) {
                            if(e.cure=="2"&&t[j].id=="medicine")continue;
                            this.warnWord.push(t[j].alias);
                            this.warnWordCode.push(t[j].id);
                        }
                    }
                }
            }
        },
		calPercent: function(e){
            var t = this.completeTypes,
                itemNum = 0;
			var cure = this.getHtmlFldValue("cure");
			var visitEffect = this.getHtmlFldValue("visitEffect");
			if(visitEffect == 2) {
				t = ["visitDate", "visitDoctor","noVisitReason", "diabetesType", "nextDate"]
			}else if(visitEffect == 9){
				t = ["visitDate", "visitDoctor","noVisitReason", "diabetesType"]
			}
			var o = t.length;
            this.focusFld = [];
            //循环判断未填写字段
            for (var s = 0; s < t.length; s++) {
                var a = t[s];
                if(a == "medicineBadEffect" && (cure == 2 || cure == "")){
                	//永远当做未填写 下面就去-1
					(this.focusFld.push(a), itemNum++)
					continue;
				}
                if(typeof e[a] == undefined){
                    (this.focusFld.push(a), itemNum++)
                    continue;
                }
                "string" == typeof e[a] ?
					null != e[a] && 0 != e[a].length || (this.focusFld.push(a), itemNum++) :
					"object" == typeof e[a] && (null != e[a] && "" != e[a].key || (this.focusFld.push(a), itemNum++))
            }

            var d = 100;
            var itemsLength = 0;
            if(this.form.topToolbar){
                var items = this.form.topToolbar.items.items;
                itemsLength = items.length;
            }
            if(cure == 2){
            	// 治疗方式为无需服药时，移除服药依从性、药物不良反应和用药情况，故需要减3
            	var sum  = 1 //移除用药情况，故初始化为1
				for(var i in this.focusFld){
					// 移除服药依从性
					if('medicine' == this.focusFld[i]){
						this.focusFld.splice(i,1)
						sum = sum + 1
						o = o - 1
						if(itemNum > 0){
							itemNum = itemNum - 1;
						}
					}
					// 移除药物不良反应
					if('medicineBadEffect' == this.focusFld[i] ){
						this.focusFld.splice(i,1)
						sum = sum + 1
						o = o - 1
						if(itemNum > 0){
							itemNum = itemNum - 1;
						}
					}
				}
            }else if(cure == 1){
            	// 服药依从性 medicine   间断/不服药 时候判断  不规律服药原因为必填
				var medicine = this.getHtmlFldValue("medicine");
				if(medicine && (medicine=="2" || medicine =="3")){
					//不规律服药原因 medicineNot
					//长度+1
					o = o+1;
					var medicineNot = this.getHtmlFldValue("medicineNot");
					if(!medicineNot || medicineNot == ""){
						this.focusFld.push("medicineNot");
						itemNum++;
					}
				}

				// 获取用药情况是否选中
				//用药情况 长度+1
				o = o+1;
				var YYQK = document.getElementById("YYQK"+this.idPostfix).textContent;
				if(!YYQK || YYQK == ""){
					this.focusFld.push("YYQK")
					itemNum++;
				}


			}
            // // 控制满意移除转归：机构及科别、转诊原因
            // if(1 == this.getHtmlFldValue('visitEvaluate')){
			// 	var sum  = 0
			// 	for(var i in this.focusFld){
			// 		// 移除机构及科别
			// 		if('agencyAndDept' == this.focusFld[i]){
			// 			this.focusFld.splice(i,1)
			// 			sum = sum + 1
			// 			itemNum--;
			// 		}
			// 		// 移除转诊原因
			// 		if('referralReason' == this.focusFld[i] ){
			// 			this.focusFld.splice(i,1)
			// 			sum = sum + 1
			// 			itemNum--;
			// 		}
			// 	}
			// 	o = o - sum
			// }else {
            // 	// 控制不为满意的其他情况，需要增加转归：机构及科别、转诊原因
			// 	itemNum = itemNum + 2
			// }
            if(itemNum == 0){
				e.completePercent = d;
                for(var i = 0;i<itemsLength;i++){
                    if(items[i].cmd == "finish"){
                        //修改完整率按钮
                        items[i].setText(("完善度100% (" + o + "/" + o + ")"));
                    }
                }
            }
            if (itemNum >= 1  ) {
                var n = o - itemNum;
                d = Math.floor(n / o * 100),e.completePercent = d;
                for(var i = 0;i<itemsLength;i++){
                    if(items[i].cmd == "finish"){
                        //修改完整率按钮
                        items[i].setText(("完善度" + d + "% (" + n + "/" + o + ")"));
                    }
                }
            }
		},

		validate: function() {},

		setBtnable: function() {
			if (!this.form.getTopToolbar()) {
				return;
			}
			var btns = this.form.getTopToolbar().items;
			if (!btns.item(0)) {
				return;
			}
			var rdStatus = this.exContext.ids.recordStatus;
			if (rdStatus && rdStatus == '1') {
				for (var i = 0; i < btns.getCount(); i++) {
					var btn = btns.item(i);
					if (btn) {
						btn.disable();
					}
				}
			}
		},

		doNew: function() {
			this.medicines = null;
			this.medicineIds = {};
			this.healths = {
				value: "",
				JKCF: ""
			};
			document.getElementById("JKCF" + this.idPostfix).innerHTML = "";
			this.op = "create"
			if (this.data) {
				this.data = {}
			}
			//清空修改原因
			var f = document.getElementById("updateReason" + this.idPostfix)
			if (f) {
				f.value = '';
			}
			var f = document.getElementById("noVisitReasonOtherNot" + this.idPostfix)
			if (f) {
				f.value = '';
			}
			for (var s = 0, sLen = this.schemas.length; s < sLen; s++) {
				var items = this.schemas[s].items;
				var n = items.length
				for (var i = 0; i < n; i++) {
					var it = items[i]
					if (this.isCreateField(it.id)) {
						// 对自创建手动创建的字段在新建里赋初值，赋值后返回true，否则字段会设置为空值
						var isSet = this.setMyFieldValue(it.id);
						if (!isSet) {
							eval("this." + it.id + ".setValue()");
						}
					} else {
						if (it.dic) {
							var fs = document.getElementsByName(it.id +
								this.idPostfix);
							if (fs && fs.length > 0) {
								for (var j = 0, len = fs.length; j < len; j++) {
									var f = fs[j];
									if (f.type == "checkbox" ||
										f.type == "radio") {
										if (f.checked) {
											f.checked = false;
											eval("this." + it.id + this.idPostfix + "Value=\"\"");
										}
									}
								}
							}
						} else {
							var f = document.getElementById(it.id +
								this.idPostfix)
							if (f) {
								f.value = f.defaultValue || '';
								if (f.defaultValue) {
									f.style.color = "#999"; // 填充注释文字，设灰色
								}
							}
						}
					}
				}
			}
			document.getElementById("YYQK" + this.idPostfix).innerHTML = "";
			//onMedicineClick(null);
			this.fieldValidate(this.schema);
			this.initData(this.exContext.args);
			this.fireEvent("doNew", this.form);
		},
		medicineModuleSave: function(medicines, values) {
			this.saveMedicine = values;
			this.medicines = medicines;
			if (medicines) {
				var YYQK = medicines.YYQK;
				document.getElementById("YYQK" + this.idPostfix).innerHTML = YYQK;
			}
		},
		healthModuleSave: function(healths, values) {
			this.saveHealths = values;
			this.healths = healths;
			if (healths) {
				var JKCF = healths.JKCF;
				document.getElementById("JKCF" + this.idPostfix).innerHTML = JKCF;
			}
		},

		getHtmlFldValue: function(fldName) {
			var fldValue = "";
			var flds = document.getElementsByName(fldName + this.idPostfix);
			var vs = [];
			for (var i = 0, len = flds.length; i < len; i++) {
				var f = flds[i];
				if (f.type == "text" || f.type == "hidden") {
					vs.push(f.value || '');
				}
				if (f.type == "radio" || f.type == "checkbox") {
					if (f.checked) {
						vs.push(f.value);
					}
				}
			}
			fldValue = vs.join(',');
			return fldValue;
		},
		onReady: function() {
			this.addFieldAfterRender();
			// 字段校验
			//this.initFieldDisable();
			this.addFieldDataValidateFun(this.schema);
			this.controlOtherFld();
			this.mutualExclusionSet();
			this.setOtherFldValuesHandle();
			// 扩展
			// this.onReadyAffter();
			this.addKeyEvent();
			var that = this;
			var cure1 = "cure1"+this.idPostfix;
			var $cure1 = $('#'+cure1);
			$cure1.on("click", function(a,b,c){
				var YYQKBtn = document.getElementById("YYQKBtn" + that.idPostfix);
				var BGLYY = document.getElementById("BGLYY" + that.idPostfix);
				var medicineNot = document.getElementsByName("medicineNot" + that.idPostfix);
				var medicineOtherNot = document.getElementById("medicineOtherNot" + that.idPostfix);
				var medicineBadEffectText = document.getElementById("medicineBadEffectText" + that.idPostfix);
				var noCureReason = document.getElementById("noCureReason" + that.idPostfix);
				var id = "medicineBadEffect";
				if(this.checked){
					// YYQKBtn.disabled = false;
				}else{
					YYQKBtn.disabled = true;
					document.getElementById("YYQK" + that.idPostfix).innerHTML = "";
					that.setHtmlFldValue("medicineNot" + that.idPostfix, -1);
					that.setRadioDisabled("medicineNot", true);
					that.setRadioDisabled2("medicine", true)
					that.setRadioValue(id, { key: "n" });
					that.setRadioDisabled(id, true)
					medicineBadEffectText.value = "";
					medicineBadEffectText.disabled = true;
					medicineOtherNot.value="";
					medicineOtherNot.disabled = true;
					noCureReason.disabled = true;
					noCureReason.value = "";
				}
			});
			var cure2 = "cure2"+this.idPostfix;
			var $cure2 = $('#'+cure2);
			$cure2.on("click", function(a,b,c){
				var YYQKBtn = document.getElementById("YYQKBtn" + that.idPostfix);
				var BGLYY = document.getElementById("BGLYY" + that.idPostfix);
				var medicineNot = document.getElementsByName("medicineNot" + that.idPostfix);
				var medicineOtherNot = document.getElementById("medicineOtherNot" + that.idPostfix);
				var medicineBadEffectText = document.getElementById("medicineBadEffectText" + that.idPostfix);
				var noCureReason = document.getElementById("noCureReason" + that.idPostfix);
				if(this.checked){
					YYQKBtn.disabled = true;
					document.getElementById("YYQK" + that.idPostfix).innerHTML = "";

				}else{
					noCureReason.disabled = true;
					noCureReason.value = "";
				}
			});
		},
		fldValidateCancel: function(fldId, alias, obj, me) {
			if (this.needRadioCancel) {
				if (obj.type == "radio") {
					var key = eval("this." + fldId + this.idPostfix + "Value");
					eval("this." + fldId + this.idPostfix + "Value='" + obj.value + "'");
					if (key == obj.value) {
						if (obj.checked) {
							obj.checked = false;
							eval("this." + fldId + this.idPostfix + "Value=\"\";");
						}
					}
				}
			}
		},
		addFieldDataValidateFun: function(schema) {
			if (!schema) {
				schema = this.schema;
			}
			var items = schema.items
			var n = items.length
			for (var i = 0; i < n; i++) {
				var it = items[i]
				if (!this.isCreateField(it.id)) {
					if (it.dic) {
						var dfs = document.getElementsByName(it.id + this.idPostfix);
						if (!dfs) {
							continue;
						}
						var notNull = false;
						if (it['not-null'] == "1" || it['not-null'] == "true") {
							notNull = true;
							var fv = this.getHtmlFldValue(it.id);
							var divId = "div_" + it.id + this.idPostfix;
							var fdiv = document.getElementById(divId);
							if (fv && fv.length > 0) {
								if (fdiv) {
									this.removeClass(fdiv, "x-form-invalid");
									fdiv.title = "";
								}
							} else {
								if (fdiv) {
									this.addClass(fdiv, "x-form-invalid");
									fdiv.title = it.alias + "为必选项!"
								}
							}
						}
						var me = this;
						for (var di = 0, dlen = dfs.length; di < dlen; di++) {
							var itemFld = dfs[di];
							var handleFun = function(fldId, alias, obj, me) {
								return function() {
									me.dicFldValidateClick(fldId, alias, obj, me);
									if (obj.type == "radio") {
										me.fldValidateCancel(fldId, alias, obj, me);
									}
								}
							}
							this.addEvent(itemFld, "click", handleFun(it.id, it.alias, itemFld, me));
						}

					} else {
						var fld = document.getElementById(it.id +
							this.idPostfix);
						if (!fld) {
							continue;
						}
						var notNull = false;
						if (it['not-null'] == "1" ||
							it['not-null'] == "true") {
							notNull = true;
							if (fld.value == "" ||
								fld.value == fld.defaultValue) {
								this.addClass(fld, "x-form-invalid");
							} else {
								this.removeClass(fld, "x-form-invalid");
							}
						}
						var me = this;
						switch (it.type) {
							case "string":
								var maxLength = it.length;
								var handleFun = function(maxLength,
									notNull, alias, obj, me) {
									return function() {
										me.validateString(maxLength,
											notNull, alias, obj, me);
									}
								}
								this.addEvent(fld, "change", handleFun(
									maxLength, notNull,
									it.alias, fld, me));
								break;
							case 'int':
								var maxValue = it.maxValue;
								var minValue = it.minValue;
								var length = it.length;
								var handleFun = function(length, minValue,
									maxValue, notNull, fid, alias, obj,
									me) {
									return function() {
										me.validateInt(length, minValue,
											maxValue, notNull, fid,
											alias, obj, me);
									}
								}
								this.addEvent(fld, "change", handleFun(
									length, minValue, maxValue,
									notNull, it.id, it.alias,
									fld, me));
								break;
							case "double":
								var length = it.length;
								var precision = it.precision;
								var maxValue = it.maxValue;
								var minValue = it.minValue;
								var handleFun = function(length, precision,
									minValue, maxValue, notNull, alias,
									obj, me) {
									return function() {
										me.validateDouble(length,
											precision, minValue,
											maxValue, notNull, alias,
											obj, me);
									}
								}
								this.addEvent(fld, "change", handleFun(
										length, precision,
										minValue, maxValue,
										notNull, it.alias,
										fld, me));
								break;
						}
					}
				}
			}
		},

		BPControl: function(fid, op, me) { // constriction diastolic
			return;
			var relative = "";
			if (fid == "constriction") {
				relative = "diastolic";
			} else {
				relative = "constriction";
			}
			var relObj = document.getElementById(relative + me.idPostfix);
			var rv = relObj.value;
			if (rv && rv != "") {
				var LBPid = fid + "_L" + me.idPostfix;
				var lo = document.getElementById(LBPid);
				var relo = document.getElementById(relative + "_L" +
					me.idPostfix);
				if (op == "add") {
					me.addClass(ro, "x-form-invalid");
					me.addClass(relo, "x-form-invalid");
				} else {
					me.removeClass(lo, "x-form-invalid");
					me.removeClass(relo, "x-form-invalid");
				}
			} else {
				me.addClass(relObj, "x-form-invalid");
			}
		},

		initHTMLFormData: function(data, schema) {
			if (!schema) {
				schema = this.schema;
			}
			this.beforeInitFormData(data); // ** 在将数据填充入表单之前做一些操作
			// Ext.apply(this.data, data)
			var items = schema.items
			var n = items.length
			for (var i = 0; i < n; i++) {
				var it = items[i]
				if (it.pkey) {
					this.initDataId = data[it.id];
				}
				if (this.isCreateField(it.id)) {
					var cfv = data[it.id]
					if (!cfv) {
						cfv = "";
					}
					if (it.type == "date") {
						if (typeof cfv != "string") {
							cfv = Ext.util.Format.date(cfv, 'Y-m-d');
						} else {
							cfv = cfv.substring(0, 10);
						}
					}
					eval("this." + it.id + ".setValue(cfv);this." + it.id +
						".validate();");
				} else {
					if (it.dic) {
						if (!this.fireEvent("dicFldSetValue", it.id, data)) {
							continue;
						} else {
							var dfs = document.getElementsByName(it.id);
							if (!dfs) {
								continue;
							}
							var dicFV = data[it.id];
							var fv = "";
							if (it.defaultValue) {
								fv = it.defaultValue.key;
							}
							if (dicFV) {
								fv = dicFV.key;
							}
							if (!fv) {
								continue;
							}
							var dvs = fv.split(",");
							for (var j = 0, len = dvs.length; j < len; j++) {
								var f = document.getElementById(it.id + "_" +
									dvs[j] + this.idPostfix);
								if (f) {
									f.checked = true;
									eval("this." + it.id + this.idPostfix + "Value=" + f.value);
								}
							}
							if (dvs.length > 0) {
								var div = document.getElementById("div_" +
									it.id + this.idPostfix);
								if (div) {
									this.removeClass(div, "x-form-invalid");
								}
							}
						}
					} else {
						var f = document.getElementById(it.id +
							this.idPostfix)
						if (f) {
							var v = data[it.id];
							if (!v) {
								v = f.defaultValue || "";
								if (f.defaultValue) {
									f.style.color = "#999";
								}
							} else {
								f.style.color = "#000"; // 不是注释文字，改黑色字体
							}
							f.value = v;
							if (it['not-null'] == "1" ||
								it['not-null'] == "true") {
								if (data[it.id] && data[it.id] != "") {
									this.removeClass(f, "x-form-invalid");
								}
							}
						}
					}
				}
			}
			this.setKeyReadOnly(true)
			// this.startValues = form.getValues(true);
			this.resetButtons(); // ** 用于页面按钮权限控制
			// this.focusFieldAfter(-1, 800);
		},

		getElementById: function(id) {
			return document.getElementById(id);
		},

		//N天前期间  eg 90天前 AddDayCount =-90; 90天后 AddDayCount= 90
		GetDateStr :function( date,AddDayCount) {
			var dd = new Date(date);
			dd.setDate(dd.getDate()+AddDayCount);//获取AddDayCount天后的日期
			var y = dd.getFullYear();
			var m = (dd.getMonth()+1)<10?"0"+(dd.getMonth()+1):(dd.getMonth()+1);//获取当前月份的日期，不足10补0
			var d = dd.getDate()<10?"0"+dd.getDate():dd.getDate();//获取当前几号，不足10补0
			return y+"-"+m+"-"+d;
		},

		checkDate :function(date1,date2){
			var oDate1 = new Date(date1);
			var oDate2 = new Date(date2);
			if(oDate1.getTime() < oDate2.getTime()){
				return true;
			} else {
				return false;
			}
		},

		operatingFormatter :function(date1,date2){
			if(this.checkDate(date1,this.GetDateStr(date2,-14))){
				return true;
			}else{
				return false;
			}
		},

		onBeforePrint: function(type, pages, ids_str) {
			if (!this.initDataId) {
				MyMessageTip.msg("提示", "请先保存记录。", true);
				return false;
			}
			pages.value = "chis.prints.htmlTemplate.hypertensionVisit";
			ids_str.value = "&phrId=" + this.exContext.ids.phrId +
				"&empiId=" + this.exContext.ids.empiId + "&visitId=" + this.data.visitId;
		},
        setImportJzInfoBtnState: function() {
            //JCGWXTT-491 设置【导入就诊数据】按钮状态
            /*
            1.1、若有过已随访记录，则默认查询在末次随访日期（处理日期）之后和当前日期之间（大于等于【末次随访日期】小于等于【当前日期】）的就诊记录：
            A、无记录：【导入就诊数据】按钮字体为黑色，hint为：上次随访后无新的就诊信息。
            B、有记录：【导入就诊数据】按钮字体为红色，hint为：上次随访后有新的就诊信息。
            1.2、若无随访记录，则默认查询最近3个月的就诊记录（大于等于【当前日期-3个月】小于等于【当前日期】）的就诊记录：
            A、无记录：【导入就诊数据】按钮字体为黑色，hint为：最近3个月无就诊信息。
            B、有记录：【导入就诊数据】按钮字体为红色，hint为：最近3个月有就诊信息。
            * */
            var planId="";
            if(this.exContext.args.planId){
                planId=this.exContext.args.planId;
            }
            if(planId==""&&this.exContext.args.r&&this.exContext.args.r.data&&this.exContext.args.r.data.planId){
                planId=this.exContext.args.r.data.planId;
            }
            if(planId!=""){
                //取末次随访日期
                var result = util.rmi.miniJsonRequestSync({
                    serviceId: "chis.CommonService",
                    method: "execute",
                    serviceAction: "getPubVisitPlanStartDate",
                    body: {
                        planId: planId
                    }
                });
                var visitDate=result.json.visitDate;
                var hasVisitPlan=result.json.hasVisitPlan;
                //取就诊记录
                var brid = this.exContext.ids.brid == undefined
                    ? null
                    : this.exContext.ids.brid;
                var list=new Array();
                if(brid!=null){
                    var cnd=['and', ['eq', ['$', 'a.BRBH'], ['i', brid]]];
                    cnd.push([
                        'ge',
                        ['$', "KSSJ"],
                        ['todate', ['s', visitDate + ' 00:00:00'],
                            ['s', 'yyyy-mm-dd hh24:mi:ss']]]);
                    result = util.rmi.miniJsonRequestSync({
                        serviceId: "phis.simpleQuery",
                        method: "execute",
                        schema:"phis.application.chis.schemas.YS_MZ_JZLS",
                        pageNo:1,
                        pageSize:10,
                        cnd:cnd
                    });
                    list=result.json.body;
                }
                var hintStr="";
                var btnColor=list.length==0?"black":"red";
                if(hasVisitPlan){
                    if(list.length==0){
                        hintStr="上次随访后无新的就诊信息";
                    }else{
                        hintStr="上次随访后有新的就诊信息";
                    }
                }else{
                    if(list.length==0){
                        hintStr="最近3个月无就诊信息";
                    }else{
                        hintStr="最近3个月有就诊信息";
                    }
                }
                var itemsLength = 0;
                if(this.form.topToolbar){
                    var items = this.form.topToolbar.items.items;
                    itemsLength = items.length;
                }
                for(var i = 0;i<itemsLength;i++) {
                    if (items[i].cmd == "importJzInfo") {
                        var btnEl=items[i].btnEl;
                        btnEl.setStyle("color", btnColor);
                        items[i].setTooltip(hintStr);
                    }
                }
            }
        },
	});

function onTextMustBlur(value, text) {
	if (!value || value == "") {
		thisPanel.addClass(text, "x-form-invalid");
	} else {
		thisPanel.removeClass(text, "x-form-invalid");
	}
}

function onVisitEffectHyChange(v, f, f1, f2, value) {
    thisPanel.visitEffectVlue = v;
	var div = document
		.getElementById("div_noVisitReason" + thisPanel.idPostfix);
	var divValue = thisPanel.getHtmlFldValue("noVisitReason");
	//注意这里要顺便把Temp.js的内容一起更改
	var changeItems = ["BGLYY", "visitEvaluate", "trainMinute",
		"trainTimesWeek", "drinkCount", "smokeCount",
		"diastolic", "constriction", "heartRate", "visitWay", "cure"
	];
	var disableField = ["this.visitDate", "constriction", "diastolic",
		"weight", "targetWeight", "smokeCount", "targetSmokeCount",
		"drinkCount", "targetDrinkCount", "trainTimesWeek","agencyAndDept","referralReason",
		"trainMinute", "targetTrainTimesWeek", "targetTrainMinute","riskUpdateReason",
		"food", "targetFood", "medicine", "visitWay", "fbs","auxiliaryCheck","medicineBadEffectText",
		"medicineNot", "tiz09", "bmi", "targetBmi","heartRate","otherSigns"
	];
	var noItems = "noVisitReason";
	var schema = this.schemas;
	if (!schema) {
		var re = util.schema
			.loadSync("chis.application.hy.schemas.MDC_HypertensionVisit_html")
		if (re.code == 200) {
			schema = re.schema;
		}
	}
	var items = schema.items;
	var size = items.length;
	if (v != 1) {
		for(var i = 0; i < disableField.length;i++) {
			var cfId = disableField[i] + thisPanel.idPostfix;
			var cf = document.getElementById(cfId);
			if (cf) {
				cf.disabled = true;
				if (cf.type == "text") {
					cf.value = "";
				}
				if (cf.type == "checkbox" || cf.type == "radio") {
					cf.checked = false;
				}
			}
		}
        var currentSymptoms = document.getElementsByName("currentSymptoms"+thisPanel.idPostfix);
        for(var i = 0; i < currentSymptoms.length;i++){
            currentSymptoms[i].disabled = true;
		}
        var salt = document.getElementsByName("salt"+thisPanel.idPostfix);
        for(var i = 0; i < salt.length;i++){
            salt[i].disabled = true;
        }
        var targetSalt = document.getElementsByName("targetSalt"+thisPanel.idPostfix);
        for(var i = 0; i < targetSalt.length;i++){
            targetSalt[i].disabled = true;
        }
        var psychologyChange = document.getElementsByName("psychologyChange"+thisPanel.idPostfix);
        for(var i = 0; i < psychologyChange.length;i++){
            psychologyChange[i].disabled = true;
        }
        var obeyDoctor = document.getElementsByName("obeyDoctor"+thisPanel.idPostfix);
        for(var i = 0; i < obeyDoctor.length;i++){
            obeyDoctor[i].disabled = true;
        }
        var visitEvaluate = document.getElementsByName("visitEvaluate"+thisPanel.idPostfix);
        for(var i = 0; i < visitEvaluate.length;i++){
            visitEvaluate[i].disabled = true;
        }
        var cure = document.getElementsByName("cure"+thisPanel.idPostfix);
        for(var i = 0; i < cure.length;i++){
            cure[i].disabled = true;
        }
        var medicineNot = document.getElementsByName("medicineNot"+thisPanel.idPostfix);
        for(var i = 0; i < medicineNot.length;i++){
            medicineNot[i].disabled = true;
        }
        var medicineBadEffect = document.getElementsByName("medicineBadEffect"+thisPanel.idPostfix);
        for(var i = 0; i < medicineBadEffect.length;i++){
            medicineBadEffect[i].disabled = true;
        }
        var medicine = document.getElementsByName("medicine"+thisPanel.idPostfix);
        for(var i = 0; i < medicine.length;i++){
            medicine[i].disabled = true;
        }
		for (var i = 0; i < size; i++) {
			var it = items[i];
			if (changeItems.indexOf(it.id) > -1) {
				it["not-null"] = 0;
			} else if (it.id == noItems) {
				it["not-null"] = 1;
			}
		}
		thisPanel.fieldValidate()
	} else {
		//移除disabled效果
		for (var i = 0, len = disableField.length; i < len; i++) {
			var cfId = disableField[i] + thisPanel.idPostfix;
			var cf = document.getElementById(cfId);
			if (cf) {
				cf.disabled = false;
			}
		}
        var currentSymptoms = document.getElementsByName("currentSymptoms"+thisPanel.idPostfix);
        for(var i = 0; i < currentSymptoms.length;i++){
            currentSymptoms[i].disabled = false;
        }
        var salt = document.getElementsByName("salt"+thisPanel.idPostfix);
        for(var i = 0; i < salt.length;i++){
            salt[i].disabled = false;
        }
        var targetSalt = document.getElementsByName("targetSalt"+thisPanel.idPostfix);
        for(var i = 0; i < targetSalt.length;i++){
            targetSalt[i].disabled = false;
        }
        var psychologyChange = document.getElementsByName("psychologyChange"+thisPanel.idPostfix);
        for(var i = 0; i < psychologyChange.length;i++){
            psychologyChange[i].disabled = false;
        }
        var obeyDoctor = document.getElementsByName("obeyDoctor"+thisPanel.idPostfix);
        for(var i = 0; i < obeyDoctor.length;i++){
            obeyDoctor[i].disabled = false;
        }
        var visitEvaluate = document.getElementsByName("visitEvaluate"+thisPanel.idPostfix);
        for(var i = 0; i < visitEvaluate.length;i++){
            visitEvaluate[i].disabled = false;
        }
        var cure = document.getElementsByName("cure"+thisPanel.idPostfix);
        for(var i = 0; i < cure.length;i++){
            cure[i].disabled = false;
        }
        var medicineNot = document.getElementsByName("medicineNot"+thisPanel.idPostfix);
        for(var i = 0; i < medicineNot.length;i++){
            medicineNot[i].disabled = false;
        }
        var medicineBadEffect = document.getElementsByName("medicineBadEffect"+thisPanel.idPostfix);
        for(var i = 0; i < medicineBadEffect.length;i++){
            medicineBadEffect[i].disabled = false;
        }
        var medicine = document.getElementsByName("medicine"+thisPanel.idPostfix);
        for(var i = 0; i < medicine.length;i++){
            medicine[i].disabled = false;
        }
		// 清空原因
		document.getElementById("noVisitReason_1" + thisPanel.idPostfix).checked = false;
		document.getElementById("noVisitReason_2" + thisPanel.idPostfix).checked = false;
		document.getElementById("noVisitReason_3" + thisPanel.idPostfix).checked = false;
		document.getElementById("noVisitReason_4" + thisPanel.idPostfix).checked = false;
		for (var i = 0; i < size; i++) {
			var it = items[i];
			if (changeItems.indexOf(it.id) > -1) {
				it["not-null"] = 1;
			} else if (it.id == noItems) {
				it["not-null"] = 0;
			}
		}
		thisPanel.fieldValidate();
	}
	var toRedObjects = getObjsByClass("classinput");
	for (var k in toRedObjects) {
		var obj = toRedObjects[k];
		if (!obj) {
			continue;
		}
		if (v != 1) {
			if (!obj.style) {
				continue;
			}
			obj.style.color = "black";
		} else {
			if (!obj.style) {
				continue;
			}
			obj.style.color = "red";
		}
	}
	var ZGYY = document.getElementById("ZGYY" + thisPanel.idPostfix);
	if (v != 1) {
		ZGYY.style.color = "red";
	} else {
		ZGYY.style.color = "black";
	}
	if(v == 9){
        thisPanel.nextDate.allowBlank = true;
        thisPanel.nextDate["not-null"] = false;
        thisPanel.nextDate.disabled = true;
	}else{
        thisPanel.nextDate.allowBlank = false;
        thisPanel.nextDate["not-null"] = true;
        thisPanel.nextDate.disabled = false;
        thisPanel.nextDate.validate();
	}
	if (thisPanel.planMode == "2") {
		var nextDate_Label = document.getElementById("nextDate_Label" + thisPanel.idPostfix);
		if (v == 1 || v == 2) {
			nextDate_Label.style.color = "red";
			thisPanel.nextDate.allowBlank = false;
			thisPanel.nextDate["not-null"] = true;
            thisPanel.nextDate.disabled = false;
			thisPanel.nextDate.validate();
		} else {
            thisPanel.nextDate.allowBlank = true;
            thisPanel.nextDate["not-null"] = false;
            thisPanel.nextDate.disabled = true;
			nextDate_Label.style.color = "black";
		}
	}
	thisPanel.fireEvent("visitEffect", v)
}

function onVisitEvaluateHyClick(v) {
    if (1 != v) {
        // if (!thisPanel.visitId) {
            MyMessageTip.msg("提示", "控制不满意或者有不良反应或并发症，请在两周内进行下一次随访。", !0);
            var t = Date.parseDate(thisPanel.visitDate.value, "Y-m-d");
            t.setDate(t.getDate() + 14), thisPanel.nextDate.setValue(t);
            thisPanel.nextDate.validate();
        // }
    } else {
        var t = Date.parseDate(thisPanel.visitDate.value, "Y-m-d");
        t.setMonth(t.getMonth() + 3),thisPanel.nextDate.setValue(t);
        // t.setDate(t.getDate() + 90), thisPanel.nextDate.setValue(t);
        thisPanel.nextDate.validate();
    }
}
function onVisitWayChange(v, f) {
	var schema = this.schemas;
	if (!schema) {
		var re = util.schema
			.loadSync("chis.application.hy.schemas.MDC_HypertensionVisit_html")
		if (re.code == 200) {
			schema = re.schema;
		}
	}
	var items = schema.items;
	for (var i = 0; i < items.length; i++) {
		var schemaItem = items[i];
		if (schemaItem.id == 'heartRate') {
			this.heartRateItem = schemaItem
			break
		}
	}
	var heartRateFld = document.getElementById("heartRate" + thisPanel.idPostfix);
	var heartRate_Label = document.getElementById("heartRate_Label" + thisPanel.idPostfix);
	if(v == "3"){
		thisPanel.removeClass(heartRateFld, "x-form-invalid");
		this.heartRateItem["not-null"] = false;
		heartRate_Label.style.color = "black";
	}else{
		thisPanel.addClass(heartRateFld, "x-form-invalid");
		this.heartRateItem["not-null"] = true;
		heartRate_Label.style.color = "red";
	}
}
function onVisitReasonChange(v, f) {
	var div = document.getElementById("div_noVisitReason" + thisPanel.idPostfix);
	var noVisitReasonOtherNot = document.getElementById("noVisitReasonOtherNot" +
		thisPanel.idPostfix);
	if (!v) {
		thisPanel.addClass(div, "x-form-invalid");
	} else {
		thisPanel.removeClass(div, "x-form-invalid");
	}
	if (v == "1" || v == "2") {
		var veFld = document.getElementById("visitEffect_9" + thisPanel.idPostfix);
		veFld.checked = true;
		thisPanel.nextDate.allowBlank = true;
		thisPanel.nextDate["not-null"] = false;
		thisPanel.nextDate.validate();
		var nextDate_Label = document.getElementById("nextDate_Label" + thisPanel.idPostfix);
		nextDate_Label.style.color = "black";
	}
	if (v == "99") {
		if (!noVisitReasonOtherNot.value || noVisitReasonOtherNot.value == "") {
			thisPanel.addClass(noVisitReasonOtherNot, "x-form-invalid");
		}
		noVisitReasonOtherNot.disabled = false;
	} else {
		thisPanel.removeClass(noVisitReasonOtherNot, "x-form-invalid");
		noVisitReasonOtherNot.value = "";
		noVisitReasonOtherNot.disabled = true;
	}
}

function onMedicineBadEffectChange(v, f) {
	if (v == "y") {
		f.style.color = "#000";
		f.disabled = false;
	} else {
		f.value = "";
		f.disabled = true;
	}
}

function onRiskinessClick(v) {
	var id = "riskiness";
	value = thisPanel.getCheckBoxValues(id);
	if (value.indexOf(v) == -1) {
		return;
	}
	if (v == "12") {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, "12")
	} else if (value.indexOf("12") != -1) {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, v)
	}

}

function onCurrentSymptomsClick(v, f, value) {
	if (!value) {
		var id = "currentSymptoms";
		value = thisPanel.getCheckBoxValues(id);
		if (value.indexOf(v) == -1) {
			if (f) {
				if (value.indexOf("10") != -1) {
					f.style.color = "#000";
					f.disabled = false;
				} else {
					f.value = "";
					f.disabled = true;
				}
			}
			return;
		}
		if (v == "9") {
			thisPanel.clearCheckBoxValues(id);
			thisPanel.setCheckBoxValues(id, "9")
			f.value = "";
			f.disabled = true;
			return;
		} else if (value.indexOf("9") != -1) {
			thisPanel.clearCheckBoxValues(id);
			thisPanel.setCheckBoxValues(id, v)
		}
	}
	if (f) {
		if (value.indexOf("10") != -1) {
			f.style.color = "#000";
			f.disabled = false;
		} else {
			f.value = "";
			f.disabled = true;
		}
	}
}

function onTargetHurtClick(v) {
	var id = "targetHurt";
	var value = thisPanel.getCheckBoxValues(id);
	if (value.indexOf(v) == -1) {
		return;
	}
	if (v == "10") {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, "10")
	} else if (value.indexOf("10") != -1) {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, v)
	}
}

function onComplicationClick(v) {
	var id = "complication";
	var value = thisPanel.getCheckBoxValues(id);
	if (value.indexOf(v) == -1) {
		return;
	}
	if (v == "16") {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, "16")
	} else if (value.indexOf("16") != -1) {
		thisPanel.clearCheckBoxValues(id);
		thisPanel.setCheckBoxValues(id, v)
	}
}

function onWeightHyChange(v, f) {
	var bmi = '';
	if (v) {
		if (window.fixGroupheight) {
			var temp = window.fixGroupheight * window.fixGroupheight / 10000;
			bmi = (v / temp).toFixed(2);
			f.style.color = "#000";
			f.value = bmi;
		} else { //thisPanel.height 如果分组身高没有就取档案的身高
			if (thisPanel.height) {
				var temp = thisPanel.height * thisPanel.height / 10000;
				bmi = (v / temp).toFixed(2);
				f.style.color = "#000";
				f.value = bmi;
			} else {
				return;
			}
		}
	} else {
		f.value = f.defaultValue;
		f.style.color = "#999";
	}
	if (bmi > 24) {
		thisPanel.loseWeight = {
			key : "2",
			text : "需要"
		};
	} else {
		thisPanel.loseWeight = {
			key : "1",
			text : "不需要"
		};
	}
	thisPanel.fieldValidate(thisPanel.schema)
}

function onWeightHyChangeTwo(v, f) {
        var fromdata = thisPanel.getFormData();
        var weight = fromdata.weight;
        var targetWeight = fromdata.targetWeight;
        if (window.fixGroupheight) {
            var temp = window.fixGroupheight * window.fixGroupheight / 10000;
            var bmi = (weight / temp).toFixed(2);
            v.style.color = "#000";
            v.value = bmi;
            var targetBmi = (targetWeight / temp).toFixed(2);
            f.style.color = "#000";
            f.value = targetBmi;
        } else {
            if (thisPanel.height) {
                var temp = thisPanel.height * thisPanel.height / 10000;
                var bmi = (weight / temp).toFixed(2);
                v.style.color = "#000";
                v.value = bmi;
                var targetBmi = (targetWeight / temp).toFixed(2);
                f.style.color = "#000";
                f.value = targetBmi;
            } else {
                return;
            }
        }
    thisPanel.fieldValidate(thisPanel.schema)
}

function onMedicineClick(v, f1) {
	var id = "medicineBadEffect";
	var flag = true;
	if (v == 3) {
		thisPanel.setRadioValue(id, {
			key: "n"
		});
		thisPanel.setRadioDisabled(id, true)
		f1.value = "";
		f1.disabled = true;
	} else if (v == 1 || v == 2) {
		thisPanel.setRadioDisabled(id, false)
		var value = thisPanel.getRadioValue(id);
		if (value == "y") {
			f1.disabled = false;
		} else {
			f1.disabled = true;
		}
		flag = false;
	} else {
		thisPanel.setRadioDisabled(id, false)
	}
	var BGLYY = document.getElementById("BGLYY" + thisPanel.idPostfix);
	var medicineNotDiv = document.getElementById("div_medicineNot" +
		thisPanel.idPostfix);
	var medicineNot = document.getElementsByName("medicineNot" +
		thisPanel.idPostfix);
	var medicineNotValue = thisPanel.getHtmlFldValue("medicineNot");
	var medicineOtherNot = document.getElementById("medicineOtherNot" +
		thisPanel.idPostfix);
	var YYQKBtn = document.getElementById("YYQKBtn" + thisPanel.idPostfix);
	if(!v) {
		YYQKBtn.disabled = true
	}
	if (v == 1 || v == "") {
		thisPanel.removeClass(medicineNotDiv, "x-form-invalid");
		medicineOtherNot.disabled = true;
		medicineOtherNot.value = "";
		thisPanel.removeClass(medicineOtherNot, "x-form-invalid");
		thisPanel.setHtmlFldValue("medicineNot" + thisPanel.idPostfix, -1);
		BGLYY.style.color = "black";
		thisPanel.setRadioDisabled("medicineNot", true);
	} else {
		BGLYY.style.color = "black";
		thisPanel.setRadioDisabled("medicineNot", false);
		if (!medicineNotValue || medicineNotValue == "") {
			thisPanel.addClass(medicineNotDiv, "x-form-invalid");
		} else if (medicineNotValue == "99") {
			medicineOtherNot.disabled = false;
			if (!medicineOtherNot.value || medicineOtherNot.value == "") {
				thisPanel.addClass(medicineOtherNot, "x-form-invalid");
			}
		} else {
			medicineOtherNot.disabled = true;
			medicineOtherNot.value = "";
			thisPanel.removeClass(medicineOtherNot, "x-form-invalid");
		}
		thisPanel.removeClass(medicineNotDiv, "x-form-invalid"); //清除当不规律用药选择不用药时，不规律用药原因的变红提醒
	}
	thisPanel.setMedicineDisabled(flag);
	thisPanel.fireEvent("medicineSelectChange", v, thisPanel);
}

function getstyle(sname) {
	for (var i = 0; i < document.styleSheets.length; i++) {
		var rules;
		if (document.styleSheets[i].cssRules) {
			rules = document.styleSheets[i].cssRules;
		} else {
			rules = document.styleSheets[i].rules;
		}
		for (var j = 0; j < rules.length; j++) {
			if (rules[j].selectorText == sname) {
				// selectorText
				// 属性的作用是对一个选择的地址进行替换.意思应该是获取RULES[J]的CLASSNAME.有说错的地方欢迎指正
				return rules[j].style;
			}
		}
	}
}

function onMedicineNotClick(value) {
	var medicineOtherNot = document.getElementById("medicineOtherNot" +
		thisPanel.idPostfix);
	var medicineNotDiv = document.getElementById("div_medicineNot" +
		thisPanel.idPostfix);
	if (value == "") {
		thisPanel.addClass(medicineNotDiv, "x-form-invalid");
	} else {
		thisPanel.removeClass(medicineNotDiv, "x-form-invalid");
	}
	if (value == "99") {
		if (!medicineOtherNot.value || medicineOtherNot.value == "") {
			thisPanel.addClass(medicineOtherNot, "x-form-invalid");
		}
		medicineOtherNot.disabled = false;
	} else {
		thisPanel.removeClass(medicineOtherNot, "x-form-invalid");
		medicineOtherNot.value = "";
		medicineOtherNot.disabled = true;
	}
}

function noMedicineHyClick(v,para2) {
	var id = "medicineBadEffect";
	var flag = true;
	//服药依从性
	var medicine = document.getElementById("div_medicine" +
		thisPanel.idPostfix);
	var BGLYY = document.getElementById("BGLYY" + thisPanel.idPostfix);
	var medicineNot = document.getElementsByName("medicineNot" +
		thisPanel.idPostfix);
	var medicineOtherNot = document.getElementById("medicineOtherNot" +
		thisPanel.idPostfix);
	var medicineBadEffectText = document.getElementById("medicineBadEffectText" + thisPanel.idPostfix);
	var YYQKBtn = document.getElementById("YYQKBtn" + thisPanel.idPostfix);
	var noCureReason = document.getElementById("noCureReason" + thisPanel.idPostfix);
	if (v == 2) {
		medicineNot.disabled = true;
		medicineNot.value = "";
		thisPanel.removeClass(medicineOtherNot, "x-form-invalid");
		thisPanel.setHtmlFldValue("medicineNot" + thisPanel.idPostfix, -1);
		BGLYY.style.color = "black";
		thisPanel.setRadioDisabled("medicineNot", true);

		//不良反应置灰
		thisPanel.setRadioDisabled2("medicine", true)
		thisPanel.setRadioValue(id, {
			key: "n"
		});
		thisPanel.setRadioDisabled(id, true)
		medicineBadEffectText.value = "";
		medicineBadEffectText.disabled = true;
        medicineOtherNot.value="";
        medicineOtherNot.disabled = true;
		//用药情况置灰
		YYQKBtn.disabled = true;
		noCureReason.disabled = false;
		//清空用药情况
		document.getElementById("YYQK" + thisPanel.idPostfix).innerHTML = "";
	} else {
		medicineNot.disabled = false;
		// thisPanel.addClass(medicineOtherNot, "x-form-invalid");
		BGLYY.style.color = "black";
		thisPanel.setRadioDisabled2("medicineNot", false);
		thisPanel.setRadioDisabled2("medicine", false)
		thisPanel.setRadioDisabled(id, false)
		var value = thisPanel.getRadioValue(id);
		if (value == "y") {
			medicineBadEffectText.disabled = false;
		} else {
			medicineBadEffectText.disabled = true;
		}
		//用药情况置灰
		YYQKBtn.disabled = false;
		noCureReason.disabled = true;
		noCureReason.value = "";
	}
	thisPanel.setMedicineDisabled(flag);
	thisPanel.fireEvent("medicineSelectChange", v, thisPanel);
}

function getObjsByClass(clsName) {
	// 依据样式名称获取DOM对象
	var tags = this.tags || document.getElementsByTagName("*");
	var list = [];
	for (var k in tags) {
		var tag = tags[k];
		if (tag.className == clsName) {
			list.push(tag);
		}
	}
	return list;
}

function onXYChange(f1, f2) {
	var v1 = f1.value;
	var v2 = f2.value;
	var d1 = f1.defaultValue;
	var d2 = f2.defaultValue;
	if (v1 == d1 || v2 == d2) {
		return;
	}
	if (v1.length <= v2.length && v1 <= v2) {
		f1.title = "收缩压必须大于舒张压。";
		f2.title = "舒张压必须小于收缩压。";
		thisPanel.addClass(f1, "x-form-invalid");
		thisPanel.addClass(f2, "x-form-invalid");
	} else {
		f1.title = "收缩压(mmHg)";
		f2.title = "舒张压(mmHg)";
		thisPanel.removeClass(f1, "x-form-invalid");
		thisPanel.removeClass(f2, "x-form-invalid");
	}
}

function onMedicineButtonClick() {
	var cfg = {
		showButtonOnTop: true,
		autoLoadSchema: false,
		autoLoadData: false,
		isCombined: true,
		closable: true,
		height: 400,
		entryName: "chis.application.hy.schemas.MDC_HypertensionVisit_medicine",
		mainApp: thisPanel.mainApp,
		exContext: thisPanel.exContext
	};
	var module;
	var p;
    var hyStatus = thisPanel.exContext.ids["MDC_HypertensionRecord.phrId.status"];
	if (!this.medicinePanel) {
		module = new chis.application.hy.script.visit.HypertensionVisitMedicineHtml(cfg);
		module.on("save", thisPanel.medicineModuleSave, thisPanel);
		p = module.initPanel();
		thisPanel.medicineModule = module;
		thisPanel.medicinePanel = p;
	} else {
		module = thisPanel.medicineModule;
		module.exContext = thisPanel.exContext
		p = thisPanel.medicinePanel
	}
    module.flag = hyStatus;

	var win = module.getWin();
	thisPanel.medicineWin = win;
	win.add(p);
	win.setPosition(360, 60)
	win.setTitle("用药情况")
	win.show();
	module.doNew();
	if (thisPanel.medicines && thisPanel.medicines["YYQk"] != "") {
		module.initFormData(thisPanel.medicines);
	}
}

function onHealthButtonClick() {
	var cfg = {
		showButtonOnTop: true,
		autoLoadSchema: false,
		autoLoadData: false,
		isCombined: true,
		closable: true,
		entryName: "chis.application.hy.schemas.MDC_HypertensionVisit_medicine",
		mainApp: thisPanel.mainApp,
		exContext: thisPanel.exContext
	};
	var module;
	var p;
	if (!this.healthPanel) {
		module = new chis.application.hy.script.visit.HypertensionVisitHealthHtml(cfg);
		module.on("save", thisPanel.healthModuleSave, thisPanel);
		p = module.initPanel();
		thisPanel.healthModule = module;
		thisPanel.healthPanel = p;
	} else {
		module = thisPanel.healthModule;
		p = thisPanel.healthPanel
	}
	var win = module.getWin();
	thisPanel.healthWin = win;
	win.add(p);
	win.setPosition(360, 60)
	win.setTitle("健康处方")
	win.show();
	module.doNew();
	if (thisPanel.healths) {
		module.initFormData(thisPanel.healths);
	}
}
