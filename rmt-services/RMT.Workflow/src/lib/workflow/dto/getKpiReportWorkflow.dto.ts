import { Transform } from 'class-transformer';
import { IsArray, IsNotEmpty, IsUUID } from 'class-validator';

export class GetKpiReportWorkflowDTO {
    @IsNotEmpty()
    @IsUUID()
    parent_id: string;

    @IsArray()
    @IsNotEmpty()
    @Transform(({ value }) => {
        if (value.length > 1) return value.split(',');
        else return value;
    })
    item_id: string[];
}
