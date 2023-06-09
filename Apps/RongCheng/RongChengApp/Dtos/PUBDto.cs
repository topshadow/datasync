using System.Text.Json.Serialization;
namespace RongChengApp.Dtos
{
    /// <summary>
    ///  糖尿病随访计划
    /// </summary>
    public class PUBGetCurYearVisitPlanInput
    {
        public string? serviceId { get; set; } = "chis.diabetesVisitService";
        public string? method { get; set; } = "execute";
        public string? schema { get; set; } = "chis.application.pub.schemas.PUB_VisitPlan";
        public int? pageSize { get; set; } = 50;
        public int? pageNo { get; set; } = 1;
        public string? queryDate { get; set; } = "full-T";
        public string? serviceAction { get; set; } = "getCurYearVisitPlan";
        public string empiId { get; set; } = "4D86075B69803B4BE05010AC32141F56";


    }
    /// <summary>
    /// 查询糖尿病全年随访记录结果
    /// </summary>
    public class PUBGetCurYearVisitPlanResult
    {
        public int code { get; set; }
        public List<PUBGetCurYearVisitPlanResultItem> body { get; set; }
    }
    public class PUBGetCurYearVisitPlanResultItem
    {
        public string? beginDate { get; set; }
        public string businessType { get; set; }
        public string businessType_text { get; set; }
        public string? empiId { get; set; }
        public string? endDate { get; set; }
        public int? extend1 { get; set; }

        public string? fixGroupDate { get; set; }
        public string? groupCode_text { get; set; }
        public string? inputUser_text { get; set; }
        public string? isReferral_text { get; set; }
        public string? lastModifyDate { get; set; }
        public string? lastModifyUnit { get; set; }
        public string? lastModifyUnit_text { get; set; }
        public string? lastModifyUser { get; set; }
        public string? lastModifyUser_text { get; set; }
        public string? planDate { get; set; }
        public string? planId { get; set; }
        public string? planStatus { get; set; }
        public string? planStatus_text { get; set; }
        public string? recordId { get; set; }
        public int? sn { get; set; }
        public string? taskDoctorId { get; set; }
        public string? taskDoctorId_text { get; set; }
        public string? visitDate { get; set; }
        public string? visitId { get; set; }
        public double? pbs { get; set; }


    }
    /// <summary>
    /// 糖尿病患者访问服务
    /// </summary>

    public class DiabetesVisitServiceInput
    {
        public string serviceId { get; set; } = "chis.diabetesVisitService";
        public string serviceAction { get; set; } = "initializeForm";
        public string method { get; set; } = "execute";
        public DiabetesVisitServiceInputBody body { get; set; }
    }
    public class DiabetesVisitServiceInputBody
    {
        public string planId { get; set; } = "0000000002249764";
        public int? sn { get; set; } = 2;
        public string? beginDate { get; set; } = "2022-11-21";
        public string? endDate { get; set; } = "2023-03-01";
        public string? beginVisitDate { get; set; } = "";
        public string? planStatus { get; set; } = "1";
        public string visitId { get; set; } = "0000000000132412";
        public string planDate { get; set; } = "2023-01-10";
        public string visitDate { get; set; } = "2023-01-09";
        public string recordId { get; set; } = String.Empty;

    }

    public class DiabetesVisitServiceResultBody
    {
        public bool isLastVisitedPlan { get; set; } = false;
        [JsonPropertyName("chis.application.dbs.schemas.MDC_DiabetesMedicine_list")]

