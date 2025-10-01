using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class WorkflowServiceHttpApi : IWorkflowHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WorkflowServiceHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task TerminateWorkflow(List<TerminateWorkflowDTO> workflowRADGUID, string token, UserInfoDTO userInfo)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("WorkflowBaseUrl").Value;
                var terminateWorkflowUrl = _config.GetSection("MicroserviceApiSettings").GetSection("TerminateWorkflowPath").Value;
                //var objValue = JsonConvert.DeserializeObject(workflowRADGUID);// payload.payload);
                var content = new StringContent(JsonConvert.SerializeObject(workflowRADGUID), Encoding.UTF8, "application/json");
                //context.Items.DownstreamRequest().Headers.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                var tokenSplit = token.Trim().Split(" ");// payload.token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
                //Call workflow service post request to terminate workflows
                var url = baseUrl + terminateWorkflowUrl;

                if (_httpClient.DefaultRequestHeaders.Contains("userinfo") == false)
                {
                    _httpClient.DefaultRequestHeaders.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                }
                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
                    throw new Exception("Error fetching TerminateWorkflow");
                }
            } catch(Exception ex)
            {
                throw new Exception("Error fetching TerminateWorkflow");
            }



        }
       
    }
}
