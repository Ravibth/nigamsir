import {
  Injectable,
  InternalServerErrorException,
  NotFoundException,
  BadRequestException,
  ConflictException,
  HttpException,
  ForbiddenException,
} from '@nestjs/common';
import crypto from 'crypto';
import { v4 as uuidv4 } from 'uuid';
import { InjectModel } from '@nestjs/sequelize';
import {
  Op,
  Sequelize,
  Sequelize as sequelize,
  WhereOptions,
  fn,
  col,
  where,
} from 'sequelize';
import { IUser } from '../../../common/decorators/user.decorator';
import { FindAndUpsertUserDto } from '../dto/findAndUpsert.dto';
import { FindUserDto } from '../dto/findUserList.dto';
import { UserListDto } from '../dto/userList.dto';
import { UserModel } from '../models/users.model';
import { UpdateUserDto } from '../dto/updateUser.dto';
import { AddUserDto } from '../dto/addUser.dto';
import { Literal } from 'sequelize/types/utils';
import { UpdateUserV2Dto } from '../dto/updateUserV2.dto';
import { UserRoleModel } from '../models/userRole.model';
import { UserDetailsWCGTDto } from '../dto/userDetailWCGT.dto';
import { RolesListMaster } from 'src/common/enum';
import { EUserCommandNames, IdentityRoles } from '../user.constant';
import { RemoveUserRoleByEmailDto } from '../dto/removeUserRoleByEmailDTO.dto';
import { SupercoachDelegateModel } from '../models/supercoachDelegate.model';
import { SupercoachDelegateDto } from '../dto/supercoachDelegate.dto';
import { AddSupercoachDelegateDto } from '../dto/addSupercoachDelegate.dto';
import { SuperCoachDelegateListDto } from '../dto/supercoachDelegateList.dto';

// import Sequelize from 'sequelize/types/sequelize';

@Injectable()
export class UserRepository {
  constructor(
    @InjectModel(UserModel) private user: typeof UserModel,
    @InjectModel(UserRoleModel) private userRole: typeof UserRoleModel,
    @InjectModel(SupercoachDelegateModel)
    private supercoachDelegate: typeof SupercoachDelegateModel,
  ) {}