        public List<object> MDC_DiabetesMedicine_list { get; set; }
        [JsonPropertyName("chis.application.dbs.schemas.MDC_DiabetesVisit_html_data")]
        public DiabetesVisitServiceResultHtmlBody MDC_DiabetesVisit_html_data { get; set; }
        // "chis.application.dbs.schemas.MDC_DiabetesVisit_html_data":


    }
    public class DiabetesVisitServiceResult
    {
        public int? code { get; set; }
        public string? diabetesType { get; set; } = "1";
        public DiabetesVisitServiceResultBody? body { get; set; }
    }
    public class DiabetesVisitServiceResultHtmlBody
    {
        /// <summary>
        /// 心理调整
        ///  { text: "良好", key: "1" }
        ///   { text: "一般", key: "2" }
        ///  { text: "差", key: "3" }
        /// </summary>
        public KeyText? psychologyChange { get; set; }
        /// <summary>
        /// 低血糖反应
        /// {text: "无", key: "1" }	
        ///  {text: "偶尔", key: "2" }	
        ///  {text: "频繁", key: "3" }	
        /// </summary>
        public KeyText? glycopenia { get; set; }

        public KeyText? visitDoctor { get; set; }
        /// <summary>
        /// {"text":"未规范","key":"0"}
        /// </summary>
        /// <value></value>
        public KeyText? standard { get; set; }
        public int? trainTimesWeek { get; set; } = 11;
        public int? targetTrainTimesWeek { get; set; } = 11;
        /// <summary>
        /// ":{"text":"服药","key":"1"},
        /// </summary>
        /// <value></value>
        public KeyText? cure { get; set; }
        public KeyText? inputUser { get; set; }
        public string? empiId { get; set; }
        public string visitId { get; set; } = "0000000000142024";
        public string? visitDate { get; set; } = "2023-05-10";
        /// <summary>
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"},
        /// </summary>
        /// <value></value>
        public KeyText? manaUnitId { get; set; }
        /// <summary>
        /// ":"2023-05-10 15:20:25",
        /// </summary>
        /// <value></value>
        public string? lastModifyDate { get; set; }
        public string? nextDate { get; set; } = "2023-05-24";
        /// <summary>
        /// 身高
        /// </summary>
        /// <value></value>
        public double? height { get; set; }
        public KeyText? adverseReactions { get; set; }
        public KeyText? diabetesChange { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        /// <value></value>
        public string? weight { get; set; }
        /// <summary>
        /// 目标体重
        /// </summary>
        /// <value></value>
        public string? targetWeight { get; set; }
        public int? targetSmokeCount { get; set; } = 11;
        /// <summary>
        /// "2023-05-10 15:19:49",
        /// </summary>
        /// <value></value>
        public string? inputDate { get; set; }
        public KeyText? visitType { get; set; }
        public KeyText? symptoms { get; set; }
        public int? diastolic { get; set; }
        public KeyText? diabetesGroup { get; set; }
        public string? nonStandard { get; set; } = "心理调整,遵医行为,低血糖反应未填写;用药情况未填写;";
        /// <summary>
        /// 建卡空腹血糖
        /// </summary>
        /// <value></value>
        public double? fbs { get; set; }
        public double? drinkCount
        {
            get; set;
        } = 11.0;
        public KeyText visitEffect { get; set; }
        /// <summary>
        /// 完成度  例如 "88%";
        /// </summary>
        /// <value></value>
        public string? completeLevel { get; set; }
        /// <summary>
        /// {
        ///         "text":"榕城区榕东社区卫生服务中心",
        ///  "key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUnit { get; set; }
        public object? medicines { get; set; }
        /// <summary>
        /// { "text":"中危","key":"2"},
        /// </summary>
        /// <value></value>
        public KeyText? dangerLevel { get; set; }
        public double? targetBmi { get; set; } = 23.15;
        public int? constriction { get; set; } = 11;
        public int? trainMinute { get; set; } = 11;
        public KeyText? inputUnit { get; set; }
        /// <summary>
        /// :{ "text":"","key":"0"},
        /// </summary>
        /// <value></value>
        public KeyText? lateInput { get; set; }
        public KeyText? visitWay { get; set; }
        public int? smokeCount { get; set; } = 11;
        public int? targetTrainMinute { get; set; } = 11;
        /// <summary>
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? visitUnit { get; set; }
        public double? targetDrinkCount { get; set; } = 11.0;
        /// <summary>
        /// { "text":"1型糖尿病","key":"1"}
        /// </summary>
        /// <value></value>
        public KeyText? diabetesType { get; set; }
        /// <summary>
        /// { "text":"规律","key":"1"}
        /// </summary>
        /// <value></value>
        public KeyText? medicine { get; set; }
        /// <summary>
        /// 足背动脉搏动
        /// { "text":"触及正常","key":"1"}
        /// {"text": "双侧减弱","key": "2"}
        /// {"text": "左侧减弱","key": "3"}
        /// {"text": "右侧减弱","key": "4"}
        /// {"text": "双侧消失","key": "5"}
        /// {"text": "左侧消失","key": "6"}
        /// {"text": "右侧消失","key": "7"}
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        /// <value></value>
        public KeyText? pulsation { get; set; }
        /// <summary>
        /// "44520200621302093"
        /// </summary>
        /// <value></value>
        public string? phrId { get; set; }
        public int? food { get; set; }
        public KeyText? lastModifyUser { get; set; }
        public int? targetFood { get; set; } 
        public string? checkId { get; set; } = "0000000000142024";
        /// <summary>
        /// 糖化血红蛋白(%):
        /// </summary>
        /// <value></value>
        public double? hbA1c { get; set; } = 11.0;
        public double? bmi { get; set; } = 34.26;
        /// <summary>
        /// 遵医嘱
        ///  {"text": "良好","key": "1"}
        ///  {"text": "一般","key": "2"}
        /// {"text": "差","key": "3"}
        /// </summary>
        public KeyText? obeyDoctor { get; set; }

    }

