import {
  BadRequestException,
  Body,
  Controller,
  Get,
  Post,
  Query,
} from "@nestjs/common";
import { IUser, User } from "../../common/decorators/user.decorator";
import { AddKpiExecutionWorkflow } from "./dto/addKpiExecutionWorkflow.dto";
import { CreateWorkflow } from "./dto/createWorkflow.dto";
import { findWorkflowDto } from "./dto/findWorkflow.dto";
import { GetKpiReportWorkflowDTO } from "./dto/getKpiReportWorkflow.dto";
import { KpiWorkflowStatusDto } from "./dto/kpiWorkflowStatus.dto";
import { UpdateFileWorkflow } from "./dto/updateFileWorkflow.dto";
import { UpdateFileWorkflowStatusDto } from "./dto/updateFileWorkflowStatus.dto";
import { UpdateKpiWorkflowDto } from "./dto/updateKpiWorkflow.dto";
import {
  UpdateWorkflow,
  UpdateWorkflowHistoryDto,
} from "./dto/updateWorkflow.dto";
import { WorkFlowAuditDTO } from "./dto/workflowAudit.dto";
import { ProxyApproval } from "./enum";
import { KpiWorkflowStatusService } from "./kpiWorkflowStatus.service";
import { WorkflowModel } from "./models/workflow.model";
import { WorkflowService } from "./workflow.service";
import { WorkflowTaskModel } from "../workflow-task/models/workflow-task.model";
import { findWorkflowTaskDto } from "./dto/findWorkflowTask.dto";
import { findWorkflowTaskDtoChanges } from "./dto/findWorkflowTaskChange.dto";
import { findWorkflowTaskDetailsDto } from "./dto/findWorkflowTaskDetails.dto";
import { CreateWorkflowResponse } from "./dto/createWorkflowResponse.dto";
import { terminateWorkflowByItemIdDTO } from "./dto/terminateWorkflowByItemId.dto";
import { GetCommentsByItemIdRequest } from "./dto/getCommentsByItemIdRequest.dto";
import { getMultipleTasksByByQueryDto } from "./dto/getMultipleTasksByByQuery.dto";
import { RefreshWorkflowTaskAssignment } from "./dto/refreshWorkflowTaskAssignment.dto";
import { terminateWorkflowByPipelineCodeAndJobCodeDTO } from "./dto/terminateWorkflowByPipelineCodeAndJobCode";
import { getWorkflowByOutcomeWorklFlowModuleAndUpdateDate } from "./dto/getWorkflowByOutcomeWorklFlowModuleAndUpdateDate.dto";
import { get } from "http";
import { UpdateSupercoachAndDelegateDto } from "./dto/updateSupercoachAndDelegate.dto";

@Controller("/")
export class WorkflowController {
  constructor(
    private service: WorkflowService,
    private kpiWorkflowStatus: KpiWorkflowStatusService
  ) {}

  @Get("v1")
  public async findAll(): // @Body() params: CreateWorkflow
  Promise<WorkflowModel[]> {
    try {
      return await this.service.findAll();
    } catch (error) {
      throw error;
    }
  }

