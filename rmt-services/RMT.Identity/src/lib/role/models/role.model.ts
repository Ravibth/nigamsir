import { DataTypes } from 'sequelize';
import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
} from 'sequelize-typescript';
import { ModalConfigs } from '../../../common/secretManager/secretManager';
const tableName = 'ROLE';

@Table({ tableName, ...ModalConfigs })
export class RoleModel extends Model {
  @Column({ type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true })
  id: number;

  @Column({ type: DataTypes.STRING, unique: 'role_active', allowNull: false })
  role_name: string;

  @Column({ type: DataTypes.STRING, allowNull: false })
  display: string;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_display: boolean;

  @Column({ type: DataTypes.STRING })
  description: string;

  @Column({
    type: DataTypes.BOOLEAN,
    defaultValue: true,
  })
  is_active: boolean;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: false })
  is_view_by_admin: boolean;

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
