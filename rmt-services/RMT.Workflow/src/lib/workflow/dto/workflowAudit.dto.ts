import { IsString } from 'class-validator';

export class WorkFlowAuditDTO {
    @IsString()
    item_id: string;
}
