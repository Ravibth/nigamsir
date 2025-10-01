import {
  IsBoolean,
  IsDateString,
  IsNumber,
  IsOptional,
  IsString,
} from "class-validator";

export class ProjectRolesResponse {
  @IsOptional()
  @IsNumber()
  id?: number;
  @IsOptional()
  @IsNumber()
  projectId?: number;
  @IsOptional()
  @IsString()
  user?: string;
  @IsOptional()
  @IsString()
  userName?: string;
  @IsOptional()
  @IsString()
  role?: string;
  @IsOptional()
  @IsString()
  description?: string;
  @IsOptional()
  @IsString()
  delegateUserName?: string;
  @IsOptional()
  @IsString()
  delegateEmail?: string;
  @IsOptional()
  @IsBoolean()
  isactive: boolean;
  @IsOptional()
  @IsDateString()
  createdat: Date;
  @IsOptional()
  @IsDateString()
  modifiedat: Date;
  @IsOptional()
  @IsString()
  createdby: string;
  @IsOptional()
  @IsString()
  modifiedby: string;
}
//  public string? DelegateUserName { get; set; }
//  public string? DelegateEmail { get; set; }
