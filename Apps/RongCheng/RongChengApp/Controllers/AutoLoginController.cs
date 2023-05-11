using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
namespace RongChengApp.Controllers
{
    /// <summary>
    /// 自动登录
    /// </summary>
    [ApiController]
    [Route("/api/AutoLogin")]
    public class AutoLoginController
    {
        /// <summary>
        /// 自动登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<bool> autoLogin()
        {
            return true;
        }
    }
}