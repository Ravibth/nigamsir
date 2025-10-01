using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using System.Net.Http.Headers;
using System.Text;

namespace RMT.Allocation.Application.HttpServices
{
    public class EmployeeMasterHttpApi : IEmployeeMasterHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public EmployeeMasterHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }


        public async Task<List<EmployeeMasterDTO>> GetEmployeeMasterDataHttpApiQuery(GetEmployeeMasterDetailsDTO request, string token)
        {
            try
            {
                // var content = new StringContent(JsonConvert.SerializeObject(request.email), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getAllEmployee = _config.GetSection("MicroserviceApiSettings").GetSection("AllUserUrl").Value;
                List<EmployeeMasterDTO> employeesMasterList = new List<EmployeeMasterDTO>();

                Dictionary<string, dynamic> queries = new()
                {
                };
                if (!String.IsNullOrEmpty(request.designation))
                {
                    queries.Add("designation", request.designation);
                }
                if (request.Emails != null && request.Emails.Count > 0)
                {
                    queries.Add("email_id", request.Emails);
                }
                var url = Helper.UrlBuilderByQuery(baseurl + getAllEmployee, queries);

                var apiResponse = await _httpClient.GetAsync(url);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    GetEmployeeMasterDetailsResponseDTO response = JsonConvert.DeserializeObject<GetEmployeeMasterDetailsResponseDTO>(resp);
                    foreach (var user in response.rows)
                    {
                        EmployeeMasterDTO employeeMaster = new()
                        {
                            empName = user.name,
                            designation = user.designation,
                            email = user.email_id,
                            location = user.location,
                            supercoach = user.supercoach_name,
                            industry = user.industry,
                            sub_industry = user.sub_industry,
                            revenue_unit = "",//Recheck
                            business_unit = user.business_unit,
                            supercoach_name = user.supercoach_name,
                            grade = user.grade,
                            co_supercoach_name = user.co_supercoach_name,
                            supercoach_mid = user.supercoach_mid,
                            co_supercoach_mid = user.co_supercoach_mid,
                            competency = user.competency,
                            uemail_id = user.uemail_id,
                            competencyId = user.competencyId,
                            employee_id = user.employee_id,
                            skill = new SkillsEntities[] { }, //TODO This need to replace 
                        };
                        employeesMasterList.Add(employeeMaster);
                    }
                    return employeesMasterList;
                }
                else
                {
                    string response = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception("Error fetching GetEmployeeMasterDataHttpApiQuery URL:" + baseurl + getAllEmployee + ", Respons:" + response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<EmployeeOfferingSolutionResp>?> GetEmpByProjectMapping(List<string>? offerings, List<string>? solutions)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("EmployeeMicroserviceBaseApiUrl").Value;
            string getApiPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmpByProjectMapping").Value;

            EmpByProjectMappingRequestDto requestObj = new()
            {
                Offerings = offerings,
                Solutions = solutions,
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getApiPath, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<EmpProjectMappingResponse>? allOfferingSolutionData = JsonConvert.DeserializeObject<List<EmpProjectMappingResponse>>(resp);

                var groupedData = allOfferingSolutionData?
                    .GroupBy(m => m.EmpMID)
                    .ToList();

                var finalResponse = new List<EmployeeOfferingSolutionResp>();

                foreach (var group in groupedData)
                {
                    finalResponse.Add(new()
                    {
                        emp_mid = group.Key,
                        offerings = group.DistinctBy(m => m.Offering).Select(m => m.Offering).ToList(),
                        solutions = group.DistinctBy(m => m.Solution).Select(m => m.Solution).ToList(),
                    });
                }
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
