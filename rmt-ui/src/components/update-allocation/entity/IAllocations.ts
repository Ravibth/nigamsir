export interface IAllocationDetails {
  id: number;
  confirmedAllocationStartDate: Date;
  confirmedAllocationEndDate: Date;
  confirmedPerDayHours: number;
  totalWorkingDays: number;
  skills: string;
  isActive: boolean;
  modifiedBy: string;
  modifiedDate: Date;
  allocationStatus: string;
  allocationType: boolean;
  totalEfforts: number;
  resourceAllocation: IAllocation[];
  isPerDayHourAllocation: boolean;
}

export interface IAllocation {
  confirmedAllocationStartDate?: Date;
  confirmedAllocationEndDate?: Date;
  confirmedPerDayHours?: number;
  totalWorkingDays?: number;
  totalEfforts?: number;
  id?: number;
  isactive?: boolean;
  index?: number;
  error_total_hours_Msg?: string;
  error_vaild_date_Msg?: string;
  isPerDayHourAllocation?: boolean;
  effort?: number;
  empEmail?: string;
  empName?: string;
  pipelineCode?: string;
  jobCode?: string;
  clientName?: string;
}
