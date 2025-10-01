import { DataTypes } from 'sequelize';
import { Column, HasMany, Model, Table } from 'sequelize-typescript';
import { ModulePermission } from './modulePermission.model';

@Table({ tableName: 'PERMISSION', timestamps: false })
export class Permission extends Model {
  @Column({
    type: DataTypes.INTEGER,

    primaryKey: true,
    unique: true,
    autoIncrement: true,
  })
  id: number;

  @Column({ type: DataTypes.STRING, unique: true, allowNull: false })
  permission_name: string;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_active: boolean;

  @HasMany(() => ModulePermission)
  module_permissions: ModulePermission[];
}
