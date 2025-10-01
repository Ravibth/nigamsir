using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using static Gateway.API.Constants;

namespace Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper
{
    public class MarketPlaceMiddlewareHelper
    {
        public static List<NotificationPayload> MarketPlaceEmployeeLikeSubmit(InitNotificationDTO notificationParams)
        {
            //Reuest Body Extraction
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);
            MarkePlaceEmpInterestDTOPayload requestBody = JsonConvert.DeserializeObject<MarkePlaceEmpInterestDTOPayload>(requestPayload.body);
            List<string> action = new();
            // 123,124 - UC018 - If a person is deactivated from the Roles &permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
            if (requestBody.isInterested == true)
            {
                action.Add(NotificationTemplateTypes.NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT);
            }
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
            foreach (var item in action)
            {
                notificaionItems.Add(new NotificationPayload
                {
                    action = item,
                    payload = JsonConvert.SerializeObject(notificationParams).ToString(),
                    token = notificationParams.token
                });
            }
            return notificaionItems;


        }
        public static List<NotificationPayload> MarketPlaceRefreshEmployeeIntrestScoreForReqCreate(InitNotificationDTO notificationParams)
        {
            var requestPayload = JsonConvert.DeserializeObject<RequisitionResponse>(notificationParams.response_payload);
            RefreshEmpProjectInterestScoreRequestDTO requestDto = new()
            {
                PipelineCode = requestPayload.PipelineCode,
                JobCode = string.IsNullOrEmpty(requestPayload.JobCode) ? null : requestPayload.JobCode,
                RequisitionActionType = Constants.CREATE_REQUISITION,
            };
            NotificationPayload payload = new()
            {
                action = "REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE",
                token = notificationParams.token,
                payload = JsonConvert.SerializeObject(requestDto)
            };
            List<NotificationPayload> result = new();
            result.Add(payload);
            return result;
        }
        public static List<NotificationPayload> MarketPlaceRefreshEmployeeIntrestScoreForReqUpdate(InitNotificationDTO notificationParams)
        {
            var responsePayloadList = JsonConvert.DeserializeObject<List<RequisitionResponse>>(notificationParams.response_payload);
            List<NotificationPayload> result = new();

            if (responsePayloadList != null && responsePayloadList.Count > 0)
            {
                var responsePayload = responsePayloadList.FirstOrDefault();
                if (responsePayload != null && !string.IsNullOrEmpty(responsePayload.PipelineCode))
                {
                    RefreshEmpProjectInterestScoreRequestDTO requestDto = new()
                    {
                        PipelineCode = responsePayload.PipelineCode,
                        JobCode = string.IsNullOrEmpty(responsePayload.JobCode) ? null : responsePayload.JobCode,
                        RequisitionActionType = Constants.UPDATE_OR_DELETE_REQUISITION,
                    };
                    NotificationPayload payload = new()
                    {
                        action = "REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE",
                        token = notificationParams.token,
                        payload = JsonConvert.SerializeObject(requestDto)
                    };
                    result.Add(payload);
                }
            }

            return result;
        }
        public static List<NotificationPayload> MarketPlaceRefreshEmployeeIntrestScoreForReqDelete(InitNotificationDTO notificationParams)
        {
            var responseBody = JsonConvert.DeserializeObject<DeleteRequisitionResponseDTO>(notificationParams.response_payload);
            List<NotificationPayload> result = new();
            if (responseBody != null && responseBody.pipelineCode != null)
            {
                RefreshEmpProjectInterestScoreRequestDTO requestDto = new()
                {
                    PipelineCode = responseBody.pipelineCode,
                    JobCode = string.IsNullOrEmpty(responseBody.jobCode) ? null : responseBody.jobCode,
                    RequisitionActionType = Constants.UPDATE_OR_DELETE_REQUISITION,

                };
                NotificationPayload payload = new()
                {
                    action = "REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE",
                    token = notificationParams.token,
                    payload = JsonConvert.SerializeObject(requestDto),
                };
                result.Add(payload);
            }
            return result;
        }
        public static List<NotificationPayload> MarketPlaceRefreshEmployeeIntrestScoreForReqBulkUpload(InitNotificationDTO notificationParams)
        {
            var responseBody = JsonConvert.DeserializeObject<BulkUploadRequisitionResponseDTO>(notificationParams.response_payload);

            List<NotificationPayload> result = new();
            if (responseBody != null && responseBody.bulkRequisition != null && responseBody.bulkRequisition.Count > 0)
            {
                var request = responseBody.bulkRequisition.FirstOrDefault();
                if (request != null)
                {
                    RefreshEmpProjectInterestScoreRequestDTO requestDto = new()
                    {
                        PipelineCode = request.PipelineCode,
                        JobCode = string.IsNullOrEmpty(request.JobCode) ? null : request.JobCode,
                        RequisitionActionType = Constants.CREATE_REQUISITION,

                    };
                    NotificationPayload payload = new()
                    {
                        action = "REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE",
                        token = notificationParams.token,
                        payload = JsonConvert.SerializeObject(requestDto),
                    };
                    result.Add(payload);
                }
            }
            return result;
        }
    }
}
