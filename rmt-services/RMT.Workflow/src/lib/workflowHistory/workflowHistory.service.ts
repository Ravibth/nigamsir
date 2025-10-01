import { Injectable } from "@nestjs/common";
import { WorkflowTaskModel } from "../workflow-task/models/workflow-task.model";
import {
  WorklFlowModule,
  WorklFlowSubModule,
  WorkflowStatus,
  WorkFlowTaskTitle,
  EEngagementStatus,
} from "../workflow/enum";
import { WorkflowModel } from "../workflow/models/workflow.model";
import { EWorkflowHistoryAction } from "./enum";
import { WorkflowHistoryRepository } from "./repository/workflowHistory.repository";
import { UpdateKpiWorkflowDto } from "../workflow/dto/updateKpiWorkflow.dto";

interface IActionDetails {
  action: EWorkflowHistoryAction;
  comments: string;
}

interface IActionDetailsReq {
  status: string;
  title: string;
}

@Injectable()
export class workflowHistoryService {
  constructor(private repository: WorkflowHistoryRepository) {}

  public async addEngagementLog(params: WorkflowModel): Promise<void> {
    try {
      const log = {
        action: EWorkflowHistoryAction.NEW_WORKFLOW,
        workflow_id: params.id,
        created_by: params.created_by,
        // workflow_task_id: "",
        comments: `new workflow has been added  for module ${params.module} and sub-module ${params.sub_module}`,
        meta: { workflow_current_status: params.status },
      };
      await this.repository.addHistory([log]);
    } catch (error) {
      throw error;
    }
  }

  public async addWorkflowHistory(params: WorkflowModel): Promise<void> {
    try {
      const { action, comments } = this.getActionDetails(
        {
          status: params.status,
          title: "",
        },
        params
      );
      const log = {
        action,
        workflow_id: params.id,
        created_by: params.updated_by || params.created_by,
        workflow_task_id: "",
        comments,
        meta: { workflow_current_status: params.status },
      };
      await this.repository.addHistory([log]);
    } catch (error) {
      throw error;
    }
  }

  public async addKpiHistory(
    params: WorkflowModel,
    args: UpdateKpiWorkflowDto,
    task?: WorkflowTaskModel
  ): Promise<void> {
    try {
      const actionDetails = {
        action: EWorkflowHistoryAction.KPI_ACTIVE_STATUS_CHANGE,
        comments: `kpi status has been changes to ${args.active}`,
      };
      const log = {
        ...actionDetails,
        workflow_id: params.id,
        created_by: params.updated_by || params.created_by,
        workflow_task_id: task?.id || "",

        meta: { workflow_current_status: params.status },
      };
      await this.repository.addHistory([log]);
    } catch (error) {
      throw error;
    }
  }

  public async addEngagementTaskLog(
    params: WorkflowTaskModel[],
    workflowDetails: WorkflowModel,
    workflow_current_status?: string
  ): Promise<void> {
    try {
      const log = params.map((taskDetails) => ({
        workflow_id: taskDetails.workflow_id,
        created_by: taskDetails.created_by,
        workflow_task_id: taskDetails.id,
        ...this.getActionDetails(taskDetails, workflowDetails),
        meta: {
          workflow_current_status: workflow_current_status
            ? workflow_current_status
            : workflowDetails.status,
        },
      }));

      await this.repository.addHistory(log);
    } catch (error) {
      throw error;
    }
  }

  getActionDetails(
    params: WorkflowTaskModel | IActionDetailsReq,
    workflow: WorkflowModel
  ): IActionDetails {
    let actionDetails: IActionDetails = {
      action: EWorkflowHistoryAction.NO_ACTION_FOUND,
      comments: "for more details look into other references",
    };
    const caseName = `${workflow.module}${workflow.sub_module}${params.status}${params.title}`;

    switch (caseName) {
      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.PENDING}${WorkFlowTaskTitle.WF_TASK_TITLE_PARTNER}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_PARTNER_APPROVAL_CREATE,
          comments: "partner approval task added",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_PARTNER}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_PARTNER_REJECT,
          comments: "approval reject by partner",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_PARTNER}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_PARTNER_APPROVED,
          comments: "approved by partner",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_SPOC}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_BUSINESS_SPOC_APPROVED,
          comments: "approved by business SPOC",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.PENDING}${WorkFlowTaskTitle.WF_TASK_TITLE_SPOC}`:
        actionDetails = {
          action:
            EWorkflowHistoryAction.ENGAGEMENT_BUSINESS_SPOC_APPROVAL_CREATE,
          comments: "business SPOC approval task added",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_SPOC}`:
        actionDetails = {
          action:
            EWorkflowHistoryAction.ENGAGEMENT_BUSINESS_SPOC_APPROVAL_CREATE,
          comments: "approval reject by business SPOC",
        };
        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_EXECUTED,
          comments: "kpi execution task is executed successfully",
        };
        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION_SELECT}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_SELECT,
          comments: "kpi execution task selected by DEP",
        };
        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION_CONFIRMED}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_CONFIRMED_BY_ET,
          comments: "kpi execution confirmed by ET",
        };

        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION_REVIEW}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_READY_REVIEW,
          comments: "kpi execution failed",
        };

        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION_REVISION}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_REVISION,
          comments: "kpi execution execution revision required request",
        };
        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_RUNNING}${WorkFlowTaskTitle.KPI_EXECUTION_INPUT_PROVIDED}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_INPUT_PROVIDED,
          comments: "input provided by ET",
        };
        break;

      case `${WorklFlowModule.KPI}${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}${EEngagementStatus.KPI_EXECUTION_DROPPED}${WorkFlowTaskTitle.KPI_EXECUTION_ET_INPUT}`:
        actionDetails = {
          action: EWorkflowHistoryAction.KPI_EXECUTION_INPUT_PENDING,
          comments: "input pending by ET",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${EEngagementStatus.engagement_accepted}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_ACCEPTED,
          comments: "engagement status has been changed to engagement accepted",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${EEngagementStatus.ENGAGEMENT_DELIVERED}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_ACCEPTED,
          comments:
            "all kpis have been successfully dropped/confirmed. Engagement marked as Delivered",
        };
        break;

      case `${WorklFlowModule.ENGAGEMENT}${WorklFlowSubModule.ENGAGEMENT}${EEngagementStatus.ENGAGEMENT_CLOSE}`:
        actionDetails = {
          action: EWorkflowHistoryAction.ENGAGEMENT_ACCEPTED,
          comments: "this engagement has been closed successfully",
        };
        break;

      default:
        break;
    }

    return actionDetails;
  }
}
