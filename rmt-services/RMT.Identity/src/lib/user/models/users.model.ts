import { DataTypes } from 'sequelize';
import {
  Table,
  Column,
  Model,
  CreatedAt,
  UpdatedAt,
  HasMany,
  HasOne,
} from 'sequelize-typescript';
import { ModalConfigs } from '../../../common/secretManager/secretManager';
import { UserRoleModel } from './userRole.model';
import { SupercoachDelegateModel } from './supercoachDelegate.model';
const tableName = 'USERS';

@Table({ tableName, ...ModalConfigs })
export class UserModel extends Model {
  // @Column({
  //   type: DataTypes.STRING,
  //   primaryKey: true,
  //   defaultValue: DataTypes.UUIDV4,
  // })
  // id?: string;

  @Column({ type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true })
  id: number;

  @Column({ type: DataTypes.STRING })
  role_ids: string;

  @Column({
    type: DataTypes.STRING,
    allowNull: false,
    validate: {
      isEmail: true,
    },
  })
  email_id: string;

  @Column({ type: DataTypes.STRING })
  name: string;

  @Column({ type: DataTypes.STRING })
  uemail_id: string;

  @Column
  entity?: string;

  @Column
  employee_id?: string;

  @Column({ unique: true })
  emp_code?: string;

  @Column
  fname: string;

  @Column
  lname: string;

  @Column({ type: DataTypes.STRING })
  designation: string;

  @Column({ type: DataTypes.STRING })
  grade?: string;

  @Column
  location?: string;

  @Column
  region_name?: string;

  @Column
  smeg?: string;

  @Column
  expertise?: string;

  @Column
  business_unit?: string;

  @Column
  co_supercoach_name?: string;

  @Column
  supercoach_name?: string;

  @Column({ type: DataTypes.STRING })
  service_line: string;

  @Column({ type: DataTypes.STRING })
  roles: string;

  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  status: boolean;
  // NEW ADDS
  @Column({ type: DataTypes.STRING })
  reporting_partner_mid?: string;
  @Column({ type: DataTypes.STRING })
  employee_status?: string;
  @Column({ type: DataTypes.DATE })
  employee_resignation_date?: Date;
  @Column({ type: DataTypes.DATE })
  employee_last_working_date?: Date;
  @Column({ type: DataTypes.STRING })
  supercoach_mid?: string;
  @Column({ type: DataTypes.STRING })
  co_supercoach_mid?: string;
  @Column({ type: DataTypes.BOOLEAN, defaultValue: true })
  is_active?: boolean;

  @Column({ type: DataTypes.STRING })
  created_by?: string;

  @CreatedAt
  @Column({ type: DataTypes.DATE })
  created_at?: Date;

  @Column({ type: DataTypes.STRING })
  updated_by?: string;

  @UpdatedAt
  @Column({ type: DataTypes.DATE })
  updated_at?: Date;

  @HasMany(() => UserRoleModel, {
    sourceKey: 'employee_id',
    foreignKey: 'employee_id',
  })
  role_list: UserRoleModel[];
  @HasOne(() => SupercoachDelegateModel, {
    sourceKey: 'supercoach_mid',
    foreignKey: 'supercoach_mid',
  })
  delegate_details: SupercoachDelegateModel;

  order?: number;

  @Column({ type: DataTypes.STRING })
  sub_industry?: string;

  @Column({ type: DataTypes.STRING })
  industry?: string;

  @Column({ type: DataTypes.STRING })
  competency?: string;

  @Column({ type: DataTypes.STRING })
  competencyId?: string;
}
