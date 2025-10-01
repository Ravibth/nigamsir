import { CreateWorkflowTaskDto } from "src/lib/workflow-task/dto/workflowTask.dto";
import { UpdateWorkflow } from "./updateWorkflow.dto";
import { WorkflowModel } from "../models/workflow.model";

export class InsertAndUpdateItemDTO {
  updateWorkflowTaskId: string;
  updateWorkflowTaskDto?: UpdateWorkflow;
  createWorkflowTaskDto?: CreateWorkflowTaskDto;
  updateWorkflowDto?: WorkflowModel;
  updateWorkflowId: string;
}
