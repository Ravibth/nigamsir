import { Injectable } from "@nestjs/common";
import { ErpService } from "../../../microserviceModules/erp/erp.service";
import { IUser } from "../../../common/decorators/user.decorator";
import { CreateWorkflow } from "../dto/createWorkflow.dto";
import {
  EEngagementStatus,
  EWorkflowSteps,
  WorkflowOutCome,
  WorkFlowTaskTitle,
  WorklFlowModule,
  WorklFlowSubModule,
} from "../enum";

@Injectable()
export class CreateWorkflowHelper {
  constructor(private erpservice: ErpService) {}
  genEngagementWorkflow(params: CreateWorkflow): CreateWorkflow {
    if (params.status == EEngagementStatus.partner_pending) {
      params.status = EEngagementStatus.partner_pending;
      params.next_step = EWorkflowSteps.Partner_Approval;
    } else {
      params.status = EEngagementStatus.partner_approved;
      params.next_step = EWorkflowSteps.Business_SPOC;
    }

    params["outcome"] = WorkflowOutCome.INPROGRESS;
    return params;
  }

  genFileUploadWorkflow(params: CreateWorkflow): CreateWorkflow {
    params.status = EEngagementStatus.in_progress_file_sanity;
    params.outcome = WorkflowOutCome.INPROGRESS;
    params.next_step = EWorkflowSteps.FILE_UPLOAD;
    return params;
  }

  genFileSanityWorkflow(params: CreateWorkflow): CreateWorkflow {
    params.status = EEngagementStatus.in_progress_file_sanity;
    params.outcome = WorkflowOutCome.INPROGRESS;
    params.next_step = null;
    return params;
  }

  genKpiWorkflow(params: CreateWorkflow): CreateWorkflow {
    params.status = EEngagementStatus.kpi_approval_pending;
    params.outcome = WorkflowOutCome.INPROGRESS;
    params.next_step = WorkFlowTaskTitle.KPI_APPROVAL_SPOC;
    return params;
  }

  generateKpiExecution(params: CreateWorkflow): CreateWorkflow {
    params.status = params.status
      ? params.status
      : EEngagementStatus.KPI_EXECUTION_RUNNING;
    params.outcome = WorkflowOutCome.INPROGRESS;
    params.next_step = params.status as unknown as EWorkflowSteps;
    return params;
  }
  //---> NEW ADDED <---
  genEmployeeAllocationWorkflow(params: CreateWorkflow): CreateWorkflow {
    if (
      params.status ===
      EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER
    ) {
      params.status =
        EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER;
      params.next_step = EWorkflowSteps.EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL;
    } else if (
      params.status ===
      EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH
    ) {
      params.status =
        EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
      params.next_step = EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL;
    } else if (
      params.status ===
      EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE
    ) {
      params.status =
        EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
      params.next_step = EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL;
    } else {
      params.status = params.status;
      params.next_step = params.status as unknown as EWorkflowSteps;
    }
    params.outcome = WorkflowOutCome.INPROGRESS;

    return params;
  }
  genUpdateEmployeeAllocationWorkflow(params: CreateWorkflow): CreateWorkflow {
    if (
      params.status.toLowerCase().trim() ===
      EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER.toLowerCase().trim()
    ) {
      params.status =
        EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER;
      params.next_step = EWorkflowSteps.EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL;
    } else if (
      params.status.toLowerCase().trim() ===
      EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH.toLowerCase().trim()
    ) {
      params.status =
        EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
      params.next_step = EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL;
    } else {
      params.status = params.status;
      params.next_step = params.status as unknown as EWorkflowSteps;
    }
    params.outcome = WorkflowOutCome.INPROGRESS;
    return params;
  }

  getUserSkillAssessmentWorkflow(params: CreateWorkflow): CreateWorkflow {
    if (
      params.status ===
      EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_PENDING
    ) {
      params.next_step =
        EWorkflowSteps.USER_SKILL_ASSESSMENT_PENDING_FOR_SUPERCOACH;
    } else if (
      params.status ===
      EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_PENDING
    ) {
      params.next_step =
        EWorkflowSteps.USER_SKILL_ASSESSMENT_PENDING_FOR_CO_SUPERCOACH;
    }
    params.outcome = WorkflowOutCome.INPROGRESS;
    return params;
  }

  async getWorkflowDetails(
    params: CreateWorkflow,
    user: IUser
  ): Promise<CreateWorkflow> {
    try {
      const sCase = `${params.module}_${params.sub_module}`
        .toLowerCase()
        .trim();

      switch (sCase) {
        case `${WorklFlowModule.ENGAGEMENT}_${WorklFlowSubModule.ENGAGEMENT}`
          .toLowerCase()
          .trim():
          params = this.genEngagementWorkflow(params);
          break;

        case `${WorklFlowModule.FILE}_${WorklFlowSubModule.FILE_WORKFLOW}`
          .toLowerCase()
          .trim():
          params = this.genFileUploadWorkflow(params);
          break;

        case `${WorklFlowModule.FILE}_${WorklFlowSubModule.FILE_SANITY}`
          .toLowerCase()
          .trim():
          params = this.genFileSanityWorkflow(params);
          break;

        case `${WorklFlowModule.KPI}_${WorklFlowSubModule.KPI_WORKFLOW}`
          .toLowerCase()
          .trim():
          params = this.genKpiWorkflow(params);
          break;

        case `${WorklFlowModule.KPI}_${WorklFlowSubModule.KPI_EXECUTION_WORKFLOW}`
          .toLowerCase()
          .trim():
          params = this.generateKpiExecution(params);
          break;
        //-----> NEW ADDED <-----
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION}`
          .toLowerCase()
          .trim():
          params = this.genEmployeeAllocationWorkflow(params);
          break;
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE}`
          .toLowerCase()
          .trim():
          params = this.genUpdateEmployeeAllocationWorkflow(params);
          break;
        case `${WorklFlowModule.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT}_${WorklFlowSubModule.WORKFLOW_SUB_MODULE_USER_SKILL_ASSESSMENT}`:
          params = this.getUserSkillAssessmentWorkflow(params);
          break;
        //add a different type of workflow
        default:
          throw new Error(
            `No case specified for ${params.module} and ${params.sub_module}`
          );
      }
      params.created_by = user.email;
      return params;
    } catch (e) {
      throw e;
    }
  }
}
