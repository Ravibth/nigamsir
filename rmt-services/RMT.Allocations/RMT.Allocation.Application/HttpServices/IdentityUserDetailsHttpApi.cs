using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMT.Allocation.Application.HttpServices
{
    public class getUserByNameOrEmailV6Request
    {
        public string name { get; set; }
    }

    public class IdentityUserDetailsHttpApi : IIdentityUserDetailsHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public IdentityUserDetailsHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }



        public async Task<List<IdentityUserResponseDTO>> GetEmployeesDataHttpApiQuery(List<string> request)
        {
            try
            {

                var finalResponse = new List<IdentityUserResponseDTO>();
                var batchSize = Convert.ToInt16(_config.GetSection("MicroserviceApiSettings").GetSection("batchSizeForInfoFetchV6").Value);
                var tasks = new List<Task<List<IdentityUserResponseDTO>>>();

                for (int i = 0; i < request.Count; i += batchSize)
                {
                    var emailsBatch = request.Skip(i).Take(batchSize).ToList();
                    tasks.Add(GetUsersDataAsync(emailsBatch));
                }

                var results = await Task.WhenAll(tasks);

                foreach (var result in results)
                {
                    finalResponse.AddRange(result);
                }

                return finalResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<IdentityUserResponseDTO>> GetUsersDataAsync(List<string> request)
        {
            try
            {
                // var content = new StringContent(JsonConvert.SerializeObject(request.email), Encoding.UTF8, "application/json");
                // _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUsers = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityGetUsersPathV6").Value;

                var finalResponse = new List<IdentityUserResponseDTO>();


                var joinedEmails = string.Join(',', request);

                getUserByNameOrEmailV6Request emails = new()
                {
                    name = joinedEmails
                };
                var content = new StringContent(JsonConvert.SerializeObject(emails), Encoding.UTF8, "application/json");

                var apiResponse = await _httpClient.PostAsync(baseurl + getUsers, content);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadFromJsonAsync<List<IdentityUserResponseDTO>>();
                    finalResponse.AddRange(resp);
                }
                else
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception("Error fetching GetEmployeesDataHttpApiQuery URL:" + baseurl + getUsers + "response -->" + response);
                }
                return finalResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<UserDTO>> GetUsersByEmailDataHttpApiQuery(List<string> emails)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUsersbyemail = _config.GetSection("MicroserviceApiSettings").GetSection("GetUsersByEmail").Value;
                var content = new StringContent(JsonConvert.SerializeObject(emails), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(baseurl + getUsersbyemail, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var finalResponse = JsonConvert.DeserializeObject<List<UserDTO>>(response);
                    return finalResponse;
                }
                else
                {
                    throw new Exception("Error in fetching email");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<UserDTO>> GetUsersBySuperCoachEmailDataHttpApiQuery(List<string> emails)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUsersbyemail = _config.GetSection("MicroserviceApiSettings").GetSection("GetUsersBySuperCoachEmails").Value;
                var content = new StringContent(JsonConvert.SerializeObject(emails), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(baseurl + getUsersbyemail, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var finalResponse = JsonConvert.DeserializeObject<List<UserDTO>>(response);
                    return finalResponse;
                }
                else
                {
                    throw new Exception("Error in fetching email");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetSupercoachUserListByAllocationSupercoachDelegate(string email)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUserInfoFromIdentity = _config.GetSection("MicroserviceApiSettings").GetSection("GetSupercoachUserListByAllocationSupercoachDelegate").Value;
                var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.GetAsync(baseurl + getUserInfoFromIdentity + $"?email={email}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    List<UserDTO> finalResponse = JsonConvert.DeserializeObject<List<UserDTO>>(resp);
                    return finalResponse;
                }
                else
                {
                    //_logger.LogInformation("Response:-" + await apiResponse.Content.ReadAsStringAsync());
                    throw new Exception("Error fetching GetUserInfo");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "");
                throw;
            }
        }
        public async Task<List<UserDTO>> GetEmailDataHttpApiQuery()
        {

            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
            string getUsersbyemail = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmails").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getUsersbyemail);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<GetUsersByEmailDTO>(res);
                var emailRes = finalResponse.rows.ToList();
                return emailRes;

            }
            else
            {
                throw new Exception("Error fetching GetEmailDataHttpApiQuery email");
            }

        }
        public async Task<UserInfoDTO> GetUserInfo(string email)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUserInfoFromIdentity = _config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoFromIdentity").Value;

                //_logger.LogInformation("---------------------------------------------------------");
                //_logger.LogInformation(baseurl + getUserInfoFromIdentity + $"?email_id={email}");
                var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
                //var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeePreferenceDetailsByEmailsPath, content);
                var apiResponse = await _httpClient.GetAsync(baseurl + getUserInfoFromIdentity + $"?email_id={email}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    UserInfoDTO finalResponse = JsonConvert.DeserializeObject<UserInfoDTO>(resp);
                    return finalResponse;
                }
                else
                {
                    //_logger.LogInformation("Response:-" + await apiResponse.Content.ReadAsStringAsync());
                    throw new Exception("Error fetching GetUserInfo");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "");
                throw;
            }
            // //string temp = "{designation: \"Associate Director\",\r\nemail: \"\",\r\nemp_code: \"C005-2\",\r\nid: 7,\r\nname: \"Ayush\",\r\nrole: \"System Admin\",\r\nroles: [\"System Admin\",\"Employee\"]}";

            // string str = "{\"id\":7,\"email\":\"\",\"name\":\"Ayush\",\"emp_code\":\"C005-2\",\"designation\":\"Associate Director\",\"roles\":[\"System Admin\",\"Resource Requestor\"],\"role\":\"System Admin\"}"";

            // UserInfoDTO finalResponse1 = JsonConvert.DeserializeObject<UserInfoDTO>(str);
            // return finalResponse1;
        }
    }
}
