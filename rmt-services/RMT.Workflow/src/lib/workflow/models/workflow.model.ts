import {
  Column,
  CreatedAt,
  DataType,
  HasMany,
  Model,
  Table,
  UpdatedAt,
} from "sequelize-typescript";
import { WorkflowTaskModel } from "../../../lib/workflow-task/models/workflow-task.model";
import { WorkflowHistoryModel } from "../../../lib/workflowHistory/models/workflowHistory.model";

import { WorkflowOutCome, WorkflowStatus } from "../enum";

@Table({ tableName: "WORKFLOW" })
export class WorkflowModel extends Model {
  @Column({
    type: DataType.STRING,
    defaultValue: DataType.UUIDV4,
    primaryKey: true,
  })
  id?: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
    unique: "work_flow_unique",
  })
  name: string; // service line

  @Column({
    type: DataType.STRING,
    allowNull: false,
    unique: "work_flow_unique",
  })
  module: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
    unique: "work_flow_unique",
  })
  sub_module: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
    unique: "work_flow_unique",
  })
  item_id: string;

  @Column({ type: DataType.STRING, allowNull: false })
  outcome: WorkflowOutCome;

  @Column({ type: DataType.STRING, allowNull: false })
  status: WorkflowStatus;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  created_by: string;

  @CreatedAt
  created_at: Date;

  @Column({
    type: DataType.STRING,
  })
  updated_by: string;
  @Column({
    type: DataType.STRING,
    allowNull: true,
  })
  entity_type: string;
  @Column({
    type: DataType.JSON,
    allowNull: true,
  })
  entity_meta_data: any;

  @UpdatedAt
  updated_at?: Date;

  @Column({ defaultValue: true })
  is_active?: boolean;

  @HasMany(() => WorkflowTaskModel)
  task_list: WorkflowTaskModel[];

  @HasMany(() => WorkflowHistoryModel)
  history: WorkflowHistoryModel[];

  @Column({
    type: DataType.STRING,
  })
  parent_id: string;
}
