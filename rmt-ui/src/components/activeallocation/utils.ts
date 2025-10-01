import { DraftStatus } from "../../global/constant";
import { WorkflowStatus } from "../../global/workflow/workflow-constant";
import { getDateWithoutTime } from "../../utils/date/dateHelper";

export const getFilterDataForAllocation = (allocationDBData: any[]) => {
  const clientNameSet = new Set();
  const expertiseSet = new Set();
  const smeSet = new Set();
  const pipelineSet = new Set();
  const jobSet = new Set();

  allocationDBData.forEach((data) => {
    if (data.clientName) {
      clientNameSet.add(data.clientName);
    }
    if (data.expertise) {
      expertiseSet.add(data.expertise);
    }
    if (data.sme) {
      smeSet.add(data.sme);
    }
    if (data.pipelineCode) {
      pipelineSet.add(data.pipelineCode + " - " + data.pipelineName);
    }
    // if (isEmployee) {
    //   if (data.jobCode) {
    //     jobSet.add(data.jobCode);
    //   }
    // }
    else {
      if (
        data.projectCode &&
        data.pipelineCode &&
        data.projectCode !== data.pipelineCode
      ) {
        let jobCode = data.projectCode;
        jobSet.add(jobCode);
      } else if (
        data.projectCode &&
        data.pipelineCode &&
        data.projectCode.toString().toUpperCase() ===
          data.pipelineCode.toString().toUpperCase()
      ) {
        let jobCode = data.projectCode;
        jobSet.add(jobCode);
      }
    }
  });
  const distinctClientNames = Array.from(clientNameSet);
  const distinctExpertises = Array.from(expertiseSet);
  const distinctSmes = Array.from(smeSet);
  const distinctPipelines = Array.from(pipelineSet);
  const distinctJobs = Array.from(jobSet);
  return {
    distinctClientNames,
    distinctExpertises,
    distinctSmes,
    distinctPipelines,
    distinctJobs,
  };
};

export const filteredAllocationList = (
  allocationList: any[],
  filterParameters: any
) => {
  if (
    filterParameters &&
    (filterParameters.designation === undefined ||
      filterParameters.designation.length === 0) &&
    (filterParameters.employeeName === undefined ||
      filterParameters.employeeName.length === 0) &&
    (filterParameters.experties === undefined ||
      filterParameters.experties.length === 0) &&
    (filterParameters.sme === undefined || filterParameters.sme.length === 0) &&
    (filterParameters.startDate === undefined ||
      filterParameters.startDate.length === 0) &&
    (filterParameters.endDate === undefined ||
      filterParameters.endDate.length === 0) &&
    (filterParameters.businessUnit === undefined ||
      filterParameters.businessUnit.length === 0) &&
    (filterParameters.revenueUnit === undefined ||
      filterParameters.revenueUnit.length === 0)
  ) {
    return allocationList;
  }
  let allocationListWithFilter = allocationList;
  const list = allocationListWithFilter.filter((data) => {
    if (
      (filterParameters.designation &&
        filterParameters.designation.findIndex(
          (e: any) =>
            e.label?.toUpperCase().trim() ===
            data?.designation?.toUpperCase().trim()
        ) !== -1) ||
      (filterParameters.experties &&
        filterParameters.experties.includes(data.experties)) ||
      (filterParameters.sme && filterParameters.sme.includes(data.sme)) ||
      (filterParameters.employeeName &&
        filterParameters.employeeName.includes(data.employeeName)) ||
      (filterParameters.startDate &&
        filterParameters.startDate <= data.startDate &&
        filterParameters.endDate &&
        filterParameters.endDate >= data.endDate) ||
      (filterParameters.businessUnit &&
        filterParameters.businessUnit.includes(data.businessUnit)) ||
      (filterParameters.revenueUnit &&
        filterParameters.revenueUnit.includes(data.revenueUnit))
    ) {
      return true;
    } else {
      return false;
    }
  });
  return list;
};

export const releaseAllocationAllowedStatues = [
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION.toLocaleLowerCase(),
  DraftStatus.DRAFT.toLocaleLowerCase(),
];

export const isReleaseResourceDisabled = (
  allocationEndDate: Date,
  allocationStatus: WorkflowStatus | DraftStatus,
  isUpdated: boolean
) => {
  const currentDate = new Date();
  if (
    getDateWithoutTime(currentDate) >= getDateWithoutTime(allocationEndDate)
  ) {
    return true;
  }
  if (
    !releaseAllocationAllowedStatues.includes(
      allocationStatus?.toLocaleLowerCase()
    )
  ) {
    return true;
  }
  if (isUpdated) {
    return true;
  }

  return false;
};

export const UpdateAllocationAllowedStatues = [
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH.toLocaleLowerCase(),
  WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION.toLocaleLowerCase(),
  DraftStatus.DRAFT.toLocaleLowerCase(),
];

export const isUpdateAllocationDisabled = (
  allocationEndDate: Date,
  allocationStatus: WorkflowStatus | DraftStatus,
  isUpdated: boolean
) => {
  //const currentDate = new Date();
  // if (getDateWithoutTime(currentDate) > getDateWithoutTime(allocationEndDate)) {
  //   return true;
  // }
  //commented to resolve 7289 point 1
  if (
    !UpdateAllocationAllowedStatues.includes(
      allocationStatus?.toLocaleLowerCase()
    )
  ) {
    return true;
  }
  if (isUpdated) {
    return true;
  }
  return false;
};
