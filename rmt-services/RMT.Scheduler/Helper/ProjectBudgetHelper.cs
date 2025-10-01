using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using RMT.Scheduler.service.Configurations.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.Configurations;
using RMT.Scheduler.service.Project;
using RMT.Scheduler.Constants;
using RMT.Scheduler.service.WCGT;

namespace RMT.Scheduler.Helper
{
    public class ProjectBudgetHelper
    {
        //private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        private readonly IConfigurationService _configurationService;
        private readonly IAzureServiceBusService _azureServiceBusService;
        private readonly IWCGTHttpService _wcgtService;

        public ProjectBudgetHelper(ITokenService tokenService, IConfigurationService configurationService, IWCGTHttpService wcgtService, IAzureServiceBusService azureServiceBusService, ILogger logger)
        {
            //_logger = loggerFactory.CreateLogger<BudgetSchedularFunction>();
            _tokenService = tokenService;
            _configurationService = configurationService;
            _wcgtService = wcgtService;
            _azureServiceBusService = azureServiceBusService;
            //_logger = logger;
        }

        public async Task ProcessProjectForBudget(ILogger _logger, int updateBudgetBatchSize, string currentToken, List<ProjectDTO> allProjects, bool sendNotification)
        {
            if (allProjects != null)
            {
                _logger.LogInformation("--BudgetSchedular--Total Projects to Process--Count-{0}", allProjects.Count);

                List<UpdateBudgetStatusDTO> updateRequest = new List<UpdateBudgetStatusDTO>();
                List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
                Dictionary<string, List<ProjectConfiguration>> allWarningAllocationConfig = new Dictionary<string, List<ProjectConfiguration>>();
                Dictionary<string, List<ProjectConfiguration>> allAlertAllocationVsBudgetConfig = new Dictionary<string, List<ProjectConfiguration>>();

                foreach (ProjectDTO project in allProjects)
                {
                    try
                    {
                        _logger.LogInformation("--BudgetSchedular--Project--Loop--PipelineCode-{0}-JobCode-{1}--Started", project.PipelineCode, project.JobCode);

                        string jobCode = project.JobCode != null ? project.JobCode : null;
                        string offerings = project.Offerings != null ? project.Offerings : null;
                        string buName = project.bu != null ? project.bu : null;
                        string buOfferingKey = $"{buName}{Constant.SeparatorPipe}{offerings}";
                        List<KeyValuePair<string, string>> projectCodeKey = new List<KeyValuePair<string, string>>();
                        projectCodeKey.Add(new KeyValuePair<string, string>(project.PipelineCode, project.JobCode != null ? project.JobCode : ""));

                        if (jobCode != null && project.EndDate != null && project.StartDate != null)
                        {
                            BudgetOverviewRequest request = new BudgetOverviewRequest
                            {
                                JobCodes = projectCodeKey,
                                StartDate = project.StartDate != null ? (DateTime)project.StartDate : null,
                                EndDate = project.EndDate != null ? (DateTime)project.EndDate : null
                            };
                            _logger.LogInformation("--BudgetSchedular--GetProjectBudgetStatus--Request--{0}", JsonConvert.SerializeObject(request));

                            var budget = await GetProjectBudgetStatus(currentToken, request, _logger);

                            _logger.LogInformation("--BudgetSchedular--GetProjectBudgetStatus--Response--{0}", JsonConvert.SerializeObject(budget));

                            List<ProjectConfiguration> warningConfig = null;
                            List<ProjectConfiguration> alertAllocationConfig = null;

                            if (!allWarningAllocationConfig.ContainsKey(buOfferingKey))
                            {
                                warningConfig = await _configurationService.GetConfigurationByConfigGroupType(buOfferingKey, Constant.ConfigGroupExpertise.WARNING_ALERT_CONDITION, currentToken, _logger);
                                //_logger.LogInformation("--BudgetSchedular--GetConfigurationByConfigGroupType--Response--{0}", JsonConvert.SerializeObject(warningConfig));
                                _logger.LogInformation("--BudgetSchedular--GetConfigurationByConfigGroupType--Request--{0}--{1}--Response--{2}",
                                    buOfferingKey, Constant.ConfigGroupExpertise.WARNING_ALERT_CONDITION, JsonConvert.SerializeObject(warningConfig));
                                allWarningAllocationConfig.Add(buOfferingKey, warningConfig);
                            }
                            else
                            {
                                warningConfig = allWarningAllocationConfig[buOfferingKey];
                            }

                            _logger.LogInformation("--BudgetSchedular--ConfigurationObject--BuOfferingKey--{0}--Value--{1}", buOfferingKey, JsonConvert.SerializeObject(warningConfig));

                            if (!allAlertAllocationVsBudgetConfig.ContainsKey(buOfferingKey))
                            {
                                alertAllocationConfig = await _configurationService.GetConfigurationByConfigGroupType(buOfferingKey, Constant.ConfigGroupExpertise.BUDGET_ALERT_CONDITION_FOR_ALLOCATION, currentToken, _logger);
                                _logger.LogInformation("--BudgetSchedular--GetConfigurationByConfigGroupType--Request--{0}--{1}--Response--{2}",
                                    buOfferingKey, Constant.ConfigGroupExpertise.BUDGET_ALERT_CONDITION_FOR_ALLOCATION, JsonConvert.SerializeObject(alertAllocationConfig));
                                allAlertAllocationVsBudgetConfig.Add(buOfferingKey, alertAllocationConfig);
                            }
                            else
                            {
                                alertAllocationConfig = allAlertAllocationVsBudgetConfig[buOfferingKey];
                            }

                            _logger.LogInformation("--BudgetSchedular--ConfigurationObject--BuOfferingKey--{0}--Value--{1}", buOfferingKey, JsonConvert.SerializeObject(alertAllocationConfig));


                            if (budget != null && budget.Count > 0)
                            {
                                Double allocationVsBudgetPercent = 0;
                                if (budget != null && budget.Count > 0)
                                {
                                    if (budget[0].TotalAllocatedCost > 0 && budget[0].BudgetedCost > 0)
                                    {
                                        allocationVsBudgetPercent = (budget[0].TotalAllocatedCost / budget[0].BudgetedCost) * 100;
                                    }
                                }
                                string BudgetStatus = allocationVsBudgetPercent < 100 ? "In-Budget" : "Out-Budget";

                                _logger.LogInformation("--BudgetSchedular--AllocationVsBudgetPercent--Value--{0}--BudgetStatus--{1}", allocationVsBudgetPercent, BudgetStatus);

                                if (warningConfig != null)
                                {
                                    var budgetWarningConfig = warningConfig.FirstOrDefault();
                                    double warningLimit = 0;
                                    Double.TryParse(budgetWarningConfig.AttributeValue, out warningLimit);

                                    if (allocationVsBudgetPercent > warningLimit && allocationVsBudgetPercent < 100)
                                    {
                                        BudgetStatus = "Reaching your Budget threshold";
                                    }

                                    if (alertAllocationConfig != null)
                                    {
                                        var budgetAlertConfig = alertAllocationConfig.FirstOrDefault();

                                        double alertNotificationLimit = 0;
                                        Double.TryParse(budgetAlertConfig.AttributeValue, out alertNotificationLimit);

                                        Boolean isOutLimit = allocationVsBudgetPercent > alertNotificationLimit;
                                        if (isOutLimit && alertNotificationLimit > 0)
                                        {
                                            // Send Notification
                                            BudgetNotifcationResponse budgetNotifcationResponse = new BudgetNotifcationResponse
                                            {
                                                JobCode = jobCode,
                                                PipelineCode = project.PipelineCode != null ? project.PipelineCode : null,
                                                Limit = alertNotificationLimit,
                                            };
                                            InitNotificationDTO initNotificationDTO = new InitNotificationDTO
                                            {
                                                path = "",
                                                request_payload = "",
                                                response_payload = JsonConvert.SerializeObject(budgetNotifcationResponse),
                                                token = currentToken,
                                                userinfo = ""
                                            };
                                            //Here we made what to send to the service layer
                                            notificaionItems.Add(new NotificationPayload
                                            {
                                                action = Constant.PROJECT_BUDGET_NOTIFICATION,
                                                payload = JsonConvert.SerializeObject(initNotificationDTO),
                                                token = currentToken
                                            });
                                            _logger.LogInformation("--BudgetSchedular--Notificaion--OutLimit-Notification-Sent--isOutLimit-{0} Response-{1}", isOutLimit, JsonConvert.SerializeObject(budgetNotifcationResponse));

                                        }
                                        else
                                        {
                                            _logger.LogInformation("--BudgetSchedular--Notificaion--OutLimit-Notification-NotSent--isOutLimit-{0}", isOutLimit);
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogInformation("--BudgetSchedular--ConfigurationObject--alertAllocationConfig Value is null");
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation("--BudgetSchedular--ConfigurationObject--warningConfig Value is null");
                                }

                                updateRequest.Add(new UpdateBudgetStatusDTO
                                {
                                    PipelineCode = project.PipelineCode != null ? project.PipelineCode : null,
                                    JobCode = project.JobCode != null ? project.JobCode : null,
                                    BudgetStatus = BudgetStatus
                                });
                            }
                            else
                            {
                                _logger.LogInformation("--BudgetSchedular--GetProjectBudgetStatus--Budget not found!");
                            }
                        }

                        _logger.LogInformation("--BudgetSchedular--Project--Loop--PipelineCode-{0}-JobCode-{1}--Finished", project.PipelineCode, project.JobCode);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("--BudgetSchedular--Project--Loop--PipelineCode-{0}-JobCode-{1}--Exception-Message-{2},StackTrace-{3}", project.PipelineCode, project.JobCode, ex.Message, ex.StackTrace);
                    }
                }

                if (updateRequest != null && updateRequest.Count > 0)
                {
                    _logger.LogInformation("--BudgetSchedular--UpdateProjectBudgetByBatch--TotalCount--{0}", updateRequest.Count);
                    await UpdateProjectBudgetByBatch(currentToken, updateRequest, updateBudgetBatchSize, _logger);
                }
                if (sendNotification == true && notificaionItems != null && notificaionItems.Count > 0)
                {
                    _logger.LogInformation("--BudgetSchedular--publishNotification--TotalCount--{0}", notificaionItems.Count);
                    await publishNotification(notificaionItems, _logger);
                }
            }
            else
            {
                _logger.LogInformation("--BudgetSchedular--Total Projects response is null");
            }
        }

        public async Task UpdateProjectBudgetByBatch(string currentToken, List<UpdateBudgetStatusDTO> updateRequest, int batchSize, ILogger logger)
        {
            foreach (var batch in GetBatches(updateRequest, batchSize))
            {
                try
                {
                    var response = await UpdateProjectBudget(currentToken, batch, logger);
                }
                catch (Exception ex)
                {
                    logger.LogInformation("--BudgetSchedular--UpdateProjectBudgetByBatch--Exception-{0}--StackTrace-{1}", ex.Message, ex.StackTrace);
                }
            }
        }

        public static IEnumerable<List<T>> GetBatches<T>(List<T> source, int batchSize)
        {
            for (int i = 0; i < source.Count; i += batchSize)
            {
                yield return source.GetRange(i, Math.Min(batchSize, source.Count - i));
            }
        }

        public async Task<List<ProjectDTO>> GetAllProjectList(string currentToken, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("Project"));
                var environmentVaribles = Environment.GetEnvironmentVariables();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var getAllPath = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GetAllProjectsForBudget]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var path = gateway + getAllPath;
                var response = await client.GetAsync(string.Format(path));
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetAllProjectList--GetAllProjectsForBudget--Response Success-{0}", result);
                    List<ProjectDTO> projectList = JsonConvert.DeserializeObject<List<ProjectDTO>>(result);
                    return projectList;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetAllProjectList--GetAllProjectsForBudget--Response Failed-{0}", result);
                    return new();
                }
            }
        }

        public async Task<List<ProjectDTO>> GetAllProjectsForBudgetByJobCodes(List<string> jobCode, string currentToken, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("Project"));
                var environmentVaribles = Environment.GetEnvironmentVariables();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var getAllPath = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GetAllProjectsForBudgetByJobCodes]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var path = gateway + getAllPath;

                jobCode = jobCode?.Where(a => !string.IsNullOrEmpty(a)).Distinct().ToList();

                var content = new StringContent(JsonConvert.SerializeObject(jobCode), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(string.Format(path), content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetAllProjectList--GetAllProjectsForBudgetByJobCodes--Response Success-{0}", result);
                    List<ProjectDTO> projectList = JsonConvert.DeserializeObject<List<ProjectDTO>>(result);
                    return projectList;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetAllProjectList--GetAllProjectsForBudgetByJobCodes--Response Failed-{0}", result);
                    return new();
                }
            }
        }


        public static async Task<List<BudgetOverViewDto>> GetProjectBudgetStatus(string currentToken, BudgetOverviewRequest request, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("Allocation"));
                var environmentVaribles = Environment.GetEnvironmentVariables();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                string json = JsonConvert.SerializeObject(request);
                var requestData = new StringContent(json, Encoding.UTF8, "application/json");
                //todo change path from appsettings
                var BudgetOverviewPath = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.BUDGET_OVERVIEW]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var response = await client.PostAsync(string.Format(gateway + BudgetOverviewPath), requestData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetProjectBudgetStatus--BUDGET_OVERVIEW--Response Success-{0}", result);
                    List<BudgetOverViewDto> projectList = JsonConvert.DeserializeObject<List<BudgetOverViewDto>>(result);
                    return projectList;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetProjectBudgetStatus--BUDGET_OVERVIEW--Response Failed-{0}", result);
                    return new();
                }
            }
        }

        public static async Task<List<ProjectDTO>> UpdateProjectBudget(string currentToken, List<UpdateBudgetStatusDTO> updateBudgetRequest, ILogger logger)
        {
            try
            {
                using (var client = AzureHttpClient.GetAzureHttpClient(true))
                {
                    Console.WriteLine(Environment.GetEnvironmentVariable("Project"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                    client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                    string json = JsonConvert.SerializeObject(updateBudgetRequest);
                    var requestData = new StringContent(json, Encoding.UTF8, "application/json");
                    var environmentVaribles = Environment.GetEnvironmentVariables();
                    var UpdateProjectBudgetStatusPath = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.UPDATEPROJECT_BUDGET]);
                    var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);

                    logger.LogInformation("--BudgetSchedular--UpdateProjectBudget--Count--{0}--Request-{1}", updateBudgetRequest.Count, json);

                    //todo change path from appsettings
                    var response = await client.PostAsync(string.Format(gateway + UpdateProjectBudgetStatusPath), requestData);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        List<ProjectDTO> projectList = JsonConvert.DeserializeObject<List<ProjectDTO>>(result);
                        logger.LogInformation($"--BudgetSchedular--UpdateProjectBudget--StatusCode- {response.StatusCode}, result- {result}");
                        return projectList;
                    }
                    else
                    {
                        throw new Exception($"Error in UpdateProjectBudget :-StatusCode- {response.StatusCode}, result- {result}");
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("--BudgetSchedular--UpdateProjectBudget--Exception-{0}", ex.Message);
                throw;
            }
        }

        public async Task<String> GetProjectActualBudgetOverShoot(string currentToken, ILogger logger)
        {
            try
            {
                var _httpClient = await _tokenService.GetCustomHttpClient();


                var EnvVarDictionary = Environment.GetEnvironmentVariables();
                var GetGatewayBaseUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var ProjectActualBudgetOverShoot = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.PROJECT_ACTUAL_BUDGET_OVERSHOOT]);
                var url = $"{GetGatewayBaseUrl}{ProjectActualBudgetOverShoot}";


                //todo change path from appsettings
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"--BudgetSchedular--GetProjectActualBudgetOverShoot--StatusCode- {response.StatusCode}, result- {result}");
                    return result;
                }
                else
                {
                    throw new Exception($"Error in GetProjectActualBudgetOverShoot :-StatusCode- {response.StatusCode}, result- {result}");
                }

            }
            catch (Exception ex)
            {
                logger.LogInformation("--BudgetSchedular--GetProjectActualBudgetOverShoot--Exception-{0}", ex.Message);
                throw;
            }
        }

        public async Task publishNotification(List<NotificationPayload> payload, ILogger logger)
        {
            string type = Constant.NotificationTypeNotification;
            foreach (var item in payload)
            {
                await _azureServiceBusService.PublishNotificationOnAzureServiceBus(item, type, logger);
            }
        }
    }
}
