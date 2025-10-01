using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Gateway.API.Helpers.IHttpServices;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper
{
    public class AllocationMiddlewareHelper : IAllocationMiddlewareHelper
    {
        private readonly IProjectHttpService _projectHttpService;
        public AllocationMiddlewareHelper(IProjectHttpService projectHttpService)
        {
            _projectHttpService = projectHttpService;
        }
        //public static async Task<List<string>> UpdateAllocationListStatus(InitNotificationDTO notificationParams)
        //{
        //    List<BulkWorkflowApprovalResponselDTO> workflowResponse = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalResponselDTO>>(notificationParams.response_payload);
        //    List<UpdateAllocationRequestDTO> requests = new List<UpdateAllocationRequestDTO>();
        //    foreach (var workflow in workflowResponse)
        //    {
        //        if (!workflow.isError)
        //        {
        //            try
        //            {
        //                var res1 = JsonConvert.SerializeObject(workflow.workflowResult);
        //                WorkflowDTO Wf = JsonConvert.DeserializeObject<WorkflowDTO>(res1);
        //                UpdateAllocationRequestDTO req = new UpdateAllocationRequestDTO()
        //                {
        //                    AllocationStatus = workflow.workflowResult.status,
        //                    guid = workflow.workflowResult.item_id
        //                };
        //                requests.Add(req);
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }

        //        }
        //    }
        //    if (!requests.IsNullOrEmpty())
        //    {
        //        var result = await _allocationHttpService.UpdateListOfAllocationStatus(requests);
        //        return result;
        //    }
        //    return null;
        //}


        public async Task<bool> AddUpdateProjectRequisitionAllocationForCreateRequisition(InitNotificationDTO notificationParams)
        {
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);
            var requestBody = JsonConvert.DeserializeObject<CreateRequisitionDTO>(requestPayload.body);

            return await _projectHttpService.AddUpdateProjectRequisitionAllocation(requestBody.PipelineCode, requestBody.JobCode, Convert.ToInt16(requestBody.NumberOfResources), 0);
        }

        public async Task<bool> AddUpdateProjectRequisitionAllocationForDeleteRequisitionById(InitNotificationDTO notificationParams)
        {
            var responseBody = JsonConvert.DeserializeObject<DeleteRequisitionResponseDTO>(notificationParams.response_payload);

            if (responseBody.Type.Equals(Constant.RequisitionTypeData.CreateRequisition))
            {
                return await _projectHttpService.AddUpdateProjectRequisitionAllocation(responseBody.pipelineCode, responseBody.jobCode, -1, 0);
            }
            return true;
        }

        public async Task<bool> AddUpdateProjectRequisitionAllocationForReleaseResourceByGuid(InitNotificationDTO notificationParams)
        {
            var response = JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(notificationParams.response_payload);
            return await _projectHttpService.AddUpdateProjectRequisitionAllocation(response.PipelineCode, response.JobCode, 0, -1);
        }

        public async Task<bool> AddUpdateProjectRequisitionAllocationForBulkRequisitionUpload(InitNotificationDTO notificationParams)
        {
            var response = JsonConvert.DeserializeObject<BulkUploadRequisitionResponseDTO>(notificationParams.response_payload);
            if (response != null && response.bulkRequisition.Count > 0)
            {
                return await _projectHttpService.AddUpdateProjectRequisitionAllocation(response.bulkRequisition[0].PipelineCode, response.bulkRequisition[0].JobCode, response.totalNumberRequisition, 0);
            }
            else
            {
                return true;
            }
        }
        
        public async Task<bool> AddUpdateProjectRequisitionAllocationForUpdateListOfAllocationStatusInResourceAllocationDetails(UpdateListOfAllocationStatusInResourceAllocationDetailsResponse response)
        {
            return await _projectHttpService.AddUpdateProjectRequisitionAllocation(response.pipelineCode, response.jobCode, 0, 0 - response.allocationDeletionCount);
        }

        public async Task<bool> AddUpdateProjectRequisitionAllocationForCommonAllocation(InitNotificationDTO notificationDTOparams)
        {
            var resourceAllocationDetailsResponses = JsonConvert.DeserializeObject<List<ResourceAllocationDetailsResponse>>(notificationDTOparams.response_payload);

            int count = 0;
            foreach (var allocation in resourceAllocationDetailsResponses)
            {
                if ((allocation.Requisition.RequisitionType.Type == RequisitionTypeData.CreateRequisition || allocation.Requisition.RequisitionType.Type == RequisitionTypeData.NamedAllocation) && allocation.AllocationStatus == RequistionStatuses.PENDING_APPROVAL) //Named allocation added
                {
                    count++;
                }
            }
            if (count > 0)
            {
                return await _projectHttpService.AddUpdateProjectRequisitionAllocation(resourceAllocationDetailsResponses[0].PipelineCode, resourceAllocationDetailsResponses[0].JobCode, 0, count);
            }
            else
            {
                return true;
            }
        }
        public static List<NotificationPayload> RollOVerAllocations(InitNotificationDTO notificationParams)
        {
            var responseBody = JsonConvert.DeserializeObject<AllocationRolloverResponseDTO>(notificationParams.response_payload);
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();

            var notifiactionParamForThisItem = new InitNotificationDTO
            {
                path = notificationParams.path,
                token = notificationParams.token,
                userinfo = notificationParams.userinfo,
                response_payload = JsonConvert.SerializeObject(responseBody),
                request_payload = JsonConvert.SerializeObject(responseBody.projectRolloverRequest)
            };
            //foreach (var action in item.actions)
            //{
            //}

            foreach (var action in responseBody.NotificationActions)
            {
                notificaionItems.Add(new NotificationPayload
                {
                    action = action,//TODO:- TO BE REMOVED
                    payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                    token = notificationParams.token
                });
            }
            //List<BulkWorkflowApprovalRequestDTO> requestBody = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalRequestDTO>>(notificationParams.response_payload);
            //foreach (var item in responseBody)
            //{


            //}

            return notificaionItems;
        }
        public static List<NotificationPayload> ReleaseResource(InitNotificationDTO notificationParams)
        {
            List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
            var notifiactionParamForThisItem = new InitNotificationDTO
            {
                path = notificationParams.path,
                token = notificationParams.token,
                userinfo = notificationParams.userinfo,
                response_payload = notificationParams.response_payload,
                request_payload = notificationParams.request_payload
            };
            notificaionItems.Add(new NotificationPayload
            {
                action = "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT",
                payload = JsonConvert.SerializeObject(notifiactionParamForThisItem).ToString(),
                token = notificationParams.token
            });
            return notificaionItems;
        }
    }
}
