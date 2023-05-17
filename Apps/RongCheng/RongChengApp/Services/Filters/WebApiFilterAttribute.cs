using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace RongChengApp.Services.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine("hello exception");
            var result = new ObjectResult(new
            {
                context.Exception.Message, // Or a different generic message
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Log the exception
            // _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

            // Set the result
            context.Result = result;
            // //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            // Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
            //     actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
            //     actionExecutedContext.Exception.StackTrace);

            // //2.返回调用方具体的异常信息
            // if (actionExecutedContext.Exception is NotImplementedException)
            // {
            //     actionExecutedContext. = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            // }
            // else if (actionExecutedContext.Exception is TimeoutException)
            // {
            //     actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            // }
            // //.....这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            // else
            // {
            //     actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            // }

            base.OnException(context);
        }
    }
}