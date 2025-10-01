import {
  Injectable,
  BadRequestException,
  ConflictException,
  ForbiddenException,
  NotFoundException,
} from '@nestjs/common';
import { faker } from '@faker-js/faker';

import { FindUserDto } from './dto/findUserList.dto';
import { UserListDto } from './dto/userList.dto';
import { UserModel } from './models/users.model';
import { UpdateUserDto } from './dto/updateUser.dto';
import { UserRepository } from './repository/user.repository';
import { DesignationRolesMapping } from './designation_roles_mapping';
import { RoleService } from '../role/role.service';
import { IUser } from '../../common/decorators/user.decorator';
import { DesignationRoleService } from '../designationRoleMapping/designationRole.service';
import { FindAndUpsertUserDto } from './dto/findAndUpsert.dto';
import { AddUserDto } from './dto/addUser.dto';
import { UpdateUserV2Dto } from './dto/updateUserV2.dto';
import { UserRoleModel } from './models/userRole.model';
import {
  BadGatewayException,
  InternalServerErrorException,
} from '@nestjs/common/exceptions';
import { ErpConnectorService } from '../../microserviceModules/erp-connector/erp.service';
import { EmployeeDto } from '../../microserviceModules/erp-connector/interface';
import { UserDetailsWCGTDto } from './dto/userDetailWCGT.dto';
import {
  FindOptions,
  SetOptions,
  SaveOptions,
  InstanceUpdateOptions,
  InstanceDestroyOptions,
  InstanceRestoreOptions,
  IncrementDecrementOptionsWithBy,
  Model,
} from 'sequelize';
import {
  AssociationActionOptions,
  AssociationGetOptions,
  $GetType,
  AssociationCountOptions,
} from 'sequelize-typescript';
import { AssociationCreateOptions } from 'sequelize-typescript/dist/model/model/association/association-create-options';
import { SequelizeHooks } from 'sequelize/types/hooks';
import { ValidationOptions } from 'sequelize/types/instance-validator';
import { UserInfoDTO } from './dto/userInfo.dto';
import { WCGTService } from 'src/microserviceModules/Wcgt/Wcgt.service';
import { IGetUserInfoWcgtResponseDTO } from 'src/microserviceModules/Wcgt/interface';
import { AddUserRoles } from './dto/addUserRoles.dto';
import { RolesListMaster } from 'src/common/enum';
import { UserDTO } from './dto/user.dto';
import { RemoveUserRoleByEmailDto } from './dto/removeUserRoleByEmailDTO.dto';
import { SupercoachDelegateDto } from './dto/supercoachDelegate.dto';
import { AddSupercoachDelegateDto } from './dto/addSupercoachDelegate.dto';
import { ClientSecretCredential } from '@azure/identity';
import { ServiceBusClient } from '@azure/service-bus';
import { ProjectEventPayloadDto } from './dto/projectEventPayload.dto';
import { SuperCoachDelegateListDto } from './dto/supercoachDelegateList.dto';

