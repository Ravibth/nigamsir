import { Module, Global } from '@nestjs/common';
import { FileMasterService } from './fileMaster.service';

@Global()
@Module({
    controllers: [],
    providers: [FileMasterService],
    exports: [FileMasterService],
})
export class FileMasterModule {}
