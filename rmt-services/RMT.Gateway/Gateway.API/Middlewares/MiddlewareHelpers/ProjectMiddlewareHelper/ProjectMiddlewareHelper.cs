using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper
{
    public class ProjectMiddlewareHelper
    {
        public static List<NotificationPayload> JobCodeChangeToNewCode(InitNotificationDTO notificationParams)
        {
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();

            var notifiactionParamForThisItem = new InitNotificationDTO
            {
                path = notificationParams.path,
                token = notificationParams.token,
                userinfo = notificationParams.userinfo,
                response_payload = notificationParams.response_payload,
                request_payload = null


            };

            notificaionItems.Add(new NotificationPayload
            {
                action = "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE",
                payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                token = notificationParams.token
            });

            notificaionItems.Add(new NotificationPayload
            {
                action = "JOB_CODE_UPDATE_TO_NEW_CODE",
                payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                token = notificationParams.token
            });

            return notificaionItems;
        }
        public static List<NotificationPayload> ProjectUpdateNotification(InitNotificationDTO notidicationParams)
        {
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
            var projectResponse = JsonConvert.DeserializeObject<UpdateProjectResponseDTO>(notidicationParams.response_payload);
            if (projectResponse.Actions != null && projectResponse.Actions.Count > 0)
            {
                var notifiactionParamForThisItem = new InitNotificationDTO
                {
                    path = notidicationParams.path,
                    token = notidicationParams.token,
                    userinfo = notidicationParams.userinfo,
                    response_payload = notidicationParams.response_payload,
                    request_payload = notidicationParams.request_payload
                };
                foreach (var action in projectResponse.Actions)
                {
                    notificaionItems.Add(new NotificationPayload
                    {
                        action = action,
                        payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                        token = notidicationParams.token
                    });
                }
            }
            return notificaionItems;
        }
        public static List<NotificationPayload> ProjectActualBudgetOverShoot(InitNotificationDTO notidicationParams)
        {
            List<NotificationPayload> notificationPayloads = new();
            var projectResponse = JsonConvert.DeserializeObject<List<ProjectActualBudgetResponse>>(notidicationParams.response_payload);
            foreach (var item in projectResponse)
            {
                var notifiactionParamForThisItem = new InitNotificationDTO
                {
                    path = notidicationParams.path,
                    token = notidicationParams.token,
                    userinfo = notidicationParams.userinfo,
                    response_payload = notidicationParams.response_payload,
                    request_payload = notidicationParams.request_payload
                };
                notificationPayloads.Add(new NotificationPayload
                {
                    action = item.Action,
                    token = notidicationParams.token,
                    payload = JsonConvert.SerializeObject(notifiactionParamForThisItem)
                });
            }
            return notificationPayloads;
        }
    }
}
