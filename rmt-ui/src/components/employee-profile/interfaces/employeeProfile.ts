// types/employeeProfile.ts

export interface EmployeeQualification {
  id: number;
  qualification_type: string;
  qualification: string;
  institute_location_name: string;
  institute_name:string;
  month_year_of_passing: string;
  area_of_specialisation: string;
  employee_mid: string;
  employee_profile_id: number;
  created_at: string;
  modified_at?: string;
  modified_by?: string;
  created_by: string;
  is_published:boolean;
}

export interface EmployeeLanguage {
  id: number;
  employee_mid: string;
  language_name: string;
  read?: string;
  write?: string;
  speak?: string;
  employee_profile_id: number;
  created_at: string;
  modified_at?: string;
  modified_by?: string;
  created_by: string;
}

export interface EmployeeProfile {
  id: number;
  employee_name: string;
  employee_email: string;
  employee_mid: string;
  designation:string;
  employee_code: string;
  business_unit: string;
  competency: string;
  supercoach_email?: string;
  supercoach_mid?: string;
  supercoach_name?: string;
  co_supercoach_email?: string;
  co_supercoach_mid?: string;
  co_supercoach_name?: string;
  location?: string;
  present_work_location?:string;          
  linkedin_url?: string;
  employee_type?: string;
  year_of_experience?: number;
  about_me?: string;
  created_at: string;
  modified_at?: string;
  modified_by?: string;
  created_by: string;
  employee_qualification?: EmployeeQualification[];
  employee_language?: EmployeeLanguage[];
  skillInformation?: skills[];
  employee_industry_expereience?: IndustryExperience[];
  employee_Work_experience?: WorkExperience[];
  employee_project_experience?: ProjectExperience[];
  experience_outside_gt?: ProjectExperienceOutsideGT[];
}

export type SkillLevel = "Excelled" | "Skilled" | "Building" | "Starting";


export interface skills{  
  skillName:string;
  proficiency:SkillLevel;
}

export interface IndustryExperience {
  industry_id: number;
  industry_name: string;
  sub_industry_name: string;
  year_of_experience: number;
  description: string;
  sub_industry_id:number;
}

export interface WorkExperience {
  id: number;
  name_of_employer: string;
  from: string;
  to: string;
  last_designation_held: string;
  details: string;
}

export interface ProjectExperience {  
  id: number;
  job_name:string;
  client_group: string;
  client_name: string;
  business_unit: string;
  offering: string;
  industry: string;
  sub_industry: string;
  role: string;
  duration: string;
  job_start_date: string;
  job_end_date: string;
  primary_el: string;
  csp: string;
  project_type: string;
  project_description: string;
  task_description: string;
  skills_utilized: string;
  actual_hours: number;
  industry_name: string;
  sub_industry_name: string;
}

export interface ProjectExperienceOutsideGT {
    project_name: string;
    client_name: string;
    industry: string;
    sub_industry: string;
    project_location: string;
    project_description: string;
    tasks_performed: string;
    job_start_date: string;
    job_end_date: string;  
}