    public class SaveDiabetesVisitInput
    {
        public string serviceId { get; set; } = "chis.diabetesVisitService";
        public string schema { get; set; } = "chis.application.dbs.schemas.MDC_DiabetesVisit_html";
        [JsonPropertyName("method")]
        public string method { get; set; } = "execute";
        public string op { get; set; } = "update";
        public string serviceAction { get; set; } = "saveDiabetesVisit";
        public SaveDiabetesVisitInputBody? body { get; set; }

    }
    public class SaveDiabetesVisitInputBody
    {
        /// <summary>
        /// 更新原因
        /// </summary>
        public string? updateReason { get; set; }
        public int? food { get; set; }
        public int? targetFood { get; set; }
        public double? hbA1c { get; set; }
        public string? visitId { get; set; } = "0000000000261684";
        public string? phrId { get; set; } = "44520200620207607";
        public string? personName { get; set; } = "";
        public string? sexCode { get; set; } = "";
        public string? birthday { get; set; } = "";
        public string? idCard { get; set; } = "";
        public string? phoneNumber { get; set; } = "";
        public string? regionCode { get; set; } = "";
        public string? empiId { get; set; } = "ece7af90a1cb46d68aa7f64fcf119703";
        public string? visitDate { get; set; } 
        /// <summary>
        /// 随访方式
        /// 1门诊 2家庭3点后
        /// </summary>
        /// <value></value>
        public string? visitWay { get; set; }
        public string? visitEffect { get; set; } = "1";
        public string? noVisitReason { get; set; } = "";
        public string? noVisitReasonOtherNot { get; set; }
        /// <summary>
        /// 症状
        /// 0. 无症状
        /// 1 头痛头晕
        /// 2. 恶心呕吐
        /// 3. 眼花耳鸣
        /// 4. 呼吸困难
        /// 5. 心悸胸闷
        /// </summary>
        /// <returns></returns>
        //public string? currentSymptoms { get; set; } = "1,2,3,4";
        public string? otherSymptoms { get; set; } = "";
        /// <summary>
        /// 症状
        /// </summary>
        public string? symptoms { get; set; } = "";
        public string? constriction { get; set; }
        public string? diastolic { get; set; }
        public string? weight { get; set; }
        public string? targetWeight { get; set; }
        public string? bmi { get; set; }
        public string? targetBmi { get; set; }
        public string? heartRate { get; set; }
        public string? targetHeartRate { get; set; }
        public string? otherSigns { get; set; } = String.Empty;
        public string? smokeCount { get; set; }
        public string? targetSmokeCount { get; set; }
        public string? drinkCount { get; set; }
        //public string drinkTypeCode { get; set; } = "";
        public string? targetDrinkCount { get; set; }
        public string? trainTimesWeek { get; set; }
        public string? targetTrainTimesWeek { get; set; }
        public string? trainMinute { get; set; }
        public string? targetTrainMinute { get; set; }
        public string? loseWeight { get; set; } = "2";
        public string? salt { get; set; } = "1";
        public string? targetSalt { get; set; } = "1";
        public string? cure { get; set; } = "1";
        public string? noCureReason { get; set; } = "";
        public string? medicine { get; set; } = "1";
        public string? medicineNot { get; set; } = "";
        public string? medicineOtherNot { get; set; }
        public string? medicineBadEffect { get; set; }
        public string? medicineBadEffectText { get; set; }
        public string? auxiliaryCheck { get; set; }
        public string? psychologyChange { get; set; } = "1";
        public string? obeyDoctor { get; set; } = "1";
        public string? riskiness { get; set; } = "";
        //public string complication { get; set; } = "";

