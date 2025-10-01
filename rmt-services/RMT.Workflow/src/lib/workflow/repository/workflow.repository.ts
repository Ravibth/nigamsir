import {
  BadRequestException,
  ConflictException,
  Injectable,
  InternalServerErrorException,
  Logger,
  NotFoundException,
} from "@nestjs/common";
import {
  Op,
  Transaction,
  where,
  WhereOptions,
  literal,
  fn,
  col,
  Sequelize,
} from "sequelize";

import { InjectModel } from "@nestjs/sequelize";
import { CreateWorkflowTaskDto } from "../../workflow-task/dto/workflowTask.dto";
import { WorkflowTaskModel } from "../../workflow-task/models/workflow-task.model";
import { WorkflowHistoryModel } from "../../workflowHistory/models/workflowHistory.model";
import { CreateWorkflow } from "../dto/createWorkflow.dto";
import {
  EEngagementStatus,
  EngagementApproverType,
  EWorkflowStep,
  EWorkflowSteps,
  EWorkflowTaskAssignedType,
  SupercoachPendingTaskTitle,
  WF_TASK_TITLE_APPROVAL_PENDING,
  WorkflowOutCome,
  WorkflowStatus,
  WorkFlowTaskTitle,
  WorklFlowModule,
  WorklFlowSubModule,
} from "../enum";
import { WorkflowModel } from "../models/workflow.model";

import { IUser } from "../../../common/decorators/user.decorator";
import { AddKpiExecutionWorkflow } from "../dto/addKpiExecutionWorkflow.dto";
import { findWorkflowDto } from "../dto/findApproval.dto";
import { GetKpiReportWorkflowDTO } from "../dto/getKpiReportWorkflow.dto";
import { UpdateFileWorkflow } from "../dto/updateFileWorkflow.dto";
import { UpdateWorkflow } from "../dto/updateWorkflow.dto";
import { WorkFlowAuditDTO } from "../dto/workflowAudit.dto";
import { CreateWorkflowHelper } from "../helpers/createWorkflow.helper";
import { transformTaskAssignee } from "../helpers/taskAssignee";
import { WorkflowModule } from "../workflow.module";
import { ConfigurationService } from "src/microserviceModules/configuration/configuration.service";
// import sequelize from "sequelize/types/sequelize";
import sequelize from "sequelize";
import { terminateWorkflowByItemIdDTO } from "../dto/terminateWorkflowByItemId.dto";
import { GetCommentsByItemIdRequest } from "../dto/getCommentsByItemIdRequest.dto";
import { GetUnique, getUserSkillWorkflowComment } from "../helpers/util";
import { getMultipleTasksByByQueryDto } from "../dto/getMultipleTasksByByQuery.dto";
import { findWorkflowTaskDtoChanges } from "../dto/findWorkflowTaskChange.dto";
import { InsertAndUpdateItemDTO } from "../dto/InsertAndUpdateItem.dto";
import { title } from "process";
import { RefreshWorkflowTaskAssignment } from "../dto/refreshWorkflowTaskAssignment.dto";
import { terminateWorkflowByPipelineCodeAndJobCodeDTO } from "../dto/terminateWorkflowByPipelineCodeAndJobCode";
import { getWorkflowByOutcomeWorklFlowModuleAndUpdateDate } from "../dto/getWorkflowByOutcomeWorklFlowModuleAndUpdateDate.dto";
import { UpdateSupercoachAndDelegateDto } from "../dto/updateSupercoachAndDelegate.dto";

const LOG_CONTEXT = "workflow-repo";

@Injectable()
export class WorkflowRepository {
  constructor(
    @InjectModel(WorkflowModel)
    private model: typeof WorkflowModel,
    @InjectModel(WorkflowTaskModel)
    private taskModel: typeof WorkflowTaskModel,
    @InjectModel(WorkflowHistoryModel)
    private workflowHistoyModel: typeof WorkflowHistoryModel,
    private readonly workflowHelperService: CreateWorkflowHelper,
    private readonly logger: Logger,
    private readonly configurationService: ConfigurationService
  ) {}

