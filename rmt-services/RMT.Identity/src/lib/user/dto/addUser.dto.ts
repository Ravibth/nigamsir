/* eslint-disable @typescript-eslint/no-inferrable-types */
import {
  IsOptional,
  IsString,
  IsEmail,
  IsNotEmpty,
  IsArray,
  IsAlpha,
  IsBoolean,
} from 'class-validator';

export class getUserByNameOrEmailV6Request {
  @IsString()
  name: string;
}
export class AddUserDto {
  @IsNotEmpty()
  @IsEmail()
  email_id: string;

  @IsNotEmpty()
  @IsArray()
  roles: string[];

  @IsNotEmpty()
  @IsArray()
  uemail_id: string;

  @IsNotEmpty()
  @IsString()
  name: string;

  @IsOptional()
  @IsString()
  employee_id?: string;

  @IsNotEmpty()
  @IsString()
  fname: string;

  @IsNotEmpty()
  @IsString()
  lname: string;

  @IsOptional()
  @IsString()
  service_line: string = 'Internal';

  @IsOptional()
  @IsString()
  designation: string = 'Employee';

  @IsOptional()
  @IsString()
  expertise: string;

  @IsOptional()
  @IsString()
  emp_code: string;

  @IsOptional()
  @IsString()
  smeg: string;

  @IsOptional()
  @IsString()
  co_supercoach_name: string;

  @IsOptional()
  @IsString()
  location: string;

  @IsOptional()
  @IsString()
  supercoach_name: string;

  @IsOptional()
  @IsBoolean()
  status: boolean = false;

  @IsOptional()
  @IsString()
  created_by?: string;

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
