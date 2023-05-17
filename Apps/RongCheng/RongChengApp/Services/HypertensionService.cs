using RongChengApp.Dtos;

namespace RongChengApp.Services;
/// <summary>
/// 糖尿病服务
/// </summary>
public class HypertensionService
{
    private readonly UtilService utilService;
    public HypertensionService(UtilService _utilService)
    {
        utilService = _utilService;
    }

    /// <summary>
    /// 糖尿病档案详情
    /// </summary>
    public async Task<InitializeRecordResult> recordDetail(InitializeRecordInput input)
    {
        var httpClient = utilService.createDefaultRequestHeaderHttpClient();
        var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
        Console.WriteLine(await rtn.Content.ReadAsStringAsync());
        return await rtn.Content.ReadFromJsonAsync<InitializeRecordResult>();

    }
}