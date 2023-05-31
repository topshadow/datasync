using Microsoft.AspNetCore.Mvc;
using RongChengApp.Dtos;

namespace RongChengApp.Services
{
    public class EHRService
    {
        private readonly UtilService utilService;
        public EHRService(UtilService _utilService)
        {
            utilService = _utilService;

        }
        /// <summary>
        /// 根据身份证号换取phrId
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public async Task<SearchEhrByIdCardResult> getphrIdByIdCard(string idCard)
        {
            var search = new SearchEhrByIdCardInput();
            search.setIdCard(idCard);
            var httpClient = utilService.createDefaultRequestHeaderHttpClient();
            var rtn = await httpClient.PostAsJsonAsync(utilService.remoteServerUrl + "?start=0&limit=50", search);
            return await rtn.Content.ReadFromJsonAsync<SearchEhrByIdCardResult>();


        }
    }
}
