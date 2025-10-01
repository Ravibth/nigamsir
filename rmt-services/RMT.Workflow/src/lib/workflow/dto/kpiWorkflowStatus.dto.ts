import { EEngagementStatus } from '../enum';

export class KpiWorkflowStatusDto {
    status: EEngagementStatus;

    next_status: EEngagementStatus[];
}
