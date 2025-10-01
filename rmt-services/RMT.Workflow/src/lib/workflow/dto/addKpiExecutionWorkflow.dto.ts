import { IsArray, IsIn, IsString, IsUUID } from "class-validator";
import { EEngagementStatus } from "../enum";

export class AddKpiExecutionWorkflow {
  @IsUUID()
  engagement_id: string;

  @IsString()
  name: string;

  @IsArray()
  item_id: string[];

  @IsIn([
    EEngagementStatus.KPI_EXECUTION_ET_INPUT,
    EEngagementStatus.KPI_EXECUTION_CONFIRMED_ET,
    EEngagementStatus.KPI_EXECUTION_COMPLETED,
    EEngagementStatus.KPI_EXECUTION_DROPPED,
    EEngagementStatus.KPI_EXECUTION_EXECUTED,
    EEngagementStatus.KPI_EXECUTION_SELECT,
    EEngagementStatus.KPI_EXECUTION_INPUT_PROVIDE,
    EEngagementStatus.KPI_EXECUTION_REVISION,
    EEngagementStatus.KPI_EXECUTION_FAILED,
  ])
  status: EEngagementStatus;
}
