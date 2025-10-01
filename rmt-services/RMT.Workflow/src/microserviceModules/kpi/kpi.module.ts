import { Module, Global } from "@nestjs/common";
import { KpiService } from "./kpi.service";

@Global()
@Module({
  controllers: [],
  providers: [KpiService],
  exports: [KpiService],
})
export class KpiModule {}
