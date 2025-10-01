import {
  IsArray,
  IsBoolean,
  IsNumber,
  IsOptional,
  IsString,
} from 'class-validator';

export class UserDetailsWCGTDto {
  @IsOptional()
  @IsNumber()
  id: number;

  @IsOptional()
  @IsString()
  emp_Email: string;

  @IsOptional()
  @IsString()
  emp_Name: string;

  @IsOptional()
  @IsString()
  emp_Designation: string;

  @IsOptional()
  @IsString()
  emp_Expertise: string;

  @IsOptional()
  @IsString()
  emp_SME: string;

  @IsOptional()
  @IsArray()
  role_list: string[];

  @IsOptional()
  @IsBoolean()
  status: boolean;
}
