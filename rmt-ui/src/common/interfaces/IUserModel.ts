export interface IUserModelMaster {
  id?: number;
  role_ids?: string;
  email_id?: string;
  name?: string;
  entity?: string;
  emp_code?: string;
  fname?: string;
  lname?: string;
  designation: string;
  grade: string;
  location?: string;
  smeg?: string;
  expertise?: string;
  business_unit?: string;
  co_supercoach_name?: string;
  supercoach_name?: string;
  service_line?: string;
  roles?: string;
  status?: boolean;
  is_active?: boolean;
  created_by?: string;
  created_at?: Date;
  updated_by?: string;
  updated_at?: Date;
  role_list?: IUserRoleModelMaster[];
  order?: number;
  sector?: string;
  industry?: string;
  competency?: string;
  competencyId?: string;
}
export interface IUserRoleModelMaster {
  id?: string;
  user?: string;
  role?: string;
  is_active?: boolean;
  created_by?: string;
  created_at?: Date;
  updated_by?: string;
  updated_at?: Date;
}
