import { IsIn } from 'class-validator';
import { EWorkflowSteps } from '../enum';

export class CreateSubtaskDto {
    @IsIn(Object.values(EWorkflowSteps))
    task_type: EWorkflowSteps;

    engagement_id: string;

    file_id: string;
}
