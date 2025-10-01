using Gateway.API.Dtos;
using Gateway.API.Helpers.HttpServices;
using Gateway.API.Helpers.IHttpServices;
using Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Gateway.API.Constants;
using static System.Collections.Specialized.BitVector32;

namespace Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper
{
    public class RolesAndPermissionsMiddlewareHelper
    {

        public static List<NotificationPayload> UpdateRolesAndPermissionsCasePayloads(InitNotificationDTO notificationParams)
        {
            //Reuest Body Extraction
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);
            UpdateUserDto requestBody = JsonConvert.DeserializeObject<UpdateUserDto>(requestPayload.body);


            List<string> action = new();

            // 123,124 - UC018 - If a person is deactivated from the Roles & permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
            if (requestBody.status == false)
            {
                action.Add(NotificationTemplateTypes.USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR);
            }
            List<NotificationPayload> notificationItems = new List<NotificationPayload>();
            foreach (var item in action)
            {
                notificationItems.Add(new NotificationPayload
                {
                    action = item,
                    payload = JsonConvert.SerializeObject(notificationParams).ToString(),
                    token = notificationParams.token
                });
            }
            return notificationItems;
        }

        public static List<NotificationPayload> AddUpdateUserRoles(InitNotificationDTO notificationParams)
        {
            var responseBody = JsonConvert.DeserializeObject<RoleUpdatedDTO>(notificationParams.response_payload);
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();

            var notifiactionParamForThisItem = new InitNotificationDTO
            {
                path = notificationParams.path,
                token = notificationParams.token,
                userinfo = notificationParams.userinfo,
                response_payload = notificationParams.response_payload,
                request_payload = notificationParams.request_payload
            };

            if (responseBody.ActionList != null && responseBody.ActionList.Count > 0 && responseBody.Roles != null)
            {
                foreach (var action in responseBody.ActionList)
                {
                    notificaionItems.Add(new NotificationPayload
                    {
                        action = action,
                        payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                        token = notificationParams.token
                    });
                }
            }



            return notificaionItems;

        }



    }
}
