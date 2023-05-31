namespace RongChengApp.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadAccountResult
    {
        public int status { get; set; } = 200;
        public string message { get; set; } = "成功";
        public LoadAccountResultData data { get; set; }
        public int current_page { get; set; } = 1;
    }
    public class LoadAccountResultData
    {
        public List<LoadAccountResultDataItem> data { get; set; }
    }
    public class LoadAccountResultDataItem
    {
        public int id { get; set; } = 1;
        public string note1 { get; set; }
        public string note2 { get; set; }
    }
}
