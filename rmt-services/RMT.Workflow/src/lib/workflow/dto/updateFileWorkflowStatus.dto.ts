import { IsArray, IsOptional, IsString } from 'class-validator';
import { EDepFileStatus } from '../../../microserviceModules/serviceConnector/enum';

class FileStatus {
    @IsString()
    file_name: string;

    @IsString()
    status: EDepFileStatus;
}

export class UpdateFileWorkflowStatusDto {
    @IsString()
    @IsOptional()
    parent_id: string;

    @IsString()
    @IsOptional()
    item_id: string;

    @IsArray()
    file_status: FileStatus[];
}
