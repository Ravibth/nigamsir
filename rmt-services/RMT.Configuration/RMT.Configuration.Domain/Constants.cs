using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain
{
    public static class ConfigurationGroupMasterSelectorConfigType
    {
        public static string BusinessUnit = "BusinessUnit";
        public static string Offerings = "Offerings";
        public static string Global = "Global";
    }

    public static class ValidationRegex
    {
        public static string ZeroToTen = "^(10|[0-9]|[0-9]\\.[0-9]+)$";
        public static string MinusOneToTen = "^(-1|10|[0-9]|[0-9]\\.[0-9]+)$";
        public static string ZeroToHundred = "^(100|[1-9][0-9]?)$";
        public static string OneToHundred = "^([1-9]|[1-9][0-9]|100)$";
        public static string ZeroToNine = "^[0-9]$";
        public static string OneToFive = "^[1-5]$";
        public static string MinusOneOrOneToFour = "^(-1|[1-4])$";
        public static string MinusOneOrOne = "^-?1$";
    }

    public static class ConfigMasterKeyDisplayLabel
    {
        public static string Location = "Location";
        public static string Same_client = "Experience working with the same client";
        public static string Competency = "Competency";
        public static string Offerings = "Offerings";
        public static string Solutions = "Solutions";
        public static string Industry = "Industry";
        public static string Sub_Industry = "Sub Industry";
        public static string Skills = "Skills";
        public static string System_Suggestion_For_Requisition_Percentage_Match = "System Suggestion For Requisition Percentage Match";
        public static string Default_Key_Selector = "DEFAULT";
        public static string RESOURCE_ALLOCATION_GROUP = "Resource allocation review by Reviewer";
        public static string ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Resource allocation review by Supercoach";
        public static string NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Number of days for employee to confirm on their allocations";
        public static string EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Review process of Employee Allocation response";
        public static string MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Maximum number of parameters for selection in Preference screen";
        public static string AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Amber condition for Budget Consumption";
        public static string ALERT_CONDITION_FOR_ALLOCATION_COST = "Alert condition for Allocation Cost";
        public static string ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Alert condition for Timesheet Hours";
        public static string PERMISSION_FOR_ADDITIONAL_EL = "Permission for Additional EL";
        public static string PERMISSION_FOR_ADDITIONAL_DELEGATE = "Permission for Additional Delegate";
        public static string NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE = "Number Of Days For Skill Approval Duedate";

    }

    public static class ConfigurationParametersEnum
    {
        public static string Location = "Location";
        public static string Same_client = "Same_client";
        public static string Competency = "competency";
        public static string Offerings = "offerings";
        public static string Solutions = "solutions";
        public static string Industry = "Industry";
        public static string Sub_Industry = "Sub_Industry";
        public static string Skills = "Skills";
        public static string System_Suggestion_For_Requisition_Percentage_Match = "System_Suggestion_For_Requisition_Percentage_Match";
        public static string RESOURCE_ALLOCATION_DB_GROUP = "Resource_allocation_review";
        public static string ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Employee_Response_Review_process_reviewed_by_Supercoach";
        public static string NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY = "Number_of_days_for_employee_to_confirm_on_their_allocations";
        public static string EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY = "Employee_Response_Review_process_reviewed_by_Resource_Requestor";
        public static string MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY = "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen";
        public static string AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY = "Amber_condition_for_Budget_Consumption";
        public static string ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY = "Alert_condition_for_Allocation_Cost";
        public static string ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY = "Alert_condition_for_Timesheet_Hours";
        public static string PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY = "Permission_for_Additional_EL";
        public static string PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY = "Permission_for_Additional_Delegate";
        public static string NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY = "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE";

    }
    public static class ConfigurationMasterControlType
    {
        public static string Integer = "Integer";
        public static string Boolean = "boolean";
    }

    public static class ConfigurationGroupMasterEnum
    {
        public static string RESOURCE_ALLOCATION_DB_GROUP = "Resource_allocation_review";
        public static string ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Employee_Response_Review_process_reviewed_by_Supercoach";
        public static string NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION_GROUP_BY = "Number_of_days_for_employee_to_confirm_on_their_allocations";
        public static string EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR_GROUP_BY = "Employee_Response_Review_process_reviewed_by_Resource_Requestor";
        public static string REQUISITION_FORM_DB_GROUP = "Requisition_form";
        public static string SYSTEM_SUGGESTED_DB_GROUP = "Weightage_for_parameters_for_System_Suggested_Requisition";
        public static string SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP = "System_Suggestion_For_Requisition_Percentage_Match";
        public static string MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN_GROUP_BY = "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen";
        public static string NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE_GROUP_BY = "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE";
        public static string AMBER_CONDITION_FOR_BUDGET_CONSUMPTION_GROUP_BY = "Amber_condition_for_Budget_Consumption";
        public static string ALERT_CONDITION_FOR_ALLOCATION_COST_GROUP_BY = "Alert_condition_for_Allocation_Cost";
        public static string ALERT_CONDITION_FOR_TIMESHEET_HOURS_GROUP_BY = "Alert_condition_for_Timesheet_Hours";
        public static string PERMISSION_FOR_ADDITIONAL_EL_GROUP_BY = "Permission_for_Additional_EL";
        public static string PERMISSION_FOR_ADDITIONAL_DELEGATE_GROUP_BY = "Permission_for_Additional_Delegate";
    }

    public class ConfigurationGroupMasterDisplayEnum
    {
        public static string RESOURCE_ALLOCATION_GROUP = "Resource allocation review by Reviewer";
        public static string ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Resource allocation review by Supercoach";
        public static string NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Number of days for employee to confirm on their allocations";
        public static string EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Review process of Employee Allocation response";
        public static string REQUISITION_FORM = "Requisition Form Parameters ";
        public static string SYSTEM_SUGGESTED_GROUP = "Weightage for Requisition Form Parameters";
        public static string SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH = "Match Range  for System Suggestions for a Requisition";
        public static string MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Maximum number of parameters for selection in Preference screen";
        public static string NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE = "Number Of Days For Skill Approval Duedate";
        public static string AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Amber condition for Budget Consumption";
        public static string ALERT_CONDITION_FOR_ALLOCATION_COST = "Alert condition for Allocation Cost";
        public static string ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Alert condition for Timesheet Hours";
        public static string PERMISSION_FOR_ADDITIONAL_EL = "Permission for Additional EL";
        public static string PERMISSION_FOR_ADDITIONAL_DELEGATE = "Permission for Additional Delegate";
    }

    public class ConfigurationGroupMasterConfigNoteEnum
    {
        public static string RESOURCE_ALLOCATION_REVIEW = "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature.";
        public static string ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION = "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the supercoach to approve the allocation. Post this period; the request will be auto-approved. Turning off; will disable the Supercoach workflow feature.";
        public static string EMPLOYEE_RESPONSE_REVIEW_PROCESS_REVIEWED_BY_RESOURCE_REQUESTOR = "Turning on this toggle the at the BU level (or Expertise Level); enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature for this scenario.";
        public static string WEIGHTAGE_FOR_REQUISITION_FORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can assigned default weights by changing the value on the slider.";
        public static string REQUISITION_FORM_PARAMETERS = "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.";
        public static string MATCH_RANGE_FOR_SYSTEM_SUGGESTIONS_FOR_A_REQUISITION = "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.";
        public static string NUMBER_OF_DAYS_EMPLOYEE_TO_CONFIRM_ALLOCATION = "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period; the request will be auto-approved.";
        public static string MAXIMUM_NUMBER_OF_PARAMETERS_FOR_SELECTION_IN_PREFERENCE_SCREEN = "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ";
        public static string AMBER_CONDITION_FOR_BUDGET_CONSUMPTION = "Setting the Percentage Budget consumption limit below; sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.";
        public static string ALERT_CONDITION_FOR_ALLOCATION_COST = "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.";
        public static string ALERT_CONDITION_FOR_TIMESHEET_HOURS = "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.";
        public static string ALERT_CONDITION_FOR_ADDITIONAL_EL = "Entering value of 1 enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Entering value of -1 will disable the feature.";
        public static string ALERT_CONDITION_FOR_ADDITIONAL_DELEGATE = "Entering value of 1 enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Entering value of -1 will disable the feature.";
        public static string NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE = "Enter a number of days to define the Due Date for the task for the Skill Reviewer.";
    }
}
