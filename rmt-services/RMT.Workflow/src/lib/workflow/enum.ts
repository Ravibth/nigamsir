export enum WorkflowOutCome {
  CLOSE = "close", // approve
  TERMINATE = "terminate", // close
  INPROGRESS = "inprogress", // reject
}
//for workflow task status
export enum WorkflowStatus {
  APPROVED = "approved",
  PENDING = "pending",
  REJECT = "rejected",
  COMPLETED = "completed",
  RUNNING = "running",
  TERMINATED = "terminated",
  FILE_UPLOADED = "file uploaded",
  FILE_SANITY_SUCCESS = "file sanity success",
  FILE_SANITY_FAILURE = "file sanity failure",

  FILE_CLEANSING_SUCCESS = "file cleansing success",
  FILE_CLEANSING_FAILURE = "file cleansing failure",

  FILE_MERGING_SUCCESS = "file merging success",
  FILE_MERGING_FAILURE = "file merging failure",

  FILE_INGESTION_SUCCESS = "file ingestion success",
  FILE_INGESTION_FAILURE = "file ingestion failure",
  // -----> NEW ADDED <-----
  APPROVED_BY_RESOURCE_REQUESTOR = "approved by resource requestor",
  ON_HOLD_RESOURCE_REQUESTOR = "on hold resource requestor",
  EMPLOYEE_ALLOCATION_REVIEWER_APPROVED = "Employee Allocation Approved By Reviewer", //NA
  EMPLOYEE_ALLOCATION_EMOPLOYEE_REJECTED = "Employee Allocation Rejected By Employee", //NA
}

export enum SeparatorEnum {
  SEPARATOR_PIPE = "|",
}

export enum ProxyApproval {
  SYSTEM_ADMIN = "SystemAdmin",
  ADMIN = "Admin",
  CEOCOO = "CEOCOO",
}

export enum WorklFlowModule {
  ENGAGEMENT = "engagement",
  FILE = "file",
  ONBOARDING = "onboarding",
  KPI = "kpi",
  // -----------------> NEW ADDED <----------------------------------
  EMPLOYEE_ALLOCATION = "Employee Allocation",
  WORKFLOW_MODULE_USER_SKILL_ASSESSMENT = "workflow_module_user_skill_assessment",
  WORKFLOW_MODULE_USER_SUPERCOACH_ASSESSMENT = "workflow_module_user_supercoach_assessment",
}
export enum RefreshWorkflowType {
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR = "employee_allocation_resource_requestor",
  WORKFLOW_MODULE_USER_SKILL_ASSESSMENT = "workflow_module_user_skill_assessment",
  WORKFLOW_MODULE_USER_SUPERCOACH_ASSESSMENT = "workflow_module_user_supercoach_assessment",
}

export enum WorklFlowSubModule {
  ENGAGEMENT = "engagement-workflow",
  FILE_UPLOAD = "file-upload",
  FILE_WORKFLOW = "file-workflow",
  FILE_SANITY = "file-sanity",
  FILE_CLEANSING = "file-cleansing",
  FILE_INGESTION = "file-ingestion",
  FILE_MERGE = "file-merge",
  ONBOARDING = "onboarding-approval-workflow",
  KPI_WORKFLOW = "kpi-workflow",
  KPI_EXECUTION_WORKFLOW = "kpi-execution-workflow",
  // -----------------> NEW ADDED <----------------------------------
  EMPLOYEE_ALLOCATION = "Employee_Allocation_workflow",
  EMPLOYEE_ALLOCATION_UPDATE = "employee_allocation_update_workflow",
  WORKFLOW_SUB_MODULE_USER_SKILL_ASSESSMENT = "workflow_sub_module_user_skill_assessment",
}

