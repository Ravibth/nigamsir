import { IBUTreeMapping } from "../../common/interfaces/IBuTreeMapping";

export enum ConfigGroupEnum {
  DAYS_PROJECT_IN_MP_GROUP = "No of days where project is available in Marketplace",
  DAYS_PROJECT_IN_MP_DB_GROUP = "No_of_days_where_project_is_available_in_Marketplace",
  DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR = "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON",
  DAYS_NOTIFICATION_FOR_RESOURCE_REQUESTOR_DB_GROUP = "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON",
  MAX_NO_OF_ITEMS_FOR_EMPLOYEE_PREFERENCE_SELECTION = "Maximum number of items to be selected for employee preference",
  MAX_NO_OF_ITEMS_FOR_EMPLOYEE_PREFERENCE_SELECTION_DB_GROUP = "Maximum_number_of_items_for_employee_preference",
  ALLOCATION_REQUEST_PROCESS = "Review process of Employee Allocation by Reviewer ",
  ALLOCATION_REQUEST_PROCESS_DB_GROUP = "Allocation_Request_Process_Reviewed_by_Reviewer",
  NUMBER_OF_DAYS_REVIEWER_TAKE_ACTION_ON_EMPLOYEE_ALLOCATION_REQUEST = "Number of days for reviewer to  take action on employee allocation requests",
  NUMBER_OF_DAYS_REVIEWER_TAKE_ACTION_ON_EMPLOYEE_ALLOCATION_REQUEST_GROUP_BY = "Number_of_days_for_reviewer_to_take_action_employee_allocation_requests",
  NUMBER_OF_DAYS_RESOURCE_REQUESTOR_TAKE_ACTION_ON_EMPLOYEE_REJECTION_OF_ALLOCATION = "Number of days for Resource Requestor to take action on employee rejection of allocation",
  NUMBER_OF_DAYS_RESOURCE_REQUESTOR_TAKE_ACTION_ON_EMPLOYEE_REJECTION_OF_ALLOCATION_GROUP_BY = "Number_of_days_for_Resource_Requestor_to_take_action_on_employee_rejection_of_allocation",
  BUDGET_CONSUMED_LIMIT = "Amber_condition_for_Budget_Consumption",
}

export interface IBusinessUnitUniqueConfigTree {
  bu: string;
  offerings: string[];
}

export const getFlattenedBuTreeMapping = (
  buTree: IBUTreeMapping[]
): IBusinessUnitUniqueConfigTree[] => {
  const collated = buTree.reduce((acc, item) => {
    const existingBU = acc.find((bu) => bu?.bu?.trim() === item?.bu?.trim());

    if (existingBU) {
      if (!existingBU?.offerings.includes(item?.offering?.trim())) {
        existingBU?.offerings.push(item?.offering?.trim());
      }
    } else {
      acc.push({ bu: item?.bu?.trim(), offerings: [item?.offering?.trim()] });
    }

    return acc;
  }, []);
  return collated;
};
