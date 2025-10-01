import { Module } from '@nestjs/common';
import { WCGTService } from './Wcgt.service';

@Module({
  providers: [WCGTService],
  exports: [WCGTService],
})
export class WCGTModule {}
