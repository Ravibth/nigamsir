import {
  IsEmail,
  IsString,
  IsInt,
  IsBoolean,
  IsOptional,
  IsDate,
  IsArray,
} from 'class-validator';

export class UserDTO {
  @IsInt()
  id: number;

  @IsString()
  role_ids: string;

  @IsEmail()
  email_id: string;

  @IsString()
  name: string;

  @IsString()
  uemail_id: string;

  @IsOptional()
  @IsString()
  entity?: string;

  @IsOptional()
  @IsString()
  employee_id?: string;

  @IsOptional()
  @IsString()
  emp_code?: string;

  @IsString()
  fname: string;

  @IsString()
  lname: string;

  @IsString()
  designation: string;

  @IsOptional()
  @IsString()
  location?: string;

  @IsOptional()
  @IsString()
  region_name?: string;

  @IsOptional()
  @IsString()
  smeg?: string;

  @IsOptional()
  @IsString()
  expertise?: string;

  @IsOptional()
  @IsString()
  business_unit?: string;

  @IsOptional()
  @IsString()
  co_supercoach_name?: string;

  @IsOptional()
  @IsString()
  supercoach_name?: string;

  @IsString()
  service_line: string;

  @IsString()
  roles: string;

  @IsBoolean()
  status: boolean;

  // NEW ADDS
  @IsOptional()
  @IsString()
  reporting_partner_mid?: string;

  @IsArray()
  @IsOptional()
  actionList?: string[];

  @IsOptional()
  @IsString()
  competency?: string;

  @IsOptional()
  @IsString()
  competencyId?: string;

  @IsOptional()
  @IsString()
  supercoach_mid?: string;

  @IsOptional()
  @IsString()
  co_supercoach_mid?: string;
}
