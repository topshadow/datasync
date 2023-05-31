using System;
using RongChengApp.Services;
using Microsoft.AspNetCore;
using Xunit.Sdk;
using Moq;
using Autofac;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using RongChengApp.Dtos;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace RongChengAppTest
{

    public class UnitTest1
    {
        private IContainer _autofacContainer;
        protected IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer == null)
                {
                    var builder = new ContainerBuilder();

                    builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ConfigService)))
                    .Where(t => t.Name.EndsWith("Service")).SingleInstance()

                    ;
                    builder.RegisterType<TestService>().SingleInstance();
                    builder.Register<IHttpClientFactory>(_ =>
                    {
                        var services = new ServiceCollection();
                        services.AddHttpClient();
                        var provider = services.BuildServiceProvider();
                        return provider.GetRequiredService<IHttpClientFactory>();
                    });
                    // Other wireups....

                    var container = builder.Build();

                    _autofacContainer = container;
                }
                var isRe = _autofacContainer.IsRegistered<TestService>();


                return _autofacContainer;
            }
        }
        protected UtilService utilService
        {
            get
            {
                return AutofacContainer.Resolve<UtilService>();
            }
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine(utilService.remoteServerUrl);

            var a = DateTime.Parse("2020-12-12 22:00:00").ToString("yyyy-MM-ddTHH:mm:ss");
            Console.WriteLine(a);
        }


        [Fact]
        public void TestCodeDom()
        {
            CodeTypeDeclaration dcl = new CodeTypeDeclaration("Dog");
            dcl.IsStruct = true;
            dcl.Attributes = MemberAttributes.Public;
            // 生成C#代码
            CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
            Console.WriteLine("生成 C# 代码：");
            provider.GenerateCodeFromType(dcl, Console.Out, null);

        }


        /// <summary>
        /// 爬取地址代码
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void crawlAddressRegionCode()
        {
            var query = new
            {
                serviceId = "chis.simpleQuery",
                method = "execute",
                schema = "chis.application.hr.schemas.EHR_AreaGrid_LIST",
                cnd = new object[] { "eq", new[] { "$", "a.parentCode" }, new string[] { "s", "0" } },
                pageSize = 50,
                pageNo = 1,
                serviceAction = ""
            };
            var url = "http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest?start=0&limit=50";
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var header = httpClient.DefaultRequestHeaders.Select(h => h.Key.ToLower() == "cookie").FirstOrDefault();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("cookie", "JSESSIONID=65D26904FA7FA79D43D9B786F61D5FC5;");
            var rtn = await httpClient.PostAsJsonAsync(url, query);
            var tesxt = await rtn.Content.ReadAsStringAsync();
            var top = await rtn.Content.ReadFromJsonAsync<SearchRegionResult>();
            foreach (var v2 in top.body)
            {
                var data = await httpClient.PostAsJsonAsync(url, createQueryObject(v2.regionCode));

                var itemResult = await data.Content.ReadFromJsonAsync<SearchRegionResult>();
                v2.body = itemResult.body;
                foreach (var v3 in v2.body)
                {
                    var v3data = await httpClient.PostAsJsonAsync(url, createQueryObject(v3.regionCode));

                    var v3itemResult = await v3data.Content.ReadFromJsonAsync<SearchRegionResult>();
                    v3.body = v3itemResult.body;
                    foreach (var v4 in v3.body)
                    {
                        var v4data = await httpClient.PostAsJsonAsync(url, createQueryObject(v4.regionCode));

                        var v4itemResult = await v4data.Content.ReadFromJsonAsync<SearchRegionResult>();
                        v4.body = v4itemResult.body;


                    }

                }

            }


            Console.WriteLine(top);
            await System.IO.File.WriteAllTextAsync("region.json", System.Text.Json.JsonSerializer.Serialize(top));





        }

        public object createQueryObject(string o)
        {
            return new
            {
                serviceId = "chis.simpleQuery",
                method = "execute",
                schema = "chis.application.hr.schemas.EHR_AreaGrid_LIST",
                cnd = new object[] { "eq", new[] { "$", "a.parentCode" }, new string[] { "s", o } },
                pageSize = 50,
                pageNo = 1,
                serviceAction = ""
            };
        }
    }
}