import {
  IsBoolean,
  IsDate,
  IsNumber,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";

export class ResouceAllocationDetailsResponse {
  @IsNumber()
  Id: number;

  @IsUUID()
  Guid: string;

  @IsString()
  PipelineCode: string;

  @IsUUID()
  @IsOptional()
  JobCode: string;

  @IsString()
  @IsOptional()
  ProjectCode: string;

  @IsOptional()
  @IsString()
  JobName?: string;

  @IsOptional()
  @IsString()
  EmpEmail: string;
  @IsOptional()
  @IsString()
  EmpName: string;
  @IsNumber()
  @IsOptional()
  RequisitionId: number;
  @IsString()
  @IsOptional()
  RecordType: string;

  @IsString()
  @IsOptional()
  AllocationStatus: string;
  @IsString()
  @IsOptional()
  ClientName: string;
  @IsNumber()
  @IsOptional()
  TotalWorkingDays: number;
  @IsNumber()
  @IsOptional()
  ConfirmedPerDayHours: number;
  @IsBoolean()
  @IsOptional()
  isPerDayHourAllocation: boolean;
  @IsNumber()
  @IsOptional()
  ResAllocDetailsId: number;
  @IsDate()
  @IsOptional()
  ConfirmedAllocationStartDate?: Date;
  @IsDate()
  @IsOptional()
  ConfirmedAllocationEndDate?: Date;
  @IsDate()
  @IsOptional()
  CreatedDate?: Date;
  @IsDate()
  @IsOptional()
  ModifiedDate?: Date;
  @IsString()
  @IsOptional()
  CreatedBy?: string;
  @IsString()
  @IsOptional()
  ModifiedBy?: string;
  @IsBoolean()
  @IsOptional()
  IsActive: boolean;
}
