using System.Collections.Generic;

namespace RMT.Scheduler.Constants
{
    public class Constant
    {
        /// <summary>
        /// Separator Pipe.
        /// </summary>
        public const char SeparatorPipe = '|';
        /// <summary>
        /// Separator Colon.
        /// </summary>
        public const char SeparatorColon = ':';
        /// <summary>
        /// Separator Comma.
        /// </summary>
        public const char SeparatorComma = ',';
        /// <summary>
        /// OpenRoundBracket -> '('
        /// </summary>
        public const char OpenRoundBracket = '(';
        /// <summary>
        /// ClosedRoundBracket => ')'
        /// </summary>
        public const char CloseRoundBracket = ')';
        /// <summary>
        /// DoubleDolarIdentifire
        /// </summary>
        public const string DoubleDolar = "$$";//unused
        /// <summary>
        /// Double @@
        /// </summary>
        public const string DoubleAtTheRate = "@@";
        /// <summary>
        /// Integer
        /// </summary>
        public const string INTEGER = "integer";
        /// <summary>
        /// Boolean
        /// </summary>
        public const string BOOLEAN = "bool";
        /// <summary>
        /// SQL
        /// </summary>
        public const string SQL = "SQL";
        /// <summary>
        /// datetime
        /// </summary>
        public const string DATETIME = "datetime";
        /// <summary>
        /// guid
        /// </summary>
        public const string GUID = "guid";
        /// <summary>
        /// EventBusIdentificationType
        /// </summary>

        public const string EventBusIdentificationType = "sync-event";


        public const string BudgetSchedulerDateFormat = "yyyy-MM-dd";

        public const string INSERT_OPRATION = "INSERT";
        public const string UPDATE_OPRATION = "UPDATE";

        public const string CHARGABLE = "Chargeable";
        public const string NONCHARGABLE = "NonChargeable";

        public const string RECURRING = "Recurring";
        public const string NONRECURRING = "Non Recurring";

        public const string SUPERCOACH_NOTIFICATION_OF_PENDING_TASK = "SUPERCOACH_NOTIFICATION_OF_PENDING_TASK";

        public static class NotificationTemplateType
        {
            public const string ALLOCATION_SUMMARY_NOTIFICATION = "ALLOCATION_SUMMARY_NOTIFICATION";
            public const string RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL = "RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL";
            public const string ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR = "ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR";
            public const string CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE = "CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE";
        }

        public static class ServiceBusActions
        {
            public const string REFRESH_PROJECT_BUDGET_STATUS = "REFRESH_PROJECT_BUDGET_STATUS";
        }

        public const string PROJECT_BUDGET_NOTIFICATION = "PROJECT_BUDGET_NOTIFICATION";
        /// <summary>
        /// Notification Subscription Filter Type
        /// </summary>
        public const string NotificationTypeNotification = "notification";


        public const string GT_HANDLER_ID = "system.@gt.com";
        public readonly static string[] SUSPENDED_STATUS = new string[] { "suspended", "lost" };

        public const string ICustomHeaderSystem = "custom_system_header";
        public const string InprogressOutcome = "inprogress";
        public const string Taskstatus = "pending";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL = "employee allocation approval task for reviewer";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL = "employee allocation approval task for employee";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL = "employee allocation approval task for supercoach";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL = "employee allocation withdrawl task for employee";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION = "employee allocation task for resource requestor after employee rejection";
        public const string WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION = "employee allocation task for reviewer after resource requestor rejection";
        public const string WF_TASK_EMPLOYEE_SKILL_SUPERCOACH_APPROVAL = "user skill assessment task for supercoach after employee added or updated a skill";
        /// <summary>
        ///     WorkflowStatus ---> Workflow task status
        /// </summary>
        public const string APPROVED = "approved";
        public const string PENDING = "pending";
        public const string REJECT = "rejected";
        //  public const string SUPERCOACH_NOTIFICATION_OF_PENDING_TASK = "SUPERCOACH_NOTIFICATION_OF_PENDING_TASK";
        /// <summary>
        /// Notification Subscription Filter Type
        /// </summary>
        // public const string NotificationTypeNotification = "notification";

