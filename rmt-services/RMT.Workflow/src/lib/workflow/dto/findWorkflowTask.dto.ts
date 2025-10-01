import {
  IsDateString,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import { WorkflowOutCome, WorkflowStatus } from "../enum";

export class findWorkflowTaskDto {
  @IsIn(Object.values(WorkflowOutCome))
  @IsOptional()
  outcome?: WorkflowOutCome;
  @IsOptional()
  status?: WorkflowStatus;
  @IsOptional()
  @IsDateString()
  workflowTask_due_date?: string;
  @IsOptional()
  taskstatus?: string;
  @IsOptional()
  assigned_to?: string;
}
