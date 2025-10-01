using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Constants
{
    public class Constants
    {
        public const string CommaSeparator = ",";
        public const string ICustomHeaderSystem = "custom_system_header";
        public static class UserRoles
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
            public const string SystemAdmin = "SystemAdmin";
            public const string AdditionalEl = "AdditionalEl";
            public const string AdditionalDelegate = "AdditionalDelegate";
            public const string EngagementLeader = "EngagementLeader";
            public const string SuperCoach = "SuperCoach";
        }

        public const string ALL = "All";
        public const string SUSPEND_STATUS = "Suspended";
        public const string LOST_STATUS = "Lost";
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

        public static class ServiceBusActions
        {
            /// <summary>
            /// USER_ALLOCATION_WORKFLOW Action Name
            /// </summary>
            public const string CreateUserAllocationWorkflowAction = "CREATE_USER_ALLOCATION_WORKFLOW";

            public const string CreateUserSkillAssessmentWorkflowAction = "CREATE_USER_SKILL_ASSESSMENT_WORKFLOW_ACTION";

            public const string UpdateMarkeplaceProjectDetails = "Update_MarketPlace_Project_Details";

            public const string PROJECT_ROLL_FORWARD_EVENT = "PROJECT_ROLL_FORWARD";

            public const string DESIGNATION_CHANGE_EVENT = "DESIGNATION_CHANGE_EVENT";

            public const string PROJECT_STATUS_CHANGE_EVENT = "PROJECT_STATUS_CHANGE";

            public const string EMPLOYEE_STATUS_CHANGE_EVENT = "EMPLOYEE_STATUS_CHANGE_EVENT";
            public const string EMPLOYEE_LEAVE_EVENT = "EMPLOYEE_LEAVE_EVENT";

            public const string PIPELINE_SUSPEND_EVENT = "PIPELINE_SUSPEND_EVENT";
            public const string REFRESH_PROJECT_COMPETENCY_EVENT = "REFRESH_PROJECT_COMPETENCY";
            public const string REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE = "REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE";
            public const string REFRESH_ALLOCATION_STATUS = "REFRESH_ALLOCATION_STATUS";
            public const string REFRESH_SKILL_STATUS = "REFRESH_SKILL_STATUS";
            public const string ADD_UPDATE_SUPERCOACH_DELEGATE = "ADD_UPDATE_SUPERCOACH_DELEGATE";
            public const string PROJECT_ROLE_UPDATE = "PROJECT_ROLE_UPDATE";
            public const string PROJECT_ACTIVATION_STATUS_EVENT = "PROJECT_ACTIVATION_STATUS_EVENT";
            public const string PROJECT_CLOSURE_STATUS_EVENT = "PROJECT_CLOSURE_STATUS_EVENT";
            public const string PROJECT_PIPELINE_ROLE_UPDATE = "PROJECT_PIPELINE_ROLE_UPDATE";
            public const string EMPLOYEE_SUPERCOACH_CHANGE = "EMPLOYEE_SUPERCOACH_CHANGE";
            public const string EMPLOYEE_CO_SUPERCOACH_CHANGE = "EMPLOYEE_CO_SUPERCOACH_CHANGE";
            public const string REFRESH_PROJECT_BUDGET_STATUS = "REFRESH_PROJECT_BUDGET_STATUS";

        }

        public const string NEW_PUSH_NOTIFICATION = "NEW_PUSH_NOTIFICATION";
        public const string CREATE_ALLOCATION_PUSH_NOTIFICATION = "CREATE_ALLOCATION_PUSH_NOTIFICATION";
        public const string CommaSplitter = ",";
        public const string Bearer = "Bearer";
        public const string PipelineCode = "PipelineCode";
        public const string Click = "click";
        public const string NotFound = "Not Found";
        public const string Absconder = "Absconder";
        public const string Voluntary = "Voluntary";
        public const string Involuntary = "Involuntary";
        public const string Termination = "Termination";
        public const string WORKFLOW_MODULE_USER_SKILL_ASSESSMENT = "WORKFLOW_MODULE_USER_SKILL_ASSESSMENT";
        public const string EMPLOYEE_ALLOCATION = "Employee Allocation";
        //Exception Messages
        public const string NoUsersFoundToSendNotification = "Users Not Found";
        public const string PipelineCodeNotFoundToFetchDetails = "Pipeline Code not found to fetch details";
        public const string PROJECT_SUSPENSION_NOTIFICATION = "PROJECT_SUSPENSION_NOTIFICATION";
        public class EnvVariblesAppsetting
        {
            public const string BASE_UI_URL = "BaseUiUrl";
            public const string BASE_GATEWAY_URL = "BaseGatewayUrl";
            //public const string BASE_GATEWAY_INTERNAL_URL = "BaseGatewayInternalUrl";
            public const string GREATE_WORKFLOW_SERVICE = "CreatWorkflowService";
            public const string REFRESH_WORKFLOW_TASK_ASSIGNMENT = "RefreshWorkflowTaskAssignment";
            public const string GET_WORKFLOW_TASK_ASSIGNMENT = "GetWorkflowTasksDetailsByQuery";
            public const string GetWorkflowSuperCoachTask = "GetWorkflowSuperCoachTask";
            public const string POST_NEW_NOTIFICATION_SERVICE = "PostNewNotificationService";
            public const string GET_NOTIFICATION_TEMPLATE = "GetNotificationTemplate";
            public const string SEND_NOTIFICATION_TEMPLATE = "SendNotification";
            public const string GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES = "GetRolesEmailByPipelineCodesAndRoles";
            public const string GET_EMPLOYEES_INFO_BY_EMAIL = "GetEmployeesInfoByEmail";
            public const string GET_RESOURCE_ALLOCATION_DETAILS_BY_GUID = "GetResourceAllocationDetailsByGuid";
            public const string GetMembersOfAllProjectsOfUsers = "GetMembersOfAllProjectsOfUsers";
            public const string GET_RESOURCE_REQUESTOR_EMAILS_BY_PIPELINE_CODE = "GetResourceRequestorEmailsByPipelineCode";
            public const string GET_PROJECT_FULL_DETAILSBY_PIPELINECODE = "GetProjectFullDetailsByPipelineCode";

            public const string UPDATE_PROJECT_DETAILS_TO_MARKETPLACE = "UpdateMarketPlaceProjectDetails";
            public const string GET_ALLOCATION_BY_LEAVES = "GetAllocationInformationByLeaves";
            public const string GET_ALLOCATION_BY_EMAIL = "GetAllocationByEmail";

            public const string UPDATE_PROJECT_SUSPENSION_STATUS = "UpdateProjectSuspensionStatus";
            public const string REPLACE_PROJECT_SUPERCOACH_ROLE = "ReplaceProjectsSuperCoachRole";
            public const string REFRESH_PROJECT_COMPETENCYAPI = "RefreshProjectCompetency";
            public const string UPDATE_PROJECT_ALLOCATION_STATUS = "UpdateListOfAllocationStatusInResourceAllocationDetails";
            public const string UPDATE_PROJECT_SKILL_STATUS = "UpdateUserSkillStatusAfterWorkflow";
            public const string RefreshEmpProjectInterestScore = "RefreshEmpProjectInterestScore";
            public const string ProjectActivationStatusChange = "ProjectActivationStatusChange";
            public const string UPDATE_SUPERCOACH_AND_DELEGATE = "UpdateSupercoachAndDelegate";
            public const string UPDATE_SUPERCOACH_AND_DELEGATE_FOR_PROJECT = "UpdateProjectRolesForSupercoachDelegate";




        }

        //public static class FieldName
        //{
        //    public const string PipelineCode = "PipelineCode";
        //    public const string JobCode = "JobCode";
        //}

        public class EnvAppSettingConstants
        {
            public const string GET_GATEWAY_BASE_URL = "BaseGatewayUrl";
            
            public const string ClientId = "clientId";
            public const string ClientSecret = "clientSecret";
            public const string Authority = "authority";
            public const string Audience = "audience";
            public const string TenantId = "tenantId";

            public const string GetAllProjectsForBudget = "GetAllProjectsForBudget";
            public const string GetAllProjectsForBudgetByJobCodes = "GetAllProjectsForBudgetByJobCodes";

            public const string PROJECT_ACTUAL_BUDGET_OVERSHOOT = "ProjectActualBudgetOverShoot";
            public const string BUDGET_OVERVIEW = "GetBudgetOverview";
            public const string UPDATEPROJECT_BUDGET = "UpdateProjectBudgetStatus";
            public const string AZURE_TOPIC_INIT = "AzureTopicInit";

            public const string UPDATE_BUDGET_BATCHSIZE = "UpdateBudgetBatchSize";

            public const string GET_CONFIGURATIONS_BY_CONFIG_TYPE = "GetConfigurationGroupByGroupNameAndConfigType";

        }

        public static class ConfigGroupExpertise
        {
            public const string WARNING_ALERT_CONDITION = "Amber_condition_for_Budget_Consumption";
            public const string BUDGET_ALERT_CONDITION_FOR_ALLOCATION = "Alert_condition_for_Allocation_Cost";
            //public const string EXPERTISE_CONFIGTYPE = "EXPERTISE";

        }

        public const char SeparatorPipe = '|';

        //public const string BudgetSchedulerDateFormat = "yyyy-MM-dd";

        public const string PROJECT_BUDGET_NOTIFICATION = "PROJECT_BUDGET_NOTIFICATION";

        public const string NotificationTypeNotification = "notification";

        public const string WF_TASK_OUTCOME = "inprogress";
        public const string WF_TASK_MODULE = "Employee Allocation";
        public const string WF_TASK_STATUS = "pending";

        public static readonly List<string> WF_TITLE_SUPERCOACH_PENDING = new List<string> { "Employee Allocation Approval Task For Supercoach", "Employee Allocation Supercoach Approval Over Resource Requestor Rejection" };

    }
}
