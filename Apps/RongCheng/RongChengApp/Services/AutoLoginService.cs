using RongChengApp.Dtos;
// using PuppeteerSharp;
// using System.Drawing.Imaging;
using System.Text;
using System.Security.Cryptography;
using HSSB;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using PuppeteerSharp.Input;
using System.Security.Principal;

namespace RongChengApp.Services
{
    public class DateTimeSign
    {
        public DateTime time { get; set; }
        public string random { get; set; }
    }
    public class AccountPassword
    {
        public string account { get; set; }
        public string password { get; set; }
    }
    /// <summary>
    /// 自动登录
    /// 单例
    /// </summary>
    public class AutoLoginService
    {
        public List<DateTimeSign> dateTimeSigns { get; set; } = new List<DateTimeSign>();
        /// <summary>
        /// 批量生成的字典  
        /// 如 2023-05-01 00  040c1137d6955a6c7db79ed....
        /// 如 2023-05-01 01  040c1137d6955a6c7db79ed....
        /// 
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public Dictionary<string, string> dicts { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 随机时间戳的random字符串
        /// </summary>
        public string d
        {
            get
            {
                var now = DateTime.Now;
                var sign = dateTimeSigns
                  .Where(time => time.time.Year == now.Year
                  && time.time.Month == now.Month
                  && time.time.Day == now.Day
                  && time.time.Hour == now.Hour).FirstOrDefault();
                if (sign != null)
                {
                    return sign.random;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 账号cookie字典
        /// </summary>

        public Dictionary<string, string> accountCookie { get; set; } = new Dictionary<string, string>();

        private readonly IHttpClientFactory httpClientFactory;
        private readonly string serverurl = "http://localhost/mock.json";
        public LoadSystemDoctorInfoResult? result { get; set; }

        public async Task batAutoLogin(List<AccountPassword> accounts)
        {
            foreach (var account in accounts)
            {
                 autoLoginByServer(account);
            }

        }


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
        // public async Task Start()
        // {
        //     var data = await loadMock();
        //     result = data;
        //     var item = data.data.data[0];
        //     await Login(item.note1, item.note2);


        // }

        public async Task<LoadSystemDoctorInfoResult> loadMock()
        {
            var http = httpClientFactory.CreateClient();
            return await http.GetFromJsonAsync<LoadSystemDoctorInfoResult>("http://localhost:5062/mock.json");

        }
        // public async Task Login(string username, string password)
        // {
        //     var isGetPublicKey = false;
        //     using var browserFetcher = new BrowserFetcher();
        //     await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        //     var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        //     {
        //         IgnoreHTTPSErrors = true,
        //         Devtools = true,
        //         Headless = false,
        //         Args = new string[] { "--disable-web-security" },
        //     });
        //     var page = await browser.NewPageAsync();
        //     var capchaSrc = "";

        //     page.Response += async (sender, e) =>
        //     {
        //         if (e.Response.Url.EndsWith("index.html"))
        //         {
        //             e.Response.Headers.Add("Access-Control-Allow-Private-Network", "true");
        //         }
        //         if (e.Response.Url.StartsWith("data:image/png;base64,"))
        //         {
        //             if (e.Response.Url.Length > capchaSrc.Length)
        //             {
        //                 capchaSrc = e.Response.Url;
        //             }
        //         }
        //         if (e.Response.Url.EndsWith("publicKey"))
        //         {
        //             isGetPublicKey = true;
        //             var cookie = e.Response.Headers["Set-Cookie"];
        //             if (this.result != null)
        //             {

        //                 var item = this.result.data.data.Where(item => item.note1 == username).FirstOrDefault();
        //                 if (item != null)
        //                 {
        //                     item.cookie = cookie;
        //                     Console.WriteLine(cookie);
        //                 }
        //             }
        //         }
        //     };




        //     await page.GoToAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
        //     await page.AddScriptTagAsync(new AddTagOptions { Url = "http://localhost:5062/fix.js", });
        //     var timer = new System.Timers.Timer(12000);
        //     timer.Elapsed += async (e, o) =>
        //     {
        //         if (isGetPublicKey)
        //         {
        //             Console.WriteLine("登陆成功");
        //             timer.Stop();
        //         }
        //         await autoRun(page, capchaSrc, username, password);
        //         capchaSrc = String.Empty;
        //     };
        //     timer.Start();


        // }

        // public async Task autoRun(IPage page, string capchaSrc, string username, string password)
        // {
        //     Console.WriteLine("auto run");

        //     await page.EvaluateExpressionAsync("document.getElementById('ext-comp-1001').value=" + "\"" + username + "\"");
        //     await page.EvaluateExpressionAsync("document.getElementById('pwd').value=" + "\"" + password + "\"");
        //     var setVarExpreess = "window.capchaSrc=" + "`" + capchaSrc + "`";
        //     await page.EvaluateExpressionAsync(setVarExpreess);

        //     await page.EvaluateExpressionAsync(" setTimeout(()=>{startCapcha()},1000) ");
        //     var starDragTimmer = new System.Timers.Timer(2000);
        //     starDragTimmer.AutoReset = false;
        //     starDragTimmer.Elapsed += async (s2, e2) =>
        //     {
        //         var x = await page.EvaluateExpressionAsync<decimal>("window['xy'].x");

        //         var y = await page.EvaluateExpressionAsync<decimal>("window['xy'].y");
        //         Console.WriteLine("获取点数据x:" + x + "  y:" + y);
        //         var target = x * (400 / 300);
        //         await autoInputCapcha(page, target, 60);

        //     };
        //     starDragTimmer.Start();



        // }
        // public async Task autoInputCapcha(IPage page, decimal target, int step)
        // {
        //     await page.Mouse.MoveAsync(226, 425, new PuppeteerSharp.Input.MoveOptions { Steps = 1 });
        //     await page.Mouse.DownAsync();
        //     Thread.Sleep(300);
        //     var r = new Random();
        //     var stepLength = target / step;
        //     for (var i = 0; i < step; i++)
        //     {
        //         if (i < 20)
        //         {
        //             await page.Mouse.MoveAsync(226 + (stepLength * i + (r.NextInt64(0, 10))), 450 + r.NextInt64(1, 20), new PuppeteerSharp.Input.MoveOptions { Steps = 3 });
        //             Thread.Sleep(1);
        //         }
        //         else if (i >= 20 && i < 40)
        //         {
        //             await page.Mouse.MoveAsync(226 + (stepLength * i + (r.NextInt64(0, 10))), 450 + r.NextInt64(1, 20), new PuppeteerSharp.Input.MoveOptions { Steps = 3 });
        //         }
        //         else
        //         {
        //             await page.Mouse.MoveAsync(226 + (stepLength * i + (r.NextInt64(0, 10))), 450 + r.NextInt64(1, 100), new PuppeteerSharp.Input.MoveOptions { Steps = 3 });
        //             Thread.Sleep((step));
        //         }
        //     }
        //     await page.Mouse.MoveAsync(226 + target + r.NextInt64(15, 25), 450, new PuppeteerSharp.Input.MoveOptions { Steps = 1 });
        //     await page.Mouse.UpAsync();
        //     Thread.Sleep(1000);
        //     await page.EvaluateExpressionAsync("""document.getElementById("logon").click()""");
        // }
        public async Task autoLoginByServer(AccountPassword account)
        {
            var httpClient = httpClientFactory.CreateClient();
            var uuid = Guid.NewGuid().ToString();
            var slider = "slider-" + uuid;
            var point = "point-" + uuid;
            var rtn = await httpClient.PostAsJsonAsync<GetCapchaInput>("http://ph01.gd.xianyuyigongti.com:9002/chis/captcha/get", new GetCapchaInput
            {
                ts = DateTime.Now.Millisecond,
                clientUid = slider
            });
            var data = await rtn.Content.ReadFromJsonAsync<GetCapchaResult>();
            Console.WriteLine(data.repData.secretKey);
            var targetPoint = await parseImageFromBase64(data.repData.originalImageBase64);
            var capchaRtn = await mockSubmitCapcha((decimal)targetPoint.X - 20, data.repData.secretKey, data.repData.token, slider, -20, account);
            var capchaResponseText = await capchaRtn.Content.ReadAsStringAsync();
            if (capchaResponseText.Contains("captchaId"))
            {
                var result = await capchaRtn.Content.ReadFromJsonAsync<GetCapchaResult>();
                await mockLogin(result, account);

            }

        }

        public async Task<Point> parseImageFromBase64(string base64Str)
        {
            var targetPoint = new Point { X = 0, Y = 0 };
            List<Point> whitePoints = new List<Point>();
            byte[] bytes = Convert.FromBase64String(base64Str);
            var stream = new MemoryStream(bytes);
            using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(stream))
            {
                for (var x = 0; x < image.Width; x++)
                {
                    for (var y = 0; y < image.Height; y++)
                    {
                        var color = image[x, y];
                        // Console.WriteLine($"r:{color.R},g:{color.G},b:{color.B}");
                        if (color.R == 255 && color.G == 255 && color.B == 255)
                        {
                            whitePoints.Add(new Point { X = x, Y = y });
                        }
                    }
                }
                Console.WriteLine(whitePoints.Count);
                for (var y = 0; y < image.Height; y++)
                {
                    var yPoints = whitePoints.Where(p => p.Y == y);
                    var start = 0;
                    var startX = 0;
                    foreach (var p in yPoints)
                    {
                        if (whitePoints.Any(po => po.X == p.X + 1))
                        {
                            start++;
                            if (start > 20)
                            {
                                Console.WriteLine($"找到  {p.X},{y}");
                                targetPoint.X = p.X;
                                targetPoint.Y = y;
                                break;
                            }
                        }
                        else
                        {
                            startX = 0;
                        }
                    }

                }
                return targetPoint;
            }
              



        }
        public async Task<HttpResponseMessage> mockSubmitCapcha(decimal moveLeftDistance, string key, string token, string slider, Int64 padding, AccountPassword account)
        {
            // moveLeftDistance = decimal.Multiply(moveLeftDistance, decimal.Divide(decimal.Parse("310"), decimal.Parse("400")));
            //图片滑动
            var pointJsonObj = new CapchaPointJson
            {
                x = decimal.Ceiling(moveLeftDistance)
            };
            var pointJsonStr = System.Text.Json.JsonSerializer.Serialize(pointJsonObj);
            byte[] bytes = Encoding.UTF8.GetBytes(pointJsonStr);
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                var encrypt = AesDecrypt(pointJsonStr, key);
                Console.WriteLine($"encrypt:{encrypt}");
                var capchaMockData = new CapchaMockData { pointJson = encrypt, token = token, clientUid = slider };
                var httpClient = httpClientFactory.CreateClient();
                var rtn = await httpClient.PostAsJsonAsync<CapchaMockData>("http://ph01.gd.xianyuyigongti.com:9002/chis/captcha/check", capchaMockData);
                return rtn;
            }
        }

        public async Task mockLogin(GetCapchaResult result, AccountPassword account)
        {
            //accountCookie.Clear();
            var httpClient = httpClientFactory.CreateClient();
            if (result.repData != null)
            {
                // var publickKey = "043b59b42bca1306be9d57258ba2ae52c9c3101343e2f76c5e9fcd806d1878ca3790dbb25cf2e829cd88412c5a421344135d9eb70962bf295de7890511a42f8a0a1e839677418a3bd412bd9d743256662d3c500f37cad6f6641beb6dad1e98126c62e14238b101d1909a8cdfb477";
                var now = DateTime.Now;
                var timeper = ((new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, 0, 0).ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
                // var d = SM2.SM2Console(publickKey, timeper);
                var url = "http://ph01.gd.xianyuyigongti.com:9002/chis/logon/myApps";
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Referer", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
                httpClient.DefaultRequestHeaders.Add("Origin", "http://ph01.gd.xianyuyigongti.com:9002/chis/index.html");
                httpClient.DefaultRequestHeaders.Add("encoding", "utf-8");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                var dtoObj = new MyRolesInputDto
                {
                    captcha = result.repData.captchaVerification,
                    d = d,
                    pwd = account.password,
                    url = "logon/myRoles",
                    uid = account.account
                };
                var urt = "47416";
                if (account.account == "605999")
                {
                    urt = "26900";
                }
                var urlParam = $"?urt={urt}&uid={dtoObj.uid}&pwd={dtoObj.pwd}&captcha={result.repData.captchaVerification}&checkCaptcha=check&deep=3&d={d}";
                url = url + urlParam;
                // var urlEncode = System.Web.HttpUtility.UrlEncode(url);

                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(dtoObj));
                var loginRtn = await httpClient.PostAsJsonAsync("http://ph01.gd.xianyuyigongti.com:9002/chis/logon/myRoles", dtoObj
               );
                var loginText = await loginRtn.Content.ReadAsStringAsync();
                String.Join(" ", loginRtn.Headers.TryGetValues("Set-Cookie", out IEnumerable<string>? setCookie));
                if (setCookie != null)
                {

                   var  Cookie = setCookie.FirstOrDefault();
                    Cookie = Cookie.Replace("Path=/chis;", "").Replace("HttpOnly", "");

                    accountCookie[account.account] = Cookie;

                    // Console.WriteLine(Cookie);
                    foreach (var item in loginRtn.Content.Headers)
                    {
                        Console.WriteLine(item.Key + ":" + item.Value);
                    }
                    var myAppsRtn = await httpClient.PostAsJsonAsync(url, new { httpMethod = "POST", url = "logon/myApp" + urlParam });
                    var myAppsData = await myAppsRtn.Content.ReadAsStringAsync();
                    Console.WriteLine(myAppsData);
                    Console.WriteLine(account.account + "登录成功");
                }
                else
                {
                    Console.WriteLine($"账号{account.account}没有登录成功");
                }

            }
        }






        public static string AesDecrypt(string content, string key)
        {
            // nodejs aes加密默认的key使用了md5加密，所以C#解密的key也要默认使用md5
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = Encoding.UTF8.GetBytes(key);
            byte[] keyArray = output;// md5.ComputeHash(output);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);
            RijndaelManaged des = new RijndaelManaged();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = des.CreateEncryptor(keyArray, null);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }
        public static byte[] HexStringToBinary(string hexstring)
        {
            var inputByteArray = new byte[hexstring.Length / 2];
            for (var x = 0; x < inputByteArray.Length; x++)
            {
                var i = Convert.ToInt32(hexstring.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            return inputByteArray;
        }


    }



}