export enum EWorkflowSteps {
  Partner_Approval = "Partner Approval",
  Business_SPOC = "Business SPOC Approval",
  COE_ADMIN = "COE_ADMIN",
  FILE_UPLOAD = "File Upload",
  FILE_WORKFLOW = "FILE_WORKFLOW",
  FILE_SANITY = "File Sanity",
  FILE_CLEANSING = "File Cleansing",
  FILE_MERGE = "File Merge",
  FILE_INGESTION = "File Ingestion",
  // ----> NEW ADDED <----------------
  EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL = "Employee Allocation Reviewer Approval",
  EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL = "Employee Allocation Supercoach Approval",
  EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL = "Employee Allocation Employee Approval",
  EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL_OVER_EMPLOYEE_REJECTION = "Employee Allocation Employee Withdrawl Over Employee Rejection",
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_APPROVAL_ON_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Approval Over Employee Rejection",
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_TERMINATION_ON_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Termination Over Employee Rejection",
  // EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Approval Over Resource Requestor Rejection",
  EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Approval Over Resource Requestor Rejection",

  USER_SKILL_ASSESSMENT_PENDING_FOR_SUPERCOACH = "User Skill Assessment Pending for Supercoach",
  USER_SKILL_ASSESSMENT_PENDING_FOR_CO_SUPERCOACH = "User Skill Assessment Pending for Co-Supercoach",
}

export const SupercoachPendingTaskTitle = [
  EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL,
  EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION,
];
export enum EngagementApproverType {
  PARTNER = "Partner",
  BusinessSPOC = "Business SPOC",
  COE_ADMIN = "COE Admin",
  COE_TECH_TEAM = "COE Tech Team",
  ENGAGEMENT_CREATOR = "Engagement Creator",
}

export enum WorkFlowTaskTitle {
  WF_TASK_TITLE_PARTNER = "Engagement Approval task for Partner",
  WF_TASK_TITLE_SPOC = "Engagement Approval task for business SPOC",
  WF_TASK_TITLE_COE = "COE admin acceptance",
  FILE_UPLOAD = "File upload by EC and COE Tech Team",
  FILE_SANITY = "Sanity for DEP",
  FILE_CLEANSING = "Cleansing for DEP",
  FILE_MERGE = "Merging for DEP",
  FILE_INGESTION = "Ingestion for DEP",
  KPI_APPROVAL_SPOC = "Kpi Approval task for Business SPOC",
  KPI_EXECUTION = "Kpi Executed",
  KPI_EXECUTION_SELECT = "Kpi Selected for engagement",
  KPI_EXECUTION_REVIEW = "Kpi ready for review",
  KPI_EXECUTION_REVISION = "Kpi revision required",

  KPI_EXECUTION_ET_INPUT = "ET input pending",
  KPI_EXECUTION_CONFIRMED = "Confirmed by ET",
  KPI_EXECUTION_INPUT_PROVIDED = "input provided by ET",
  // -----> NEW ADDED <------
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL = "Employee Allocation Approval Task For Reviewer",
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL = "Employee Allocation Approval Task For Employee",
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL = "Employee Allocation Withdrawl Task For Employee",
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION = "Employee Allocation Task For Resource Requestor After Employee Rejection",
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION = "Employee Allocation Task For Resource Requestor After Employee Rejection For Termination",
  WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_ = "User Skill Assessment Task For Supercoach after Employee added or updated a skill",
  WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_CO_SUPERCOACH_ = "User Skill Assessment Task For Co-Supercoach after Employee added or updated a skill",
  WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL = "Employee Allocation Approval Task For Supercoach",
  WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Task For Reviewer After Resource Requestor Rejection",

  // WF_TASK_EMPLOYEE_ALLOCATION_REVIEWER_AFTER_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Task For Reviewer After Resource Requestor Rejection",
  WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Task For Supercoach After Resource Requestor Rejection",
}

export const WF_TASK_TITLE_APPROVAL_PENDING = [
  WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.toString(),
  WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL.toString(),
  WorkFlowTaskTitle.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION.toString(),
];

export type EWorkflowStep = EWorkflowSteps | WorkFlowTaskTitle;

