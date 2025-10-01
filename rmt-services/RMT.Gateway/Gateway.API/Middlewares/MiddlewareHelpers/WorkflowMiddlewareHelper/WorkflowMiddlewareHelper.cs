using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper
{
    public class WorkflowMiddlewareHelper
    {
        public static List<NotificationPayload> NewWorkflowCreated(InitNotificationDTO notificationParams)
        {
            //Reuest Body Extraction
            //var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);
            //WorkflowCreateRequestDTO requestBody = JsonConvert.DeserializeObject<WorkflowCreateRequestDTO>(requestPayload.body);
            WorkflowDTO responseBody = JsonConvert.DeserializeObject<WorkflowDTO>(notificationParams.response_payload);
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();

            if (!responseBody.actions.IsNullOrEmpty())
            {
                foreach (var action in responseBody.actions)
                {
                    //Here we made what to send to the service layer
                    notificaionItems.Add(new NotificationPayload
                    {
                        action = action,
                        payload = JsonConvert.SerializeObject(notificationParams).ToString(),
                        token = notificationParams.token
                    });
                }
            }
            //switch (responseBody.module)
            //{
            //    //New User Allocation Workflow
            //    case Constants.WorkflowConstants.WORKFLOW_MODULE_EMPLOYEE_ALLOCATION:
            //        return WORKFLOW_MODULE_EMPLOYEE_ALLOCATION(responseBody);
            //        break;
            //    default:
            //        break;
            //}

            return notificaionItems;
        }

        public static List<NotificationPayload> BulkApprovalUpdated(InitNotificationDTO notificationParams)
        {
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
            List<BulkWorkflowApprovalResponselDTO> responseBody = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalResponselDTO>>(notificationParams.response_payload);
            //List<BulkWorkflowApprovalRequestDTO> requestBody = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalRequestDTO>>(notificationParams.response_payload);
            foreach (var item in responseBody)
            {
                if (!item.actions.IsNullOrEmpty() && !item.isError)
                {
                    var notifiactionParamForThisItem = new InitNotificationDTO
                    {
                        path = notificationParams.path,
                        token = notificationParams.token,
                        userinfo = notificationParams.userinfo,
                        response_payload = JsonConvert.SerializeObject(item),
                        request_payload = JsonConvert.SerializeObject(item.requestPayload)
                    };
                    foreach (var action in item.actions)
                    {
                        notificaionItems.Add(new NotificationPayload
                        {
                            action = action,
                            payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                            token = notificationParams.token
                        });
                    }
                }
            }

            return notificaionItems;
        }
        //public static List<string> WORKFLOW_MODULE_EMPLOYEE_ALLOCATION(WorkflowDTO responseBody)
        //{
        //    List<string> action = new();

        //    var pendingTaskLists = responseBody.task_list
        //        .Where(m => m.status.ToLower().Trim().Equals(Constants.WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim())).ToList();
        //    foreach (var task in pendingTaskLists)
        //    {
        //        // 41,42 - UC010 - Notification for allocation of resources to a project (Reviewer if configuration is enabled) for reviewer 
        //        if (task.title == Constants.WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL)
        //        {
        //            action.Add(Constants.NotificationTemplateTypes.NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT);
        //        }
        //    }
        //    return action;
        //}
    }
}
