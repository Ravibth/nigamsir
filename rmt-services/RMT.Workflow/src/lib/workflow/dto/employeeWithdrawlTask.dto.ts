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
  employeeEmail: string;
}