  public async findAll(): Promise<WorkflowModel[]> {
    try {
      return await this.model.findAll();
    } catch (err) {
      throw new InternalServerErrorException();
    }
  }
  public async updateSupercoachAndDelegate(
    params: UpdateSupercoachAndDelegateDto
  ) {
    try {
      const [allocationWorkflowTask] = await this.taskModel.sequelize.query(
        `
        SELECT wt.* , w.entity_meta_data
        FROM "WORKFLOW_TASK" as wt
        JOIN "WORKFLOW" w ON w.id = wt.workflow_id
        WHERE 
          wt.status = :status
          AND w.module = 'Employee Allocation'
          AND w.outcome = :outcome
          AND EXISTS (
            SELECT 1
            FROM jsonb_array_elements(wt.assigned_to_json::jsonb) AS elem
            WHERE elem ->> 'supercoach_email' ILIKE :supercoach_email
          );
        `,
        {
          replacements: {
            supercoach_email: params.supercoach_email,
            // allocation_delegate_email: params.prev_allocation_delegate_email,
            status: params.status,
            outcome: params.outcome,
          },
        }
      );
      const [skillWorkflowTask] = await this.taskModel.sequelize.query(
        `
        SELECT wt.* 
        FROM "WORKFLOW_TASK" as wt
        JOIN "WORKFLOW" w ON w.id = wt.workflow_id
        WHERE 
          wt.status = :status
          AND w.module IN ('WORKFLOW_MODULE_USER_SKILL_ASSESSMENT' , 'workflow_module_user_supercoach_assessment')
          AND w.outcome = :outcome
          AND EXISTS (
            SELECT 1
            FROM jsonb_array_elements(wt.assigned_to_json::jsonb) AS elem
            WHERE elem ->> 'supercoach_email' ilike (:supercoach_email) 
          );
        `,
        {
          replacements: {
            supercoach_email: params.supercoach_email,
            // skill_delegate_email: params.prev_skill_delegate_email,
            status: params.status,
            outcome: params.outcome,
          },
        }
      );
      if (allocationWorkflowTask && allocationWorkflowTask.length > 0) {
        const updated_assigned_to =
          params.new_allocation_delegate_email !== null
            ? `${params.supercoach_email},${params.new_allocation_delegate_email}`
            : params.supercoach_email;
        const updated_assigned_to_json = [
          {
            supercoach_email: params.supercoach_email,
            allocation_delegate_email: params.new_allocation_delegate_email,
          },
        ];
        const tasks_id = allocationWorkflowTask.map((x: any) => x.id);
        await this.taskModel.update(
          {
            assigned_to: updated_assigned_to?.toLowerCase()?.trim(),
            assigned_to_json: updated_assigned_to_json,
          },
          {
            where: {
              id: {
                [Op.in]: tasks_id,
              },
            },
          }
        );

        const pipelineCodeJobCodeGrp = allocationWorkflowTask.map((x: any) => ({
          pipeline_code: x.entity_meta_data.PipelineCode,
          job_code: x.entity_meta_data.JobCode,
        }));

        const pcJcMap = new Map<string, string[] | null>();

        pipelineCodeJobCodeGrp.forEach((x) => {
          if (!pcJcMap.has(x.pipeline_code)) {
            pcJcMap.set(x.pipeline_code, x.job_code ? [x.job_code] : null);
          } else {
            if (!x.job_code) {
              if (pcJcMap.get(x.pipeline_code) !== null) {
                pcJcMap.set(x.pipeline_code, null);
              }
            } else {
              const jbCodes: string[] | null = pcJcMap.get(x.pipeline_code);
              if (jbCodes !== null && !jbCodes.includes(x.job_code)) {
                jbCodes.push(x.job_code);
              }
            }
          }
        });

        // Build final array with each object containing pipeline_code and job_code (one per row)
        const finalArray: {
          pipeline_code: string;
          job_code: string | null;
          supercoach_email: string;
          prev_allocation_delegate_email: string;
          new_allocation_delegate_email: string;
        }[] = [];

        pcJcMap.forEach((job_codes, pipeline_code) => {
          if (job_codes === null) {
            finalArray.push({
              pipeline_code,
              job_code: null,
              supercoach_email: params.supercoach_email,
              prev_allocation_delegate_email:
                params.prev_allocation_delegate_email,
              new_allocation_delegate_email:
                params.new_allocation_delegate_email,
            });
          } else {
            job_codes.forEach((job_code) => {
              finalArray.push({
                pipeline_code,
                job_code,
                supercoach_email: params.supercoach_email,
                prev_allocation_delegate_email:
                  params.prev_allocation_delegate_email,
                new_allocation_delegate_email:
                  params.new_allocation_delegate_email,
              });
            });
          }
        });

        console.log(finalArray);
      }
      if (skillWorkflowTask && skillWorkflowTask.length > 0) {
        const updated_assigned_to =
          params.new_skill_delegate_email !== null
            ? `${params.supercoach_email},${params.new_skill_delegate_email}`
            : params.supercoach_email;
        const updated_assigned_to_json = [
          {
            supercoach_email: params.supercoach_email,
            skill_delegate_email: params.new_skill_delegate_email,
          },
        ];
        const tasks_id = skillWorkflowTask.map((x: any) => x.id);
        await this.taskModel.update(
          {
            assigned_to: updated_assigned_to?.toLowerCase()?.trim(),
            assigned_to_json: updated_assigned_to_json,
          },
          {
            where: {
              id: {
                [Op.in]: tasks_id,
              },
            },
          }
        );
      }
      return { allocationWorkflowTask, skillWorkflowTask };
    } catch (error) {
      throw error;
    }
  }
  public async getWorkflowByQuery(params: any): Promise<WorkflowModel[]> {
    try {
      return await this.model.findAll({
        where: { ...params },
      });
    } catch (err) {
      throw new InternalServerErrorException();
    }
  }
  public async getWorkflowByUpdateQuery(
    params: getWorkflowByOutcomeWorklFlowModuleAndUpdateDate
  ): Promise<WorkflowModel[]> {
    try {
      const where: any = {};

      if (params.outcome) {
        where.outcome = params.outcome;
      }

      if (params.module) {
        where.module = params.module;
      }

      if (params.updated_at) {
        const date = new Date(params.updated_at);
        const startOfDay = new Date(
          date.getFullYear(),
          date.getMonth(),
          date.getDate()
        );
        const endOfDay = new Date(
          date.getFullYear(),
          date.getMonth(),
          date.getDate() + 1
        );
        where.updatedAt = {
          [Op.gte]: startOfDay,
          [Op.lt]: endOfDay,
        };
      }

      return await this.model.findAll({ where });
    } catch (err) {
      throw new InternalServerErrorException();
    }
  }
  public async terminateInProgressWorkflowByItemId(
    updateItem: terminateWorkflowByItemIdDTO
  ) {
    try {
      const data = {
        outcome: WorkflowOutCome.TERMINATE,
        status: updateItem.WorkflowStatus,
      } as WorkflowModule;
      const result = this.model.update(data, {
        where: {
          item_id: updateItem.ItemId,
          outcome: WorkflowOutCome.INPROGRESS,
        },
      });
    } catch (error) {
      throw new Error(error);
    }
  }

  public async GetEmployeeAllocationTaskInfoByPipelineCodeAndJobCode(
    pipelineCode: string,
    jobCode: string | null
  ) {
    const tasks: WorkflowTaskModel[] = await this.taskModel.sequelize.query(
      `SELECT t.*
   FROM "WORKFLOW_TASK" AS t
   JOIN "WORKFLOW" AS w ON t.workflow_id = w.id
   WHERE t.status ILIKE ANY (ARRAY[?::text, ?::text])
     AND w.outcome ILIKE ?
     AND w."module" ILIKE ?
     AND LOWER(w.entity_meta_data->>'PipelineCode') ILIKE LOWER(?)
     AND ( LOWER(w.entity_meta_data->>'JobCode') ILIKE LOWER(?) OR LOWER(w.entity_meta_data->>'JobCode') IS NULL );`,
      {
        replacements: [
          WorkflowStatus.PENDING,
          WorkflowStatus.REJECT,
          WorkflowOutCome.INPROGRESS,
          WorklFlowModule.EMPLOYEE_ALLOCATION, // Ensure this matches exactly
          pipelineCode,
          jobCode,
        ],
        type: sequelize.QueryTypes.SELECT,
      }
    );
    return tasks;
  }

  public async terminateWorkflowByPipelineCodeAndJobCode(
    params: terminateWorkflowByPipelineCodeAndJobCodeDTO
  ) {
    try {
      const tasks =
        await this.GetEmployeeAllocationTaskInfoByPipelineCodeAndJobCode(
          params.PipelineCode,
          params.JobCode ?? null
        );
      this.logger.log(JSON.stringify(tasks, undefined, 2));
      const workflow_ids = GetUnique(tasks.map((e) => e.workflow_id));
      this.logger.log(JSON.stringify(workflow_ids, undefined, 2));

      const response = await this.model.update(
        { outcome: WorkflowOutCome.TERMINATE },
        {
          where: { id: { [Op.in]: workflow_ids } },
        }
      );
      return;
    } catch (err) {
      this.logger.log(err);
      throw err;
    }
  }

