import { IsEnum, IsOptional, IsUUID } from 'class-validator';
import { EWorkflowHistoryAction } from '../enum';

export class FindWorkflowHistory {
    @IsEnum(EWorkflowHistoryAction)
    @IsOptional()
    action: EWorkflowHistoryAction;

    @IsUUID()
    @IsOptional()
    workflow_id: string;

    @IsUUID()
    @IsOptional()
    workflow_task_id: string;
}
