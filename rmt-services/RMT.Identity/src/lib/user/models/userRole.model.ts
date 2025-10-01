import { DataTypes } from 'sequelize';
import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
  BelongsTo,
  ForeignKey,
} from 'sequelize-typescript';
import { ModalConfigs } from '../../../common/secretManager/secretManager';
import { UserModel } from './users.model';
const tableName = 'USER_ROLE';

@Table({ tableName, ...ModalConfigs })
export class UserRoleModel extends Model {
  @Column({
    type: DataTypes.STRING,
    defaultValue: DataTypes.UUIDV4,
    primaryKey: true,
  })
  id?: string;

 
  @Column({ type: DataTypes.STRING, allowNull: false })
  user: string;

  @ForeignKey(() => UserModel)
  @Column({ type: DataTypes.STRING, allowNull: false })
  employee_id: string;

  

  @Column({ type: DataTypes.STRING, allowNull: false })
  role: string;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_active?: boolean;

  @Column({ type: DataTypes.STRING, allowNull: false })
  created_by?: string;

  @CreatedAt
  @Column({ type: DataTypes.DATE })
  created_at?: Date;

  @Column({ type: DataTypes.STRING })
  updated_by?: string;

  @UpdatedAt
  @Column({ type: DataTypes.DATE })
  updated_at?: Date;
}
