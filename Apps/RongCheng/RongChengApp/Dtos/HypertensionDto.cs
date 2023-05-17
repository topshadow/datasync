using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RongChengApp.Dtos
{
    /// <summary>
    /// 基础输出
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseOutput<T>
    {
        public List<T> body { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        /// <value></value>
        public int code { get; set; }
        public int pageNo { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        /// <value></value>
        public int pageSize { get; set; }

        /// <summary>
        /// 总计数量
        /// </summary>
        /// <value></value>
        public int totalCount { get; set; }
    }





    /// <summary>
    /// 查询高血压
    /// </summary>

    public class QueryHypertension
    {
        /// <summary>
        /// 查询分页下标,默认从1开始
        /// </summary>
        /// <value></value>
        [DefaultValue(1)]
        public int pageNo { get; set; } = 1;
        /// <summary>
        /// 查询分页大小
        /// </summary>
        /// <value></value>
        [DefaultValue(25)]
        public int pageSize { get; set; } = 25;

        /// <summary>
        /// 服务操作  不需要传递
        /// </summary>
        /// <value></value>
        [DefaultValue("listHypertensionRecord")]
        public string serviceAction { get; set; }
        = "listHypertensionRecord";
        /// <summary>
        /// 查询年,默认当前年
        /// </summary>
        /// <value></value>
        public int year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// 服务id 不需要传递,默认chis.hypertensionService
        /// </summary>
        /// <value></value>
        [DefaultValue("chis.hypertensionService")]
        public string serviceId { get; set; } = "chis.hypertensionService";
        /// <summary>
        /// 查询拼接条件
        /// 默认 ["eq",["$","a.status"],["s","0"]]
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <returns></returns>
        public List<object> cnd { get; set; } = new List<object>() { "eq", new List<string> { "$", "a.status" }, new List<string>() { "s", "0" } };
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DefaultValue("1")]
        public string checkType { get; set; } = "1";
        /// <summary>
        /// method
        /// </summary>
        /// <value></value>
        [System.Text.Json.Serialization.JsonPropertyName(name: "method")]
        [DefaultValue("execute")]
        public string method { get; set; } = "execute";
        [DefaultValue("chis.application.hy.schemas.MDC_HypertensionRecord")]
        public string schema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionRecord";
    }



    /// <summary>
    /// 每一个患者的信息
    /// </summary>
    public class QueryHypertensionResultItem
    {
        /// <summary>
        /// 患者地址
        /// 如 "广东省揭阳市榕城区榕东街道梅兜村"
        /// </summary>
        /// <value></value>
        public string? address { get; set; }
        /// <summary>
        /// 服药后 ,血压正常原因
        /// </summary>
        /// <value></value>
        public string? afterMedicine { get; set; }
        /// <summary>
        /// 患者生日 格式为1956-09-26
        /// </summary>
        /// <value></value>
        public DateTime? birthday { get; set; }

        /// <summary>
        /// 检查类型，例如 2023年未检
        /// </summary>
        /// <value></value>
        public string? checkType { get; set; }
        // clinicAddress
        // : 
        // "1"
        // clinicAddress_text
        // : 
        // "本院"
        // complication
        // : 
        // "0"
        // complication_text
        // : 
        // ""
        // confirmDate
        // : 
        // "2023-01-05"
        // constriction
        // : 
        // 132
        // contact
        // : 
        // "魏潮锥"
        // contactPhone
        // : 
        // "15914935999"
        // createDate
        // : 
        // "2023-01-12"
        // createUnit
        // : 
        // "445202605"
        // createUnit_text
        // : 
        // "榕城区榕东社区卫生服务中心"
        // createUser
        // : 
        // "605050"
        // createUser_text
        // : 
        // "陈翀"
        // diastolic
        // : 
        // 78
        // drink
        // : 
        // "1"
        // drinkOver
        // : 
        // "2"
        // drinkOver_text
        // : 
        // "否"
        // drink_text
        // : 
        // "从不"
        // eateHabit
        // : 
        // "1"
        // eateHabit_text
        // : 
        // "荤素均衡"
        public string? empiId { get; set; }
        // : 
        // "4D86083F73673B4BE05010AC32141F56"
        // endCheck
        // : 
        // "1"
        // endCheck_text
        // : 
        // "待核实"
        // familyHistroy
        // : 
        // "父亲：无父亲疾病史 母亲：无母亲疾病史 兄弟：无兄弟姐妹疾病史"
        // familyHistroy_text
        // : 
        // ""
        // height
        // : 
        // 144
        public string? idCard { get; set; }
        // inputDate
        // : 
        // "2023-01-28 10:50:18"
        // inputUnit
        // : 
        // "445202605"
        // inputUnit_text
        // : 
        // "榕城区榕东社区卫生服务中心"
        // inputUser
        // : 
        // "605050"
        // inputUser_text
        // : 
        // "陈翀"
        // lastModifyDate
        // : 
        // "2023-01-28 15:33:55"
        // lastModifyUnit
        // : 
        // "445202605"
        // lastModifyUnit_text
        // : 
        // "榕城区榕东社区卫生服务中心"
        // lastModifyUser
        // : 
        // "605100"
        // lastModifyUser_text
        // : 
        // "魏晓东"
        // manaDoctorId
        // : 
        // "605100"
        // manaDoctorId_text
        // : 
        // "魏晓东"
        // manaUnitId
        // : 
        // "445202605"
        // manaUnitId_text
        // : 
        // "榕城区榕东社区卫生服务中心"
        // mobileNumber
        // : 
        // "13640358215"
        // personName
        // : 
        // "叶秀红"
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? phrId { get; set; }
        // : 
        // "44520200620207432"
        // planTypeCode
        // : 
        // "-1"
        // recordSource
        // : 
        // "4"
        // recordSource_text
        // : 
        // "门诊就诊"
        // regionCode_text
        // : 
        // "梅兜村"
        // registeredPermanent
        // : 
        // "1"
        // registeredPermanent_text
        // : 
        // "户籍"
        // riskiness
        // : 
        // "0"
        // riskiness_text
        // : 
        // ""
        // sexCode
        // : 
        // "2"
        // sexCode_text
        // : 
        // "女"
        // signFlag
        // : 
        // "y"
        // signFlag_text
        // : 
        // "是"
        // smoke
        // : 
        // "4"
        // smoke_text
        // : 
        // "从不吸"
        // status
        // : 
        // "0"
        // status_text
        // : 
        // "正常"
        // targetHurt
        // : 
        // "0"
        // targetHurt_text
        // : 
        // ""
        // train
        // : 
        // "4"
        // train_text
        // : 
        // "不运动"
        // viability
        // : 
        // "1"
        // viability_text
        // : 
        // "完全自理"
        // visitDate
        // : 
        // "2023-01-28"
        // visitEffect
        // : 
        // "1"
        // visitEffect_text
        // : 
        // "继续随访"
        // weight
        // : 
        // 39.5
    }
    /// <summary>
    /// 查询高血压档案
    /// </summary>
    public class InitializeRecordInput
    {
        public string serviceId { get; set; } = "chis.hypertensionService";
        public string serviceAction { get; set; } = "initializeRecord";
        public string method { get; set; } = "execute";
        public InitializeRecordInputBody body { get; set; }

    }
    public class InitializeRecordInputBody
    {
        public string phrId { get; set; } = "44520200620207607";
        public string empiId { get; set; } = "ece7af90a1cb46d68aa7f64fcf119703";

    }

    public class InitializeRecordResult
    {
        public int? code { get; set; } = 200;
        public InitializeRecordResultBody body { get; set; }
    }
    public class InitializeRecordResultBody
    {
        public string manaDoctorId { get; set; } = "605100";
        [JsonPropertyName("chis.application.hy.schemas.MDC_HypertensionRecord_data")]
        public InitializeRecordResultBodyRecordData? MDC_HypertensionRecord_data { get; set; }





    }
    public class InitializeRecordResultBodyRecordData
    {
        public object MDC_HypertensionMedicine_actions { get; set; } = new { create = true, update = false };
        [JsonPropertyName("chis.application.hy.schemas.MDC_HypertensionRecord_data")]
        public object MDC_HypertensionRecord_data { get; set; } = new { riskiness = new { text = "", key = "0" } };
        /// <summary>
        /// 经常就诊地点
        /// {"text":"本院", "key":"1" },
        /// {"text":"其他一级医院", "key":"2" },
        ///key: "3"text: "本区二、三级医院"
        /// {text: "其他", key: "4"}
        /// </summary>
        /// <value></value>
        public KeyText? clinicAddress { get; set; }
        /// <summary>
        /// 家族史 母亲
        ///例如 无母亲疾病史
        /// </summary>
        /// <value></value>
        public string? jzsmq { get; set; }
        /// <summary>
        /// 建档人员
        /// {"text":"魏晓东","key":"605100"},
        /// </summary>
        /// <value></value>
        public KeyText? inputUser { get; set; }
        public string? empiId { get; set; } = "ece7af90a1cb46d68aa7f64fcf119703";
        /// <summary>
        /// 家族史 子女
        /// "无子女疾病史
        /// </summary>
        /// <value></value>
        public string jzszn { get; set; }
        /// <summary>
        /// 管辖机构
        /// {"text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? manaUnitId { get; set; }
        public string? visitDate { get; set; } = "2023-05-12";
        public string? lastModifyDate { get; set; } = "2023-05-12 16:08:11";
        /// <summary>
        /// 身高
        /// /// </summary>
        /// <value></value>
        public double? height { get; set; } = 170.0;
        /// <summary>
        /// 家族史父亲
        /// </summary>
        /// <value></value>
        public string jzsfq { get; set; } = "无父亲疾病史";
        /// <summary>
        /// { "text":"待核实","key":"1"},
        /// </summary>
        /// <value></value>
        public KeyText endCheck { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        /// <value></value>
        public double? weight { get; set; } = 70.0;
        /// <summary>
        /// 建档日期
        /// </summary>
        /// <value></value>
        public string? inputDate { get; set; } = "2023-04-24 15:16:31";
        /// <summary>
        /// 舒张压
        /// </summary>
        /// <value></value>
        public int? diastolic { get; set; }
        /// <summary>
        /// { "text":"","key":"0"}
        /// </summary>
        /// <value></value>
        public KeyText? targetHurt { get; set; }
        /// <summary>
        /// :{ "text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? createUnit { get; set; }
        /// <summary>
        /// { "text":"继续随访","key":"1"}
        /// </summary>
        /// <value></value>
        public KeyText? visitEffect { get; set; }
        /// <summary>
        /// { "text":"正常","key":"0"}
        /// </summary>
        /// <value></value>
        public KeyText? status { get; set; }
        /// <summary>
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUnit { get; set; }
        /// <summary>
        /// { "text":"","key":"0"}
        /// </summary>
        /// <value></value>
        public KeyText? complication { get; set; }
        /// <summary>
        /// 家族史(子女)
        /// 无兄弟姐妹疾病史
        /// </summary>
        /// <value></value>
        public string? jzsxdjm { get; set; }
        /// <summary>
        /// 生活自理能力
        /// key: "1"text: "完全自理"
        /// key 2 部分自理
        /// { "text":"完全不能自理","key":"3"}
        /// </summary>
        /// <value></value>
        public KeyText? viability { get; set; }
        /// <summary>
        /// 收缩压
        /// 110
        /// </summary>
        /// <value></value>
        public int? constriction { get; set; }
        /// <summary>
        /// { "text":"榕城区榕东社区卫生服务中心","key":"445202605"},
        /// </summary>
        /// <value></value>
        public KeyText? inputUnit { get; set; }
        /// <summary>
        /// 检出途径
        /// key: "1"text: "健康档案"
        /// key: "2"text: "35岁首审测压"
        /// key: "3"text: "普查"
        ///  key: "4"text: "门诊就诊"
        ///  key: "5" text: "体检"
        /// </summary>
        /// <value></value>
        public KeyText? recordSource { get; set; }
        public string? createDate { get; set; } = "2023-04-24 00:00:00";
        public string op { get; set; } = "update";
        public string planTypeCode { get; set; } = "-1";
        public string phrId { get; set; } = "44520200620207607";
        /// <summary>
        /// { "text":"魏晓东","key":"605100"}
        /// </summary>
        /// <value></value>
        public KeyText? lastModifyUser { get; set; }
        /// <summary>
        /// 临床确诊时间2023-04-01
        /// </summary>
        /// <value></value>
        public string? confirmDate { get; set; }
        /// <summary>
        /// 确诊单位
        /// </summary>
        /// <value></value>
        public string? confirmText { get; set; }
        public object? _actions { get; set; }
        /// <summary>
        /// 责任医生
        /// { "text":"魏晓东","key":"605100"},
        /// </summary>
        /// <value></value>
        public KeyText? manaDoctorId { get; set; }
        /// <summary>
        /// 创建人
        /// { "text":"魏晓东","key":"605100"}
        /// </summary>
        /// <value></value>
        public KeyText? createUser { get; set; }


        [JsonPropertyName("chis.application.hy.schemas.MDC_HypertensionMedicine_list")]
        public List<object> MDC_HypertensionMedicine_list { get; set; } = new List<object>();
        public object? MDC_HypertensionRecord_actions { get; set; }
    }

    public class SaveHypertensionRecordInput
    {
        public string serviceId { get; set; } = "chis.hypertensionService";
        public string method { get; set; } = "execute";
        public string op { get; set; } = "update";
        public string schema { get; set; } = "chis.application.hy.schemas.MDC_HypertensionRecord";
        public SaveHypertensionRecordInputBody body { get; set; }
        public string serviceAction { get; set; } = "saveHypertensionRecord";

    }
    public class SaveHypertensionRecordInputBody
    {
        public string? phrId { get; set; } = "44520200620207607";
        public string? empiId { get; set; } = "ece7af90a1cb46d68aa7f64fcf119703";
        public string? manaDoctorId { get; set; } = "605100";
        public string? manaUnitId { get; set; } = "445202605";
        public string? recordSource { get; set; } = "5";
        public string? confirmDate { get; set; } = "2023-04-01";
        public string? deaseAge { get; set; } = "1月";
        public string? confirmText { get; set; } = "test确诊单位";
        public string clinicAddress { get; set; } = "4";
        public string viability { get; set; } = "2";
        public string height { get; set; } = "170.00";
        public string? weight { get; set; } = "70.00";
        public string bmi { get; set; } = "24.22";
        public int? constriction { get; set; } = 110;
        public int? diastolic { get; set; } = 90;
        public string riskiness { get; set; } = "0";
        public string targetHurt { get; set; } = "0";
        public string complication { get; set; } = "0";
        public string afterMedicine { get; set; } = "";
        public string hypertensionGroup { get; set; } = "";
        public string riskLevel { get; set; } = "";
        public string createUnit { get; set; } = "445202605";
        public string createUser { get; set; } = "605100";
        public string createDate { get; set; } = "2023-04-24";
        public string inputUnit { get; set; } = "445202605";
        public string inputUser { get; set; } = "605100";
        public string inputDate { get; set; } = "2023-04-24 15:16:31";
        public string jzsfq { get; set; } = "";
        public string qtjzsfq { get; set; } = "";
        public string jzsmq { get; set; } = "";
        public string qtjzsmq { get; set; } = "";
        public string jzsxdjm { get; set; } = "";
        public string qtjzsxdjm { get; set; } = "";
        public string jzszn { get; set; } = "";
        public string qtjzszn { get; set; } = "";
        public string? endCheck { get; set; } = "1";
        public string? visitEffect { get; set; } = "1";
        public string? visitDate { get; set; } = "2023-05-12";
        public string? cancellationDate { get; set; }
        public string? cancellationReason { get; set; }
        public string? status { get; set; }
        public string planTypeCode { get; set; } = "-1";
        public string lastModifyUser { get; set; }
        public string? lastModifyDate { get; set; } = "2023-05-12 18:10:51";
        /// <summary>
        /// "445202605"}
        /// </summary>
        /// <value></value>
        public string? lastModifyUnit { get; set; }
    }

}