@Injectable()
export class UserService {
  constructor(
    private readonly userRepository: UserRepository,
    private readonly roleService: RoleService,
    private readonly designationService: DesignationRoleService,
    private readonly erpConnector: ErpConnectorService,
    private readonly wcgtService: WCGTService,
  ) {}
  async GetSupercoachUserListByAllocationSupercoachDelegate(email: string) {
    try {
      return await this.userRepository.GetSupercoachUserListByAllocationSupercoachDelegate(
        email,
      );
    } catch (error) {
      throw error;
    }
  }
  async getSupercoachAndDelegatesList(params: SuperCoachDelegateListDto) {
    try {
      console.log(params);
      const result: SupercoachDelegateDto[] =
        await this.userRepository.getSupercoachAndDelegatesList(params);
      return result;
    } catch (error) {
      throw error;
    }
  }
  async getSuperCoachAndDelegateBySupercoachMid(supercoach_mid: string) {
    try {
      const supercoach_delegate =
        await this.userRepository.getSuperCoachAndDelegate(supercoach_mid);
      return supercoach_delegate;
    } catch (error) {
      throw error;
    }
  }
  async addSupercoachDelegate(param: AddSupercoachDelegateDto, user: IUser) {
    try {
      const supercoachInfo = await this.userRepository.getSuperCoachAndDelegate(
        param.supercoach_mid,
      );
      const result = await this.userRepository.addSupercoachDelegate(param);
      const payload = {
        supercoach_mid: param.supercoach_mid,
        supercoach_email: param.supercoach_email,
        new_allocation_delegate_name: param.allocation_delegate_name,
        prev_allocation_delegate_email: !(
          supercoachInfo === null || supercoachInfo === undefined
        )
          ? supercoachInfo.allocation_delegate_email
          : null,
        prev_allocation_delegate_mid: !(
          supercoachInfo === null || supercoachInfo === undefined
        )
          ? supercoachInfo.allocation_delegate_mid
          : null,
        new_allocation_delegate_email: result.allocation_delegate_email,
        new_allocation_delegate_mid: param.allocation_delegate_mid,
        prev_skill_delegate_email: !(
          supercoachInfo === null || supercoachInfo === undefined
        )
          ? supercoachInfo.skill_delegate_email
          : null,
        prev_skill_delegate_mid: !(
          supercoachInfo === null || supercoachInfo === undefined
        )
          ? supercoachInfo.skill_delegate_mid
          : null,
        new_skill_delegate_email: result.skill_delegate_email,
        new_skill_delegate_mid: param.skill_delegate_mid,
        new_skill_delegate_name: param.skill_delegate_name,
        status: 'pending',
        outcome: 'inprogress',
      };
      const action_payload_project: ProjectEventPayloadDto = {
        action: 'ADD_UPDATE_SUPERCOACH_DELEGATE',
        payload: JSON.stringify(payload),
        token: user.token,
      };
      const notification_payload = {
        path: '',
        response_payload: JSON.stringify({
          supercoach_email: payload.supercoach_email,
          allocation_supercoach_email: payload.new_allocation_delegate_email,
          skill_supercoach_email: payload.new_skill_delegate_email,
        }),
        token: user.token,
      };
      if (
        payload.new_allocation_delegate_email &&
        payload.new_allocation_delegate_email !== ''
      ) {
        const action_allocation_delegate_payload_notification: ProjectEventPayloadDto =
          {
            action: 'NOTIFICATION_FOR_ALLOCATION_SUPERCOACH_DELEGATE_CHANGE',
            payload: JSON.stringify(notification_payload),
            response_payload: JSON.stringify(notification_payload),
            token: user.token,
          };
        await this.publishMassageToServiceBusTopic(
          action_allocation_delegate_payload_notification,
          'notification',
        );
      }
      if (
        payload.new_skill_delegate_email &&
        payload.new_skill_delegate_email !== ''
      ) {
        const action_allocation_delegate_payload_notification: ProjectEventPayloadDto =
          {
            action: 'NOTIFICATION_FOR_SKILL_SUPERCOACH_DELEGATE_CHANGE',
            payload: JSON.stringify(notification_payload),
            token: user.token,
          };
        await this.publishMassageToServiceBusTopic(
          action_allocation_delegate_payload_notification,
          'notification',
        );
      }

      await this.publishMassageToServiceBusTopic(
        action_payload_project,
        'project',
      );

      return result;
    } catch (error) {
      throw error;
    }
  }
  publishMassageToServiceBusTopic = async (payload, type: string) => {
    const connectionString = process.env['AzureServiceBus'].toString().trim();
    const topic = process.env['topic'].toString().trim();
    const serviceBusConnectionMethod = process.env['ServiceBusConnectionMethod']
      .toString()
      .trim();
    let serviceBusClient;
    if (serviceBusConnectionMethod === 'AD') {
      const fullQualifiedName = process.env[
        'AzureServiceBus__fullyQualifiedNamespace'
      ]
        .toString()
        .trim();
      const clientId: string = process.env['SBClientId'].toString().trim();
      const clientSecret: string = process.env['SBClientSecret']
        .toString()
        .trim();
      const tenantId: string = process.env['SBTenantId'].toString().trim();
      const credential = new ClientSecretCredential(
        tenantId,
        clientId,
        clientSecret,
      );
      serviceBusClient = new ServiceBusClient(fullQualifiedName, credential);
    } else {
      // serviceBusClient = new ServiceBusClient(
      //   `Endpoint=sb://rmt-service-bus-dev-nagarro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Q7yAV+QD/ubFvE6vO4czLXRNlKfHjdZV1+ASbCu0ZpE=`,
      // );
      serviceBusClient = new ServiceBusClient(connectionString);
    }
    const serviceBusSender = serviceBusClient.createSender(topic);
    // const body = JSON.stringify(payload);

    await serviceBusSender.sendMessages({
      body: payload,
      applicationProperties: {
        type: type,
      },
    });
  };
  async addUser(params: AddUserDto, user: IUser): Promise<UserModel> {
    try {
      const roles = params.roles;
      const roleModels = await this.roleService.getRolesByQuery(
        {
          role_name: roles,
        },
        ['id', 'role_name'],
      );

      if (roleModels.length === 0) {
        throw new NotFoundException('Given roles not found');
      }

      const roleNames = roleModels.map((role) => role.role_name);
      params.roles = roleNames;
      params.created_by = user.email || 'wcgt';
      let newUser = await this.userRepository.addUser(params);

      newUser = this.addRoleNameFieldFromRoleList([newUser])[0];

      return newUser;
    } catch (error) {
      throw error;
    }
  }
  async getUsersByUserEmails(emails: string[]): Promise<UserModel[]> {
    try {
      return await this.userRepository.getUsersByEmails(emails);
    } catch (error) {}
  }
  async getUsersBySuperCoachEmails(emails: string[]): Promise<UserModel[]> {
    try {
      return await this.userRepository.getUsersBySuperCoachEmails(emails);
    } catch (error) {}
  }

