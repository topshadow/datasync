using System.ComponentModel;
namespace RongChengApp.Dtos
{





    public class SaveBasicPersonalInformationInput
    {
        public string? serviceId { get; set; } = "chis.basicPersonalInformationService";
        public string serviceAction { get; set; } = "saveBasicPersonalInformation";
        public string op { get; set; } = "update";
        public SaveBasicPersonalInformationInputBody body { get; set; }

        public string method { get; set; } = "execute";



    }
    public class SaveBasicPersonalInformationInputBody
    {
        /// <summary>
        /// 高血压同步信息
        /// </summary>
        /// <value></value>
        public SaveHypertensionRecordInputBody? hypertensionResult { get; set; }
        /// <summary>
        /// 糖尿病档案同步信息
        /// </summary>
        /// <value></value>
        public PUBSaveDiabetesRecordInputBody? pubRecordInfo { get; set; }

        public string? phrId { get; set; }

        public string? empiId { get; set; }
        public string? middleId { get; set; }
        public string? cardNo { get; set; }
        public string? personName { get; set; }
        public string? sexCode { get; set; } = "0";
        public DateTime? birthday { get; set; }
        [DefaultValue("421182199311130058")]
        public string? idCard { get; set; }
        public string? workPlace { get; set; }
        public string? mobileNumber { get; set; }
        public string? contact { get; set; }
        public string? contactPhone { get; set; }
        public string? registeredPermanent { get; set; }
        public string? homePlace { get; set; }
        public string? homePlaceNumber { get; set; }
        /// <summary>
        /// 例如= "广东省揭阳市大南海华侨管理区溪西镇永安村委会";
        /// </summary>
        /// <value></value>
        public string? address { get; set; }
        public string? adressNumber { get; set; }
        public string? signFlag { get; set; }
        public string? recordSource { get; set; }
        public string? nationCode { get; set; }
        public string? bloodTypeCode { get; set; }
        public string? rhBloodCode { get; set; }
        public string? educationCode { get; set; }
        public string? workCode { get; set; }
        public string? maritalStatusCode { get; set; }
        public string? personGroup { get; set; }
        public string? insuranceCode { get; set; }
        public string? insuranceType { get; set; }
        public string? masterFlag { get; set; }
        public string? familyId { get; set; }
        public string? diseasetext_check_gm { get; set; }
        public string? a_qt1 { get; set; }
        public string? diseasetext_check_bl { get; set; }
        public string? diseasetext_radio_jb { get; set; }
        public string? diseasetext_check_jb { get; set; }
        public string? confirmdate_gxy { get; set; }
        public string? confirmdate_gxb { get; set; }
        public string? confirmdate_exzl { get; set; }
        public string? confirmdate_zxjsjb { get; set; }
        public string? confirmdate_gzjb { get; set; }
        public string? confirmdate_zyb { get; set; }
        public string? confirmdate_px { get; set; }
        public string? confirmdate_qt { get; set; }
        public string? confirmdate_tnb { get; set; }
        public string? confirmdate_mxzsxfjb { get; set; }
        public string? confirmdate_nzz { get; set; }

        public string? confirmdate_jhb { get; set; }
        public string? confirmdate_xtjx { get; set; }
        public string? confirmdate_szjb { get; set; }
        public string? confirmdate_qtfdcrb { get; set; }
        public string? diseasetext_zyb { get; set; }
        public string? diseasetext_exzl { get; set; } = "exzlms";
        public string? diseasetext_qt { get; set; } = "jbqt";
        public string? diseasetext_ss { get; set; } = "0302";
        public string? diseasetext_ss0 { get; set; } = "a手术";
        public string? startdate_ss0 { get; set; }
        public string? diseasetext_ss1 { get; set; } = "b手术";
        public string? startdate_ss1 { get; set; }
        public string? diseasetext_ws { get; set; } = "0601";
        public string? diseasetext_ws0 { get; set; } = "";
        public string? startdate_ws0 { get; set; }
        public string? diseasetext_ws1 { get; set; } = "";
        public string? startdate_ws1 { get; set; } = "";
        public string? diseasetext_sx { get; set; } = "0402";
        public string? diseasetext_sx0 { get; set; } = "sx0";
        public string? startdate_sx0 { get; set; }
        public string? diseasetext_sx1 { get; set; } = "sx1";
        public string? startdate_sx1 { get; set; }
        public string? diseasetext_check_fq { get; set; } = "0701";
        public string? qt_fq1 { get; set; }
        public string? diseasetextCheckMQ { get; set; } = "0801";
        public string? qt_mq1 { get; set; }
        public string? diseasetextCheckXDJM { get; set; } = "0901";
        public string? qt_xdjm1 { get; set; }
        public string? diseasetextCheckZN { get; set; }
        public string? qt_zn1 { get; set; }
        public string? diseasetextRedioYCBS { get; set; } = "0501";
        public string? diseasetextYCBS { get; set; }
        public string? diseasetextCheckCJ { get; set; } = "1102,1103,1104,1105,1106,1107,1199";
        public string? cjqk_qtcj1 { get; set; } = "qtcj";
        public string? isFillShhj { get; set; } = "y";
        public string? shhjCheckCFPFSS { get; set; } = "1";
        public string? shhjCheckRLLX { get; set; } = "9";
        public string? shhjCheckYS { get; set; }
        public string? shhjCheckCS { get; set; } = "1";
        public string? shhjCheckQCL { get; set; }
        public string? deadFlag { get; set; }
        public string? deadDate { get; set; }
        public string? deadReason { get; set; }
        public string? regionCode { get; set; } = "445224109211";
        public string? manaDoctorId { get; set; } = "605100";
        public string? manaUnitId { get; set; }
        /// <summary>
        /// {
        ///        "text":"榕城区榕东社区卫生服务中心","k5202605"
        /// </summary>
        /// <value></value>
        public KeyText? createUnit { get; set; } = new KeyText { text = "榕城区榕东社区卫生服务中心", key = "k5202605" };

        public string createUser { get; set; } = "605100";
        /// <summary>
        /// 日期格式2023-05-16 10:00:00 转为2023-05-16T10:00:00
        /// </summary>
        /// <value></value>
        public string? createDate { get; set; }
        public string? addressCode { get; set; } = null;
        public string? homePlaceCode { get; set; } = null;
        public string? QTRQDESC { get; set; }
    }


    public class LoadBasicPersonalInformationInput
    {
        public string serviceId { get; set; } = "chis.basicPersonalInformationService";
        public string serviceAction { get; set; } = "LoadBasicPersonalInformation";
        public LoadBasicPersonalInformationInputBody body { get; set; }
        public string op { get; set; } = "update";
        public string method { get; set; } = "execute";
    }
    public class LoadBasicPersonalInformationInputBody
    {
        public string? empiId { get; set; }
        public string regionCode { get; set; } = string.Empty;

    }

    public class BatSearchPersonInfoInput
    {
        /// <summary>
        /// 批量员工字符串id 逗号连接
        /// </summary>
        /// <value></value>
        public string empiIds { get; set; } = String.Empty;
    }
    /// <summary>
    /// 批量搜索患者信息
    /// </summary>
    public class BatSearchPersonInfoResult
    {

        public List<LoadBasicPersonalInformationResult> body { get; set; } = new List<LoadBasicPersonalInformationResult>();
    }



}