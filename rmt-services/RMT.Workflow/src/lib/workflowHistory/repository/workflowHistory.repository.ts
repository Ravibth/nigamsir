import { Injectable, InternalServerErrorException } from "@nestjs/common";
import { InjectModel } from "@nestjs/sequelize";
import { CreateWorkflowHistory } from "../dto/createHistory.dto";
import { FindWorkflowHistory } from "../dto/findHistory.dto";
import { WorkflowHistoryModel } from "../models/workflowHistory.model";
import { v4 } from "uuid";

@Injectable()
export class WorkflowHistoryRepository {
  constructor(
    @InjectModel(WorkflowHistoryModel)
    private model: typeof WorkflowHistoryModel
  ) {}

  async findAll(params: FindWorkflowHistory): Promise<WorkflowHistoryModel[]> {
    try {
      return await this.model.findAll({
        where: { ...params },
      });
    } catch (err) {
      throw new InternalServerErrorException();
    }
  }

  async addHistory(
    params: CreateWorkflowHistory[]
  ): Promise<WorkflowHistoryModel[]> {
    try {
      const transformedParams = params.reduce((obj, item) => {
        const uuid = v4();
        item.id = uuid;
        obj[uuid] = item;
        return obj;
      }, {});
      console.log(transformedParams);
      // TODO: this History is providing error due to meta

      const workflowHistories = await this.model.bulkCreate(
        Object.values(transformedParams).map(({ meta, ...item }) => ({
          ...item,
        }))
      );
      console.log(workflowHistories);
      await Promise.all(
        workflowHistories.map((history) => {
          history.set("meta", transformedParams[history.id].meta);
          return history.save();
        })
      );

      return workflowHistories;
      // return null;
    } catch (err) {
      console.log(err);
      throw new InternalServerErrorException();
    }
  }
}
