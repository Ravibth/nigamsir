import { IsArray, IsOptional, IsString } from 'class-validator';

export class SuperCoachDelegateListDto {
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  business_unit?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  competency?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  designation?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  grade?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  location?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  employee_mid?: string[];
  @IsOptional()
  @IsArray()
  @IsString({ each: true })
  allocdelegate_mid?:string[];
}
