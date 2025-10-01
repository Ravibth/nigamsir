using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// NotificationScheduler > Not in use anymore
    /// to get the leave for the user from wcgt db 
    /// and get the conflicting allocation for the same dates. chk with rahul 
    /// </summary>
    public class NotificationScheduler
    {
        private readonly ITokenService _tokenService;
        public NotificationScheduler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        //Not in Use Anymore
        //Employee Leave Notification 
        [FunctionName("NotificationScheduler")]
        public async Task RunAsync([TimerTrigger("%NotificationSchedulerTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            //log.LogInformation("Azure Function NotificationScheduler Started");
            //// HttpClient _httpClient = new HttpClient();
            //// var currentToken = await _tokenService.GetToken();
            //// _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
            //// _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            //try
            //{
            //    var _httpClient = await _tokenService.GetCustomHttpClient();
            //    var result = await Execute(_httpClient, log);

            //    log.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            //}
            //catch (Exception ex)
            //{
            //    log.LogError(ex, ex.Message);
            //    throw;
            //}
            //log.LogInformation("Azure Function NotificationScheduler Completed");
        }
        public async Task<List<EmployeeLeavesDTO>> Execute(HttpClient _httpClient, ILogger log)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseurl = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            var GetEmployeeLeavesByCreatedAt = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GET_EMPLOYEE_LEAVES]);
            var GetAllocationInformationByLeaves = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GET_ALLOCATION_INFORMATION_BY_LEAVES]);
            var urlBuilder = new UriBuilder(baseurl + GetEmployeeLeavesByCreatedAt);
            var leaveDto = new LeaveParamsDTO()
            {
                created_at = DateTime.Now.Date
            };
            var content = new StringContent(JsonConvert.SerializeObject(leaveDto), Encoding.UTF8, "application/json");
            try
            {
                var result = await _httpClient.PostAsync(urlBuilder.Uri, content);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var leaves = JsonConvert.DeserializeObject<List<EmployeeLeavesDTO>>(response);
                    if (leaves != null && leaves.Count > 0)
                    {
                        var leaveData = leaves.Select(x => new GetAllocationByEmployeeEmailAndLeaveDTO
                        {
                            EmployeeEmail = x.emp_email,
                            LeaveStartDate = x.leave_start_date.Value.ToDateTime(TimeOnly.MinValue).Date,
                            LeaveEndDate = x.leave_end_date.Value.ToDateTime(TimeOnly.MinValue).Date

                        }).ToList();
                        urlBuilder = new UriBuilder(baseurl + GetAllocationInformationByLeaves);
                        var allocationContent = new StringContent(JsonConvert.SerializeObject(leaveData), Encoding.UTF8, "application/json");
                        var allocationResult = await _httpClient.PostAsync(urlBuilder.Uri, allocationContent);
                        if (allocationResult.IsSuccessStatusCode)
                        {
                            var allocationResponse = await allocationResult.Content.ReadAsStringAsync();
                            var allocationFinalResult = JsonConvert.DeserializeObject<List<AllocationWithLeavesAndResourceRequestorsResponse>>(allocationResponse);

                        }
                    }
                    return leaves;
                }
                else
                {
                    throw new Exception("Fetching Error");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Someting Went Wrong While fetching the leave on the basis of created_at", ex);
            }
            Console.WriteLine("Under Exeution of Notification Scheduler");
        }

    }
}
