export interface IAllocationDetails {
  allocationStatus: string;
  allocationType: string;
  confirmedPerDayHours: String;
  designation: string;
  empEmail: string;
  empName: string;
  endDate: Date;
  id: number;
  jobCode: string;
  jobName: string;
  pipelineCode: string;
  pipelineName: string;
  // projectCode: string;
  recordType: string;
  requisitionId: number;
  startDate: Date;
  skills: {
    skillName: string;
    resourceAllocationID: number;
  };
  resourceAllocation: {
    clientName: string;
    confirmedAllocationEndDate: Date;
    confirmedAllocationStartDate: Date;
    isActive: boolean;
    isPerDayHourAllocation: boolean;
  };
}
