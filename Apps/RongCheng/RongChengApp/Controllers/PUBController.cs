using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
using RongChengApp.Services;
namespace RongChengApp.Controllers
{
    /// <summary>
    /// 糖尿病业务
    /// </summary>
    [ApiController]
    [Route("/api/PUB")]
    public class PUBController
    {
        private readonly UtilService utilService;
        public PUBController(UtilService _utilService) { utilService = _utilService; }

        /// <summary>
        /// 获取全年的糖尿病随访记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<PUBGetCurYearVisitPlanResult> getCurYearVisitPlan([FromBody] PUBGetCurYearVisitPlanInput input)
        {
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
            var data = await rtn.Content.ReadFromJsonAsync<PUBGetCurYearVisitPlanResult>();
            Console.WriteLine(data);
            return data;
        }
        /// <summary>
        /// 获取随访记录详情
        ///  </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<DiabetesVisitServiceResult> diabetesVisitService([FromBody] DiabetesVisitServiceInput input)
        {
            var httpclient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpclient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
            var data = await rtn.Content.ReadFromJsonAsync<DiabetesVisitServiceResult>();
            return data;

        }

        /// <summary>
        /// 保存糖尿病随访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<object> saveDiabetesVisit([FromBody] SaveDiabetesVisitInput input)
        {
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest?d=" + utilService.autoLoginService.d, input);
            return await rtn.Content.ReadAsStringAsync();


        }
        /// <summary>
        /// 糖尿病档案查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<PUBDiabetesRecordServiceResult> pUBDiabetesRecordServiceDetail([FromBody] PUBDiabetesRecordServiceInput input)
        {
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl, input);
            return await rtn.Content.ReadFromJsonAsync<PUBDiabetesRecordServiceResult>();


        }
        /// <summary>
        /// 更新糖尿病档案查询
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<PUBSaveDiabetesRecordResult> saveDiabetesRecord([FromBody] PUBSaveDiabetesRecordInput input)
        {
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl, input);
            return await rtn.Content.ReadFromJsonAsync<PUBSaveDiabetesRecordResult>();
        }

    }



}