  public async getUsersByEmails(userEmails: string[]): Promise<UserModel[]> {
    try {
      const userEmailsLower = userEmails.map((e) => e.toLowerCase().trim());
      let users = null;
      if (typeof userEmails === 'string') {
        users = await this.user.findAll({
          where: {
            [Op.and]: [
              {
                uemail_id: {
                  [Op.iLike]:
                    typeof userEmails === 'string'
                      ? [userEmails]
                      : `%${userEmails}`,
                },
              },
              {
                status: true,
              },
            ],
          },
          include: [
            {
              model: this.supercoachDelegate,
            },
          ],
        });
      } else {
        users = await this.user.findAll({
          where: {
            [Op.and]: [
              sequelize.where(
                sequelize.fn('LOWER', sequelize.col('email_id')),
                { [Op.in]: userEmailsLower },
              ),
              {
                status: true,
              },
            ],
          },
          include: [
            {
              model: this.supercoachDelegate,
            },
          ],
        });
      }

      return users;
    } catch (ex) {
      throw ex;
    }
  }
  public async getSuperCoachAndDelegate(supercoach_mid: string) {
    try {
      const supercoachInfo = await this.supercoachDelegate.findOne({
        where: {
          supercoach_mid: {
            [Op.iLike]: `${supercoach_mid.toLowerCase().trim()}`,
          },
        },
      });
      return supercoachInfo;
    } catch (error) {
      throw error;
    }
  }
  public async GetSupercoachUserListByAllocationSupercoachDelegate(
    email: string,
  ) {
    try {
      var supercoach_delegate_list = await this.supercoachDelegate.findAll({
        where: {
          allocation_delegate_email: {
            [Op.iLike]: email,
          },
        },
      });
      var response = await this.user.findAll({
        where: where(fn('LOWER', col('employee_id')), {
          [Op.in]: supercoach_delegate_list.map((e) =>
            e.supercoach_mid.toLowerCase().trim(),
          ),
        }),
      });
      return response;
    } catch (error) {
      throw error;
    }
  }
  public async getSupercoachAndDelegatesList(
    params: SuperCoachDelegateListDto,
  ) {
    try {
      const comp_list =
        params.competency && params.competency.length > 0
          ? params.competency.map((x) => x.toLowerCase().trim())
          : null;
      const business_unit_list =
        params.business_unit && params.business_unit.length > 0
          ? params.business_unit.map((x) => x.toLowerCase().trim())
          : null;
      const designation_list =
        params.designation && params.designation.length > 0
          ? params.designation.map((x) => x.toLowerCase().trim())
          : null;
      const grade_list =
        params.grade && params.grade.length > 0
          ? params.grade.map((x) => x.toLowerCase().trim())
          : null;
      const location_list =
        params.location && params.location.length > 0
          ? params.location.map((x) => x.toLowerCase().trim())
          : null;
      const employee_mid_list =
        params.employee_mid && params.employee_mid.length > 0
          ? params.employee_mid.map((x) => x.toLowerCase().trim())
          : null;
      const allocationdelegate_mid_list =
        params.allocdelegate_mid && params.allocdelegate_mid.length > 0
          ? params.allocdelegate_mid.map((x) => x.toLowerCase().trim())
          : null;
      const query = `
          SELECT DISTINCT ON (users.supercoach_mid) 
              users.supercoach_mid, 
              user_list.email_id, 
              user_list.name, 
              user_list.designation, 
              user_list.location, 
              user_list.business_unit, 
              user_list.competency, 
              user_list.grade,
              supercoach_delegate.allocation_delegate_mid, 
              supercoach_delegate.allocation_delegate_email, 
              supercoach_delegate.allocation_delegate_name, 
              supercoach_delegate.skill_delegate_mid, 
              supercoach_delegate.skill_delegate_email, 
              supercoach_delegate.skill_delegate_name
          FROM "USERS" AS users
          LEFT JOIN "USERS" AS user_list ON user_list.employee_id = users.supercoach_mid
          LEFT JOIN "SUPERCOACH_DELEGATES" AS supercoach_delegate ON users.supercoach_mid = supercoach_delegate.supercoach_mid
          WHERE users.supercoach_mid IS NOT NULL
            AND (
                  $1::text[] IS NULL
                  OR array_length($1::text[], 1) = 0
                  OR LOWER(user_list.competency) = ANY($1::text[])
                )
            AND ($2::text[] IS NULL OR array_length($2::text[], 1) = 0 OR LOWER(user_list.designation) = ANY($2::text[]))
            AND ($3::text[] IS NULL OR array_length($3::text[], 1) = 0 OR LOWER(user_list.business_unit) = ANY($3::text[]))
            AND ($4::text[] IS NULL OR array_length($4::text[], 1) = 0 OR LOWER(user_list.grade) = ANY($4::text[]))
            AND ($5::text[] IS NULL OR array_length($5::text[], 1) = 0 OR LOWER(user_list.location) = ANY($5::text[]))
            AND ($6::text[] IS NULL OR array_length($6::text[], 1) = 0 OR LOWER(users.supercoach_mid) = ANY($6::text[]))
            AND ($7::text[] IS NULL OR array_length($7::text[], 1) = 0 OR LOWER(supercoach_delegate.allocation_delegate_mid) = ANY($7::text[]))
        `;
      const result = await this.user.sequelize.query(query, {
        bind: [
          comp_list,
          designation_list,
          business_unit_list,
          grade_list,
          location_list,
          employee_mid_list,
          allocationdelegate_mid_list,
        ],
      });
      if (result && result.length > 0 && result[0].length > 0) {
        return result[0] as SupercoachDelegateDto[];
      }
      return [];
    } catch (ex) {
      throw ex;
    }
  }

