namespace RongChengApp.Dtos
{
    public class GetCapchaInput
    {
        public string captchaType { get; set; } = "blockPuzzle";
        /// <summary>
        /// slider
        /// </summary>
        /// <value></value>
        public string clientUid { get; set; }
        public long ts { get; set; }
    }
    public class GetCapchaResult
    {
        /// <summary>
        /// 0000 成功
        /// </summary>
        /// <value></value>
        public string? repCode { get; set; }
        public GetCapchaResultData repData { get; set; }
        public string? repMsg { get; set; }
        public bool success { get; set; }
    }
    public class GetCapchaResultData
    {
        public string? captchaId { get; set; }
        public string? captchaType { get; set; }
        public string? captchaOriginalPath { get; set; }
        public string? browserInfo { get; set; }
        public string? captchaFontSize { get; set; }
        public string? captchaFontType { get; set; }
        public string? captchaVerification { get; set; }
        public string? clientUid { get; set; }
        public string jigsawImageBase64 { get; set; }
        public string originalImageBase64 { get; set; }
        public object? point { get; set; }
        public object? pointJson { get; set; }
        public object? pointList { get; set; }
        public object? projectCode { get; set; }
        public bool result { get; set; }
        public string secretKey { get; set; }
        public string token { get; set; }
        public object? ts { get; set; }
        public object wordList { get; set; }

    }
    public class CapchaMockData
    {
        public string captchaType { get; set; } = "blockPuzzle";
        public string? pointJson { get; set; }
        public string token { get; set; }
        public string clientUid { get; set; }
        public long ts { get; set; }
        public CapchaMockData()
        {
            var now = DateTime.Now;
            ts = ((now.ToUniversalTime().Ticks - 621355968000000000) / 10000);

        }

    }
    public class CapchaPointJson
    {
        public decimal x { get; set; }
        public decimal y { get; set; } = decimal.Parse("5.0");

    }
    public class CheckCapchaResult
    {

        public string? repCode { get; set; }
        public CheckCapchaResultData? repData { get; set; }
        public string? repMsg { get; set; }
        public bool success { get; set; }
    }

    public class CheckCapchaResultData
    {
        public object? browserInfo { get; set; }
        public object? captchaFontSize { get; set; }
        public object? captchaFontType { get; set; }
        public string? captchaId { get; set; }
        public string? captchaOriginalPath { get; set; }
        public string? captchaType { get; set; }
        public string? captchaVerification { get; set; }
        public string clientUid { get; set; }
        public string jigsawImageBase64 { get; set; }
        public string originalImageBase64 { get; set; }
        public object? point { get; set; }
        public string pointJson { get; set; }
        public string pointList { get; set; }
        public string projectCode { get; set; }
        public bool result { get; set; }
        public object? secretKey { get; set; }
        public string token { get; set; }
        public string ts { get; set; }
        public object? wordList { get; set; }
    }

    public class LoginMyAppsInput
    {
        /// <summary>
        /// /logon/myApps?urt=46476&uid=&pwd&capcha&checkCaptcha=check&deep=3&deep=04153da207cf3ab55e337edab5014f3731a06851aa276619243b4bcdfa99e20514caf613ad7bf70acf82d6ee778960a44fb4db6fc4d4de4dac42f5a710e78fb7b207f1c963da59cd2cb0bd2d7e9fdd393068d7c104fbe28f5501cadc4dd42d5a0adbf1c8b7b7fa48b8cdea7768e2&
        /// </summary>
        /// <value></value>
        public string url { get; set; }
        public string httpMethod { get; set; } = "POST";

    }
    public class MyRolesInputDto
    {
        public string captcha { get; set; }
        public string d { get; set; }
        public string pwd { get; set; } = "Admin123!";
        public string url { get; set; } = "logon/myRoles";
        public string uid { get; set; } = "605100";
    }

}