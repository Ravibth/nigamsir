using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.IHttpServices;
using MediatR;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain;

namespace RMT.Projects.Application.HttpServices
{
    public class ResourceAllocationHttpApi : IResourceAllocationHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ResourceAllocationHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<SuspendAllocationResponse>> SuspendAllocationHttpApiQuery(SuspendAllocationCommand request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string getSuspendedAllocationPath = _config.GetSection("MicroserviceApiSettings").GetSection("SuspendAllocationPath").Value;
            var content = new StringContent(JsonConvert.SerializeObject(request.ProjectCode), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getSuspendedAllocationPath, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<SuspendAllocationResponse> finalResponse = JsonConvert.DeserializeObject<List<SuspendAllocationResponse>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching SuspendAllocationHttpApiQuery-" + resp);
            }
        }

        public async Task<List<ProjectAllocatedHoursRatioDto>> GetAllocatedHoursRatioByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string getAllocatedHoursRatioByPipelineCode = _config.GetSection("MicroserviceApiSettings").GetSection("getAllocatedHoursRatioByPipelineCode").Value;

            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodes), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getAllocatedHoursRatioByPipelineCode, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<ProjectAllocatedHoursRatioDto> finalResponse = JsonConvert.DeserializeObject<List<ProjectAllocatedHoursRatioDto>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getAllocatedHoursRatioByPipelineCode + "-" + resp);
            }
        }

        public async Task<List<GetAllActiveRequisitionDTO>> GetAllActiveRequisitionsbyPipelineCodeHttpApiQuery(List<KeyValuePair<string, string>> request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string getAllRequisition = _config.GetSection("MicroserviceApiSettings").GetSection("getAllActiveRequisition").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("PipelineCode", request.Find(m => m.Key.Equals("PipelineCode")).Value);
            query.Add("JobCode", request.Find(m => m.Key.Equals("JobCode")).Value);


            var url = Helper.UrlBuilderByQuery(baseurl + getAllRequisition, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                var finalRes = JsonConvert.DeserializeObject<List<GetAllActiveRequisitionDTO>>(res);
                return finalRes;
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }

        }

        public async Task<List<AllocationDayResourceGroup>> PublishResouceAllocationDayGroupedHttpApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string publishResouceAllocationDayGrouped = _config.GetSection("MicroserviceApiSettings").GetSection("PublishResouceAllocationDayGrouped").Value;
            
            var url = baseurl + publishResouceAllocationDayGrouped;
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                var finalRes = JsonConvert.DeserializeObject<List<AllocationDayResourceGroup>>(res);
                return finalRes;
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }
        }

        public async Task<List<GetAllActiveRequisitionDTO>> GetAllRequisitionByProjectCodeForProjectDetailsQuery(List<KeyValuePair<string, string>> request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string getAllRequisition = _config.GetSection("MicroserviceApiSettings").GetSection("getAllActiveRequisitionForProjectDetails").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("PipelineCode", request.Find(m => m.Key.Equals("PipelineCode")).Value);
            query.Add("JobCode", request.Find(m => m.Key.Equals("JobCode")).Value);


            var url = Helper.UrlBuilderByQuery(baseurl + getAllRequisition, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                var finalRes = JsonConvert.DeserializeObject<List<GetAllActiveRequisitionDTO>>(res);
                return finalRes;
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }


        }

        public async Task<List<ResourceAllocationDetailsResponse>> GetActivePublishedAllocationByPipeLineCode(List<KeyValuePair<string, string>> request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string getAllocation = _config.GetSection("MicroserviceApiSettings").GetSection("GetPublishedActiveAllocationByPipeLineCode").Value;
            GetActivePublishedAllocationByPipeLineCodeRequestDTO req = new GetActivePublishedAllocationByPipeLineCodeRequestDTO
            {
                PipelineCodes = request
            };
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getAllocation, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                var finalRes = JsonConvert.DeserializeObject<List<ResourceAllocationDetailsResponse>>(res);
                return finalRes;
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }
        }

        //public async Task<List<GetActiveAllocationByPipeLineCodeDTO>> GetAllAllocationbyPiplelineCodeHttpApiQuery(string request)
        //{
        //    string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
        //    string getAllAllocation = _config.GetSection("MicroserviceApiSettings").GetSection("GetAllActiveAllocation").Value;
        //    var query = new Dictionary<string, dynamic>();
        //    query.Add("PipelineCode", request);
        //    var url = Helper.UrlBuilderByQuery(baseurl + getAllAllocation, query);
        //    var apiResponse = await _httpClient.GetAsync(url);
        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        var res = await apiResponse.Content.ReadAsStringAsync();
        //        var finalRes = JsonConvert.DeserializeObject<List<GetActiveAllocationByPipeLineCodeDTO>>(res);
        //        return finalRes;
        //    }
        //    else
        //    {
        //        throw new Exception("error in fetching allocation");
        //    }
        //}

        public async Task<Boolean> UpdateAllocationbyPiplelineCodeHttpApiQuery(string pipelineCode, string jobCode, string new_pipelineCode, string new_JobCode, string new_JobName, string updatedBy)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
            string UpdateAllActiveAllocation = _config.GetSection("MicroserviceApiSettings").GetSection("UpdateProjectJobCode").Value;
            var payload = new ChangeCodeDTO();
            payload.pipelineCode = pipelineCode;
            payload.jobCode = jobCode;
            payload.newJobCode = new_JobCode;
            payload.newPipelineCode = new_pipelineCode;
            payload.newJobName = new_JobName;
            payload.modifiedBy = updatedBy;

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + UpdateAllActiveAllocation, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + response);
            }
        }
    }
}