  public async getEmployeeTaskCountByQuery(params: any): Promise<number> {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.like]: params.taskstatus,
          },
          assigned_to: {
            [Op.substring]: params?.assigned_to?.toLowerCase(),
          },
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: params.outcome,
              },
            },
          },
        ],
      });

      //return result;
      if (result) {
        return result.length;
      } else {
        return 0;
      }
    } catch (error) {}
  }

  public async getEmployeeWorkflowWithdrwalTask(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.like]: WorkflowStatus.REJECT,
          },
          // assigned_to: {
          //   [Op.like]: params.employeeEmail.trim().toLowerCase(),
          // },
          assigned_to: sequelize.where(
            sequelize.fn("LOWER", sequelize.col("assigned_to")),
            "LIKE",
            "%" + params.employeeEmail.trim().toLowerCase() + "%"
          ),
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: WorkflowOutCome.INPROGRESS,
              },
              module: {
                [Op.like]: WorklFlowModule.EMPLOYEE_ALLOCATION,
              },
              status: {
                [Op.in]: [
                  EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE,
                  EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR,
                ],
              },
            },
          },
        ],
      });
      return result;
    } catch (error) {}
  }

  public async getWorkflowTasksDetails(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.like]: params.workflow_task_status,
          },
          assigned_to: sequelize.where(
            sequelize.fn("LOWER", sequelize.col("assigned_to")),
            "LIKE",
            "%" + params.employeeEmail.trim().toLowerCase() + "%"
          ),
          // assigned_to: {
          //   [Op.like]: "%" + params.employeeEmail.trim().toLowerCase() + "%",
          // },
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: params.outcome,
              },
              module: {
                [Op.like]: params.module,
              },
              // sub_module: {
              //   [Op.like]: params.sub_module,
              // },
            },
          },
        ],
      });
      return result;
    } catch (error) {
      throw Error(error);
    }
  }

  public async refreshPendingWorkflowTasksForAllocationWorkflow(
    params: RefreshWorkflowTaskAssignment
  ) {
    const txn = await this.taskModel.sequelize.transaction();
    try {
      if (
        params.previousAssignTo === null ||
        params.previousAssignTo.length === 0
      ) {
        return;
      }
      const allocationTasks: WorkflowTaskModel[] =
        await this.taskModel.sequelize.query(
          `SELECT t.*
   FROM "WORKFLOW_TASK" AS t
   JOIN "WORKFLOW" AS w ON t.workflow_id = w.id
   WHERE t.status ILIKE ANY (ARRAY[?::text, ?::text])
     AND t.title ILIKE ANY (ARRAY[?::text, ?::text])
     AND t.assigned_to ILIKE ?
     AND w.outcome ILIKE ?
     AND w."module" ILIKE ?
     AND LOWER(w.entity_meta_data->>'PipelineCode') ILIKE LOWER(?)
     AND ( LOWER(w.entity_meta_data->>'JobCode') ILIKE LOWER(?) OR LOWER(w.entity_meta_data->>'JobCode') IS NULL );`,
          {
            replacements: [
              WorkflowStatus.PENDING,
              WorkflowStatus.REJECT,
              WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION,
              WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION,
              `%${params.previousAssignTo}%`,
              WorkflowOutCome.INPROGRESS,
              WorklFlowModule.EMPLOYEE_ALLOCATION, // Ensure this matches exactly
              params.pipelineCode,
              params.jobCode,
            ],
            type: sequelize.QueryTypes.SELECT,
          }
        );
      this.logger.log(allocationTasks);
      const updateTaskDetails = allocationTasks.map((task) => {
        const taskChagngeDetails: {
          id: string;
          assigned_to: string;
          title: string;
        } = {
          id: task.id,
          assigned_to: task?.assigned_to?.toLowerCase()?.trim(),
          title: task.title,
        };
        const assigned_to_list = taskChagngeDetails.assigned_to.split(",");
        let updated_assigned_list = [];

        if (
          params.currentAssignTo === null ||
          params.currentAssignTo.length === 0
        ) {
          updated_assigned_list = assigned_to_list.filter(
            (e) =>
              e.toLowerCase().trim() !==
              params.previousAssignTo.toLowerCase().trim()
          );
          //need to remove the previous one
        } else {
          updated_assigned_list = assigned_to_list.map((assignment) => {
            if (
              assignment.toLowerCase().trim() ===
              params.previousAssignTo.toLowerCase().trim()
            ) {
              return params.currentAssignTo;
            } else {
              return assignment;
            }
          });
        }
        return {
          ...taskChagngeDetails,
          assigned_to: updated_assigned_list?.join(",")?.toLowerCase(),
        };
      });
      for (const taskDetails of updateTaskDetails) {
        if (
          WF_TASK_TITLE_APPROVAL_PENDING.indexOf(
            taskDetails.title.toLowerCase().trim()
          ) > -1
        )
          await this.taskModel.update(
            {
              assigned_to: taskDetails?.assigned_to?.toLowerCase()?.trim(),
            },
            {
              where: {
                id: taskDetails.id,
              },
              transaction: txn,
            }
          );
      }
      await txn.commit();
    } catch (err) {
      this.logger.log(err);
      await txn.rollback();
      throw Error(err);
    }
  }

  public async refreshPendingWorkflowTasksForAllocationSuperCoachWorkflow(
    params: RefreshWorkflowTaskAssignment,
    allocation_assignment: any,
    allocation_assignment_json: any
  ) {
    try {
      const pendingsTasks: WorkflowTaskModel[] =
        await this.getPendingWorkflowTasksForAllocationSuperCoach(
          params.previousAssignTo,
          params.employeeEmail,
          WorkflowStatus.PENDING,
          WorkflowOutCome.INPROGRESS,
          WorklFlowModule.EMPLOYEE_ALLOCATION
        );
      this.logger.log(pendingsTasks);
      const taskIds = pendingsTasks.map((task) => task.id);
      console.log(taskIds);
      this.logger.log(taskIds);
      await this.taskModel.update(
        {
          assigned_to: allocation_assignment,
          assigned_to_json: allocation_assignment_json,
        },
        {
          where: {
            id: { [Op.in]: taskIds },
          },
        }
      );
    } catch (err) {
      throw Error(err);
    }
  }
  public async getPendingWorkflowTasksForAllocationSuperCoach(
    supercoach_email: string,
    employee_email: string,
    workflow_task_status: string,
    workflow_status: string,
    workflow_module: string
  ) {
    try {
      const pendingsTasks: WorkflowTaskModel[] =
        await this.taskModel.sequelize.query(
          `SELECT t.*
          FROM "WORKFLOW_TASK" AS t
          JOIN "WORKFLOW" AS w ON t.workflow_id = w.id
          WHERE t.status ILIKE ANY (ARRAY[?::text])
          AND t.assigned_to_json->0->>'supercoach_email' ILIKE  ?
          AND w.outcome ILIKE ?
          AND w."module" ILIKE ?
          AND LOWER(w.entity_meta_data->>'EmpEmail') ILIKE LOWER(?);`,
          {
            replacements: [
              workflow_task_status,
              `%${supercoach_email}%`,
              workflow_status,
              workflow_module, // Ensure this matches exactly
              employee_email,
            ],
            type: sequelize.QueryTypes.SELECT,
          }
        );
      return pendingsTasks;
    } catch (error) {
      this.logger.error(error);
      throw error;
    }
  }
  public async refreshPendingWorkflowTasksForSkillWorkflow(
    params: RefreshWorkflowTaskAssignment,
    skill_assignment: any,
    skill_assignment_json: any
  ) {
    try {
      const skillTasks: WorkflowTaskModel[] =
        await this.taskModel.sequelize.query(
          `SELECT t.*
      FROM "WORKFLOW_TASK" AS t
      JOIN "WORKFLOW" AS w ON t.workflow_id = w.id
      WHERE t.status ILIKE ANY (ARRAY[?::text])
     AND t.assigned_to_json->0->>'supercoach_email' ILIKE  ? 
     AND w.outcome ILIKE ?
     AND w."module" ILIKE ?
     AND LOWER(w.entity_meta_data->>'Email') ILIKE LOWER(?);`,
          {
            replacements: [
              WorkflowStatus.PENDING,
              `%${params.previousAssignTo}%`,
              WorkflowOutCome.INPROGRESS,
              WorklFlowModule.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT, // Ensure this matches exactly
              params.employeeEmail,
            ],
            type: sequelize.QueryTypes.SELECT,
          }
        );
      this.logger.log(skillTasks);
      const taskIds = skillTasks.map((task) => task.id);

      console.log(taskIds);
      this.logger.log(taskIds);
      await this.taskModel.update(
        {
          assigned_to: skill_assignment,
          assigned_to_json: skill_assignment_json,
        },
        {
          where: {
            id: { [Op.in]: taskIds },
          },
        }
      );
    } catch (err) {
      throw Error(err);
    }
  }
  public async getWorkflowTasksByQuery(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.like]: params.taskstatus,
          },
       assigned_to: sequelize.where(
          sequelize.fn("LOWER", sequelize.col("assigned_to")),
          "LIKE",
          `%${params.assigned_to.toLowerCase()}%`
        ),
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: params.outcome,
              },
            },
          },
        ],
        order: [
          ["due_date", "DESC"],
          ["updated_at", "DESC"],
        ],
      });
      return result;
    } catch (err) {
      //TODO:check
      throw Error(err);
    }
  }

  public async getWorkflowTasksByStatusAndOutCome(
    params: any
  ): Promise<WorkflowTaskModel[]> {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.like]: params.taskstatus,
          },
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: params.outcome,
              },
            },
          },
        ],
        order: [
          ["due_date", "DESC"],
          ["updated_at", "DESC"],
        ],
      });
      return result;
    } catch (err) {
      //TODO:check
      throw Error(err);
    }
  }

  public async getWorkflowTasksByWorkflowStatusAndTaskStatus(
    params: findWorkflowTaskDtoChanges
  ): Promise<WorkflowTaskModel[]> {
    try {
      const whereCondition: WhereOptions = {};
      if (params.due_date) {
        whereCondition.where = {
          due_date: {
            [Op.lt]: params.due_date,
          },
        };
      }

      const result = await this.taskModel.findAll({
        where: {
          ...whereCondition.where,
          status: {
            [Op.like]: params.taskstatus,
          },
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.like]: params.outcome,
              },
            },
          },
        ],
      });
      return result;
    } catch (error) {}
  }
  addWorkingDays = (date: Date, noOfDays: number) => {
    for (let i = 0; i < noOfDays; ) {
      date.setDate(date.getDate() + 1);
      if (date.getDay() !== 0 && date.getDay() !== 6) {
        i++;
      }
    }
    return date;
  };
  getDueDateForResourceReviewer(reviewerConfigData: any) {
    const configValue = JSON.parse(reviewerConfigData.attributeValue);
    const noOfDays = parseInt(configValue, 10);
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }
  getDueDateForEmployee(employeeConfigData: any) {
    //TODO: - CHANGES TO BE MADE ACCORDING TO CONFIGURATION SCREEN
    const configValue = JSON.parse(employeeConfigData.attributeValue);
    const noOfDays = parseInt(configValue, 10); //changed
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }
  getDueDateForEmployeeWithdrawl(employeeWithdrawlConfigData: any) {
    //TODO: - CHANGES TO BE MADE ACCORDING TO CONFIGURATION SCREEN
    const configValue = JSON.parse(employeeWithdrawlConfigData.attributeValue);
    const noOfDays = parseInt(configValue, 10);
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }
  getDueDateForResourceRequestorAction(resourceRequestorConfigData: any) {
    //TODO: - CHANGES TO BE MADE ACCORDING TO CONFIGURATION SCREEN
    const configValue = JSON.parse(resourceRequestorConfigData.attributeValue);
    const noOfDays = parseInt(configValue, 10);
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }
  getDueDateForCSPAction(cspConfigData: any) {
    //TODO: - CHANGES TO BE MADE ACCORDING TO CONFIGURATION SCREEN
    const configValue = JSON.parse(cspConfigData.attributeValue);
    const noOfDays = parseInt(configValue, 10);
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }

  getDueDateForSkillDueAssetment(configData: any) {
    //TODO: - CHANGES TO BE MADE ACCORDING TO CONFIGURATION SCREEN
    const configValue = JSON.parse(configData.attributeValue);
    const noOfDays = parseInt(configValue, 10);
    const currentDate = new Date();
    const resultDate = this.addWorkingDays(currentDate, noOfDays);
    resultDate.setHours(0, 0, 0, 0);
    // currentDate.setDate(currentDate.getDate() + noOfDays);
    return resultDate;
  }
  generateWorkFlowTaskDetails(
    params: CreateWorkflow,
    step: EWorkflowStep | EEngagementStatus,
    user?: IUser
  ): CreateWorkflowTaskDto {
    try {
      const taskDetails: CreateWorkflowTaskDto = {
        status: WorkflowStatus.PENDING,
        created_by: params.created_by,
        updated_by: params.created_by,
      };
      const currentDateTime = new Date();
      const sCase = step;
      console.log(sCase, " --- Generate task s case");
      this.logger.debug(sCase + " --- Generate task case", LOG_CONTEXT);
      switch (sCase) {
        // -----> NEW ADDED <-----
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL:
          taskDetails.description = "Reviewer Approval Task";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_REVIEWER_APPROVAL;
          taskDetails.assigned_to = params.assigned_to;
          const reviewerDueDate = this.getDueDateForResourceReviewer(
            params.configurations.reviewerconfiguration
          );
          // taskDetails.due_date = reviewerDueDate;
          //TODO: - to be changed
          taskDetails.due_date = reviewerDueDate;
          break;
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL:
          taskDetails.description = "Supercoach Approval Task";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL;
          taskDetails.assigned_to = params.assigned_to;
          taskDetails.assigned_to_json = params.assigned_to_json; //change
          const supercoachDueDate = this.getDueDateForResourceReviewer(
            params.configurations.supercoachconfiguration
          );
          // taskDetails.due_date = reviewerDueDate;
          //TODO: - to be changed
          taskDetails.due_date = supercoachDueDate;
          break;

        case EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL:
          taskDetails.description = "Employee Approval Task";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL;
          taskDetails.assigned_to = params.assigned_to; //check
          const employeeDueDate = this.getDueDateForEmployee(
            params.configurations.employeeconfiguration
          );
          //TODO:- to be changed
          taskDetails.due_date = employeeDueDate;
          break;
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL_OVER_EMPLOYEE_REJECTION:
          taskDetails.description = "Employee Withdrawl Task";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL;
          taskDetails.assigned_to = params.assigned_to;
          //TODO: - WIP
          const employeeWithdrawlDueDate = this.getDueDateForEmployeeWithdrawl(
            params.configurations.employeeWithdrawlConfiguration
          );
          //TODO:- to be changed
          taskDetails.due_date = employeeWithdrawlDueDate;
          break;
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_APPROVAL_ON_EMPLOYEE_REJECTION:
          taskDetails.description =
            "Recource Requestor Approval Task After Employee Rejection";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION;
          taskDetails.assigned_to = params.assigned_to;
          //TODO: - WIP
          const resourceRequestorDueDate =
            this.getDueDateForResourceRequestorAction(
              params.configurations.resourceRequestorConfiguration
            );
          //TODO:- to be changed
          taskDetails.due_date = resourceRequestorDueDate;
          break;
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_TERMINATION_ON_EMPLOYEE_REJECTION:
          taskDetails.description =
            "Resource Requestor Termination Task after Employee Rejection";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION;
          taskDetails.assigned_to = params.assigned_to;
          break;
        case EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION:
          taskDetails.description =
            "Supercoach Approval Task After Resource Request Rejects Employee Rejection";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_EMPLOYEE_ALLOCATION_SUPERCOACH_AFTER_RESOURCE_REQUESTOR_REJECTION;
          taskDetails.assigned_to = params.assigned_to;
          taskDetails.assigned_to_json = params.assigned_to_json;

          //TODO: - WIP

          break;
        case EWorkflowSteps.USER_SKILL_ASSESSMENT_PENDING_FOR_SUPERCOACH:
          taskDetails.description =
            "User Skill Assessment Request Pending for Super coach ";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_SUPERCOACH_;
          taskDetails.assigned_to = params.assigned_to;
          taskDetails.assigned_to_json = params.assigned_to_json;
          taskDetails.due_date = this.getDueDateForSkillDueAssetment(
            params.configurations.skilDueDateconfiguration
          );
          taskDetails.comment = getUserSkillWorkflowComment(
            "",
            params.comments ? params.comments : "",
            user.email,
            new Date(),
            user.name
          );
          // taskDetails.comment = params.comments ? params.comments : "";
          break;
        case EWorkflowSteps.USER_SKILL_ASSESSMENT_PENDING_FOR_CO_SUPERCOACH:
          taskDetails.description =
            "User Skill Assessment Request Pending for Co Super coach ";
          taskDetails.type = EWorkflowTaskAssignedType.USER;
          taskDetails.title =
            WorkFlowTaskTitle.WF_TASK_TITLE_SKILL_ASSESSMENT_FOR_CO_SUPERCOACH_;
          taskDetails.assigned_to = params.assigned_to;
          taskDetails.comment = getUserSkillWorkflowComment(
            "",
            params.comments ? params.comments : "",
            user.email,
            new Date(),
            user.name
          );
          break;
        default:
          throw new BadRequestException("No case matched for task");
      }

      return taskDetails;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async employeeAllocationTermiation(user: IUser, currentDate: Date) {
    try {
      const result = await this.taskModel.findAll({
        where: {
          status: {
            [Op.iLike]: WorkflowStatus.REJECT,
          },
          title: {
            [Op.iLike]:
              WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL,
          },
          due_date: {
            [Op.lt]: currentDate,
          },
        },
        include: [
          {
            model: WorkflowModel,
            where: {
              outcome: {
                [Op.iLike]: WorkflowOutCome.INPROGRESS,
              },
              module: {
                [Op.iLike]: WorklFlowModule.EMPLOYEE_ALLOCATION,
              },
              status: {
                [Op.iLike]:
                  EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE,
              },
            },
          },
        ],
      });
      if (result && result.length > 0) {
        //TODO: 1. Update and Testing(of current functionalty)
        //TODO: 1. Updation of Due date(of current functionalty)
        result.forEach((item) => {
          const id = item.workflow_id;
          const data = {
            outcome: WorkflowOutCome.TERMINATE,
          } as WorkflowModule;
          this.model.update(data, {
            where: {
              id,
            },
          });
        });
      }
    } catch (error) {
      throw new Error(error);
    }
  }
  // NOT_IN_USE
  // async getDueDateForRevieweInEmployeeAllocation(user: IUser) {
  //   try {
  //     const result =
  //       await this.configurationService.getConfigurationByExpertiesNameAndGroupName(
  //         "Ndo",
  //         "Resource_allocation_review",
  //         user
  //       );
  //     console.log("Got result: " + result);
  //   } catch (error) {
  //     throw new Error(
  //       "Something Went Wrong In Fetching Configurations Data " + error
  //     );
  //   }
  //   return "result";
  // }
  async addWorkflow(
    params: CreateWorkflow,
    user: IUser,
    getTask = false,
    preTxn?: Transaction
  ): Promise<WorkflowModel> {
    const txn = preTxn ? preTxn : await this.model.sequelize.transaction();
    try {
      const { next_step, ...args } =
        await this.workflowHelperService.getWorkflowDetails(params, user);
      const findExistingOpen = await this.model.findOne({
        where: {
          item_id: params.item_id,
          outcome: WorkflowOutCome.INPROGRESS,
        },
        include: {
          model: WorkflowTaskModel,
        },
      });

      if (findExistingOpen) {
        if (
          findExistingOpen.module.toLowerCase().trim() ===
            WorklFlowModule.EMPLOYEE_ALLOCATION.toLocaleLowerCase().trim() &&
          findExistingOpen.sub_module.toLowerCase().trim() ===
            WorklFlowSubModule.EMPLOYEE_ALLOCATION.toLowerCase().trim() &&
          findExistingOpen.status.toLowerCase() ===
            EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE.toLowerCase()
        ) {
          //update the workflow
          const updateExistingOpen = findExistingOpen.dataValues;
          updateExistingOpen["status"] =
            EEngagementStatus.EMPLOYEE_ALLOCATION_TERMINATION_BY_RR;
          updateExistingOpen["outcome"] = WorkflowOutCome.TERMINATE;
          updateExistingOpen["updated_by"] = user.email;
          const { id, ...rest } = updateExistingOpen;
          const updatedResult = await this.model.update(rest, {
            where: {
              id,
            },
            transaction: txn,
          });
          //TODO:- to be removed
          const findExistingOpenTask = await this.taskModel.findOne({
            where: {
              workflow_id: id,
              title:
                WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL,
            },
          });
          if (!findExistingOpenTask) {
            throw new Error("No such Task found");
          }
          if (
            findExistingOpenTask &&
            findExistingOpenTask.status.toLowerCase() !==
              WorkflowStatus.REJECT.toLowerCase()
          ) {
            throw new Error("Task Does not exists in pending state");
          }
          const updatedExistingOpenTask = findExistingOpenTask.dataValues;
          updatedExistingOpenTask["status"] = WorkflowStatus.TERMINATED;
          updatedExistingOpenTask["updated_by"] = user.email;
          const { id: taskId, ...taskRest } = updatedExistingOpenTask;
          const updatedResult2 = await this.taskModel.update(taskRest, {
            where: {
              id: taskId,
            },
            transaction: txn,
          });
        } else {
          this.logger.debug("Workflow Already exists", LOG_CONTEXT);
          throw new ConflictException("Already exists");
        }
      }

      let addedTask;

      // Worklfow Model
      const workFlow = await this.model.create(
        { ...args },
        {
          transaction: txn,
        }
      );

      if (workFlow && Object.keys(workFlow).length > 0) {
        const workflowTask = this.generateWorkFlowTaskDetails(
          args,
          next_step,
          user
        );
        workflowTask.workflow_id = workFlow.id;
        addedTask = await this.addWorkflowTask(workflowTask, txn);

        if (!preTxn) await txn.commit();
      } else {
        this.logger.debug("Workflow insertion failed", LOG_CONTEXT);
        throw new BadRequestException(
          "Failed to Insert The data into the Workflow Table"
        );
      }

      return getTask
        ? addedTask
        : await this.model.findOne({
            where: { id: workFlow.id },
            include: {
              model: WorkflowTaskModel,
              // where: { id: addedTask.id },
            },
          });
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      await txn.rollback();
      throw new InternalServerErrorException(err);
    }
  }

  async addWorkflowTask(
    params: CreateWorkflowTaskDto,
    preTxn?: Transaction
  ): Promise<WorkflowTaskModel> {
    let txn = preTxn;
    try {
      if (!preTxn) {
        txn = await this.taskModel.sequelize.transaction();
      }

      const taskAdded = await this.taskModel.create(
        { ...params },
        {
          transaction: txn,
        }
      );

      if (!preTxn) {
        await txn.commit();
      }

      return taskAdded;
    } catch (err) {
      if (!preTxn) {
        await txn.rollback();
      }
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }

  public async findAllWorkflow(query: findWorkflowDto, select = []) {
    try {
      const finalQuery: WhereOptions = {
        where: { ...query },
        // raw: true,
        nest: true,
      };

      if (select.length) {
        finalQuery.select = select;
      }
      console.log("finalQuery====>", finalQuery);
      return await this.model.findOne(finalQuery);
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }

  public async findAllWorkflowArray(
    query: findWorkflowDto,
    select = []
  ): Promise<WorkflowModel[]> {
    try {
      const finalQuery: WhereOptions = {
        where: { ...query },
        // raw: true,
        nest: true,
      };

      if (select.length) {
        finalQuery.select = select;
      }
      return await this.model.findAll(finalQuery);
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }

  public async findAllWorkflowTask(query: Record<string, any>, select = []) {
    try {
      const finalQuery: WhereOptions = {
        where: { ...query },
        // raw: true,
        nest: true,
      };

      if (select.length) {
        finalQuery.select = select;
      }
      return await this.taskModel.findOne(finalQuery);
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }

  async addNextTaskOrWorkflow(
    params: UpdateWorkflow & UpdateFileWorkflow,
    // txn: Transaction,
    user: IUser
  ): Promise<any> {
    try {
      const sCase = `${params.status}_${params.next_step}`;
      let nextTask;
      let task;
      //On the basis of status OF WORKFLOW_TASK and next_step new workflow_task or workflow is added
      switch (sCase) {
        // -----> NEW ADDED <-----
        case `${WorkflowStatus.APPROVED}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL}`:
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              created_by: params.updated_by,
              assigned_to: params?.assigned_to?.toLowerCase()?.trim(),
              assigned_to_json: params.assigned_to_json,
              configurations: params.configuration,
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          break;
        case `${WorkflowStatus.APPROVED}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL}`:
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              created_by: params.updated_by,
              assigned_to: params?.assigned_to?.toLowerCase()?.trim(),
              assigned_to_json: params.assigned_to_json,
              configurations: params.configuration,
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          break;
        case `${WorkflowStatus.REJECT}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL_OVER_EMPLOYEE_REJECTION}`:
          //TODO: Add following:- not comming to this as of now
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              created_by: params.updated_by,
              assigned_to: params?.assigned_to?.toLowerCase()?.trim(),
              assigned_to_json: params.assigned_to_json,
              configurations: params.configuration,
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          // add a new task for employee for revering his/her decision -> Withdraw //assign to -> employee
          // addWorkflowTask()
          // add a new task for resource_requestor --> to accept or reject employee rejection // -> conditionally configuration// assign to->resource requestor
          //3. add a new task for resource_requestor to create a new allocation//assign  to  -> resource requestor
          break;
        case `${WorkflowStatus.REJECT}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_APPROVAL_ON_EMPLOYEE_REJECTION}`:
          //TODO: Add A new Task For Your Work:-
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              created_by: params.updated_by,
              configurations: params.configuration,
              assigned_to_json: params.assigned_to_json,
              // assigned_to: params.assigned_to,
              // TODO: - Uncomment and change
              assigned_to: params?.assigned_to_meta?.resourceRequestorEmails
                ?.toLowerCase()
                ?.trim(), //changes required for assignType
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          //Task for resource requestor
          break;
        case `${WorkflowStatus.REJECT}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_TERMINATION_ON_EMPLOYEE_REJECTION}`:
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              created_by: params.updated_by,
              configurations: params.configuration,
              assigned_to_json: params.assigned_to_json,
              assigned_to: params?.assigned_to_meta?.resourceRequestorEmails
                ?.toLowerCase()
                ?.trim(),
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          break;
        case `${WorkflowStatus.REJECT}_${EWorkflowSteps.EMPLOYEE_ALLOCATION_SUPERCOACH_APPROVAL_ON_RESOURCE_REQUESTOR_REJECTION}`:
          nextTask = this.generateWorkFlowTaskDetails(
            {
              name: "",
              module: WorklFlowModule.EMPLOYEE_ALLOCATION,
              sub_module: WorklFlowSubModule.EMPLOYEE_ALLOCATION,
              assigned_to_json: params.assigned_to_json,
              assigned_to: params?.assigned_to?.toLowerCase()?.trim(),
              created_by: params.updated_by,
              configurations: params.configuration,
            } as CreateWorkflow,
            params.next_step
          );
          nextTask.workflow_id = params.workflow_id;
          // task = await this.addWorkflowTask(nextTask, txn);
          //remaining till now
          break;
        default:
          return;
      }

      return nextTask;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  async updatePreviousTasks(
    params: UpdateWorkflow & UpdateFileWorkflow,
    user: IUser
    // txn: Transaction
  ) {
    try {
      let result = null;
      const sCase = `${params.workflow_task_title}`.toLowerCase();
      switch (sCase) {
        // case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION.toLowerCase():
        //   const getWorkflowTaskOfEmployeeWithdrawl =
        //     await this.findAllWorkflowTask({
        //       workflow_id: params.workflow_id, // inProgressWorkflow.id,
        //       title:
        //         WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_WITHDRAWL,
        //     });
        //   if (!getWorkflowTaskOfEmployeeWithdrawl) {
        //     this.logger.debug(
        //       "Unable to find WorkflowTask of Employee Withdrawl"
        //     );
        //     throw new Error(
        //       "Unable to find WorkflowTask of Employee Withdrawl"
        //     );
        //   }
        //   if (
        //     getWorkflowTaskOfEmployeeWithdrawl.status.toLowerCase() !==
        //     WorkflowStatus.PENDING
        //   ) {
        //     this.logger.error(
        //       "Action is Already taken over Employee Withdrawl"
        //     );
        //     throw new Error("Action is Already taken over Employee Withdrawl");
        //   }
        //   const updatedWorkflowTaskOfEmployeeWithdrawl =
        //     getWorkflowTaskOfEmployeeWithdrawl.dataValues;
        //   updatedWorkflowTaskOfEmployeeWithdrawl["status"] =
        //     WorkflowStatus.TERMINATED; //doubt
        //   updatedWorkflowTaskOfEmployeeWithdrawl["updated_by"] =
        //     params.updated_by || "System";
        //   const { id, ...rest } = updatedWorkflowTaskOfEmployeeWithdrawl;
        //   await this.taskModel.update(rest, {
        //     where: {
        //       id,
        //     },
        //     transaction: txn,
        //   });
        //   result = await this.taskModel.findOne({
        //     where: { id },
        //   });
        //   break;
        case WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL.toLowerCase():
          let getWorkflowTaskOfResourceRequestor =
            await this.findAllWorkflowTask({
              workflow_id: params.workflow_id, // inProgressWorkflow.id,
              title:
                WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION,
            });
          if (getWorkflowTaskOfResourceRequestor == null) {
            getWorkflowTaskOfResourceRequestor = await this.findAllWorkflowTask(
              {
                workflow_id: params.workflow_id, // inProgressWorkflow.id,
                title:
                  WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_AFTER_EMPLOYEE_REJECTION_FOR_TERMINATION,
              }
            );
          }
          if (
            getWorkflowTaskOfResourceRequestor &&
            getWorkflowTaskOfResourceRequestor.status.trim().toLowerCase() !==
              WorkflowStatus.PENDING.trim().toLowerCase()
          ) {
            throw new BadRequestException(
              "Action Already Taken Up By Resource Requestor"
            );
          }
          if (
            getWorkflowTaskOfResourceRequestor &&
            getWorkflowTaskOfResourceRequestor.status.trim().toLowerCase() ===
              WorkflowStatus.PENDING.toLowerCase()
          ) {
            const updatedWorkflowTaskOfResourceRequestor =
              getWorkflowTaskOfResourceRequestor.dataValues;
            updatedWorkflowTaskOfResourceRequestor["status"] =
              WorkflowStatus.TERMINATED;
            // updatedTask = await this.repository.updateWorkflowtask(
            //   updatedWorkflowTaskOfResourceRequestor,
            //   user
            // );
            updatedWorkflowTaskOfResourceRequestor["updated_by"] =
              params.updated_by || "System";
            const { id, ...rest } = updatedWorkflowTaskOfResourceRequestor;
            await this.taskModel.update(rest, {
              where: {
                id,
              },
              // transaction: txn,
            });
            result = await this.taskModel.findOne({
              where: { id },
            });
          }
          if (
            params.status.trim().toLowerCase() ===
            WorkflowStatus.REJECT.toLowerCase().trim()
          ) {
            //TODO: due_date update concept
            // const dueDateToUpdate = {
            //   due_date: new Date(2023, 0, 1),
            // } as unknown as WorkflowTaskModel;
            // this.taskModel.update(dueDateToUpdate, {
            //   where: {
            //     id: params.workflow_task_id,
            //   },
            //   transaction: txn,
            // });
          }
          break;
        default:
          break;
      }
      return result;
    } catch (error) {
      // await txn.rollback();
      throw new Error("Error in Updation of previous tasks " + error);
    }
  }

  async bulkCreateOrUpdate(params: InsertAndUpdateItemDTO[]) {
    let workflowTasks: WorkflowTaskModel[] = [];
    const txn = await this.model.sequelize.transaction();
    try {
      const updateworkflowTasks = params
        .filter(
          (item) =>
            item.updateWorkflowTaskDto !== undefined &&
            Object.keys(item.updateWorkflowTaskDto).length > 0
        )
        .map((item) => {
          return {
            id: item.updateWorkflowTaskId,
            status: item.updateWorkflowTaskDto.status,
            updated_by: item.updateWorkflowTaskDto.updated_by,
            comment: item.updateWorkflowTaskDto.comment,
            created_at: new Date().toISOString(),
            title: "",
            workflow_id: "",
            assigned_to: "",
            type: "",
            created_by: "",
            description: "",
          };
        });

      if (updateworkflowTasks.length > 0) {
        const bulkUpdateworkflowTask = await this.taskModel.bulkCreate(
          updateworkflowTasks,
          {
            updateOnDuplicate: ["comment", "updated_by", "status"],
            transaction: txn,
          }
        );
        workflowTasks = [...bulkUpdateworkflowTask];
      }

      const bulkAddNewWorkflowTask = params
        .filter(
          (item) =>
            item.createWorkflowTaskDto !== undefined &&
            Object.keys(item.createWorkflowTaskDto).length > 0
        )
        .map((item) => {
          return {
            ...item.createWorkflowTaskDto,
          };
        });

      if (bulkAddNewWorkflowTask.length > 0) {
        const addNewWorkflowTask = await this.taskModel.bulkCreate(
          bulkAddNewWorkflowTask,
          {
            transaction: txn,
          }
        );
        workflowTasks = workflowTasks.concat(addNewWorkflowTask);
      }
      const bulkUpdateWorkflow = params
        .filter(
          (item) =>
            item.updateWorkflowDto !== undefined &&
            Object.keys(item.updateWorkflowDto).length > 0
        )
        .map((item, idx) => {
          return {
            id: item.updateWorkflowId,
            outcome: item.updateWorkflowDto.outcome,
            updated_by: item.updateWorkflowDto.updated_by,
            status: item.updateWorkflowDto.status,
            name: "",
            module: "", // Required field
            sub_module: "", // Required field
            item_id: `${idx}`, // Required field
            created_by: "",
          };
        });

      if (bulkUpdateWorkflow.length > 0) {
        const updatedWorkflow = await this.model.bulkCreate(
          bulkUpdateWorkflow,
          {
            updateOnDuplicate: ["outcome", "updated_by", "status"],
            conflictAttributes: ["id"],
            transaction: txn,
          }
        );
      }
      await txn.commit();
      return workflowTasks;
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      await txn.rollback();
      throw new InternalServerErrorException(err);
    }
  }

  async updateApproval(
    params: UpdateWorkflow & UpdateFileWorkflow,
    user: IUser
  ): Promise<InsertAndUpdateItemDTO> {
    // const txn = await this.model.sequelize.transaction();

    try {
      const {
        next_step,
        workflow_id,
        workflow_table_outcome,
        workflow_table_status,
        proxy_approval_by,
        ...rest
      } = params;
      const workflowTaskDataToBe = {
        status: rest.status,
        updated_by: rest.updated_by || "System",
        comment: rest.comment || "",
      } as UpdateWorkflow;
      const finalResponse: InsertAndUpdateItemDTO = {
        updateWorkflowTaskDto: new UpdateWorkflow(),
        updateWorkflowTaskId: "",
        updateWorkflowId: "",
        createWorkflowTaskDto: new CreateWorkflowTaskDto(),
      };
      if (proxy_approval_by) {
        workflowTaskDataToBe.proxy_approval_by = proxy_approval_by;
      }

      console.log("workflowTaskDataToBe====>", workflowTaskDataToBe);
      //QUERY TYPE
      const taskModelQuery: WhereOptions = {
        id: { [Op.in]: [params.id] },
      };
      //UPDATING FEW PROPS OF CRRENT WORKFLOW TAST WITHOUT MAKING ANY CHANGE
      // const result = await this.taskModel.update(workflowTaskDataToBe, {
      //   where: {
      //     // status: WorkflowStatus.PENDING,
      //     id: params.id,
      //   },
      //   transaction: txn,
      // });
      finalResponse.updateWorkflowTaskId = params.id;
      finalResponse.updateWorkflowTaskDto = workflowTaskDataToBe;
      const newValue = await this.taskModel.findOne({
        where: {
          id: params.id,
        },
      });

      // if (result && result[0] > 0) {
      /** Adding COE Admin Task in case of BSPOC Approval */
      //ADDING NEW WORKFLOW
      //PARAMS -> WANT WE GET AS AN INPUT
      //USER -> CURRENT USER USER
      //TODO:- To Check Not In Use
      const prevUpdated: any = await this.updatePreviousTasks(
        params,
        user
        // txn
      );
      if (prevUpdated && prevUpdated !== null) {
        taskModelQuery.id[Op.in].push(prevUpdated.id);
      }
      const taskDto = await this.addNextTaskOrWorkflow(params, user);
      finalResponse.createWorkflowTaskDto = taskDto;
      // UNKNOWN
      // if (task) {
      //   taskModelQuery.id[Op.in].push(task.id);
      // }
      if (
        params.workflow_task_title ===
          WorkFlowTaskTitle.WF_TASK_TITLE_EMPLOYEE_ALLOCATION_EMPLOYEE_APPROVAL &&
        params.status === WorkflowStatus.REJECT
        // params.configuration. //configuration service check
      ) {
        console.log(params.configuration.resourceRequestorConfiguration);
        const resourceRequestorConfigValue = parseInt(
          params.configuration.resourceRequestorConfiguration.attributeValue
        );
        const isRR = resourceRequestorConfigValue === -1 ? false : true;
        //TODO:- change the status resourceRequestorConfigValue.activationStatus !== "true" isRR
        // if (isRR) {
        params.next_step = isRR
          ? EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_APPROVAL_ON_EMPLOYEE_REJECTION
          : EWorkflowSteps.EMPLOYEE_ALLOCATION_RESOURCE_REQUESTOR_TERMINATION_ON_EMPLOYEE_REJECTION;
        const taskDto = await this.addNextTaskOrWorkflow(params, user);
        finalResponse.createWorkflowTaskDto = taskDto;
        // workflow_table_status =
        //   EEngagementStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR;
        // if (task) {
        //   taskModelQuery.id[Op.in].push(task.id);
        // }
        // }
      }
      /** END */
      //UPDATING WORKFLOW
      const workflowDataToBe = {
        status: workflow_table_status,
        updated_by: rest.updated_by,
      } as WorkflowModel;

      if (workflow_table_outcome) {
        workflowDataToBe.outcome = workflow_table_outcome as WorkflowOutCome;
      }
      finalResponse.updateWorkflowDto = workflowDataToBe;
      finalResponse.updateWorkflowId = workflow_id;
      // await this.model.update(workflowDataToBe, {
      //   where: {
      //     id: workflow_id,
      //   },
      //   transaction: txn,
      // });

      // await txn.commit();
      return finalResponse;
      // return await this.taskModel.findAll({
      //   where: taskModelQuery,
      // });
      // } else {
      //   throw new NotFoundException("No Approval pending to update");
      // }
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      // await txn.rollback();
      throw new InternalServerErrorException();
    }
  }

  public async addKpiExecutionWorkflow(
    params: AddKpiExecutionWorkflow,
    user: IUser
  ): Promise<WorkflowModel[]> {
    try {
      const workflows = await this.model.findAll({
        where: {
          item_id: { [Op.in]: params.item_id },
          parent_id: params.engagement_id,
          sub_module: WorklFlowSubModule.KPI_EXECUTION_WORKFLOW,
          outcome: WorkflowOutCome.INPROGRESS,
        },
      });

      const itemIdObject = workflows.reduce((t, v) => {
        t[v.item_id] = v.item_id;
        return t;
      }, {});

      const notAddedWorkflow = params.item_id.filter((wf) => !itemIdObject[wf]);

      if (notAddedWorkflow.length) {
        this.logger.debug("Adding KPI execution workflow", LOG_CONTEXT);

        const addedWorkflows = await Promise.all(
          params.item_id.map((itemid) => {
            return this.addWorkflow(
              {
                item_id: itemid,
                parent_id: params.engagement_id,
                name: params.name,
                status: params.status,
                module: WorklFlowModule.KPI,
                sub_module: WorklFlowSubModule.KPI_EXECUTION_WORKFLOW,
              } as CreateWorkflow,
              user
            );
          })
        );

        return addedWorkflows;
      } else {
        this.logger.debug("No Workflowfound to add", LOG_CONTEXT);
        return [];
      }
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }

  public async updateWorkflow(params: {
    id: string;
    status: string;
    outcome?: string;
    updated_by: string;
  }): Promise<WorkflowModel> {
    try {
      const { id, ...rest } = params;
      const update = await this.model.update(rest, {
        where: {
          id,
        },
      });

      return await this.model.findOne({
        where: { id },
      });
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }
  public async updateWorkflowtask(
    params: UpdateWorkflow,
    user: IUser,
    preTxn?: Transaction
  ): Promise<WorkflowTaskModel> {
    let txn = preTxn;
    try {
      if (!preTxn) {
        txn = await this.taskModel.sequelize.transaction();
      }
      const { id, ...rest } = params;
      rest.updated_by = user.email;
      const update = await this.taskModel.update(rest, {
        where: {
          id,
        },
      });

      return await this.taskModel.findOne({
        where: { id },
      });
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);

      throw new InternalServerErrorException();
    }
  }

  async getKpiReportWorkflow(query: GetKpiReportWorkflowDTO): Promise<any> {
    try {
      return await this.model.findAll({
        where: {
          item_id: query.item_id,
          parent_id: query.parent_id,
          status: {
            [Op.in]: [
              EEngagementStatus.KPI_EXECUTION_CONFIRMED_ET,
              EEngagementStatus.KPI_EXECUTION_DROPPED,
            ],
          },
        },
        // attributes: ['updated_at', 'item_id', 'parent_id'],
        order: [["updated_at", "ASC"]],
      });
    } catch (error) {
      throw error;
    }
  }
  public async getAuditWorkflow(
    params: WorkFlowAuditDTO
  ): Promise<WorkflowModel[]> {
    try {
      const workflowData = await this.model.findAll({
        where: { ...params },
        include: {
          model: WorkflowHistoryModel,
        },
      });
      const parent_id = params.item_id;
      const kpiworkflowData = await this.model.findAll({
        where: { parent_id: parent_id },
        include: {
          model: WorkflowHistoryModel,
        },
      });
      const workFlowAudit = [...workflowData, ...kpiworkflowData];
      return workFlowAudit;
    } catch (err) {
      throw new InternalServerErrorException();
    }
  }

  public async getCommentsByItemId(
    query: GetCommentsByItemIdRequest
  ): Promise<any> {
    try {
      if (query.itemId && query.itemId.length > 0) {
        return await this.model.findAll({
          attributes: ["item_id"],
          where: {
            item_id: {
              [Op.in]: Array.isArray(query.itemId)
                ? query.itemId
                : [query.itemId],
            },
            is_active: true,
          },
          include: {
            model: this.taskModel,
            attributes: ["comment"],
            // attributes: ["comment", "updated_by", "updated_at"],
            where: {
              [Op.and]: [
                { comment: { [Op.not]: null } }, // Comments is not null
                literal("TRIM(comment) != ''"), // Comments is not empty
              ],
            },
          },
        });
      } else {
        return [];
      }
    } catch (error) {
      throw error;
    }
  }

  public async getMySkillTasksAssigned(user: IUser): Promise<any> {
    try {
      return await this.model.findAll({
        where: {
          is_active: true,
          outcome: WorkflowOutCome.INPROGRESS,
          module: {
            [Op.iLike]: WorklFlowModule.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT,
          },
          sub_module: {
            [Op.iLike]:
              WorklFlowSubModule.WORKFLOW_SUB_MODULE_USER_SKILL_ASSESSMENT,
          },
        },
        order: [["updated_at", "DESC"]],
        include: {
          model: this.taskModel,
          where: {
            assigned_to: {
              [Op.substring]: user?.email?.toLowerCase(),
            },
            status: WorkflowStatus.PENDING,
          },
        },
      });
    } catch (error) {
      throw error;
    }
  }
  public async getMultipleTasksByByQuery(
    params: getMultipleTasksByByQueryDto
  ): Promise<WorkflowModel[]> {
    try {
      return await this.model.findAll({
        where: { ...params },
        include: {
          model: this.taskModel,
        },
      });
    } catch (err) {
      throw new InternalServerErrorException(err);
    }
  }
}
