using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Allocation;
using RMT.Scheduler.DTOs.GT360;
using RMT.Scheduler.DTOs.Response;
using RMT.Scheduler.service;
using RMT.Scheduler.service.GT360;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// TimesheetScheduler > not in use anymore
    /// </summary>
    public class TimesheetScheduler
    {
        private readonly IGTTokenService _gtTokenService;
        private readonly IGT360HttpService _gt360HttpService;
        private readonly ITokenService _rmtTokenService;

        public TimesheetScheduler(IGTTokenService gtTokenService, ITokenService rmtTokenService, IGT360HttpService gt360HttpService)
        {
            _gtTokenService = gtTokenService;
            _gt360HttpService = gt360HttpService;
            _rmtTokenService = rmtTokenService;
        }

        //Not in Use Anymore
        [FunctionName("TimesheetScheduler")]
        //public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        public async Task RunAsync([TimerTrigger("%TimesheetSchedulerTriggerTime%")] TimerInfo myTimer, ILogger logger)
        {

            //logger.LogInformation("Azure Function TimesheetScheduler Started");
            //try
            //{

            //    string currentToken = _rmtTokenService.GetToken().Result;

            //    if (!string.IsNullOrEmpty(currentToken))
            //    {
            //        await Execute(logger, currentToken);
            //    }
            //    else
            //    {
            //        logger.LogInformation($"Azure Function TimesheetScheduler Bearer Token is empty");
            //    }

            //    logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);
            //    throw;
            //}
            //logger.LogInformation("Azure Function TimesheetScheduler Completed");
        }

        public async Task Execute(ILogger logger, string rmtBearerToken)
        {

            //Get timesheet data based on date for all emplyee

            //Get all approved allocations for the date 

            DateTime executionDate = DateTime.Now.Date;
            int daysAdjustment = Convert.ToInt16(Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.TimesheetSchedulerDaysAdjustment));

            GetConfirmedPerDayHoursByDateDto dateRangeObj = new GetConfirmedPerDayHoursByDateDto()
            {
                StartDate = executionDate.AddDays(daysAdjustment),
                EndDate = executionDate.AddDays(daysAdjustment),
                GetClientIds = true,
                GetEmployeeIds = true
            };

            //Get client ids for all the projects
            //get empids for all employees
            logger.LogInformation("GetConfirmedPerDayHoursByDate---Start");

            List<EmployeePerDayAllocationDto> perdayEmployeeHours = await GetConfirmedPerDayHoursByDate(dateRangeObj, logger, rmtBearerToken);

            logger.LogInformation("GetConfirmedPerDayHoursByDate---Completed");

            if (perdayEmployeeHours != null && perdayEmployeeHours.Count > 0)
            {
                logger.LogInformation("PostDateToGTTimesheetAPI---Start");
                try
                {
                    await PostDateToGTTimesheetAPI(logger, perdayEmployeeHours);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "PostDateToGTTimesheetAPI---Failed");
                }
                logger.LogInformation("PostDateToGTTimesheetAPI---Completed");

            }

        }

        private async Task PostDateToGTTimesheetAPI(ILogger logger, List<EmployeePerDayAllocationDto> perdayEmployeeHours)
        {
            GT360TokenResponseDto gtToken = await _gtTokenService.GetToken();
            if (gtToken != null && gtToken.statusCode == GT360Constants.TokenServiceSuccessCode)
            {
                logger.LogInformation("Succesfully fetched the Token response");

                logger.LogInformation("PostDateToGTTimesheetAPI Started");

                GT360TimesheetRequestDto timesheetRequest;

                foreach (var item in perdayEmployeeHours)
                {
                    try
                    {
                        //todo
                        //populate the object based on itemAllocation object properties
                        timesheetRequest = new GT360TimesheetRequestDto();

                        timesheetRequest.sClientID = item.ClientId;
                        timesheetRequest.sProjectID = item.JobCode;
                        timesheetRequest.sActivityID = string.Empty;
                        timesheetRequest.sActivityID = string.Empty;
                        timesheetRequest.sMinuts = Convert.ToString(item.AllocatedPerDayHour);
                        timesheetRequest.sMinuts = "0";
                        timesheetRequest.sNarration = string.Empty;
                        timesheetRequest.sDate = item.AllocationDate.ToString("dd-mm-yy");
                        timesheetRequest.sEmpID = item.EmpMID;
                        timesheetRequest.sToken = gtToken.data?.stoken;

                        GT360TimesheetResponseDto timesheetResponse = await _gt360HttpService.PostTimeSheetData(timesheetRequest);

                        if (timesheetResponse != null)
                        {
                            logger.LogInformation("Timesheet API data posted successfully", item);
                        }
                        else
                        {
                            throw new Exception("Timesheet API data posted failed");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message, item);
                    }
                }
                logger.LogInformation("PostDateToGTTimesheetAPI Finished");

            }
            else
            {
                throw new Exception("Not able to fetch the Authentication Token!");
            }

        }

        private async Task<List<EmployeePerDayAllocationDto>> GetConfirmedPerDayHoursByDate(GetConfirmedPerDayHoursByDateDto dateRange, ILogger logger, string rmtBearerToken)
        {
            logger.LogInformation("GetConfirmedPerDayHoursByDate------ Starts");
            HttpClient client = AzureHttpClient.GetAzureHttpClient(true);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rmtBearerToken);
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

            string mpBaseUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL);
            string endpointUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.GetConfirmedPerDayHoursByDate);
            string url = mpBaseUrl + endpointUrl;

            string json = JsonConvert.SerializeObject(dateRange);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation("GetConfirmedPerDayHoursByDate----- Url-{0}", url);

            var response = await client.PostAsync(url, requestData);

            List<EmployeePerDayAllocationDto> responseObj = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("GetConfirmedPerDayHoursByDate----- SuccessReponse-{0},{1}", response.StatusCode, responseStr);

                responseObj = JsonConvert.DeserializeObject<List<EmployeePerDayAllocationDto>>(responseStr);
            }
            else
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("GetConfirmedPerDayHoursByDate----- FailResponse-{0},{1}", response.StatusCode, responseStr);
            }

            logger.LogInformation("GetConfirmedPerDayHoursByDate------ Ends");

            return responseObj;
        }
    }
}
