using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RongChengApp.Services;

namespace RongChengApp.Pages
{
    public partial class Index:ComponentBase
    {
        public StringNumber tabIndex { get; set; } = 1;
        [Inject] IJSRuntime jsRuntime { get; set; }
        [Inject] AutoLoginService autoLoginService { get; set; }
        [Inject] AccountService accountService { get; set; }
        public async Task random()
        {
            var now = DateTime.Now;


            var time = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, 0);
            var max = 3 * 24;
            var result = new List<DateTimeSign>();
            var ppp = "04C7E34D7FB4EECE60C29ED53867F98AA072C0B562787BCA312919EB3E12753BAC462AC485866DC7264CCF03A47C975807674B5684596A96814EC8E59AE17A2974";

            var times = new List<DateTime>();
            for (var i = 0; i < max; i++)
            {
                var currentTime = time.AddHours(i);
                times.Add(currentTime);
            }
            var randomData = await jsRuntime.InvokeAsync<List<string>>($"aaaEncBat", times, ppp);
            //   result.Add(new DateTimeSign{time=currentTime,random=random});
            foreach (var ti in times)
            {
                result.Add(new DateTimeSign { time = ti, random = randomData[times.IndexOf(ti)] });
            }
            autoLoginService.dateTimeSigns = result;



        }
    }
}
