import {
  IsBoolean,
  IsDate,
  IsNumber,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";

export class UpdateAllocationResponse {
  @IsNumber()
  Id: number;
  @IsUUID()
  Guid: string;
  @IsString()
  PipelineCode: string;
  @IsUUID()
  JobCode: string;
  @IsString()
  ProjectCode: string;
  @IsOptional()
  @IsString()
  JobName?: string;
  @IsString()
  PipelineName: string;
  @IsString()
  EmpEmail: string;
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
