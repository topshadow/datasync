namespace RongChengApp.Services
{

    public class ApiConfig
    {
        public string serverUrl { get; set; }
    }
    public class ConfigService
    {
        private readonly IConfiguration configuration;
        /// <summary>
        /// 应用第三方参数配置
        /// </summary>
        /// <value></value>
        public ApiConfig apiConfig { get; set; }
        public ConfigService(IConfiguration _configuration)
        {
            configuration = _configuration;
            apiConfig = configuration.GetSection("ApiConfig").Get<ApiConfig>();

        }

    }
}