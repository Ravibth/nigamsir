import {
  BadRequestException,
  Injectable,
  Logger,
  NotFoundException,
} from "@nestjs/common";
import { validate } from "class-validator";
import { IKpiUpdate } from "../../microserviceModules/kpi/interfaces";
import { IUser } from "../../common/decorators/user.decorator";
import { EngagementService } from "../../microserviceModules/engagement/engagement.service";
import {
  IEngagementUpdate,
  IKpiExecutionUpdate,
} from "../../microserviceModules/engagement/interfaces";
import { IdentityService } from "../../microserviceModules/identity/identity.service";
import { KpiService } from "../../microserviceModules/kpi/kpi.service";
import { EDepFileStatus } from "../../microserviceModules/serviceConnector/enum";
import { FileMasterService } from "../../microserviceModules/serviceConnector/fileMaster.service";
import { WorkflowTaskModel } from "../workflow-task/models/workflow-task.model";
import { workflowHistoryService } from "../workflowHistory/workflowHistory.service";
import { AddKpiExecutionWorkflow } from "./dto/addKpiExecutionWorkflow.dto";
import { CreateWorkflow } from "./dto/createWorkflow.dto";
import { GetKpiReportWorkflowDTO } from "./dto/getKpiReportWorkflow.dto";
import { UpdateFileWorkflow } from "./dto/updateFileWorkflow.dto";
import { UpdateFileWorkflowStatusDto } from "./dto/updateFileWorkflowStatus.dto";
import { UpdateKpiWorkflowDto } from "./dto/updateKpiWorkflow.dto";
import {
  NotificationAction,
  UpdateEngagementByCEODto,
  UpdateWorkflow,
  UpdateWorkflowHistoryDto,
} from "./dto/updateWorkflow.dto";
import { WorkFlowAuditDTO } from "./dto/workflowAudit.dto";
import {
  AllocationMetaTypes,
  ConfigurationGroupName,
  EEngagementStatus,
  EUpdateInMSType,
  EWorkflowSteps,
  EWorkflowTaskAssignedType,
  GTSystemCredentials,
  NotificationTemplateTypes,
  RefreshWorkflowType,
  SeparatorEnum,
  WorkflowCommentsEnum,
  WorkflowOutCome,
  WorkflowStatus,
  WorkFlowTaskTitle,
  WorklFlowModule,
  WorklFlowSubModule,
} from "./enum";
import { WorkflowModel } from "./models/workflow.model";
import { WorkflowRepository } from "./repository/workflow.repository";
import { ConfigurationService } from "src/microserviceModules/configuration/configuration.service";
import { parse } from "path";
import { AllocationService } from "src/microserviceModules/allocation/allocation.service";
import { ProjectService } from "src/microserviceModules/project/project.service";
import { WorkflowModule } from "./workflow.module";
import { updateAllocationStatusDTO } from "./dto/updateAllocationStatus.dto";
import { Json } from "sequelize/types/utils";
import { BulkApprovalResponse } from "./dto/BulkApprovalResponse.dto";
import { CreateWorkflowResponse } from "./dto/createWorkflowResponse.dto";
import { UpdateAllocationResponse } from "./dto/updateAllocationResponse.dto";
import { RollOverResouceAllocationDetailsResponse } from "./dto/rollOverResponse.dto";
import { ResouceAllocationDetailsResponse } from "./dto/resourceAllocationDetailsResponse.dto";
import { ProjectRolesResponse } from "./dto/projectRolesResponse.dto";
import { terminateWorkflowByItemIdDTO } from "./dto/terminateWorkflowByItemId.dto";
import { GetCommentsByItemIdRequest } from "./dto/getCommentsByItemIdRequest.dto";
import { getUserSkillWorkflowComment } from "./helpers/util";
import { getMultipleTasksByByQueryDto } from "./dto/getMultipleTasksByByQuery.dto";
import { findWorkflowTaskDtoChanges } from "./dto/findWorkflowTaskChange.dto";
import { InsertAndUpdateItemDTO } from "./dto/InsertAndUpdateItem.dto";
import { Op, WhereOptions } from "sequelize";
import { RefreshWorkflowTaskAssignment } from "./dto/refreshWorkflowTaskAssignment.dto";
import { ServiceBusClient } from "@azure/service-bus";
import { Payload } from "@nestjs/microservices";
import { ClientSecretCredential } from "@azure/identity";
import { ProjectEventPayloadDto } from "./dto/eventPayload.dto";
import { UpdateAllocationRequestDTO } from "./dto/updateAllocationRequest.dto";
import { UpdateUserSkillStatusAfterWorkflowRequestDTO } from "./dto/UpdateUserSkillStatusAfterWorkflowReques.dto";
import { terminateWorkflowByPipelineCodeAndJobCodeDTO } from "./dto/terminateWorkflowByPipelineCodeAndJobCode";
import { getWorkflowByOutcomeWorklFlowModuleAndUpdateDate } from "./dto/getWorkflowByOutcomeWorklFlowModuleAndUpdateDate.dto";
import { UpdateSupercoachAndDelegateDto } from "./dto/updateSupercoachAndDelegate.dto";
import { findWorkflowTaskDetailsDto } from "./dto/findWorkflowTaskDetails.dto";

const LOG_CONTEXT = "workflow-service";

@Injectable()
export class WorkflowService {
  constructor(
    private repository: WorkflowRepository,
    private readonly historyService: workflowHistoryService,
    private readonly engagementService: EngagementService,
    private readonly kpiService: KpiService,
    private readonly logger: Logger,
    private identityService: IdentityService,
    private readonly fileMasterService: FileMasterService,
    private readonly configurationService: ConfigurationService,
    private readonly allocationService: AllocationService,
    private readonly projectService: ProjectService
  ) {}

  public async findAll(): Promise<WorkflowModel[]> {
    try {
      return await this.repository.findAll();
    } catch (error) {
      throw error;
    }
  }

  public async terminateWorkflowByPipelineCodeAndJobCode(
    params: terminateWorkflowByPipelineCodeAndJobCodeDTO
  ) {
    try {
      return await this.repository.terminateWorkflowByPipelineCodeAndJobCode(
        params
      );
    } catch (err) {
      this.logger.log(err);
      throw err;
    }
  }