        /// <summary>
        /// Notification Subscription Filter Type
        /// </summary>
        public static List<string> WF_TASK_TITLE = new List<string>
                                {
                                    Constant.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL,
                                    Constant.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL,
                                    Constant.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL,
                                    Constant.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION,
                                    Constant.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION,
                                    Constant.WF_TASK_EMPLOYEE_SKILL_SUPERCOACH_APPROVAL
                                };

        public class NotificationActions
        {
            public const string NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP = "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP";
        }

        public class EnvAppSettingConstants
        {
            public const string numberOfDaysToConsiderToFetchInitially = "numberOfDaysToConsiderToFetchInitially";
            public const string GET_EMPLOYEE_LEAVES = "GetEmployeeLeaves";
            public const string GET_GATEWAY_BASE_URL = "GatewayBaseUrl";
            public const string GET_ALLOCATION_INFORMATION_BY_LEAVES = "GetAllocationInformationByLeaves";
            public const string GET_PIPELINE_LIST_FROM_WCGT = "GetPipelineListFromWCGT";
            public const string GET_PROJECT_BUDGET_BY_MODIFIED_DATERANGE = "GetProjectBudgetByModifiedDateRange";
            public const string UPDATE_PROJECT_SUSPENSION_STATUS = "UpdateProjectSuspensionStatus";
            public const string GET_TASK_WORKFLOWS_URL = "GetTaskWorkflowsUrl";
            public const string UPDATE_WORKFLOW_TASK_URL = "UpdateWorkflowTaskUrl";
            public const string BULK_UPDATE_WORKFLOW_TASK_URL = "BulkUpdateWorkflowTaskUrl";
            public const string WORKFLOW_TASK = "WorkflowTasks";
            public const string REPORT_URL = "RefreshReportEmployee";
            public const string REFRESH_REPORT_VIEWS_URL = "RefreshReportViews";
            public const string REFRESH_REPORT_VIEWNAMES_URL = "RefreshReportViewName";//Comma(,) seperated values

            public const string WORKFLOW_TERMINATION_URL = "WorkflowTermination";
            public const string ClientId = "clientId";
            public const string ClientSecret = "clientSecret";
            public const string Authority = "authority";
            public const string Audience = "audience";
            public const string TenantId = "tenantId";
            public const string GET_JOB_LIST_FROM_WCGT = "GetJobListFromWCGT";
            //public const string GET_ALL_PROJECTS = "GetAllProjects";
            public const string GetAllProjectsForBudget = "GetAllProjectsForBudget";
            public const string GetAllProjectsForBudgetByJobCodes = "GetAllProjectsForBudgetByJobCodes";

            public const string GET_REQUISTION_BY_DATE = "GetRequisitionDetailsByDate";
            public const string PROJECT_ACTUAL_BUDGET_OVERSHOOT = "ProjectActualBudgetOverShoot";

            public const string SCHEDULER_MAPPING_FILEPATH = "SchedulerMappingFilePath";
            public const string BUDGET_OVERVIEW = "GetBudgetOverview";
            public const string UPDATEPROJECT_BUDGET = "UpdateProjectBudgetStatus";
            public const string AZURE_TOPIC_INIT = "AzureTopicInit";
            public const string DO_DETAILED_LOGGING = "DoDetailedLogging";
            public const string BLOB_Connection_Method = "BlobWCGTConnectionMethod";
            public const string BLOB_Connection_String = "BlobWCGTConnectionString";
            public const string BLOB_ContainerName = "BlobWCGTContainerName";
            public const string BLOB_WCGT_Sync_FileName = "BlobWCGTSyncFileName";

            public const string UPDATE_BUDGET_BATCHSIZE = "UpdateBudgetBatchSize";
            public const string DISABLE_PUBLISHSYNCEVENT = "DisablePublishSyncEvent";

            //  public const string WORKFLOW_TASK = "WorkflowTasks";
            public const string GET_CONFIGURATIONS_BY_CONFIG_TYPE = "GetConfigurationGroupByGroupNameAndConfigType";


