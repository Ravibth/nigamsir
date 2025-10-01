import {
  IsOptional,
  IsString,
  IsUUID,
  IsEnum,
  ValidateIf,
  IsNotEmpty,
} from "class-validator";
import { EEngagementStatus, EWorkflowStep, WorkflowOutCome } from "../enum";
export interface IConfiguration {
  skilDueDateconfiguration?: any;
  reviewerconfiguration?: any;
  supercoachconfiguration?: any;
  employeeconfiguration?: any;
  employeeWithdrawlConfiguration?: any;
  resourceRequestorConfiguration?: any;
  cspConfigurations?: any;
}

export class CreateWorkflow {
  @IsString()
  name: string; // service line

  @IsString()
  module: string;

  @IsString()
  sub_module: string;

  @IsUUID()
  item_id: string;

  @IsOptional()
  @IsString()
  created_by?: string;

  @IsString()
  @IsNotEmpty()
  @ValidateIf((o) => o.name === "stat")
  assigned_to: string;

  @IsOptional()
  @IsEnum(EEngagementStatus)
  status?: EEngagementStatus;

  @IsOptional()
  @IsEnum(WorkflowOutCome)
  outcome?: WorkflowOutCome;

  next_step?: EWorkflowStep;

  @IsOptional()
  entity_type?: string;
  @IsOptional()
  entity_meta_data?: any;
  @IsOptional()
  assigned_to_json?: any;

  @IsOptional()
  @IsString()
  parent_id?: string;

  @IsOptional()
  configurations?: IConfiguration = {};

  @IsOptional()
  comments?: string;
}
