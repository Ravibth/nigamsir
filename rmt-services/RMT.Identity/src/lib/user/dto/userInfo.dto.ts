import {
  IsArray,
  IsBoolean,
  IsDate,
  IsNumber,
  IsOptional,
  IsString,
} from 'class-validator';

export class UserInfoDTO {
  @IsOptional()
  @IsNumber()
  id?: number;

  @IsOptional()
  @IsString()
  role_ids?: string;

  @IsOptional()
  @IsString()
  employee_id?: string;

  @IsOptional()
  @IsString()
  email_id?: string;

  @IsOptional()
  @IsString()
  name?: string;

  @IsOptional()
  @IsString()
  emp_code?: string;

  @IsOptional()
  @IsArray()
  uemail_id: string;

  @IsOptional()
  @IsString()
  fname?: string;
  @IsOptional()
  @IsString()
  lname?: string;

  @IsOptional()
  @IsString()
  designation?: string;

  @IsOptional()
  @IsString()
  grade?: string;

  @IsOptional()
  @IsString()
  service_line?: string;

  @IsOptional()
  @IsString()
  roles?: string;

  @IsOptional()
  @IsArray()
  role_list?: string[];

  @IsOptional()
  @IsBoolean()
  status?: boolean;

  @IsOptional()
  @IsBoolean()
  is_active?: boolean;

  @IsOptional()
  @IsBoolean()
  is_existing?: boolean;

  @IsOptional()
  @IsString()
  created_by?: string;

  @IsOptional()
  @IsDate()
  created_at?: Date;

  @IsOptional()
  @IsString()
  updated_by?: string;

  @IsOptional()
  @IsDate()
  updated_at?: Date;

  @IsOptional()
  @IsString()
  supercoach_name?: string;

  @IsOptional()
  @IsString()
  co_supercoach_name?: string;

  @IsOptional()
  @IsString()
  smeg?: string;

  @IsOptional()
  @IsString()
  expertise?: string;

  @IsOptional()
  @IsString()
  location?: string;

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