            public const string GT360TokenApiUrl = "GT360TokenApiUrl";
            public const string GT360UserName = "GT360UserName";
            public const string GT360Password = "GT360Password";
            public const string GT360SubmitTimesheetApiUrl = "GT360SubmitTimesheetApiUrl";
            public const string UpdateExpiredMarketPlaceProjectsUrl = "UpdateExpiredMarketPlaceProjectsUrl";
            public const string UnpublishProjectFromMarketPlaceUrl = "UnpublishProjectFromMarketPlaceUrl";
            public const string GetConfirmedPerDayHoursByDate = "GetConfirmedPerDayHoursByDate";
            public const string GetProjectFullDetailsByUniqueCodes = "GetProjectFullDetailsByUniqueCodes";
            public const string TimesheetSchedulerDaysAdjustment = "TimesheetSchedulerDaysAdjustment";
            public const string MarkeplaceSchedulerDaysAdjustment = "MarkeplaceSchedulerDaysAdjustment";

            public const string GetProjectListedInMarketplaceByProjectListingDate = "GetProjectListedInMarketplaceByProjectListingDate";

            //public const string ClientId = "ClientId";//
            //public const string ClientSecret = "ClientSecret";//
            //public const string Authority = "Authority";//
            //public const string Audience = "Audience";//
            //public const string TenantId = "TenantId";//



            public const string oraclePassword = "oraclePassword";
            public const string oracleUserID = "oracleUserID";
            public const string oracleDataSource = "oracleDataSource";

            public const string UpdatePublishAllocationActualEfforts = "UpdatePublishAllocationActualEfforts";
            public const string dateTimePattern = "dateTimePattern";

            public const string ConfigDbConnectionString = "ConfigDbConnectionString";
            public const string GetOfferingSolutionsByJobCode = "GetOfferingSolutionsByJobCode";
            public const string AddEmployeeProjectMapping = "AddEmployeeProjectMapping";

            public const string dateTimePatternForOracle = "dateTimePatternForOracle";
            public const string executeAdditionalOracleCommands = "executeAdditionalOracleCommands";

            public const string TimesheetActualsBatchSize = "TimesheetActualsBatchSize";
            public const string BudhgetSchedulerForAllProject = "BudhgetSchedulerForAllProject";
            public const string WCGTDbConnectionString = "WCGTDbConnectionString";
            public const string TimesheetTableName = "TimesheetTableName";
            public const string TimesheetColumnName = "TimesheetColumnName";
            public const string TimesheetWhereColumnName = "TimesheetWhereColumnName";
        }
        
        public static class GT360Constants
        {
            public const string RecordSuccessMessage = "Record Inserted Successfully";

            public const int TokenServiceSuccessCode = 200;


        }
        public static class ConfigGroupExpertise
        {
            public const string WARNING_ALERT_CONDITION = "Amber_condition_for_Budget_Consumption";
            public const string BUDGET_ALERT_CONDITION_FOR_ALLOCATION = "Alert_condition_for_Allocation_Cost";
            public const string EXPERTISE_CONFIGTYPE = "EXPERTISE";

        }
        public static class EventConstants
        {
            public const string ROLL_FORWARD_EVENT = "PROJECT_ROLL_FORWARD";
            public const string NEW_PIPELINE_INSERTION_EVENT = "NEW_PIPELINE_INSERTION";
            public const string PROJECT_STATUS_CHANGE_EVENT = "PROJECT_STATUS_CHANGE";
            public const string PIPELINE_STATUS_WON_EVENT = "PIPELINE_STATUS_WON";
            public const string PIPELINE_STATUS_CLOSED_EVENT = "PIPELINE_STATUS_CLOSED";
            public const string PIPELINE_STATUS_SUSPENDED_EVENT = "PIPELINE_STATUS_SUSPENDED";
            public const string JOB_INSERT_EVENT = "JOB_INSERT_EVENT";
            public const string EMPLOYEE_LEAVE_EVENT = "EMPLOYEE_LEAVE_EVENT";
            public const string DESIGNATION_CHANGE_EVENT = "DESIGNATION_CHANGE_EVENT";
            public const string EMPLOYEE_STATUS_CHANGE_EVENT = "EMPLOYEE_STATUS_CHANGE_EVENT";
            public const string PIPELINE_CONVERTED_TO_JOB_EVENT = "PIPELINE_CONVERTED_TO_JOB";
            public const string PROJECT_ROLE_UPDATE = "PROJECT_ROLE_UPDATE";
            public const string PROJECT_PIPELINE_ROLE_UPDATE = "PROJECT_PIPELINE_ROLE_UPDATE";
            public const string EMPLOYEE_SUPERCOACH_CHANGE = "EMPLOYEE_SUPERCOACH_CHANGE";
            public const string EMPLOYEE_CO_SUPERCOACH_CHANGE = "EMPLOYEE_CO_SUPERCOACH_CHANGE";
            public const string PROJECT_ACTIVATION_STATUS_EVENT = "PROJECT_ACTIVATION_STATUS_EVENT";
            public const string PROJECT_CLOSURE_STATUS_EVENT = "PROJECT_CLOSURE_STATUS_EVENT";

        }