  addSortingKeyToLoggedInUserRecord(
    users: UserModel[],
    email: string,
  ): UserModel[] {
    const index = users.findIndex(
      (user) => user.email_id.toLowerCase() === email.toLowerCase(),
    );

    if (index === -1) {
      return users;
    }

    return (users = users
      .map((user, i) => {
        user.order = i == index ? 0 : 1;

        return user;
      })
      .sort((a, b) => a.order - b.order));
  }
  public async getUsersByRole(
    roleId: number,
    user: IUser,
  ): Promise<UserModel[]> {
    try {
      const roleUsers = await this.userRepository.getUsersByRole(roleId, user);

      return this.addRoleNameFieldFromRoleList(roleUsers);
    } catch (error) {
      throw error;
    }
  }

  public async getUsersByRoleV2(
    roleName: string,
    user: IUser,
  ): Promise<UserModel[]> {
    try {
      const params: FindUserDto = {
        roles: [roleName],
        skip: null,
        limit: null,
      };

      const roleUsers = await this.userRepository.getAllUsers(params, user);

      return roleUsers.rows || [];
    } catch (error) {
      throw error;
    }
  }

  public async updateUserRoles(
    emailId: string,
    params: UpdateUserDto,
    user: IUser,
  ): Promise<UserModel> {
    if (emailId.toLowerCase() == user.email.toLowerCase()) {
      throw new ConflictException('can not update yourself');
    }

    params = await this.validateUserRoles(emailId, params, user);

    let roleUsers = await this.userRepository.updateUserRoles(emailId, params);

    roleUsers = this.addRoleNameFieldFromRoleList([roleUsers])[0];

    return roleUsers;
  }

  public async updateUser(
    emailId: string,
    params: UpdateUserV2Dto,
    user: IUser,
  ): Promise<UserDTO> {
    if (emailId.toLowerCase() == user.email.toLowerCase()) {
      throw new ConflictException('can not update yourself');
    }

    params = await this.validateUserRoles(emailId, params, user);

    let updatedUser = await this.userRepository.updateUser(
      emailId,
      params,
      user,
    );

    updatedUser = this.addRoleNameFieldFromRoleList([updatedUser])[0];

    const actionList: string[] = [];
    if (updatedUser.roles.includes('Admin')) {
      actionList.push('ADMIN_ASSIGNED_BY_SYSTEM_ADMIN');
    }
    const result = updatedUser;
    result.dataValues.actionList = actionList;
    return result;
  }

