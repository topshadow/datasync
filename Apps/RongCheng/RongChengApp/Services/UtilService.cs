namespace RongChengApp.Services
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class UtilService
    {
        public readonly string remoteServerUrl = "http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest";
        private readonly IHttpClientFactory httpClientFactory;
        public readonly AutoLoginService autoLoginService;
        private readonly AccountService accountService;
        public UtilService(IHttpClientFactory _httpClientFactory, AutoLoginService _autoLoginService, AccountService accountService)
        {
            httpClientFactory = _httpClientFactory;
            autoLoginService = _autoLoginService;
            this.accountService = accountService;
        }
        /// <summary>
        /// 创建带有默认请求头的httpClient 带有cookie则为请求cookie
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public HttpClient createDefaultRequestHeaderHttpClient(string? cookie)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrEmpty(cookie))
            {
                httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
            }
            httpClient.DefaultRequestHeaders.Add("Host", "ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002");
            httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");
            return httpClient;

        }
        public HttpClient createDefaultRequestHeaderHttpClient()
        {
            var cookie = accountService.getCookieByAccount();

            return createDefaultRequestHeaderHttpClient(cookie);

        }


    }
}