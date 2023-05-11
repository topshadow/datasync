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
            return await rtn.Content.ReadFromJsonAsync<PUBGetCurYearVisitPlanResult>();

        }

    }



}