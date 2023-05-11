using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using RongChengApp.Dtos;
using RongChengApp.Services;
namespace RongChengApp.Controllers
{



    [ApiController]
    [Route("[controller]")]

    public class HypertensController
    {
        public readonly HttpClient httpClient;
        private readonly AutoLoginService autoLoginService;
        public HypertensController(HttpClient _httpClient, AutoLoginService _autoLoginService)
        {
            httpClient = _httpClient;
            autoLoginService = _autoLoginService;
        }


        /// <summary>
        /// 查询高血压记录
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("listHypertensionRecord")]
        public async Task<BaseOutput<QueryHypertensionResultItem>> listHypertensionRecord([FromBody] QueryHypertension body)
        {

            httpClient.DefaultRequestHeaders.Add("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            var res = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest?start={body.pageNo}&limit={body.pageSize}",

       body
             );
            var resText = await res.Content.ReadAsStringAsync();
            Console.WriteLine(resText);


            var data = await res.Content.ReadFromJsonAsync<BaseOutput<QueryHypertensionResultItem>>();


            return data;
        }
        /// <summary>
        ///  查询日期是否存在随访记录
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("saveRepairPlan")]
        public async Task<SaveRepairPlanResult> saveRepairPlan([FromBody] SaveRepairPlanInput body)
        {

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add
              ("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            var res = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", body);
            var resText = await res.Content.ReadAsStringAsync();
            Console.WriteLine(resText);
            return await res.Content.ReadFromJsonAsync<SaveRepairPlanResult>();



        }
        /// <summary>
        /// 获取某个患者的全年随访记录
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<GetCurYearVisitPlanResult> getCurYearVisitPlan([FromBody] GetCurYearVisitPlanInput body)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add
              ("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            var res = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", body);
            var resText = await res.Content.ReadAsStringAsync();
            Console.WriteLine(resText);
            return await res.Content.ReadFromJsonAsync<GetCurYearVisitPlanResult>();
        }
        // /// <summary>
        // /// 随访初始化
        // /// </summary>
        // /// <returns></returns>
        // public async Task<sbyte>  visitInitialize(){

        // }
        /// <summary>
        /// 获取某次随访详情
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<GetVisitInfoResult> getVisitInfo([FromBody] GetVisitInfoInput body)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add
              ("Cookie", autoLoginService.Cookie);
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
            var res = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", body);
            var resText = await res.Content.ReadAsStringAsync();
            Console.WriteLine(resText);
            return await res.Content.ReadFromJsonAsync<GetVisitInfoResult>();


        }
        /// <summary>
        /// 自动添加访问记录
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<AutoAddVisitRecordResult> autoAddVisitRecordInput([FromBody] AutoAddVisitRecordInput body)
        {




            var queryIdCard = new QueryHypertension
            {

                cnd = new List<object>{
        "and",
        new List<object> {"eq", new List<object>{"$","a.status"}, new List<object> {"s","0"}} as object,
       new List<object>{"like", new List<object>{"$","b.idCard"},new List<object>{"s",$"%{body.idcard}%" }}

                }
            };
            var empiId = String.Empty;
            var phrId = String.Empty;
            var userList = await this.listHypertensionRecord(queryIdCard);
            if (userList.body.Count <= 0)
            {
                return new AutoAddVisitRecordResult { ok = false, message = "找不到对应身份证号的患者信息" };
            }
            else
            {
                empiId = userList.body[0].empiId;
                phrId = userList.body[0].phrId;
                var input = new SaveRepairPlanInput
                {

                    body = new SaveRepairPlanBodyInput
                    {
                        beginDate = body.inputDate.AddDays(-5),
                        dateField = body.inputDate,
                        empiid = empiId,
                        phrld = phrId,
                        endDate = body.inputDate.AddDays(5)

                    }
                };
                var saveRtn = await this.saveRepairPlan(input);
                if (saveRtn.code == 200)
                {
                    // if (saveRtn?.body?.exist == true)
                    // {


                    var fullYearRes = await this.getCurYearVisitPlan(new GetCurYearVisitPlanInput { empiId = empiId });
                    var datestr = body.inputDate.ToString("yyyy-MM-dd");
                    Console.WriteLine("datestr:" + datestr);
                    var record = fullYearRes.body.Where(record => record.planDate == datestr).FirstOrDefault();
                    if (record != null)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Cookie", autoLoginService.Cookie);
                        httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

                        var saveUpdateInput = new SaveHypertensionVisitInput
                        {
                            op = String.IsNullOrEmpty(record.visitId) ? "create" : "update",
                            body = new SaveHypertensionVisitInputBody
                            {

                                visitId = record.visitId,
                                planId = record.planId,
                                empiId = empiId,
                                phrId = phrId,
                                targetWeight = body.targetWeight,
                                heartRate = body.heartRate,
                                visitDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                weight = body.weight,
                                bmi = body.bmi,
                                targetBmi = body.targetBmi,
                                smokeCount = body.smokeCount,
                                targetSmokeCount = body.targetSmeCount,
                                drinkCount = body.drinkCount,
                                targetDrinkCount = body.targetDrinkCount,
                                trainMinute = body.trainMinute,
                                trainTimesWeek = body.trainTimesWeek,
                                targetTrainTimesWeek = body.targetTrainTimesWeek,
                                targetTrainMinute = body.targetTrainMinute,

                                visitWay = body.visitWay,
                                cure = body.cure,
                                obeyDoctor = body.obeyDoctor,
                                referralReason = body.referralReason,
                                medicine = body.medicine,
                                medicineBadEffect = body.medicineBadEffect,
                                currentSymptoms = body.currentSymptoms,
                                psychologyChange = body.psychologyChange,
                                inputDate = body.inputDate,
                                constriction = body.constriction,
                                diastolic = body.diastolic,
                                medicineBadEffectText = body.medicineBadEffectText,
                                medicineNot = body.medicineNot,
                                medicineOtherNot = body.medicineOtherNot,
                                visitEvaluate = body.visitEvaluate,

                                nextDate = body.nextDate.ToString("yyyy-MM-dd"),
                                salt = body.salt,
                                inputUser = "605100",
                                targetSalt = body.targetSalt,
                                // auxiliaryCheck = body.auxiliaryCheck
                            }
                        };
                        var res = await httpClient.PostAsJsonAsync($"http://ph01.gd.xianyuyigongti.com:9002/chis/*.jsonRequest", saveUpdateInput);
                        var dataText = await res.Content.ReadAsStringAsync();
                        Console.WriteLine("datatext:" + dataText);
                        var data = await res.Content.ReadFromJsonAsync<SaveHypertensionVisitResult>();
                        if (data.code == 200)
                        {
                            return new AutoAddVisitRecordResult { ok = true, message = "上传成功" };
                        }
                        else
                        {
                            return new AutoAddVisitRecordResult { ok = false, message = data.code + "" + data.msg };
                        }

                        return new AutoAddVisitRecordResult { ok = true, message = "模板已存在，准备更新模板" };

                    }
                    else
                    {
                        return new AutoAddVisitRecordResult { ok = false, message = "错误,找不到对应的随访记录用以修改" };
                    }

                    // }
                    // else
                    // {
                    //     return new AutoAddVisitRecordResult { ok = true, message = "模板不存在,已经创建完成新的默认模板" };
                    // }

                    // return new AutoAddVisitRecordResult { ok = true, message = "创建模板成功" };
                }
                else
                {
                    return new AutoAddVisitRecordResult { ok = false, message = "创建模板失败" };
                }


                return new AutoAddVisitRecordResult { ok = true, message = $"找到患者信息empiId:{empiId}  phrId:{phrId}" };

            }


            return new AutoAddVisitRecordResult { ok = true, message = "同步成功" };
        }
    }





}