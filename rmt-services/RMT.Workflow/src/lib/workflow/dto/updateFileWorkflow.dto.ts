import {
  IsEnum,
  IsNotEmpty,
  IsOptional,
  IsString,
  IsUUID,
  ValidateIf,
} from "class-validator";
import { EDepFileStatus } from "../../../microserviceModules/serviceConnector/enum";
import { EWorkflowSteps, WorkflowStatus } from "../enum";

export class UpdateFileWorkflow {
  @IsUUID()
  item_id?: string;

  parent_id?: string;

  workflow_id?: string;

  updated_by?: string;

  @IsNotEmpty()
  @IsEnum(WorkflowStatus)
  status?: WorkflowStatus;

  file_status?: EDepFileStatus;

  @IsString()
  @IsNotEmpty()
  @ValidateIf((o) => o.status === "rejected")
  comment: string;

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
  @IsOptional()
  file_name?: string;

  @IsString()
  @IsOptional()
  module?: string;

  @IsString()
  @IsOptional()
  sub_module?: string;

  @IsString()
  @IsOptional()
  file_id?: string;

  workflow_table_status?: string;

  workflow_table_outcome?: string;

  proxy_approval_by?: string;

  // @IsString()
  id?: string;

  next_step?: EWorkflowSteps;

  next_step_meta?: Record<string, any>;
}
