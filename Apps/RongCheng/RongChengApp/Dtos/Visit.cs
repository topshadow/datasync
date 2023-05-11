using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace RongChengApp.Dtos
{
    /// <summary>
    /// 查询某日期随访记录是否存在
    /// </summary>
    public class SaveRepairPlanInput
    {
        public SaveRepairPlanBodyInput body { get; set; }
        /// <summary>
        /// 默认execute  非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("execute")]
        public string? method { get; set; } = "execute";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.application.pub.schemas.PUB_VisitPlan_Mdc")]

        public string schema { get; set; } = "chis.application.pub.schemas.PUB_VisitPlan_Mdc";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("saveRepairPlan")]

        public string serviceAction { get; set; } = "saveRepairPlan";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.hypertensionVisitService")]
        public string serviceId { get; set; } = "chis.hypertensionVisitService";

    }

    public class SaveRepairPlanBodyInput
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        /// <value></value>
        public DateTime beginDate { get; set; }
        /// <summary>
        /// 要查询的日期
        /// </summary>
        /// <value></value>
        public DateTime dateField { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        /// <value></value>
        public string? empiid { get; set; }
        public DateTime? endDate { get; set; }

        /// <summary>
        /// phrId
        /// </summary>
        /// <value></value>
        public string? phrld { get; set; }

    }


    public class SaveRepairPlanResult
    {

        public SaveRepairPlanResultBody? body { get; set; }

        public int code { get; set; }
    }

    public class SaveRepairPlanResultBody
    {
        public bool? exist { get; set; }
    }


    /// <summary>
    /// 初始化随访计划
    /// </summary>
    public class PlanVisitInitializeInput
    {
        /// <summary>
        /// 方法,非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("execute")]
        public string method { get; set; } = "execute";
        /// <summary>
        /// 服务名,非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("visitInitialize")]
        public string serviceAction { get; set; } = "visitInitialize";

        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.hypertensionVisitService")]
        public string serviceId { get; set; } = "chis.hypertensionVisitService";

        public PlanVisitInitializeBodyInput body { get; set; }

    }

    /// <summary>
    /// 初始化随访记录请求体
    /// </summary>
    public class PlanVisitInitializeBodyInput
    {

        /// <summary>
        /// 业务类型,非必填
        /// </summary>
        /// <value></value>
        [DefaultValue(1)]
        public int businessType { get; set; } = 1;
        public string empiId { get; set; }
        [DefaultValue("chis.application.mpi.schemas.MPI_DemographicInfo")]
        public string empiSchema { get; set; } = "chis.application.mpi.schemas.MPI_DemographicInfo";
        [DefaultValue("chis.application.hy.schemas.MDC_HypertensionFixGroup")]
        public string fixGroupSchema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionFixGroup";
        [DefaultValue("chis.application.hc.schemas.HC_HealthCheck_list")]
        public string healthCheckSchema { get; set; } = "chis.application.hc.schemas.HC_HealthCheck_list";
        public string lastBeginDate { get; set; } = "2023-04-20";

        [DefaultValue("2023-05-04")]
        public string lastEndDate { get; set; } = "2023-05-04";

        [DefaultValue("chis.application.hr.schemas.EHR_LifeStyle")]
        public string lifeStyleSchema { get; set; } = "chis.application.hr.schemas.EHR_LifeStyle";
        public string occurDate { get; set; } = "2023-04-20";

        public string phrId { get; set; }
        public string planDate { get; set; } = "2023-04-27";

        public string planId { get; set; } =
        "0000000002489878";
        public string recordSchema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionRecord";
        public string visitId { get; set; } = "";

    }

    /// <summary>
    /// 查询全年的随访记录
    /// </summary>
    public class GetCurYearVisitPlanInput
    {
        /// <summary>
        /// 服务
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.hypertensionVisitService")]
        public string serviceId { get; set; } = "chis.hypertensionVisitService";
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <value></value>
        [DefaultValue("execute")]
        public string method { get; set; } = "execute";
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.application.pub.schemas.PUB_VisitPlan_Mdc")]
        public string schema { get; set; } = "chis.application.pub.schemas.PUB_VisitPlan_Mdc";

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DefaultValue(25)]
        public int pageSize { get; set; } = 25;

        /// <summary>
        /// 分页下标从1开始
        /// </summary>
        /// <value></value>
        [DefaultValue(1)]
        public int pageNo { get; set; } = 1;
        /// <summary>
        /// 默认全年
        /// </summary>
        [DefaultValue("full-T")]
        public string queryDate { get; set; } = "full-T";
        [DefaultValue("getCurYearVisitPlan")]
        public string? serviceAction { get; set; } = "getCurYearVisitPlan";
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? empiId { get; set; }
    }
    /// <summary>
    /// 查询全年访问结果
    /// </summary>
    public class GetCurYearVisitPlanResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        /// <value></value>
        public int code { get; set; }
        public List<GetCurYearVisitPlanResultItem> body { get; set; }
    }
    /// <summary>
    /// 查询全年访问记录明细
    /// </summary>
    public class GetCurYearVisitPlanResultItem
    {
        public string? beginDate { get; set; }
        /// <summary>
        /// 业务类型 1 高血压随访
        /// </summary>
        /// <value></value>
        public string? businessType { get; set; }

        /// <summary>
        ///  描述   "高血压随访";
        /// /// </summary>
        /// <value></value>
        public string? businessType_text { get; set; }


        public string? empiId { get; set; }
        /// <summary>
        /// 例如 "2023-06-13";
        /// </summary>
        /// <value></value>
        public string? endDate { get; set; }
        public int? extend1 { get; set; }
        public string? fixGroupDate { get; set; }
        public string? isReferral { get; set; }
        public string? isReferral_text { get; set; }
        public string? planDate { get; set; }
        public string? planId { get; set; }

        /// <summary>
        /// 0 应访
        /// 1 已经访问
        /// 3. 未访
        /// </summary>
        public string? planStatus { get; set; }
        /// <summary>
        /// 0 应访
        /// 1 已经访问
        /// 3. 未访
        /// </summary>
        /// <value></value>
        public string? planStatus_text { get; set; }

        public string? recordId { get; set; }
        public object? sn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? visitDate { get; set; }

        public string? visitId { get; set; }
    }

    public class VisitInitializeInput
    {
        [DefaultValue("chis.hypertensionVisitService")]
        public string serviceId { get; set; } = "chis.hypertensionVisitService";
        [DefaultValue("visitInitialize")]
        public string serviceAction { get; set; } = "visitInitialize";

        [DefaultValue("execute")]
        public string method { get; set; } = "execute";
        public VisitInitializeInputBody body { get; set; }
    }
    public class VisitInitializeInputBody
    {
        public string lifeStyleSchema { get; set; } = "chis.application.hr.schemas.EHR_LifeStyle";
        public string healthCheckSchema { get; set; } = "chis.application.hc.schemas.HC_HealthCheck_list";
        public string fixGroupSchema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionFixGroup";
        public string recordSchema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionRecord";
        public string empiSchema { get; set; } = "chis.application.mpi.schemas.MPI_DemographicInfo";
        public string empiId { get; set; } = "ece7af90a1cb46d68aa7f64fcf119703";
        public string phrId { get; set; } = "44520200620207607";
        public string visitId { get; set; }
        public string lastEndDate { get; set; } = "2023-05-05";
        public string lastBeginDate { get; set; } = "2023-04-21";
        public string planDate { get; set; } = "2023-04-28";
        public string planId { get; set; } = "0000000002489885";
        public string occurDate { get; set; } = "2023-04-21";
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DefaultValue(1)]
        public int businessType { get; set; } = 1;
    }
    /// <summary>
    /// 获取随访记录详情
    /// </summary>
    public class GetVisitInfoInput
    {
        /// <summary>
        /// 非必填 默认chis.hypertensionVisitService
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.hypertensionVisitService")]
        public string serviceId { get; set; } = "chis.hypertensionVisitService";
        /// <summary>
        /// 非必填 默认getVisitInfo
        /// </summary>
        /// <value></value>
        [DefaultValue("getVisitInfo")]
        public string serviceAction { get; set; } = "getVisitInfo";

        /// <summary>
        /// 非必填 默认execute
        /// </summary>
        /// <value></value>
        [DefaultValue("execute")]
        public string method { get; set; } = "execute";
        public GetVisitInfoInputBody body { get; set; }

    }
    public class GetVisitInfoInputBody
    {
        /// <summary>
        /// 记录的visitId
        /// </summary>
        /// <value></value>
        public string pkey { get; set; }
        /// <summary>
        /// 随访id
        /// </summary>
        /// <value></value>
        public string planId { get; set; }
        public string empiId { get; set; }
    }

    /// <summary>
    /// 获取一条随访记录详情结果
    /// </summary>
    public class GetVisitInfoResult
    {
        public int code { get; set; }
        public GetVisitInfoResultBody body { get; set; }
    }
    public class GetVisitInfoResultBody
    {
        public decimal height { get; set; }
        public object? manaUnitId { get; set; }
        public GetVisitInfoResultVisitInfo visitInfo { get; set; }

    }

    public class GetVisitInfoResultVisitInfo
    {
        public List<object>? MDC_HypertensionMedicine_list { get; set; }
        public KeyText acceptDegree { get; set; }
        /// <summary>
        /// 转诊机构及科室
        /// </summary>
        /// <value></value>
        public string agencyAndDept { get; set; }
        /// <summary>
        /// 辅助检查 
        /// 长度限制50
        /// </summary>
        /// <value></value>
        public string? auxiliaryCheck { get; set; }
        /// <summary>
        /// 体质指数 
        /// </summary>
        /// <value></value>
        public decimal? bmi { get; set; }
        public KeyText cardiovascularEvent { get; set; }
        public string checkId { get; set; }
        public object complication { get; set; }
        public object complicationIncrease { get; set; }
        /// <summary>
        /// 血压 收缩压
        /// </summary>
        /// <value></value>
        public decimal? constriction { get; set; }
        /// <summary>
        /// 治疗方式 
        /// 1  服药
        /// 2  无需服药
        /// </summary>
        /// <value></value>
        public KeyText cure { get; set; }

        /// <summary>
        /// 症状  
        /// key 9 无症状 1 头痛头晕, 2 恶心呕吐3.眼花耳鸣4.呼吸困难5.心悸胸闷6.鼻衄出血不止7.四肢发麻8.下肢水肿 10.其他 
        /// 示例 结果 
        /// {key: "1,2,3,4,5,6,7,8,10"
        /// text: "头痛头晕,恶心呕吐,眼花耳鸣,呼吸困难,心悸胸闷,鼻衄出血不止,四肢发麻,下肢水肿,其他"}
        /// </summary>
        /// <value></value>
        public KeyText? currentSymptoms { get; set; }
        /// <summary>
        /// 血压 舒张压
        /// </summary>
        /// <value></value>
        public decimal? diastolic { get; set; }
        /// <summary>
        /// 日饮酒 (单位两)
        /// </summary>
        /// <value></value>
        public decimal? drinkCount { get; set; }
        public object? drinkTypeCode { get; set; }
        public string empiId { get; set; }
        public object? healthProposal { get; set; }
        public object? healthRecipe { get; set; }
        /// <summary>
        /// 心率  分钟/次 例如 123
        /// </summary>
        /// <value></value>
        public int? heartRate { get; set; }
        public object hypertensionGroup { get; set; }
        public object incorrectMedicine { get; set; }
        public string inputDate { get; set; }
        public KeyText inputUnit { get; set; }
        /// <summary>
        ///  随访医生 
        /// key  例如 605100 账号
        /// text 例如 魏晓东
        /// </summary>
        /// <value></value>
        public KeyText inputUser { get; set; }
        public string? lastModifyDate { get; set; }
        public KeyText lastModifyUnit { get; set; }
        public KeyText lastModifyUser { get; set; }
        public KeyText lateInput { get; set; }
        public KeyText loseWeight { get; set; }
        public object manaDoctorId { get; set; }
        public object manaUnitId { get; set; }
        /// <summary>
        /// 服药依从性
        /// 1 规律
        /// 2. 间断
        /// 3. 不服药
        /// </summary>
        /// <value></value>
        public KeyText medicine { get; set; }
        /// <summary>
        ///  药物不良反应
        /// y 有
        /// n 无
        /// </summary>
        /// <value></value>
        public KeyText medicineBadEffect { get; set; }
        /// <summary>
        /// 药物不良反应文本描述
        /// </summary>
        /// <value></value>
        public object medicineBadEffectText { get; set; }
        /// <summary>
        /// 不服药的原因
        /// 1 经济原因
        /// 2. 忘记
        /// 3. 不良反应
        ///  4.     配药不方便
        /// 99 其他
        /// 
        /// </summary>
        /// <value></value>
        public KeyText medicineNot { get; set; }
        /// <summary>
        /// 服药依从性原因 其他文本描述
        /// </summary>
        /// <value></value>
        public string medicineOtherNot { get; set; }
        /// <summary>
        /// 默认 {"YYQK":""}
        /// </summary>
        /// <value></value>
        public Medicines? medicines { get; set; }
        /// <summary>
        /// 下次随访日期
        /// </summary>
        /// <value></value>
        public string nextDate { get; set; }
        public object noMedicine { get; set; }
        /// <summary>
        /// 转归原因
        /// 1 死亡
        /// 2. 迁出
        /// 3. 失访
        /// 4. 拒绝
        /// </summary>
        /// <value></value>
        public object noVisitReason { get; set; }
        public object nonMedicineWay { get; set; }
        /// <summary>
        /// 遵医行为
        ///  1  良好
        /// 2.  一般
        /// 3.  差
        /// </summary>
        /// <value></value>
        public object obeyDoctor { get; set; }
        public object otherReason { get; set; }
        /// <summary>
        ///  其他阳性体征
        /// </summary>
        /// <value></value>
        public string otherSigns { get; set; }
        /// <summary>
        /// 其他状态文本描述
        /// </summary>
        /// <value></value>
        public object otherSymptoms { get; set; }
        public string phrId { get; set; }
        /// <summary>
        /// 心理调整
        /// "1" 良好
        /// "2" 一般
        /// "3" 差
        /// </summary>
        /// <value></value>
        public KeyText psychologyChange { get; set; }
        /// <summary>
        /// 转诊原因
        /// </summary>
        /// <value></value>
        public string referralReason { get; set; }
        public KeyText riskLevel { get; set; }
        public object riskiness { get; set; }
        /// <summary>
        /// 盐摄入
        /// key 1  轻
        /// key 2 中
        /// key 3 重
        /// </summary>
        /// <value></value>
        public KeyText salt { get; set; }
        /// <summary>
        /// 日吸烟量
        /// </summary>
        /// <value></value>
        public decimal? smokeCount { get; set; }
        /// <summary>
        ///  目标体质指数
        /// </summary>
        /// <value></value>
        public decimal? targetBmi { get; set; }
        /// <summary>
        /// 目标日饮酒 (单位两)
        /// </summary>
        /// <value></value>
        public decimal? targetDrinkCount { get; set; }
        public object targetHeartRate { get; set; }
        public object targetHurt { get; set; }
        /// <summary>
        /// 目标盐摄入
        /// key 1  轻
        /// key 2 中
        /// key 3 重
        /// </summary>
        /// <value></value>
        public KeyText targetSalt { get; set; }
        /// <summary>
        /// 目标日吸烟量
        /// </summary>
        /// <value></value>
        public int? targetSmokeCount { get; set; }
        /// <summary>
        /// 目标周训练 每次训练时长 /分钟
        /// </summary>
        /// <value></value>
        public int? targetTrainMinute { get; set; }
        /// <summary>
        /// 目标周训练次数
        /// </summary>
        /// <value></value>
        public int? targetTrainTimesWeek { get; set; }
        /// <summary>
        /// 目标体重 kg
        /// </summary>
        /// <value></value>
        public decimal? targetWeight { get; set; }
        /// <summary>
        /// 周训练 ,每次训练时间 /分钟
        /// </summary>
        /// <value></value>
        public int? trainMinute { get; set; }
        /// <summary>
        /// 周训练次数
        /// </summary>
        /// <value></value>
        public int? trainTimesWeek { get; set; }
        public object treatEffect { get; set; }
        /// <summary>
        /// 随访日期
        /// </summary>
        /// <value></value>
        public string visitDate { get; set; }
        public KeyText visitDoctor { get; set; }
        /// <summary>
        /// 转归
        /// 
        /// 1 继续随访
        /// 2. 暂时失访
        /// 3. 终止管理
        /// 
        /// </summary>
        /// <value></value>
        public KeyText visitEffect { get; set; }
        /// <summary>
        ///随访分类
        /// 1控制满意
        /// 2.控制不满意
        /// 3. 不良反应
        /// 4.并发症
        /// </summary>
        /// <value></value>
        public KeyText visitEvaluate { get; set; }
        public string visitId { get; set; }
        public KeyText visitUnit { get; set; }
        /// <summary>
        /// 随访方式
        /// key 1 text 门诊
        /// key 2  text 家庭
        /// key 3 text 电话
        /// </summary>
        /// <value></value>
        public KeyText visitWay { get; set; }
        public object waistLine { get; set; }
        /// <summary>
        /// 体重 kg
        /// </summary>
        /// <value></value>
        public decimal? weight { get; set; }
    }

    public class KeyText
    {
        public object key { get; set; }
        public string text { get; set; }
    }
    public class Medicines
    {
        /// <summary>
        /// 合成文字描述
        /// 例如艾叶 1.0#22#颗/   白术 122.0#11#包/   
        /// </summary>
        /// <value></value>
        public string? YYQK { get; set; }
        /// <summary>
        /// 药物id
        /// </summary>
        /// <value></value>
        public string? drugId1 { get; set; }
        public string? drugId2 { get; set; }
        public string? drugId3 { get; set; }
        public string? drugId4 { get; set; }
        public string? drugId5 { get; set; }

        public string? drugNames1 { get; set; }
        public string? drugNames2 { get; set; }
        public string? drugNames3 { get; set; }
        public string? drugNames4 { get; set; }
        public string? drugNames5 { get; set; }

        public string? drugNames1_YPXH { get; set; }
        public string? drugNames2_YPXH { get; set; }
        public string? drugNames3_YPXH { get; set; }
        public string? drugNames4_YPXH { get; set; }
        public string? drugNames5_YPXH { get; set; }


        public string? everyDayTime1 { get; set; }
        public string? everyDayTime2 { get; set; }
        public string? everyDayTime3 { get; set; }
        public string? everyDayTime4 { get; set; }
        public string? everyDayTime5 { get; set; }

        public decimal? medicineDosage1 { get; set; }
        public decimal? medicineDosage2 { get; set; }
        public decimal? medicineDosage3 { get; set; }
        public decimal? medicineDosage4 { get; set; }
        public decimal? medicineDosage5 { get; set; }


        public string? medicineFrequency1 { get; set; }
        public string? medicineFrequency2 { get; set; }
        public string? medicineFrequency3 { get; set; }
        public string? medicineFrequency4 { get; set; }
        public string? medicineFrequency5 { get; set; }

        public string? medicineId1 { get; set; }
        public string? medicineId2 { get; set; }
        public string? medicineId3 { get; set; }
        public string? medicineId4 { get; set; }
        public string? medicineId5 { get; set; }

        public string? medicineName1 { get; set; }
        public string? medicineName2 { get; set; }
        public string? medicineName3 { get; set; }
        public string? medicineName4 { get; set; }
        public string? medicineName5 { get; set; }

    }

    /// <summary>
    /// 自动同步随访记录
    /// </summary>
    public class AutoAddVisitRecordInput
    {
        /// <summary>
        /// 用户的身份证号
        /// </summary>
        /// <value></value>
        public string idcard { get; set; }

        public string bmi { get; set; } = "90";
        public string targetBmi { get; set; } = "50";
        public string constriction { get; set; } = "11";
        public string cure { get; set; } = "1";
        public string currentSymptoms { get; set; } = "0";
        public string diastolic { get; set; } = "50";
        public string drinkCount { get; set; } = "1";
        public string targetDrinkCount { get; set; } = "3";
        public DateTime inputDate { get; set; }
        public string weight { get; set; } = "60";
        public string targetWeight { get; set; } = "65";
        public string heartRate { get; set; } = "70";
        public string inputUser { get; set; } = "605100";
        public string smokeCount { get; set; } = "1";
        public string targetSmokeCount { get; set; } = "1";
        public string psychologyChange { get; set; }
        public string obeyDoctor { get; set; } = "1";
        public string referralReason { get; set; } = String.Empty;
        public string medicine { get; set; } = String.Empty;
        public string medicineBadEffect { get; set; } = String.Empty;
        public string medicineBadEffectText { get; set; } = String.Empty;
        public string medicineNot { get; set; } = String.Empty;
        public string medicineOtherNot { get; set; } = String.Empty;
        public string salt { get; set; } = "1";
        public string targetSalt { get; set; } = "1";
        public string targetTrainMinute { get; set; } = "1";
        public string targetTrainTimesWeek { get; set; } = "1";
        public string trainMinute { get; set; } = "1";
        public string trainTimesWeek { get; set; } = "1";
        public string visitWay { get; set; } = "1";
        public string auxiliaryCheck { get; set; }
        /// <summary>
        /// 下次随访日期
        /// </summary>
        /// <value></value>
        public DateTime nextDate { get; set; }
        public string visitEvaluate { get; set; } = "1";
        public string targetSmeCount { get; set; } = "1";

    }
    public class AutoAddVisitRecordResult
    {
        public bool ok { get; set; }
        public string message { get; set; }
    }
    /// <summary>
    /// 保存随访记录
    /// </summary>
    public class SaveHypertensionVisitInput
    {

        public string method { get; set; } = "execute";
        public string op { get; set; } = "update";
        public string schema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionVisit";
        public string serviceAction { get; set; } = "saveHypertensionVisit";
        public string serviceId { get; set; } = "chis.hypertensionVisitService";
        public SaveHypertensionVisitInputBody body { get; set; }

    }
    public class SaveHypertensionVisitInputBody
    {
        public string? acceptDegree { get; set; } = String.Empty;
        public string agencyAndDept { get; set; } = "";
        public string auxiliaryCheck { get; set; } = String.Empty;
        public string birthday { get; set; } = String.Empty;
        public string? bmi { get; set; } = String.Empty;
        public string? cardiovascularEvent { get; set; } = "1";
        public string complication { get; set; } = "";
        public string complicationIncrease { get; set; } = "";
        public string constriction { get; set; } = "130";
        public bool controlBad { get; set; } = true;
        public string cure { get; set; } = String.Empty;
        public string currentSymptoms { get; set; } = String.Empty;
        public string diastolic { get; set; } = String.Empty;
        public string drinkCount { get; set; } = String.Empty;
        public string drinkTypeCode { get; set; } = String.Empty;
        public string drugNames1 { get; set; } = String.Empty;
        public string drugNames2 { get; set; } = String.Empty;
        public string drugNames3 { get; set; } = String.Empty;
        public string drugNames4 { get; set; } = String.Empty;
        public string drugNames5 { get; set; } = String.Empty;
        public string empiId { get; set; } = String.Empty;
        public string everyDayTime1 { get; set; } = String.Empty;
        public string everyDayTime2 { get; set; } = String.Empty;
        public string everyDayTime3 { get; set; } = String.Empty;
        public string everyDayTime4 { get; set; } = String.Empty;
        public string everyDayTime5 { get; set; } = String.Empty;
        public string fixGroupDate { get; set; } = "2023-04-22";
        public string healthProposal { get; set; } = String.Empty;
        public string healthRecipe { get; set; } = String.Empty;
        public string heartRate { get; set; } = String.Empty;
        public string height { get; set; } = "170";
        public object hypertensionGroup { get; set; }
        public string hypertensionGroupName { get; set; } = String.Empty;
        public string idCard { get; set; } = String.Empty;
        public string incorrectMedicine { get; set; } = String.Empty;
        public DateTime inputDate { get; set; }

        public string inputUnit { get; set; }
        public string inputUser { get; set; } = "605100";
        public DateTime lastModifyDate { get; set; }
        public string lastModifyUnit { get; set; }
        //"445202605"
        public string lastModifyUser { get; set; } = "605100";
        public string lateInput { get; set; } = "";
        public string loseWeight { get; set; } = "1";
        public string manaDoctorId { get; set; } = String.Empty;
        public string medicine { get; set; } = String.Empty;
        public string medicineBadEffect { get; set; } = String.Empty;
        public string medicineBadEffectText { get; set; } = String.Empty;
        public List<object> medicineList = new List<object>();
        public string medicineNot { get; set; } = String.Empty;

        public string medicineOtherNot { get; set; } = String.Empty;
        public string medicineType1 { get; set; } = String.Empty;
        public string medicineType2 { get; set; } = String.Empty;
        public string medicineType3 { get; set; } = String.Empty;
        public string medicineType4 { get; set; } = String.Empty;
        public string medicineType5 { get; set; } = String.Empty;
        public string medicineUnit1 { get; set; } = String.Empty;
        public string medicineUnit2 { get; set; } = String.Empty;
        public string medicineUnit3 { get; set; } = String.Empty;
        public string medicineUnit4 { get; set; } = String.Empty;
        public string medicineUnit5 { get; set; } = String.Empty;
        public string needAssess { get; set; } = String.Empty;
        public bool needChangeGroup { get; set; } = false;
        public bool needInsertPlan { get; set; } = false;
        public bool needReferral { get; set; } = false;
        /// <summary>
        ///  "2023-05-06"
        /// </summary>
        /// <value></value>
        public string nextDate { get; set; }
        public string nextPlanId { get; set; } = "0000000002492377";
        public string noMedicine { get; set; } = String.Empty;
        public string noVisitReason { get; set; } = String.Empty;
        public string nonMedicineWay { get; set; } = String.Empty;
        public string obeyDoctor { get; set; } = String.Empty;
        public object? oldGroup { get; set; } = null;
        public string oneDosage1 { get; set; } = String.Empty;
        public string oneDosage2 { get; set; } = String.Empty;
        public string oneDosage3 { get; set; } = String.Empty;
        public string oneDosage4 { get; set; } = String.Empty;
        public string oneDosage5 { get; set; } = String.Empty;
        public string otherReason { get; set; } = String.Empty;
        public string otherSigns { get; set; } = String.Empty;
        public string otherSymptoms { get; set; } = String.Empty;
        public string personName { get; set; } = String.Empty;
        public string phoneNumber { get; set; } = String.Empty;
        public string phrId { get; set; } = "44520200620701581";
        public string planDate { get; set; } = "2023-04-22";
        public string planId { get; set; } = "0000000002482002";
        public string psychologyChange { get; set; } = String.Empty;
        public string referralReason { get; set; } = String.Empty;
        public string regionCode { get; set; } = String.Empty;
        public string regionCode_text { get; set; } = String.Empty;
        public string riskLevel { get; set; } = String.Empty;
        public string riskiness { get; set; } = String.Empty;
        public string? salt { get; set; } = String.Empty;
        public string sexCode { get; set; } = String.Empty;
        public string smokeCount { get; set; } = "10";
        public int sn { get; set; } = 1;
        public string status { get; set; } = "0";
        public string targetBmi { get; set; } = String.Empty;
        public string targetDrinkCount { get; set; } = "";

        public string targetHurt { get; set; } = "";
        public string targetSalt { get; set; } = "";
        public string targetSmokeCount { get; set; } = "1";
        public string targetTrainMinute { get; set; } = "11";
        public string targetTrainTimesWeek { get; set; } = "11";
        public string targetWeight { get; set; } = String.Empty;
        public string trainMinute { get; set; } = "11";
        public string trainTimesWeek { get; set; } = "11";
        public string treatEffect { get; set; } = String.Empty;
        public string visitDate { get; set; } = "2023-04-22";
        public string visitDoctor { get; set; } = "605100";
        public string visitEffect { get; set; } = "1";
        public string visitEvaluate { get; set; } = "2";
        public string visitId { get; set; } = "0000000000261454";
        public string visitUnit { get; set; } = "445202605";
        public string visitWay { get; set; } = "1";
        public List<object> vmList { get; set; } = new List<object>();
        public string waistLine { get; set; } = String.Empty;
        public string weight { get; set; } = "62";
    }
    public class SaveHypertensionVisitResult
    {
        public int code { get; set; }
        public string? msg { get; set; }
    }
}







/**
  "random": "041d4bef94cd9d36e98f68c17ae1ffda0790d68e5397ec3fe7f1b9c8f34b6a01ff29ac17d2f426fb7df523a64b4319caba9428153a518e8541414345a9fb295804bea8b6b51d41f8657e7f45fc231e2c25e5d3d71c16bb7d6060e56a8a6ee7602925177b3f50acae6aab43",
  "cookie": "JSESSIONID=13B6984CF50A365962C10A75D4DC0978; 605100=%u9B4F%u6653%u4E1C@photo/605100.jpg; cur_cauid=",
  "serviceId": "chis.hypertensionVisitService",
  "method": "execute",
  "schema": "chis.application.pub.schemas.PUB_VisitPlan_Mdc",
  "pageSize": 25,
  "pageNo": 1,
  "serviceAction": "getCurYearVisitPlan",
  "empiId": "ece7af90a1cb46d68aa7f64fcf119703"
*/