  public async addSupercoachDelegate(param: AddSupercoachDelegateDto) {
    const txn = await this.supercoachDelegate.sequelize.transaction();
    try {
      const supercoachInfo = await this.supercoachDelegate.findOne({
        where: {
          supercoach_mid: {
            [Op.iLike]: `${param.supercoach_mid.toLowerCase().trim()}`,
          },
        },
      });
      if (supercoachInfo) {
        await this.supercoachDelegate.update(
          {
            allocation_delegate_email: param.allocation_delegate_email,
            allocation_delegate_mid: param.allocation_delegate_mid,
            allocation_delegate_name: param.allocation_delegate_name,
            skill_delegate_email: param.skill_delegate_email,
            skill_delegate_mid: param.skill_delegate_mid,
            skill_delegate_name: param.skill_delegate_name,
          },
          {
            where: {
              supercoach_mid: {
                [Op.iLike]: `${param.supercoach_mid.toLowerCase().trim()}`,
              },
            },
            transaction: txn,
          },
        );
      } else {
        const newScDelegate = {
          id: uuidv4(),
          created_at: new Date(),
          created_by: 'Desp',
          updated_at: new Date(),
          updated_by: 'Desp',

          ...param,
        };
        console.log(newScDelegate);
        await this.supercoachDelegate.create(newScDelegate, {
          transaction: txn,
        });
      }
      if (param.allocation_delegate_email) {
        const isUserExists = await this.userRole.findOne({
          where: {
            [Op.and]: [
              {
                user: {
                  [Op.iLike]: `${param.allocation_delegate_email
                    .toLowerCase()
                    .trim()}`,
                },
              },
              {
                role: {
                  [Op.iLike]: `${'supercoach'.toLowerCase().trim()}`,
                },
              },
            ],
          },
        });
        if (!isUserExists) {
          const data = {
            role: 'SuperCoach',
            user: param.allocation_delegate_email,
            employee_id: param.allocation_delegate_mid,
            is_active: true,
            updated_at: new Date(),
            created_at: new Date(),
            updated_by: 'Desc',
            created_by: 'Desc',
          };
          await this.userRole.create(data, {
            transaction: txn,
          });
        } else {
          await this.userRole.update(
            { is_active: true },
            { where: { id: isUserExists.id }, transaction: txn },
          );
        }
      }
      if (param.skill_delegate_email) {
        const isUserExists = await this.userRole.findOne({
          where: {
            [Op.and]: [
              {
                user: {
                  [Op.iLike]: `${param.skill_delegate_email
                    .toLowerCase()
                    .trim()}`,
                },
              },
              {
                role: {
                  [Op.iLike]: `${'skillsupercoach'.toLowerCase().trim()}`,
                },
              },
            ],
          },
        });
        if (!isUserExists) {
          const data = {
            role: 'SkillSuperCoach',
            user: param.skill_delegate_email,
            employee_id: param.skill_delegate_mid,
            is_active: true,
            updated_at: new Date(),
            created_at: new Date(),
            updated_by: 'Desc',
            created_by: 'Desc',
          };
          await this.userRole.create(data, {
            transaction: txn,
          });
        } else {
          await this.userRole.update(
            { is_active: true },
            { where: { id: isUserExists.id }, transaction: txn },
          );
        }
      }
      await txn.commit();
      //todo :- we are not removing anything as of now from the previous data
      return await this.supercoachDelegate.findOne({
        where: {
          supercoach_mid: {
            [Op.iLike]: `${param.supercoach_mid.toLowerCase().trim()}`,
          },
        },
      });
    } catch (error) {
      await txn.rollback();
      throw error;
    }
  }

  public async getUsersBySuperCoachEmails(
    userEmails: string[],
  ): Promise<UserModel[]> {
    try {
      const userEmailsLower = userEmails.map((e) => e.toLowerCase().trim());
      let users = null;
      if (typeof userEmails === 'string') {
        users = await this.user.findAll({
          where: {
            [Op.and]: [
              {
                supercoach_name: {
                  [Op.iLike]:
                    typeof userEmails === 'string'
                      ? [userEmails]
                      : `%${userEmails}`,
                },
              },
              {
                status: true,
              },
            ],
          },
        });
      } else {
        users = await this.user.findAll({
          where: {
            [Op.and]: [
              sequelize.where(
                sequelize.fn('LOWER', sequelize.col('supercoach_name')),
                { [Op.in]: userEmailsLower },
              ),
              {
                status: true,
              },
            ],
          },
        });
      }

      return users;
    } catch (ex) {
      throw ex;
    }
  }
  public async getUsersByRole(
    roleId: number,
    user: IUser,
  ): Promise<UserModel[]> {
    try {
      const query: WhereOptions = {};

      if (!user.roles.includes(IdentityRoles.SYSTEM_ADMIN)) {
        //SystemAdmin Changes System Admin
        query.roles = {
          [Op.notILike]: `%${IdentityRoles.SYSTEM_ADMIN.toLowerCase().trim()}%`, //SystemAdmin Changes system admin
        };
      }

      const users = await this.user.findAll({
        where: query,
        include: [
          {
            model: UserRoleModel,
            where: {
              is_active: true,
            },
          },
        ],
        order: this.getOrderQueryByEmail(user),
      });

      return users;
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException();
    }
  }

  async addUser(params: AddUserDto): Promise<UserModel> {
    try {
      const emp_code = 'C' + Math.floor(Math.random() * (999 - 100 + 1) + 100);
      //params['emp_code'] = emp_code;
      const { roles, ...rest } = params;

      const isUserExists = await this.user.findOne({
        where: {
          uemail_id: { [Op.iLike]: `${params.email_id}` },
        },
        attributes: ['id'],
      });

      if (isUserExists) {
        throw new ConflictException('User already exists');
      }

      const maxId = await this.user.findOne({
        where: {},
        attributes: [
          [this.user.sequelize.fn('max', this.user.sequelize.col('id')), 'id'],
        ],
        raw: true,
      });
      const user = await this.user.create(
        { ...rest, id: maxId?.id + 1 },
        { isNewRecord: true },
      );

      await this.userRole.bulkCreate(
        roles.map((role) => ({
          role,
          user: params.email_id,
          created_by: params.created_by || '',
          updated_by: params.created_by || '',
          is_active: true,
        })),
      );

      return await this.user.findOne({
        where: {
          email_id: user.email_id,
        },
        include: [
          {
            model: UserRoleModel,
            separate: true,
            where: { is_active: true },
          },
        ],
      });
    } catch (error) {
      throw new InternalServerErrorException(error.message);
    }
  }

