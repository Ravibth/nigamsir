using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class WCGTMasterHttpApi : IWCGTMasterHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WCGTMasterHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<WcgtCompetencyMasterDTO>> GetCompetencyMaster()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getCompetencyMasterUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetCompetencyMaster").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getCompetencyMasterUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<WcgtCompetencyMasterDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WcgtCompetencyMasterDTO>>();
                return finalResponse;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetCompetencyMaster-" + response);
            }
        }
 

        public async Task<List<WCGTJobCodeClientDTO>> GetClientListByJobCodes(List<string> request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getClientListWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("GetClientListByJobCodes").Value;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync(baseurl + getClientListWcgtMaster, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                if (response != null)
                {

                    List<WCGTJobCodeClientDTO> finalResponse = JsonConvert.DeserializeObject<List<WCGTJobCodeClientDTO>>(response);
                    //List<GetRateDesignationDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<GetRateDesignationDTO>>();
                    return finalResponse;
                }
                else
                {
                    return new List<WCGTJobCodeClientDTO> { };
                }

            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetRateByDesignation-" + response);

            }

        }

        public async Task<List<WCGTDesigantionDTO>> GetDesignationWCGTMAsterHttpApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getDesignationWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("getDesignationWcgtMaster").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getDesignationWcgtMaster);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<WCGTDesigantionDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTDesigantionDTO>>();
                return finalResponse;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetDesignationWCGTMAsterHttpApiQuery-" + response);

            }
        }

        public async Task<List<WCGTMasterDataDTO>> GetWCGTMAsterDataHttpApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("getWcgtMaster").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getWcgtMaster);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<WCGTMasterDataDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTMasterDataDTO>>();
                return finalResponse;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetWCGTMAsterDataHttpApiQuery-" + response);

            }

        }
        public async Task<List<WCGTLocationDTO>> GetLocationWCGTMAsterHttpApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getLocationWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("getLocationWcgtMaster").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getLocationWcgtMaster);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<WCGTLocationDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTLocationDTO>>();
                return finalResponse;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetLocationWCGTMAsterHttpApiQuery-" + response);

            }

        }

        public async Task<List<WCGTIndustryDTO>> GetIndustryWCGTMAsterHttpApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getIndustryWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("getIndustryWcgtMaster").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getIndustryWcgtMaster);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<WCGTIndustryDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTIndustryDTO>>();
                return finalResponse;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetIndustryWCGTMAsterHttpApiQuery-" + response);

            }

        }
        public async Task<List<GetRateDesignationDTO>> GetRateByDesignation(List<GetRateDesignationRequestDTO> request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getDesignationWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("GetRateByDesignation").Value;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync(baseurl + getDesignationWcgtMaster, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                if (response != null)
                {

                    List<GetRateDesignationDTO> finalResponse = JsonConvert.DeserializeObject<List<GetRateDesignationDTO>>(response);
                    //List<GetRateDesignationDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<GetRateDesignationDTO>>();
                    return finalResponse;
                }
                else
                {
                    return new List<GetRateDesignationDTO> { };
                }

            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetRateByDesignation-" + response);

            }

        }

        public class GetResignedAndAbscondedUsersByEmailsRequest
        {
            public List<string> emails { get; set; }
        }

        public async Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> GetResignedAndAbscondedUsersByEmails(List<string> emails)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string GetResignedAndAbscondedUsersWithLastAvailableDay = _config.GetSection("MicroserviceApiSettings").GetSection("GetResignedAndAbscondedUsersWithLastAvailableDay").Value;

            GetResignedAndAbscondedUsersByEmailsRequest body = new()
            {
                emails = emails
            };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + GetResignedAndAbscondedUsersWithLastAvailableDay, content);
            var response = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                if (response != null)
                {
                    List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> finalResponse = JsonConvert.DeserializeObject<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>>(response);
                    return finalResponse;
                }
                else
                {
                    return new List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> { };
                }
            }
            else
            {
                throw new Exception($"Error fetching GetResignedAndAbscondedUsersByEmails- {response}");

            }
        }

        public async Task<List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion>> GetUserLeaveHolidayResponseForSystemSuggestion(GetUserLeaveHolidayWithUserMasterRequestDTOForSystemSuggestion request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getUserLeaveHolidayResponseForSystemSuggestionUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetUserLeaveHolidayResponseForSystemSuggestion").Value;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync(baseurl + getUserLeaveHolidayResponseForSystemSuggestionUrl, content);
            var response = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion> finalResp = JsonConvert.DeserializeObject<List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion>>(response);
                return finalResp;
            }
            else
            {
                throw new Exception("Error fetching getUserLeaveHolidayResponseForSystemSuggestion URL:" + baseurl + getUserLeaveHolidayResponseForSystemSuggestionUrl + ", Response:" + response);
            }
        }
    }
}
