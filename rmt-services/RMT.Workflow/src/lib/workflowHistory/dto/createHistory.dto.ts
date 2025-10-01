import { IsEnum, IsOptional, IsString, IsUUID } from "class-validator";
import { EWorkflowHistoryAction } from "../enum";
import { IHistoryMeta } from "../models/workflowHistory.model";

export class CreateWorkflowHistory {
  @IsEnum(EWorkflowHistoryAction)
  action: EWorkflowHistoryAction;

  @IsUUID()
  workflow_id: string;

  @IsUUID()
  @IsOptional()
  workflow_task_id?: string;

  created_by: string;

  @IsString()
  comments?: string;

  meta?: IHistoryMeta;

  id?: string;
}
