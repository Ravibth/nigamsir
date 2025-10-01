import { DataTypes } from 'sequelize';

import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
  Scopes,
} from 'sequelize-typescript';
import { RoleModel } from '../../role/models/role.model';
import { ModalConfigs } from '../../../common/secretManager/secretManager';
const tableName = 'DESIGNATION_ROLE';

@Scopes(() => ({
  role_details: {
    include: [
      {
        model: RoleModel,
        attributes: ['role_name', 'id'],
        association: DesignationRoleModel.hasOne(RoleModel, {
          constraints: false,
          foreignKey: 'id',
          as: 'role_details',
          sourceKey: 'role_id',
        }),
      },
    ],
  },
}))
@Table({ tableName, ...ModalConfigs })
export class DesignationRoleModel extends Model {
  @Column({
    type: DataTypes.NUMBER,
    unique: true,
    autoIncrement: true,
    primaryKey: true,
  })
  id: number;

  @Column({ type: DataTypes.STRING })
  designation: string;

  @Column({ type: DataTypes.INTEGER })
  role_id: number;

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

  role_details?: RoleModel;
}
