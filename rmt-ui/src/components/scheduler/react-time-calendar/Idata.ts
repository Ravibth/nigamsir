export interface GroupKey {
  id: any | number;
  projectName?: any;
  isHeader?: boolean;
  defaultExpanded: boolean;
  details?: any[];
  project_status?: string;
  projectJobCodes?: any[];
  allocationHours?: string;
  pipelineCode?: any | string;
  budgetStatus?: any | string;
  chargableType?: any | string;
  clientName?: any | string;
  location?: any | string;
  startDate?: any;
  endDate?: any;
  sme?: any | string;
  revenueUnit?: any | string;
  expertise?: any | string;
  jobCode?: any | string;
  projectAllocationStatus?: any | string;
  pipelineName: any | string;
  // job_id?: number;
  requisitions: IRequisitions[];
}

export enum EAllocationStatuses {
  PENDING = "Allocation Pending",
  ON_HOLD = "Allocation On Hold",
  COMPLETE = "Allocation Complete",
}

export enum ProjectAllocationStatus {
  ALLOCATION_COMPLETED = "ALLOCATION_COMPLETED",
  ALLOCATION_IN_PROGRESS = "ALLOCATION_INPROGRESS",
  PENDING_ALLOCATION = "PENDING_ALLOCATION",
}

export enum EApprovalStatuses {
  PENDING_FOR_APPROVAL = "Pending For Approval",
}

export enum ERequisitionDesignations {
  CONSULTANT = "Consultant",
  SR_CONSULTANT = "Sr. Consultant",
  MANAGER = "Manager",
}

export interface GroupChildKeys {
  id: number;
  title: string;
  user: string;
  approval_status?: string;
}

export interface IRequisitions {
  requisition_id: number;
  JobCode: string;
  designation: string;
  resource_number: number;
}