  public async updateUserRoles(
    emailId: string,
    params: UpdateUserDto,
  ): Promise<UserModel> {
    try {
      const query: WhereOptions = {
        where: {
          email_id: { [Op.iLike]: `%${emailId}` },
        },
      };

      const user = await this.user.findOne({ ...query, attributes: ['id'] });

      if (!user) {
        throw new NotFoundException('user not found');
      }
      const users = await this.user.update(params, {
        where: query.where,
      });

      query.include = [
        {
          model: UserRoleModel,
          separate: true,
          where: { is_active: true },
        },
      ];
      return await this.user.findOne(query);
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException(error.message);
    }
  }

  async updateRole(
    emailId: string,
    roles: string[],
    currentUser: IUser,
  ): Promise<boolean> {
    try {
      const userRoles = await this.userRole.findAll({
        where: { user: emailId },
        attributes: ['user', 'role', 'id', 'is_active'],
      });

      if (userRoles.length == 0) {
        const _employee_id = emailId.split(
          EUserCommandNames.EMP_ID_SPLITTER,
        )[0];
        const userRoles = await this.userRole.bulkCreate(
          roles.map((role) => ({
            role,
            created_by: currentUser.email,
            user: emailId,
            employee_id: _employee_id,
          })),
          { returning: true },
        );

        return true;
      }

      const rolesToBeUpdated = userRoles.filter(
        (role) => roles.includes(role.role) && !role.is_active,
      );

      const notFoundRoles = userRoles.filter(
        (role) => !roles.includes(role.role),
      );

      const newRoles = roles.filter(
        (role) =>
          !userRoles.some(
            (userRole) => userRole.role.toLowerCase() === role.toLowerCase(),
          ),
      );

      if (rolesToBeUpdated.length) {
        await this.userRole.update(
          { is_active: true, updated_by: currentUser.email },
          {
            where: { id: { [Op.in]: rolesToBeUpdated.map((role) => role.id) } },
          },
        );
      }

      if (notFoundRoles.length) {
        await this.userRole.update(
          { is_active: false, updated_by: currentUser.email },
          { where: { id: { [Op.in]: notFoundRoles.map((role) => role.id) } } },
        );
      }
      if (newRoles.length) {
        const _employee_id = emailId.split(
          EUserCommandNames.EMP_ID_SPLITTER,
        )[0];
        await this.userRole.bulkCreate(
          newRoles.map((role) => ({
            role,
            created_by: currentUser.email,
            user: emailId,
            employee_id: _employee_id,
          })),
        );
      }

      return true;
    } catch (error) {
      throw error;
    }
  }

  async updateBulkRoles(
    emailId: string,
    roles: string[],
    currentUser: IUser,
  ): Promise<boolean> {
    try {
      const userRoles = await this.userRole.findAll({
        where: { user: emailId },
        attributes: ['user', 'role', 'id', 'is_active'],
      });

      if (userRoles.length == 0) {
        const _employee_id = emailId.split(
          EUserCommandNames.EMP_ID_SPLITTER,
        )[0];
        const userRoles = await this.userRole.bulkCreate(
          roles.map((role) => ({
            role,
            created_by: currentUser.email,
            user: emailId,
            employee_id: _employee_id,
          })),
          { returning: true },
        );

        return true;
      }

      const rolesToBeUpdated = userRoles.filter(
        (role) => roles.includes(role.role) && !role.is_active,
      );

      const newRoles = roles.filter(
        (role) =>
          !userRoles.some(
            (userRole) => userRole?.role?.toLowerCase() === role?.toLowerCase(),
          ),
      );

      if (rolesToBeUpdated.length) {
        await this.userRole.update(
          { is_active: true, updated_by: currentUser.email },
          {
            where: { id: { [Op.in]: rolesToBeUpdated.map((role) => role.id) } },
          },
        );
      }

      if (newRoles.length) {
        const _employee_id = emailId.split(
          EUserCommandNames.EMP_ID_SPLITTER,
        )[0];
        await this.userRole.bulkCreate(
          newRoles.map((role) => ({
            role,
            created_by: currentUser.email,
            user: emailId,
            employee_id: _employee_id,
          })),
        );
      }

      return true;
    } catch (error) {
      throw error;
    }
  }

