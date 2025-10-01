export enum EAllocationType {
  SYSTEM_SUGGESTED_ALLOCATION = "SYSTEM_SUGGESTED_ALLOCATION",
  NAME_ALLOCATION = "NAME_ALLOCATION",
  SAME_TEAM_ALLOCATION = "SAME_TEAM_ALLOCATION",
  BULK_ALLOCATION = "BULK_ALLOCATION",
  UPDATE_ALLOCATION = "UPDATE_ALLOCATION",
  CREATE_REQUISITION = "CREATE_REQUISITION",
  ROLL_FORWARD_ALLOCATION = "ROLL_FORWARD_ALLOCATION",
}

export enum EBaseCommonAllocationMainControlForm {
  startDate = "startDate",
  endDate = "endDate",
  noOfHours = "noOfHours",
  isRequisition = "isRequisition",
  isPerDayHourAllocation = "isPerDayHourAllocation",
  applyToAll = "applyToAll",
}

export enum ENavigatingFromToUpdateCommonScreen {
  PROJECT_LISTING = "PROJECT_LISTING",
  ALLOCATIONS_TAB = "ALLOCATIONS_TAB",
}

export const EAllocationTypeArray = [
  { id: 1, name: EAllocationType.NAME_ALLOCATION },
  { id: 2, name: EAllocationType.SAME_TEAM_ALLOCATION },
  { id: 3, name: EAllocationType.CREATE_REQUISITION },
  { id: 4, name: EAllocationType.ROLL_FORWARD_ALLOCATION },
  { id: 5, name: EAllocationType.BULK_ALLOCATION },
];
