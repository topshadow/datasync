using System.ComponentModel;
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

}