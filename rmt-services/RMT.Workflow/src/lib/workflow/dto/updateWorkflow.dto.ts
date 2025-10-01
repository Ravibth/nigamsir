import {
  IsArray,
  IsDateString,
  IsEnum,
  IsNotEmpty,
  IsOptional,
  IsString,
  IsUUID,
  ValidateIf,
} from "class-validator";
import { EEngagementStatus, EWorkflowSteps, WorkflowStatus } from "../enum";
import { IConfiguration } from "./createWorkflow.dto";

class UpsertEngagementTeamMember {
  engagement_id?: string;

  @IsNotEmpty()
  @IsString()
  emp_name: string;

  @IsNotEmpty()
  @IsString()
  user_type: string;

  @IsNotEmpty()
  @IsString()
  email_id: string;

  created_by?: string;
}

export class UpdateEngagementByCEODto {
  // @IsString()
  id: string;

  // @IsString()
  updated_by: string;

  @IsArray()
  resources: UpsertEngagementTeamMember[];

  @IsOptional()
  @IsString()
  version: string;

  @IsOptional()
  @IsString()
  status: EEngagementStatus.accepted_by_coe_admin;

  @IsNotEmpty()
  @IsDateString()
  coe_start_date?: Date;

  @IsOptional()
  @IsString()
  remarks: string;
}
export class NotificationAction {
  action: string;
}
export interface IAssignedToMeta {
  resourceRequestorEmails?: string;
}
export class UpdateWorkflow {
  @IsUUID()
  @IsOptional()
  workflow_id?: string;

  @IsUUID()
  item_id?: string;

  workflow_task_id?: string;
  @IsOptional()
  workflow_task_title?: string;

  parent_id?: string;

  updated_by?: string;

  @IsOptional()
  assigned_to?: string;

  @IsNotEmpty()
  @IsEnum(WorkflowStatus)
  status?: WorkflowStatus;

  @IsString()
  @IsNotEmpty()
  @ValidateIf((o) => o.status === "rejected")
  @IsOptional()
  comment?: string;

  @IsString()
  @IsOptional()
  remarks?: string;

  @IsString()
  @IsOptional()
  type?: string;

  @IsOptional()
  @IsString()
  service_line?: string;

  @IsString()
  workflow_table_status?: string;

  @IsString()
  workflow_table_outcome?: string;

  @IsString()
  proxy_approval_by?: string;

  // @IsString()
  id?: string;

  @IsEnum(EWorkflowSteps)
  next_step?: EWorkflowSteps;

  @IsOptional()
  coe_acceptance_meta?: UpdateEngagementByCEODto;

  @IsOptional()
  assigned_to_meta?: IAssignedToMeta = {};

  @IsOptional()
  assigned_to_json?: any;

  @IsOptional()
  configuration?: IConfiguration = {};
}

export class UpdateWorkflowHistoryDto {
  @IsUUID()
  item_id?: string;

  updated_by?: string;

  @IsNotEmpty()
  @IsEnum(EEngagementStatus)
  status?: EEngagementStatus;
}
