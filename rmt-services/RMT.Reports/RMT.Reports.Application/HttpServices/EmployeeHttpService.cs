using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Reports.Application.DTO.Request;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.HttpServices
{
    public class EmployeeHttpService : IEmployeeHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public EmployeeHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<EmpProjectMappingResponse>?> GetEmpByProjectMapping(List<string>? offerings, List<string>? solutions)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("EmployeeMicroserviceBaseApiUrl").Value;
            string getApiPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmpByProjectMapping").Value;

            EmpByProjectMappingRequestDto requestObj = new EmpByProjectMappingRequestDto()
            {
                Offerings = offerings,
                Solutions = solutions,
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getApiPath, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<EmpProjectMappingResponse>? finalResponse = JsonConvert.DeserializeObject<List<EmpProjectMappingResponse>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getApiPath + "-" + resp);
            }
        }

    }
}
