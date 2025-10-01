export enum EConfigurationMasterGroups {
  RESOURCE_ALLOCATION_DB_GROUP = "Resource_allocation_review",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY = "Number_of_days_for_employee_to_confirm_on_their_allocations",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY = "Employee_Response_Review_process_reviewed_by_Resource_Requestor",
  REQUISITION_FORM_DB_GROUP = "Requisition_form",
  SYSTEM_SUGGESTED_DB_GROUP = "Weightage_for_parameters_for_System_Suggested_Requisition",
  SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP = "System_Suggestion_For_Requisition_Percentage_Match",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY = "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY = "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY = "Amber_condition_for_Budget_Consumption",
  ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY = "Alert_condition_for_Allocation_Cost",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY = "Alert_condition_for_Timesheet_Hours",
  PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY = "Permission_for_Additional_EL",
  PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY = "Permission_for_Additional_Delegate",
}

export enum EConfigurationMasterGroupsLabel {
  RESOURCE_ALLOCATION_GROUP = "Resource allocation review by Reviewer",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Number of days for employee to confirm on their allocations",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Review process of Employee Allocation response",
  REQUISITION_FORM = "Requisition Form Parameters ",
  SYSTEM_SUGGESTED_GROUP = "Weightage for Requisition Form Parameters",
  SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH = "Match Range  for System Suggestions for a Requisition",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Maximum number of parameters for selection in Preference screen",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE = "Number Of Days For Skill Approval Duedate",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Amber condition for Budget Consumption",
  ALERT_CONDITION_FOR_ALLOCATION_COST = "Alert condition for Allocation Cost",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Alert condition for Timesheet Hours",
  PERMISSION_FOR_ADDITIONAL_EL = "Permission for Additional EL",
  PERMISSION_FOR_ADDITIONAL_DELEGATE = "Permission for Additional Delegate",
}

export enum EConfigNoteEnum {
  RESOURCE_ALLOCATION_REVIEW = "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period, the request will be auto-approved. Turning off, will disable the Reviewer workflow feature.",
  EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Turning on this toggle the at the BU level (or Expertise Level), enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period, the request will be auto-approved. Turning off, will disable the Reviewer workflow feature for this scenario.",
  WEIGHTAGE_FOR_REQUISITION_FORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can assigned default weights by changing the value on the slider.",
  REQUISITION_FORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.",
  MATCH_RANGE_FOR_SYSTEM_SUGGESTIONS_FOR_A_REQUISITION = "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below, will show system suggestions results where the resource match score is above 10%.",
  NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period, the request will be auto-approved.",
  MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ",
  AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Setting the Percentage Budget consumption limit below, sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_ALLOCATION_COST = "Setting the Percentage Budget consumption limit below, sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Setting the Percentage Budget consumption limit below, sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.",
  ALERT_CONDITION_FOR_ADDITIONAL_EL = "Turning on this toggle enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate.  Turning off will disable this feature. ",
  ALERT_CONDITION_FOR_ADDITIONAL_DELEGATE = "Turning on this toggle enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Turning off will disable this feature. ",
  NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE = "Enter a number of days to define the Due Date for the task for the Skill Reviewer.",
}

export enum EConfigTypeEnum {
  OFFERINGS = "Offerings",
}

export enum EKeySelectorsForMinBreakup {
  DEFAULT = "DEFAULT",
}

export const EKeySelectorSeparator = "|";

export enum ETextFieldRendererControlTypes {
  INTEGER = "integer",
}

export interface IConfigurationMainBreakupMetaValues {
  key: string;
  displayKey: string;
  value: string;
}

export interface IConfigurationMainBreakups {
  id: string;
  configurationMasterId: string;
  keySelector: string;
  metaValue: any;
  createdBy: string;
  modifiedBy: string;
  createdAt: Date;
  modifiedAt: Date;
  configurationMainBreakupMetaValues: IConfigurationMainBreakupMetaValues[];
}

export interface IConfigurationMasterSchema {
  key: string;
  keyDisplay: string;
  description: string;
  controlType: string;
  validationRegEx: RegExp;
}

export interface IConfigurationMaster {
  id: string;
  configGroup: EConfigurationMasterGroups;
  configGroupDisplay: EConfigurationMasterGroupsLabel;
  description: string;
  configNote: EConfigNoteEnum;
  globalDefaultDisplay: boolean;
  selectorWiseDisplay: boolean;
  selectorConfigType: string;
  schema?: any;
  schemaValues: IConfigurationMasterSchema[];
  configurationMainBreakups: IConfigurationMainBreakups[];
}

export const ConfigBuOfferingKeyCreator = (
  businessUnit: string,
  offering: string
) => {
  return `${businessUnit ?? ""}${EKeySelectorSeparator}${offering ?? ""}`;
};
