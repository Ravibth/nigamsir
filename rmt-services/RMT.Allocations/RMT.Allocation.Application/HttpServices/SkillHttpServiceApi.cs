using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class SkillHttpServiceApi : ISkillHttpServiceApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SkillHttpServiceApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<SkillCodeNameDTO>> GetSkillCodeName()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("skillapiUrl").Value;
            string getSkillCodeName = _config.GetSection("MicroserviceApiSettings").GetSection("getskillcodenamepath").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + getSkillCodeName);
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseFound = await apiResponse.Content.ReadAsStringAsync();
                List<SkillCodeNameDTO> finalResponse = JsonConvert.DeserializeObject<List<SkillCodeNameDTO>>(responseFound);
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching GetSkillCodeName");

            }

        }
        public async Task<List<SkillCodeNameDTO>> GetMandatorySkill(string? competency, string? competencyId, string designation)
        {
            if (String.IsNullOrEmpty(competency) && String.IsNullOrEmpty(competencyId))
            {
                throw new Exception("Please provide either competency or copetencyId");
            }

            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("skillapiUrl").Value;
            string getMandatorySkillPath = _config.GetSection("MicroserviceApiSettings").GetSection("getMandatorySkillPath").Value;
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (!String.IsNullOrEmpty(competency))
            {
                queryString.Add("Competency", competency);
            }
            if (!String.IsNullOrEmpty(competencyId))
            {
                queryString.Add("CompetencyId", competencyId);
            }
            queryString.Add("Designation", designation);
            var apiResponse = await _httpClient.GetAsync(baseurl + getMandatorySkillPath + "?" + queryString);
            if (apiResponse.IsSuccessStatusCode)
            {
                List<SkillCodeNameDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<SkillCodeNameDTO>>();
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching GetMandatorySkill");

            }

        }

        public async Task<List<UserSkillDto>> GetApprovedSkill(List<string> email)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("skillapiUrl").Value;
            string getMandatorySkillPath = _config.GetSection("MicroserviceApiSettings").GetSection("getApprovedSkillPath").Value;
            var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getMandatorySkillPath, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseFound = await apiResponse.Content.ReadAsStringAsync();
                List<UserSkillDto> finalResponse = JsonConvert.DeserializeObject<List<UserSkillDto>>(responseFound);
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching GetApprovedSkill");

            }

        }
    }
}
