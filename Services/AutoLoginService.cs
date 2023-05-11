namespace WebApplication1.Services
{
    /// <summary>
    /// 自动登录
    /// 单例
    /// </summary>
    public class AutoLoginService
    {

        private readonly IHttpClientFactory httpClientFactory;


        public AutoLoginService(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        /// <value></value>
        public Timer? autoLoginTimer { get; set; }
        /// <summary>
        /// 启动自动登录
        /// </summary>
        public void Start()
        {
            var http = httpClientFactory.CreateClient();
            http.PostAsJsonAsync<LOaddo>("10.1.7.107:9502/hospital/multiple/public/get_mc_doctor")


        }
    }
}