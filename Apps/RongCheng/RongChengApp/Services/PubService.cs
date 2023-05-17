using RongChengApp.Dtos;

namespace RongChengApp.Services;
/// <summary>
/// 糖尿病服务
/// </summary>
public class PubService
{
    private readonly UtilService utilService;
    public PubService(UtilService _utilService)
    {
        utilService = _utilService;
    }
    /// <summary>
    /// 患者糖尿病档案详情
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PUBDiabetesRecordServiceResult> detail(PUBDiabetesRecordServiceInput input)
    {

        var httpClient = utilService.createDefaultRequestHeaderHttpClient();
        var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl, input);
        return await rtn.Content.ReadFromJsonAsync<PUBDiabetesRecordServiceResult>();

    }

}