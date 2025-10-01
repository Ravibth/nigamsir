import { IsIn, IsUUID } from "class-validator";
import { EEngagementStatus } from "../enum";

export class KpiWorkflowStatusDto {
  @IsUUID()
  id: string;

  @IsIn([
    EEngagementStatus.KPI_EXECUTION_ET_INPUT,
    EEngagementStatus.KPI_EXECUTION_CONFIRMED_ET,
    EEngagementStatus.KPI_EXECUTION_COMPLETED,
    EEngagementStatus.KPI_EXECUTION_DROPPED,
    EEngagementStatus.KPI_EXECUTION_EXECUTED,
    EEngagementStatus.KPI_EXECUTION_SELECT,
    EEngagementStatus.KPI_EXECUTION_INPUT_PROVIDE,
    EEngagementStatus.KPI_EXECUTION_REVISION,
  ])
  status: EEngagementStatus;
}
