export const ALLOCATION_SERVICES = {
  getAvaiableHoursByEmailId: "ResourceAllocation/GetAvaiableHoursByEmailId",
  // getAllocationByID: "ResourceAllocation/GetAllocationByID?allocationId=",
  getAllocationByEmail: "ResourceAllocation/GetAllocationsByEmail?EmpEmail=",
  // updateAllocationByID: "ResourceAllocation/UpdateAllocationByID",
  GetUsersTimeline: "ResourceAllocation/GetUsersTimeline",
  GetUsersTimelineForMultipleDates:
    "ResourceAllocation/GetUsersTimelineForMultipleDates",
  GetMultipleAllocationsByRequisitionIDs:
    "ResourceAllocation/GetMultipleAllocationsByRequisitionIDs",
  getRequisitionByEMailPipeline:
    "ResourceAllocation/GetRequisitionByEMailPipeline",
  getRequisitionDetailsByRequisitionId:
    "Requisition/GetRequisitionDetailsByRequisitionId",
  submitRequisitionForProjectCode:
    "Requisition/SubmitRequisitionForProjectCode",
  //getRolloverUsersTimeline: "ResourceAllocation/GetRolloverUsersTimeline",
  //getResourceAllocationByGuid: "ResourceAllocation/GetResourceAllocationByGuid",
  updateRequisition: "Requisition/UpdateRequisition",
  GetAllRequisitionByProjectCode:
    "Requisition/GetAllRequisitionByProjectCode?PipelineCode=",
  bulkUploadRequisition: "BulkRequisition/BulkUploadRequisition",
  uploadWcgtValidation: "BulkRequisition/WcgtValidation",
  IsRequisitionExistsInProjectCode:
    "Requisition/IsRequisitionExistsInProjectCode?pipelineCode=",
};

export const CONFIGURATION_SERVICES = {
  getAllBusinessMaster: "WcgtData/GetBUTreeMappingList",
  // getAllExpertiseMaster: "Configuration/GetExpertiesMastersByBUId",
  // getAllSMEGMaster: "Configuration/GetAllSMEGMasters",
  // getAllRUMaster: "Configuration/GetAllRUMasters",
  getDesignationList: "WcgtData/GetDesignationList",
  getSectorIndustryList: "WcgtData/GetSectorIndustryList",
  getLocationList: "WcgtData/GetLocationList",
};

export const CONFIGURATION_WORKFLOW = {
  getWorkflowTasks: "workflow/v1/workflowTasks/query",
  getEmployeewithdrawalWorkflowTasks:
    "workflow/v1/GetEmployeeWithdrawlTaskByQuery",
  getWorkflowTasksDetailsByEmailId:
    "workflow/v1/GetWorkflowTasksDetailsByQuery",
  updateWorkflow: "workflow/v1/update-workflow",
  bulkupdateWorkflow: "workflow/v1/bulk/update-workflow",
  getWorkflowByItemGuid: "workflow/v1/query",
  //getMultipleTasksByByQuery: "workflow/v1/getMultipleTasksByByQuery",
};

export const PROJECT_SERVICES = {
  getProjectByCode: "Project/GetProjectByCode",
};

export interface GetUsersTimelinePayload {
  emails: string[];
  start_date: Date;
  end_date: Date;
}
