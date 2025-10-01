export interface EmployeeDto {
  entity: string;
  emp_id: string;
  emp_code: string;
  fname: string;
  lname: string;
  name: string;
  designation: string;
  department: string;
  location: string;
  expertise: string;
  smeg: string;
  email_id: string;
  lwd: string;
  co_supercoach_name: string;
  supercoach_name: string;
  is_active: string;
  cr_date: string;
  up_date: string;
}

export interface IEmployeeQuery {
  email: string;
}
