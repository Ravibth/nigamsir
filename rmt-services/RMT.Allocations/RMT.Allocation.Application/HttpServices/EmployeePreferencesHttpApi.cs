using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using System.Text;

namespace RMT.Allocation.Application.HttpServices
{
    public class EmployeePreferencesHttpApi : IEmployeePreferencesHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public EmployeePreferencesHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<EmployeePreferencesByEmailDTO>> GetEmployeePreferenceDetailsByEmails(GetEmployeePreferenceDetailsDTO request)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("EmployeeMicroserviceBaseApiUrl").Value;
                string getEmployeePreferenceDetailsByEmailsPath = _config.GetSection("MicroserviceApiSettings").GetSection("getEmployeePreferenceDetailsByEmailsPath").Value;

                var content = new StringContent(JsonConvert.SerializeObject(request.email), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeePreferenceDetailsByEmailsPath, content);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();   
                    List<EmployeePreferencesByEmailDTO> finalResponse = JsonConvert.DeserializeObject<List<EmployeePreferencesByEmailDTO>>(resp);
                    return finalResponse;
                }
                else
                {
                    string response = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception("Error fetching GetEmployeePreferenceDetailsByEmails" + response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
