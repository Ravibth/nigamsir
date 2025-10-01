import { DraftStatus } from "../constant";
import { capitalizeFirstLetter } from "../utils";
import {
  SkillWorkflow,
  UiAllocationStatus,
  WorkflowStatus,
  WorkflowStatusColor,
  WorkflowStatusPrimaryColor,
} from "./workflow-constant";

export const getEmployeeAllocationStatus = (status = "") => {
  const sCase = status.toLowerCase().trim();
  let allocationStatus = "";
  let color = "";
  let bgColor = "";
  switch (sCase) {
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.PENDING_WITH_REVIEWER;
      color = WorkflowStatusColor.PENDING_REVIEWER_CONFIRMATION;
      bgColor = WorkflowStatusPrimaryColor.PENDING_REVIEWER_CONFIRMATION;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER}`
      .toLowerCase()
      .trim():
      //todo:- to be changed
      allocationStatus =
        WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH}`
      .toLowerCase()
      .trim():
      //todo:- to be changed
      allocationStatus =
        WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.ALLOCATION_COMPLETE;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.ALLOCATION_COMPLETE;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.PENDING_WITH_EMPLOYEE;
      color = WorkflowStatusColor.PENDING_RESOURCE_CONFIRMATION;
      bgColor = WorkflowStatusPrimaryColor.PENDING_RESOURCE_CONFIRMATION;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.ALLOCATION_COMPLETE;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE}`
      .toLowerCase()
      .trim():
      //TODO:- status to be taken
      allocationStatus = UiAllocationStatus.Allocation_Rejected_by_Employee;
      // WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR}`
      .toLowerCase()
      .trim():
      allocationStatus =
        UiAllocationStatus.PENDING_RESPONSE_REVIEW_BY_REQUESTOR;
      color = WorkflowStatusColor.PENDING_RESPONSE_REVIEW_BY_REQUESTOR;
      bgColor = WorkflowStatusPrimaryColor.PENDING_RESPONSE_REVIEW_BY_REQUESTOR;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.ALLOCATION_COMPLETE;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION}`
      .toLowerCase()
      .trim():
      //TODO: status to be taken
      allocationStatus =
        WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      // allocationStatus = UiAllocationStatus
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION}`
      .toLowerCase()
      .trim():
      allocationStatus =
        UiAllocationStatus.PENDING_RESPONSE_REVIEW_BY_SUPERCOACH;
      color = WorkflowStatusColor.PENDING_RESPONSE_REVIEW_BY_SUPERCOACH;
      bgColor =
        WorkflowStatusPrimaryColor.PENDING_RESPONSE_REVIEW_BY_SUPERCOACH;
      break;
    case `${WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION}`
      .toLowerCase()
      .trim():
      allocationStatus = UiAllocationStatus.ALLOCATION_COMPLETE;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${WorkflowStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION}`
      .toLowerCase()
      .trim():
      //TODO: status to be taken
      allocationStatus =
        WorkflowStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      break;
    case `${DraftStatus.DRAFT}`.toLowerCase().trim():
      allocationStatus = "Draft";
      color = WorkflowStatusColor.DRAFT;
      bgColor = WorkflowStatusPrimaryColor.DRAFT;
      break;
    case `${SkillWorkflow.APPROVED_SKILL}`.toLowerCase().trim():
      allocationStatus = SkillWorkflow.APPROVED_SKILL;
      color = WorkflowStatusColor.ALLOCATED;
      bgColor = WorkflowStatusPrimaryColor.ALLOCATED;
      break;
    case `${SkillWorkflow.REJECTED_SKILL}`.toLowerCase().trim():
      allocationStatus = SkillWorkflow.REJECTED_SKILL;
      color = WorkflowStatusColor.REJECTED;
      bgColor = WorkflowStatusPrimaryColor.REJECTED;
      break;
    case `${SkillWorkflow.PENDING}`.toLowerCase().trim():
      allocationStatus = SkillWorkflow.PENDING;
      color = WorkflowStatusColor.PENDING_REVIEWER_CONFIRMATION;
      bgColor = WorkflowStatusPrimaryColor.PENDING_REVIEWER_CONFIRMATION;
      break;
    default:
      allocationStatus = capitalizeFirstLetter(status);
      color = WorkflowStatusColor.DRAFT;
      bgColor = WorkflowStatusPrimaryColor.DRAFT;
      break;
  }
  return { allocationStatus, color, bgColor };
};
