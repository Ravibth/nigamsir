using Newtonsoft.Json;
using RMT.Scheduler.DTOs.GT360;
using RMT.Scheduler.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.GT360
{
    public class GT360HttpService : IGT360HttpService
    {
        private readonly IGTTokenService _gtTokenService;
        public GT360HttpService(IGTTokenService gtTokenService)
        {
            _gtTokenService = gtTokenService;
        }

        public async Task<GT360TimesheetResponseDto> PostTimeSheetData(GT360TimesheetRequestDto requestData)
        {
            GT360TimesheetResponseDto responsData = null;
            var EnvVarDictionary = Environment.GetEnvironmentVariables();
            var gtT360SubmitTimesheetApiUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GT360SubmitTimesheetApiUrl]);

            var _httpClient = _gtTokenService.GetCustomHttpClient();

            var httpRequestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(httpRequestContent);
            Console.WriteLine("-----------------------------------------");

            var httpResponse = await _httpClient.PostAsync(gtT360SubmitTimesheetApiUrl, httpRequestContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStr = await httpResponse.Content.ReadAsStringAsync();
                responsData = JsonConvert.DeserializeObject<GT360TimesheetResponseDto>(responseStr);
                return responsData;
            }
            else
            {
                throw new Exception("Unable to post GT timesheet content");
            }
            return responsData;
        }


    }
}