  public async updateUserBulkRoles(
    emailId: string,
    params: UpdateUserV2Dto,
    user: IUser,
  ): Promise<UserModel> {
    if (emailId.toLowerCase() == user.email.toLowerCase()) {
      throw new ConflictException('can not update yourself');
    }

    params = await this.validateUserRoles(emailId, params, user);

    let updatedUser = await this.userRepository.updateBulkUserRoles(
      emailId,
      params,
      user,
    );

    if (updatedUser) {
      updatedUser = this.addRoleNameFieldFromRoleList([updatedUser])[0];
    } else {
      throw new Error('Failed to update user');
    }

    return updatedUser;
  }

  bulkUpdationProcess(element: AddUserRoles, user: IUser) {
    return new Promise(async (resolve, reject) => {
      try {
        const result = await this.updateUserBulkRoles(
          element.email_id,
          { roles: element.roles } as UpdateUserV2Dto,
          user,
        );
        resolve(result);
      } catch (err) {
        resolve(err);
      }
    });
  }
  async bulkUpdateRole(params: AddUserRoles[], user: IUser) {
    try {
      await Promise.all(
        params.map((item) => this.bulkUpdationProcess(item, user)),
      )
        .then((res) => {
          console.log('finished ');
        })
        .catch((err) => {
          console.log('err', err);
        });
    } catch (err) {
      throw new Error(err);
    }
  }
  public async deleteUser(roleUserId: number): Promise<any> {
    const roleUsers = await this.userRepository.deleteUser(roleUserId);

    return roleUsers;
  }

  public async genNewUserMeta(
    params: FindAndUpsertUserDto,
  ): Promise<UserModel> {
    try {
      const designations = [
        'Partner',
        'Executive Director',
        'Director',
        'Manager',
        'Assistant Manager',
        'Associate Director',
        'Associate Partner',
        'Consultant',
        'Analyst',
        'Associate',
        'Senior Associate',
        'Trainee',
      ];

      const random = Math.floor(Math.random() * designations.length);

      const names = {
        firstName: faker.name.firstName(),
        lastName: faker.name.lastName(),
      };
      const user = {
        email_id: params.emailId,
        role_list: [],
        role_ids: '',
        name: `${names.firstName} ${names.lastName}`,
        fname: names.firstName,
        lname: names.lastName,
        service_line: 'Internal',
        designation: designations[random],
        status: true,
      };

      return user as UserModel;
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException();
    }
  }

  transformIntoUserModel(user: EmployeeDto): UserModel {
    const { is_active, cr_date, up_date, expertise, emp_code, ...rest } = user;

    return {
      ...rest,
      expertise,
      service_line: expertise,
      is_active: is_active == 'Y',
      status: is_active == 'Y',
      emp_code: emp_code,
      created_at: new Date(cr_date),
      updated_at: new Date(up_date),
    } as unknown as UserModel;
  }

  transformUserModelIntoUserInfo(user: UserModel): UserInfoDTO {
    const { role_list, ...rest } = user.dataValues;
    if (role_list.length) {
      return {
        ...rest,
        role_list: role_list.map((item) => item.dataValues.role),
      };
    }
    return {
      ...rest,
      role_list: [RolesListMaster.Employee],
    };
  }
  transformWCGTUserResponseIntoUserInfo(
    user: IGetUserInfoWcgtResponseDTO,
  ): UserInfoDTO {
    return {
      role_ids: '',
      email_id: user.email_id,
      name: user.name,
      emp_code: user.employee_code,
      fname: user.first_name,
      lname: user.last_name,
      designation: user.designation,
      service_line: user.service_line,
      roles: RolesListMaster.Employee,
      role_list: [],
      status: user.isactive,
      is_active: user.isactive,
      is_existing: false,
      created_by: user.createdby,
      created_at: user.createdat,
      updated_by: user.modifiedby,
      updated_at: user.modifiedat,
      supercoach_name: user.supercoach_name,
      co_supercoach_name: user.co_supercoach_name,
      supercoach_mid: user.supercoach_mid,
      co_supercoach_mid: user.co_supercoach_mid,
      smeg: user.smeg,
      expertise: user.expertise,
      location: user?.location?.location_name,
      uemail_id: user.uemail_id,
      employee_id: user.employee_id,
      competency: user.competency,
      competencyId: user.competencyId,
    };
  }
  public async getUserInfoByEmail(
    params: FindAndUpsertUserDto,
    select?: (keyof UserModel)[],
    loggedInuser?: IUser,
  ): Promise<UserInfoDTO> {
    try {
      const user = await this.userRepository.getUserByEmailForModulePermission(
        params,
        select,
        loggedInuser,
      );
      if (user) {
        return this.transformUserModelIntoUserInfo(user);
      }
      const userFromWcgt = await this.wcgtService.getUserDetailsByEmailFromWCGT(
        {
          emp_emailid: params.emailId,
        },
        loggedInuser,
      );
      if (!userFromWcgt) {
        throw new BadGatewayException('User not found');
      }
      return this.transformWCGTUserResponseIntoUserInfo(userFromWcgt);
    } catch (err) {
      throw new Error(` Method:- GetUserInfoByEmail, Err:- ${err.message}`);
    }
  }

