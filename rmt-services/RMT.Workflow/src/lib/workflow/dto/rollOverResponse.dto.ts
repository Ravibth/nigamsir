import {
  IsBoolean,
  IsDate,
  IsNumber,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";

export class RollOverResouceAllocationDetailsResponse {
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
  ProjectCode: string;

  @IsOptional()
  @IsString()
  JobName?: string;

  @IsOptional()
  @IsString()
  PipelineName: string;
  @IsOptional()
  @IsString()
  EmpEmail: string;
  @IsOptional()
  @IsString()
  EmpName: string;
  @IsNumber()
  RequisitionId: number;
  @IsString()
  RecordType: string;
  @IsBoolean()
  IsContinuousAllocation: boolean;
  @IsString()
  Description: string;

  @IsNumber()
  TotalEffort: number;
  @IsString()
  AllocationStatus: string;
  @IsString()
  @IsOptional()
  ClientName: string;
  @IsNumber()
  TotalWorkingDays: number;
  @IsNumber()
  ConfirmedPerDayHours: number;
  @IsBoolean()
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
  IsActive: boolean;
}