export enum EEngagementStatus { // workflow status
  onboard = "Engagement Onboarded",
  submitted = "Draft",
  business_spoc_approved = "Engagement Onboarded",
  business_spoc_reject = "Engagement On Hold",
  partner_reject = "Partner Approval on Hold",
  partner_approved = "Pending for Business SPOC Approval",
  partner_pending = "Pending for Partner Approval",
  accepted_by_coe_admin = "Accepted By COE",
  in_progress_data_processing = "In Progress-Data Processing",

  in_progress_file_upload = "In Progress-File Upload",
  in_progress_file_sanity = "In Progress-File Sanity",
  in_progress_file_sanity_failed = "Failed-File Sanity",
  in_progress_file_sanity_success = "Complete-File Sanity",
  kpi_approval_pending = "Pending for Business SPOC Review",
  approved = "approved",
  reject = "reject",

  coe_accepted = "COE Accepted",
  engagement_accepted = "Engagement Accepted",
  ENGAGEMENT_DELIVERED = "Delivered",
  ENGAGEMENT_CLOSE = "Closed",
  KPI_EXECUTION_SELECT = "Selected for Engagement",
  KPI_EXECUTION_EXECUTED = "Executed",
  KPI_EXECUTION_ET_INPUT = "Pending with ET for Inputs",
  KPI_EXECUTION_COMPLETED = "Completed",
  KPI_EXECUTION_DROPPED = "Dropped",
  KPI_EXECUTION_REVISION = "Revision Required",
  KPI_EXECUTION_INPUT_PROVIDE = "Inputs Provided",
  KPI_EXECUTION_CONFIRMED_ET = "Confirmed by ET",
  KPI_EXECUTION_READY_FOR_REVIEW = "Ready for ET Review",
  KPI_EXECUTION_RUNNING = "Running",
  KPI_EXECUTION_FAILED = "Failed",
  ////////////////////////////////
  // --------------------------------> NEW ADDED <----------------------------------------------------------------
  EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER = "Employee Allocation Pending With Reviewer", //done
  EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH = "Employee Allocation Pending With Supercoach", //done

  EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER = "Employee Allocation Accepted By Reviewer", //TODO: should be added in ui()
  EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH = "Employee Allocation Accepted By Supercoach", //TODO: should be added in ui()
  EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER = "Employee Allocation Rejected By Reviewer", //TODO: should be added in ui
  EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH = "Employee Allocation Rejected By Supercoach", //TODO: should be added in ui

  EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE = "Employee Allocation Pending With Employee", //done
  EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE = "Employee Allocation Accepted By Employee", //done
  EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE = "Employee Allocation Rejected By Employee", //done config
  EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR = "Employee Allocation Rejected By Employee Pending For RR", //done
  EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE = "Employee Allocation Withdrawl By Employee", //done
  EMPLOYEE_ALLOCATION_TERMINATION_BY_RR = "Employee Allocation Terminated By Resource Requestor",
  EMPLOYEE_ALLOCATION_TERMINTION_DUE_TO_PROJECT_ROLL_FORWARD = "Employee Allocation Termination Due To Project Roll Forward",
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Accepted Employee Rejection", //done
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Rejected Employee Rejection", //done current + upcoming Pending with CSP
  // EMPLOYEE_ALLOCATION_REVIEWER_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection", //Employee is allocated successfully //done
  // Employee_ALLOCATION_REVIEWER_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Rejected Resource Requestor Rejected Employee Rejection", //Employee is released successfully //done
  EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Accepted Resource Requestor Rejected Employee Rejection", //Employee is allocated successfully //done
  Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Rejected Resource Requestor Rejected Employee Rejection", //Employee is released successfully //done

  //-----------------------------> Skill Assessment Workflow <------------------------------
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_PENDING = "Skill Assessment Pending With Supercoach",
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_APPROVED = "Skill Assessment Approved by Supercoach",
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_REJECTED = "Skill Assessment Rejected by Supercoach",
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_PENDING = "Skill Assessment Pending With Co-Supercoach",
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_APPROVED = "Skill Assessment Approved by Co-Supercoach",
  WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_REJECTED = "Skill Assessment Rejected by Co-Supercoach",
}

