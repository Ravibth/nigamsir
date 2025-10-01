using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Infrastructure
{
    
    public class Constants
    {
        public class TimelineType
        {
            public const string LEAVE = "leave";
            public const string FULL_DAY_LEAVE = "FULL_DAY_LEAVE";
            public const string FIRST_HALF_LEAVE = "FIRST_HALF_LEAVE";
            public const string SECOND_HALF_LEAVE = "SECOND_HALF_LEAVE";
            public const string ALLOCATION = "allocation";
            public const string AVAILABLE = "available";
            public const string HOLIDAY = "holiday";
        }
        public class TimelineDisplayText
        {
            public const string LEAVE = "Leave";
            public const string FULL_DAY_LEAVE = "Leave";
            public const string FIRST_HALF_LEAVE = "First Half Leave";
            public const string SECOND_HALF_LEAVE = "Second Half Leave";
            public const string ALLOCATION = "Allocation";
            public const string AVAILABLE = "Available";
            public const string HOLIDAY = "Holiday";
        }

        public static class RequisitionSkillType
        {
            public const string OPTIONAL_SKILL = "Additional Optional Skills";
            public const string MANDATORY_SKILL = "Expertise skills (These are mandatory for requisition)";
        }

        public const string connectionString = "AllocationDB";
        public const string SystemSuggestionsSP = "sp_system_suggestions";
        public const string limit_count = "limit_count";
        public const string pagination = "pagination";
        public const string minimumPercentageForSystemSuggestions = "minimum_percentage_for_system_suggestions";
        public const string pref_weightage_constraint = "pref_weightage_constraint";
        public const string requisition_details = "requisition_details";
        public const string employee_details = "employee_details";
        public const string requisition_id = "requisition_id";
        public const string filter = "filter";
        public const string parameter_value_pairs = "parameter_value_pairs";
        public const string SystemSuggestionsSPResponse = "var_resp";
        public const string orderScoreBy = "order_score_by";

        public const string UserAvailabilitySP = "sp_availability_check";
        public const string ResourceAvailabilitySP = "sp_availability";
        public const string Leaves = "leaves";
        public const string StartDate = "start_date";
        public const string EndDate = "end_date";
        public const string TotalRequiredHours = "total_required_hours";
        public const string PerDayMaxEffort = "perday_max_effort";
        public const string UserAvailabilitySPResponse = "response";
        //public const int workinghourPerDay = 8; // duplicate workinghourPerDay
        public static Int64 WorkingHourPerDay = 8;//Default value
        public static Int64 HalfDayHours = 4;//Default value
        public static List<short> NonWorkingDays = new List<short>() { 0, 6 };//Default value
        public const string error_IsAvailableHour_PerDay = "{0} Hour is not available in these days [{1}]";
        public const string error_IsAvailableHour_TotalEffort = "{0} Hour is not available in above days.";
        public const string error_IsAvailableHour_Crossed_Last_Available_Day = "User is not available after {0}.";
        public const string sp_update_jobcode = "sp_jobcode_update";
        public const string pipeline_code_column_name = "pipeline_code";
        public const string job_code_column_name = "job_code";
        public const string new_job_code_column_name = "new_job_code";
        public const string modified_by_column_name = "modified_by";
        public const string new_pipeline_code_column_name = "new_pipeline_code";
        public const string new_job_name_column_name = "new_job_name";

        public const string UserAbscondedOrResignedType = "Not available";
        public static readonly string[] ALLOCATION_ACCEPT_STATUS = { "Employee Allocation Accepted By Reviewer","Employee Allocation Accepted By Supercoach" ,"Employee Allocation Accepted By Employee", "Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection" };


        public static class UserRoles
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Resource_Requestor = "Resource Requestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string CEOCOO = "CEOCOO";
            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
            public const string SystemAdmin = "SystemAdmin";
            public const string AdditionalEl = "AdditionalEl";
            public const string AdditionalDelegate = "AdditionalDelegate";
            public const string EngagementLeader = "EngagementLeader";
            public const string LeadGenerator = "LeadGenerator";
            public const string JobManager = "JobManager";
            public const string SMEGLeader = "SMEGLeader";
            public const string ProposedCSP = "ProposedCSP";
            public const string ProposedEL = "ProposedEL";
            public const string FindingPartner = "FindingPartner";
            public const string EO = "EO";
            public const string CSL = "CSL";
            public const string AssignmentIncharge = "AssignmentIncharge";
            public const string CSP = "CSP";
            public const string SuperCoach = "SuperCoach";
        }

        public const string AllocationDayFunction = "sp_allocationday_view";
        public const string AllocationDayResourceFunction = "sp_allocationday_resource_view";
        public const string AllocationDesignationSP = "sp_allocation_designation_view";
        public const string UpdateDesingationSP = "sp_update_designation_cost";

        /// <summary>
        /// USER_ALLOCATION_WORKFLOW Action Name
        /// </summary>
        public const string CreateUserAllocationWorkflowAction = "CREATE_USER_ALLOCATION_WORKFLOW";

        public static class WorkflowStatus
        {
            public const string EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER = "Employee Allocation Pending With Reviewer"; // 1.
            public const string EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH = "Employee Allocation Pending With Supercoach"; // 1.
            public const string EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH = "Employee Allocation Rejected By Supercoach"; // 1.
            public const string EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER = "Employee Allocation Accepted By Reviewer"; // 2.single step Wf Accepted
            public const string EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH = "Employee Allocation Accepted By Supercoach"; // 2.single step Wf Accepted

            public const string EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER = "Employee Allocation Rejected By Reviewer"; // 3.
            public const string EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE = "Employee Allocation Pending With Employee"; // 4.pending with employee
            public const string EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE = "Employee Allocation Accepted By Employee"; //5. Accepted
            public const string EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE = "Employee Allocation Rejected By Employee"; //6. done config
            public const string EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR = "Employee Allocation Rejected By Employee Pending For RR"; //7. done
            public const string EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE = "Employee Allocation Withdrawl By Employee"; //done left
            public const string EMPLOYEE_ALLOCATION_TERMINATION_BY_RR = "Employee Allocation Terminated By Resource Requestor";//8. 
            public const string EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Accepted Employee Rejection"; //done
            public const string EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Rejected Employee Rejection"; //done current + upcoming Pending with CSP
            //public const string EMPLOYEE_ALLOCATION_REVIEWER_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection"; //Employee is allocated successfully //done Approved
            //public const string Employee_ALLOCATION_REVIEWER_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Rejected Resource Requestor Rejected Employee Rejection"; //Employee is released successfully //done
            public const string EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Accepted Resource Requestor Rejected Employee Rejection"; //Employee is allocated successfully //done
            public const string Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Rejected Resource Requestor Rejected Employee Rejection"; //Employee is released successfully //done
            public const string WORKFLOW_NAME_ROLLOVER_ALLOCATION = "Employee Allocation Workflow By RollOver Allocations";
            public const string WORKFLOW_MODULE_EMPLOYEE_ALLOCATION = "Employee Allocation";
            public const string WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION = "Employee_Allocation_workflow";
            public const string WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER = "Employee Allocation Pending With Reviewer";
            public const string WORKFLOW_ENTITY_TYPE_RESOURCE_ALLOCATION_RESPONSE = "ResourceAllocationResponseDTO";
            public const string WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE = "Employee Allocation Pending With Employee";
            public const string EMPLOYEE_ALLOCATION_TERMINTION_DUE_TO_PROJECT_ROLL_FORWARD = "Employee Allocation Termination Due To Project Roll Forward";
            public const string WORKFLOW_TERMINATED_BY_JOBCODE_MOVE = "Employee Allocation Workflow Terminated By Job Code Move";

        }
        public class NotificationActions
        {
            public const string NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED = "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED";
            public const string ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER = "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER";
            public const string ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY = "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY";
            public const string ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY = "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY";
        }


        public static class ConfigurationTypes
        {
            public const string RESOURCE_ALLOCATION_REVIEW = "Resource_allocation_review";
            public const string SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP = "System_Suggestion_For_Requisition_Percentage_Match";
        }


    }


}