import { DataTypes } from "sequelize";
import {
  Column,
  BelongsTo,
  CreatedAt,
  DataType,
  ForeignKey,
  Model,
  Table,
  UpdatedAt,
} from "sequelize-typescript";
import { EWorkflowTaskAssignedType, WorkflowStatus } from "../../workflow/enum";
import { WorkflowModel } from "../../workflow/models/workflow.model";

@Table({ tableName: "WORKFLOW_TASK" })
export class WorkflowTaskModel extends Model {
  @Column({
    type: DataType.STRING,
    defaultValue: DataType.UUIDV4,
    primaryKey: true,
  })
  id?: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  assigned_to: string;

  @Column({
    type: DataType.STRING,
    allowNull: true,
  })
  assigned_to_userName?: string;
  @Column({
    type: DataType.JSON,
    allowNull: true,
  })
  assigned_to_json?: any;
  @Column({
    type: DataType.STRING,
  })
  comment: string;

  @CreatedAt
  @Column({
    type: DataType.DATE,
  })
  created_at: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  created_by: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  description: string;

  @Column({
    type: DataType.DATE,
  })
  due_date: string;

  @Column({
    type: DataType.STRING,
  })
  proxy_approval_by: string;

  @Column({ type: DataType.STRING, allowNull: false })
  status: WorkflowStatus;

  @Column({ type: DataType.STRING, allowNull: false })
  title: string;

  @Column({ type: DataType.STRING, allowNull: false })
  type: EWorkflowTaskAssignedType;

  @UpdatedAt
  updated_at?: Date;

  @Column({
    type: DataType.STRING,
  })
  updated_by: string;

  @BelongsTo(() => WorkflowModel)
  workflow: WorkflowModel;

  @ForeignKey(() => WorkflowModel)
  @Column({ type: DataTypes.STRING, allowNull: false })
  workflow_id: string;
}