        //public string complicationIncrease { get; set; } = "";
        //public string? riskLevel { get; set; }
        //public string? riskUpdateReason { get; set; }
        //public string visitEvaluate { get; set; } = "1";
        //public string healthProposal { get; set; } = String.Empty;
        //public string drugNames1 { get; set; } = "";
        //public string medicineType1 { get; set; } = String.Empty;
        //public string everyDayTime1 { get; set; } = String.Empty;
        //public string? oneDosage1 { get; set; }
        //public string medicineUnit1 { get; set; } = "";
        //public string drugNames2 { get; set; } = "";
        //public string medicineType2 { get; set; } = "";
        //public string everyDayTime2 { get; set; } = "";
        //public string oneDosage2 { get; set; } = "";
        //public string medicineUnit2 { get; set; } = "";
        //public string drugNames3 { get; set; } = String.Empty;
        //public string medicineType3 { get; set; } = String.Empty;
        //public string everyDayTime3 { get; set; } = String.Empty;
        //public string oneDosage3 { get; set; } = String.Empty;
        //public string? medicineUnit3 { get; set; } = String.Empty;
        //public string drugNames4 { get; set; } = String.Empty;
        //public string medicineType4 { get; set; } = "";
        //public string everyDayTime4 { get; set; } = String.Empty;
        //public string oneDosage4 { get; set; } = String.Empty;
        //public string medicineUnit4 { get; set; } = String.Empty;
        //public string drugNames5 { get; set; } = String.Empty;
        //public string medicineType5 { get; set; } = String.Empty;
        //public string everyDayTime5 { get; set; } = String.Empty;
        //public string? oneDosage5 { get; set; }
        //public string? medicineUnit5 { get; set; }
        //public string? agencyAndDept { get; set; } = String.Empty;
       
        /// <summary>
        /// 胰岛素种类
        /// </summary>
        public string ydstype1 { get; set; }

        public string ydstype2 { get; set; }
        public string ydstype3 { get; set; }
        /// <summary>
        /// 胰岛素数量
        /// </summary>
        
        public string ydsfre1 { get; set; }
        public string ydsfre2 { get; set; }
        public string ydsfre3 { get; set; }

