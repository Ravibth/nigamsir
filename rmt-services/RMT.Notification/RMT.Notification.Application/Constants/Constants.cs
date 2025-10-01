using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Infrastructure.Constants.Constants;

namespace RMT.Notification.Application.Constants
{
    public static class Constants
    {
        public const string EmailType = "Email";
        public const string PushType = "Push";
        //public const string NotificationTokenPattern = @"<([^>]+)>";
        public const int N0_OF_DAY = -1;
        public const string GET_RESOURCE_ALLOCATION_DETAILS = "RESOURCEALLOCATIONDETAILS";
        public const string GET_USER_INFO = "GETUSERINFO";
        public const string GET_PROJECT_DETAILS_BY_PIPELINE_CODE_JOB_CODE = "GETPROJECTDETAILSBYPIPELINECODEANDJOBCODE";
        public const string GET_PROJECT_RESOURCE_REQUESTOR = "GETPROJECTRESOURCEREQUESTOR";
        public const string EXTRACT_BASE_URL = "CONFIG";

        public const string NEW_ALLOCATION = "New Allocations";
        public const string RELEASE_ALLOCATION = "Release Allocation";
        public const string UPDATE_ALLOCATION = "Update Allocations";
        public const string CLOSED_ALLOCATION = "Rejected Allocations";

        public const string NEW_REQUISTION = "New Requisitions";
        public const string RELEASE_REQUISTION = "Delete Requisitions";
        public const string UPDATE_REQUISTION = "Update Requisitions";

        public const string ALLOCATION_SUMMARY_SUBJECT = "Summary of your projects";


        public const string EMPLOYEE_ALLOCATION = "Employee Allocation";
        public const string WORKFLOW_MODULE_USER_SKILL_ASSESSMENT = "WORKFLOW_MODULE_USER_SKILL_ASSESSMENT";

        public const string EMAIL_REGEX_PATTERN = "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}";

        public readonly static string[] ALLOCATION_SUMMARY_SENDER = { "Delegate", "Resource Requestor", "CSP", "SMEGLeader", "ProposedCSP" , "ProposedEL", "EO" , "CSL", "ResourceRequestor", "JobManager", "EngagementLeader", "Reviewer" };

        public const string ALLOCATION_SUMMARY_NOTIFICATION = "ALLOCATION_SUMMARY_NOTIFICATION";

        public const string EMPLOYEE_ALLOCATION_WF = "Employee Allocation";
        
        public const string WF_STATUS_CLOSE = "close";

        public const string CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE = "CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE";

        public const string RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL = "RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL";

        public const string ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR = "ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR";

        public static List<string> ProjectRoles = new List<string>
        {
            UserRoles.ResourceRequestor,
            UserRoles.Delegate,
            UserRoles.Reviewer,
            UserRoles.AdditionalEl,
            UserRoles.AdditionalDelegate,
            UserRoles.EngagementLeader,
            UserRoles.CSP,
            UserRoles.ProposedEL,
            UserRoles.ProposedCSP,
            UserRoles.SMEGLeader,
            UserRoles.JobManager
        };
        public static List<string> KeysOfDateTimes = new List<string>()
        {
            "created_at",
            "modifiedat",
            "allocationStartDate",
            "allocationEndDate",
            "enddate",
            "startdate",
            "enddate",
            "userassignmentdate",
            "leavestartdate",
            "leaveenddate",
            "suspendedmodifyat",
            "suspendedat",
            "pipelinestartdate",
            "pipelineoldstartdate"
        };