        public static class EventCategoryNames
        {
            public const string DATABASE_EVENT = "DatabaseEvent";
            public const string PUBLISH_EVENT = "PublishEvent";
        }

        public static List<string> DatabaseEventsNames = new List<string>()
        {
            EventConstants.PIPELINE_CONVERTED_TO_JOB_EVENT
        };

        public static class TableNameConstants
        {
            public const string PROJECT_TABLE_NAME = "Projects";
            public const string JOBS_TABLE_NAME = "Jobs";
            public const string PIPELINES_TABLE_NAME = "Pipelines";
            public const string USERS_TABLE_NAME = "USERS";
            public const string EMPLOYEES_TABLE_NAME = "Employees";
            public const string USER_ROLE_TABLE_NAME = "USER_ROLE";
            public const string ROLE_TABLE_NAME = "Role";
            public const string BUTREEMAPPING_TABLE_NAME = "BUTreeMappings";
            public const string JOB_ROLES_TABLE_NAME = "JobRoles";
            public const string PIPELINE_ROLES_TABLE_NAME = "PipelineRoles";
            public const string PROJECT_ROLES_TABLE_NAME = "ProjectRoles";

        }
        public static class TableColumnNames
        {
            public const string START_DATE = "start_date";
            public const string PROJECT_START_DATE = "StartDate";
            public const string PIPELINE_JOB_START_DATE = "job_start_date";
            public const string END_DATE = "end_date";
            public const string UPDATED_END_DATE = "updated_end_date";
            public const string PROJECT_END_DATE = "EndDate";
            public const string PIPELINE_JOB_END_DATE = "job_end_date";


            public const string JOB_CODE = "job_code";
            public const string PROJECT_JOB_CODE = "JobCode";

            public const string IS_ACTIVE = "isactive";
            public const string IS_ACTIVE_USER_TABLE = "is_active";
            public const string ProjectActivationStatus = "ProjectActivationStatus";
            public const string ProjectClosureState = "ProjectClosureState";
            public const string closed_job = "closed_job";
            public const string SUPERCOACH_MID = "supercoach_mid";
            public const string REPORTING_PARTNER_MID = "reporting_partner_mid";
            public const string CO_SUPERCOACH_MID = "co_supercoach_mid";

            public const string CREATED_BY = "createdby";
            public const string CREATED_BY_USERS = "created_by";
            public const string CREATED_AT = "createdat";
            public const string CREATED_AT_USERS = "created_at";

            public const string MODIFIED_BY = "modifiedby";
            public const string MODIFIED_AT = "modifiedat";
            public const string MODIFIED_AT_USER = "updated_at";
            public const string MODIFIED_BY_USER = "updated_by";
            public const string ID = "ID";
            public const string CHARGABLE_TYPE = "ChargableType";
            public const string PROJECT_TYPE = "ProjectType";
            public const string IS_RECURRING = "recurring";
            public const string RECURRING_TYPE = "recurring_type";
        }
        public static class SchedularLogsSyncStatus
        {
            public const string INPROGRESS = "INPROGRESS";
            public const string SYNCED = "SYNCED";
            public const string FAILED = "FAILED";
        }

        public static class SchedularLogsType
        {
            public const string Timesheet = "Timesheet";
            public const string SyncFunction = "SyncFunction";
            public const string BudgetStatusScheduler = "BudgetStatusScheduler";
        }

        public static class TypeConstant
        {
            public const string SQL_QUERY = "SQL_QUERY";
        }
    }
    public enum TASK_TITLES
    {
        //WFTASKTITLEEMPLOYEEALLOCATIONREVIEWERAPPROVAL /*= */"Employee Allocation Approval Task For Reviewer"
    }
}