  public async getUserByEmail(
    params: FindAndUpsertUserDto,
    select?: (keyof UserModel)[],
    loggedInuser?: IUser,
  ): Promise<UserModel> {
    try {
      const user = await this.userRepository.getUserByEmail(
        params,
        select,
        loggedInuser,
      );

      if (process.env.NODE_ENV === 'dev') return user;
      const userFromWcgt = await this.erpConnector.getUserDetailsByEmail(
        {
          email: params.emailId,
        },
        loggedInuser,
      );

      if (!user) {
        if (userFromWcgt && userFromWcgt.length == 0) {
          throw new NotFoundException('User not found');
        }

        return this.transformIntoUserModel(userFromWcgt[0]);
      } else if (user && userFromWcgt && userFromWcgt.length > 0) {
        const user_data = {
          name: userFromWcgt[0].name,
          emp_code: userFromWcgt[0].emp_code,
          designation: userFromWcgt[0].designation,
          fname: userFromWcgt[0].fname,
          lname: userFromWcgt[0].lname,
          expertise: userFromWcgt[0].expertise,
          supercoach_name: userFromWcgt[0].supercoach_name,
          co_supercoach_name: userFromWcgt[0].co_supercoach_name,
          is_active: userFromWcgt[0].is_active == 'Y' ? true : false,
          entity: userFromWcgt[0].entity,
          updated_by: 'wcgt',
          status: userFromWcgt[0].is_active == 'Y' ? true : false,
        };
        user.set(user_data);
        await user.save();
      }
      return user;
    } catch (err) {
      throw err;
    }
  }

  public async getUserByEmailForMicro(params): Promise<UserModel[]> {
    try {
      return await this.userRepository.getUserByEmailForMicro(params);
    } catch (err) {
      throw err;
    }
  }

  public async getUserForAuthV2(params, loggedINUser: IUser): Promise<any> {
    try {
      const response = await this.userRepository.checkUserActiveStatus(
        params.email_id,
      );
      return response;
    } catch (err) {
      throw err;
    }
  }

  public async getUserForAuth(params, loggedINUser: IUser): Promise<any> {
    try {
      const response = await this.userRepository.checkUserActiveStatus(
        params.email_id,
      );
      if (response == null) {
        const userFromWcgt =
          await this.wcgtService.getUserDetailsByEmailFromWCGT(
            {
              emp_emailid: params.email_id,
            },
            loggedINUser,
          );
        if (!userFromWcgt) {
          throw new BadGatewayException('User not found');
        } else {
          const wcgtUser = {
            email_id: userFromWcgt.email_id,
            emp_code: userFromWcgt.employee_code,
            business_unit: userFromWcgt.business_unit,
            expertise: userFromWcgt.expertise,
            smeg: userFromWcgt.smeg,
            designation: userFromWcgt.designation,
            service_line: userFromWcgt.service_line,
            status: userFromWcgt.isactive,
            fname: userFromWcgt.first_name,
            lname: userFromWcgt.last_name,
            name: userFromWcgt.name,
            location: userFromWcgt?.location?.location_name,
            roles: [RolesListMaster.Employee],
            id: 0,
            role_ids: '',
            role: RolesListMaster.Employee,
            supercoach_name: userFromWcgt.supercoach_name,
            co_supercoach_name: userFromWcgt.co_supercoach_name,
            supercoach_mid: userFromWcgt.supercoach_mid,
            co_supercoach_mid: userFromWcgt.co_supercoach_mid,
            uemail_id: `${userFromWcgt.employee_mid}__${userFromWcgt.email_id}`,
            employee_id: userFromWcgt.employee_mid,
            competency: userFromWcgt.competency,
            competencyId: userFromWcgt.competencyId,
          };
          return wcgtUser;
        }
      }
      return response;
    } catch (err) {
      throw err;
    }
  }