export enum EWorkflowTaskAssignedType {
  ROLE = "Role",
  USER = "User",
}

export enum EUpdateInMSType {
  COE_ACCEPTANCE = "COE_ACCEPTANCE",
}

export enum GTSystemCredentials {
  GT_SYSTEM_EMAIL_ID = "System@gt.com",
  GT_SYSTEM_NAME = "System",
}
export enum WorkflowCommentsEnum {
  EMPLOYEE_WITHDRAWL_COMMENTS = "EMPLOYEE_WITHDRAWS_ALLOCATION_REJECTION",
}
export enum ConfigurationGroupName {
  RESOURCE_ALLOCALTION_REVIEWER = "Resource_allocation_review",
  ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Employee_Response_Review_process_reviewed_by_Supercoach",
  ALLOCATION_PENDING_WITH_EMPLOYEE = "Number_of_days_for_employee_to_confirm_on_their_allocations",
  ALLOCATION_WITHDRAWL_PENDING_WITH_EMPLOYEE = "Allocation_pending_with_employee_for_withdrawl",
  ALLOCATION_RESOURCE_REQUESTOR_TO_ACCEPT_ALLOCATION = "Employee_Response_Review_process_reviewed_by_Resource_Requestor",
  ALLOCATION_CSP_TO_ACT_ON_RESOURCE_REQUESTOR_REJECTION = "Allocation_csp_to_act_on_resource_requestor_rejection",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUEDATE = "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE",
}

export enum AllocationMetaTypes {
  UPDATE_ALLOCATION_RESPONSE = "UpdateAllocationResponseDTO",
  ROLL_OVER_ALLOCATION = "RollOverResponseDTO",
  RESOURCE_ALLOCATION_RESPONSE = "ResourceAllocationResponseDTO",
}

export enum NotificationTemplateTypes {
  // 1,2 - UC002 - New Job/ Pipeline Notification (Added from WCGT) to Resource Requestor
  NEW_JOB_ADDED_FROM_WCGT = "NEW_JOB_ADDED_FROM_WCGT",

  // 3,4 - UC007 - Assignment of Delegate by Requestor
  ASSIGNMENT_OF_DELEGATE_BY_REQUESTOR = "ASSIGNMENT_OF_DELEGATE_BY_REQUESTOR",

  // 5,6 - UC040 - Assignment of Additional EL by Requestor
  ASSIGNMENT_OF_ADDITIONAL_EL_BY_REQUESTOR = "ASSIGNMENT_OF_ADDITIONAL_EL_BY_REQUESTOR",

  // 7,8 - UC041 - Assignment of Additional Delegate by Additional EL
  ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL = "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL",

  // 9,10 - UC002 - X days before Project start date if Project status is not WON
  X_DAYS_BEFORE_PROJECT_START_DATE = "X_DAYS_BEFORE_PROJECT_START_DATE",

  // 11-12 - UC002 - Job/ Pipeline Update Notification(Update in project details from WCGT)
  JOB_UPDATED_FROM_WCGT = "JOB_UPDATED_FROM_WCGT",

  // 13,14 - UC039 - Project End Date Update (done in RMS)
  PROJECT_END_DATE_UPDATE = "PROJECT_END_DATE_UPDATE",

  // 15,16 - UC039 - Project End Date Update - End Date rolled back, allocations updated accordingly
  PROJECT_END_DATE_UPDATE_ROLLED_BACK = "PROJECT_END_DATE_UPDATE_ROLLED_BACK",

  // 17,18 - UC038 - Assignment of Additional EL by Delegate
  ASSIGNMENT_OF_ADDITIONAL_EL_BY_DELEGATE = "ASSIGNMENT_OF_ADDITIONAL_EL_BY_DELEGATE",

  // 19,20 - UC004 - Notification to Employee for Project Listing in Marketplace (Fixed Frequency)
  NOTIFICATION_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE = "NOTIFICATION_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE",

