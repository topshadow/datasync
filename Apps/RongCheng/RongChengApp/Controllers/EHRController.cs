using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
using RongChengApp.Services;
using Mapster;
namespace RongChengApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EHRController
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AutoLoginService autoLoginService;
        private readonly UtilService utilService;
        private readonly PubService pubService;
        private readonly HypertensionService hypertensionService;
        public EHRController(IHttpClientFactory _httpClientFactory,
        AutoLoginService _autoLoginService,
         UtilService _utilService,
        PubService _pubService,
        HypertensionService _hypertensionService
        )
        {
            httpClientFactory = _httpClientFactory;
            autoLoginService = _autoLoginService;
            utilService = _utilService;
            pubService = _pubService;
            hypertensionService = _hypertensionService;
        }

        /// <summary>
        /// 查询患者信息档案
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<EHRHealthRecordResult> eHR_HealthRecord([FromBody] EHRHealthRecordInput body)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("cookie", autoLoginService.Cookie);
            var rtn = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest?start={(body.pageNo - 1) * body.pageSize}&limit={body.pageSize}", body);

            var data = await rtn.Content.ReadFromJsonAsync<EHRHealthRecordResult>();

            return data;

        }


        /// <summary>
        /// 时间段查询新增或修改的患者
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<EHRHealthRecordResult> eHR_HealthRecordQueryByTimespan([FromBody] EHRHealthRecordQueryByTimespanInput input)
        {

            var body = new EHRHealthRecordInput
            {
                pageNo = input.pageNo,
                pageSize = input.pageSize,
                cnd = new List<object>{
                "or",

             new object[]{"and",new object[]{"gt",new []{"$","to_char(b.createTime,'yyyy-MM-dd')"},new []{"s",input.start} },new object[]{"lt",new []{"$","to_char(b.createTime,'yyyy-MM-dd')"},new []{"s",input.end } }},
             new object[]{"and",new object[]{"gt",new []{"$","to_char(b.lastModifyTime,'yyyy-MM-dd')"},new []{"s",input.start} },new object[]{"lt",new []{"$","to_char(b.lastModifyTime,'yyyy-MM-dd')"},new []{"s",input.end}}
              }
            }
            };
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("Host", "ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");

            var rtn = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest?start{(input.pageNo - 1) * input.pageSize}&limit={input.pageSize}", body);



            var text = await rtn.Content.ReadAsStringAsync();
            Console.WriteLine(text); ;



            var data = await rtn.Content.ReadFromJsonAsync<EHRHealthRecordResult>();

            return data;

        }
        /// <summary>
        /// 更新患者信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<string> updateEhr([FromBody] SaveBasicPersonalInformationInput input)
        {
            // input.body.createDate=DateTime.Now
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("Host", "ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(input));
            var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
            var str = await rtn.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            return str;
        }
        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<LoadBasicPersonalInformationOutput> LoadBasicPersonalInformation([FromBody] LoadBasicPersonalInformationInput input)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("Host", "ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");

            var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
            return await rtn.Content.ReadFromJsonAsync<LoadBasicPersonalInformationOutput>();

        }
        /// <summary>
        /// 批量搜索患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<BatSearchPersonInfoResult> batSearchPersonInfo([FromBody] BatSearchPersonInfoInput input)
        {
            var httpClient = httpClientFactory.CreateClient();

            var result = new BatSearchPersonInfoResult();


            // var taskList = new List<Task<LoadBasicPersonalInformationOutput>>();
            foreach (var item in input.empiIds.Split(","))
            {
                var data = new SearchPersonAllInfoResult { };
                var rtn = await LoadBasicPersonalInformation(new LoadBasicPersonalInformationInput { body = new LoadBasicPersonalInformationInputBody { empiId = item } });
                data.personInfo = rtn.body;
                var pubRecord = await pubService.detail(new PUBDiabetesRecordServiceInput { body = new PUBDiabetesRecordServiceInputBody { empiId = item } });
                data.pubRecordInfo = pubRecord;
                var hypertensionRecord = await hypertensionService.recordDetail(new InitializeRecordInput { body = new InitializeRecordInputBody { empiId = item } });
                data.hypertensionResult = hypertensionRecord;
                result.body.Add(data);
            }
            return result;
        }
        /// <summary>
        /// 批量更新患者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<object> batUpdateEhrs([FromBody] BatSaveBasicPersonalInfomationInput input)
        {
            var taskList = new List<Task>();
            var result = new List<object>();
            foreach (var item in input.items)
            {
                var r = await getphrIdByIdCard(item.idCard);

                if (r.body.Count > 0)
                {
                    if (!String.IsNullOrEmpty(item.createDate))
                    {
                        if (!item.createDate.Contains("T"))
                        {
                            item.createDate = DateTime.Parse(item.createDate).ToString("yyyy-MM-ddTHH:mm:ss");
                        }
                    }
                    item.phrId = r.body[0].phrId;
                    item.empiId = r.body[0].empiId;
                    item.cardNo = r.body[0].idCard;
                    item.idCard = r.body[0].idCard;
                    var r2 = await this.updateEhr(new SaveBasicPersonalInformationInput { body = item });
                    result.Add(new { ok = true, phrId = item.phrId, idCard = item.idCard, empiId = item.empiId });
                }
                else
                {

                    result.Add(new { ok = false, msg = "尚未注册", idCard = item.cardNo });
                }
            }


            return result;

        }
        /// <summary>
        /// 根据身份证号查旬患者phrid
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<SearchEhrByIdCardResult> getphrIdByIdCard(string idCard)
        {
            var search = new SearchEhrByIdCardInput();
            search.setIdCard(idCard);
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl + "?start=0&limit=50", search);
            return await rtn.Content.ReadFromJsonAsync<SearchEhrByIdCardResult>();


        }
        /// <summary>
        /// 测试异常处理
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<object> test()
        {
            throw new Exception("测试异常处理");
        }
    }
}