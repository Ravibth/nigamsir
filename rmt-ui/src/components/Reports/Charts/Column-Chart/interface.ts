import { IReportDashboardFilterControl } from "../../Filters/uitls";
import { ISelectedChartData } from "../../interface";

export interface IColumnChartProps {
  selectedChartData: ISelectedChartData;
  setSelectedChartData: React.Dispatch<
    React.SetStateAction<ISelectedChartData>
  >;
  setIsOpen: React.Dispatch<React.SetStateAction<boolean>>;
  filterParameters: IReportDashboardFilterControl;
  toggleValue: string;
  chartTitle: string;
  startDate: string;
  endDate: string;
  setRowData: React.Dispatch<React.SetStateAction<any[]>>;
  setColDef: React.Dispatch<React.SetStateAction<any[]>>;
  setRowTabularData: React.Dispatch<React.SetStateAction<any[]>>;
  userLeaderRoles: any;
}

export interface ICapacityUtilizationChart {
  business_unit: string;
  expertise: string;
  sme_group_name: string;
  actual_log_hours: number;
  capacity: number;
  allocation_hours: number;
  allocated_chargable_hr: number;
  allocated_chargable_cost: number;
  allocated_non_chargable_hr: number;
  allocated_non_chargable_cost: number;
  job_chargeable_cost: number;
  job_non_chargeable_cost: number;
  job_chargeable_hours: number;
  job_non_chargeable_hours: number;
  availability: number;
  allocated_cost: number;
  actual_cost: number;
  capacity_cost: number;
  email_id: string;
  competency?: string;
  availability_cost: number;
  location: string;
  leave_hrs: number;
  allocation_percent: number;
  allocation_chargability_percent: number;
  actual_chargability_percent: number;
  supercoach_mid: string;
  supercoach_name: string;
  csc_mid: string;
  csc_name: string;
  skills:string;
}
export interface ICapacityUtilizationColumnBarChart {
  name: string;
  availability: number;
  allocation: number;
  allocated_chargable_hr: number;
  allocated_chargable_cost: number;
  allocated_non_chargable_hr: number;
  allocated_non_chargable_cost: number;
  capacity: number;
  allocated_cost: number;
  actual_cost: number;
  capacity_cost: number;
  job_chargeable_cost: number;
  job_non_chargeable_cost: number;
  job_chargeable_hours: number;
  job_non_chargeable_hours: number;
  chargability: number;
  chargability_percentage: number;
  capacity_percentage: number;
  availability_cost: number;
  actual_log_hours: number;
  leave_hrs: number;
  allocation_percent: number;
  allocation_chargability_percent: number;
  actual_chargability_percent: number;
}
