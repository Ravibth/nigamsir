import {
  IsDateString,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import { WorkflowOutCome, WorkflowStatus } from "../enum";

export class findWorkflowTaskDtoChanges {
  @IsIn(Object.values(WorkflowOutCome))
  @IsOptional()
  outcome?: WorkflowOutCome;
  @IsOptional()
  taskstatus?: string;
  @IsOptional()
  due_date?: Date;
}
