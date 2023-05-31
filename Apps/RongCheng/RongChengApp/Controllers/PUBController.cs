using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
using RongChengApp.Services;
using System.Text.Json;

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
        private readonly PubService pubService;
        private readonly EHRService eHRService;
        public PUBController(UtilService _utilService, PubService _pubService, EHRService _eHRService) { utilService = _utilService; pubService = _pubService; eHRService = _eHRService; }

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
        /// <summary>
        /// 自动推送同步糖尿病随访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<object> autoSaveDiabetesRecord([FromBody] SaveDiabetesVisitInputBody input)
        {

            
            var query = new PUBVisitListQuery
            {
                cnd = new List<object> {
            "and",
                new List<object> { "eq", new string[] { "$", "a.businessType" }, new string[] { "s", "2" } },
             new object[]   {"like", new object[] { "$", "b.idCard" }, new object[]{ "s", "%"+input.idCard+"%" } } }
            };

            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var data = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl + "?start=0&limit=50", query);
            var r = await data.Content.ReadAsStringAsync();
            var text = await data.Content.ReadFromJsonAsync<PUBVisitListQueryResult>();
            var exsitPlan = text.body.Where(t => !string.IsNullOrEmpty(t.planDate))
                 .Where(t => t.planDate.Substring(0, 10) == input.visitDate).FirstOrDefault();
            // 同步数据
            if (exsitPlan == null)
            {
                await this.createPUBVisitPlan(input.idCard, input.planDate);
                data = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl + "?start=0&limit=50", query);
                text = await data.Content.ReadFromJsonAsync<PUBVisitListQueryResult>();
                exsitPlan = text.body.Where(t => t.planDate.Substring(0, 10) == input.planDate.Substring(0, 10)).FirstOrDefault();
            }
            var person = await eHRService.getphrIdByIdCard(input.idCard);
            input.empiId = exsitPlan.empiId;
            input.planId = exsitPlan.planId;
            input.phrId = person.body[0].phrId;
            input.visitId=exsitPlan.visitId;
            input.visitDate = input.planDate.Substring(0,10);
            var body = new SaveDiabetesVisitInput { body = input };
            Console.WriteLine(JsonSerializer.Serialize(body));

            var result = await this.saveDiabetesVisit(body);


            return result;

        }
        [HttpPost("[action]")]
        public async Task<SearchEhrByIdCardResult> createPUBVisitPlan(string idCard, string dateField)
        {
            var person = await eHRService.getphrIdByIdCard(idCard);
            if (person == null)
            {
                return new SearchEhrByIdCardResult() { ok=false,msg="用户尚未同步信息"};
            }

            var data = new
            {
                serviceId = "chis.diabetesVisitService",
                serviceAction = "saveRepairPlan",
                method = "execute",
                schema = "chis.application.pub.schemas.PUB_VisitPlan",
                body = new
                {
                    phrld = person.body[0].phrId,
                    empiid = person.body[0].empiId,
                    dateField = DateTime.Parse(dateField).ToString("yyyy-MM-dd 00:00:00"),
                    beginDate = "2023-05-15T00:00:00",
                    endDate = "2023-05-29T00:00:00"
                }
            };
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl, data);
            var text = await rtn.Content.ReadAsStringAsync();
            return person;

        }

    }



}