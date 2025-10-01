export const ExpertiseData = [
  {
    id: 1,
    configGroup: "Resource allocation review",
    isAll: false,
    allValue: true,
    attributeName: "EXPERTISE_1",
    configName: "Resource allocation review",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: {
      activationStatus: "BOOLEAN",
      noOfDays: "INTEGER",
    },
    attributeValue: {
      activationStatus: true,
      noOfDays: 9,
    },
  },
  {
    id: 2,
    configGroup: "Weightage for parameters for System Suggested Requisition",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_1",
    configName: "LOCATION",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 8,
  },
  {
    id: 3,
    configGroup: "Resource allocation review",
    isAll: false,
    allValue: true,
    attributeName: "EXPERTISE_2",
    configName: "Resource allocation review",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: {
      activationStatus: "BOOLEAN",
      noOfDays: "NUMBER",
    },
    attributeValue: {
      activationStatus: true,
      noOfDays: 12,
    },
  },
  {
    id: 4,
    configGroup: "Resource allocation review",
    isAll: false,
    allValue: true,
    attributeName: "EXPERTISE_3",
    configName: "Resource allocation review",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: {
      activationStatus: "BOOLEAN",
      noOfDays: "NUMBER",
    },
    attributeValue: {
      activationStatus: true,
      noOfDays: 10,
    },
  },
  {
    id: 5,
    configGroup: "Weightage for parameters for System Suggested Requisition",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_1",
    configName: "SME",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 8,
  },
  {
    id: 6,
    configGroup: "Weightage for parameters for System Suggested Requisition",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_2",
    configName: "LOCATION",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 4,
  },
  {
    id: 7,
    configGroup: "No of days where project is available in Marketplace",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_1",
    configName: "No of days where project is available in Marketplace",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 14,
  },
  {
    id: 8,
    configGroup: "No of days where project is available in Marketplace",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_2",
    configName: "No of days where project is available in Marketplace",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 15,
  },
  {
    id: 9,
    configGroup: "No of days where project is available in Marketplace",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_3",
    configName: "No of days where project is available in Marketplace",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 9,
  },
  {
    id: 10,
    configGroup:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_1",
    configName:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 10,
  },
  {
    id: 11,
    configGroup:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_2",
    configName:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 5,
  },
  {
    id: 12,
    configGroup:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    isAll: false,
    allValue: 6,
    attributeName: "EXPERTISE_3",
    configName:
      "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
    // configKey: "1"
    ConfigType: "EXPERTISE",
    valueType: "NUMBER",
    attributeValue: 19,
  },
];

