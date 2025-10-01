import { Module, Logger } from "@nestjs/common";
import { SequelizeModule } from "@nestjs/sequelize";
import { FileMasterModule } from "../../microserviceModules/serviceConnector/fileMaster.module";
import { EngagementModule } from "../../microserviceModules/engagement/engagement.module";
import { WorkflowTaskModel } from "../workflow-task/models/workflow-task.model";
import { WorkflowHistoryModel } from "../workflowHistory/models/workflowHistory.model";
import { WorkflowHistoryModule } from "../workflowHistory/workflowHistory.module";
import { CreateWorkflowHelper } from "./helpers/createWorkflow.helper";
import { WorkflowModel } from "./models/workflow.model";
import { WorkflowRepository } from "./repository/workflow.repository";
import { WorkflowController } from "./workflow.controller";
import { WorkflowService } from "./workflow.service";
import { KpiModule } from "../../microserviceModules/kpi/kpi.module";
import { KpiWorkflowStatusService } from "./kpiWorkflowStatus.service";
import { ConfigurationModule } from "src/microserviceModules/configuration/configuration.module";

@Module({
  imports: [
    SequelizeModule.forFeature([
      WorkflowModel,
      WorkflowTaskModel,
      WorkflowHistoryModel,
    ]),
    WorkflowHistoryModule,
    EngagementModule,
    ConfigurationModule,
    FileMasterModule,
    KpiModule,
  ],
  providers: [
    WorkflowService,
    KpiWorkflowStatusService,
    CreateWorkflowHelper,
    WorkflowRepository,
    Logger,
  ],
  controllers: [WorkflowController],
})
export class WorkflowModule {}
