import { IsEnum, IsString } from "class-validator";
import { WorkflowOutCome, WorklFlowModule, WorklFlowSubModule } from "../enum";

export class findWorkflowDto {
  item_id?: string;

  @IsString()
  parent_id?: string;

  @IsEnum(WorkflowOutCome)
  outcome?: WorkflowOutCome;

  @IsString()
  id?: string;

  module?: WorklFlowModule;

  sub_module?: WorklFlowSubModule;
}
