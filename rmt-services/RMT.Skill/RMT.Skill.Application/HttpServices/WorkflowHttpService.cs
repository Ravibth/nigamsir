using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.HttpServices
{
    public class WorkflowHttpService : IWorkflowHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public WorkflowHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<WorkflowDTO>> GetWorkflowDetailsByItemId(List<Guid> item_id)
        {
            string workflowBaseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WorkflowBaseUrl").Value;
            string getMultipleTasksByByQueryWorkflowPath = _config.GetSection("MicroserviceApiSettings").GetSection("getMultipleTasksByByQueryWorkflow").Value;

            Dictionary<string, dynamic> queries = new Dictionary<string, dynamic>();
            queries.Add("item_id", item_id.Select(m => m.ToString()).ToList());
            var url = Helper.UrlBuilderByQuery(workflowBaseurl + getMultipleTasksByByQueryWorkflowPath, queries);
            var apiResponse = await _httpClient.GetAsync(url);
            var resp = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode)
            {
                List<WorkflowDTO> finalResponse = JsonConvert.DeserializeObject<List<WorkflowDTO>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching GetWorkflowDetailsByItemId" + resp);
            }

        }
    }
}
