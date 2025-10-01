using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Gateway.API.ServiceLayerHelper.DTOs;
using Microsoft.Extensions.Configuration;
using Gateway.API.Dtos;
using Gateway.API.Helpers.HttpServices;
using Gateway.API.Services;
using Polly;
using Gateway.API.Helpers.IHttpServices;

namespace Gateway.API.ServiceLayerHelper.WorkflowService
{
    public class WorkflowService : IWorkflowService
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IUserAccessor _userAccessor;
        private readonly IIdentityHttpServices _identityHttpServices;
        //private readonly ILogger _logger;

        public WorkflowService(HttpClient httpClient, IConfiguration config, IUserAccessor userAccessor, IIdentityHttpServices identityHttpServices)
        {
            //_httpClient = httpClient;
            _config = config;
            _userAccessor = userAccessor;
            _identityHttpServices = identityHttpServices;
        }

        public async Task initWorkflow(NotificationPayloadDTO payload)
        {
            //_logger.LogInformation($"Entering init workflow {payload.action}");
            switch (payload.action)
            {
                case Constants.Constants.CreateUserAllocationWorkflowAction:
                    //_logger.LogInformation($"Entering CreateUserAllocationWorkflowAction action {payload.action}");
                    await CreateWorkflow(payload);
                    break;
                case Constants.Constants.UpdateUserAllocationWorkflowAction:
                    await UpdateWorkflow(payload);
                    break;
                case Constants.Constants.CreateUserSkillAssessmentWorkflowAction:
                    await CreateWorkflow(payload);
                    break;

                default:
                    throw new ArgumentException("No action matched");
            }
        }
        public async Task CreateWorkflow(NotificationPayloadDTO payload)
        {
            try
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();

                var baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GatewayBaseUrl").Value;
                var createWorkflowUrl = "workflow/v1";


                var objValue = JsonConvert.DeserializeObject(payload.payload);
                var content = new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
                var tokenSplit = payload.token.Trim().Split(" ");

                HttpClient _httpClient = new();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);

                //Call worflow service post request to create new workflow
                var url = baseUrl + createWorkflowUrl;


                //--------------------------

                var email = _userAccessor.GetEmail();

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(resp);
                    throw new Exception("Error fetching CreateWorkflow" + resp);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error in Create Workflow", ex.Message, ex.StackTrace, ex.InnerException);
                throw;
            }
        }

        public async Task UpdateWorkflow(NotificationPayloadDTO payload)
        {
            //Call worflow service PUT request to update workflow
        }
    }
}