  public async updateUser(
    emailId: string,
    params: UpdateUserV2Dto,
    currentUser: IUser,
  ): Promise<UserModel> {
    try {
      const query: WhereOptions = {
        where: {
          email_id: { [Op.iLike]: `%${emailId}` },
        },
      };
      const user = await this.user.findOne({ ...query, attributes: ['id'] });
      if (!user) {
        throw new NotFoundException('user not found');
      }

      const { roles, ...rest } = params;

      if (params.roles.length > 0) {
        await this.updateRole(emailId, params.roles, currentUser);
      }

      if (Object.keys(rest).length) {
        await this.user.update(rest, {
          where: query.where,
        });
      }

      return await this.user.findOne({
        where: query.where,
        include: [
          {
            model: UserRoleModel,
            required: false,
            where: {
              is_active: true,
            },
          },
        ],
      });
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException(error.message);
    }
  }

  public async updateBulkUserRoles(
    emailId: string,
    params: UpdateUserV2Dto,
    currentUser: IUser,
  ): Promise<UserModel> {
    try {
      const query: WhereOptions = {
        where: {
          email_id: { [Op.iLike]: `%${emailId}` },
        },
      };
      const user = await this.user.findOne({ ...query, attributes: ['id'] });
      if (!user) {
        throw new NotFoundException('user not found');
      }

      const { roles, ...rest } = params;

      if (params.roles.length > 0) {
        await this.updateBulkRoles(emailId, params.roles, currentUser);
      }

      if (Object.keys(rest).length) {
        await this.user.update(rest, {
          where: query.where,
        });
      }

      return await this.user.findOne({
        where: query.where,
        include: [
          {
            model: UserRoleModel,
            required: false,
            where: {
              is_active: true,
            },
          },
        ],
      });
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException(error.message);
    }
  }

  public async deleteUser(user: number) {
    try {
      await this.user.update(
        {
          status: false,
        },
        {
          where: { id: user },
        },
      );
    } catch (error) {
      throw new InternalServerErrorException(error.message);
    }
  }
  public async getUserByEmailForMicro(params: any): Promise<UserModel[]> {
    try {
      const { email_id } = params;
      // let emails = typeof email_id == 'string' ? [email_id] : email_id;
      const emailIdInput = typeof email_id === 'string' ? [email_id] : email_id;

      // Construct an array of case-insensitive conditions using `Op.or`
      const conditions = emailIdInput.map((id) =>
        sequelize.where(
          sequelize.fn('lower', sequelize.col('email_id')),
          'ilike',
          `%${id.toLowerCase()}%`,
        ),
      );
      const query = {
        where: {
          [Op.or]: conditions,
        },
      };
      // const query: WhereOptions = {
      //   where: {
      //     email_id: {
      //       [Op.in]: typeof email_id == 'string' ? [email_id] : email_id,
      //     },
      //   },
      // };
      const users = await this.user.findAll(query);

      return users;
    } catch (error) {
      throw new InternalServerErrorException(error.message);
    }
  }