export enum ConfigGroupEnum {
  RESOURCE_ALLOCATION_GROUP = "Resource allocation review by Reviewer",
  RESOURCE_ALLOCATION_DB_GROUP = "Resource_allocation_review",
  SYSTEM_SUGGESTED_GROUP = "Weightage for Requisition Form Parameters",
  SYSTEM_SUGGESTED_DB_GROUP = "Weightage_for_parameters_for_System_Suggested_Requisition",
  DAYS_PROJECT_IN_MP_GROUP = "No of days where project is available in Marketplace",
  DAYS_PROJECT_IN_MP_DB_GROUP = "No_of_days_where_project_is_available_in_Marketplace",
  DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR = "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
  DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR_DB_GROUP = "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON",
  MAX_NO_OF_ITEMS_FOR_EMPLOYEE_PREFERENCE_SELECTION = "Maximum number of items to be selected for employee preference",
  MAX_NO_OF_ITEMS_FOR_EMPLOYEE_PREFERENCE_SELECTION_DB_GROUP = "Maximum_number_of_items_for_employee_preference",
  REQUISITION_FORM = "Requisition Form Parameters ",
  REQUISITION_FORM_DB_GROUP = "Requisition_form",
  SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH = "Match Range  for System Suggestions for a Requisition",
  SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP = "System_Suggestion_For_Requisition_Percentage_Match",
  ALLOCATION_REQUEST_PROCESS = "Review process of Employee Allocation by Reviewer ",
  ALLOCATION_REQUEST_PROCESS_DB_GROUP = "Allocation_Request_Process_Reviewed_by_Reviewer",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Review process of Employee Allocation response",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY = "Employee_Response_Review_process_reviewed_by_Resource_Requestor",
  NUMBER_OF_DAYS_REVIEWER_TAKE_ACTION_ON_EMPLOYEE_ALLOCATION_REQUEST = "Number of days for reviewer to  take action on employee allocation requests",
  NUMBER_OF_DAYS_REVIEWER_TAKE_ACTION_ON_EMPLOYEE_ALLOCATION_REQUEST_GROUP_BY = "Number_of_days_for_reviewer_to_take_action_employee_allocation_requests",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Number of days for employee to confirm on their allocations",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY = "Number_of_days_for_employee_to_confirm_on_their_allocations",
  NUMBER_OF_DAYS_RESOURCE_REQUESTOR_TAKE_ACTION_ON_EMPLOYEE_REJECTION_OF_ALLOCATION = "Number of days for Resource Requestor to take action on employee rejection of allocation",
  NUMBER_OF_DAYS_RESOURCE_REQUESTOR_TAKE_ACTION_ON_EMPLOYEE_REJECTION_OF_ALLOCATION_GROUP_BY = "Number_of_days_for_Resource_Requestor_to_take_action_on_employee_rejection_of_allocation",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Maximum number of parameters for selection in Preference screen",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY = "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Amber condition for Budget Consumption",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY = "Amber_condition_for_Budget_Consumption",
  ALERT_CONDITION_FOR_ALLOCATION_COST = "Alert condition for Allocation Cost",
  ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY = "Alert_condition_for_Allocation_Cost",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Alert condition for Timesheet Hours",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY = "Alert_condition_for_Timesheet_Hours",
  BUDGET_CONSUMED_LIMIT = "Amber_condition_for_Budget_Consumption",
  PREMISSION_FOR_ADDITIONAL_EL = "Permission for Additional EL",
  PREMISSION_FOR_ADDITIONAL_EL_GROUP_BY = "Permission_for_Additional_EL",
  PREMISSION_FOR_ADDITIONAL_DELEGATE = "Permission for Additional Delegate",
  PREMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY = "Permission_for_Additional_Delegate",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUEDATE = "Number Of Days For Skill Approval Duedate",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUEDATE_GROUP_BY = "Number_Of_Days_For_Skill_Approval_Duedate",
}

export enum ConfigTypeEnum {
  EXPERTISE = "EXPERTISE",
  BUSINESS_UNIT = "BUSINESS_UNIT",
}

export enum ConfigNoteEnum {
  RESOURCE_ALLOCATION_REVIEW = "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period, the request will be auto-approved. Turning off, will disable the Reviewer workflow feature.",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Turning on this toggle the at the BU level (or Expertise Level), enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period, the request will be auto-approved. Turning off, will disable the Reviewer workflow feature for this scenario.",
  WEIGHTAGE_FOR_REQUISITION_FORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can assigned default weights by changing the value on the slider.",
  REQUISITIONFORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.",
  MATCH_RANGE_FOR_SYSTEM_SUGGESTIONS_FOR_A_REQUISITION = "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below, will show system suggestions results where the resource match score is above 10%.",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period, the request will be auto-approved.",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Setting the Percentage Budget consumption limit below, sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_ALLOCATION_COST = "Setting the Percentage Budget consumption limit below, sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Setting the Percentage Budget consumption limit below, sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_ADDITIONAL_EL = "Turning on this toggle enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate.  Turning off will disable this feature. ",
  ALERT_CONDITION_FOR_ADDITIONAL_DELEGATE = "Turning on this toggle enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Turning off will disable this feature. ",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUEDATE = "Enter a number of days to define the Due Date for the task for the Skill Reviewer.",
}
