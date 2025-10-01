using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.PushNotificationService
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;
        private readonly ILogger<ProjectSubscription> _logger;

        public PushNotificationService(HttpClient httpClient, IConfigurationService configurationService, ILogger<ProjectSubscription> logger)
        {
            this._httpClient = AzureHttpClient.GetAzureHttpClient(true);
            this._logger = logger;
        }

        //public async Task initPushNotification(NotificationPayloadDTO payload)
        //{
        //    var notification = new List<PostNewPushNotificationDTO>();
        //    switch (payload.action)
        //    {
        //        case Constants.Constants.CREATE_ALLOCATION_PUSH_NOTIFICATION:
        //            notification = await CREATE_ALLOCATION_PUSH_NOTIFICATION(payload);
        //            //var ab = GetKeyValuePair(payload);

        //            break;


        //        default:
        //            throw new ArgumentException("No action matched");
        //    }
        //    if (notification != null)
        //    {
        //        await PostNewNotification(notification, payload.token);
        //    }
        //    return;
        //}

        //todo Dummy to be removed
        //public List<KeyValuePair<string, string>> GtekeyvaluePairs(NotificationTemplateDTO obj)
        //{
        //    var keys = new string[] { "projectName", "projectCode" };
        //    var dataFields = new List<KeyValuePair<string, string>>();
        //    foreach (var item in keys)
        //    {
        //        var data = Convert.ToString(obj.GetType().GetProperty(item).GetValue(obj, null));
        //        dataFields.Add(new KeyValuePair<string, string>(item, data));
        //    }
        //    return dataFields;
        //}





        //public async Task<List<PostNewPushNotificationDTO>> CREATE_ALLOCATION_PUSH_NOTIFICATION(NotificationPayloadDTO notificationPayloadDTO)
        //{

        //    ///todo check again hardcoded values.
        //    var payload = JsonConvert.DeserializeObject<InitNotificationDTO>(notificationPayloadDTO.payload);
        //    var dataFields = new List<KeyValuePair<string, string>>();
        //    var url = "http://localhost:3000/";

        //    dataFields.Add(new KeyValuePair<string, string>(NotificationTemplatePayloads.PROJECT_NAME, "Project 1"));
        //    dataFields.Add(new KeyValuePair<string, string>(NotificationTemplatePayloads.DATE, DateTime.Now.ToString()));
        //    //dataFields.Add(new KeyValuePair<string, string>(NotificationTemplatePayloads.CLICK_LINK, Helper.GetClickableLink(url, "Click")));

        //    var pushNewNotificationPayload = new List<PostNewPushNotificationDTO>();
        //    var templateFetch = await _configurationService.GetNotificationTemplate(new string[] { NotificationTemplateTypes.ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL }, notificationPayloadDTO.token);
        //    foreach (var item in templateFetch)
        //    {
        //        var modifiedTemplate = _configurationService.TransformMessageTemplateAccordingToPayloads(item.template, item.payload, dataFields);
        //        pushNewNotificationPayload.Add(
        //            new PostNewPushNotificationDTO
        //            {
        //                type = Constants.Constants.CREATE_ALLOCATION_PUSH_NOTIFICATION,
        //                message = modifiedTemplate,
        //                meta = { },
        //                users = new string[] { "", "" },
        //                notification_template_id = item.Id
        //            });
        //    }
        //    return pushNewNotificationPayload;
        //}

        public async Task PostNewPushNotification(List<PostNewPushNotificationDTO> payload, string token)
        {
            _logger.LogInformation("--ServiceBus---Notification---PostNewPushNotification-http");

            foreach (var item in payload)
            {
                var environmentVarible = Environment.GetEnvironmentVariables();
                var baseUrl = Convert.ToString(environmentVarible[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var postNewNotificationService = Convert.ToString(environmentVarible[EnvVariblesAppsetting.POST_NEW_NOTIFICATION_SERVICE]);
                var url = baseUrl + postNewNotificationService;
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
                _logger.LogInformation("--ServiceBus---Notification---PostNewPushNotification->url-{0}-{1}", url, content);
                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("--ServiceBus---PostNewPushNotification---apiResponse--- Success in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);

                }
                else
                {
                    throw new Exception("Error Posting Notification");
                }
            }
        }
    }
}