  // 21,22 - UC004 - Project being no longer available in the Marketplace
  PROJECT_NO_LONGER_AVAILABLE_IN_MARKETPLACE = "PROJECT_NO_LONGER_AVAILABLE_IN_MARKETPLACE",

  // 23,24 - UC043 - Notification for interests in marketplace against their project
  NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT = "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT",

  // 25,26 - UC043 - MP Interests Summary Notification  post removal of project from MP
  MP_INTERESTS_SUMMARY_NOTIFICATION_POST_REMOVAL_OF_PROJECT_FROM_MP = "MP_INTERESTS_SUMMARY_NOTIFICATION_POST_REMOVAL_OF_PROJECT_FROM_MP",

  // 27,28 - UC020 - Project roll forward notification
  PROJECT_ROLL_FORWARD_NOTIFICATION = "PROJECT_ROLL_FORWARD_NOTIFICATION",

  // 29,30 - UC020 - Roll Forward of allocations done by the user
  ROLL_FORWARD_OF_ALLOCATIONS_DONE_BY_THE_USER = "ROLL_FORWARD_OF_ALLOCATIONS_DONE_BY_THE_USER",

  // 31,32 - UC024 - Project Suspension Notification
  PROJECT_SUSPENSION_NOTIFICATION = "PROJECT_SUSPENSION_NOTIFICATION",

  // 33,34 - UC024 - Employee release due to suspension of project
  EMPLOYEE_RELEASE_DUE_TO_SUSPENSION_OF_PROJECT = "EMPLOYEE_RELEASE_DUE_TO_SUSPENSION_OF_PROJECT",

  // 35,36 - UC028 - Resource Requestor to be notified for any allocation conflicts within last 30 working days of employee in the organization.
  RESOURCE_REQUESTOR_TO_BE_NOTIFIED_FOR_ANY_ALLOCATION_CONFLICTS_WITHIN_LAST_30_WORKING_DAYS_OF_EMPLOYEE_IN_THE_ORGANIZATION = "RESOURCE_REQUESTOR_TO_BE_NOTIFIED_FOR_ANY_ALLOCATION_CONFLICTS_WITHIN_LAST_30_WORKING_DAYS_OF_EMPLOYEE_IN_THE_ORGANIZATION",

  // 37,38 - UC028 - Resource Requestor to be notified  if Employee on Leave/ Absconding during Project Allocation
  RESOURCE_REQUESTOR_TO_BE_NOTIFIED__IF_EMPLOYEE_ON_LEAVE_OR_ABSCONDING_DURING_PROJECT_ALLOCATION = "RESOURCE_REQUESTOR_TO_BE_NOTIFIED__IF_EMPLOYEE_ON_LEAVE_OR_ABSCONDING_DURING_PROJECT_ALLOCATION",

  // 39,40 - UC005 - Requisition created
  REQUISITION_CREATED = "REQUISITION_CREATED",

  // 41,42 - UC010 - Notification for allocation of resources to a project (Reviewer if configuration is enabled) for reviewer
  NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT",

  // 43,44 - UC010 - Notification for allocation of a resources to a project (Notification directly to the employee for confirmation) for employee
  NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT",

  // 45,46 - UC012 - Notification for allocation update of resources to a project (Reviewer if configuration is enabled) for reviewer
  NOTIFICATION_TO_REVIEWER_UPDATE_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_REVIEWER_UPDATE_ALLOCATION_OF_RESOURCE_TO_PROJECT",

  // 47,48 - UC029 - Notification to employee for updating time-sheet once RMS sends daily allocation update to RMS
  NOTIFICATION_TO_EMPLOYEE_FOR_UPDATING_TIME_SHEET_ONCE_RMS_SENDS_ALLOCATION_UPDATE_TO_RMS = "NOTIFICATION_TO_EMPLOYEE_FOR_UPDATING_TIME_SHEET_ONCE_RMS_SENDS_ALLOCATION_UPDATE_TO_RMS",

