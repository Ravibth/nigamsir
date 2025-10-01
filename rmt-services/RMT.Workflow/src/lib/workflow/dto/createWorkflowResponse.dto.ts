import { WorkflowTaskModel } from "../../../lib/workflow-task/models/workflow-task.model";
import { WorkflowHistoryModel } from "../../../lib/workflowHistory/models/workflowHistory.model";

import {
  NotificationTemplateTypes,
  WorkflowOutCome,
  WorkflowStatus,
} from "../enum";

export class CreateWorkflowResponse {
  id?: string;

  name: string; // service line

  module: string;

  sub_module: string;

  item_id: string;

  outcome: WorkflowOutCome;

  status: WorkflowStatus;

  created_by: string;

  created_at: Date;

  updated_by: string;
  entity_type: string;
  entity_meta_data: any;
  updated_at?: Date;

  is_active?: boolean;

  task_list: WorkflowTaskModel[];

  history: WorkflowHistoryModel[];

  parent_id: string;
  actions?: string[];
}
