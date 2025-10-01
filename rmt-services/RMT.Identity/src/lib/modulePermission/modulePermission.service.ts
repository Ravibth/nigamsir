import { BadGatewayException, Injectable, Logger } from '@nestjs/common';
import { UserService } from '../user/user.service';
import { FindModulePermissionDto } from './dto/findModulePermission.dto';
import {
  IModulePermissionsAccessDetails,
  ModulePermissionListDto,
} from './dto/modulePermissionList.dto';
import { ModulePermissionRepository } from './modulePermission.repository';
import { RoleService } from '../role/role.service';
import { IUser } from '../../common/decorators/user.decorator';
import { IdentityRoles } from '../user/user.constant';

const LOG_CONTEXT = 'identity-service';
@Injectable()
export class ModulePermissionService {
  constructor(
    private readonly logger: Logger,
    private readonly modulePermissionRepository: ModulePermissionRepository,
    private readonly userService: UserService,
    private readonly roleService: RoleService,
  ) {}

  public async getRoleModulePermissionsV2(
    param: FindModulePermissionDto,
    loggedInUser: IUser,
  ): Promise<ModulePermissionListDto[]> {
    try {
      this.logger.debug('1', LOG_CONTEXT);
      const [allModulePermissions, assignedModulePermission] =
        await Promise.all([
          this.getAllPermissionMapping(),
          this.modulePermissionRepository.getRoleModulePermissions(
            param.roleName,
          ),
        ]);
      this.logger.debug('2', LOG_CONTEXT);
      let assignedModulePermissionMapping: Map<
        string,
        ModulePermissionListDto
      > = new Map();

      assignedModulePermissionMapping = assignedModulePermission.reduce(
        (acc, mod) => {
          mod.is_assigned = true;
          mod.permissions = this.addMissingKeysInPermissions(
            mod.permissions,
            null,
          );
          acc.set(mod.module_name, mod);
          return acc;
        },
        assignedModulePermissionMapping,
      );
      this.logger.debug('3', assignedModulePermissionMapping, LOG_CONTEXT);
      for (const value of allModulePermissions) {
        if (!assignedModulePermissionMapping.has(value.module_name)) {
          value.is_assigned = false;
          assignedModulePermission.push(value);
        }
      }
      this.logger.debug('4', assignedModulePermission, LOG_CONTEXT);

      return assignedModulePermission.sort((valueA, valueB) =>
        valueA.module_name
          .toLowerCase()
          .localeCompare(valueB.module_name.toLowerCase()),
      );
    } catch (error) {
      this.logger.log(error, LOG_CONTEXT);
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  public async getRoleModulePermissions(
    param: FindModulePermissionDto,
    loggedInUser: IUser,
  ): Promise<ModulePermissionListDto[]> {
    try {
      if (param.emailId) {
        const user = await this.userService.getUserInfoByEmail(
          {
            emailId: param.emailId,
          },
          [],
          loggedInUser,
        );
        if (user.is_active) param.roleName = user.role_list.map((role) => role);
        else {
          throw new BadGatewayException('User not found');
        }
      }
      this.logger.debug('1', LOG_CONTEXT);
      if (param.roleName) {
        const [allModulePermissions, assignedModulePermission] =
          await Promise.all([
            this.getAllPermissionMapping(),
            this.modulePermissionRepository.getRoleModulePermissions(
              param.roleName,
            ),
          ]);
        this.logger.debug('2', LOG_CONTEXT);
        let assignedModulePermissionMapping: Map<
          string,
          ModulePermissionListDto
        > = new Map();

        assignedModulePermissionMapping = assignedModulePermission.reduce(
          (acc, mod) => {
            mod.is_assigned = true;
            mod.permissions = this.addMissingKeysInPermissions(
              mod.permissions,
              null,
            );
            acc.set(mod.module_name, mod);
            return acc;
          },
          assignedModulePermissionMapping,
        );
        this.logger.debug('3', assignedModulePermissionMapping, LOG_CONTEXT);
        for (const value of allModulePermissions) {
          if (!assignedModulePermissionMapping.has(value.module_name)) {
            value.is_assigned = false;
            assignedModulePermission.push(value);
          }
        }
        this.logger.debug('4', assignedModulePermission, LOG_CONTEXT);

        return assignedModulePermission.sort((valueA, valueB) =>
          valueA.module_name
            .toLowerCase()
            .localeCompare(valueB.module_name.toLowerCase()),
        );
      } else {
        return [];
      }
    } catch (error) {
      this.logger.log(error, LOG_CONTEXT);
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  public async getAllModulePermissionsByRole() {
    try {
      const modulePermissionMapping =
        await this.modulePermissionRepository.getAllModulePermissionsByRole();
      this.logger.debug(
        'modulePermissionMapping',
        modulePermissionMapping,
        LOG_CONTEXT,
      );

      return modulePermissionMapping;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  addMissingKeysInPermissions(
    params: IModulePermissionsAccessDetails,
    userInfo?: IUser,
  ): IModulePermissionsAccessDetails {
    try {
      if (userInfo && userInfo.roles.includes(IdentityRoles.SYSTEM_ADMIN)) {
        //SystemAdmin Changes System Admin
        return {
          create: true,
          read: true,
          approve: true,
          update: true,
          delete: true,
        };
      }

      if (params.create == undefined) {
        params.create = false;
      }

      if (params.read == undefined) {
        params.read = false;
      }

      if (params.update == undefined) {
        params.update = false;
      }

      if (params.delete == undefined) {
        params.delete = false;
      }

      if (params.approve == undefined) {
        params.approve = false;
      }

      return params;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  public async getAllPermissionMapping(userInfo?: IUser) {
    try {
      const modulePermissionMapping =
        await this.modulePermissionRepository.getAllRoleModulePermissionMapping();
      console.log('Before adding missing keys', modulePermissionMapping);
      (modulePermissionMapping || []).forEach((element) => {
        element.permissions = this.addMissingKeysInPermissions(
          element.permissions,
          userInfo,
        );
      });
      console.log('After adding missing keys', modulePermissionMapping);
      return modulePermissionMapping;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  public async getModulePermissionForAuth(emailId?: string) {
    try {
      this.logger.debug('getModulePermissionForAuth', LOG_CONTEXT);
      return await this.modulePermissionRepository.getModulePermissionForAuth(
        emailId,
      );
    } catch (error) {
      this.logger.log(error, LOG_CONTEXT);
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
}
