import { Module, Global } from "@nestjs/common";
import { IdentityService } from "./identity.service";

@Global()
@Module({
  controllers: [],
  providers: [IdentityService],
  exports: [IdentityService],
})
export class IdentityModule {}
