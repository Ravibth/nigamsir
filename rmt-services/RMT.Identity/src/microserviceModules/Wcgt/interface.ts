export interface IGetUserInfoWcgtRequestDTO {
  emp_emailid?: string;
  emp_mid?: string;
}
export interface IGetUserInfoWcgtResponseDTO {
  employee_mid: string;
  company_name: string;
  employee_code: string;
  first_name: string;
  middle_name: string;
  last_name: string;
  name: string;
  designation_id: string;
  designation?: string;
  department: string;
  location_id: string;
  service_line_id: string;
  service_line?: string;
  email_id: string;
  joining_date: Date;
  reporting_partner_mid: string;
  group_head_mid: string;
  business_unit_id: string;
  business_unit?: string;
  smeg_id: string;
  smeg?: string;
  sme_id: string;
  sme?: string;
  expertise_id: string;
  expertise?: string;
  specical_day: Date;
  birthday: Date;
  isactive: boolean;
  competency: string;
  competencyId: string;
  location: {
    location_id: string;
    location_mid: string;
    location_name: string;
    isactive: true;
    createdat: Date;
    modifiedat: Date;
    createdby: string;
    modifiedby: string;
  };
  createdat: Date;
  modifiedat: Date;
  createdby: string;
  modifiedby: string;
  uemail_id: string;
  employee_id: string;
  supercoach_name: string;
  co_supercoach_name: string;
  supercoach_mid: string;
  co_supercoach_mid: string;
}
