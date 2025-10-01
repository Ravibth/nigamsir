using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// WorkflowScheduler > To Auto approve or terminate the workflow tasks based on due date and config no of days.
    /// </summary>
    public class WorkflowScheduler
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;

        public WorkflowScheduler(ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = AzureHttpClient.GetAzureHttpClient(true);
        }

        [FunctionName("WorkflowScheduler")]
        public async Task Run([TimerTrigger("%WorkflowSchedulerTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("Azure Function WorkflowScheduler Started");
            try
            {
                //httpClient /update /bearer token
                //var token = new TokenService();
                //var currentToken = await token.GetToken();
                var currentToken = await _tokenService.GetToken();

                if (!string.IsNullOrEmpty(currentToken))
                {
                    log.LogInformation($"WorkflowSchedulerTriggerTime--Token created successfully");
                    //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer ${currentToken}");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                    _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                    try
                    {
                        log.LogInformation($"WorkflowSchedulerTriggerTime Timer trigger function Started at: {DateTime.Now} , Token : ${currentToken}");
                        var allWorkflowString = await ExecuteTask(_httpClient, log);
                        log.LogInformation($"WorkflowSchedulerTriggerTime Timer trigger function Finished at: {DateTime.Now} , Token : ${currentToken} , Data : ${JsonConvert.DeserializeObject(allWorkflowString)}");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Went wrong ", ex);
                    }
                }
                else
                {
                    log.LogInformation($"WorkflowSchedulerTriggerTime--token creation failed--Token : ${currentToken}");
                }
                log.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

                log.LogInformation("Azure Function Completed");
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                throw;
            }

            log.LogInformation("Azure Function WorkflowScheduler Completed");
        }

        private UpdateWorkflowDto getUpdatedTask(WorkflowTaskRequest workflowTask)
        {
            var updatedWorkFlowTask = new UpdateWorkflowDto()
            {
                workflow_id = workflowTask.workflow_id,
                item_id = workflowTask.workflow.item_id,
                workflow_task_id = workflowTask.id,
                assigned_to = "system@system.com",
                status = Constant.APPROVED,
                remarks = "auto approved by the system",
                workflow_task_title = workflowTask.title
            };
            return updatedWorkFlowTask;
        }

        private async Task<string> ExecuteTask(HttpClient _httpClient, ILogger log)
        {
            //todo change path from appsettings
            log.LogInformation($"--WorkflowScheduler--ExecuteTask--Start");

            var environmentVariables = Environment.GetEnvironmentVariables();
            var gatewayBaseUrl = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            var getTaskWorkflowsUrl = gatewayBaseUrl + Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GET_TASK_WORKFLOWS_URL]);
            var updateWorkflowTaskUrl = gatewayBaseUrl + Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.UPDATE_WORKFLOW_TASK_URL]);
            var bulkUpdateWorkflowTaskUrl = gatewayBaseUrl + Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.BULK_UPDATE_WORKFLOW_TASK_URL]);
            var workflowTermination = gatewayBaseUrl + Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.WORKFLOW_TERMINATION_URL]);

            log.LogInformation($"--WorkflowScheduler--ExecuteTask--workflowTermination--Start--gatewayBaseUrl-{gatewayBaseUrl}-getTaskWorkflowsUrl-{getTaskWorkflowsUrl}-updateWorkflowTaskUrl-{updateWorkflowTaskUrl}-workflowTermination-{workflowTermination}");

            //StringContent terminationContent = new StringContent(null, Encoding.UTF8, "application/json");

            log.LogInformation($"--WorkflowScheduler--ExecuteTask--workflowTermination-URL--{workflowTermination}");

            var termiationResult = await _httpClient.PostAsync(workflowTermination, null);
            if (termiationResult.IsSuccessStatusCode)
            {
                var worflowTasksString = await termiationResult.Content.ReadAsStringAsync();
                log.LogInformation($"--WorkflowScheduler--ExecuteTask--workflowTermination--{termiationResult.StatusCode}--{worflowTasksString}");
            }
            else
            {
                var terminationResponse = await termiationResult.Content.ReadAsStringAsync();
                throw new Exception($"--WorkflowScheduler--ExecuteTask--workflowTermination--{termiationResult.StatusCode}--{terminationResponse}");
            }
            var outcome = "inprogress";
            var taskStatus = "pending";

            log.LogInformation($"--WorkflowScheduler--ExecuteTask--workflowTermination--end");
            log.LogInformation($"--------------------------------------------------------");

            log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--Start");

            var current_date = DateOnly.FromDateTime(DateTime.Now.Date);

            var current_formatted_date = current_date.ToString("yyyy-MM-dd");

            getTaskWorkflowsUrl = getTaskWorkflowsUrl + $"&due_date={current_formatted_date}";

            log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--Url: {getTaskWorkflowsUrl}");

            var response = await _httpClient.GetAsync(getTaskWorkflowsUrl);
            //foreach (var item in response) { }
            //Get All Pending task based on due date
            //Foreach {
            //Process each task and approve restecive task wi th approve payload
            //}
            if (response.IsSuccessStatusCode)
            {
                var worflowTasksString = await response.Content.ReadAsStringAsync();
                log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--{response.StatusCode}--{worflowTasksString}");

                var workflowTasks = JsonConvert.DeserializeObject<List<WorkflowTaskRequest>>(worflowTasksString);
                List<UpdateWorkflowDto> finalTaskToUpdate = new List<UpdateWorkflowDto>();
                if (workflowTasks != null && workflowTasks.Count > 0)
                {
                    int count = 0;
                    foreach (var task in workflowTasks)
                    {
                        count++;
                        if (count >= 100)
                        {
                            break;
                        }
                        if (task.status.Trim().ToLower() == Constant.Taskstatus.Trim().ToLower()
                            && task.workflow.outcome.Trim().ToLower() == Constant.InprogressOutcome.ToLower().Trim()
                            && task.due_date != null)
                        {
                            log.LogInformation($"--WorkflowScheduler--If--Status--{task.status}--Outcome--{task.workflow.outcome}--due_date--{task.due_date}");

                            log.LogInformation($"{task.workflow.name} got the due date :- {DateTime.Parse(task.due_date).ToUniversalTime()} and the current datetime :- {DateTime.Now.ToUniversalTime()}");

                            var due_date = DateOnly.FromDateTime(DateTime.Parse(task.due_date).Date);
                            if (due_date < current_date)
                            {
                                log.LogInformation($"--WorkflowScheduler--duedate less then currntdate--{task.status}--Title--{task.title.ToLower().Trim()}");
                                // int index = Constant.WF_TASK_TITLE.IndexOf(task.title.ToLower().Trim());
                                int index = Constant.WF_TASK_TITLE.FindIndex(x => x.Equals(task.title.ToLower(), StringComparison.OrdinalIgnoreCase));
                                log.LogInformation($"--WorkflowScheduler--duedate less then currntdate-- index -- {index}--Title--{task.title}");
                                if (index > -1)
                                {
                                    var updatedWorkflowTask = getUpdatedTask(task);
                                    finalTaskToUpdate.Add(updatedWorkflowTask);
                                }                               
                            }
                            else
                            {
                                log.LogInformation($"--WorkflowScheduler--duedate greater then currntdate--{task.status}--Title--{task.title.ToLower().Trim()}");
                            }
                        }
                        //else
                        //{
                        //    log.LogInformation($"--WorkflowScheduler--Else--Status--{task.status}--Outcome--{task.workflow.outcome}--Outcome--{task.due_date}");
                        //}
                    }
                }
                else
                {
                    log.LogInformation($"--WorkflowScheduler--ExecuteTask--workflowTasks count is null or zero");
                }
                if (finalTaskToUpdate.Count > 0)
                {
                    log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--finalTaskToUpdate.Count--{finalTaskToUpdate.Count}");
                    string updatedWorkflowTaskString = JsonConvert.SerializeObject(finalTaskToUpdate);
                    StringContent content = new StringContent(updatedWorkflowTaskString, Encoding.UTF8, "application/json");
                    try
                    {
                        log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--bulkUpdateWorkflowTaskUrl--{bulkUpdateWorkflowTaskUrl}");
                        HttpResponseMessage updatedResp = await _httpClient.PostAsync(new Uri(bulkUpdateWorkflowTaskUrl), content);
                        if (updatedResp.IsSuccessStatusCode)
                        {
                            var resp = await updatedResp.Content.ReadAsStringAsync();
                            log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--bulkUpdateWorkflowTaskUrl--Success--{response.StatusCode}--{resp}");
                        }
                        else
                        {
                            var resp = await updatedResp.Content.ReadAsStringAsync();
                            log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--bulkUpdateWorkflowTaskUrl--Error--{response.StatusCode}--{resp}");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--bulkUpdateWorkflowTaskUrl--Exception--{ex}--StackTrace--{ex.StackTrace}");
                        throw new Exception("Someting Went Wrong While Updating Task ", ex);
                    }
                }
                else
                {
                    log.LogInformation($"--WorkflowScheduler--ExecuteTask--finalTaskToUpdate is zero");
                }

                log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--Complete");

                log.LogInformation($"--WorkflowScheduler--ExecuteTask--End--Sucess");

                return worflowTasksString;

            }
            else
            {
                var worflowTasksStringResponse = await response.Content.ReadAsStringAsync();
                log.LogInformation($"--WorkflowScheduler--ExecuteTask--AutoApprove--{response.StatusCode}--{worflowTasksStringResponse}");
                log.LogInformation($"--WorkflowScheduler--ExecuteTask--End--Exception");

                throw new Exception("Something weng wrong");
            }

        }
    }
}
