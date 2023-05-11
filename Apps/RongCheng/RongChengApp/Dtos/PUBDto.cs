using System.Text.Json.Serialization;
namespace RongChengApp.Dtos
{
    /// <summary>
    ///  糖尿病随访计划
    /// </summary>
    public class PUBGetCurYearVisitPlanInput
    {
        public string serviceId { get; set; } = "chis.diabetesVisitService";
        public string method { get; set; } = "execute";
        public string schema { get; set; } = "chis.application.pub.schemas.PUB_VisitPlan";
        public int pageSize { get; set; } = 50;
        public int pageNo { get; set; } = 1;
        public string queryDate { get; set; } = "full-T";
        public string serviceAction { get; set; } = "getCurYearVisitPlan";
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
    }
    public class DiabetesVisitServiceResultHtmlBody
    {

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
        public double? weight { get; set; }
        /// <summary>
        /// 目标体重
        /// </summary>
        /// <value></value>
        public double? targetWeight { get; set; }
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
        public object medicines { get; set; }
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
        public KeyText medicine { get; set; }
        /// <summary>
        /// { "text":"触及正常","key":"1"}
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
        public int? targetFood { get; set; } = 11;
        public string? checkId { get; set; } = "0000000000142024";
        public double? hbA1c { get; set; } = 11.0;
        public double? bmi { get; set; } = 34.26;

    }
}