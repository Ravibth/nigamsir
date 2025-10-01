import { IEmployeeLeaveHolidayAndAvailablity } from "../../../common/interfaces/IEmployeeModel";

export interface TimelineGroup {
  id: string;
  title: React.ReactNode;
}

export enum EDateControl {
  start_date = "startDate",
  end_date = "endDate",
}

export interface AggregatedEmployee {
  start: Date;
  end: Date;
  allocation_hrs: number;
  availablity_hrs: number;
  leave_hrs: number;
  holiday_hrs: number;
  employee_mid: string;
  employee_name: string;
  employee_email: string;
  employee_email_uid: string;
  totalTimelineAllocationHrs: number;
  totalTimelineLeaveHrs: number;
  totalTimelineHolidayHrs: number;
  totalTimelineAvailablity: number;
}

export interface IEmployeeItemRow {
  employee: AggregatedEmployee;
  filteredEmployees:IEmployeeLeaveHolidayAndAvailablity[];
  currentView:string;
}