  async getAllUsers(params: FindUserDto, user: IUser): Promise<UserListDto> {
    try {
      const users = await this.userRepository.getAllUsers(params, user);

      users.rows = this.addRoleNameFieldFromRoleList(users.rows);

      return users;
    } catch (err) {
      throw err;
    }
  }

  async getAllUsersList(user: IUser): Promise<any> {
    try {
      const users = await this.userRepository.getAllUsersList(user);
      users.rows = this.addRoleNameFieldFromRoleList(users.rows);

      return users;
    } catch (err) {
      throw err;
    }
  }

  async getListOfAllUser(): Promise<any> {
    try {
      const users = await this.userRepository.getListOfAllUser();
      users.rows = this.addRoleNameFieldFromRoleList(users.rows);
      return users;
    } catch (err) {
      throw err;
    }
  }

  async fetchRoles(
    designation: string,
    loggedInUser: IUser,
  ): Promise<string[]> {
    try {
      const [allRoles, designationMappedRoles] = await Promise.all([
        this.roleService.getRoles(loggedInUser),
        this.designationService.getRolesByQuery({}, ['designation']),
      ]);

      const drr = designationMappedRoles.reduce((acc, role) => {
        acc[role.role_details.role_name.toLowerCase()] = true;
        return acc;
      }, {});

      const nonMappedRoles = allRoles
        .filter((role) => !drr[role.role_name.toLowerCase()])
        .map((role) => role.role_name);

      const rolesToBeMapped = designationMappedRoles
        .filter(
          (role) =>
            role.designation.toLowerCase() === designation.toLowerCase(),
        )
        .map((role) => role.role_details.role_name);

      return [...new Set(nonMappedRoles.concat(rolesToBeMapped))];
    } catch (err) {
      throw err;
    }
  }

  async validateUserRoles(
    emailId: string,
    params: UpdateUserDto | UpdateUserV2Dto,
    userInfo: IUser,
  ): Promise<UpdateUserDto> {
    try {
      const user = await this.userRepository.getUserBymail(
        { emailId },
        ['roles', 'designation', 'status'],
        userInfo,
      );

      if (user && !user.status && params.roles && !params.status) {
        throw new BadRequestException(
          'can not update user with in-active status',
        );
      }

      //   if (
      //     user &&
      //     !user.status &&
      //     user.roles.toLowerCase().includes('admin')
      //   ) {
      //     throw new BadRequestException(
      //       'can not update user with in-active status',
      //     );
      //   }
      // }

      if (!params.roles || params.roles.length == 0) {
        return params;
      }

      if (
        params.roles &&
        params.roles &&
        userInfo.email.toLowerCase() === emailId.toLowerCase()
      ) {
        throw new BadRequestException('User can not update their roles');
      }

      const roles = params.roles;

      const designationRoleMapping = await this.fetchRoles(
        user.designation,
        userInfo,
      );

      const isRoleAssignable = roles.every((role) =>
        designationRoleMapping.includes(role),
      );

      // if (!isRoleAssignable) {
      //   throw new BadRequestException(
      //     `Supplied roles can not be assigned to the user having ${user.designation} designation.`,
      //   );
      // }

      const roleModels = await this.roleService.getRolesByQuery(
        {
          role_name: roles,
        },
        ['id'],
      );

      const roleIds = roleModels.map((role) => role.id).join(',');
      if (roleIds.length === 0) {
        throw new BadRequestException('Roles not found for given role names');
      }

      params.role_ids = roleIds;
      return params;
    } catch (error) {
      throw error;
    }
  }

