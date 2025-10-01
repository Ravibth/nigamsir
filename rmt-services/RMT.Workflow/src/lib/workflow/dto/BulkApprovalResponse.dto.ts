import { UpdateWorkflow } from "./updateWorkflow.dto";
import { UpdateFileWorkflow } from "./updateFileWorkflow.dto";
import { WorkflowModel } from "../models/workflow.model";
import { WorkflowTaskModel } from "src/lib/workflow-task/models/workflow-task.model";
import { NotificationTemplateTypes } from "../enum";

export class BulkApprovalResponse {
  requestPayload: { item_id: string; workflow_id: string };
  workflowResult: WorkflowModel | null;
  actions: string[] | null;
  result: WorkflowTaskModel[] | null;
  error: any;
  isError: boolean;
}
