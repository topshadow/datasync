using Microsoft.Extensions.Primitives;

using RongChengApp.Dtos;

namespace RongChengApp.Services
{
    /// <summary>
    /// 动态账号服务 scope
    /// </summary>
    public class AccountService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly AutoLoginService autoLoginService;
        private readonly IHttpClientFactory httpClientFactory;
        public AccountService(IHttpContextAccessor _contextAccessor, AutoLoginService autoLoginService, IHttpClientFactory _httpClientFactory)
        {
            this.contextAccessor = _contextAccessor;
            this.autoLoginService = autoLoginService;
            this.httpClientFactory = _httpClientFactory;
        }

        /// <summary>
        /// 根据账号名获取账号的登录cookie
        /// 默认根据当前请求头 `account` 的作为登录账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string getCookieByAccount()
        {
            contextAccessor.HttpContext.Request.Headers.TryGetValue("account", out StringValues account);
            var accountStr = account.ToString();
            if(!autoLoginService.accountCookie.ContainsKey(accountStr))
            {
                throw new Exception($"当前账号:{accountStr}不存在,或者尚未登录");
            }
            return autoLoginService.accountCookie[accountStr];
        }

        public async Task<List<AccountPassword>> loadAccounts()
        {
            var httpClient = httpClientFactory.CreateClient();
            var serverUrl = "http://ygt.xenpie.com:9502";
            //var serverUrl = "http://10.1.7.107:9502";
            var data = await httpClient.PostAsJsonAsync(serverUrl+ "/hospital/multiple/public/get_mc_doctor", new
            {
                page = 1,
                size = 500,
                type = 2,
                org_id = "306582797094981",
                token_key = "R58cAUtL3xiDdNXSMQ6j"
            }
                 );
            var text = await data.Content.ReadAsStringAsync();
            Console.Write(text);
            var result = await data.Content.ReadFromJsonAsync<LoadAccountResult>();
            return result.data.data.Select(r => new AccountPassword { account = r.note1, password = r.note2 }).ToList();
        }


    }
}
