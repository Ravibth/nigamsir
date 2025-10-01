import { EDepFileStatus } from '../enum';

export class FileDetailsResDto {
    id: string;

    internal_name: string;

    file_name: string;

    status: EDepFileStatus;
}