        /// <summary>
        /// 转诊原因
        /// </summary>
        public string? referralReason { get; set; }
        /// <summary>
        /// "2023-07-28",
        /// </summary>
        /// <value></value>
        public string? nextDate { get; set; }
        /// <summary>
        /// 单元测试
        /// </summary>
        /// <value></value>
        public string? visitDoctor { get; set; } = "605100";
        //public string visitUnit { get; set; } = "445202605";
        //public string treatEffect { get; set; } = "1";
        //public string cardiovascularEvent { get; set; } = "1";
        //public string waistLine { get; set; } = "";
        //public string hypertensionGroup { get; set; } = "";
        //public string incorrectMedicine { get; set; } = "";
        //public string noMedicine { get; set; } = "";
        //public string otherReason { get; set; } = "";
        //public string healthRecipe { get; set; } = "";
        //public string nonMedicineWay { get; set; } = "";
        //public string acceptDegree { get; set; } = "1";
        //public string inputUnit { get; set; } = "445202605";
        //public string inputUser { get; set; } = "605100";
        /// <summary>
        /// 建档日期
        /// 例如"2023-04-26 00:00:00",
        /// </summary>
        /// <returns></returns>
        //public string? inputDate { get; set; }
        //public string? manaDoctorId { get; set; }
        /// <summary>
        /// "n",
        /// </summary>
        /// <returns></returns>
        //public string? lateInput { get; set; }
        //public string lastModifyUser { get; set; } = "605100";
        ///// <summary>
        ///// 445202605
        ///// </summary>
        ///// <returns></returns>
        //public string? lastModifyUnit { get; set; }
        /// <summary>
        /// 2023-04-26 17:15:05
        /// </summary>
        /// <returns></returns>
        //public string? lastModifyDate { get; set; }
        /// <summary>
        /// 0正常
        /// </summary>
        /// <returns></returns>
        public string? status { get; set; }
        //public string? regionCode_text { get; set; }
        //public string lastVisitId { get; set; } = "0000000000000000";
        public string? height { get; set; }
        public string? planDate { get; set; } = "2023-04-24";
        public string? planId { get; set; } = "0000000002495911";
        public string? endDate { get; set; } = "2023-04-29T00:00:00";
        /// <summary>
        /// "2023-04-19T00:00:00"
        /// </summary>
        /// <value></value>
        //public string? beginDate { get; set; }
        //public string? fixGroupDate { get; set; }
        public string? sn { get; set; }
        public string? pbs { get; set; }
        /// <summary>
        /// "【目标量(两)】未填写<br>&nbsp;&nbsp;&nbsp;&nbsp;【用药情况】未填写"
        /// </summary>
        /// <value></value>
        public string? nonStandard { get; set; }
        public string? fbs { get; set; }
        public string? standard { get; set; } = "0";
        public string? completeLevel { get; set; } = "93%";
        //public List<object> medicineList { get; set; } = new List<object> { };
        public bool needAssess { get; set; } = false;
        //public bool controlBad { get; set; } = false;
        //public string hypertensionGroupName { get; set; } = String.Empty;
        //public bool? needInsertPlan { get; set; } = false;
        //public bool needReferral { get; set; } = false;
        //public bool needChangeGroup { get; set; } = false;
        /// <summary>
        /// 糖化血红蛋白测试日期
        /// </summary>
        public string? testDate { get; set; }
        /// <summary>
        /// 随访分类
        /// { text: "控制满意", key: "1" }
        /// { text: "控制不满意", key: "2" }
        /// { text: "不良反应", key: "3" }
        /// { text: "并发症", key: "4" }
        /// </summary>
        public string? visitType { get; set; }

    }
    /// <summary>
    /// 糖尿病档案详情输入
    /// </summary>
    public class PUBDiabetesRecordServiceInput
    {
        public string serviceId { get; set; } = "chis.diabetesRecordService";
        public string serviceAction { get; set; } = "initializeRecord";
        public string method { get; set; } = "execute";
        public PUBDiabetesRecordServiceInputBody? body { get; set; }
    }
    public class PUBDiabetesRecordServiceInputBody
    {
        public string empiId { get; set; } = "875f62096e46447780fb11473f9b912f";
        public string phrId { get; set; } = "44520200621302093";
    }
    public class PUBDiabetesRecordServiceResult
    {
        public int? code { get; set; } = 200;
        public PUBDiabetesRecordServiceResultBody? body { get; set; }

    }
    /// <summary>
    /// 糖尿病随访详情
    /// </summary>
    public class PUBDiabetesRecordServiceResultBody
    {
        /// <summary>
        /// 血糖 危险因素：01|年龄≥40岁,02|有糖调节受损史,03|超重（BMI≥24kg/m2）或肥胖（BMI≥28kg/m2）和（或）中心型肥胖（男性腰围≥90cm，女性腰围≥85cm）,04|静坐生活方式,05|级亲属中有2型糖尿病史,06|有巨大儿（出生体重≥4kg）生产史或妊娠糖尿病史的妇女,07|高血压（收缩压≥140或收缩压≥90mmHg），或正在接受降压治疗,08|血脂异常{高密度脂蛋白胆固醇（HDL-C）≤0.91mmol/L，甘油三酯≥2.22mmol/L}，或正在接受调脂治疗,09|动脉粥样硬化性心脑血管疾病患者,10|有一过性类固醇糖尿病病史者,11|多囊卵巢综合征（PCOS）患者,12|长期接受抗精神精神病药物和（或）抗抑郁症药物治疗的患者
        /// </summary>
        /// <value></value>
        public KeyText? riskFactors { get; set; }
        /// <summary>
        /// 空腹血糖值
        /// </summary>
        public double? fbs { get; set; }
        /// <summary>
        /// 血糖 糖化血红蛋白(%)
        /// </summary>
        /// <value></value>
        public double? HbA1c { get; set; }
        /// <summary>
        /// 家族史 母亲
        /// 例如  "恶性肿瘤、脑卒中、严重精神障碍、结核病、肝炎、先天畸形、111";
        /// </summary>
        /// <value></value>
        public string? jzsmq { get; set; }
        /// <summary>
        /// {"text":"魏晓东","key":"605100"}
        /// </summary>
        /// <value></value>
        public KeyText? inputUser { get; set; }
        public string empiId { get; set; } = "875f62096e46447780fb11473f9b912f";
        /// <summary>
        /// 家族史 子女
        /// </summary>
        /// <value></value>
        public string jzszn { get; set; }
        /// <summary>
        /// 管辖机构
        /// {"text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? manaUnitId { get; set; }
        /// <summary>
        /// "2023-05-10",
        /// </summary>
        /// <value></value>
        public string? visitDate { get; set; }
        public string? lastModifyDate { get; set; } = "2023-05-10 15:19:49";
        /// <summary>
        /// 身高
        /// </summary>
        /// <value></value>
        public double? height { get; set; }
        /// <summary>
        /// 家族史 父亲
        /// </summary>
        /// <value></value>
        public string? jzsfq { get; set; }
        /// <summary>
        /// {"text":"待核实","key":"1"}
        /// </summary>
        /// <value></value>
        public KeyText? endCheck { get; set; }
        public double? weight { get; set; } = 111.0;
        /// <summary>
        /// 建档日期
        /// 例如  "2023-05-10 15:16:46";
        /// </summary>
        /// <value></value>
        public string? inputDate { get; set; }
        /// <summary>
        /// 血糖单位
        /// :{ "text":"mmol/L","key":"1"}
        /// </summary>
        /// <value></value>
        public KeyText? unit { get; set; }
        /// <summary>
        /// { "text":"一组","key":"01"},
        /// </summary>
        /// <value></value>
        public KeyText? diabetesGroup { get; set; }
        /// <summary>
        /// 建档机构
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"},
        /// </summary>
        /// <value></value>
        public KeyText? createUnit { get; set; }
        /// <summary>
        /// :{ "text":"继续随访","key":"1"},
        /// </summary>
        /// <value></value>
        public KeyText? visitEffect { get; set; }
        /// <summary>
        /// { "text":"正常","key":"0"},
        /// </summary>
        /// <value></value>
        public KeyText? status { get; set; }
        /// <summary>
        /// :{ "text":"榕城区榕东社区卫生服务中心","key":"445202605"},
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUnit { get; set; }
        /// <summary>
        ///  建卡餐后血糖
        /// </summary>
        /// <value></value>
        public double? pbs { get; set; }
        /// <summary>
        /// 家族史 兄弟姐妹
        /// </summary>
        /// <value></value>
        public string? jzsxdjm { get; set; }
        /// <summary>
        /// { "text":"中危","key":"2"}
        /// </summary>
        /// <value></value>
        public KeyText? dangerLevel { get; set; }
        /// <summary>
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? inputUnit { get; set; }
        public string? createDate { get; set; } = "2023-05-10";
        public string op { get; set; } = "update";
        public string planTypeCode { get; set; } = "03";
        /// <summary>
        /// 确诊年月
        /// 如 "2023-03-01";
        /// </summary>
        /// <value></value>
        public string? diagnosisDate { get; set; }
        /// <summary>
        /// 确诊单位
        /// </summary>
        /// <value></value>
        public string? diagnosisUnit { get; set; }
        /// <summary>
        /// 确诊时症状
        /// </summary>
        /// <value></value>
        public string? diagnosisSymptom { get; set; }
        /// <summary>
        ///  { "text":"1型糖尿病", "key":"1" }
        /// key: "1"text: "1型糖尿病"
        ///  key: "2"text: "2号糖尿病"
        ///  key: "3"text: "营养不良型"
        ///  key: "5"text: "IGT"
        /// key: "6"text: "IFG"
        /// diabetesType: {text: "其他", key: "9"}
        /// </summary>
        /// <value></value>
        public KeyText? diabetesType { get; set; }
        /// <summary>
        /// 档案编号 phrId
        ///如 "44520200621302093";
        /// </summary>
        /// <value></value>
        public string? phrId { get; set; }
        /// <summary>
        /// { "text":"魏晓东","key":"605100"},
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUser { get; set; }
        public object? _actions { get; set; }
        /// <summary>
        /// :{ "text":"魏晓东","key":"605100"},
        /// </summary>
        /// <value></value>
        public KeyText? manaDoctorId { get; set; }
        /// <summary>
        /// 建档人员
        /// </summary>
        /// <value></value>
        public KeyText? createUser { get; set; }
    }

