import { Global, Logger, Module } from "@nestjs/common";
import { ProjectService } from "./project.service";

@Global()
@Module({
  controllers: [],
  providers: [ProjectService, Logger],
  exports: [ProjectService],
})
export class ProjectModule {}
