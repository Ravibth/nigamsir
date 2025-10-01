using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices
{
    public class WorkflowHttpService: IWorkflowHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public WorkflowHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task RefreshAssignedTask(RefreshAssignedWorkflowTask request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WorkflowServiceBaseUrl").Value;
            string refreshWorkflowTask = _config.GetSection("MicroserviceApiSettings").GetSection("RefreshWorkflowTaskAssignment").Value;
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + refreshWorkflowTask, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }
        }
        public async Task TerminateWorkflowByPipelineCodeAndJobCode(TerminateWorkflowByPipelineCodeAndJobCodeRequestDTO request)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WorkflowServiceBaseUrl").Value;
            string terminateWorkflowByPipelineCodeAndJobCode = _config.GetSection("MicroserviceApiSettings").GetSection("TerminateWorkflowByPipelineCodeAndJobCode").Value;
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + terminateWorkflowByPipelineCodeAndJobCode, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
            }
            else
            {
                var res = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("error in fetching requisition-" + res);
            }
        }
    }
}
