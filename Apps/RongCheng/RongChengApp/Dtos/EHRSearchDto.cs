using System.ComponentModel;
namespace RongChengApp.Dtos
{





    public class SaveBasicPersonalInformationInput
    {
        public string? serviceId { get; set; } = "saveBasicPersonalInformation";
        public string serviceAction { get; set; } = "saveBasicPersonalInformation";
        public string op { get; set; } = "update";
        public SaveBasicPersonalInformationInputBody body { get; set; }

        public string method { get; set; } = "execute";



    }
    public class SaveBasicPersonalInformationInputBody
    {
        public string? empiId { get; set; } = "b9d8e98c2f334c57b36be9d8c6c12a87";
        public string? phrId { get; set; } = "44520311521400001";
        public string? middleId { get; set; } = "44520311521400001";
        public string? cardNo { get; set; } = "";
        public string? personName { get; set; } = "杨越2";
        public string? sexCode { get; set; } = "0";
        public DateTime? birthday { get; set; }
        [DefaultValue("421182199311130058")]
        public string? idCard { get; set; } = "421182199311130058";
        public string? workPlac { get; set; } = "a";
        public string? mobileNumber { get; set; } = "13419597065";
        public string? contact { get; set; }
        public string? contactPhone { get; set; }
        public string? registeredPermanent { get; set; }
        public string? homePlace { get; set; }
        public string? homePlaceNumber { get; set; }
        public string? address { get; set; } = "广东省揭阳市大南海华侨管理区溪西镇永安村委会";
        public string? adressNumber { get; set; }
        public string? signFlag { get; set; } = "n";
        public string? recordSource { get; set; } = "2";
        public string? nationCode { get; set; } = "02";
        public string? bloodTypeCode { get; set; }
        public string? rhBloodCode { get; set; }
        public string? educationCode { get; set; } = "31";
        public string? workCode { get; set; } = "9-9";
        public string? maritalStatusCode { get; set; } = "40";
        public string? personGroup { get; set; } = "04,09,10,11,12,13,14,99";
        public string? insuranceCode { get; set; } = "01,03,04,05,06,07,99";
        public string? insuranceType { get; set; } = "其他医疗费用";
        public string? masterFlag { get; set; } = "n";
        public string? familyId { get; set; }
        public string? diseasetext_check_gm { get; set; } = "0102,0103,0104,0109";
        public string? a_qt1 { get; set; } = "过敏文本";
        public string? diseasetext_check_bl { get; set; } = "1201";
        public string? diseasetext_radio_jb { get; set; } = "02";
        public string? diseasetext_check_jb { get; set; } = "0202,0203,0204,0205,0206,0207,0208,0209,0210,0212,0298,0299";

        public DateTime? confirmdate_gxy { get; set; }
        public DateTime? confirmdate_gxb { get; set; }
        public DateTime? confirmdate_exzl { get; set; }
        public DateTime? confirmdate_zxjsjb { get; set; }
        public DateTime? confirmdate_gzjb { get; set; }
        public DateTime? confirmdate_zyb { get; set; }
        public string? confirmdate_px { get; set; }
        public string? confirmdate_qt { get; set; }
        public DateTime? confirmdate_tnb { get; set; }
        public DateTime? confirmdate_mxzsxfjb { get; set; }
        public DateTime? confirmdate_nzz { get; set; }

        public DateTime? confirmdate_jhb { get; set; }
        public string? confirmdate_xtjx { get; set; }
        public string? confirmdate_szjb { get; set; }
        public DateTime? confirmdate_qtfdcrb { get; set; }
        public string? diseasetext_zyb { get; set; }
        public string? diseasetext_exzl { get; set; } = "exzlms";
        public string? diseasetext_qt { get; set; } = "jbqt";
        public string? diseasetext_ss { get; set; } = "0302";
        public string? diseasetext_ss0 { get; set; } = "a手术";
        public DateTime? startdate_ss0 { get; set; }
        public string? diseasetext_ss1 { get; set; } = "b手术";
        public DateTime? startdate_ss1 { get; set; }
        public string? diseasetext_ws { get; set; } = "0601";
        public string? diseasetext_ws0 { get; set; } = "";
        public string? startdate_ws0 { get; set; }
        public string? diseasetext_ws1 { get; set; } = "";
        public string? startdate_ws1 { get; set; } = "";
        public string? diseasetext_sx { get; set; } = "0402";
        public string? diseasetext_sx0 { get; set; } = "sx0";
        public DateTime? startdate_sx0 { get; set; }
        public string? diseasetext_sx1 { get; set; } = "sx1";
        public DateTime? startdate_sx1 { get; set; }
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
        public KeyText createUnit { get; set; }

        public string createUser { get; set; } = "605100";
        public DateTime? createDate { get; set; }
        public string? addressCode { get; set; } = null;
        public string? homePlaceCode { get; set; } = null;
        public string? QTRQDESC { get; set; }
        // public string? ey { get; set; }

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
        public string empiId { get; set; } = "2d0c5855195b46e0a3898d7ba9caf3e8";
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
    public class BatSearchPersonInfoResult
    {
        public List<LoadBasicPersonalInformationResult> body { get; set; } = new List<LoadBasicPersonalInformationResult>();
    }

}