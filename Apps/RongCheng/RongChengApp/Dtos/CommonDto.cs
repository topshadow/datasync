namespace RongChengApp.Dtos;


public class CommonDto
{

    public string? msg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public bool ok { get; set; } = true;
}

/// <summary>
/// 搜索省市区街道结果
/// </summary>
public class SearchRegionResult
{
    /// <summary>
    /// 成功Success
    /// </summary>
    /// <value></value>
    public string? msg { get; set; }
    public int code { get; set; }
    public int? pageNo { get; set; } = 1;
    public int? totalCount { get; set; } = 31;
    public List<SearchRegionResultBodyItem> body { get; set; } = new List<SearchRegionResultBodyItem>();

}
public class SearchRegionResultBodyItem
{
    public string regionCode { get; set; } = "11";
    public string parentCode { get; set; } = "0";
    public string regionName { get; set; } = "北京市";
    public string isFamily { get; set; } = "b";
    public string isFamily_text { get; set; } = "";
    public string pyCode { get; set; } = "BJS";
    public string isBottom { get; set; } = "n";
    public string isBottom_text { get; set; } = "否";
    public List<SearchRegionResultBodyItem> body { get; set; } = new List<SearchRegionResultBodyItem>();


}