    public class PUBSaveDiabetesRecordInput
    {
        public string serviceId { get; set; } = "chis.diabetesRecordService";
        public string method { get; set; } = "execute";
        public string op { get; set; } = "update";
        public string schema { get; set; } = "chis.application.dbs.schemas.MDC_DiabetesRecord";
        public PUBSaveDiabetesRecordInputBody body { get; set; }
        public string serviceAction { get; set; } = "saveDiabetesRecord";
    }
    public class PUBSaveDiabetesRecordInputBody
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string? idCard { get; set; }

        public string? phrId { get; set; } = "44520200621302093";
        public string? signFlag { get; set; } = "n";
        public string? empiId { get; set; } = "875f62096e46447780fb11473f9b912f";
        public string? manaDoctorId { get; set; } = "605100";
        public string? manaUnitId { get; set; } = "445202605";
        public string? riskFactors { get; set; } = "";
        public string? diabetesType { get; set; } = "1";
        public string? diagnosisDate { get; set; } = "2023-03-01";
        public string? years { get; set; } = "2月";
        public string? diagnosisUnit { get; set; } = "aa";
        public string? diagnosisSymptom { get; set; }
        public string? diabetesGroup { get; set; }
        public string? fbs { get; set; }
        public string? pbs { get; set; }
        public string? unit { get; set; }
        public string? HbA1c { get; set; }
        public string? height { get; set; }
        public double? weight { get; set; }
        public string? bmi { get; set; } = "34.26";
        public string? createUnit { get; set; } = "445202605";
        public string createUser { get; set; } = "605100";
        public string? createDate { get; set; } = "2023-05-10";
        public string? status { get; set; }
        public string? endCheck { get; set; }
        public string? visitEffect { get; set; }
        public string? visitDate { get; set; }
        public string? cancellationDate { get; set; }
        public string? cancellationReason { get; set; }
        public string? jzsfq { get; set; } = "高血压";
        public string? qtjzsfq { get; set; } = "";
        public string? jzsmq { get; set; } = "恶性肿瘤、脑卒中、严重精神障碍、结核病、肝炎、先天畸形、111";
        public string? qtjzsmq { get; set; } = "";
        public string? jzsxdjm { get; set; } = "高血压、xdjmqt";
        public string? qtjzsxdjm { get; set; } = "";
        public string? jzszn { get; set; } = "高血压";
        public string? qtjzszn { get; set; }
        public string? planTypeCode { get; set; } = "03";
        public string? lastModifyUser { get; set; }
        /// <summary>
        /// :"2023-05-10 15:19:49",
        /// </summary>
        /// <value></value>
        public string? lastModifyDate { get; set; }
        /// <summary>
        /// 445202605
        /// </summary>
        /// <value></value>
        public string? lastModifyUnit { get; set; }
        /// <summary>
        /// :"605100",
        /// </summary>
        /// <value></value>
        public string? inputUser { get; set; }
        /// <summary>
        /// "2023-05-10 15:16:46"
        /// </summary>
        /// <value></value>
        public string? inputDate { get; set; }
        /// <summary>
        /// 445202605
        /// </summary>
        /// <value></value>
        public string? inputUnit { get; set; }
        public string? dangerLevel { get; set; }



    }
    /// <summary>
    /// 保存糖尿病档案结果
    /// </summary>
    public class PUBSaveDiabetesRecordResult
    {
        public string msg { get; set; } = "糖尿病档案保存成功";
        public bool hasVisit { get; set; } = true;
        public int? code { get; set; } = 200;
        public PUBSaveDiabetesRecordResultBody body { get; set; }

    }
    public class PUBSaveDiabetesRecordResultBody
    {
        /// <summary>
        /// {"text":"魏晓东","key":"605100"}
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUser { get; set; }
        public string? lastModifyDate { get; set; } = "2023-05-12 18:51:55";
        /// <summary>
        /// {"text":"榕城区榕东社区卫生服务中心","key":"445202605"
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUnit { get; set; }
    }