  stringSorting = (valueA: string, valueB: string) => {
    try {
      return valueA.toLowerCase().localeCompare(valueB.toLowerCase());
    } catch (err) {
      return 0;
    }
  };

  addRoleNameFieldFromRoleList(users: UserModel[]) {
    try {
      if (!Array.isArray(users)) {
        users = [users];
      }

      for (const user of users) {
        user['dataValues']['role_list'] = (
          user['dataValues']['role_list'] || []
        )
          .filter(
            (role) => role.user.toLowerCase() == user.email_id.toLowerCase(),
          )
          .sort((valueA: UserRoleModel, valueB: UserRoleModel) =>
            this.stringSorting(valueA.role, valueB.role),
          );

        user.role_list = (user.role_list || [])
          .filter(
            (role) => role.user.toLowerCase() == user.email_id.toLowerCase(),
          )
          .sort((valueA: UserRoleModel, valueB: UserRoleModel) =>
            this.stringSorting(valueA.role, valueB.role),
          );
        user.roles = user.role_list.map((role) => role.role).join(',');
      }

      return users;
    } catch (err) {
      return users;
    }
  }

  public async getEmpDetailsFromWCGT(
    param: string,
  ): Promise<UserDetailsWCGTDto> {
    try {
      return await this.userRepository.getEmpDetailsFromWCGT(param);
    } catch (error) {
      throw error;
    }
  }

  public async updateUserStatus(
    emailId: string,
    isActive: boolean,
    user: IUser,
  ): Promise<boolean> {
    try {
      return await this.userRepository.updateUserStatus(
        emailId,
        isActive,
        user,
      );
    } catch (error) {
      throw error;
    }
  }

