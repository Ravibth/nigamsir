import { DataTypes } from 'sequelize';

import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
  Scopes,
} from 'sequelize-typescript';
const tableName = 'DESIGNATION_ROLE';

@Scopes(() => ({
  role_details: {
    include: [
      {
        attributes: ['role_name', 'id'],
      },
    ],
  },
}))
@Table({ tableName })
export class DesignationRoleModelTest extends Model {
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
}
