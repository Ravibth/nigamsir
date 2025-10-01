using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using System.Text;
using RMT.Scheduler.DTOs.MarketPlace;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.Constants;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// MarkeplaceScheduler > to get expired project from market place and unpublish the same from market place 
    /// and unpublish the same from project db also and send notification for delisting from marketplace
    /// </summary>
    public class MarkeplaceScheduler
    {
        //private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        private readonly IAzureServiceBusService _azureServiceBusService;
        public MarkeplaceScheduler(ITokenService tokenService, IAzureServiceBusService azureServiceBusService)
        {
            _tokenService = tokenService;
            _azureServiceBusService = azureServiceBusService;
        }

        [FunctionName("MarkeplaceScheduler")]
        public async Task Run([TimerTrigger("%MarkeplaceSchedulerTriggerTime%")] TimerInfo myTimer, ILogger logger)
        {

            logger.LogInformation("Azure Function MarkeplaceScheduler Started");
            try
            {
                string currentToken = _tokenService.GetToken().Result;

                if (!string.IsNullOrEmpty(currentToken))
                {
                    await Execute(logger, currentToken);

                }
                else
                {
                    logger.LogInformation($"Azure Function MarkeplaceScheduler Bearer Token is empty");
                }

                logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            }
            catch (Exception ex)
            {
                logger.LogInformation("Azure Function MarkeplaceScheduler Failed");
                logger.LogError(ex, ex.Message);
                throw;
            }

            logger.LogInformation("Azure Function MarkeplaceScheduler Completed");

        }

        private async Task Execute(ILogger logger, string currentToken)
        {
            //Update and get all projects for which expiration dates is passed in marketplace
            DateTime executionDate = DateTime.Now.Date;

            logger.LogInformation($"UpdateExpiredMarketPlaceProjects----- Execution Date {executionDate}");

            UpdateExpiredMarketPlaceProjectsDto expiryDateObj = new UpdateExpiredMarketPlaceProjectsDto()
            {
                ExpiryDate = executionDate,
                DaysAdjustment = Convert.ToInt16(Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.MarkeplaceSchedulerDaysAdjustment))
            };

            List<MarketPlaceProjectDetailResponse> expiredMpProjects = await UpdateExpiredMarketPlaceProjects(expiryDateObj, currentToken, logger);

            if (expiredMpProjects != null && expiredMpProjects.Count > 0)
            {
                logger.LogInformation($"UpdateExpiredMarketPlaceProjects----- Expired project count {expiredMpProjects.Count}");

                List<UpdatePublishedToMarketPlaceDTO> unpublishMPProjectsDto = new List<UpdatePublishedToMarketPlaceDTO>();

                foreach (MarketPlaceProjectDetailResponse mpProject in expiredMpProjects)
                {
                    //process each project by pipelinejob code and update in project service 

                    unpublishMPProjectsDto.Add(new UpdatePublishedToMarketPlaceDTO
                    {
                        IsPublishedToMarketPlace = mpProject.IsPublishedToMarketPlace.Value,
                        PipelineCode = mpProject.PipelineCode,
                        JobCode = mpProject.JobCode
                    });
                }

                if (unpublishMPProjectsDto != null && unpublishMPProjectsDto.Count > 0)
                {
                    //reset published flag to false in project service for all above projects
                    await UnpublishProjectFromMarketPlace(unpublishMPProjectsDto, currentToken, logger);
                }
                foreach (var mpProject in expiredMpProjects)
                {
                    PublishProjectInMarketPlace(expiryDateObj, mpProject, currentToken, logger);
                }
            }
            else
            {
                logger.LogInformation($"UpdateExpiredMarketPlaceProjects----- No expired project found");

            }
        }

        private async Task PublishProjectInMarketPlace(UpdateExpiredMarketPlaceProjectsDto expiryDateObj, MarketPlaceProjectDetailResponse mpProject, string currentToken, ILogger logger)
        {
            InitNotificationDTO notificationDTO = new InitNotificationDTO()
            {
                path = "",
                request_payload = JsonConvert.SerializeObject(expiryDateObj),
                response_payload = JsonConvert.SerializeObject(mpProject),
                token = currentToken,
                userinfo = ""
            };
            NotificationPayload notificationPayload = new NotificationPayload()
            {
                action = Constant.NotificationActions.NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP,
                payload = JsonConvert.SerializeObject(notificationDTO),
                token = currentToken
            };
            await _azureServiceBusService.PublishNotificationOnAzureServiceBus(notificationPayload, Constant.NotificationTypeNotification, logger);
        }

        /// <summary>
        /// Update expired project flag in market place and return affected records
        /// </summary>
        /// <param name="expiryDate"></param>
        /// <param name="bearerToken"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task<List<MarketPlaceProjectDetailResponse>> UpdateExpiredMarketPlaceProjects(UpdateExpiredMarketPlaceProjectsDto expiryDateObj, string bearerToken, ILogger logger)
        {
            logger.LogInformation("UpdateExpiredMarketPlaceProjects------ Starts");
            HttpClient client = AzureHttpClient.GetAzureHttpClient(true);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

            string mpBaseUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL);
            string endpointUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.UpdateExpiredMarketPlaceProjectsUrl);
            string url = mpBaseUrl + endpointUrl;

            string json = JsonConvert.SerializeObject(expiryDateObj);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation("UpdateExpiredMarketPlaceProjects----- Url-{0}, {1}", url, json);

            var response = await client.PostAsync(url, requestData);

            List<MarketPlaceProjectDetailResponse> expiredMarketProjects = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("UpdateExpiredMarketPlaceProjects----- SuccessReponse-{0},{1}", response.StatusCode, responseStr);

                expiredMarketProjects = JsonConvert.DeserializeObject<List<MarketPlaceProjectDetailResponse>>(responseStr);
            }
            else
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("UpdateExpiredMarketPlaceProjects----- FailResponse-{0},{1}", response.StatusCode, responseStr);
            }

            logger.LogInformation("UpdateExpiredMarketPlaceProjects------ Ends");

            return expiredMarketProjects;
        }

        /// <summary>
        /// unpublish project from project db for publish to marketplace flag
        /// </summary>
        /// <param name="unpublishMPProjectsDto"></param>
        /// <param name="bearerToken"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task<List<MarketPlaceProjectDetailResponse>> UnpublishProjectFromMarketPlace(List<UpdatePublishedToMarketPlaceDTO> unpublishMPProjectsDto, string bearerToken, ILogger logger)
        {
            logger.LogInformation("UnpublishProjectFromMarketPlace------ Starts");
            HttpClient client = AzureHttpClient.GetAzureHttpClient(true);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

            string mpBaseUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL);
            string endpointUrl = Environment.GetEnvironmentVariable(Constants.Constant.EnvAppSettingConstants.UnpublishProjectFromMarketPlaceUrl);
            string url = mpBaseUrl + endpointUrl;

            string json = JsonConvert.SerializeObject(unpublishMPProjectsDto);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation("UnpublishProjectFromMarketPlace----- Url-{0}", url);

            var response = await client.PostAsync(url, requestData);

            List<MarketPlaceProjectDetailResponse> unpublishProjects = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("UnpublishProjectFromMarketPlace----- SuccessReponse-{0},{1}", response.StatusCode, responseStr);

                unpublishProjects = JsonConvert.DeserializeObject<List<MarketPlaceProjectDetailResponse>>(responseStr);
            }
            else
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                logger.LogInformation("UnpublishProjectFromMarketPlace----- FailResponse-{0},{1}", response.StatusCode, responseStr);

            }

            logger.LogInformation("UnpublishProjectFromMarketPlace------ Ends");

            return unpublishProjects;
        }

    }
}
