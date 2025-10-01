import {
  IsDateString,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import { WorkflowOutCome, WorkflowStatus } from "../enum";

export class getWorkflowByOutcomeWorklFlowModuleAndUpdateDate {
  @IsIn(Object.values(WorkflowOutCome))
  @IsOptional()
  outcome?: WorkflowOutCome;
  @IsOptional()
  module?: string;
  @IsOptional()
  updated_at?: Date;
}
