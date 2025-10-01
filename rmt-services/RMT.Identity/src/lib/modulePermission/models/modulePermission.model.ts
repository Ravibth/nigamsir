import { DataTypes } from 'sequelize';
import {
  BelongsTo,
  Column,
  CreatedAt,
  ForeignKey,
  Model,
  Table,
  UpdatedAt,
} from 'sequelize-typescript';
import { Modules } from './module.model';
import { Permission } from './permission.model';

@Table({ tableName: 'MODULE_PERMISSION' })
export class ModulePermission extends Model {
  @Column({ type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true })
  id: number;

  @ForeignKey(() => Modules)
  @Column({ type: DataTypes.INTEGER, allowNull: false })
  module_id: number;

  @ForeignKey(() => Permission)
  @Column({ type: DataTypes.INTEGER, allowNull: false })
  permission_id: number;

  @Column({ type: DataTypes.STRING, allowNull: false, unique: true })
  code: string;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
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

  @BelongsTo(() => Modules)
  modules: Modules;

  @BelongsTo(() => Permission)
  permission: Permission;
}
