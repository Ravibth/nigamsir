import { Module } from '@nestjs/common';
import { ErpConnectorService } from './erp.service';

@Module({
  providers: [ErpConnectorService],
  exports: [ErpConnectorService],
})
export class ErpConnectorModule {}
