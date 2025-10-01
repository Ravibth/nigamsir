using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService;
using ServiceLayer.Services.IdentityService;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper.DTOs;
using ServiceLayer.Services.ProjectService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper
{
    public class WorkflowNotificationHelper : IWorkflowNotificationHelper
    {
        private readonly IAllocationService _allocationService;
        private readonly IProjectService _projectService;
        private readonly IIdentityService _identityService;

        public WorkflowNotificationHelper(IAllocationService allocationService, IProjectService projectService, IIdentityService identityService)
        {
            _allocationService = allocationService;
            _projectService = projectService;
            _identityService = identityService;
        }

        public async Task<List<Dictionary<string, string>>> NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            Dictionary<string, string> keyValuePairsResponse = new();

            // Reviewer,ProjectName,Date
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);
            var responsePayload = JsonConvert.DeserializeObject<WorkflowDTO>(notificationPayloadDTO.response_payload);

            var task = responsePayload.task_list
                .Where(m => m.status.Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING) && m.title.Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL)).FirstOrDefault();

            //TODO Check how to got project name from allocation id
            //! Warning Using Pipeline Name instead of project name for now
            if (task != null)
            {
                keyValuePairsResponse.Add(NotificationTemplatePayloads.REVIEWER_NAME, task.assigned_to);
                keyValuePairsResponse.Add(NotificationTemplatePayloads.DATE, task.created_at.Value.Date.ToString());
                var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.item_id, notificationPayloadDTO.token);
                keyValuePairsResponse.Add(NotificationTemplatePayloads.PROJECT_NAME, allocationDetails.PipelineName);
                keysValuePairResults.Add(keyValuePairsResponse);
            }

            return keysValuePairResults;
        }
        public async Task<List<Dictionary<string, string>>> RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            Dictionary<string, string> keyValuePairsResponse = new();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            //var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result
                .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                .FirstOrDefault();

            if (task != null)
            {
                //string[] reviewers = task.assigned_to.Split(Constants.Constants.CommaSeparator);
                keyValuePairsResponse.Add(NotificationTemplatePayloads.REVIEWER_NAME, task.assigned_to);
                var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
                keyValuePairsResponse.Add(NotificationTemplatePayloads.PROJECT_NAME, allocationDetails.PipelineName);
                keysValuePairResults.Add(keyValuePairsResponse);
            }
            return keysValuePairResults;
        }
        public async Task<List<Dictionary<string, string>>> RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            //Dictionary<string, string> keyValuePairsResponse = new();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            //var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await _projectService.GetResourceRequestorEmailsByPipelineCode(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();

            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }

            foreach (var kv in projectRoles.AsEnumerable())
            {
                Dictionary<string, string> keyValuePairs = new()
                {
                    { NotificationTemplatePayloads.PROJECT_NAME , allocationDetails.PipelineName }
                };
                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    keyValuePairs.Add(NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value));
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    keyValuePairs.Add(NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value));
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    keyValuePairs.Add(NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value));
                }
                keysValuePairResults.Add(keyValuePairs);
            }
            if (responsePayload.result.Count > 0)
            {
                Dictionary<string, string> keyValuePairsResponse = new();
                keyValuePairsResponse.Add(NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(",", requestorRoles.Select(e => e.User)));
                keyValuePairsResponse.Add(NotificationTemplatePayloads.PROJECT_NAME, allocationDetails.PipelineName);
                keysValuePairResults.Add(keyValuePairsResponse);
            }
            return keysValuePairResults;
        }
        public async Task<List<Dictionary<string, string>>> NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            //var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var task = responsePayload.result
                       .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim()))
                       .FirstOrDefault();
            if (task != null)
            {
                string[] employees = task.assigned_to.Split(Constants.Constants.CommaSplitter);
                Dictionary<string, string> keyValuePairsResponse = new()
                {
                    { NotificationTemplatePayloads.EMPLOYEE_NAME , string.Join(Constants.Constants.CommaSplitter, employees) },
                    {NotificationTemplatePayloads.PROJECT_NAME , allocationDetails.PipelineName},
                    {NotificationTemplatePayloads.DATE, Convert.ToString(task.created_at) }
                };
                keysValuePairResults.Add(keyValuePairsResponse);
            }
            return keysValuePairResults;
        }
        //public async Task<Dictionary<string, string>> NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        //{
        //    Dictionary<string, string> keyValuePairsResponse = new();
        //    var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
        //    if (responsePayload != null && !responsePayload.isError)
        //    {
        //        var task = responsePayload.result
        //                                    .Where(m => m.status.Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING) && m.title.Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL)).FirstOrDefault();
        //        var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.requestPayload.item_id, notificationPayloadDTO.token);
        //        keyValuePairsResponse.Add(NotificationTemplatePayloads.EMPLOYEE_NAME, allocationDetails.EmpEmail);
        //        keyValuePairsResponse.Add(NotificationTemplatePayloads.DATE, allocationDetails.ModifiedDate.Date.ToString());
        //        keyValuePairsResponse.Add(NotificationTemplatePayloads.PROJECT_NAME, allocationDetails.PipelineName);
        //    }
        //    return keyValuePairsResponse;
        //}

        /// <summary>
        /// 67,68 - UC025 - DONE
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keyValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                            .FirstOrDefault();
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            if (task != null)
            {
                Dictionary<string, string> keyValuePairsResponse = new Dictionary<string, string>()
                {
                    {NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                    {NotificationTemplatePayloads.PROJECT_NAME ,  string.IsNullOrEmpty(allocationDetails.JobName) ? allocationDetails.PipelineName : allocationDetails.JobName},
                    {NotificationTemplatePayloads.DATE , Convert.ToString(task.created_at) }
                };
                keyValuePairResults.Add(keyValuePairsResponse);
            }
            return keyValuePairResults;
        }
        /// <summary> 
        /// // 69,70 - UC025 - DONE
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await _projectService.GetResourceRequestorEmailsByPipelineCode(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            //employee task
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                            .FirstOrDefault();
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();
            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }
            foreach (var kv in projectRoles.AsEnumerable())
            {

                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                        { NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                        { NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                        { NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
            }
            if (requestorRoles.Count > 0)
            {
                Dictionary<string, string> rrKeyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                        { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME , string.Join(Constants.Constants.CommaSplitter , requestorRoles.Select(e => e.User))},
                    };
                keysValuePairResults.Add(rrKeyValuePairs);
            }
            return keysValuePairResults;
        }
        /// <summary>
        /// // 81,82 - UC025
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keyValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_REJECT.ToLower().Trim()))
                            .FirstOrDefault();
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            if (task != null)
            {
                Dictionary<string, string> keyValuePairsResponse = new Dictionary<string, string>()
                {
                    {NotificationTemplatePayloads.EMPLOYEE_NAME , task.assigned_to },
                    {NotificationTemplatePayloads.PROJECT_NAME ,  string.IsNullOrEmpty(allocationDetails.JobName) ? allocationDetails.PipelineName : allocationDetails.JobName},
                    {NotificationTemplatePayloads.DATE , Convert.ToString(task.created_at) }
                };
                keyValuePairResults.Add(keyValuePairsResponse);
            }
            return keyValuePairResults;
        }
        /// <summary>
        /// // 83,84 - UC025
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await _projectService.GetResourceRequestorEmailsByPipelineCode(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            //Resource Requestor task after employee rejection
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim()))
                            .FirstOrDefault();
            if (task == null)
            {
                return keysValuePairResults;
            }
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();
            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }
            foreach (var kv in projectRoles.AsEnumerable())
            {

                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME ,  string.Join(Constants.Constants.CommaSplitter,allocationDetails.EmpEmail)},
                        { NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME ,  string.Join(Constants.Constants.CommaSplitter,allocationDetails.EmpEmail)},
                        { NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME ,  string.Join(Constants.Constants.CommaSplitter,allocationDetails.EmpEmail)},
                        { NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
            }
            keysValuePairResults.Add(new Dictionary<string, string>()
            {
                { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                { NotificationTemplatePayloads.EMPLOYEE_NAME ,  string.Join(Constants.Constants.CommaSplitter,allocationDetails.EmpEmail) },
                { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, task.assigned_to }
            });
            return keysValuePairResults;
        }
        /// <summary>
        /// // 85,86 - UC025
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keyValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                            .FirstOrDefault();
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            if (task != null)
            {
                Dictionary<string, string> keyValuePairsResponse = new Dictionary<string, string>()
                {
                    {NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                    {NotificationTemplatePayloads.PROJECT_NAME ,  string.IsNullOrEmpty(allocationDetails.JobName) ? allocationDetails.PipelineName : allocationDetails.JobName},
                    {NotificationTemplatePayloads.DATE , Convert.ToString(task.created_at) }
                };
                keyValuePairResults.Add(keyValuePairsResponse);
            }
            return keyValuePairResults;
        }
        /// <summary>
        /// // 87,88 - UC025
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            //RESOURCE REQUESTOR TERMINATED TASK
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_TERMINATED.ToLower().Trim()))
                            .FirstOrDefault();
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);

            if (task == null)
            {
                return keysValuePairResults;
            }
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();
            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }
            foreach (var kv in projectRoles.AsEnumerable())
            {
                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
            }
            if (requestorRoles.Count > 0)
            {
                Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(Constants.Constants.CommaSplitter, string.Join(Constants.Constants.CommaSplitter , requestorRoles) ) }
                    };
                keysValuePairResults.Add(keyValuePairs);
            }
            return keysValuePairResults;
        }
        /// <summary>
        /// //89,90 UC025
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            //RESOURCE_REQUESTOR APPROVED TASK OF EMPLOYEE REJECTION
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                            .FirstOrDefault();
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            if (task == null)
            {
                return keysValuePairResults;
            }
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();
            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }
            foreach (var kv in projectRoles.AsEnumerable())
            {
                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) }
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
            }
            if (requestorRoles.Count > 0)
            {
                Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(Constants.Constants.CommaSplitter, string.Join(Constants.Constants.CommaSplitter , requestorRoles) ) }
                    };
                keysValuePairResults.Add(keyValuePairs);
            }
            return keysValuePairResults;
        }
        /// <summary>
        /// //91,92 UC025
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var task = responsePayload.result
                            .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_REJECT.ToLower().Trim()))
                            .FirstOrDefault();
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            if (requestorRoles != null && requestorRoles.Count > 0)
            {
                Dictionary<string, string> keyValuePairsResponse = new Dictionary<string, string>()
                {
                    {NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                    {NotificationTemplatePayloads.PROJECT_NAME ,  string.IsNullOrEmpty(allocationDetails.JobName) ? allocationDetails.PipelineName : allocationDetails.JobName},
                    {NotificationTemplatePayloads.DATE , Convert.ToString(responsePayload.result[0].created_at) },
                    {NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(Constants.Constants.CommaSplitter, string.Join(Constants.Constants.CommaSplitter , requestorRoles) )}
                };
                keysValuePairResults.Add(keyValuePairsResponse);
            }
            return keysValuePairResults;
        }
        /// <summary>
        /// //99,100 UC026 --- COMPLETED
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var taskRejectedByRequestor = responsePayload.result
                           .Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_REJECT.ToLower().Trim()))
                           .FirstOrDefault();
            var taskForReviewer = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim()))
                           .FirstOrDefault();
            if (taskRejectedByRequestor == null || taskForReviewer == null)
            {
                return keysValuePairResults;
            }
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            List<RoleEmailsByPipelineCodeResponse> projectsWithRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(new List<PipelineCodeAndRolesDto>() { new PipelineCodeAndRolesDto() { pipelineCode = allocationDetails.PipelineCode, jobCode = allocationDetails.JobCode } }, notificationPayloadDTO.token);
            Dictionary<string, List<string>> projectRoles = new Dictionary<string, List<string>>();
            if (projectsWithRoles.Count > 0)
            {
                projectRoles = projectsWithRoles.FirstOrDefault().RolesEmails;
            }
            foreach (var kv in projectRoles.AsEnumerable())
            {
                if (kv.Key.Equals(UserRoles.Delegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) },
                        { NotificationTemplatePayloads.REVIEWER_NAME ,  taskForReviewer.assigned_to}
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalDelegate))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) },
                        { NotificationTemplatePayloads.REVIEWER_NAME ,  taskForReviewer.assigned_to}
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
                if (kv.Key.Equals(UserRoles.AdditionalEl))
                {
                    Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.ADDITIONAL_EL_NAME, string.Join(Constants.Constants.CommaSplitter, kv.Value) },
                        { NotificationTemplatePayloads.REVIEWER_NAME ,  taskForReviewer.assigned_to}
                    };
                    keysValuePairResults.Add(keyValuePairs);
                }
            }
            if (requestorRoles.Count > 0)
            {
                Dictionary<string, string> keyValuePairs = new()
                    {
                        { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                        { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                        { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(Constants.Constants.CommaSplitter, string.Join(Constants.Constants.CommaSplitter , requestorRoles) ) },
                        { NotificationTemplatePayloads.REVIEWER_NAME ,  taskForReviewer.assigned_to}
                    };
                keysValuePairResults.Add(keyValuePairs);
            }
            return keysValuePairResults;
        }
        /// <summary>
        /// //101,102 UC026 --- COMPLETED
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim()))
                           .FirstOrDefault();
            if (task == null)
            {
                return keysValuePairResults;
            }
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                { NotificationTemplatePayloads.REVIEWER_NAME ,  task.assigned_to}
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// //103,104 UC026
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                           .FirstOrDefault();
            if (task == null)
            {
                return keysValuePairResults;
            }
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                { NotificationTemplatePayloads.REVIEWER_NAME ,  task.assigned_to}
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// //105,106 UC026
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                           .FirstOrDefault();
            if(task == null)
            {
                return keysValuePairResults;
            }
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode , allocationDetails.JobCode , notificationPayloadDTO.token);
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                { NotificationTemplatePayloads.REVIEWER_NAME ,  task.assigned_to},
                { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME , string.Join(Constants.Constants.CommaSeparator , requestorRoles) }
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// //107,108 UC026 --- WRONG_TEXT_PROVIDED
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                           .FirstOrDefault();
            if (task == null)
            {
                return keysValuePairResults;
            }
            var allocationDetails = await _allocationService.GetResourceAllocationDetailsByGuid(responsePayload.workflowResult.item_id, notificationPayloadDTO.token);
            var requestorRoles = await GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(allocationDetails.PipelineCode, allocationDetails.JobCode, notificationPayloadDTO.token);
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.PROJECT_NAME , string.IsNullOrEmpty(allocationDetails.JobCode) ? allocationDetails.PipelineName : allocationDetails.JobName  },
                { NotificationTemplatePayloads.EMPLOYEE_NAME , allocationDetails.EmpEmail },
                { NotificationTemplatePayloads.REVIEWER_NAME ,  task.assigned_to},
                { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME , string.Join(Constants.Constants.CommaSeparator , requestorRoles) }
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }

        public async Task<List<Dictionary<string, string>>> SUPERCOACH_NOTIFICATION_OF_PENDING_TASK(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
           // var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<Dictionary<string, List<WorkflowNotificationDTO>>>(notificationPayloadDTO.response_payload);

            PendingTaskNotificationDTO pendingTaskNotification = new PendingTaskNotificationDTO()
            {
                Allocation = new List<AllocationPendingTaskNotificationDTO>(),
                Skills = new List<SkillPendingTaskNotificationDTO>(),
                Email = string.Empty
            };
            foreach (KeyValuePair<string, List<WorkflowNotificationDTO>> entry in responsePayload)
            {

                pendingTaskNotification.Email = entry.Key;
                var list = entry.Value;
                foreach (var item in list)
                {
                    AllocationPendingTaskNotificationDTO allocation = new AllocationPendingTaskNotificationDTO();
                    SkillPendingTaskNotificationDTO skillTask = new SkillPendingTaskNotificationDTO();
                    if (item.workflow.module == Constants.Constants.EMPLOYEE_ALLOCATION)
                    {
                        allocation.Title = item.title;
                        allocation.DueDate = DateOnly.FromDateTime(item.due_date.Value.Date);
                        allocation.ReceivedFrom = item.created_by;
                        allocation.ReceivedDate = DateOnly.FromDateTime(item.created_at.Date);
                        //allocation.DaysLeft = (int)(allocation.DueDate - DateTime.Now).TotalDays;
                        allocation.DaysLeft =  item.due_date != null ? ( item.due_date.Value.Date - DateTime.Now.Date).Days : 0;
                        allocation.Status = item.status;
                        allocation.JobCode = JObject.Parse(item.workflow.entity_meta_data.ToString())["JobCode"].ToString();
                        pendingTaskNotification.Allocation.Add(allocation);
                    }
                    else if (item.workflow.module == Constants.Constants.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT)
                    {
                        var meta = JObject.Parse(item.workflow.entity_meta_data.ToString());
                        skillTask.Status = item.status;
                        skillTask.Title = item.title;
                        skillTask.ReceivedDate = DateOnly.FromDateTime(item.created_at.Date);
                        skillTask.EmployeeName = meta["Name"].ToString();
                        skillTask.SkillName = meta["SkillName"].ToString();
                        skillTask.SkillLevel = meta["Proficiency"].ToString();
                        skillTask.Age = (DateTime.Now.Date - item.created_at.Date).Days;
                        pendingTaskNotification.Skills.Add(skillTask);
                    }
                }

            }

            var allocationTableContent = GenerateAllocationEmailBody(pendingTaskNotification);
            var skillTableContent = GenerateSkillEmailBody(pendingTaskNotification);
            Dictionary<string, string> keyValuePairs = new()
            {

                { NotificationTemplatePayloads.EMPLOYEE_NAME , pendingTaskNotification.Email },
                { NotificationTemplatePayloads.TABLE_DESIGN_FOR_ALLOCATION , allocationTableContent },
                { NotificationTemplatePayloads.TABLE_DESIGN_FOR_SKILL , skillTableContent}
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }

        public static string GenerateSkillEmailBody(PendingTaskNotificationDTO pendingTaskNotification)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (pendingTaskNotification.Skills.Count > 0)
            {
                tableContent.Append("<table border='1'><tr><th>Title</th><th>Task ID</th>" +
                    "<th>Request Received Date</th>" +
                    "<th>Request Age (Days)</th>" +
                    "<th>Status </th>" +
                    "<th>Employee Name</th>" +
                    "<th>Skills Name </th>" +
                    "<th>Skill Proficiency Level </th>" +
                    " </tr>");
                foreach (var item in pendingTaskNotification.Skills)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.Title).Append("</td>");
                    tableContent.Append("<td>").Append(item.TaskID).Append("</td>");
                    tableContent.Append("<td>").Append(item.ReceivedDate).Append("</td>");
                    tableContent.Append("<td>").Append(item.Age).Append("</td>");
                    tableContent.Append("<td>").Append(item.Status).Append("</td>");
                    tableContent.Append("<td>").Append(item.EmployeeName).Append("</td>");
                    tableContent.Append("<td>").Append(item.SkillName).Append("</td>");
                    tableContent.Append("<td>").Append(item.SkillLevel).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                if (pendingTaskNotification.Skills.Count > 0)
                {
                    body.Append("<h2>Skill Assessment Requests:</h2>");
                }
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }
        public static string GenerateAllocationEmailBody(PendingTaskNotificationDTO pendingTaskNotification)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();
            if (pendingTaskNotification.Allocation.Count > 0)
            {
                tableContent.Append("<table border='1'><tr><th>Title</th><th>Request Received Date</th><th>Request Received From</th>" +
              "<th> Due Date </th>" +
              "<th>No of days left to take action </th>" +
              "<th>Status </th>" +
              "<th>JOb Code </th>" +
              " </tr>");
                foreach (var item in pendingTaskNotification.Allocation)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.Title).Append("</td>");
                    tableContent.Append("<td>").Append(item.ReceivedDate).Append("</td>");
                    tableContent.Append("<td>").Append(item.ReceivedFrom).Append("</td>");
                    tableContent.Append("<td>").Append(item.DueDate).Append("</td>");
                    tableContent.Append("<td>").Append(item.DaysLeft).Append("</td>");
                    tableContent.Append("<td>").Append(item.Status).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobCode).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                body.Append("<h2>Allocation Requests:</h2>");
                body.Append("<p>In case no action is taken on the allocation request, the allocation will be auto-approved.\r\n:</p>");
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();
        }

        /// <summary>
        /// //130 , 131 UC028
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<WorkflowDTO>(notificationPayloadDTO.response_payload);
            //var responsePayload1 =  JArray.Parse(notificationPayloadDTO.response_payload);
            
            //responsePayload1.Where(obj => )
            //var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.task_list.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_PENDING.ToLower().Trim()))
                           .FirstOrDefault();
            var employeeInfo = await _identityService.GetEmployeeDetailsByEmails(new List<string> { task.assigned_to }   , notificationPayloadDTO.token);
            //var workflowResult = responsePayload.workflowResult;
            if(task == null )
            {
                return keysValuePairResults;
            }
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.SUPERCOACHNAME_NAME , employeeInfo.Count > 0 ? employeeInfo.FirstOrDefault().name + "(" + task.assigned_to + ")" : task.assigned_to },
                { NotificationTemplatePayloads.EMPLOYEE_NAME ,  responsePayload.entity_meta_data["Email"].Value }
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// //132,133 UC028
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim()))
                           .FirstOrDefault();
            var workflowResult = responsePayload.workflowResult;
            if (task == null || workflowResult == null)
            {
                return keysValuePairResults;
            }
            var employeeInfo = await _identityService.GetEmployeeDetailsByEmails(new List<string> { task.assigned_to }, notificationPayloadDTO.token);
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.SUPERCOACHNAME_NAME , employeeInfo.Count > 0 ? employeeInfo.FirstOrDefault().name + "(" + task.assigned_to + ")" : task.assigned_to },
                { NotificationTemplatePayloads.EMPLOYEE_NAME , workflowResult.entity_meta_data["Email"].Value }
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// //134,135 UC028
        /// </summary>
        /// <param name="notificationPayloadDTO"></param>
        /// <param name="requiredKeys"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            List<Dictionary<string, string>> keysValuePairResults = new List<Dictionary<string, string>>();
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var responsePayload = JsonConvert.DeserializeObject<BulkUpdateWorkflowResponseDTO>(notificationPayloadDTO.response_payload);
            var task = responsePayload.result.Where(m => m.title.ToLower().Trim().Equals(WorkflowConstants.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_.ToLower().Trim()) && m.status.ToLower().Trim().Equals(WorkflowConstants.WORKFLOW_TASK_STATUS_REJECT.ToLower().Trim()))
                           .FirstOrDefault();
            var workflowResult = responsePayload.workflowResult;
            if (task == null || workflowResult == null)
            {
                return keysValuePairResults;
            }
            var employeeInfo = await _identityService.GetEmployeeDetailsByEmails(new List<string> { task.assigned_to }, notificationPayloadDTO.token);
            var comment =  JsonConvert.DeserializeObject<List<SkillReviewCommentDTO>>(task.comment).OrderBy(e => e.created_at).FirstOrDefault();
            Dictionary<string, string> keyValuePairs = new()
            {
                { NotificationTemplatePayloads.SUPERCOACHNAME_NAME , employeeInfo.Count > 0 ? employeeInfo.FirstOrDefault().name + "(" + task.assigned_to + ")"  : task.assigned_to},
                { NotificationTemplatePayloads.EMPLOYEE_NAME ,  workflowResult.entity_meta_data["Email"].Value },
                { NotificationTemplatePayloads.REJECTION_REASON , comment.comment }
            };
            keysValuePairResults.Add(keyValuePairs);
            return keysValuePairResults;
        }
        /// <summary>
        /// HELPER FUNCTION FOR GETTING RESOURCE_REQUESTORS
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<List<string>> GetResourceRequestorEmailsByPipelineCodeAndJobCodeAsync(string pipelineCode, string? jobCode, string token)
        {
            try
            {
                var requestorRoles = await _projectService.GetResourceRequestorEmailsByPipelineCode(pipelineCode, jobCode, token);
                return requestorRoles.Select(rr => rr.User).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Resource Requestor Details");
                throw;
            }
        }
    }

}
