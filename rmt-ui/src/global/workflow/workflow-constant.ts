//UI STATUS
export enum UiAllocationStatus {
  PENDING_WITH_REVIEWER = "Pending Reviewer Confirmation",
  PENDING_WITH_SUPERCOACH = "Pending Supercoach Confirmation",
  PENDING_WITH_EMPLOYEE = "Pending Resource Confirmation",
  PENDING_RESPONSE_REVIEW_BY_REQUESTOR = "Pending Response Review by Requestor",
  PENDING_RESPONSE_REVIEW_BY_SUPERCOACH = "Pending Response Review by Supercoach",
  ALLOCATION_COMPLETE = "Allocation Complete",
  Allocation_Rejected_by_Employee = "Allocation Rejected by Employee",
}
// WORKFLOW STATUS
export enum WorkflowStatus {
  EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER = "Employee Allocation Pending With Reviewer",
  EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH = "Employee Allocation Pending With Supercoach", //TODO :- WFCHANGE
  EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE = "Employee Allocation Pending With Employee",
  EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH = "Employee Allocation Accepted By Supercoach", //TODO :- WFCHANGE
  EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER = "Employee Allocation Rejected By Reviewer",
  EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH = "Employee Allocation Rejected By Supercoach", //TODO :- WFCHANGE
  EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER = "Employee Allocation Accepted By Reviewer",
  EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE = "Employee Allocation Accepted By Employee",
  EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE = "Employee Allocation Rejected By Employee",
  EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR = "Employee Allocation Rejected By Employee Pending For RR",
  EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE = "Employee Allocation Withdrawl By Employee",
  EMPLOYEE_ALLOCATION_TERMINATION_BY_RR = "Employee Allocation Terminated By Resource Requestor", //termination condition
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Accepted Employee Rejection",
  EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION = "Employee Allocation Resource Requestor Rejected Employee Rejection",
  // EMPLOYEE_ALLOCATION_REVIEWER_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection",
  // Employee_ALLOCATION_REVIEWER_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Reviewer Rejected Resource Requestor Rejected Employee Rejection",
  EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Accepted Resource Requestor Rejected Employee Rejection", //TODO :- WFCHANGE
  Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION = "Employee Allocation Supercoach Rejected Resource Requestor Rejected Employee Rejection", //TODO :- WFCHANGE
}

export enum SkillWorkflow {
  APPROVED_SKILL = "APPROVED",
  REJECTED_SKILL = "REJECTED",
  PENDING = "PENDING",
}

export enum WorkflowStatusColor {
  PENDING_REVIEWER_CONFIRMATION = "#ffe5d2",
  PENDING_RESOURCE_CONFIRMATION = "#f9ab73",
  PENDING_RESPONSE_REVIEW_BY_REQUESTOR = "#82d5dd",
  PENDING_RESPONSE_REVIEW_BY_SUPERCOACH = "#82d5dd",
  ALLOCATED = "#c1d8b2",
  REJECTED = "#F2641630",
  DRAFT = "rgba(0, 0, 0, 0.08)",
}

export enum WorkflowStatusPrimaryColor {
  PENDING_REVIEWER_CONFIRMATION = "#9b7400",
  PENDING_RESOURCE_CONFIRMATION = "#933d00",
  PENDING_RESPONSE_REVIEW_BY_REQUESTOR = "#005e66",
  PENDING_RESPONSE_REVIEW_BY_SUPERCOACH = "#005e66",
  ALLOCATED = "#317e01",
  REJECTED = "#e60000",
  DRAFT = "#000",
}

export enum WORKFLOW_MODULE {
  EMPLOYEE_ALLOCATION = "Employee Allocation",
  WORKFLOW_MODULE_USER_SKILL_ASSESSMENT = "workflow_module_user_skill_assessment",
}
