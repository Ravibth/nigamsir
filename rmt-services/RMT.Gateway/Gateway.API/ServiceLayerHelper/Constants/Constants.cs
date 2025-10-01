using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.API.ServiceLayerHelper.Constants
{
    public class Constants
    {
        public static class UserRoles
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string CEOCOO = "CEOCOO";
            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
            public const string SystemAdmin = "SystemAdmin";
            public const string AdditionalEl = "AdditionalEl";
            public const string AdditionalDelegate = "AdditionalDelegate";
            public const string EngagementLeader = "EngagementLeader";
        }

        public const string ALL = "All";

        public static readonly List<string> DetailsToFetchViaPipelineCode = new List<string>
        {
            UserRoles.ResourceRequestor,
            UserRoles.Delegate,
            UserRoles.Reviewer,
            UserRoles.Leaders,
            UserRoles.AdditionalEl,
            UserRoles.AdditionalDelegate,
            UserRoles.EngagementLeader,
            ALL,
            NotificationTemplatePayloads.PROJECT_NAME,
            NotificationTemplatePayloads.PROJECT_STATUS,
        };

        /// <summary>
        /// USER_ALLOCATION_WORKFLOW Action Name
        /// </summary>
        public const string CreateUserAllocationWorkflowAction = "CREATE_USER_ALLOCATION_WORKFLOW";
        public const string UpdateUserAllocationWorkflowAction = "UPDATE_USER_ALLOCATION_WORKFLOW";
        public const string CreateUserSkillAssessmentWorkflowAction = "CREATE_USER_SKILL_ASSESSMENT_WORKFLOW_ACTION";

        public const string NEW_PUSH_NOTIFICATION = "NEW_PUSH_NOTIFICATION";
        public const string CREATE_ALLOCATION_PUSH_NOTIFICATION = "CREATE_ALLOCATION_PUSH_NOTIFICATION";
        public const string CommaSplitter = ",";
        public const string Bearer = "Bearer";
        public const string PipelineCode = "PipelineCode";
        public const string Click = "click";
        public const string NotFound = "Not Found";

        //Exception Messages
        public const string NoUsersFoundToSendNotification = "Users Not Found";
        public const string PipelineCodeNotFoundToFetchDetails = "Pipeline Code not found to fetch details";

        public class EnvVariblesAppsetting
        {
            public const string BASE_UI_URL = "BaseUiUrl";
            public const string BASE_GATEWAY_URL = "BaseGatewayUrl";
            public const string GREATE_WORKFLOW_SERVICE = "CreatWorkflowService";
            public const string POST_NEW_NOTIFICATION_SERVICE = "PostNewNotificationService";
            public const string GET_NOTIFICATION_TEMPLATE = "GetNotificationTemplate";
            public const string GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES = "GetRolesEmailByPipelineCodesAndRoles";
            public const string GET_EMPLOYEES_INFO_BY_EMAIL = "GetEmployeesInfoByEmail";
            public const string GET_RESOURCE_ALLOCATION_DETAILS_BY_GUID = "GetResourceAllocationDetailsByGuid";
        }
    }
}
