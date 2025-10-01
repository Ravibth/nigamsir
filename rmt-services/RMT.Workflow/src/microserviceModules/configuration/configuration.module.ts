import { Global, Logger, Module } from "@nestjs/common";
import { ConfigurationService } from "./configuration.service";

@Global()
@Module({
  controllers: [],
  providers: [ConfigurationService, Logger],
  exports: [ConfigurationService],
})
export class ConfigurationModule {}
