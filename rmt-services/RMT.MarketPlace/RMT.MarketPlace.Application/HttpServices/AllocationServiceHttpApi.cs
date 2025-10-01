using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using RMT.MarketPlace.Application.HttpServices.DTOs;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Responses;

namespace RMT.MarketPlace.Application.HttpServices
{
    public class AllocationServiceHttpApi : IAllocationServiceHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AllocationServiceHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<ResourceAllocationResponse>> GetAllocationsByEmail(string empEmailId)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("AllocationMicroserviceBaseApiUrl").Value;
            string endpointUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetAllocationsByEmail").Value;

            var urlBuilder = new UriBuilder(baseurl + endpointUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);

            query["EmpEmail"] = empEmailId;

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Split(" ")[1]);

            var apiResponse = await _httpClient.GetAsync(url);

            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ResourceAllocationResponse>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching allocation");
            }
        }

        public async Task<List<GetAllRequisitionByProjectCodeResponse>> GetAllRequisitionByProjectCode(string? PipelineCode, string? JobCode, int? limit, int? pagination, string? currentEmp, bool? ScoreCalculationForRequisitionIdsAllowed, bool? IsRequsitionFilterByProjectRoles)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("AllocationMicroserviceBaseApiUrl").Value;
            string endpointUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetAllRequisitionByProjectCode").Value;
            /*var queriess = new Dictionary<string, dynamic>();

            queriess.Add("ProjectCode", ProjectCode);
            queriess.Add("limit", limit);
            queriess.Add("pagination", pagination);
            queriess.Add("currentEmp", currentEmp);
            queriess.Add("ScoreCalculationForRequisitionIdsAllowed", ScoreCalculationForRequisitionIdsAllowed);
            var url = Helper.UrlBuilderByQuery(baseurl + endpointUrl, queriess);*/
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Split(" ")[1]);

            var urlBuilder = new UriBuilder(baseurl + endpointUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);

            query["PipelineCode"] = PipelineCode;
            query["JobCode"] = string.IsNullOrEmpty(JobCode) ? "undefined" : JobCode;
            query["limit"] = limit.ToString();
            query["pagination"] = pagination.ToString();
            query["currentEmp"] = currentEmp;
            query["ScoreCalculationForRequisitionIdsAllowed"] = ScoreCalculationForRequisitionIdsAllowed.ToString();
            query["IsRequsitionFilterByProjectRoles"] = IsRequsitionFilterByProjectRoles.ToString();

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();

            var apiResponse = await _httpClient.GetAsync(url);

            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                var finalResponse = JsonConvert.DeserializeObject<List<GetAllRequisitionByProjectCodeResponse>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching Score {resp}");
            }
        }
    }
}
