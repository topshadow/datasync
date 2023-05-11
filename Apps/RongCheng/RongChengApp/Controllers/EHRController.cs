using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
using RongChengApp.Services;
namespace RongChengApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EHRController
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AutoLoginService autoLoginService;
        public EHRController(IHttpClientFactory _httpClientFactory, AutoLoginService _autoLoginService)
        {
            httpClientFactory = _httpClientFactory;
            autoLoginService = _autoLoginService;
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
            //["and",["eq",["$","a.status"],["s","0"]],["eq",["$","a.inputDate"],["todate",["s","2023-05-01"],["s","yyyy-mm-dd"]]]]

            // Console.WriteLine(input.start.ToString("yyyy-MM-dd"));
            var body = new EHRHealthRecordInput
            {
                pageNo = input.pageNo,
                pageSize = input.pageSize,
                cnd = new List<object>{
                "or",
            //   new List<object>{"and",
            //   new object[]{"eq", new string[] {"$", "a.review"}, new string []{"s", "0"}},
            //   new List<object>{"eq",new []{"$","a.status"},new []{"s","0"} },
    //  new object[]{"like",new object[]{"$","a.manaUnitId"},new string[]{"s", "445202605"}  },
            //   },
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
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("Host", "ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");

            var rtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", input);
            return await rtn.Content.ReadAsStringAsync();
        }
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

        [HttpPost("[action]")]
        public async Task<BatSearchPersonInfoResult> batSearchPersonInfo([FromBody] BatSearchPersonInfoInput input)
        {
            var httpClient = httpClientFactory.CreateClient();

            var result = new BatSearchPersonInfoResult();
            var taskList = new List<Task<LoadBasicPersonalInformationOutput>>();
            foreach (var item in input.empiIds.Split(","))
            {
                var task = LoadBasicPersonalInformation(new LoadBasicPersonalInformationInput { body = new LoadBasicPersonalInformationInputBody { empiId = item } }).ContinueWith(r =>
                {
                    result.body.Add(r.Result.body);
                    return r.Result;
                });
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());
            return result;
        }
    }
}