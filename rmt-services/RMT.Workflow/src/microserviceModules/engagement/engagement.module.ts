import { Module, Global, Logger } from "@nestjs/common";
import { EngagementService } from "./engagement.service";

@Global()
@Module({
  controllers: [],
  providers: [EngagementService, Logger],
  exports: [EngagementService],
})
export class EngagementModule {}
