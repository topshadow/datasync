using System.ComponentModel;
namespace RongChengApp.Dtos
{
    public class EHRHealthRecordQueryByTimespanInput
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <value></value>
        public string start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <value></value>
        public string end { get; set; }
        /// <summary>
        /// 默认1
        /// </summary>
        /// <value></value>
        [DefaultValue(1)]
        public int pageNo { get; set; } = 1;
        /// <summary>
        /// 分页大小
        /// </summary>
        /// <value></value>
        [DefaultValue(20)]
        public int pageSize { get; set; } = 20;
    }
    /// <summary>
    /// 查询已审核信息
    /// </summary>
    public class EHRHealthRecordInput
    {
        public List<object> cnd { get; set; } = new List<object> { };
        /// <summary>
        /// 非必填
        /// chis.application.hr.schemas.EHR_HealthRecordAndMPIList
        /// chis.application.hr.schemas.EHR_HealthRecord
        /// </summary>
        /// <value></value>
        public string schema { get; set; } = "chis.application.hr.schemas.EHR_HealthRecordAndMPIList";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        public string? action { get; set; } = "";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        public string? serviceId { get; set; } = "chis.listSqlQuery";
        /// <summary>
        /// 非必填
        /// </summary>
        /// <value></value>
        public string method { get; set; } = "execute";
        public int pageNo { get; set; } = 1;
        public int pageSize { get; set; } = 200;

    }
    public class EHRHealthRecordResult
    {
        /// <summary>
        /// 200 正常
        /// </summary>
        /// <value></value>
        public int code { get; set; }
        /// <summary>
        /// 例如success
        /// </summary>
        /// <value></value>
        public string msg { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public List<EHRHealthRecordResultItem> body { get; set; }
    }
    public class EHRHealthRecordResultItem
    {
        public string? ER_text { get; set; }
        public string? FU_text { get; set; }
        public string? LAO_text { get; set; }
        public string? PID { get; set; }
        public string? RQBJ_text { get; set; }
        public string? address { get; set; }
        public string? adressNumber { get; set; }
        public string? birthday { get; set; }
        public string cancellationDate { get; set; }
        public string? cancellationDetail { get; set; }
        public string? cancellationReason { get; set; }
        public string? cancellationUnit { get; set; }
        public string? cancellationUser { get; set; }
        public string? contact { get; set; }
        public string contactPhone { get; set; }
        public string? createDate { get; set; }
        public string? createTime { get; set; }
        public string? createUnit_text { get; set; }
        public string? createUser_text { get; set; }
        public string? deadDate { get; set; }
        public string deadFlag_text { get; set; }
        public string deadReason { get; set; }
        public string? empiId { get; set; }
        public string? familyId { get; set; }
        public string? fatherId { get; set; }
        public string? fatherName { get; set; }
        public string? homePlace { get; set; }
        public string? homePlaceNumber { get; set; }
        public string? hrId { get; set; }

        public string? idCard { get; set; }
        public string? incomeSource { get; set; }

        public string? inputDate { get; set; }

        public string? inputUnit_text { get; set; }
        public string? inputUser_text { get; set; }

        public string? isAgrRegister { get; set; }
        public string? isDiabetes_text { get; set; }

        public string isHypertension_text { get; set; }

        public string? lastModifyDate { get; set; }

        public string? lastModifyTime { get; set; }
        public string? lastModifyUnit_text { get; set; }
        public string? lastModifyUser_text { get; set; }
        /// <summary>
        /// 主治医生名字
        /// 例如魏晓东
        /// </summary>
        /// <value></value>
        public string? manaDoctorId_text { get; set; }
        /// <summary>
        /// 榕城区榕东社区卫生服务中心
        /// 责任社区名字
        /// </summary>
        /// <value></value>
        public string? manaUnitId_text { get; set; }
        public string masterFlag_text { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        /// <value></value>
        public string? mobileNumber { get; set; }
        public string? motherId { get; set; }
        public string? motherName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string? oldlastVisitDate { get; set; }
        public string? partnerId { get; set; }
        public string? partnerName { get; set; }
        public string? per_Columns { get; set; }
        /// <summary>
        ///姓名
        ///   "张惠卿"
        /// </summary>
        /// <value></value>
        public string? personName { get; set; }
        public string? phrId { get; set; }
        public string? recordSource { get; set; }
        public string? regionCode_text { get; set; }
        public string? registeredPermanent_text { get; set; }
        public string? relaCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <value></value>
        public string? remark { get; set; }
        public string? review_text { get; set; }
        public string? sexCode_text { get; set; }
        public string? signFlag_text { get; set; }
        /// <summary>
        /// 状态文本
        /// </summary>
        /// <value></value>
        public string? status_text { get; set; }
        /// <summary>
        /// 人员工作类型代码 例如 不便分类的其他从业人员"
        /// </summary>
        public string? workCode_text { get; set; }
    }


    /// <summary>
    /// 查询个人基础信息
    /// </summary>
    public class LoadBasicPersonalInformationResult
    {
        /// <summary>
        /// 糖尿病档案信息
        /// </summary>
        /// <value></value>
        public PUBDiabetesRecordServiceResultBody? pubRecordInfo { get; set; }
        /// <summary>
        /// 高血压档案信息
        /// </summary>
        /// <value></value>
        public InitializeRecordResultBodyRecordData? hypertensionResult { get; set; }
        public string? GAO { get; set; }
        public string? PID { get; set; }
        public string? PKRK { get; set; }
        public string? TANG { get; set; }
        /// <summary>
        /// 过敏描述
        /// </summary>
        /// <value></value>
        public string? a_qt1 { get; set; }
        /// <summary>
        /// 现住址
        /// </summary>
        /// <value></value>
        public string? address { get; set; }
        /// <summary>
        /// 生日  "1993-11-13"
        /// </summary>
        /// <value></value>
        public string? birthday { get; set; }
        /// <summary>
        /// 血型类型
        /// key: "1" text: "A型"
        ///  key: "2" text: "B型"
        ///  key: "3" text: "O型"
        ///  key: "4" text: "AB型"
        /// key: "5" text: "不详"
        /// </summary>
        /// <value></value>
        public KeyText? bloodTypeCode { get; set; }
        /// <summary>
        /// {text: "居民身份证", key: "01"}
        /// {text: "护照", key: "03"}
        /// {text: "港澳居民身份证", key: "06"}
        /// {text:"台湾居民来往内地通行证",key:"07"}
        /// {text:"其他法定有效证件",key:"99"}
        /// </summary>
        /// <value></value>
        public string? cardType { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        /// <value></value>
        public string? contact { get; set; }
        /// <summary>
        /// 	联系人电话
        /// </summary>
        /// <value></value>
        public string? contactPhone { get; set; }

        /// <summary>
        /// 建档日期
        /// </summary>
        /// <value></value>
        public string? createDate { get; set; }
        /// <summary>
        /// 系统维护 无需填写
        /// </summary>
        /// <value></value>
        public string? createTime { get; set; }
        /// <summary>
        /// 创建社区
        /// 例如 {key: "445202605"
        ///text: "445202605"}
        /// </summary>
        /// <value></value>
        public KeyText? createUnit { get; set; }
        /// <summary>
        /// 建档
        /// 例如{ text: "魏晓东", key: "605100"}
        /// key 是医生的账号
        /// </summary>
        /// <value></value>
        public KeyText? createUser { get; set; }
        /// <summary>
        /// 死亡日期
        /// </summary>
        /// <value></value>
        public string? deadDate { get; set; }
        /// <summary>
        /// 是否死亡 
        /// { text: "否", key: "n"}
        /// { text: "否", key: "y"}
        /// </summary>
        /// <value></value>
        public KeyText? deadFlag { get; set; }
        /// <summary>
        /// 死亡原因
        /// </summary>
        /// <value></value>
        public string? deadReason { get; set; }
        /// <summary>
        /// 文化程度
        /// {key: "10",text: "研究生教育"}
        /// key: "20" text: "大学本科/专科教育"
        /// key "30" ,text :"大学专科和专科学校"
        /// key "40" text 中等专业学校
        /// key "50" text:"技工学校"
        /// {key: "60",text: "普通高级中学教育"}
        /// key 70 text:"初中"
        /// key 80 text:"小学"
        /// key 90 text:"文盲或半文盲 "
        /// key 91 text:"不详"
        /// </summary>
        /// <value></value>
        public KeyText? educationCode { get; set; }
        /// <summary>
        /// 残疾情况 可以逗号连接 如{"text": "听力残疾 ,精神残疾,其他残疾","key": "1103,1107,1199"}
        /// key: "1101"text: "无残疾"
        /// key: "1102"text: "视力残疾"
        /// key: "1103"text: "听力残疾 
        /// 1104 言语残疾
        /// 1105 肢体残疾
        /// 1106 智力残疾
        /// key: "1107,"text: 精神残疾
        /// key 1199 text其他残疾
        /// 
        /// key: ",,,,1107,1199,"
        ///text: " ,,,,精神残疾,,视力残疾"
        /// </summary>
        /// <value></value>
        public KeyText? diseasetextCheckCJ { get; set; }
        /// <summary>
        /// 其他残疾情况 文本描述
        /// </summary>
        /// <value></value>
        public string? cjqk_qtcj1 { get; set; }
        public string? qt_zn1 { get; set; }
        /// <summary>
        /// 家族历史  母亲, 可以逗号拼接 如 key: "0804,0805" text: "冠心病,慢性阻塞性肺疾病"
        /// key: "0801" text: "无残疾"
        /// key: "0802" text: "高血压"
        /// key: "0803" text: "糖尿病"
        /// key: "0804"text: "冠心病"
        /// key: "0805",text: "慢性阻塞性肺疾病"
        /// 0806,恶性肿瘤
        /// 0807 脑卒中
        /// 0808,重性精神疾病
        /// 0809,结核病
        /// 0810,肝炎
        /// 0811,先天畸形
        /// 0899,其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetextCheckMQ { get; set; }
        /// <summary>
        /// 家族历史  母亲 其他
        /// </summary>
        /// <value></value>
        public string? qt_mq1 { get; set; }
        /// <summary>
        /// 家族历史 父亲 其他
        /// </summary>
        /// <value></value>
        public string? qt_fq1 { get; set; }

        /// <summary>
        ///  { text: "无兄弟姐妹疾病史", key: "0901"}
        /// key: "0902",text: "高血压"
        /// key: "0901" text: "无残疾"
        /// key: "0902" text: "高血压"
        /// key: "0903" text: "糖尿病"
        /// key: "0904"text: "冠心病"
        /// key: "0905",text: "慢性阻塞性肺疾病"
        /// 0906,恶性肿瘤
        /// 0907 脑卒中
        /// 0908,重性精神疾病
        /// 0909,结核病
        /// 0910,肝炎
        /// 0911,先天畸形
        /// 0999,其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetextCheckXDJM { get; set; }
        /// <summary>
        /// 职业病 文本描述
        /// </summary>
        /// <value></value>
        public string? diseasetext_zyb { get; set; }
        /// <summary>
        /// 职业病日期
        /// </summary>
        /// <value></value>
        public string? confirmdate_zyb { get; set; }
        public string? confirmdate_yzjsza { get; set; }
        public string? QTRQDESC { get; set; }
        public string? confirmdate_jhb { get; set; }
        /// <summary>
        ///  { text: "子女无遗传病历史", key: "1001"}
        /// key: "1002",text: "高血压"

        /// key: "1003" text: "糖尿病"
        /// key: "1004"text: "冠心病"
        /// key: "1005",text: "慢性阻塞性肺疾病"
        /// 1006,恶性肿瘤
        /// 1007 脑卒中
        /// 1008,重性精神疾病
        /// 1009,结核病
        /// 1010,肝炎
        /// 1011,先天畸形
        /// 1099,其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetextCheckZN { get; set; }
        /// <summary>
        /// 残疾单选值
        /// </summary>
        /// <value></value>
        public string? diseasetextRedioCJ { get; set; }
        /// <summary>
        /// 家族历史  母亲 单选
        /// </summary>
        /// <value></value>
        public string? diseasetextRedioMQ { get; set; }
        /// <summary>
        /// 家族历史 兄弟姐妹 单选值 如 1002
        /// </summary>
        /// <value></value>
        public string? diseasetextRedioXDJM { get; set; }
        /// <summary>
        /// 遗传病史 
        /// { text: "无遗传病史", key: "0501"}
        /// key: "0502"
        /// text: "有遗传病史"
        /// </summary>
        /// <value></value>
        public KeyText? diseasetextRedioYCBS { get; set; }
        /// <summary>
        /// 遗传病史 文本描述
        /// </summary>
        /// <value></value>
        public string? diseasetextYCBS { get; set; }

        /// <summary>
        /// 暴 露 史	 可以逗号连接
        /// { text: "无暴露史", key: "1201"}
        /// key: "1202"text: "化学品"
        /// 
        /// key: "1203," text: "毒物"
        /// key 1204 text 射线
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_check_bl { get; set; }
        /// <summary>
        /// 家族史 父亲
        /// key: "0702"text: "高血压"
        ///  { text: "父亲无遗传病历史", key: "0701"}
        /// key: "0702",text: "高血压"

        /// key: "0703" text: "糖尿病"
        /// key: "0704"text: "冠心病"
        /// key: "0705",text: "慢性阻塞性肺疾病"
        /// 0706,恶性肿瘤
        /// 0707 脑卒中
        /// 0708,重性精神疾病
        /// 0709,结核病
        /// 0710,肝炎
        /// 0711,先天畸形
        /// 0799,其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_check_fq { get; set; }
        /// <summary>
        /// 药物过敏
        ///  { text: "无药物过敏史", key: "0101"}
        /// key: "0102,青霉素
        /// 0103"磺胺
        /// 
        /// 0104,链霉素
        /// 0109,其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_check_gm { get; set; }
        /// <summary>
        /// 既往史-疾病  可逗号连接  key: "0202,0203" text: "高血压,糖尿病"
        /// {  key: "0201",text: "无疾病史"}
        ///  key: "0202" text: "高血压"
        /// key :"0203"  text:"糖尿病"
        /// { "text": "冠心病", "key": "0204"}
        ///0205 慢性阻塞性肺疾病
        ///0206 恶性肿瘤
        /// 0207 脑卒
        /// 0208 重性精神疾病
        /// 0209 结核病
        /// 0210 肝脏疾病
        /// 0212 职业病
        /// 0298 其他法定传染病
        ///
        /// 0299 其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_check_jb { get; set; }
        /// <summary>
        /// 暴露史描述文本
        /// </summary>
        /// <value></value>
        public string? diseasetext_radio_bl { get; set; }
        /// <summary>
        /// 过敏  选中的值
        /// 01 无
        /// </summary>
        /// <value></value>
        public string? diseasetext_radio_gm { get; set; }
        /// <summary>
        /// 既往史-疾病  可逗号连接  key: "0202,0203" text: "高血压,糖尿病"
        /// {  key: "0201",text: "无疾病史"}
        ///  key: "0202" text: "高血压"
        /// key :"0203"  text:"糖尿病"
        /// { "text": "冠心病", "key": "0204"}
        ///0205 慢性阻塞性肺疾病
        ///0206 恶性肿瘤
        /// 0207 脑卒
        /// 0208 重性精神疾病
        /// 0209 结核病
        /// 0210 肝脏疾病
        /// 0212 职业病
        /// 0298 其他法定传染病
        ///
        /// 0299 其他
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_radio_jb { get; set; }
        /// <summary>
        ///既往史-疾病-冠心病 日期 如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_gxb { get; set; }
        /// <summary>
        ///既往史-疾病-恶性肿瘤 日期 如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_exzl { get; set; }
        /// <summary>
        ///既往史-疾病-糖尿病 日期 如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_tnb { get; set; }
        /// <summary>
        ///既往史-疾病-高血压 日期 如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_gxy { get; set; }
        /// <summary>
        ////既往史-疾病-慢性阻塞性肺疾病 日期  如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_mxzsxfjb { get; set; }
        /// <summary>
        ////既往史-疾病-脑卒中 日期  如2023-02-01
        /// </summary>
        /// <value></value>
        public string? confirmdate_nzz { get; set; }
        /// <summary>
        /// 其他法定传染病 日期
        /// </summary>
        /// <value></value>
        public string? confirmdate_qtfdcrb { get; set; }
        /// <summary>
        /// 既往史-疾病-其他 日期
        /// </summary>
        /// <value></value>
        public string? confirmdate_qt { get; set; }
        /// <summary>
        /// 其他疾病描述
        /// </summary>
        /// <value></value>
        public string? diseasetext_qt { get; set; }
        /// <summary>
        /// 
        /// 单选家族史 父亲
        ///  key: "0702"text: "高血压"
        ///  { text: "父亲无遗传病历史", key: "0701"}
        /// key: "0702",text: "高血压"
        /// key: "0703" text: "糖尿病"
        /// key: "0704"text: "冠心病"
        /// key: "0705",text: "慢性阻塞性肺疾病"
        /// 0706,恶性肿瘤
        /// 0707 脑卒中
        /// 0708,重性精神疾病
        /// 0709,结核病
        /// 0710,肝炎
        /// 0711,先天畸形
        /// 0799,其他
        /// </summary>
        /// <value></value>
        public string? diseasetext_redio_fq { get; set; }
        /// <summary>
        /// 恶性肿瘤描述文本
        /// </summary>
        /// <value></value>
        public string? diseasetext_exzl { get; set; }

        ///<summary>
        /// 既往史 手术
        /// { text: "无手术史", key: "0301"}
        /// key: "0302"  有手术史1
        ///  key 0303  text: "有手术史2"
        ///</summary>
        /// <value></value>
        public KeyText? diseasetext_ss { get; set; }
        /// <summary>
        ///既往史 手术史1 文本描述 若无则为"无手术史"
        /// </summary>
        /// <value></value>
        public string? diseasetext_ss0 { get; set; }
        /// <summary>
        /// 既往史 输血,可逗号连接
        /// {key: "0401", text: "无输血史"}
        /// key: "0402"text: "有输血史1"
        /// key 0402 text 有输血史2
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_sx { get; set; }
        /// <summary>
        /// 既往史输血 文本描述
        /// </summary>
        /// <value></value>
        public string? diseasetext_sx0 { get; set; }
        /// <summary>
        /// 第二次输血原因
        /// </summary>
        /// <value></value>
        public string? diseasetext_sx1 { get; set; }
        /// <summary>
        /// 既往史 外伤 可逗号连接 key: "0602,0602" text: "有外伤史,有外伤史"
        /// { key: "0601", text: "无外伤史",}
        /// key: "0602" text: "有外伤史"
        /// </summary>
        /// <value></value>
        public KeyText? diseasetext_ws { get; set; }
        /// <summary>
        /// 既往史 外伤描述文本
        /// 如无外伤
        /// </summary>
        /// <value></value>
        public string? diseasetext_ws0 { get; set; }
        /// <summary>

        public string? email { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        /// <value></value>
        public string? empiId { get; set; }
        /// <summary>
        /// 户籍住址
        /// </summary>
        /// <value></value>
        public string? homePlace { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        /// <value></value>
        public string? idCard { get; set; }
        
        /// <summary>
        /// 医疗费用支付方式
        /// key: "01" text: 城镇职工基本医疗保险 
        /// key  "02" text:城镇(城乡)居民基本医疗保险
        /// key 03 text:新型农村合作医疗 
        /// key 04 贫困救助
        /// key 05 商业医疗保险
        /// key 06 全公费
        /// key 07 全自费
        /// key: "99" text 其他
        /// </summary>
        /// <value></value>
        public KeyText? insuranceCode { get; set; }
        // insuranceCode1: null
        // insuranceText: null
        /// <summary>
        /// 医疗费用支付方式 其他用途的值
        /// insuranceType
        /// </summary>
        /// <value></value>
        public string? insuranceType { get; set; }
        // isAgrRegister: null
        // lastModifyTime: null
        // lastModifyUnit: null
        // lastModifyUser: null
        /// <summary>
        /// 责任医生 如 { text: "魏晓东", key: "605100"} 是账号
        /// </summary>
        /// <value></value>
        public KeyText? manaDoctorId { get; set; }

        /// <summary>
        /// 管辖社区
        /// </summary>
        /// <value></value>
        public KeyText? manaUnitId { get; set; }
        /// <summary>
        /// 婚姻状况
        ///  { text: "未婚", key: "10"}
        /// key: "20"text: "已婚"
        /// key: "30"text: "丧偶"
        /// key: "40"text: "离婚"
        /// { text: "未说明的婚姻状况", key: "90"}
        /// </summary>
        /// <value></value>
        public KeyText? maritalStatusCode { get; set; }
        /// <summary>
        /// masterFlag: {text: "否", key: "n"}
        /// </summary>
        /// <value></value>
        public KeyText? masterFlag { get; set; }
        public string? middleId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        /// <value></value>
        public string? mobileNumber { get; set; }
        /// <summary>
        /// 民族 { text: "汉族", key: "01"}
        /// </summary>
        /// <value></value>
        public KeyText? nationCode { get; set; }
        public string? nationalityCode { get; set; }
        // perage: 353
        /// <summary>
        ///姓名
        /// </summary>
        /// <value></value>
        public string? personName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <value></value>
        public int? peryage { get; set; }
        /// <summary>
        /// 优先使用mobileNumber
        /// </summary>
        /// <value></value>
        public string? phoneNumber { get; set; }
        public string? photo { get; set; }
        public string? phrId { get; set; }
        /// <summary>
        /// 地区代码,网络地址
        ///  { text: "东南村", key: "445202006213"}
        /// </summary>
        /// <value></value>
        public KeyText? regionCode { get; set; }
        public string? regionCode_text { get; set; }
        /// <summary>
        ///  { text: "户籍", key: "1"}
        /// {text:"非户籍",key:"2"}
        /// </summary>
        /// <value></value>
        public KeyText? registeredPermanent { get; set; }
        /// <summary>
        /// rh
        /// { text: "
        /// 阳性", key: "1"}
        /// { text: "阴性", key: "2"}
        /// { text: "不详", key: "3"}
        /// </summary>
        /// <value></value>
        public KeyText? rhBloodCode { get; set; }
        /// <summary>
        /// 性别
        ///  { text: "男", key: "1"}
        /// key: "0"text: "未知的性别"
        /// </summary>
        /// <value></value>
        public KeyText? sexCode { get; set; }
        /// <summary>
        /// 人群分类
        /// 04 0-6岁儿童 
        /// 09 残疾人群
        /// 10 计生特殊家庭
        /// 11 非低保五保户
        /// 12 五保户
        /// 13 非低保五保户
        /// 99其他
        /// </summary>
        /// <value></value>
        public KeyText? personGroup { get; set; }

        /// <summary>
        /// 厨房排风设施
        /// { text: "无", key: "1"}
        /// key: "2"text: "油烟机
        /// key: "3"text: "换气扇"
        ///key: "4"text: "烟囱"
        /// 
        /// </summary>
        /// <value></value>
        public KeyText? shhjCheckCFPFSS { get; set; }
        /// <summary>
        /// { text: "卫生厕所", key: "1"}
        /// key: "2"text: "一格或二格粪池式"
        /// key: "3"text: "马桶"
        //// key: "4" 露天粪坑
        /// key: "5"text: "简易棚厕"
        ///  key: "9" {text: "其他",}
        /// </summary>
        /// <value></value>
        public KeyText? shhjCheckCS { get; set; }

        /// <summary>
        /// 
        /// 禽畜栏
        /// { text: "无", key: "0"}
        /// key: "1"text: "单设"
        /// 
        /// key: "3"text: "室内"
        /// 4 text 室外
        /// </summary>
        /// <value></value>
        public KeyText? shhjCheckQCL { get; set; }
        /// <summary>
        /// 燃料类型	
        ///  { text: "液化气", key: "1"}
        /// key: "2" text: "煤"
        /// 3  天然气
        /// 4 沼气
        /// key: "5"text: "柴火"
        /// key: "9""其他"
        /// 
        /// 
        /// </summary>
        /// <value></value>
        public KeyText? shhjCheckRLLX { get; set; }
        /// <summary>
        /// 饮水
        ///  { text: "自来水", key: "1"}
        ///key: "2"text: "经净化过滤的水"
        /// key: "3"text: "井水"
        /// key: "4"text: "河湖水"
        /// key: "5" text: "塘水"
        /// key: "9" text: "其他"
        /// </summary>
        /// <value></value>
        public KeyText? shhjCheckYS { get; set; }
        /// <summary>
        /// 签约标致
        ///  "n" 否
        /// y "是"
        /// </summary>
        /// <value></value>
        public KeyText? signFlag { get; set; }
        /// <summary>
        /// 手术1 开始日期 如 2023-05-09
        /// </summary>
        /// <value></value>
        public string? startdate_ss0 { get; set; }
        /// <summary>
        /// 手术2 开始日期 
        /// </summary>
        /// <value></value>
        public string? startdate_ss1 { get; set; }
        /// <summary>
        /// 输血0 起始日期 如 2023-05-23
        /// </summary>
        /// <value></value>
        public string? startdate_sx0 { get; set; }
        /// <summary>
        /// 输血1
        /// </summary>
        /// <value></value>
        public string? startdate_sx1 { get; set; }
        /// <summary>
        /// 输血1 开始日期 
        /// </summary>
        /// <value></value>
        public string? startdate_ws0 { get; set; }
        /// 
        /// 状态 0
        public string? status { get; set; }
        /// <summary>
        /// 版本号 不需要关心
        /// </summary>
        /// <value></value>
        public object? versionNumber { get; set; }
        /// <summary>
        /// text: "国家机关、党群组织、企业、事业单位负责人", key: "0
        /// 1专业技术人员 
        /// 2 办事人员和有关人员 
        /// 3商业、服务业人员
        ///4农、林、牧、渔、水利业生产人员 
        /// 5生产、运输设备操作人员及有关人员 
        /// 6军人 
        /// 7不便分类的其他从业人
        /// 8 无职业
        ///  </summary>
        /// <value></value>
        public KeyText? workCode { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        /// <value></value>
        public string? workPlace { get; set; }
        public string? zipCode { get; set; }
    }

    public class LoadBasicPersonalInformationOutput
    {
        public int code { get; set; }
        public string selectMhcDoctorId { get; set; }
        public LoadBasicPersonalInformationResult body { get; set; }

    }
    /// <summary>
    /// 批量更新患者信息
    /// </summary>
    public class BatSaveBasicPersonalInfomationInput
    {
        public List<SaveBasicPersonalInformationInputBody> items { get; set; } = new List<SaveBasicPersonalInformationInputBody>();
    }

    public class SearchEhrByIdCardInput
    {
        public string serviceId { get; set; } = "chis.listSqlQuery";
        public string method { get; set; } = "execute";
        public string schema { get; set; } = "chis.application.hr.schemas.EHR_HealthRecordAndMPIList";
        public object cnd { get; set; } = new object[] { "and",
        new object[] { "and",  new  object[]{"like", new  []{"$", "b.idCard"},new []{"s", "421182199311130057%"}} } };
        public int pageSize { get; set; } = 50;
        public int pageNo { get; set; } = 1;
        public string serviceAction { get; set; } = String.Empty;

        public void setIdCard(string idCard)
        {
            cnd = new object[] { "and",
        new object[] { "and",  new  object[]{"like", new  []{"$", "b.idCard"},new []{"s", idCard+"%"}} } };
        }

    }
    public class SearchEhrByIdCardResult
    {

        public string? msg { get; set; } = "Success";
        public int? code { get; set; } = 200;
        public bool ok { get; set; } = true;
        public int? pageNo { get; set; } = 1;
        public int? pageSize { get; set; } = 50;
        public int? totalCount { get; set; } = 1;
        public List<SearchEhrByIdCardResultItem>? body { get; set; } = new List<SearchEhrByIdCardResultItem>();
    }

    public class SearchEhrByIdCardResultItem
    {
        public string? manaUnitId_text { get; set; }
        public string? fatherName { get; set; }
        public string? homePlace { get; set; }
        public string? idCard { get; set; }
        public string? mobileNumber { get; set; }
        public string? isHypertension_text { get; set; }
        public string? manaDoctorId_text { get; set; }
        public string? inputUser_text { get; set; }
        public string? empiId { get; set; }
        public string? recordSource_text { get; set; }
        public string? signFlag_text { get; set; }
        public string? phrId { get; set; }


    }
}