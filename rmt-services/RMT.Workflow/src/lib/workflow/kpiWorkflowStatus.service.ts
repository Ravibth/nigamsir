import { Injectable } from "@nestjs/common";
import { KpiWorkflowStatusDto } from "./dto/kpiWorkflowStatus.dto";
import { EEngagementStatus } from "./enum";

@Injectable()
export class KpiWorkflowStatusService {
  getStatusMapping(): KpiWorkflowStatusDto[] {
    const statusMapping = [
      {
        status: EEngagementStatus.KPI_EXECUTION_SELECT,
        next_status: [
          EEngagementStatus.KPI_EXECUTION_EXECUTED,
          EEngagementStatus.KPI_EXECUTION_ET_INPUT,
        ],
      },

      {
        status: EEngagementStatus.KPI_EXECUTION_EXECUTED,
        next_status: [
          EEngagementStatus.KPI_EXECUTION_READY_FOR_REVIEW,
          EEngagementStatus.KPI_EXECUTION_DROPPED,
        ],
      },

      {
        status: EEngagementStatus.KPI_EXECUTION_READY_FOR_REVIEW,
        next_status: [
          EEngagementStatus.KPI_EXECUTION_CONFIRMED_ET,
          EEngagementStatus.KPI_EXECUTION_DROPPED,
          EEngagementStatus.KPI_EXECUTION_REVISION,
        ],
      },

      {
        status: EEngagementStatus.KPI_EXECUTION_REVISION,
        next_status: [
          EEngagementStatus.KPI_EXECUTION_ET_INPUT,
          EEngagementStatus.KPI_EXECUTION_DROPPED,
          EEngagementStatus.KPI_EXECUTION_EXECUTED,
        ],
      },

      {
        status: EEngagementStatus.KPI_EXECUTION_ET_INPUT,
        next_status: [
          EEngagementStatus.KPI_EXECUTION_INPUT_PROVIDE,
          EEngagementStatus.KPI_EXECUTION_DROPPED,
        ],
      },
      {
        status: EEngagementStatus.KPI_EXECUTION_INPUT_PROVIDE,

        next_status: [
          EEngagementStatus.KPI_EXECUTION_EXECUTED,
          EEngagementStatus.KPI_EXECUTION_READY_FOR_REVIEW,
          EEngagementStatus.KPI_EXECUTION_ET_INPUT,
        ],
      },
      {
        status: EEngagementStatus.KPI_EXECUTION_CONFIRMED_ET,
        next_status: [],
      },

      {
        status: EEngagementStatus.KPI_EXECUTION_DROPPED,
        next_status: [],
      },
    ];

    return statusMapping;
  }
}