  public async getWorkflowTasksDetails(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      return await this.repository.getWorkflowTasksDetails(params);
    } catch (error) {
      throw error;
    }
  }
  public async getEmployeeWithdrwalTask(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      return await this.repository.getEmployeeWorkflowWithdrwalTask(params);
    } catch (error) {
      throw error;
    }
  }

  public async getEmployeeTaskCountByQuery(params: any): Promise<number> {
    try {
      return await this.repository.getEmployeeTaskCountByQuery(params);
    } catch (error) {
      throw error;
    }
  }

  public async terminateWorkflowByItemId(
    updateItem: terminateWorkflowByItemIdDTO
  ) {
    return new Promise(async (resolve, reject) => {
      try {
        const inProgressWorkflow = await this.repository.findAllWorkflow(
          {
            item_id: updateItem.ItemId, // engag
            outcome: WorkflowOutCome.INPROGRESS,
          },
          ["id", "name", "module", "sub_module"]
        );
        console.log(inProgressWorkflow);
        if (!inProgressWorkflow) {
          console.log("No Workflow Found in Progress state");
          throw new NotFoundException("No Workflow Found in Progress state");
        }
        await this.repository.terminateInProgressWorkflowByItemId(updateItem);
        resolve({
          isError: false,
          error: null,
          terminatedWorkflowItemId: updateItem.ItemId,
        });
      } catch (error) {
        // throw new Error(error);
        resolve({
          isError: true,
          error: error.toString(),
          terminatedWorkflowItemId: null,
        });
      }
    });
  }
  public async terminateWorkflowByListOfItemId(
    updateItems: terminateWorkflowByItemIdDTO[],
    user: IUser
  ) {
    try {
      const totalResults: any[] = [];
      await Promise.all(
        updateItems.map((items) => this.terminateWorkflowByItemId(items))
      )
        .then((resp) => {
          totalResults.push(resp);
        })
        .catch((err) => {
          totalResults.push(err);
        });
      return totalResults;
    } catch (error) {
      console.log("Some thing went Wrong in termination of list of items");
      throw new Error(error);
    }
  }
  public async getWorkflowByQuery(params: any): Promise<WorkflowModel[]> {
    try {
      return this.repository.getWorkflowByQuery(params);
    } catch (error) {
      //TODO: throw error
      throw error;
    }
  }
  public async getWorkflowByUpdateQuery(
    params: getWorkflowByOutcomeWorklFlowModuleAndUpdateDate
  ): Promise<WorkflowModel[]> {
    try {
      return this.repository.getWorkflowByUpdateQuery(params);
    } catch (error) {
      //TODO: throw error
      throw error;
    }
  }
  public async getWorkflowTasksByQuery(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      if (params.assigned_to != null) {
        return await this.repository.getWorkflowTasksByQuery(params);
      } else {
        return await this.repository.getWorkflowTasksByStatusAndOutCome(params);
      }
    } catch (error) {
      // TODO: check error
      throw new Error(error);
    }
  }
  public async getWorkflowTasksByWorkflowStatusAndTaskStatus(
    params: findWorkflowTaskDtoChanges
  ): Promise<WorkflowTaskModel[]> {
    try {
      return await this.repository.getWorkflowTasksByWorkflowStatusAndTaskStatus(
        params
      );
    } catch (error) {
      // TODO: check error
      throw new Error(error);
    }
  }
  async GetUsersByEmails(emails: string[], token: string) {
    try {
      const result = await this.identityService.GetUsersByEmails(
        { email_id: emails },
        token
      );
      if (result.length === 0) {
        throw new Error(
          "No user Found in the identity for the provided emails"
        );
      }
      return result;
    } catch (error) {
      throw new Error("Someting went wrong in fetching user information");
    }
  }
  async updateSupercoachAndDelegate(
    params: UpdateSupercoachAndDelegateDto,
    user: IUser
  ) {
    try {
      const result = this.repository.updateSupercoachAndDelegate(params);
      // await this.projectService.UpdateProjectRolesForSupercoachDelegate(
      //   params,
      //   user
      // );
      return result;
    } catch (error) {
      throw error;
    }
  }
  async GetConfiguration(
    offeringsName: string,
    buName: string,
    groupName: string,
    configKey: string,
    configType: string,
    user: IUser
  ) {
    try {
      const buOfferingKey =
        buName + SeparatorEnum.SEPARATOR_PIPE + offeringsName;

      const result0: any =
        await this.configurationService.GetConfigurationByConfigGroupConfigKeyAndConfigType(
          groupName,
          configKey,
          configType,
          user
        );
      if (result0.length > 0) {
        const configs = result0[0].projectConfigurations.filter(
          (configInfo) =>
            configInfo?.attributeName?.toLowerCase().trim() ===
            buOfferingKey?.toLowerCase()?.trim()
        );
        if (configs.length > 0) {
          this.logger.log(JSON.stringify(configs[0]));
          return configs[0];
        } else {
          return {
            attributeValue: result0[0].allValue,
          };
        }
      } else {
        throw new Error("No Configuration found for the above params");
      }
    } catch (error) {
      // TODO: Throw exception
      throw new Error(
        "Something Went Wrong In Fetching Configurations Data " + error
      );
    }
  }
  async GetResourceAllocationDestails(item_id: string, user: IUser) {
    try {
      const result: any =
        await this.allocationService.GetResourceAllocationDetailsById(
          item_id,
          user
        );
      return result;
    } catch (error) {
      throw new Error(
        "Something Went Wrong In Fetching Allocations Data " + error
      );
    }
  }
  async GetResourceReviewerEmailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    user: IUser
  ) {
    try {
      const result: any =
        await this.projectService.GetResourceReviewerEmailsByPipelineCode(
          pipelineCode,
          jobCode,
          user
        );
      return result;
    } catch (error) {
      throw new Error(
        "Something Went Wrong In Fetching Resouce Reviewers Data " + error
      );
    }
  }
  async GetResourceRequestorsEmailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    workflowStartedBy: string,
    user: IUser
  ) {
    try {
      // const result: any =
      //   await this.projectService.GetResourceRequestorEmailsByPipelineCode(
      //     pipelineCode,
      //     jobCode,
      //     user
      //   );
      const result: any =
        await this.projectService.GetRequestorEmailsForAllocationWorkflow(
          pipelineCode,
          jobCode,
          workflowStartedBy,
          user
        );
      return result;
    } catch (error) {
      throw new Error(
        "Something Went Wrong in fetching Resource Requstors data " + error
      );
    }
  }
  async GetProjectDetailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    user: IUser
  ) {
    try {
      const result: any =
        await this.projectService.GetProjectDetailsByPipelineCode(
          pipelineCode,
          jobCode,
          user
        );
      return result;
    } catch (error) {
      throw new Error("Couldn't get project details " + error);
    }
  }
  async GetConfigurationForCreateWorkflow(params: CreateWorkflow, user: IUser) {
    const module = params.module.trim().toLowerCase();
    const sub_module = params.sub_module.trim().toLowerCase();
    const status = params.status.trim().toLowerCase();
    const sCase = `${module}_${sub_module}_${status}`;
    let employeeInformation;
    let expertiesName;
    let buName;
    let groupName;
    let pipelineCode;
    let jobCode;
    let pipelineCodeForAllocationResult;
    let projectDetails;
    let resourceAllocationDetails;
    let reviewers: ProjectRolesResponse[];
    let reviewerConfigData;
    let supercoachConfigData;
    let skillDueDateConfigData;
    try {
      switch (sCase) {
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION}_${EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER}`.toLowerCase():
          groupName = ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER;
          pipelineCodeForAllocationResult = this.GetPipelineCodeForAllocation(
            params.entity_type,
            params.entity_meta_data
          );
          pipelineCode = pipelineCodeForAllocationResult.pipelineCode;
          jobCode = pipelineCodeForAllocationResult.jobCode;
          const reviewersResponse =
            await this.GetResourceReviewerEmailsByPipelineCode(
              pipelineCode,
              jobCode,
              user
            );
          reviewers = JSON.parse(reviewersResponse);

          projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          console.log(projectDetails);
          // TODO: Uncomment & check if configuration don't exist
          expertiesName = projectDetails.offerings;
          buName = projectDetails.bu;
          console.log(reviewers);
          console.log(resourceAllocationDetails);
          params.assigned_to = reviewers
            .map((d) => d.user)
            .join()
            .toLowerCase();
          //assignment of names
          reviewerConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "OFFERINGS", //TODO: chnage
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }

          // params.configurations.reviewerconfiguration = reviewerConfigData[0];
          params.configurations.reviewerconfiguration = reviewerConfigData;

          break;
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION}_${EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH}`.toLowerCase():
          groupName =
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION;
          pipelineCodeForAllocationResult = this.GetPipelineCodeForAllocation(
            params.entity_type,
            params.entity_meta_data
          );
          pipelineCode = pipelineCodeForAllocationResult.pipelineCode;
          jobCode = pipelineCodeForAllocationResult.jobCode;
          //Todo:- Change Things -> DONE
          const userInformation = await this.GetUsersByEmails(
            [pipelineCodeForAllocationResult.employeeEmail.toLowerCase()],
            user.token
          );
          //TODO:- 1 -> Checked
          const employeeInformation = userInformation?.map((e) => {
            if (e?.delegate_details?.allocation_delegate_email) {
              return `${e?.supercoach_name},${e.delegate_details.allocation_delegate_email}`;
            } else {
              return e?.supercoach_name;
            }
          });
          projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          expertiesName = projectDetails.offerings;
          buName = projectDetails.bu;
          params.assigned_to = employeeInformation
            .map((d) => d)
            .join()
            .toLowerCase();
          //TODO :- Checked
          params.assigned_to_json = userInformation?.map((d) => {
            return {
              supercoach_email: d.supercoach_name,
              allocation_delegate_email: d?.delegate_details
                ?.allocation_delegate_email
                ? d?.delegate_details?.allocation_delegate_email
                : null,
            };
          });
          //assignment of names
          supercoachConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "OFFERINGS", //TODO: chnage
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }
          params.configurations.supercoachconfiguration = supercoachConfigData;

          break;
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION}_${EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE}`.toLowerCase():
          //project pipeline code
          //TODO:- replace by params.item_id
          // resourceAllocationDetails = await this.GetResourceAllocationDestails(
          //   // params.item_id,
          //   "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
          //   user
          // );
          // pipelineCode = resourceAllocationDetails.pipelineCode;
          pipelineCodeForAllocationResult = this.GetPipelineCodeForAllocation(
            params.entity_type,
            params.entity_meta_data
          );
          pipelineCode = pipelineCodeForAllocationResult.pipelineCode;
          jobCode = pipelineCodeForAllocationResult.jobCode;
          //assigned to
          params.assigned_to =
            pipelineCodeForAllocationResult.employeeEmail.toLowerCase();
          //expName
          projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          //TODO: - change group name to employee
          groupName = ConfigurationGroupName.ALLOCATION_PENDING_WITH_EMPLOYEE;
          // groupName = ConfigurationGroupName.ALLOCATION_PENDING_WITH_EMPLOYEE;
          expertiesName = projectDetails.offerings;
          buName = projectDetails.bu;
          // expertiesName = "Ndo";
          const employeeConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "OFFERINGS", //TODO: chnages
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }
          params.configurations.employeeconfiguration = employeeConfigData;
          console.log(employeeConfigData);
          break;
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE}_${EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER}`.toLowerCase():
          groupName = ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER;
          // expertiesName = "Ndo";
          //TODO:- replace by params.item_id
          // resourceAllocationDetails = await this.GetResourceAllocationDestails(
          //   // params.item_id,
          //   "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
          //   user
          // );
          // pipelineCode = resourceAllocationDetails.pipelineCode;
          pipelineCodeForAllocationResult = this.GetPipelineCodeForAllocation(
            params.entity_type,
            params.entity_meta_data
          );
          pipelineCode = pipelineCodeForAllocationResult.pipelineCode;
          jobCode = pipelineCodeForAllocationResult.jobCode;
          const reviewersRoles =
            await this.GetResourceReviewerEmailsByPipelineCode(
              pipelineCode,
              jobCode,
              user
            );

          reviewers = JSON.parse(reviewersRoles);
          projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          console.log(projectDetails);
          // TODO: Uncomment & check if configuration don't exist
          expertiesName = projectDetails.offerings;
          buName = projectDetails.bu;
          console.log(reviewers);
          console.log(resourceAllocationDetails);
          params.assigned_to = reviewers
            .map((d) => d.user)
            .join()
            .toLowerCase();
          //TODO: Assigned_to_name
          reviewerConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "OFFERINGS", //TODO: Chnage
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }

          params.configurations.reviewerconfiguration = reviewerConfigData;
          break;
        case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE}_${EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH}`.toLowerCase():
          groupName =
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION;
          // expertiesName = "Ndo";
          //TODO:- replace by params.item_id
          // resourceAllocationDetails = await this.GetResourceAllocationDestails(
          //   // params.item_id,
          //   "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
          //   user
          // );
          // pipelineCode = resourceAllocationDetails.pipelineCode;
          pipelineCodeForAllocationResult = this.GetPipelineCodeForAllocation(
            params.entity_type,
            params.entity_meta_data
          );
          pipelineCode = pipelineCodeForAllocationResult.pipelineCode;
          jobCode = pipelineCodeForAllocationResult.jobCode;
          //TODO:- DONE
          const allocationUserInformation = await this.GetUsersByEmails(
            [pipelineCodeForAllocationResult.employeeEmail.toLowerCase()],
            user.token
          );
          projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          console.log(projectDetails);
          // TODO: Uncomment & check if configuration don't exist
          expertiesName = projectDetails.offerings;
          buName = projectDetails.bu;
          console.log(reviewers);
          console.log(resourceAllocationDetails);
          //TODO:- 2 -> Checked
          params.assigned_to = allocationUserInformation
            ?.map((d) => {
              if (d?.delegate_details?.allocation_delegate_email) {
                return `${d.supercoach_name},${d.delegate_details.allocation_delegate_email}`
                  .toLowerCase()
                  .trim();
              } else {
                return d.supercoach_name.toLowerCase().trim();
              }
            })
            .join()
            .toLowerCase();
          params.assigned_to_json = allocationUserInformation?.map((d) => {
            return {
              supercoach_email: d.supercoach_name,
              allocation_delegate_email:
                d?.delegate_details?.allocation_delegate_email,
            };
          });
          //TODO: Assigned_to_name
          supercoachConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "OFFERINGS", //TODO: Chnage
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }

          params.configurations.supercoachconfiguration = supercoachConfigData;
          break;

        case `${WorklFlowModule.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT}_${WorklFlowSubModule.WORKFLOW_SUB_MODULE_USER_SKILL_ASSESSMENT}_${EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_PENDING}`.toLowerCase():
          groupName =
            ConfigurationGroupName.NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUEDATE;

          skillDueDateConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            groupName,
            groupName,
            "GLOBAL",
            user
          );
          if (!params.configurations) {
            params.configurations = {};
          }

          params.configurations.skilDueDateconfiguration =
            skillDueDateConfigData;

          break;

        default:
          break;
      }
    } catch (error) {
      throw new Error(error);
    }
  }

  public async addWorkflow(
    params: CreateWorkflow,
    user: IUser
  ): Promise<CreateWorkflowResponse> {
    try {
      this.logger.debug("Adding workflow", LOG_CONTEXT);
      await this.GetConfigurationForCreateWorkflow(params, user);

      const workflow = await this.repository.addWorkflow(params, user);

      if (workflow) {
        // await this.updateWorkflowStatus(workflow, user);
        this.logger.debug("Adding workflow history", LOG_CONTEXT);
        await this.historyService.addEngagementLog(workflow);
      }

      if (workflow.task_list.length) {
        this.logger.debug("Adding workflow task", LOG_CONTEXT);
        this.historyService.addEngagementTaskLog(workflow.task_list, workflow);
      }
      const notificationAction = this.GetNotificationAction(
        workflow.task_list,
        workflow,
        false
      );
      this.logger.debug("Succesfull-", LOG_CONTEXT);
      console.log("Succesfull-");
      const updateListResponse =
        this.UpdateRespectiveTablesForCreateOrUpdateWorkflow(
          "create_workflow_title",
          workflow.module,
          { ...workflow.dataValues, actions: notificationAction },
          null,
          user
        );
      const payload: ProjectEventPayloadDto = {
        action: updateListResponse.action,
        payload: JSON.stringify(updateListResponse.response),
        token: user.token,
      };
      await this.publishMassageToServiceBusTopic(payload, "project");
      return { ...workflow.dataValues, actions: notificationAction };
    } catch (error) {
      console.log("Error-", error);
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  validateBusinessSPOC(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel
  ): UpdateFileWorkflow & UpdateWorkflow {
    try {
      if (workflowTask.assigned_to !== "Business SPOC") {
        return params;
      }

      if (
        params.type == EWorkflowTaskAssignedType.ROLE &&
        !["Business SPOC", "SystemAdmin"].some((value) =>
          user.roles.includes(value)
        )
      ) {
        throw new BadRequestException("Only business spoc can approve/reject");
      }

      params.next_step = null;
      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async validateEngagementAcceptanceTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel
  ): Promise<UpdateFileWorkflow & UpdateWorkflow> {
    try {
      if (
        workflowTask.assigned_to === "COE Admin" &&
        !user.roles.includes("COE Admin") &&
        !user.roles.includes("SystemAdmin")
      ) {
        throw new BadRequestException("Only COE Admin can update");
      }

      if (
        user.service_line.toLowerCase() !== workflow.name.toLowerCase() &&
        !user.roles.includes("SystemAdmin")
      ) {
        throw new BadRequestException(
          "User with same service line as engagement will approved"
        );
      }

      const meta = new UpdateEngagementByCEODto();
      params.coe_acceptance_meta =
        params.coe_acceptance_meta || ({} as UpdateEngagementByCEODto);

      meta.resources = params.coe_acceptance_meta.resources;
      meta.coe_start_date = params.coe_acceptance_meta.coe_start_date;

      meta.status = params.coe_acceptance_meta.status;
      meta.version = params.coe_acceptance_meta.version;
      meta.remarks = params.coe_acceptance_meta.remarks;

      const errors = await validate(meta);

      console.log(errors);
      if (errors.length > 0) {
        throw new BadRequestException(
          "One of key value from coe acceptance meta is missing"
        );
      }
      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  validatePartnerAcceptance(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel,
    isproxyApproval: string = null
  ) {
    try {
      const userEmail = user.email;
      console.log(userEmail, " Approver email id");

      if (
        params["type"] == EWorkflowTaskAssignedType.USER &&
        userEmail !== workflowTask.assigned_to &&
        isproxyApproval == null
      ) {
        throw new BadRequestException("Only stakeholder can approve/reject");
      }

      if (
        isproxyApproval != null &&
        params["type"] == EWorkflowTaskAssignedType.USER
      ) {
        params["proxy_approval_by"] = isproxyApproval;
      }

      params.next_step = EWorkflowSteps.Business_SPOC;

      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  validateFileUploadEngagementTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel,
    isproxyApproval: string = null
  ): UpdateFileWorkflow & UpdateWorkflow {
    try {
      params.next_step =
        params.file_status == EDepFileStatus.FAILED
          ? null
          : EWorkflowSteps.FILE_CLEANSING;

      params.status =
        params.file_status == EDepFileStatus.FAILED
          ? WorkflowStatus.FILE_SANITY_FAILURE
          : WorkflowStatus.FILE_SANITY_SUCCESS;

      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  validateKpiApproval(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel
  ): UpdateFileWorkflow & UpdateWorkflow {
    try {
      if (user.service_line.toLowerCase() !== workflow.name.toLowerCase()) {
        throw new BadRequestException(
          "User can not update different service line workflow"
        );
      }

      if (
        !user.roles.includes("Business SPOC") &&
        !user.roles.includes("SystemAdmin")
      ) {
        throw new BadRequestException("Only Business SPOC can update");
      }

      params.next_step = null;

      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  GetPipelineCodeForAllocation = (metaEntityType: string, meta: any) => {
    switch (metaEntityType.toUpperCase().trim()) {
      case AllocationMetaTypes.UPDATE_ALLOCATION_RESPONSE.toUpperCase().trim():
        let updateAllocationResponse = new UpdateAllocationResponse();
        updateAllocationResponse = meta || ({} as UpdateAllocationResponse);
        return {
          pipelineCode: updateAllocationResponse.PipelineCode,
          jobCode: updateAllocationResponse.JobCode,
          employeeEmail: updateAllocationResponse.EmpEmail,
          employeeName: updateAllocationResponse.EmpName,
        };
        break;
      case AllocationMetaTypes.ROLL_OVER_ALLOCATION.toUpperCase().trim():
        let rolloverAllocationDetailsResponse =
          new RollOverResouceAllocationDetailsResponse();
        rolloverAllocationDetailsResponse =
          meta || ({} as RollOverResouceAllocationDetailsResponse);
        return {
          pipelineCode: rolloverAllocationDetailsResponse.PipelineCode,
          jobCode: rolloverAllocationDetailsResponse.JobCode,
          employeeEmail: rolloverAllocationDetailsResponse.EmpEmail,
          employeeName: rolloverAllocationDetailsResponse.EmpName,
        };
        break;
      case AllocationMetaTypes.RESOURCE_ALLOCATION_RESPONSE.toUpperCase().trim():
        ResouceAllocationDetailsResponse;
        let resourceAllocationDetailsResponse =
          new ResouceAllocationDetailsResponse();
        resourceAllocationDetailsResponse =
          meta || ({} as ResouceAllocationDetailsResponse);
        return {
          pipelineCode: resourceAllocationDetailsResponse.PipelineCode,
          jobCode: resourceAllocationDetailsResponse.JobCode,
          employeeEmail: resourceAllocationDetailsResponse.EmpEmail,
          employeeName: resourceAllocationDetailsResponse.EmpName,
        };
        break;
      default:
        break;
    }
  };

  async validateReviewerTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel
  ) {
    try {
      if (
        workflow.module.toLowerCase().trim() ===
          WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
        workflow.sub_module.toLowerCase().trim() ===
          WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim()
      ) {
        if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.REJECT.toLowerCase()
        ) {
          params.next_step = null;
        } else if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.APPROVED.toLowerCase()
        ) {
          //TODO :- change the configuration for the selection of next step
          //1. if sc config is ON -> SC
          //2. if sc config is OFF -> Employee

          const result = this.GetPipelineCodeForAllocation(
            workflow.entity_type,
            workflow.entity_meta_data
          );
          const pipelineCode = result.pipelineCode;
          const jobCode = result.jobCode;
          //assigned to
          const projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          // TODO:- change configuration
          const expertiesName = projectDetails.offerings;
          const buName = projectDetails.bu;
          const superCoachConfig = await this.GetConfiguration(
            expertiesName,
            buName,
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
            "OFFERINGS",
            user
          );
          if (Number(superCoachConfig.attributeValue) === -1) {
            //task for employee
            params.assigned_to = result.employeeEmail.toLowerCase();

            params.next_step =
              EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL;
            params.workflow_table_status =
              EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
          } else {
            //task for supercoach
            params.next_step =
              EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL;
            params.workflow_table_status =
              EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
            //TODO: - Change Things here DONE
            const userDetails = await this.GetUsersByEmails(
              [result.employeeEmail],
              user.token
            );
            this.logger.log(userDetails);

            const supercoach =
              userDetails.length === 1
                ? userDetails[0].supercoach_name?.toLowerCase()?.trim()
                : "";
            //TODO:- 3 -> Checked
            params.assigned_to = userDetails
              ?.map((d) => {
                if (d?.delegate_details?.allocation_delegate_email) {
                  return `${d.supercoach_name},${d.delegate_details.allocation_delegate_email}`;
                } else {
                  return d.supercoach_name;
                }
              })
              .join()
              .toLowerCase();
            params.assigned_to_json = userDetails?.map((d) => {
              return {
                supercoach_email: d.supercoach_name,
                allocation_delegate_email:
                  d?.delegate_details?.allocation_delegate_email,
              };
            });
          }
          const groupName =
            ConfigurationGroupName.ALLOCATION_PENDING_WITH_EMPLOYEE;
          const employeeConfigData = await this.GetConfiguration(
            expertiesName,
            buName,
            // ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER,
            groupName,
            groupName,
            "OFFERINGS", //TODO: changes
            user
          );
          if (!params.configuration) {
            params.configuration = {};
          }
          params.configuration.employeeconfiguration = employeeConfigData;
          params.configuration.supercoachconfiguration = superCoachConfig;
        }
      } else if (
        workflow.module.toLowerCase().trim() ===
          WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
        workflow.sub_module.toLowerCase().trim() ===
          WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE.toLowerCase().trim()
      ) {
        if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.REJECT.toLowerCase()
        ) {
          params.next_step = null;
        } else if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.APPROVED.toLowerCase()
        ) {
          //TODO :- change the configuration for the selection of next step
          //1. if sc config is ON -> SC
          //2. if sc config is OFF -> null
          const result = this.GetPipelineCodeForAllocation(
            workflow.entity_type,
            workflow.entity_meta_data
          );
          const pipelineCode = result.pipelineCode;
          const jobCode = result.jobCode;
          //assigned to
          const projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          // TODO:- change configuration
          const expertiesName = projectDetails.offerings;
          const buName = projectDetails.bu;
          const superCoachConfig = await this.GetConfiguration(
            expertiesName,
            buName,
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
            ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
            "OFFERINGS",
            user
          );
          if (Number(superCoachConfig.attributeValue) === -1) {
            params.next_step = null;
            params.workflow_table_status =
              EEngagementStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER;
            params.workflow_table_outcome = WorkflowOutCome.CLOSE;
          } else {
            params.next_step =
              EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL;
            params.workflow_table_status =
              EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
            params.workflow_table_outcome = WorkflowOutCome.INPROGRESS;
            //TODO:- Change -> DONE
            const userDetails = await this.GetUsersByEmails(
              [result.employeeEmail],
              user.token
            );
            this.logger.log(userDetails);
            const supercoach =
              userDetails.length === 1
                ? userDetails[0].supercoach_name?.toLowerCase()?.trim()
                : "";
            //TODO:- 4 -> Checked

            params.assigned_to = userDetails
              ?.map((d) => {
                if (d?.delegate_details?.allocation_delegate_email) {
                  return `${d.supercoach_name},${d.delegate_details.allocation_delegate_email}`;
                } else {
                  return d.supercoach_name;
                }
              })
              .join()
              .toLowerCase();
            params.assigned_to_json = userDetails?.map((d) => {
              return {
                supercoach_email: d.supercoach_name,
                allocation_delegate_email:
                  d?.delegate_details?.allocation_delegate_email,
              };
            });
          }
          if (!params.configuration) {
            params.configuration = {};
          }
          params.configuration.supercoachconfiguration = superCoachConfig;
        }
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async validateSupercoachTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel
  ) {
    try {
      if (
        workflow.module.toLowerCase().trim() ===
          WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
        workflow.sub_module.toLowerCase().trim() ===
          WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim()
      ) {
        if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.REJECT.toLowerCase()
        ) {
          params.next_step = null;
        } else if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.APPROVED.toLowerCase()
        ) {
          //change
          params.next_step =
            EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL;
          // TODO:- change item_id
          const result = this.GetPipelineCodeForAllocation(
            workflow.entity_type,
            workflow.entity_meta_data
          );
          const pipelineCode = result.pipelineCode;
          const jobCode = result.jobCode;
          //assigned to
          params.assigned_to = result.employeeEmail.toLowerCase();
          const projectDetails = await this.GetProjectDetailsByPipelineCode(
            pipelineCode,
            jobCode,
            user
          );
          // TODO:- change configuration
          const expertiesName = projectDetails.offerings;
          const buName = projectDetails.bu;
          const groupName =
            ConfigurationGroupName.ALLOCATION_PENDING_WITH_EMPLOYEE;
          //employeeConfigData
          const configData = await this.GetConfiguration(
            expertiesName,
            buName,
            // ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER,
            groupName,
            groupName,
            "OFFERINGS", //TODO: changes
            user
          );
          if (!params.configuration) {
            params.configuration = {};
          }
          params.configuration.employeeconfiguration = configData;
          console.log(configData);
        }
      } else if (
        workflow.module.toLowerCase().trim() ===
          WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
        workflow.sub_module.toLowerCase().trim() ===
          WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE.toLowerCase().trim()
      ) {
        if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.REJECT.toLowerCase()
        ) {
          params.next_step = null;
        } else if (
          params.status.toLowerCase().trim() ===
          WorkflowStatus.APPROVED.toLowerCase()
        ) {
          params.next_step = null;
        }
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async validateEmployeeTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel
  ) {
    try {
      // validation
      if (
        params.status.toLowerCase().trim() ===
        WorkflowStatus.APPROVED.toLowerCase()
      ) {
        params.next_step = null;
      } else if (
        params.status.toLowerCase().trim() ===
        WorkflowStatus.REJECT.toLowerCase()
      ) {
        //NOTE:- Rejection can be taken into account only once
        // params.next_step =
        //   EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL_OVER_EMPLOYEE_REJECTION;
        // TODO:- change item_id
        // const resourceAllocationDetails =
        //   await this.GetResourceAllocationDestails(
        //     // params.item_id,
        //     "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
        //     user
        //   );
        const result = this.GetPipelineCodeForAllocation(
          workflow.entity_type,
          workflow.entity_meta_data
        );
        const pipelineCode = result.pipelineCode;
        const jobCode = result.jobCode;
        //assigned to
        // params.assigned_to = resourceAllocationDetails?.empEmail;
        const projectDetails = await this.GetProjectDetailsByPipelineCode(
          pipelineCode,
          jobCode,
          user
        );
        //TODO:- Change to configurations
        const expertiesName = projectDetails.offerings;
        const buName = projectDetails.bu;
        // const groupName =
        //   ConfigurationGroupName.ALLOCATION_WITHDRAWL_PENDING_WITH_EMPLOYEE;
        //allocationWithdrawlConfigData, RR_configData
        // const employeeWithdrawlConfigData = await this.GetConfiguration(
        //   "Ndo",
        //   ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER,
        //   user
        // );
        if (!params.configuration) {
          params.configuration = {};
        }
        // params.configuration.employeeWithdrawlConfiguration =
        //   employeeWithdrawlConfigData[0];
        // console.log(employeeWithdrawlConfigData);

        //TITLE : - RESOURCE REQUESTOR CONFIGURATION START

        const resourceRequestorTaskConfigData = await this.GetConfiguration(
          expertiesName,
          buName,
          // ConfigurationGroupName.RESOURCE_ALLOCALTION_REVIEWER,
          ConfigurationGroupName.ALLOCATION_RESOURCE_REQUESTOR_TO_ACCEPT_ALLOCATION,
          ConfigurationGroupName.ALLOCATION_RESOURCE_REQUESTOR_TO_ACCEPT_ALLOCATION,
          "OFFERINGS", //TODO: change
          user
        );
        if (!params.configuration) {
          params.configuration = {};
        }

        params.configuration.resourceRequestorConfiguration =
          resourceRequestorTaskConfigData;
        console.log(resourceRequestorTaskConfigData);
        if (!params.assigned_to_meta) {
          params.assigned_to_meta = {};
        }
        const resourceRequestorEmails =
          await this.GetResourceRequestorsEmailsByPipelineCode(
            pipelineCode,
            jobCode,
            workflow.created_by,
            user
          );
        const resourceRequestors: ProjectRolesResponse[] = JSON.parse(
          resourceRequestorEmails
        );
        const updatedAssignedTo = {
          employeeEmails: params.assigned_to,
          requestorEmails: resourceRequestorEmails,
        };
        //TODO :- RR Name

        const requestorEmails = resourceRequestors.map((d) => d.user);
        const additionalDelegateEmail = [];
        resourceRequestors.forEach((d) => {
          if (
            d.delegateEmail !== undefined &&
            d.delegateEmail &&
            d.delegateEmail.length > 0
          ) {
            additionalDelegateEmail.push(d.delegateEmail);
          }
        });
        let finalRequestors = [...requestorEmails];
        if (additionalDelegateEmail && additionalDelegateEmail.length > 0) {
          finalRequestors = [...finalRequestors, ...additionalDelegateEmail];
        }
        const finalRequestorsStr = finalRequestors.join(",").toLowerCase();
        params.assigned_to_meta.resourceRequestorEmails = finalRequestorsStr;
        console.log(params.assigned_to_meta);
        // params.assigned_to_meta.resourceRequestorEmails =
        // resourceRequestorEmails;
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  validateEmployeeWithdrawlTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel
  ) {
    //validation
    //Next Step is alawys null -> workflow ends alawys
  }
  async validateResourceRequestorTerminationTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel
  ) {
    params.next_step = null;
  }
  async validateResourceRequestorTask(
    params: UpdateFileWorkflow & UpdateWorkflow,
    user: IUser,
    workflow: WorkflowModel
  ) {
    //validation
    if (
      params.status.trim().toLowerCase() ===
      WorkflowStatus.APPROVED.toLowerCase()
    ) {
      params.next_step = null;
    } else if (
      params.status.trim().toLowerCase() === WorkflowStatus.REJECT.toLowerCase()
    ) {
      params.next_step =
        EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION;
      // TODO:- change item_id
      // const resourceAllocationDetails =
      //   await this.GetResourceAllocationDestails(
      //     // params.item_id,
      //     "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
      //     user
      //   );
      const result = this.GetPipelineCodeForAllocation(
        workflow.entity_type,
        workflow.entity_meta_data
      );
      const pipelineCode = result.pipelineCode;
      const jobCode = result.jobCode;
      //assigned to
      //TODO:- Change Things ->DONE
      const userInformation = await this.GetUsersByEmails(
        [result.employeeEmail.toLowerCase().trim()],
        user.token
      );
      //TODO:- 5 -> Checked

      params.assigned_to = userInformation
        ?.map((d) => {
          if (d?.delegate_details?.allocation_delegate_email) {
            return `${d.supercoach_name},${d.delegate_details.allocation_delegate_email}`;
          } else {
            return d.supercoach_name;
          }
        })
        .join()
        .toLowerCase();
      params.assigned_to_json = userInformation?.map((d) => {
        return {
          supercoach_email: d.supercoach_name,
          allocation_delegate_email:
            d?.delegate_details?.allocation_delegate_email,
        };
      });
      //TODO: Assign_to_name
      const projectDetails = await this.GetProjectDetailsByPipelineCode(
        pipelineCode,
        jobCode,
        user
      );
      const expertiesName = projectDetails.offerings;
      const buName = projectDetails.bu;
      // const groupName =
      //   ConfigurationGroupName.ALLOCATION_CSP_TO_ACT_ON_RESOURCE_REQUESTOR_REJECTION;
      //allocationWithdrawlConfigData, RR_configData
      const scConfigData = await this.GetConfiguration(
        expertiesName,
        buName,
        ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
        ConfigurationGroupName.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
        "OFFERINGS", //TODO: change it
        user
      );
      if (!params.configuration) {
        params.configuration = {};
      }
      params.configuration.supercoachconfiguration = scConfigData;
      //TODO:- assign configuration
    } else {
      this.logger.log("Unactionable State");
      throw new Error(
        `No Next Step can be defined for ${params.status} status`
      );
    }
    //configuration values for CSP
  }

  async updateFileWorkflowStatus(
    params: UpdateFileWorkflowStatusDto,
    user: IUser
  ): Promise<any> {
    try {
      const pendingWorkflows = await this.repository.findAllWorkflowArray(
        {
          parent_id: params.parent_id, // engag
          outcome: WorkflowOutCome.INPROGRESS,
        },
        ["id", "name", "module", "sub_module"]
      );

      if (!pendingWorkflows) {
        throw new NotFoundException("No Engagement Found in Progress state");
      }

      let fileDetails = await this.fileMasterService.findDetails(
        { id: pendingWorkflows.map((w) => w.item_id).join(",") },
        user.token
      );
      fileDetails = fileDetails.map((file) => {
        const fileNameFormat = file.internal_name.split("/")[1];
        file.internal_name = fileNameFormat;
        return file;
      });

      const fileStatusMap: Map<string, Record<string, any>> = new Map<
        string,
        Record<string, any>
      >();

      for (const fileStatus of params.file_status) {
        fileStatusMap.set(fileStatus.file_name, fileStatus);
      }

      for (const tempWorkflow of pendingWorkflows) {
        fileStatusMap.set(tempWorkflow.item_id, tempWorkflow);
      }

      const filesStatusToUpdate = fileDetails.filter(
        (wFile) =>
          fileStatusMap.has(wFile.internal_name) &&
          [EDepFileStatus.COMPLETED, EDepFileStatus.FAILED].includes(
            fileStatusMap.get(wFile.internal_name).status
          )
      );

      if (filesStatusToUpdate.length) {
        for (const file of filesStatusToUpdate) {
          const workflow = fileStatusMap.get(file.id) as WorkflowModel;
          const fStatus = fileStatusMap.get(file.internal_name);

          await this.updateWorkflowTask(
            workflow,
            {
              comment: "updating file status",
              file_status: fStatus.status,
            },
            user
          );
        }
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async getAllocationSupercoachWorkflowTask(
    params: findWorkflowTaskDetailsDto
  ) {
    try {
      return await this.repository.getPendingWorkflowTasksForAllocationSuperCoach(
        params.supercoachEmail,
        params.employeeEmail,
        params.workflow_task_status,
        params.outcome,
        params.module
      );
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  private async GetUserAndUserDelegateCollectiveInformation(
    employeeEmail: string,
    token: string
  ) {
    const userInformation = await this.GetUsersByEmails(
      [employeeEmail?.toLowerCase()?.trim() || ""],
      token
    );
    const skillEmployeeInformation = userInformation?.map((e) => {
      if (e?.delegate_details?.skill_delegate_email) {
        return `${e?.supercoach_name},${e.delegate_details.skill_delegate_email}`;
      } else {
        return e?.supercoach_name;
      }
    });
    const new_skill_supercoach_assignment = skillEmployeeInformation
      .map((d) => d)
      .join()
      .toLowerCase();
    const new_skill_supercoach_assignment_json = userInformation?.map((d) => {
      return {
        supercoach_email: d.supercoach_name,
        skill_delegate_email: d?.delegate_details?.skill_delegate_email
          ? d?.delegate_details?.skill_delegate_email
          : null,
      };
    });
    const allocationEmployeeInformation = userInformation?.map((e) => {
      if (e?.delegate_details?.allocation_delegate_email) {
        return `${e?.supercoach_name},${e.delegate_details.allocation_delegate_email}`;
      } else {
        return e?.supercoach_name;
      }
    });
    const new_allocation_supercoach_assignment = allocationEmployeeInformation
      .map((d) => d)
      .join()
      .toLowerCase();
    const new_allocation_supercoach_assignment_json = userInformation?.map(
      (d) => {
        return {
          supercoach_email: d.supercoach_name,
          allocation_delegate_email: d?.delegate_details
            ?.allocation_delegate_email
            ? d?.delegate_details?.allocation_delegate_email
            : null,
        };
      }
    );
    this.logger.log(JSON.stringify(userInformation));
    this.logger.log(JSON.stringify(new_skill_supercoach_assignment));
    this.logger.log(JSON.stringify(new_skill_supercoach_assignment_json));
    this.logger.log(JSON.stringify(new_allocation_supercoach_assignment));
    this.logger.log(JSON.stringify(new_allocation_supercoach_assignment_json));
    return {
      userInformation,
      new_skill_supercoach_assignment,
      new_skill_supercoach_assignment_json,
      new_allocation_supercoach_assignment,
      new_allocation_supercoach_assignment_json,
    };
  }
  async refreshAssignment(
    params: RefreshWorkflowTaskAssignment,
    user: IUser
  ): Promise<any> {
    try {
      this.logger.debug(params);
      this.logger.log(JSON.stringify(params));

      switch (params?.type?.toLowerCase()?.trim()) {
        case RefreshWorkflowType.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR.toLowerCase().trim():
          await this.repository.refreshPendingWorkflowTasksForAllocationWorkflow(
            params
          );
          break;
        case RefreshWorkflowType.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toLowerCase().trim():
          const collective_information_for_skill =
            await this.GetUserAndUserDelegateCollectiveInformation(
              params.employeeEmail,
              user.token
            );
          await this.repository.refreshPendingWorkflowTasksForSkillWorkflow(
            params,
            collective_information_for_skill.new_skill_supercoach_assignment,
            collective_information_for_skill.new_skill_supercoach_assignment_json
          );
          break;
        case RefreshWorkflowType.WORKFLOW_MODULE_USER_SUPERCOACH_ASSESSMENT.toLowerCase().trim():
          const collective_information_for_allocation =
            await this.GetUserAndUserDelegateCollectiveInformation(
              params.employeeEmail,
              user.token
            );
          await this.repository.refreshPendingWorkflowTasksForAllocationSuperCoachWorkflow(
            params,
            collective_information_for_allocation.new_allocation_supercoach_assignment,
            collective_information_for_allocation.new_allocation_supercoach_assignment_json
          );
          break;

        default:
          break;
      }
      return null;
    } catch (error) {
      throw Error(error);
    }
  }
  GetNotificationAction(
    workflowTasks: WorkflowTaskModel[],
    workflow: WorkflowModel,
    isWorkflowUpdate = true
  ) {
    const notificationActions: string[] = [];

    switch (workflow.status.toLocaleLowerCase().trim()) {
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER.toLocaleLowerCase().trim():
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT
        ); //TODO: WIP
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE.toLowerCase().trim():
        const workflowTaskForReviewer: WorkflowTaskModel[] =
          workflowTasks.filter(
            (wt) =>
              wt.title.toLowerCase().trim() ===
                WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL.toLowerCase().trim() &&
              wt.status.toLocaleLowerCase().trim() ===
                WorkflowStatus.APPROVED.toLocaleLowerCase().trim()
          );
        if (workflowTaskForReviewer && workflowTaskForReviewer.length === 1) {
          if (workflowTaskForReviewer[0].updated_by === "System@gt.com") {
            notificationActions.push(
              NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER
            );
            notificationActions.push(
              NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR
            );
          } else {
            notificationActions.push(
              NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER
            );
            notificationActions.push(
              NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR
            );
          }
        }
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT
        );
        // 42 , 43  Notification for allocation of a resources to a project (Notification directly to the employee , config for Review is OFF )
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT
        );
        if (!isWorkflowUpdate) {
          //R17
          notificationActions.push(
            NotificationTemplateTypes.PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF
          );
        } else {
          // R4
          notificationActions.push(
            NotificationTemplateTypes.PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS
          );
        }
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH.toLowerCase().trim():
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER.toLocaleLowerCase().trim():
        // 141 , 142 Resource Allocation Review Notification - Rejected - Notification to Requestor
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR
        );
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE.toLocaleLowerCase().trim():
        const employeeTask = workflowTasks.filter(
          (wt) =>
            wt.title.toLowerCase().trim() ===
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.toLowerCase().trim()
        );
        const requestorTaskForReview = workflowTasks.filter(
          (wt) =>
            wt.title.toLowerCase().trim() ===
              WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.toLowerCase().trim() &&
            wt.status.toLowerCase().trim() ===
              WorkflowStatus.TERMINATED.toLowerCase().trim()
        );
        const requestorTaskForTermination = workflowTasks.filter(
          (wt) =>
            wt.title.toLowerCase().trim() ===
              WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.toLowerCase().trim() &&
            wt.status.toLowerCase().trim() ===
              WorkflowStatus.TERMINATED.toLowerCase().trim()
        );
        if (
          requestorTaskForReview.length === 0 &&
          requestorTaskForTermination.length === 0 &&
          employeeTask &&
          employeeTask.length === 1
        ) {
          //CASE FOR EMPLOYEE TO ACCEPT
          if (
            employeeTask[0].status.toLowerCase().trim() ===
            WorkflowStatus.APPROVED.toLowerCase().trim()
          ) {
            if (
              workflow.updated_by.split("__")[1].toLowerCase().trim() ===
              process.env.SYSTEM_EMAIL_ID
            ) {
              // AUTO APPROVED BY THE EMPLOYEE
              // 73,74 - UC025
              notificationActions.push(
                NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEWER_RESOURCE_AUTO_APPROVE_FOR_EMPLOYEE
              );
              // 75,76 - UC025
              notificationActions.push(
                NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_AUTO_APPROVED_FOR_EMPLOYEE
              );
            } else {
              //APPROVED BY THE EMPLOYEE
              // 67,68 - UC025
              notificationActions.push(
                NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE
              );
              // 69,70 - UC025
              notificationActions.push(
                NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE
              );
            }
          }
        } else if (
          (requestorTaskForReview.length === 1 ||
            requestorTaskForTermination.length === 1) &&
          employeeTask &&
          employeeTask.length === 1
        ) {
          // 85, 86 UC025
          notificationActions.push(
            NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION
          );
          // 87 , 88 UC025
          notificationActions.push(
            NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE
          );
        }
        if (
          workflow.updated_by.split("__")[1].toLowerCase().trim() ===
          process.env.SYSTEM_EMAIL_ID
        ) {
          // 149 , 150 Project Allocation Review (Resource)- Auto Approve for employee.Auto-allocation alert to employee.
          notificationActions.push(
            NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS
          );
        } else {
          if (
            employeeTask[0].comment.toLowerCase().trim() ===
            WorkflowCommentsEnum.EMPLOYEE_WITHDRAWL_COMMENTS.toLowerCase().trim()
          ) {
            notificationActions.push(
              NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION
            );
            notificationActions.push(
              NotificationTemplateTypes.PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE
            );
          } else {
            // 147 ,148 , 151 , 152 Project Allocation Review (Resource) - Approved by Employee
            notificationActions.push(
              NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS
            );
            // R6
            notificationActions.push(
              NotificationTemplateTypes.PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE
            );
          }
        }
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE.toLocaleLowerCase().trim():
        //81,82 UC025
        notificationActions.push(
          NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON
        );
        //83,84 UC025
        notificationActions.push(
          NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON
        );
        // 143 , 144 Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION
        );
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR.toLocaleLowerCase().trim():
        //81,82 UC025
        notificationActions.push(
          NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON
        );
        //83,84 UC025
        notificationActions.push(
          NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON
        );
        // 145 , 146 Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON
        );
        // }
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION.toLowerCase().trim():
        //89,90 UC025
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR
        );
        //91,92 UC025
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE
        );
        if (
          workflow.updated_by.split("__")[1].toLowerCase().trim() ===
          process.env.SYSTEM_EMAIL_ID
        ) {
          //153 , 154 Employee Rejection approved by  Requestor  - Notification to Employee
          notificationActions.push(
            NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION
          );
        } else {
          //153 , 154 Employee Rejection approved by  Requestor  - Notification to Employee
          notificationActions.push(
            NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION
          );
        }
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION.toLowerCase().trim():
        //99,100 UC026
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR
        );
        //101,102 UC026
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER
        );
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION.toLowerCase().trim():
        // //103,104 UC026
        // notificationActions.push(
        //   NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER
        // );
        // //105,106 UC026
        // notificationActions.push(
        //   NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR
        // );
        // //107,108 UC026
        // notificationActions.push(
        //   NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE
        // );
        // 147 ,148 , 151 , 152 Project Allocation Review (Resource) - Approved by Employee
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS
        );
        break;
      case EEngagementStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION.toLowerCase().trim():
        // 137 , 138 Resource Allocation Review Notification - Rejected - Notification to Requestor
        notificationActions.push(
          NotificationTemplateTypes.REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE
        );
        //157 , 158 Reviewer to Accept Employee Response -  Notification to Employee
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION
        );
        break;
      case EEngagementStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER.toLowerCase().trim():
        //139 , 140 Notification for allocation of a resources to a project (Notification directly to the employee once allocation is updated)
        notificationActions.push(
          NotificationTemplateTypes.NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE
        );
        notificationActions.push(
          NotificationTemplateTypes.PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE
        );
        break;
      case EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_PENDING.toLowerCase().trim():
        //130 , 131 UC028
        notificationActions.push(
          NotificationTemplateTypes.EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH
        );
        break;
      case EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_APPROVED.toLowerCase().trim():
        //132 , 133 UC028
        notificationActions.push(
          NotificationTemplateTypes.SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST
        );
        break;
      case EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_REJECTED.toLowerCase().trim():
        //134 , 135 UC028
        notificationActions.push(
          NotificationTemplateTypes.SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST
        );
      default:
        break;
    }
    return notificationActions;
  }
  async updateApproval(
    updateToBe: UpdateWorkflow & UpdateFileWorkflow,
    user: IUser,
    isproxyApproval: string = null
  ): Promise<InsertAndUpdateItemDTO> {
    try {
      const inProgressTask = await this.repository.findAllWorkflowTask({
        id: updateToBe.workflow_task_id,
      });
      if (!inProgressTask) {
        this.logger.log("Workflow task not found");
        throw new NotFoundException("workflow task not found");
      }
      this.logger.log(inProgressTask);
      //get active workflow of the current_update_workflow_workflow_task
      const inProgressWorkflow = await this.repository.findAllWorkflow(
        {
          id: inProgressTask.workflow_id, // engag
          outcome: WorkflowOutCome.INPROGRESS,
        },
        ["id", "name", "module", "sub_module"]
      );
      console.log(inProgressWorkflow);
      if (!inProgressWorkflow) {
        console.log("No Workflow Found in Progress state");
        throw new NotFoundException("No Workflow Found in Progress state");
      }
      updateToBe.workflow_id = inProgressWorkflow.id;
      updateToBe.workflow_task_title = inProgressTask.title;
      updateToBe.item_id = inProgressWorkflow.item_id;

      const insertAndUpdateItems = await this.updateWorkflowTask(
        inProgressWorkflow,
        updateToBe,
        user,
        isproxyApproval
      );

      return insertAndUpdateItems;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  bulkUpdationProcess(
    element: UpdateWorkflow & UpdateFileWorkflow,
    user: IUser
  ): Promise<InsertAndUpdateItemDTO> {
    return new Promise(async (resolve, reject) => {
      // updateToBe.forEach(async (element) => {
      try {
        const result = await this.updateApproval(element, user);
        resolve(result);
        // const workflow = await this.repository.findAllWorkflow(
        //   {
        //     id: element.workflow_id,
        //   },
        //   []
        // );
        // const noficationAction = this.GetNotificationAction(result, workflow);
        // resolve({
        //   requestPayload: element,
        //   workflowResult: workflow,
        //   actions: noficationAction,
        //   result: result,
        //   error: null,
        //   isError: false,
        // });
      } catch (err) {
        // console.log(JSON.stringify(err));
        // // resolve({ error: JSON.stringify(err), isError: true });
        // resolve({
        //   requestPayload: element,
        //   result: null,
        //   workflowResult: null,
        //   actions: [],
        //   error: err.toString(),
        //   isError: true,
        // });
        console.log(err);
        // totalResults.push(error);
      }
      // });
    });
  }
  publishMassageToServiceBusTopic = async (
    payload: ProjectEventPayloadDto,
    type: string
  ) => {
    const connectionString = process.env["AzureServiceBus"].toString().trim();
    const topic = process.env["topic"].toString().trim();
    const serviceBusConnectionMethod = process.env["ServiceBusConnectionMethod"]
      .toString()
      .trim();
    let serviceBusClient;
    if (serviceBusConnectionMethod === "AD") {
      const fullQualifiedName = process.env[
        "AzureServiceBus__fullyQualifiedNamespace"
      ]
        .toString()
        .trim();
      const clientId: string = process.env["SBClientId"].toString().trim();
      const clientSecret: string = process.env["SBClientSecret"]
        .toString()
        .trim();
      const tenantId: string = process.env["SBTenantId"].toString().trim();
      const credential = new ClientSecretCredential(
        tenantId,
        clientId,
        clientSecret
      );
      serviceBusClient = new ServiceBusClient(fullQualifiedName, credential);
    } else {
      this.logger.log(connectionString);
      // serviceBusClient = new ServiceBusClient(
      //   `Endpoint=sb://rmt-service-bus-dev-nagarro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Q7yAV+QD/ubFvE6vO4czLXRNlKfHjdZV1+ASbCu0ZpE=`
      // );
      serviceBusClient = new ServiceBusClient(connectionString);
    }
    const serviceBusSender = serviceBusClient.createSender(topic);
    // const body = JSON.stringify(payload);

    await serviceBusSender.sendMessages({
      body: payload,
      applicationProperties: {
        type: type,
      },
    });
  };
  async bulkUpdateApproval(
    updateToBe: UpdateWorkflow[] & UpdateFileWorkflow[],
    user: IUser,
    isproxyApproval: string = null
  ): Promise<BulkApprovalResponse[]> {
    try {
      //todo: proper result array
      const totalResults: any[] = [];
      const fr: InsertAndUpdateItemDTO[] = [];
      for (const update of updateToBe) {
        const res = await this.bulkUpdationProcess(update, user);
        fr.push(res);
      }
      console.log(fr);

      const workflowTasks = await this.repository.bulkCreateOrUpdate(fr);

      const updatedWorkflows = updateToBe.map((item) => item.workflow_id);

      const whereCondition: WhereOptions = {};
      whereCondition.where = {
        id: {
          [Op.in]: [...updatedWorkflows],
        },
      };
      const workflows = await this.getWorkflowByQuery(whereCondition.where);
      const workflowAndWorkflowTask: BulkApprovalResponse[] = [];

      for (const workflow of workflows) {
        const workflowTaskForWorkflow = workflowTasks.filter(
          (e) => e.workflow_id === workflow.id
        );
        const actions: string[] = this.GetNotificationAction(
          workflowTaskForWorkflow,
          workflow,
          true
        );
        workflowAndWorkflowTask.push({
          requestPayload: {
            item_id: workflow.item_id,
            workflow_id: workflow.id,
          },
          workflowResult: workflow,
          result: workflowTaskForWorkflow,
          actions: actions,
          error: null,
          isError: false,
        });
      }
      console.log("returning ", fr);
      const updateListResponse =
        this.UpdateRespectiveTablesForCreateOrUpdateWorkflow(
          "update_workflow_title",
          workflowAndWorkflowTask[0].workflowResult.module,
          null,
          workflowAndWorkflowTask,
          user
        );
      // for (const details of workflowAndWorkflowTask) {
      //   const payload: ProjectEventPayloadDto = {
      //     action: "action",
      //     payload: "payload",
      //     token: "token",
      //   };
      //   await publishMassageToServiceBusTopic(payload, "topic");
      // }
      const payload: ProjectEventPayloadDto = {
        action: updateListResponse.action,
        payload: JSON.stringify(updateListResponse.response),
        token: user.token,
      };
      await this.publishMassageToServiceBusTopic(payload, "project");
      console.log(workflowAndWorkflowTask);
      return workflowAndWorkflowTask;
    } catch (error) {
      console.log("Some thing went Wrong in Bulk upload approval ", error);
      throw new Error(error);
    }
  }
  UpdateRespectiveTablesForCreateOrUpdateWorkflow(
    workflow_type: string,
    workflow_module: string,
    createWorkflowDto: CreateWorkflowResponse = null,
    updateWorkflowDto: BulkApprovalResponse[] = null,
    user: IUser
  ) {
    const result: {
      action: string | null;
      response:
        | UpdateUserSkillStatusAfterWorkflowRequestDTO[]
        | UpdateAllocationRequestDTO[]
        | null;
    } = {
      action: null,
      response: [],
    };
    switch (workflow_module.toLowerCase().trim()) {
      case WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim():
        const allocationResult = this.UpdateAllocationListStatus(
          workflow_type.toLowerCase().trim(),
          createWorkflowDto,
          updateWorkflowDto,
          user
        );
        result.action = "REFRESH_ALLOCATION_STATUS";
        result.response = allocationResult;
        break;
      case WorklFlowModule.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toLowerCase().trim():
        const skillResult = this.UpdateUserSkillStatusAfterWorkflow(
          workflow_type.toLowerCase().trim(),
          createWorkflowDto,
          updateWorkflowDto,
          user
        );
        result.action = "REFRESH_SKILL_STATUS";
        result.response = skillResult;
        break;
      default:
        break;
    }
    return result;
  }
  UpdateAllocationListStatus(
    workflow_type: string,
    createWorkflowDto: CreateWorkflowResponse = null,
    updateResponse: BulkApprovalResponse[] = null,
    user: IUser
  ) {
    const response: UpdateAllocationRequestDTO[] = [];
    this.logger.log(createWorkflowDto, workflow_type);
    switch (workflow_type.toLowerCase().trim()) {
      case "create_workflow_title":
        if (createWorkflowDto !== null) {
          response.push({
            AllocationStatus: createWorkflowDto.status,
            guid: createWorkflowDto.item_id,
            WorkflowModule: createWorkflowDto.module,
            WorkflowSubModule: createWorkflowDto.sub_module,
            token: user.token,
          });
        }
        break;
      case "update_workflow_title":
        if (updateResponse) {
          for (const resp of updateResponse) {
            if (!resp.isError) {
              response.push({
                AllocationStatus: resp.workflowResult.status,
                guid: resp.workflowResult.item_id,
                WorkflowModule: resp.workflowResult.module,
                WorkflowSubModule: resp.workflowResult.sub_module,
                token: user.token,
              });
            }
          }
        }
        break;
      default:
        break;
    }
    return response;
  }
  UpdateUserSkillStatusAfterWorkflow(
    workflow_type: string,
    createWorkflowDto: CreateWorkflowResponse = null,
    updateResponse: BulkApprovalResponse[] = null,
    user: IUser
  ) {
    const response: UpdateUserSkillStatusAfterWorkflowRequestDTO[] = [];
    switch (workflow_type.toLowerCase().trim()) {
      // case "create_workflow_title":
      //   break;
      case "update_workflow_title":
        if (updateResponse) {
          for (const resp of updateResponse) {
            if (!resp.isError) {
              response.push({
                Id: resp.workflowResult.item_id,
                ModifiedAt: new Date(),
                ModifiedBy: resp.result[0].updated_by,
                ActionPerformed:
                  resp.result[0].status.toLowerCase().trim() ===
                  WorkflowStatus.APPROVED.toLowerCase().trim()
                    ? "APPROVED"
                    : "REJECTED",
              });
            }
          }
        }
        break;
      default:
        break;
    }
    return response;
  }

  async updateWorkflowTask(
    inProgressWorkflow: WorkflowModel,
    updateToBe: UpdateWorkflow & UpdateFileWorkflow,
    user: IUser,
    isproxyApproval: string = null
  ): Promise<InsertAndUpdateItemDTO> {
    try {
      const { remarks, ...rest } = updateToBe;
      let params = rest;
      // let updatedTask: any = null;
      // Finding the workflow task on the basis of the workflow table id from the workflow_task table
      // so it is mendatory to keep the correct status
      //worong query
      const getWorkflowTask = await this.repository.findAllWorkflowTask({
        workflow_id: inProgressWorkflow.id, // engag
        // status: WorkflowStatus.PENDING,
        id: updateToBe.workflow_task_id,
        title: updateToBe.workflow_task_title,
        // title:
        // title
      });
      // {
      // 2,2
      // }

      // UNKNOWN
      const updateInMs = {} as { type: EUpdateInMSType; meta: any };
      if (!getWorkflowTask) {
        throw new NotFoundException("No Task Found");
      } else if (
        getWorkflowTask.title.toLowerCase().trim() !==
          WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.toLowerCase().trim() &&
        WorkflowStatus.PENDING.trim().toLowerCase() !==
          getWorkflowTask.status.trim().toLowerCase()
      ) {
        throw new NotFoundException("No Task Found In Pending State");
      } else if (
        getWorkflowTask.title.toLowerCase().trim() ===
          WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.toLowerCase().trim() &&
        WorkflowStatus.REJECT.toLowerCase().trim() ===
          getWorkflowTask.status.toLowerCase().trim() &&
        !(
          inProgressWorkflow.status.toLowerCase().trim() ===
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE.toLowerCase().trim() ||
          inProgressWorkflow.status.toLowerCase().trim() ===
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR.toLowerCase().trim()
        )
      ) {
        throw new NotFoundException("No Task Found For Update");
      }
      params["type"] = getWorkflowTask.type;
      params["workflow_id"] = getWorkflowTask.workflow_id;
      if (getWorkflowTask && Object.keys(getWorkflowTask).length > 0) {
        const sCase = `${getWorkflowTask.title}`;
        console.log("Approver S Case", sCase);
        //UNKNOWN
        switch (sCase) {
          case WorkFlowTaskTitle.WF_TASK_TITLE_COE:
            params = await this.validateEngagementAcceptanceTask(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask
            );
            updateInMs.type = EUpdateInMSType.COE_ACCEPTANCE;
            updateInMs.meta = params.coe_acceptance_meta;

            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_SPOC:
            params = this.validateBusinessSPOC(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask
            );
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_PARTNER:
            params = this.validatePartnerAcceptance(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask,
              isproxyApproval
            );
            break;
          case WorkFlowTaskTitle.FILE_UPLOAD:
            params = this.validateFileUploadEngagementTask(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask,
              isproxyApproval
            );
            break;

          case WorkFlowTaskTitle.FILE_SANITY:
            params = this.validateFileUploadEngagementTask(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask,
              isproxyApproval
            );
            break;

          case WorkFlowTaskTitle.KPI_APPROVAL_SPOC:
            params = this.validateKpiApproval(params, user, inProgressWorkflow);
            break;
          // -----> NEWADDED <-----
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL:
            await this.validateReviewerTask(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask
            );
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL:
            await this.validateSupercoachTask(
              params,
              user,
              inProgressWorkflow,
              getWorkflowTask
            );
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL:
            await this.validateEmployeeTask(params, user, inProgressWorkflow);
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL:
            //finding the workflow_task for status of ResourceRequestor Descision
            // this.validateEmployeeWithdrawlTask(
            //   params,
            //   user,
            //   inProgressWorkflow
            // );
            // const getWorkflowTaskOfResourceRequestor =
            //   await this.repository.findAllWorkflowTask({
            //     workflow_id: inProgressWorkflow.id,
            //     title:
            //       WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION,
            //   });

            // if (
            //   getWorkflowTaskOfResourceRequestor &&
            //   getWorkflowTaskOfResourceRequestor.status.trim().toLowerCase() !==
            //     WorkflowStatus.PENDING.trim().toLowerCase()
            // ) {
            //   throw new BadRequestException(
            //     "Action Already Taken Up By Resource Requestor"
            //   );
            // }
            // if (
            //   getWorkflowTaskOfResourceRequestor &&
            //   getWorkflowTaskOfResourceRequestor.status.trim().toLowerCase() ===
            //     WorkflowStatus.PENDING.toLowerCase()
            // ) {
            //   const updatedWorkflowTaskOfResourceRequestor =
            //     getWorkflowTaskOfResourceRequestor.dataValues;
            //   updatedWorkflowTaskOfResourceRequestor["status"] =
            //     WorkflowStatus.TERMINATED;
            //   updatedTask = await this.repository.updateWorkflowtask(
            //     updatedWorkflowTaskOfResourceRequestor,
            //     user
            //   );
            // }
            params.next_step = null;
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION:
            await this.validateResourceRequestorTask(
              params,
              user,
              inProgressWorkflow
            );

            //RR -> Approve the employee rejection -> Employee Rejected successfully -> Employee Action to be closed
            //RR -> Reject the employee rejection -> Employee Stuck -> Move Conflict to CSP -> Employee Action to be closed
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION:
            await this.validateResourceRequestorTerminationTask(
              params,
              user,
              inProgressWorkflow
            );
            break;
          case WorkFlowTaskTitle.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION:
            params.next_step = null;
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_:
            params.next_step = null;
            params.comment = getUserSkillWorkflowComment(
              getWorkflowTask.dataValues?.comment
                ? getWorkflowTask.dataValues.comment
                : "",
              remarks,
              user.email,
              new Date(),
              user.name
            );
            break;
          case WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_CO_SUPERCOACH_:
            params.next_step = null;
            params.comment = getUserSkillWorkflowComment(
              getWorkflowTask.dataValues?.comment
                ? getWorkflowTask.dataValues.comment
                : "",
              remarks,
              user.email,
              new Date(),
              user.name
            );
            break;
          default:
            throw new Error("No Workflow matched");
        }
        if (params.comment == undefined || params.comment == "") {
          params.comment = remarks ? remarks : "";
        }
        //for reject and approve only
        params = this.getRequiredApprovals(
          params,
          inProgressWorkflow,
          getWorkflowTask
        );

        params.id = getWorkflowTask.id;

        params.updated_by = user.email || "system";
        console.log("Before updateApproval", params);
        const updateAndInsertItems = await this.repository.updateApproval(
          params,
          user
        );
        // if (updatedTask !== null) {
        //   workflowTasks.push(updatedTask);
        // }
        // const workflow_current_status: string = params.workflow_table_status;
        //update in microservice --> case basis
        //inProgressWorkflow not used current one present
        // await this.updateWorkflowStatus(inProgressWorkflow, user);
        // this.historyService.addEngagementTaskLog(
        //   workflowTasks,
        //   inProgressWorkflow,
        //   workflow_current_status
        // );
        // await this.updateRefServices(
        //   inProgressWorkflow,
        //   user,
        //   updateToBe,
        //   getWorkflowTask,
        //   updateInMs
        // );

        return updateAndInsertItems;
      } else {
        throw new NotFoundException("No pending approval found");
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async employeeAllocationTermination(user: IUser, currentDate: Date) {
    try {
      const data = await this.repository.employeeAllocationTermiation(
        user,
        currentDate
      );
      return data;
    } catch (error) {
      throw new Error(error);
    }
  }
  async updateWorkflowStatus(workflow: WorkflowModel, user: IUser) {
    // currentWorkflowProgress
    //change condition
    const workflowDb = await this.repository.findAllWorkflow({
      id: workflow.id, // engag
    });
    console.log("Updated Workflow Data From Db", workflowDb);
    const module = workflowDb.module.trim().toLowerCase();
    const sub_module = workflowDb.sub_module.trim().toLowerCase();
    const sCase = `${module}_${sub_module}`;
    switch (sCase) {
      case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION}`.toLowerCase():
        //TODO:- replace by params.item_id
        this.UpdateAllocationStatusInResourceAllocationDetails(
          {
            Guid: "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
            // Guid: workflowDb.item_id,
            AllocationStatus: workflowDb.status,
          } as unknown as updateAllocationStatusDTO,
          user
        );
        break;
      case `${WorklFlowModule.EMPLOYEE_ALLOCATION}_${WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE}`.toLowerCase():
        //TODO:- replace by params.item_id
        this.UpdateAllocationStatusInResourceAllocationDetails(
          {
            Guid: "44815ad6-b68f-4363-9f91-4b0fd2a4b8c5",
            // Guid: workflowDb.item_id,
            AllocationStatus: workflowDb.status,
          } as unknown as updateAllocationStatusDTO,
          user
        );
        break;
      default:
        break;
    }
  }
  async UpdateAllocationStatusInResourceAllocationDetails(
    updateAllocationBody: updateAllocationStatusDTO,
    user: IUser
  ) {
    try {
      const result = await this.allocationService.UpdateAllocationStatus(
        updateAllocationBody,
        user
      );
      return result;
    } catch (error) {
      throw new Error(
        "Something went wrong in Updating Allocation Status " + error
      );
    }
  }
  async updateRefServices(
    inProgressWorkflow: WorkflowModel,
    user: IUser,
    updateToBe: UpdateWorkflow & UpdateFileWorkflow,
    task: WorkflowTaskModel,
    extraMeta: any
  ): Promise<void> {
    try {
      if (inProgressWorkflow.module == WorklFlowModule.KPI) {
        await this.updateKpi(true, user.token, {
          id: task.workflow_id,
          status: "",
          remarks: updateToBe.comment,
        });
      }
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);

      throw error;
    }
  }

  async updateEngagement(
    isfetchNew: boolean,
    user: IUser,
    params: IEngagementUpdate
  ): Promise<void> {
    try {
      if (isfetchNew) {
        const workflow = await this.repository.findAllWorkflow(
          { id: params.id },
          ["status"]
        );
        params.id = workflow.item_id;
        params.toUpdate = { ...params.toUpdate, status: workflow.status };
      }

      await this.engagementService.updateStatus(params, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async updateEngagementForProxy(
    isfetchNew: boolean,
    user: IUser,
    params: IEngagementUpdate
  ): Promise<void> {
    try {
      if (isfetchNew) {
        const workflow = await this.repository.findAllWorkflow(
          { id: params.id },
          ["status"]
        );
        params.id = workflow.item_id;
        params.toUpdate = { ...params.toUpdate, status: workflow.status };
      }

      await this.engagementService.updateEngagementForProxy(params, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async updateKpiExecutionStatus(
    params: IKpiExecutionUpdate,
    user: IUser
  ): Promise<void> {
    try {
      await this.engagementService.updateKpiExecutionStatus(params, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async updateKpi(
    isfetchNew: boolean,
    token: string,
    params: IKpiUpdate
  ): Promise<void> {
    try {
      if (isfetchNew) {
        const workflow = await this.repository.findAllWorkflow(
          { id: params.id },
          ["status"]
        );
        params.id = workflow.item_id;
        params.status = workflow.status;
        params.remarks = params.remarks;
      }

      await this.kpiService.updateKpi(params, token);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  getRequiredApprovals(
    params: UpdateFileWorkflow & UpdateWorkflow,
    workflow: WorkflowModel,
    workflowTask: WorkflowTaskModel
  ) {
    const type = `${params.status}${workflowTask.title}`.toLowerCase();

    this.logger.debug("getRequiredApprovals type===>" + type, LOG_CONTEXT);

    switch (type.toLowerCase()) {
      //here we need to create status for workflow_task and workflow table
      //-----> NEW ADDED <-----
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL}`.toLowerCase():
        if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim()
        ) {
          //TODO:- CHANGE
          params.status = WorkflowStatus.APPROVED;
          params.workflow_table_outcome = WorkflowOutCome.INPROGRESS;
          params.workflow_table_status = params.workflow_table_status;
        } else if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE.toLowerCase().trim()
        ) {
          //TODO :- CHANGE
          params.status = WorkflowStatus.APPROVED;
          params.workflow_table_outcome = params.workflow_table_outcome;
          params.workflow_table_status = params.workflow_table_status;
        }
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL}`.toLowerCase():
        if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim()
        ) {
          params.status = WorkflowStatus.REJECT;
          params.workflow_table_outcome = WorkflowOutCome.CLOSE;
          params.workflow_table_status =
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER;
        } else if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE.toLowerCase().trim()
        ) {
          params.status = WorkflowStatus.REJECT;
          params.workflow_table_outcome = WorkflowOutCome.CLOSE;
          params.workflow_table_status =
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER;
        }
        //
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL}`.toLowerCase():
        if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim()
        ) {
          params.status = WorkflowStatus.APPROVED;
          params.workflow_table_outcome = WorkflowOutCome.INPROGRESS;
          params.workflow_table_status =
            EEngagementStatus.EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
        } else if (
          workflow.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          workflow.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION_UPDATE.toLowerCase().trim()
        ) {
          params.status = WorkflowStatus.APPROVED;
          params.workflow_table_outcome = WorkflowOutCome.CLOSE;
          params.workflow_table_status =
            EEngagementStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH;
        }
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL}`.toLowerCase():
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_SUPERCOACH;
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL}`.toLowerCase():
        //TODO: ADD BELOW CONDITIONS
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE;
        //condition if employee had approved his allocation
        //status -> approved
        //outcome -> closed
        // wf_status -> completed.EMPLOYEE_ALLOCATION_.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL}`.toLowerCase():
        //TODO: ADD BELOW CONDITIONS
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.INPROGRESS;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE;
        const resourceRequestorConfigValue = parseInt(
          params.configuration.resourceRequestorConfiguration.attributeValue
        );
        this.logger.log(`Debugging`, JSON.stringify(params.configuration));
        this.logger.log(
          `Debugging1`,
          JSON.stringify(params.configuration.resourceRequestorConfiguration)
        );
        this.logger.log(`Debugging2`, resourceRequestorConfigValue);
        //TODO:- Remove Permanaer false
        const isRR = resourceRequestorConfigValue === -1 ? false : true;
        if (isRR) {
          params.workflow_table_status =
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR;
        }
        break;

      //condition if employee had rejected his allocation
      //status -> rejected/"TBD"
      //outcome -> inprogress
      //wf_status -> WorkflowStatus.EMPLOYEE_ALLOCATION_EMOPLOYEE_REJECTED
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL}`.toLocaleLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE;
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION}`.toLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_ACCEPT_EMPLOYEE_REJECTION;
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION}`.toLowerCase():
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.INPROGRESS;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_REJECT_EMPLOYEE_REJECTION;
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION}`.toLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.TERMINATE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_TERMINATION_BY_RR;
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION}`.toLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION;
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION}`.toLowerCase():
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.Employee_ALLOCATION_SUPERCOACH_REJECTED_RESOURCE_REQUESTOR_REJECTION;
        break;

      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_}`.toLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_APPROVED;
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_}`.toLowerCase():
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_REJECTED;
        break;
      case `${WorkflowStatus.APPROVED}${WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_CO_SUPERCOACH_}`.toLowerCase():
        params.status = WorkflowStatus.APPROVED;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_APPROVED;
        break;
      case `${WorkflowStatus.REJECT}${WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_CO_SUPERCOACH_}`.toLowerCase():
        params.status = WorkflowStatus.REJECT;
        params.workflow_table_outcome = WorkflowOutCome.CLOSE;
        params.workflow_table_status =
          EEngagementStatus.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_REJECTED;
        break;
      default:
        break;
    }
    // console.log(params, '----params');
    return params;
  }

  async addKpiExecutionWorkflow(
    params: AddKpiExecutionWorkflow,
    user: IUser
  ): Promise<any> {
    try {
      const workflows = await this.repository.addKpiExecutionWorkflow(
        params,
        user
      );

      if (!workflows.length) {
        this.logger.debug("No Kpi execution workflow found", LOG_CONTEXT);
        throw new NotFoundException("No Kpi execution workflow found");
      }

      for (const workflow of workflows) {
        await Promise.all([
          this.historyService.addEngagementLog(workflow),
          this.historyService.addEngagementTaskLog(
            workflow.task_list,
            workflow
          ),
        ]);
      }
      return workflows;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async updateKpiExecutionWorkflow(
    params: UpdateKpiWorkflowDto,
    user: IUser
  ): Promise<any> {
    try {
      const pendingWorkflows = await this.repository.findAllWorkflow(
        {
          parent_id: params.parent_id,
          item_id: params.item_id, // engag
          sub_module: WorklFlowSubModule.KPI_EXECUTION_WORKFLOW,
          outcome: WorkflowOutCome.INPROGRESS,
        },
        ["id", "name", "module", "sub_module", "status"]
      );

      if (!pendingWorkflows) {
        this.logger.debug(
          "No Kpi execution workflow Found in Progress state",
          LOG_CONTEXT
        );
        // await this.addKpiExecutionWorkflow(
        //     {
        //         engagement_id: params.parent_id,
        //         item_id: [params.item_id],
        //         name: pendingWorkflows.name,
        //         status: params.status,
        //     },
        //     user,
        // );
        // pendingWorkflows = await this.repository.findAllWorkflow(
        //     {
        //         parent_id: params.parent_id,
        //         item_id: params.item_id, // engag
        //         sub_module: WorklFlowSubModule.KPI_EXECUTION_WORKFLOW,
        //         outcome: WorkflowOutCome.INPROGRESS,
        //     },
        //     ['id', 'name', 'module', 'sub_module', 'status'],
        // );
        throw new NotFoundException(
          "No Kpi execution workflow Found in Progress state"
        );
      }

      const findTask = await this.repository.findAllWorkflowTask({
        workflow_id: pendingWorkflows.id,
        status: "Running",
      });

      if (params.active !== undefined) {
        await this.historyService.addKpiHistory(
          pendingWorkflows,
          params,
          findTask
        );
        return pendingWorkflows;
      }

      if (!findTask) {
        this.logger.debug(
          "No Kpi workflow task Found in Progress state",
          LOG_CONTEXT
        );
        throw new NotFoundException(
          "No Kpi workflow task Found in Progress state"
        );
      }

      const workflowUpdate = {
        id: pendingWorkflows.id,
        status: params.status,
        updated_by: user.email,
      };

      const workflowtaskUpdate = {
        id: findTask.id,
        status: WorkflowStatus.COMPLETED,
        updated_by: user.email,
      };

      let isAddNewWorkflow = false;

      if ([EEngagementStatus.KPI_EXECUTION_SELECT].includes(params.status)) {
        workflowUpdate["outcome"] = "closed";
        isAddNewWorkflow = true;
      } else {
        const newTask = this.repository.generateWorkFlowTaskDetails(
          {
            name: user.service_line.toLowerCase(),
            module: WorklFlowModule.KPI,

            sub_module: WorklFlowSubModule.KPI_EXECUTION_WORKFLOW,
            created_by: user.email,
          } as CreateWorkflow,
          params.status
        );
        newTask.workflow_id = pendingWorkflows.id;
        const workflowTasks = await this.repository.addWorkflowTask(newTask);
        await this.historyService.addEngagementTaskLog(
          [workflowTasks],
          pendingWorkflows
        );
      }

      await Promise.all([
        this.repository.updateWorkflow(workflowUpdate),
        // -----> NEW COMMENTED <-----
        // this.repository.updateWorkflowtask(workflowtaskUpdate),
      ]);

      if (isAddNewWorkflow) {
        await this.addKpiExecutionWorkflow(
          {
            engagement_id: pendingWorkflows.parent_id,
            item_id: [pendingWorkflows.item_id],
            name: pendingWorkflows.name,
            status: EEngagementStatus.KPI_EXECUTION_SELECT,
          },
          user
        );
      }

      return pendingWorkflows;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  async getKpiReportWorkflow(query: GetKpiReportWorkflowDTO): Promise<any> {
    try {
      return await this.repository.getKpiReportWorkflow(query);
    } catch (error) {
      throw error;
    }
  }
  async getUserDetails(email: string[], token: string): Promise<any[]> {
    try {
      console.time("===>USER");
      return new Promise(async (resolve, reject) => {
        return await this.identityService
          .getAllUserDetails(
            {
              email_id: email,
            },
            token
          )
          .then((respone) => {
            console.timeEnd("===>USER");
            resolve(respone);
          })
          .catch((err) => {
            reject(err);
          });
      });
    } catch (err) {
      throw [err];
    }
  }
  public async getAuditData(
    params: WorkFlowAuditDTO,
    user: IUser
  ): Promise<WorkflowModel[]> {
    try {
      const response = await this.repository.getAuditWorkflow(params);
      let createdById = [];
      response.forEach((workflow) => {
        const email = workflow?.history?.map((log) => log?.created_by);
        createdById = [createdById, ...email];
      });

      const userDetails = await this.getUserDetails(
        Array.from(createdById),
        user.token
      );
      response.forEach((workflow) => {
        workflow?.history?.forEach((log) => {
          userDetails.find((user) => {
            if (user.email_id == log.created_by) {
              log.created_by = user.name;
            }
          });
        });
      });
      return response;
    } catch (error) {
      throw error;
    }
  }

  async updateWorkflowAndHistory(
    updateToBe: UpdateWorkflowHistoryDto,
    user: IUser
  ): Promise<WorkflowModel> {
    try {
      const msg = "No workflow Found in Progress state";
      const inProgressWorkflow = await this.repository.findAllWorkflow(
        {
          item_id: updateToBe.item_id, // engag
          outcome: WorkflowOutCome.INPROGRESS,
        },
        ["id", "name", "module", "sub_module"]
      );

      if (!inProgressWorkflow) {
        throw new NotFoundException(msg);
      }

      if (updateToBe.status === EEngagementStatus.ENGAGEMENT_CLOSE) {
        inProgressWorkflow.set("outcome", WorkflowOutCome.CLOSE);
      }

      inProgressWorkflow.set("status", updateToBe.status);
      inProgressWorkflow.set("updated_by", user.email);
      await inProgressWorkflow.save();

      this.historyService.addWorkflowHistory(inProgressWorkflow);

      return inProgressWorkflow;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  public async getCommentsByItemId(
    query: GetCommentsByItemIdRequest
  ): Promise<any> {
    try {
      return await this.repository.getCommentsByItemId(query);
      // return data.map((item) => {
      //   return {
      //     item_id: item.item_id,
      //     task_list: item.task_list.map(
      //       (tasksComments) => tasksComments.comment
      //     ),
      //   };
      // });
    } catch (error) {
      throw error;
    }
  }

  public async getMySkillTasksAssigned(user: IUser): Promise<any> {
    try {
      return await this.repository.getMySkillTasksAssigned(user);
    } catch (error) {
      throw error;
    }
  }

  public async getMultipleTasksByByQuery(
    params: getMultipleTasksByByQueryDto
  ): Promise<WorkflowModel[]> {
    try {
      return this.repository.getMultipleTasksByByQuery(params);
    } catch (error) {
      throw error;
    }
  }
}