        public class EnvVariblesAppsetting
        {
            public const string BASE_UI_URL = "BaseUiUrl";
            public const string BASE_GATEWAY_URL = "BaseGatewayUrl";
            //public const string BASE_GATEWAY_INTERNAL_URL = "BaseGatewayInternalUrl";
            public const string GREATE_WORKFLOW_SERVICE = "CreatWorkflowService";
            public const string POST_NEW_NOTIFICATION_SERVICE = "PostNewNotificationService";
            public const string GET_NOTIFICATION_TEMPLATE = "GetNotificationTemplate";
            public const string GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES = "GetEmailOfProjectRoles";
            public const string GET_EMPLOYEES_INFO_BY_EMAIL = "GetEmployeesInfoByEmail";
            public const string GET_RESOURCE_ALLOCATION_DETAILS_BY_GUID = "GetResourceAllocationDetailsByGuid";
            public const string GetMembersOfAllProjectsOfUsers = "GetMembersOfAllProjectsOfUsers";
            public const string GET_RESOURCE_REQUESTOR_EMAILS_BY_PIPELINE_CODE = "GetResourceRequestorEmailsByPipelineCode";
            public const string GET_PROJECT_FULL_DETAILSBY_PIPELINECODE = "GetProjectFullDetailsByPipelineCode";
            public const string RESOURCE_ALLOCATION_BASE_URI = "ResourceAllocationMicroserviceBaseApiUrl"; 
            public const string PROJECT_CONFIG_BASE_URI = "ProjectConfigBaseUrl";
            public const string UPDATE_PROJECT_DETAILS_TO_MARKETPLACE = "UpdateMarketPlaceProjectDetails";
            public const string SKILLS_BASE_URI = "SkillsMicroserviceBaseApiUrl";
            public const string GETUSERSKILLBYID = "GetUserSkillsByID";
            public const string GETPROJECTCONFIG = "GetExpertiesConfigurationByExpertiesNameAndConfigGroup";
            public const string MicroserviceApiSettings = "MicroserviceApiSettings";
            public const string GetUserInfoFromIdentity = "GetUserInfoFromIdentity";
            public const string GETUSERBYEMAILS = "GetUsersByEmails";
            public const string GetUserDetailsFromIdentity = "GetUserDetailsFromIdentity";
            public const string IdentityServiceBaseUrl = "IdentityServiceBaseUrl";
            public const string NotificationTokenPatternRegex = "NotificationTokenPatternRegex";
            public const string NotificationRegexPrefix = "NotificationRegexPrefix";
            public const string NotificationRegexsuffix = "NotificationRegexsuffix";
            public const string GetProjectListedInMarketplaceByProjectListingDate = "GetProjectListedInMarketplaceByProjectListingDate";
            public const string MarketPlaceBaseUrl = "MarketPlaceBaseUrl";
            public const string SystemAdminEmailId = "SystemEmailId";
            public const string SystemAdminDisplayName = "SystemEmailDisplayName";
            

            public const string AllocationSummaryAdjustDays = "AllocationSummaryAdjustDays";
            public const string MarketPlaceSubscriptionAdjustDays = "MarketPlaceSubscriptionAdjustDays";
            public const string AdditionOfNewProjectToRMSAdjustDays = "AdditionOfNewProjectToRMSAdjustDays";
            public const string PendingAllocationAndSkillsTaskAdjustDays = "PendingAllocationAndSkillsTaskAdjustDays";

            public const string PushNotificationSinceNumberOfDays = "PushNotificationSinceNumberOfDays";
            public const string SummaryNotificationItemsToRun = "SummaryNotificationItemsToRun";
            
            public const string BaseUrl = "BaseUrl";
            public const string MARKETPLACE_BASE_URI = "MarketPlaceServiceBaseUrl";
            public const string MarketplaceDetailsIntrestURI = "GetAllMarketPlaceProjectDetailsIntrest";

            public const string GET_REQUISTION_BY_DATE = "GetRequisitionDetailsByDate";
            public const string GET_ALLOCATION_BY_DATE = "GetPublishAllocationDetailsByDate";
            public const string ProjectServiceBaseUrl = "ProjectServiceBaseUrl";
            public const string GET_WORKFLOW_TASK = "GetWorkflowTasks";
            public const string workflowClosedByUpdateDate = "workflowClosedByUpdateDate";
            public const string WORKFLOW_BASE_URL = "WorkflowBaseUrl";
            public const string Get_All_Project_By_CreationDate = "GetAllProjectByCreationDate";
        }

    }
}
