import { IsBoolean, IsIn, IsOptional, IsString } from 'class-validator';
import { EEngagementStatus } from '../enum';

export class UpdateKpiWorkflowDto {
    @IsString()
    @IsOptional()
    parent_id: string;

    @IsString()
    @IsOptional()
    item_id: string;

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
        EEngagementStatus.KPI_EXECUTION_READY_FOR_REVIEW,
    ])
    status: EEngagementStatus;

    @IsBoolean()
    @IsOptional()
    active: boolean;
}
