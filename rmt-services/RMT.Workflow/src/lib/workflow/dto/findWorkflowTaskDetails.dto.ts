import {
  IsDateString,
  IsEnum,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import {
  WorkflowOutCome,
  WorkflowStatus,
  WorklFlowModule,
  WorklFlowSubModule,
} from "../enum";
export class findWorkflowTaskDetailsDto {
  @IsOptional()
  @IsString()
  employeeEmail?: string;
  @IsOptional()
  @IsString()
  supercoachEmail?: string;

  @IsOptional()
  @IsEnum(WorkflowOutCome)
  outcome?: WorkflowOutCome;

  @IsOptional()
  @IsEnum(WorklFlowModule)
  module?: WorklFlowModule;

  @IsOptional()
  @IsEnum(WorklFlowSubModule)
  sub_module?: WorklFlowSubModule;

  @IsOptional()
  @IsEnum(WorkflowStatus)
  workflow_task_status?: WorkflowStatus;
  @IsOptional()
  @IsEnum(WorkflowOutCome)
  workflow_status?: WorkflowOutCome;
}
