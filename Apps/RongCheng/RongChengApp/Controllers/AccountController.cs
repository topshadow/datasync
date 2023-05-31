using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace RongChengApp.Controllers
{
    [ApiController]
    [Route("/api/Account")]
    public class AccountController
    {
        private readonly IHttpContextAccessor contextAccessor;
        public AccountController(IHttpContextAccessor _httpContextAccessor)
        {
            contextAccessor = _httpContextAccessor;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<object> batSync()
        {
            contextAccessor.HttpContext.Request.Headers.TryGetValue("account", out StringValues account);

            return "ok";
        }



    }
}