  @Post("v1")
  public async add(
    @Body() params: CreateWorkflow,
    @User() user: IUser
  ): Promise<CreateWorkflowResponse> {
    try {
      console.log("user of add workflow", user);
      console.log("params of add work flow", params);
      params.name = params.name.toLowerCase();
      const workflow = await this.service.addWorkflow(params, user);
      console.log("workflow added.");
      return workflow;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
  @Get("v1/GetWorkflowTasksDetailsByQuery")
  public async getWorkflowTasksDetailsByQuery(
    @Query() params: findWorkflowTaskDetailsDto
  ) {
    try {
      const workflowTasks = await this.service.getWorkflowTasksDetails(params);
      return workflowTasks;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
  @Get("v1/GetWorkflowSuperCoachTask")
  public async getWorkflowSuperCoachTask(
    @Query() params: findWorkflowTaskDetailsDto
  ) {
    try {
      const workflowTask =
        await this.service.getAllocationSupercoachWorkflowTask(params);
      return workflowTask;
    } catch (error) {
      throw error;
    }
  }
  @Get("v1/GetEmployeeWithdrawlTaskByQuery")
  public async getEmployeeWithdrwalTaskByQuery(
    @Query() params: findWorkflowTaskDetailsDto
  ) {
    try {
      const workflowWithdrwalTask = await this.service.getEmployeeWithdrwalTask(
        params
      );
      return workflowWithdrwalTask;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  @Get("v1/GetEmployeeTaskCountByQuery")
  public async getEmployeeTaskCountByQuery(
    @Query() params: findWorkflowTaskDto
  ) {
    try {
      const workflowTaskCount = await this.service.getEmployeeTaskCountByQuery(
        params
      );
      return workflowTaskCount;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  @Post("v1/data-processing-task")
  public async addWorkflowTask(
    @Body() params: CreateWorkflow,
    @User() user: IUser
  ): Promise<CreateWorkflowResponse> {
    try {
      params.name = params.name.toLowerCase();
      const workflow = await this.service.addWorkflow(params, user);
      return workflow;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  @Get("v1/query")
  public async getByQuery(
    @Query() params: findWorkflowDto
  ): Promise<WorkflowModel[]> {
    try {
      console.log("query", params);
      const workflow = await this.service.getWorkflowByQuery(params);

      return workflow;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
  @Get("v1/workflowClosedByUpdateDate")
  public async getWorkflowByOutcomeWorklFlowModuleAndUpdateDate(
    @Query() params: getWorkflowByOutcomeWorklFlowModuleAndUpdateDate
  ) {
    try {
      return await this.service.getWorkflowByUpdateQuery(params);
    } catch (error) {
      throw error;
    }
  }

  @Get("v1/workflowTasks/query")
  public async getWorkflowTasksByQuery(
    @Query() params: findWorkflowTaskDto
  ): Promise<WorkflowTaskModel[]> {
    try {
      console.log("query", params);
      const workflowTasks = await this.service.getWorkflowTasksByQuery(params);
      return workflowTasks;
    } catch (error) {
      throw new Error(error);
    }
  }
  @Get("v1/workflowTasks/queryByWorkflowStatusAndTaskStatus")
  public async getWorkflowTasksByWorkflowStatusAndTaskStatus(
    @Query() params: findWorkflowTaskDtoChanges
  ): Promise<WorkflowTaskModel[]> {
    try {
      console.log("query", params);
      const workflowTasks =
        await this.service.getWorkflowTasksByWorkflowStatusAndTaskStatus(
          params
        );
      return workflowTasks;
    } catch (error) {
      throw new Error(error);
    }
  }

  @Post("v1/update-workflow")
  public async updateApproval(
    @Body() params: UpdateFileWorkflow & UpdateWorkflow,
    @User() user: IUser
  ) {
    try {
      console.log("update-workflow==>", params, user);
      params.service_line = params.service_line || user.service_line;
      return await this.service.updateApproval(params, user);
    } catch (error) {
      throw error;
    }
  }
  @Post("v1/bulk/update-workflow")
  public async updateApprovalsInBulk(
    @Body() params: UpdateFileWorkflow[] & UpdateWorkflow[],
    @User() user: IUser
  ) {
    try {
      const result = await this.service.bulkUpdateApproval(params, user);
      return result;
    } catch (error) {
      throw new Error(error);
    }
  }
  @Post("v2/bulk/update-workflow")
  public async updateApprovalsBulk(
    @Body() params: UpdateFileWorkflow[] & UpdateWorkflow[],
    @User() user: IUser
  ) {
    try {
      const result = await this.service.bulkUpdateApproval(params, user);
      return result;
    } catch (error) {
      throw new Error(error);
    }
  }
  @Post("update-supercoach-and-delegate")
  public async updateSupercoachAndDelegate(
    @Body() params: UpdateSupercoachAndDelegateDto,
    @User() user: IUser
  ) {
    const result = await this.service.updateSupercoachAndDelegate(params, user);
    return result;
  }
  @Post("v1/employee-allocation-termination")
  public async employeeAllocationTermination(@User() user: IUser) {
    try {
      console.log("date Information", new Date(2024, 1, 1));
      this.service.employeeAllocationTermination(user, new Date());
    } catch (error) {
      throw new Error(error);
    }
  }
  @Post("v1/terminate-inprogress-workflow-by-ItemId")
  public async terminateWorkflowByListOfItemId(
    @Body() ItemId: terminateWorkflowByItemIdDTO[],
    @User() user: IUser
  ) {
    try {
      const result = await this.service.terminateWorkflowByListOfItemId(
        ItemId,
        user
      );
      return result;
    } catch (error) {
      throw new Error(error);
    }
  }
  @Post("v1/terminateWorkflowByPipelineCodeAndJobCode")
  public async terminateWorkflowByPipelineCodeAndJobCode(
    @Body() params: terminateWorkflowByPipelineCodeAndJobCodeDTO
  ) {
    try {
      const result =
        await this.service.terminateWorkflowByPipelineCodeAndJobCode(params);
    } catch (error) {
      throw error;
    }
  }

  public async;

  @Post("v1/update-file-workflow-status")
  public async updateFileWorkflow(
    @Body() params: UpdateFileWorkflowStatusDto,
    @User() user: IUser
  ) {
    try {
      return await this.service.updateFileWorkflowStatus(params as any, user);
    } catch (error) {
      throw error;
    }
  }

  @Post("v1/refresh-workflow-task-assignment")
  public async refreshWorkflowTaskAssignment(
    @Body() params: RefreshWorkflowTaskAssignment,
    @User() user: IUser
  ) {
    try {
      return await this.service.refreshAssignment(params, user);
    } catch (error) {
      throw error;
    }
  }
  @Post("v1/update-kpi-workflow-status")
  public async updateKpiWorkflow(
    @Body() params: UpdateKpiWorkflowDto,
    @User() user: IUser
  ) {
    try {
      return await this.service.updateKpiExecutionWorkflow(params as any, user);
    } catch (error) {
      throw error;
    }
  }

  @Post("v1/add-kpi-workflow")
  public async addKpiExecutionWorkflow(
    @Body() params: AddKpiExecutionWorkflow,
    @User() user: IUser
  ) {
    try {
      return await this.service.addKpiExecutionWorkflow(params as any, user);
    } catch (error) {
      throw error;
    }
  }

  // @Post("v1/update-proxy-workflow")
  // public async updateProxyApproval(@Body() params, @User() user: IUser) {
  //   try {
  //     const roles = user.roles;
  //     const isproxyApproval = roles.find((el) => {
  //       if (
  //         el.toLowerCase() === ProxyApproval.SYSTEM_ADMIN.toLowerCase() ||
  //         el.toLowerCase() === ProxyApproval.ADMIN.toLowerCase()
  //       ) {
  //         return el;
  //       }
  //     });

  //     if (isproxyApproval != null) {
  //       const res = await this.service.updateApproval(
  //         params,
  //         user,
  //         isproxyApproval
  //       );
  //       console.log("res resr resr", res);
  //       if (res && res.length == 2) {
  //         const engid = params.engagement_id;
  //         const sttaus = "Pending for Business SPOC Approval";

  //         await this.service.updateEngagementForProxy(false, user, {
  //           id: engid,
  //           toUpdate: {
  //             status: sttaus,
  //           },
  //         });
  //         return res;
  //       } else {
  //         throw new BadRequestException(
  //           "Only Proxy Approval can approve/reject"
  //         );
  //       }
  //     } else {
  //       throw new BadRequestException("Only Proxy Approval can approve/reject");
  //     }
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // }

  @Get("v1/kpi-workflow-status")
  public async getKpiWorkflowStatus(): Promise<KpiWorkflowStatusDto[]> {
    try {
      return this.kpiWorkflowStatus.getStatusMapping();
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
  @Get("v1/audit")
  public async getByEngagementId(
    @Query() params: WorkFlowAuditDTO,
    @User() user: IUser
  ): Promise<WorkflowModel[]> {
    try {
      console.log("query", params);
      const workflow = await this.service.getAuditData(params, user);

      return workflow;
    } catch (error) {
      console.log(error);
    }
  }

  @Get("get-all-workflows")
  public async findAllWorkFlowData(
    @Body() params: CreateWorkflow
  ): Promise<WorkflowModel[]> {
    try {
      return await this.service.findAll();
    } catch (error) {
      throw error;
    }
  }

  @Get("v1/get-kpi-report-workflow")
  public async getKpiReportWorkflow(
    @Query() query: GetKpiReportWorkflowDTO
  ): Promise<any> {
    try {
      return await this.service.getKpiReportWorkflow(query);
    } catch (error) {
      throw error;
    }
  }

  @Post("v1/update-workflow-history")
  public async updateWorkflowAndHistory(
    @Body() params: UpdateWorkflowHistoryDto,
    @User() user: IUser
  ) {
    try {
      return await this.service.updateWorkflowAndHistory(params, user);
    } catch (error) {
      throw error;
    }
  }

  @Get("v1/getCommentsByItemId")
  public async getCommentsByItemId(
    @Query() query: GetCommentsByItemIdRequest
  ): Promise<any> {
    try {
      return await this.service.getCommentsByItemId(query);
    } catch (error) {
      throw error;
    }
  }

  @Get("v1/getMySkillTasksAssigned")
  public async getMySkillTasksAssigned(@User() user: IUser): Promise<any> {
    try {
      return await this.service.getMySkillTasksAssigned(user);
    } catch (error) {
      throw error;
    }
  }

  @Get("v1/getMultipleTasksByByQuery")
  public async getMultipleTasksByByQuery(
    @Query() params: getMultipleTasksByByQueryDto
  ): Promise<WorkflowModel[]> {
    try {
      const workflow = await this.service.getMultipleTasksByByQuery(params);
      return workflow;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
