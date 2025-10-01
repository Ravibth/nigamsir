export interface IRequisition {
  id: string;
  requisitionDemand: string;
  businessUnit: string;
  clientName: string;
  createdAt: Date;
  createdBy: string;
  demands: any;
  description: string;
  designation: string;
  effortsPerDay: number;
  endDate: Date;
  expertise: string;
  isActive: boolean;
  isPerDayHourAllocation: boolean;
  jobCode: string;
  jobName: string;
  modifiedAt: Date;
  modifiedBy: string;
  pipelineCode: string;
  pipelineName: string;
  requisitionParameterValues: any;
  requisitionParameters: any;
  requisitionSkill: any;
  requisitionStatus: string;
  requisitionType: any;
  requisitionTypeId: number;
  smeg: string;
  startDate: Date;
  totalHours: number;
  requisitionId: string;
}

export interface IResourceAllocation {
  id: string;
  currency: any;
  efforts: number;
  empEmail: string;
  endDate: string;
  isPerDayAllocation: boolean;
  jobCode: string;
  jobName: string;
  pipelineCode: string;
  pipelineName: string;
  publishedResAllocDetailsId: string;
  ratePerHour: number;
  requisition: any;
  requisitionId: string;
  resourceAllocationDays: any;
  startDate: Date;
  totalWorkingDays: number;
  type: string;
  unPublishedResAllocDetailsId: any;
  skills: ISkill[];
}

export interface ISkill {
  id: string;
  publishedResAllocDetailsId: string;
  requisition: any;
  requisitionId: string;
  resAllocDetails: any;
  skillCode: string;
  skillName: string;
  type: string;
  unPublishedResAllocDetailsId: any;
  startDate: Date;
  totalEffort: number;
}

export interface IResourceAllocationDetails {
  id: string;
  guid: string;
  allocationStatus: string;
  allocationVersion: number;
  confirmedAllocationDate: Date;
  createdAt: Date;
  createdBy: string;
  description: string;
  empEmail: string;
  empName: string;
  endDate: Date;
  isActive: boolean;
  isUpdated: boolean;
  jobCode: string;
  jobName: string;
  modifiedAt: string;
  modifiedBy: string;
  pipelineCode: string;
  pipelineName: string;
  requisition: IRequisition;
  resourceAllocations: IResourceAllocation[];
  skills: ISkill[];
  startDate: Date;
  totalEffort: number;
  type: string;
}

export {};
