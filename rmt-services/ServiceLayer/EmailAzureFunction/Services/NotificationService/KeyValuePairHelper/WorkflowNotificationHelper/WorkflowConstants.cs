using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper
{
    public static class WorkflowConstants
    {
        /// <summary>
        /// Titles For Employee Allocation Workflow
        /// </summary>
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL = "Employee Allocation Approval Task For Reviewer";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL = "Employee Allocation Approval Task For Employee";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL = "Employee Allocation Withdrawl Task For Employee";
        public const string WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION = "Employee Allocation Task For Resource Requestor After Employee Rejection";
        public const string WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Task For Reviewer After Resource Requestor Rejection";
        public const string WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_ = "User Skill Assessment Task For Supercoach after Employee added or updated a skill";

        /// <summary>
        /// WORKFLOW MODULE
        /// </summary>
        public const string WORKFLOW_MODULE_EMPLOYEE_ALLOCATION = "Employee Allocation";
        /// <summary>
        ///WORKFLOW SUB_MODULE
        /// </summary>
        public const string WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION = "Employee_Allocation_workflow";
        public const string WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION_UPDATE = "employee_allocation_update_workflow";
        /// <summary>
        /// WORKFLOW STATUS
        /// </summary>
        public const string WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER = "Employee Allocation Pending With Reviewer";
        public const string WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE = "Employee Allocation Pending With Employee";
        /// <summary>
        /// WORKFLOW NAME
        /// </summary>
        public const string WORKFLOW_NAME_UPDATE_ALLOCATION_BY_ID = "Employee Allocation Workflow By Update Allocation By Id";
        public const string WORKFLOW_NAME_UPDATE_ROLL_OVER_ALLOCATIONS = "Employee Allocation Workflow By Update Roll Over Allocations";
        public const string WORKFLOW_NAME_SAME_TEAM_ALLOCATION = "Employee Allocation Workflow By Same Team Allocations";
        /// <summary>
        /// WORKFLOW ENTITY_TYPE
        /// </summary>
        public const string WORKFLOW_ENTITY_TYPE_UPDATE_ALLOCATION_RESPONSE = "UpdateAllocationResponseDTO";
        public const string WORKFLOW_ENTITY_TYPE_ROLL_OVER_ALLOCATION = "RollOverResponseDTO";
        public const string WORKFLOW_ENTITY_TYPE_RESOURCE_ALLOCATION_RESPONSE = "ResourceAllocationResponseDTO";

        public const string WORKFLOW_TASK_STATUS_APPROVED = "approved";
        public const string WORKFLOW_TASK_STATUS_PENDING = "pending";
        public const string WORKFLOW_TASK_STATUS_REJECT = "rejected";
        public const string WORKFLOW_TASK_STATUS_TERMINATED = "terminated";
    }
}
