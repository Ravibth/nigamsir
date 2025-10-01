import { DataTypes } from 'sequelize';
import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
  HasMany,
} from 'sequelize-typescript';
import { ModulePermission } from './modulePermission.model';

@Table({ tableName: 'MODULE' })
export class Modules extends Model {
  @Column({
    type: DataTypes.INTEGER,
    autoIncrement: true,
    unique: true,
    primaryKey: true,
  })
  id: number;

  @Column({ type: DataTypes.STRING, unique: true, allowNull: false })
  module_name: string;

  @Column({ type: DataTypes.STRING, allowNull: false })
  module_display: string;

  @Column({ type: DataTypes.INTEGER, allowNull: false })
  order: number;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_active: boolean;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_display: boolean;

  @Column({ type: DataTypes.INTEGER })
  parent_id: number;

  @CreatedAt
  @Column({ type: DataTypes.DATE })
  created_at: Date;

  @Column({ type: DataTypes.STRING })
  updated_by: string;

  @UpdatedAt
  @Column({ type: DataTypes.DATE })
  updated_at: Date;

  @HasMany(() => ModulePermission)
  module_permissions: ModulePermission[];
}
