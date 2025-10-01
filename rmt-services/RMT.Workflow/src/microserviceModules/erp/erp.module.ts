import { Module, Global, Logger } from '@nestjs/common';
import { ErpService } from './erp.service';

@Global()
@Module({
    controllers: [],
    providers: [ErpService, Logger],
    exports: [ErpService],
})
export class ErpModule {}