    public class PUBVisitListQuery
    {
        public string serviceId { get; set; } = "chis.diabetesVisitService";
        [JsonPropertyName("method")]
        public string method { get; set; } = "execute";
        public string schema { get; set; } = "chis.application.dbs.schemas.MDC_DiabetesVisitPlan";
        public List<object> cnd { get; set; } = new List<object> {
            new object[] {"and",
                new List<object> { "eq", new string[] { "$", "a.businessType" }, new string[] { "s", "2" } },
             new object[]   {"like", new object[] { "$", "b.idCard" }, new object[]{ "s", "%421182199311130057%" } } } };

        public int pageSize { get; set; } = 50;
        public int pageNo { get; set; } = 1;
        public string serviceAction { get; set; } = "listDiabetesVistPlan";
    }


    public class PUBVisitListQueryResult
    {
        public int pageSize { get; set; } = 50;
        public int code { get; set; } = 200;
        public int totalCount { get; set; } = 2;
        public List<PUBVisitListQueryResultBodyItem> body { get; set; }
        public int pageNo { get; set; } = 1;
    }
    public class PUBVisitListQueryResultBodyItem
    {
        public string visitDoctor { get; set; } = "605100";
        public string homePlace { get; set; } = "中国天津市市辖区河东区";
        public int trainTimesWeek { get; set; } = 11;
        public string endDate { get; set; } = "2023-06-29 00:00:00";
        public string idCard { get; set; } = "421182199311130057";
        public string mobileNumber { get; set; } = "13419597065";
        public string planStatus { get; set; } = "1";
        public string recordId { get; set; } = "44520200621302093";
        public string empiId { get; set; } = "875f62096e46447780fb11473f9b912f";
        public string visitId { get; set; } = "0000000000142024";
        public string planId { get; set; } = "0000000002522223";
        public string planDate { get; set; }
    }

}