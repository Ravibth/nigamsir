using Newtonsoft.Json;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.IdentityService;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.RolesAndPermissionHelper;
using ServiceLayer.Services.ProjectService.DTOs;
using ServiceLayer.Services.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper.DTOs;
using Newtonsoft.Json.Linq;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper
{
    public class MarketPlace : IMarketPlace
    {
        private readonly IProjectService _projectService;
        private readonly IIdentityService _identityService;
        public MarketPlace(IProjectService projectService, IIdentityService identityService)
        {
            _projectService = projectService;
            _identityService = identityService;
        }



        // 23,24 - UC043 - Notification for interests in marketplace against their project 
        public async Task<List<Dictionary<string, string>>> NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationPayloadDTO.request_payload);
            MarkePlaceEmpInterestRequest _requestBody = JsonConvert.DeserializeObject<MarkePlaceEmpInterestRequest>(requestPayload.body);
            MarkePlaceEmpInterestDTOResponse _responseBody = JsonConvert.DeserializeObject<MarkePlaceEmpInterestDTOResponse>(notificationPayloadDTO.response_payload);
            //var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<dynamic>(requestPayload.body);
            var pipelineCode = _requestBody.pipelineCode;
            //Dictionary<string, object> keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestPayload.body);

            List<Dictionary<string, string>> keyValuePairsResponse = new();

            Task<Project> projectResults = _projectService.GetProjectDetailByPipelineCode(pipelineCode, notificationPayloadDTO.token);
            //  Task<List<GetMembersOfAllProjectsOfUserResponse>> listOfProjectsAndItsStakeholders = _projectService.GetListOfAllMembersOfAllProjectsOfUser(new List<string> { deactivatedUser[0] }, notificationPayloadDTO.token);

            await Task.WhenAll(projectResults);
            //var deactivatedUserEmail = listOfUserNames[0].ToLower().Trim();
            //var deactivatedUserName = listOfUserNames.Result.Where(m => m.email_id.ToLower().Trim().Equals(deactivatedUserEmail)).FirstOrDefault();
            //var disabledUserProjectInfo = listOfProjectsAndItsStakeholders.Result.Where(m => m.userEmail.Trim().ToLower().Equals(deactivatedUserEmail)).FirstOrDefault();
            // var disabledUserProjectInfo = null;

            if (projectResults != null && projectResults.Result != null)
            {
                var projectDetails = projectResults.Result;
                //************ get All Requestor Requestor 
                var resourceRequestor = projectDetails.ProjectRoles.Where(m => m.Role.Trim()
                .ToLower().Equals(Constants.Constants.UserRoles.ResourceRequestor.ToLower())).ToList();
                Dictionary<string, string> keyValues = new()
                    {
                        { NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME, string.Join(Constants.Constants.CommaSplitter , resourceRequestor.Select(e => e.User)) },
                        { NotificationTemplatePayloads.PROJECT_NAME, projectDetails.ProjectName },
                        { NotificationTemplatePayloads.LIKE_COUNT, Convert.ToString(_responseBody.NoOfInterested)},
                        { NotificationTemplatePayloads.CLICK_LINK, projectDetails.ProjectName },
                    };
                keyValuePairsResponse.Add(keyValues);

            }
            return keyValuePairsResponse;
        }
    }

}
