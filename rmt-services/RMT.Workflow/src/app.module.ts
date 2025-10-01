import { Module } from "@nestjs/common";
import { APP_FILTER } from "@nestjs/core";
import { SequelizeModule, SequelizeModuleOptions } from "@nestjs/sequelize";
import { AppController } from "./app.controller";
import { HttpExceptionFilter } from "./common/filters/httpException.filter";

import { SecretManager } from "./common/secretManager/secretManager";
import { WorkflowModule } from "./lib/workflow/workflow.module";
import { IdentityModule } from "./microserviceModules/identity/identity.module";
import { WorkflowHistoryModule } from "./lib/workflowHistory/workflowHistory.module";
import { EngagementModule } from "./microserviceModules/engagement/engagement.module";
import { ErpModule } from "./microserviceModules/erp/erp.module";
import { ConfigurationModule } from "./microserviceModules/configuration/configuration.module";
import { AllocationModule } from "./microserviceModules/allocation/allocation.module";
import { ProjectModule } from "./microserviceModules/project/project.module";

@Module({
  imports: [
    SequelizeModule.forRootAsync({
      useFactory: async (): Promise<SequelizeModuleOptions> => {
        const secretManager = SecretManager.getInstance();
        return secretManager.dbConfig;
      },
    }),
    WorkflowModule,
    WorkflowHistoryModule,
    EngagementModule,
    ErpModule,
    ConfigurationModule,
    AllocationModule,
    ProjectModule,
    IdentityModule,
  ],

  controllers: [AppController],
  providers: [
    {
      provide: APP_FILTER,
      useClass: HttpExceptionFilter,
    },
  ],
})
export class AppModule {}
