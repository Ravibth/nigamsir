import { DataTypes } from "sequelize";
import {
  Column,
  Model,
  Table,
  CreatedAt,
  ForeignKey,
} from "sequelize-typescript";
import { WorkflowModel } from "../../../lib/workflow/models/workflow.model";
import { EWorkflowHistoryAction } from "../enum";
import { DataTypes as CDType } from "../../../common/dataTypes/customDataTypes";

export interface IHistoryMeta {
  workflow_current_status: string;
}

@Table({ tableName: "WORKFLOW_HISTORY", updatedAt: false })
export class WorkflowHistoryModel extends Model {
  @Column({
    type: DataTypes.STRING,
    unique: true,
    primaryKey: true,
    defaultValue: DataTypes.UUIDV4,
  })
  id: number;

  @Column({ type: DataTypes.STRING, allowNull: false })
  action: EWorkflowHistoryAction;

  @ForeignKey(() => WorkflowModel)
  @Column({ type: DataTypes.STRING, allowNull: false })
  workflow_id: string;

  @Column({ type: DataTypes.STRING })
  workflow_task_id: string;

  @Column({ type: DataTypes.STRING(500) })
  comments: string;

  @Column({ type: DataTypes.STRING, allowNull: false })
  created_by: string;

  @CreatedAt
  created_at: string;

  @Column({ type: CDType["SNOWFLAKE_VARIANT"] })
  meta: IHistoryMeta;
}
