import { DataTypes } from 'sequelize';
import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
} from 'sequelize-typescript';
import { ModalConfigs } from '../../../common/secretManager/secretManager';

const tableName = 'ROLE_MODULE_PERMISSION';

@Table({ tableName, ...ModalConfigs })
export class RoleModulePermissionModel extends Model {
  @Column({ type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true })
  id: number;

  @Column({
    type: DataTypes.STRING,
    allowNull: false,
    unique: 'role_module_permission',
  })
  role_name: number;

  @Column({
    type: DataTypes.INTEGER,
    allowNull: false,
    unique: 'role_module_permission',
  })
  module_permission_id: number;

  @Column({ type: DataTypes.BOOLEAN })
  is_active: boolean;

  @Column({ type: DataTypes.STRING })
  created_by: string;

  @CreatedAt
  @Column({ type: DataTypes.DATE })
  created_at: Date;

  @Column({ type: DataTypes.STRING })
  updated_by: string;

  @UpdatedAt
  @Column({ type: DataTypes.DATE })
  updated_at: Date;
}
