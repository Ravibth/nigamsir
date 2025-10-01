import { Module } from "@nestjs/common";
import { SequelizeModule } from "@nestjs/sequelize";
import { WorkflowHistoryModel } from "./models/workflowHistory.model";
import { WorkflowHistoryRepository } from "./repository/workflowHistory.repository";
import { WorkflowHistoryController } from "./workflowHistory.controller";
import { workflowHistoryService } from "./workflowHistory.service";

@Module({
  imports: [SequelizeModule.forFeature([WorkflowHistoryModel])],
  providers: [WorkflowHistoryRepository, workflowHistoryService],
  controllers: [WorkflowHistoryController],
  exports: [workflowHistoryService],
})
export class WorkflowHistoryModule {}
