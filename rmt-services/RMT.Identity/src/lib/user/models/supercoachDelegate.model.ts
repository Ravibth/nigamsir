import { DataTypes } from 'sequelize';
import { Column, ForeignKey, Model, Table } from 'sequelize-typescript';
import { ModalConfigs } from 'src/common/secretManager/secretManager';
import { UserModel } from './users.model';

const tableName = 'SUPERCOACH_DELEGATES';
@Table({ tableName, ...ModalConfigs })
export class SupercoachDelegateModel extends Model {
  @Column({ type: DataTypes.UUID, primaryKey: true, allowNull: false })
  id: string;
  @ForeignKey(() => UserModel)
  @Column({ type: DataTypes.STRING, allowNull: false })
  supercoach_mid: string;
  @Column({ type: DataTypes.STRING, allowNull: true })
  allocation_delegate_name: string;
  @Column({ type: DataTypes.STRING, allowNull: true })
  allocation_delegate_mid: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  allocation_delegate_email: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  skill_delegate_name: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  skill_delegate_mid: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  skill_delegate_email: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  created_by: string;

  @Column({ type: DataTypes.STRING, allowNull: true })
  updated_by: string;

  @Column({ type: DataTypes.DATE, allowNull: false })
  created_at: Date;

  @Column({ type: DataTypes.DATE, allowNull: true })
  updated_at: Date;
}
