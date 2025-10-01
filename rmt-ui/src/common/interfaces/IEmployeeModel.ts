export interface IEmployeeModel {
  employee_mid: string;
  company_name: string;
  employee_code: string;
  first_name: string;
  middle_name: string;
  last_name: string;
  name: string;
  designation_id: string;
  department: string;
  location_id: string;
  email_id: string;
  joining_date: Date;
  reporting_partner_mid: string;
  group_head_mid: string;
  business_unit_id: string;
  CompetencyId: string;
  specical_day: Date;
  birthday: Date;
  isactive: boolean;
  supercoach_mid: string;
  resignation_date: Date;
  proposed_lwd: Date;
  employee_status: string;
  location: ILocation;
  createdat: Date;
  modifiedat: Date;
  createdby: string;
  modifiedby: string;
  allocation_hrs: number;
}

export interface IEmployeeLeaveHolidayAndAvailablity {
  allocation_hrs: number;
  available_hrs: number;
  email_id: string;
  email_id_uid: string;
  business_unit_id?: string;
  competencyId?: string;
  holiday_hrs: number;
  leave_hrs: number;
  name: string;
  employee_mid: string;
  working_date: Date;
}

export interface ILocation {
  location_id: string;
  location_mid: string;
  location_name: string;
  region_name: string;
  isactive: boolean;
  createdat: Date;
  modifiedat: Date;
  createdby: string;
  modifiedby: string;
}
