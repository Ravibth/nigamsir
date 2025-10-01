using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.EmployeeService;
using ServiceLayer.Services.IdentityService;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.DTOs;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.RolesAndPermissionHelper;
using ServiceLayer.Services.ProjectService;
using ServiceLayer.Services.ProjectService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper
{
    public class RolesAndPermission : IRolesAndPermission
    {
        private readonly IProjectService _projectService;
        private readonly IIdentityService _identityService;
        public RolesAndPermission(IProjectService projectService, IIdentityService identityService)
        {
            _projectService = projectService;
            _identityService = identityService;
        }

        // 123,124 - UC018 - If a person is deactivated from the Roles & permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
        public async Task<List<Dictionary<string, string>>> USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);

            var deactivatedUser = requestQueryKeyValuePairs.Find(m => m.Key.Equals("emailId")).Value.ToList();

            List<Dictionary<string, string>> keyValuePairsResponse = new();

            Task<List<EmployeeInfoDTO>> listOfUserNames = _identityService.GetEmployeeDetailsByEmails(deactivatedUser, notificationPayloadDTO.token);
            Task<List<GetMembersOfAllProjectsOfUserResponse>> listOfProjectsAndItsStakeholders = _projectService.GetListOfAllMembersOfAllProjectsOfUser(new List<string> { deactivatedUser[0] }, notificationPayloadDTO.token);

            await Task.WhenAll(listOfUserNames, listOfProjectsAndItsStakeholders);
            var deactivatedUserEmail = deactivatedUser[0].ToLower().Trim();

            var deactivatedUserName = listOfUserNames.Result.Where(m => m.email_id.ToLower().Trim().Equals(deactivatedUserEmail)).FirstOrDefault();

            var disabledUserProjectInfo = listOfProjectsAndItsStakeholders.Result.Where(m => m.userEmail.Trim().ToLower().Equals(deactivatedUserEmail)).FirstOrDefault();
            if (disabledUserProjectInfo != null)
            {
                foreach (var item in disabledUserProjectInfo.ProjectMembers)
                {
                    Dictionary<string, string> keyValues = new()
                    {
                        { NotificationTemplatePayloads.EMPLOYEE_NAME, deactivatedUserName.name },
                        { NotificationTemplatePayloads.PROJECT_NAME, item.ProjectName },
                    };
                    if (item.RoleEmails.ContainsKey(Constants.Constants.UserRoles.ResourceRequestor))
                    {
                        var value = item.RoleEmails[Constants.Constants.UserRoles.ResourceRequestor].Where(m => !m.ToLower().Trim().Equals(deactivatedUserEmail)).ToList();
                        keyValues.Add(NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, String.Join(Constants.Constants.CommaSplitter, value));
                    }
                    if (item.RoleEmails.ContainsKey(Constants.Constants.UserRoles.Delegate))
                    {
                        var value = item.RoleEmails[Constants.Constants.UserRoles.Delegate].Where(m => !m.ToLower().Trim().Equals(deactivatedUserEmail)).ToList();
                        keyValues.Add(NotificationTemplatePayloads.DELEGATE_NAME, String.Join(Constants.Constants.CommaSplitter, value));
                    }
                    if (item.RoleEmails.ContainsKey(Constants.Constants.UserRoles.AdditionalEl))
                    {
                        var value = item.RoleEmails[Constants.Constants.UserRoles.AdditionalEl].Where(m => !m.ToLower().Trim().Equals(deactivatedUserEmail)).ToList();
                        keyValues.Add(NotificationTemplatePayloads.ADDITIONAL_EL_NAME, String.Join(Constants.Constants.CommaSplitter, value));
                    }
                    if (item.RoleEmails.ContainsKey(Constants.Constants.UserRoles.AdditionalDelegate))
                    {
                        var value = item.RoleEmails[Constants.Constants.UserRoles.AdditionalEl].Where(m => !m.ToLower().Trim().Equals(deactivatedUserEmail)).ToList();
                        keyValues.Add(NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME, String.Join(Constants.Constants.CommaSplitter, value));
                    }
                    keyValuePairsResponse.Add(keyValues);
                }
            }
            return keyValuePairsResponse;
        }
    }
}