  // 49,50 - UC021 - User being onboarded
  USER_BEING_ONBOARDED = "USER_BEING_ONBOARDED",

  // 51,52 - UC018 - User roles & permissions update saved
  USER_ROLES_PERMISSIONS_UPDATE_SAVED = "USER_ROLES_PERMISSIONS_UPDATE_SAVED",

  // 53,54 - UC017 - Reminder to Reviewer to take action pending request (for no action - auto-approval)
  REMINDER_TO_REVIEWER_TO_TAKE_ACTION_PENDING_REQUEST_FOR_NO_ACTION_AUTO_APPROVAL = "REMINDER_TO_REVIEWER_TO_TAKE_ACTION_PENDING_REQUEST_FOR_NO_ACTION_AUTO_APPROVAL",

  // 55,56 - UC017 - Resource Allocation Review Notification - Approved by Reviewer
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER",

  // 57,58 - UC017 - Resource Allocation Review Notification - Approved by Reviewer- Notification to Requestor
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR",

  // 59,60 - UC017 - Resource Allocation Review Notification - Auto-approved - Notification to Reviewer
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER",

  // 61,62 - UC017 - Resource Allocation Review Notification - Auto-approved - Notification to Requestor
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR",

  // 63,64 - UC017 - Resource Allocation Review Notification - Reject -  Notification to Reviewer
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECT_NOTIFICATION_TO_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECT_NOTIFICATION_TO_REVIEWER",

  // 65,66 - UC017 - Resource Allocation Review Notification - Rejected - Notification to Requestor
  RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECTED_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECTED_NOTIFICATION_TO_REQUESTOR",

  // 67,68 - UC025 - Project Allocation Review (Resource) - Approved by Employee
  PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE",

  // 69,70 - UC025 - Project Allocation Review (Resource) - Notification to Resource Requestor once Approved by Employee
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE",

  // 71,72 - UC025 - Project Allocation Review (Resource) - Reminder to Employee
  PROJECT_ALLOCATION_REVIEW_RESOURCE_REMINDER_TO_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_REMINDER_TO_EMPLOYEE",

  // 73,74 - UC025 - Project Allocation Review (Resource)- Auto Approve for employee
  PROJECT_ALLOCATION_REVIEWER_RESOURCE_AUTO_APPROVE_FOR_EMPLOYEE = "PROJECT_ALLOCATION_REVIEWER_RESOURCE_AUTO_APPROVE_FOR_EMPLOYEE",

  // 75,76 - UC025 - Project Allocation Review (Resource)-  Notification to Resource Requestor once Auto-Approved for employee
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_AUTO_APPROVED_FOR_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_AUTO_APPROVED_FOR_EMPLOYEE",

  // 77,78 - UC025 - Project Allocation Review (Resource) - Rejected by Employee ( Configuration for review by Requestor is OFF) TO EMPLOYEE
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF",

  // 79,80 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is OFF)
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF",

  // 81,82 - UC025 - Project Allocation Review (Resource) - Rejected by Employee ( Configuration for review by Requestor is ON)
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON",

  // 83,84 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON",

  // 85,86 - UC025 - Project Allocation Review (Resource) - Withdrawal by Employee post rejection of allocation
  PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION = "PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION",

  // 87,88 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once withdrawal by Employee
  PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE",

  // 89,90 - UC025 - Employee Rejection approved by Requestor - Notification to Requestor
  EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR = "EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR",

  // 91,92 - UC025 - Employee Rejection approved by  Requestor  - Notification to Employee
  EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE = "EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE",

  // 93,94 - UC026 - Reminder to Requestor to review rejection from employee
  REMINDER_TO_REQUESTOR_TO_REVIEW_REJECTION_FROM_EMPLOYEE = "REMINDER_TO_REQUESTOR_TO_REVIEW_REJECTION_FROM_EMPLOYEE",