  public async getUserByEmail(
    params: FindAndUpsertUserDto,
    select: (keyof UserModel)[] = [],
    loggedInuser?: IUser,
  ): Promise<UserModel> {
    try {
      const { emailId } = params;
      const query: WhereOptions = {
        where: {
          [Op.and]: [
            {
              uemail_id: { [Op.iLike]: `${emailId}` },
            },
            {
              status: true,
            },
          ],
        },

        include: {
          model: UserRoleModel,
          separate: true,
          where: {
            is_active: true,
          },
        },
      };

      if (select.length > 0) {
        query.attributes = select;
      }

      const user = await this.user.findOne(query);

      if (
        user &&
        user.email_id !== loggedInuser.email &&
        user.role_list.some(
          (role) =>
            role.role.toLowerCase() ===
            IdentityRoles.SYSTEM_ADMIN.toLowerCase(),
        )
      ) {
        throw new NotFoundException('User not found');
      }

      return user;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }
      throw new InternalServerErrorException();
    }
  }

  public async getUserByEmailForModulePermission(
    params: FindAndUpsertUserDto,
    select: (keyof UserModel)[] = [],
    loggedInuser?: IUser,
  ): Promise<UserModel> {
    try {
      const { emailId } = params;
      const query: WhereOptions = {
        where: {
          [Op.and]: [
            {
              uemail_id: { [Op.iLike]: `${emailId}` },
            },
            {
              is_active: true,
            },
          ],
        },

        include: {
          model: UserRoleModel,
          separate: true,
          where: {
            is_active: true,
          },
        },
      };

      if (select.length > 0) {
        query.attributes = select;
      }

      const user = await this.user.findOne(query);

      // if (
      //   user &&
      //   user.email_id !== loggedInuser.email &&
      //   user.role_list.some(
      //     (role) =>
      //       role.role.toLowerCase() ===
      //       IdentityRoles.SYSTEM_ADMIN.toLowerCase(),
      //   )
      // ) {
      //   throw new NotFoundException('User not found');
      // }

      return user;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }
      throw new InternalServerErrorException();
    }
  }

  public async getUserBymail(
    params: FindAndUpsertUserDto,
    select: (keyof UserModel)[] = [],
    loggedInuser?: IUser,
  ): Promise<UserModel> {
    try {
      const { emailId } = params;
      const query: WhereOptions = {
        where: {
          email_id: { [Op.iLike]: emailId },
          // status: true,
        },

        include: {
          model: UserRoleModel,
          separate: true,
          where: {
            is_active: true,
          },
        },
      };

      if (select.length > 0) {
        query.attributes = select;
      }

      const user = await this.user.findOne(query);

      if (
        user &&
        user.email_id !== loggedInuser.email &&
        user.role_list.some(
          (role) =>
            role.role.toLowerCase() ===
            IdentityRoles.SYSTEM_ADMIN.toLowerCase(),
        )
      ) {
        throw new NotFoundException('User not found');
      }

      return user;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }
      throw new InternalServerErrorException();
    }
  }

  getOrderQueryByEmail(user: IUser): Literal {
    return this.user.sequelize.literal(
      `CASE WHEN "email_id" = '${user.email}' THEN 0 ELSE 1 END, 
    CASE WHEN "status" = false THEN 1 ELSE 0 END, 
    "email_id" ASC`,
    );
  }

  public async getAllUsers(
    params: FindUserDto,
    user: IUser,
  ): Promise<UserListDto> {
    try {
      const { skip, limit, roles, include_sa, email_id, ...rest } = params;
      const query: WhereOptions = rest;
      query.is_active = true;
      // query.status = true;
      const userRoleModelQuery: WhereOptions = {};

      if (rest.name) {
        query.name = {
          [Op.iLike]: `%${rest.name}%`,
        };
      }

      if (email_id) {
        query.email_id = {
          [Op.in]: email_id,
        };
      }
      if (params.designation && params.designation.length > 0) {
        query.designation = {
          [Op.in]: params.designation,
        };
      }
      if (
        !user?.roles.includes(IdentityRoles.SYSTEM_ADMIN) ||
        include_sa === 'false'
      ) {
        //SystemAdmin Changes System Admin
        userRoleModelQuery.role = {
          [Op.notILike]: `%${IdentityRoles.SYSTEM_ADMIN.toLowerCase().trim()}%`, //SystemAdmin Changes system admin
        };
      }

      userRoleModelQuery.is_active = true;

      if (roles && roles.length) {
        query.email_id = sequelize.literal(
          ` ("UserModel"."email_id" IN (select "ur"."user" from "USER_ROLE" "ur" where  "ur"."is_active"=true and  "ur"."role" IN (${roles
            .map((role) => `'${role}'`)
            .join()})))`,
        );
      }

      const users = await this.user.findAndCountAll({
        where: query,
        limit,
        offset: skip,
        order: user ? this.getOrderQueryByEmail(user) : [['name', 'asc']],
        include: [
          {
            model: UserRoleModel,
            where: userRoleModelQuery,
          },
        ],
      });
      return users;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }

  public async getAllUsersList(user: IUser): Promise<any> {
    try {
      const userRoleModelQuery: WhereOptions = {};
      if (!user?.roles.includes(IdentityRoles.SYSTEM_ADMIN)) {
        //SystemAdmin Changes System Admin
        userRoleModelQuery.role = {
          [Op.notILike]: `%${IdentityRoles.SYSTEM_ADMIN.toLowerCase().trim()}%`, //SystemAdmin Changes system admin
        };
      }
      userRoleModelQuery.is_active = true;

      const users = await this.user.findAll({
        order: user ? this.getOrderQueryByEmail(user) : [['name', 'asc']],
        // include: [
        //   {
        //     model: UserRoleModel,
        //     where: userRoleModelQuery,
        //   },
        // ],
      });
      return users;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }
  async joinedTableResultIntoArray(data): Promise<any> {
    const user = data.reduce(
      (acc, row) => {
        acc.roles.push(row.role);

        return acc;
      },
      { ...data[0], roles: [] },
    );
    if (user?.roles?.length == 0 || !user?.roles?.includes('Employee')) {
      user.role = 'Employee';
      user.roles.push('Employee');
      const newRole = ['Employee'];
      const _employee_id = user.email.split(
        EUserCommandNames.EMP_ID_SPLITTER,
      )[0];
      await this.userRole.bulkCreate(
        newRole.map((role) => ({
          role,
          created_by: user.email,
          user: user?.email,
          employee_id: _employee_id,
        })),
        { returning: true },
      );
    }
    return user;
  }
  public async getListOfAllUser(): Promise<any> {
    try {
      const userRoleModelQuery: WhereOptions = {};
      userRoleModelQuery.is_active = true;

      const users = await this.user.findAll({
        include: [
          {
            model: UserRoleModel,
            where: userRoleModelQuery,
          },
        ],
      });
      return users;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }

  async checkUserActiveStatus(emailId: string): Promise<UserModel> {
    try {
      const query = `
      select "user"."id","user"."emp_code","user"."name","user"."email_id" "email", "ur"."role", "user"."designation","user"."service_line","user"."supercoach_name","user"."co_supercoach_name"
      ,"user"."employee_id","user"."uemail_id","user"."competency","user"."competencyId", "user".supercoach_mid, "user".co_supercoach_mid
      ,"user"."business_unit","user"."location","user"."grade"
      from "USERS" "user"
      left join "USER_ROLE" "ur" on "ur"."employee_id" = "user"."employee_id" and "ur"."is_active" = true
      where "uemail_id" ilike '${emailId}' and "status"= true`;

      const [result, _] = await this.user.sequelize.query(query);

      const user = (result.length && result) || undefined;

      if (!user) {
        return null;
      }
      return await this.joinedTableResultIntoArray(result);
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  // public async getEmpDetailsFromWCGT(
  //   param: string,
  // ): Promise<UserDetailsWCGTDto> {
  //   try {
  //     const query: WhereOptions = {};
  //     // query.email_id = param;
  //     // const userObj = await this.user.findOne({
  //     //   where: query
  //     // });
  //     // return userObj;

  //     const wcgtUser: UserDetailsWCGTDto = {
  //       emp_Email: '',
  //       emp_Name: '',
  //       emp_Designation: 'Consultant',
  //       emp_Expertise: 'expertise1',
  //       emp_SME: 'sme1',
  //     };

  //     return wcgtUser;
  //   } catch (error) {
  //     console.error(error);
  //     throw error;
  //   }
  // }

  public async getEmpDetailsFromWCGT(
    param: string,
  ): Promise<UserDetailsWCGTDto | null> {
    try {
      const wcgtUsers: UserDetailsWCGTDto[] = [
        {
          id: 1,
          emp_Email: 'DemoUser1@gmail.com',
          emp_Name: 'Demo User 1',
          emp_Designation: 'Consultant',
          emp_Expertise: 'expertise1',
          emp_SME: 'sme1',
          role_list: ['Admin', 'Employee'],
          status: true,
        },
        {
          id: 2,
          emp_Email: 'DemoUser2@gmail.com',
          emp_Name: 'Demo User 2',
          emp_Designation: 'Manager',
          emp_Expertise: 'expertise2',
          emp_SME: 'sme2',
          role_list: ['Resource Requestor', 'Employee'],
          status: true,
        },
        {
          id: 3,
          emp_Email: 'DemoUser3@gmail.com',
          emp_Name: 'Demo User 3',
          emp_Designation: 'Manager',
          emp_Expertise: 'expertise3',
          emp_SME: 'sme3',
          role_list: ['Resource Requestor'],
          status: true,
        },
        {
          id: 4,
          emp_Email: 'DemoUser4@gmail.com',
          emp_Name: 'Demo User 4',
          emp_Designation: 'Manager',
          emp_Expertise: 'expertise4',
          emp_SME: 'sme4',
          role_list: ['Employee'],
          status: true,
        },
        {
          id: 5,
          emp_Email: 'DemoUser5@gmail.com',
          emp_Name: 'Demo User 5',
          emp_Designation: 'Sr. Manager',
          emp_Expertise: 'expertise5',
          emp_SME: 'sme5',
          role_list: ['Employee', 'Resource Requestor'],
          status: true,
        },
        {
          id: 6,
          emp_Email: 'DemoUser6@gmail.com',
          emp_Name: 'Demo User 6',
          emp_Designation: 'Sr. Manager',
          emp_Expertise: 'expertise6',
          emp_SME: 'sme6',
          role_list: ['Employee', 'Resource Requestor', 'Admin'],
          status: true,
        },
        {
          id: 7,
          emp_Email: 'DemoUser7@gmail.com',
          emp_Name: 'Demo User 7',
          emp_Designation: 'Asst. Manager',
          emp_Expertise: 'expertise7',
          emp_SME: 'sme7',
          role_list: ['Employee', 'Admin'],
          status: true,
        },
        {
          id: 8,
          emp_Email: 'DemoUser8@gmail.com',
          emp_Name: 'Demo User 8',
          emp_Designation: 'Asst. Manager',
          emp_Expertise: 'expertise8',
          emp_SME: 'sme8',
          role_list: ['Employee', 'Delegate'],
          status: true,
        },
        {
          id: 9,
          emp_Email: 'DemoUser9@gmail.com',
          emp_Name: 'Demo User 9',
          emp_Designation: 'Asst. Manager',
          emp_Expertise: 'expertise9',
          emp_SME: 'sme9',
          role_list: ['Delegate'],
          status: true,
        },
        {
          id: 10,
          emp_Email: 'john.smith@example.com',
          emp_Name: 'John Smith',
          emp_Designation: 'Asst. Manager',
          emp_Expertise: 'expertise9',
          emp_SME: 'sme9',
          role_list: ['Delegate'],
          status: true,
        },
      ];
      const matchingUser = wcgtUsers.find(
        (user) => user?.emp_Email?.toLowerCase() === param.toLowerCase(),
      );
      if (matchingUser) {
        return matchingUser;
      } else {
        return null;
      }
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  public async updateUserStatus(
    emailId: string,
    isActive: boolean,
    user: IUser,
  ): Promise<boolean> {
    try {
      const updatedRows = await this.user.update(
        {
          is_active: isActive,
          updated_by: 'UpdatedByUser', //user.email
          updated_at: new Date(),
        },
        {
          where: { email_id: emailId },
        },
      );

      return updatedRows.length > 0;
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  public async getandcheckUserByEmail(
    params: FindAndUpsertUserDto,
    select: (keyof UserModel)[] = [],
    loggedInuser?: IUser,
  ): Promise<UserModel> {
    try {
      const { emailId } = params;

      const query: WhereOptions = {
        where: {
          [Op.and]: [
            {
              uemail_id: { [Op.iLike]: `${emailId}` },
            },
            {
              status: true,
            },
          ],
        },

        include: {
          model: UserRoleModel,
          separate: true,
          where: {
            is_active: true,
          },
        },
      };

      if (select.length > 0) {
        query.attributes = select;
      }

      const user = await this.user.findOne(query);

      if (
        user &&
        user.email_id !== loggedInuser.email &&
        user.role_list.some(
          (role) =>
            role.role.toLowerCase() ===
            IdentityRoles.SYSTEM_ADMIN.toLowerCase(),
        )
      ) {
        throw new NotFoundException('User not found');
      }

      return user;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }
      throw new InternalServerErrorException();
    }
  }

  public async getUserByNameOrEmail(
    params: any,
    select: (keyof UserModel)[] = [],
  ): Promise<Array<any>> {
    try {
      const { name } = params;
      let whereCondition = '';
      const paramArray = name?.split(',');
      paramArray.forEach((item: any, index: number) => {
        if (index == 0) {
          whereCondition += ` LOWER("USERS".email_id) like '%${item.toLowerCase()}%' OR LOWER("USERS".name) like '${item.toLowerCase()}%'`;
        } else {
          whereCondition += `OR( LOWER("USERS".email_id) like '%${item.toLowerCase()}' OR LOWER("USERS".name) like '${item.toLowerCase()}%' )`;
        }
      });
      const query =
        `SELECT "USERS".id, "USERS".role_ids, "USERS".name, "USERS".email_id, "USERS".employee_id, "USERS".entity, "USERS".emp_code, "USERS".fname, "USERS".lname, "USERS".designation, "USERS".grade,
      "USERS".location, "USERS".expertise, "USERS".smeg, "USERS".supercoach_name, "USERS".co_supercoach_name, "USERS".service_line, "USERS".roles, 
      "USERS".created_by, "USERS".is_active, "USERS".status, "USERS".updated_by,"USERS".created_at,"USERS".business_unit,"USERS".region_name ,"USERS".uemail_id
      , "USERS"."competency", "USERS"."competencyId", "USERS".supercoach_mid, "USERS".co_supercoach_mid , "SUPERCOACH_DELEGATES".*
       FROM "USERS" LEFT JOIN "SUPERCOACH_DELEGATES" 
        ON "USERS".supercoach_mid = "SUPERCOACH_DELEGATES".supercoach_mid where (` +
        whereCondition +
        ` ) and "USERS".status= true;`;
      // where (LOWER("USERS".email_id) like '${name.toLowerCase()}%' OR LOWER("USERS".name) like '${name.toLowerCase()}%' )and "USERS".status= true;`;
      // const query = `
      // select "user"."id","user"."emp_code","user"."name","user"."email_id" "email", "ur"."role", "user"."designation","user"."service_line"
      // from "USERS" "user"
      // inner join "USER_ROLE" "ur" on "ur"."user" = "user"."email_id" and "ur"."is_active" = true
      // where "email_id" ilike '%${name}' and "status"= true`;

      const [result, _] = await this.user.sequelize.query(query);

      const user = (result.length && result) || undefined;
      return user;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }
      throw new InternalServerErrorException();
    }
  }

  public async removeUserRoleByEmail(
    params: RemoveUserRoleByEmailDto[],
  ): Promise<any> {
    try {
      const conditions = params.flatMap((userRole) =>
        userRole.roles.map((role) => ({
          user: userRole.email_id,
          role: role,
        })),
      );

      const result = await this.userRole.update(
        { is_active: false },
        {
          where: {
            [Op.or]: conditions,
          },
          returning: true,
        },
      );
      return result;
    } catch (error) {
      throw error;
    }
  }
}