  public async getandcheckUserByEmail(
    params: FindAndUpsertUserDto,
    select?: (keyof UserModel)[],
    loggedInuser?: IUser,
  ): Promise<UserInfoDTO> {
    try {
      const user = await this.userRepository.getandcheckUserByEmail(
        params,
        select,
        loggedInuser,
      );

      if (user) {
        const isEmployeeRolePresent = user.role_list.find(
          (item: UserRoleModel) => item.role == 'Employee',
        );
        const userResp: UserInfoDTO = {
          id: user.id,
          role_ids: '',
          email_id: user.email_id,
          name: user.name,
          fname: user.fname,
          lname: user.lname,
          designation: user.designation,
          grade: user.grade,
          service_line: user.service_line,
          roles: user.roles,
          status: user.status,
          is_existing: true,
          role_list: user.role_list
            ?.filter((a) => a.is_active)
            ?.map((item: UserRoleModel) => item.role),
          supercoach_name: user.supercoach_name,
          co_supercoach_name: user.co_supercoach_name,
          supercoach_mid: user.supercoach_mid,
          co_supercoach_mid: user.co_supercoach_mid,
          uemail_id: `${user.email_id}`,
          employee_id: user.employee_id,
          competency: user.competency,
          competencyId: user.competencyId,
        };
        if (!isEmployeeRolePresent) {
          userResp.role_list.push(RolesListMaster.Employee);
        }
        if (userResp.status == false) {
          throw new NotFoundException('User Disabled');
        }
        return userResp;
      }

      const userFromWcgt = await this.wcgtService.getUserDetailsByEmailFromWCGT(
        {
          emp_emailid: params.emailId,
        },
        loggedInuser,
      );
      // const userFromWcgt = await this.userRepository.getEmpDetailsFromWCGT(
      //   params.emailId,
      // );
      if (userFromWcgt) {
        const userResp: UserInfoDTO = {
          role_ids: '',
          email_id: userFromWcgt.email_id,
          name: userFromWcgt.name,
          fname: userFromWcgt?.first_name || '',
          lname: userFromWcgt?.last_name || '',
          designation: userFromWcgt.designation,
          service_line: userFromWcgt.service_line || '',
          roles: userFromWcgt['roles'] || [RolesListMaster.Employee],
          status: userFromWcgt.isactive,
          is_existing: false,
          role_list: userFromWcgt['role_list'] || [RolesListMaster.Employee],
          supercoach_name: userFromWcgt.supercoach_name,
          co_supercoach_name: userFromWcgt.co_supercoach_name,
          supercoach_mid: userFromWcgt.supercoach_mid,
          co_supercoach_mid: userFromWcgt.co_supercoach_mid,
          uemail_id: `${userFromWcgt.employee_mid}__${userFromWcgt.email_id}`,
          employee_id: userFromWcgt.employee_mid,
          competency: userFromWcgt.competency,
          competencyId: userFromWcgt.competencyId,
        };
        //Add user to User and Role
        const newUser: AddUserDto = {
          email_id: userFromWcgt.email_id,
          roles: userFromWcgt['roles'] || [RolesListMaster.Employee],
          name: userFromWcgt.name,
          service_line: userFromWcgt.service_line || '',
          designation: userFromWcgt.designation,
          expertise: userFromWcgt.expertise,
          emp_code: userFromWcgt.employee_code,
          smeg: userFromWcgt.smeg,
          co_supercoach_name: userFromWcgt.co_supercoach_name,
          location: userFromWcgt?.location?.location_name,
          supercoach_name: userFromWcgt.supercoach_name,
          status: userFromWcgt.isactive,
          created_by: 'Wcgt',
          supercoach_mid: userFromWcgt.supercoach_mid,
          co_supercoach_mid: userFromWcgt.co_supercoach_mid,
          uemail_id: `${userFromWcgt.employee_mid}__${userFromWcgt.email_id}`,
          employee_id: userFromWcgt.employee_mid,
          fname: userFromWcgt?.first_name || '',
          lname: userFromWcgt?.last_name || '',
        };
        //await this.addUser(newUser, loggedInuser);
        return userResp;
      }
      throw new NotFoundException('User not found');
    } catch (err) {
      throw err;
    }
  }
  public async getUserByNameOrEmail(
    params: any,
    select?: (keyof UserModel)[],
  ): Promise<Array<any>> {
    try {
      const users = await this.userRepository.getUserByNameOrEmail(
        params,
        select,
      );

      // const supercoach_delegate =
      //   await this.userRepository.getSuperCoachAndDelegate(user.supercoach_mid);
      const userResp: Array<any> = [];
      if (users) {
        users.forEach(async (user: any) => {
          const userResponse: any = {
            id: user.id,
            role_ids: '',
            emailId: user.email_id,
            employeeId: user.employee_id,
            name: user.name,
            fname: user.fname,
            lname: user.lname,
            designation: user.designation,
            grade: user.grade,
            BusinessUnit: user.business_unit,
            roles: user.roles,
            status: user.status,
            is_existing: true,
            role_list: [],
            Supercoach: user.supercoach_name,
            supercoach_name: user.supercoach_name,
            co_supercoach_name: user.co_supercoach_name,
            smeg: user.smeg,
            expertise: user.expertise,
            location: user.location,
            empCode: user.emp_code,
            serviceLine: user.service_line,
            region_name: user.region_name,
            uemail_id: user.uemail_id,
            supercoach_mid: user.supercoach_mid,
            co_supercoach_mid: user.co_supercoach_mid,
            competency: user.competency,
            competencyId: user.competencyId,
            delegate_details: {
              supercoach_mid: user.supercoach_mid,
              allocation_delegate_name: user.allocation_delegate_name,
              allocation_delegate_mid: user.allocation_delegate_mid,
              allocation_delegate_email: user.allocation_delegate_email,
              skill_delegate_name: user.skill_delegate_name,
              skill_delegate_mid: user.skill_delegate_mid,
              skill_delegate_email: user.skill_delegate_email,
            },
          };
          userResp.push(userResponse);
        });
        return userResp;
      } else {
        return [];
      }
    } catch (err) {
      throw err;
    }
  }

  public async removeUserRoleByEmail(
    params: RemoveUserRoleByEmailDto[],
  ): Promise<any> {
    try {
      return await this.userRepository.removeUserRoleByEmail(params);
    } catch (error) {
      throw error;
    }
  }
}