  // 95,96 - UC026 - Auto-approve Employee Rejection - Notification to Requestor
  AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_REQUESTOR = "AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_REQUESTOR",

  // 97,98 - UC026 - Auto-approve Employee Rejection - Notification to Employee
  AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_EMPLOYEE = "AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_EMPLOYEE",

  // 99,100 - UC026 - Employee Rejection rejected by Requestor - Notification to Requestor
  EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR = "EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR",

  // 101,102 - UC026 - Employee Rejection rejected by Requestor - Escalation Notification to Reviewer
  EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER = "EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER",

  // 103,104 - UC026 - Reviewer to Accept Requestor Response -  Notification to Reviewer
  REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER",

  // 105,106 - UC026 - Reviewer to Accept Requestor Response -  Notification to Requestor
  REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR",

  // 107,108 - UC026 - Reviewer to Accept Requestor Response -  Notification to Employee
  REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE",

  // 109,110 - UC026 - Reminder to Reviewer to take action on pending requests for requestor response
  REMINDER_TO_REVIEWER_TO_TAKE_ACTION_ON_PENDING_REQUEST_FOR_REQUESTOR_RESPONSE = "REMINDER_TO_REVIEWER_TO_TAKE_ACTION_ON_PENDING_REQUEST_FOR_REQUESTOR_RESPONSE",

  // 111,112 - UC026 - Auto- approval of requests escalated to Reviewer - notification to Reviewer
  AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REVIEWER = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REVIEWER",

  // 113,114 - UC026 - Auto- approval of requests escalated to Reviewer -notification to Requestor
  AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REQUESTOR = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REQUESTOR",

  // 115,116 - UC026 - Auto- approval of requests escalated to Reviewer -notification to Employee
  AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_EMPLOYEE = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_EMPLOYEE",

  // 117,118 - UC026 - Reviewer to Reject Requestor Response - Notification to Reviewer
  REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER",

  // 119,120 - UC026 - Reviewer to Reject Requestor Response -  Notification to Requestor
  REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR",

  // 121,122 - UC026 - Reviewer to Reject Requestor Response -  Notification to Employee
  REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE",

  // 123,124 - UC018 - If a person is deactivated from the Roles & permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
  USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR = "USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR",
  // 130,131 - UC028 - Employee shall be  notified for submission of their skill update request sent to Supercoach.
  EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH = "EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH",
  // 132,133 - UC028 - ACCEPT Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
  SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST = "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST",
  // 134,135 - UC028 - REJECT- Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
  SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST = "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST",
  //153 , 154 Employee Rejection approved by  Requestor  - Notification to Employee
  NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION = "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION",
  //153 , 154 Employee Rejection auto approved by  Requestor  - Notification to Employee
  NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION = "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION",
  // 149 , 150 Project Allocation Review (Resource)- Auto Approve for employee.Auto-allocation alert to employee.
  NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS = "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS",
  // 147 ,148 , 151 , 152 Project Allocation Review (Resource) - Approved by Employee
  NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS = "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS",
  // 145 , 146 Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
  NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON = "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON",
  // 143 , 144 Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
  NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION = "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION",
  // 141 , 142 Resource Allocation Review Notification - Rejected - Notification to Requestor
  EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR = "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR",
  // 42 , 43  Notification for allocation of a resources to a project (Notification directly to the employee , config for Review is OFF )
  NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT = "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT",
  //157 , 158 Reviewer to Accept Employee Response -  Notification to Employee
  NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION = "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION",
  //139 , 140 Notification for allocation of a resources to a project (Notification directly to the employee once allocation is updated)
  NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE = "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE",
  //137, 138
  REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE = "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE",
  // NOTIFICATION TO EMPLOYEE AFTER EMPLOYEE ACCEPTS
  NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS = "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS",
  //NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION
  NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION = "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION",
  //R17
  PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF = "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF",
  // R4
  PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS = "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS",
  //R6
  PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE = "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE",
  PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE = "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE",
  PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE = "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE",
}
