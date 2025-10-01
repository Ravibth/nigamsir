import { DataTypes } from "sequelize";
import {
  Column,
  CreatedAt,
  DataType,
  Model,
  Table,
  UpdatedAt,
} from "sequelize-typescript";

export enum EModeOfOperation {
  SELF = "SELF Execution",
  COE = "COE Support",
}

export enum EEngagementStatus {
  onboard = "Onboarded",
  business_spoc_approved = "Approved By Business SPOC",
  business_spoc_pending = "Approval Pending By Business SPOC",

  business_spoc_reject = "Rejected By Business SPOC",
  partner_reject = "Rejected By Partner",
  partner_approved = "Approved By Partner",
  partner_pending = "Approval Pending By Partner",
}

const tableName = "ENGAGEMENT";
@Table({ tableName })
export class EngagementOnboardingWorkflowModel extends Model {
  @Column({
    type: DataType.STRING,
    defaultValue: DataType.UUIDV4,
    primaryKey: true,
  })
  id: string;

  @Column({ type: DataType.STRING, allowNull: false })
  engagement_name: string;

  @Column
  status: string;

  @Column
  business_area: string;

  @Column
  data_software: string;

  @Column({ allowNull: false })
  client_id: string;

  @Column({ type: DataType.BOOLEAN, allowNull: false, defaultValue: false })
  is_new: boolean;

  @Column({ allowNull: true })
  parent_id: string;

  @Column({ type: DataType.STRING, allowNull: false })
  service_line: string;

  @Column({ type: DataType.STRING, allowNull: false })
  mode_of_operation: EModeOfOperation;

  @Column({ type: DataType.STRING, allowNull: true })
  job_code: string;

  @Column({ type: DataType.STRING, allowNull: false })
  entity_legal_name: string;

  @Column({ type: DataType.STRING, allowNull: false })
  tentative_no_files: string;

  @Column({ type: DataType.STRING, allowNull: false })
  tentative_data_size_in_gb: string;

  @Column({ type: DataType.BOOLEAN, allowNull: false, defaultValue: false })
  data_extraction_required: boolean;

  @Column
  remarks: string;

  @Column({ defaultValue: true })
  is_active: boolean;

  @Column({ type: DataTypes.DATE, allowNull: false })
  start_date: Date;

  @Column({ type: DataTypes.DATE, allowNull: false })
  end_date: Date;

  @Column({ type: DataTypes.DATE })
  commence_date: Date;

  @Column({ type: DataTypes.DATE })
  delivery_date: Date;

  @Column({ type: DataTypes.STRING, allowNull: false })
  created_by: string;

  @Column({ type: DataTypes.STRING })
  updated_by: string;

  @CreatedAt
  created_at: Date;

  @UpdatedAt
  updated_at: Date;
}
