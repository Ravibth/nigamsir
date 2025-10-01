import {
  IsDateString,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import { WorkflowOutCome, WorkflowStatus } from "../enum";

export class findWorkflowDto {
  @IsUUID()
  @IsOptional()
  id?: string;

  @IsUUID()
  @IsOptional()
  item_id?: string;

  @IsUUID()
  @IsOptional()
  parent_id?: string;

  @IsIn(Object.values(WorkflowOutCome))
  @IsOptional()
  outcome?: WorkflowOutCome;

  @IsOptional()
  assigned_to?: string;
  @IsOptional()
  status?: WorkflowStatus;
  @IsOptional()
  @IsDateString()
  workflowTask_due_date?: string;
}
