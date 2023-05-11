namespace RongChengApp.Dtos
{

    /// <summary>
    /// 查询系统医生信息账号
    /// </summary>
    public class LoadSystemDoctorInfoInput
    {
        public int type { get; set; } = 2;
        public long org_id { get; set; } = 306582797094981;

    }
    public class LoadSystemDoctorInfoResult
    {

        public int status { get; set; }
        public string message { get; set; }
        public LoadSystemDoctorInfoPage data { get; set; }


    }
    public class LoadSystemDoctorInfoPage
    {
        public int current_page { get; set; } = 1;

        public string first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; } = 1;
        public string last_page_url { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }
        public int per_page { get; set; } = 10;
        public string prev_page_url { get; set; }
        public int? to { get; set; }
        public int total { get; set; }
        public List<LoadSystemDoctorInfoPageItem> data { get; set; }
    }
    public class LoadSystemDoctorInfoPageItem
    {

        public int id { get; set; }
        public int p_id { get; set; }
        public long org_id { get; set; }
        public string from_code { get; set; }
        public string to_code { get; set; }
        public string from_name { get; set; }
        public string to_name { get; set; }
        public int type { get; set; }
        public int to_type { get; set; }
        public string note1 { get; set; }
        public string note2 { get; set; }
        public string note3 { get; set; }
        public int sort { get; set; }
        /// <summary>
        /// 同步状态
        /// </summary>
        /// <value></value>
        public SyncStatus status { get; set; } = SyncStatus.Load;
        public string? cookie { get; set; }

    }
    /// <summary>
    /// 同步状态
    /// </summary>
    public enum SyncStatus
    {
        /// 已加载
        Load,
        /// 已经同步
        Synced

    }
}