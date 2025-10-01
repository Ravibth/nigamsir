import { Global, Logger, Module } from "@nestjs/common";
import { AllocationService } from "./allocation.service";

@Global()
@Module({
  controllers: [],
  providers: [AllocationService, Logger],
  exports: [AllocationService],
})
export class AllocationModule {}
