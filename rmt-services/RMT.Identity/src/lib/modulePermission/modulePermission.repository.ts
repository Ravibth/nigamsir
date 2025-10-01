import {
  Injectable,
  InternalServerErrorException,
  Logger,
} from '@nestjs/common';
import { BadRequestException } from '@nestjs/common/exceptions';
import { InjectModel } from '@nestjs/sequelize';
import { ModulePermissionDTO } from './dto/modulePermission';
import { ModulePermissionListDto } from './dto/modulePermissionList.dto';
import { Modules } from './models/module.model';
import { ModulePermission } from './models/modulePermission.model';
import { Permission } from './models/permission.model';
import { RoleModulePermissionModel } from './models/roleModulePermission.model';
import { IdentityRoles } from '../user/user.constant';

const LOG_CONTEXT = 'identity-repo';

@Injectable()
export class ModulePermissionRepository {
  constructor(
    @InjectModel(Modules) private modules: typeof Modules,
    @InjectModel(ModulePermission)
    private modulePermission: typeof ModulePermission,
    @InjectModel(Permission) private permission: typeof Permission,
    @InjectModel(RoleModulePermissionModel)
    private roleModulePermission: typeof RoleModulePermissionModel,
    private readonly logger: Logger,
  ) {}

  public async getRoleModulePermissions(roleNames: string[]) {
    try {
      const modulePermissionMapping = await this.getModulePermissionMapping(
        roleNames,
      );
      this.logger.debug(
        'modulePermissionMapping',
        modulePermissionMapping,
        LOG_CONTEXT,
      );
      return modulePermissionMapping;
    } catch (error) {
      this.logger.log(error, LOG_CONTEXT);
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  private async getRoleModulePermissionIds(roleId: number) {
    try {
      const modulePermissionIds = await this.roleModulePermission
        .findAll({
          where: {
            role_id: roleId,
            is_display: true,
            is_active: true,
          },
        })
        .then((res) => {
          return res;
        });

      return Promise.all(modulePermissionIds).then((values) => {
        const modulePermissionIds = [];
        values
          .flat(1)
          .forEach((res) => modulePermissionIds.push(res.module_permission_id));
        const permissionIds = [...new Set(modulePermissionIds)];
        return permissionIds;
      });
    } catch (error) {
      throw new InternalServerErrorException();
    }
  }

  private async getModulePermissionMapping(
    roleNames?: string[],
  ): Promise<ModulePermissionListDto[]> {
    try {
      if (!roleNames || roleNames.length === 0) {
        throw new BadRequestException(
          'Role is required for module & permission',
        );
      }

      const whereClause = roleNames.includes(IdentityRoles.SYSTEM_ADMIN) //SystemAdmin Changes System Admin
        ? 'where "rmp"."is_active" = true'
        : `where "rmp"."is_active" = true and "rmp"."role" In('` +
          roleNames.join(`','`) +
          `') `;

      const query = `select "m"."module_name" "module_name" , "m"."module_display" "module_display" ,json_object_agg( distinct lower("p"."permission_name"),
        true
        ) "permissions" from "ROLE_MODULE_PERMISSION" "rmp"
        join "MODULE_PERMISSION" "mp" on "mp"."code" = "rmp"."module_permission_id"
        join "PERMISSION" "p" on "p"."id" = "mp"."permission_id"
        join "MODULE" "m" on "m"."id" = "mp"."module_id" and "m"."is_display" = true
        ${whereClause}  group by "m"."module_name" , "m"."module_display"`;

        console.log('getModulePermissionMapping->query', query);

      const [result, _] = await this.roleModulePermission.sequelize.query(
        query,
        {
          replacements: { roleNames: roleNames },
        },
      );

      return result as ModulePermissionListDto[];
    } catch (err) {
      if (err.status === 400) throw err;
      else throw new InternalServerErrorException();
    }
  }

  async getAllRoleModulePermissionMapping(): Promise<
    ModulePermissionListDto[]
  > {
    try {
      const query = `select "m"."module_name" "module_name", "m"."id" "module_id","m"."module_display" "module_display",
      json_object_agg(lower("p"."permission_name"), true) "permissions"
      from "MODULE_PERMISSION" "mp"
      join "MODULE" "m" on "m"."id" = "mp"."module_id" and "m"."is_display" = true
      join "PERMISSION"  "p" on "p"."id" = "mp"."permission_id"
      group by "module_name", "m"."id"
      order by "module_name" asc`;

      const [result, _] = await this.modulePermission.sequelize.query(query);

      return result as ModulePermissionListDto[];
    } catch (error) {
      throw error;
    }
  }

  private async populateModulePermissions(modulePermissionMap: any) {
    try {
      const modulePermissions = new Array<ModulePermissionDTO>();
      const moduleNames = this.getModuleName(
        modulePermissionMap.module_id,
      ).then((result) => {
        return result;
      });
      const modulePermissionNames = this.getPermissionName(
        modulePermissionMap.permission_id,
      ).then((result) => {
        return result;
      });
      return Promise.all([moduleNames, modulePermissionNames]).then(
        (result) => {
          const r = Object.assign({}, ...result);
          modulePermissions.push({
            moduleName: r.module_name,
            permissionName: r.permission_name,
          });
          return modulePermissions;
        },
      );
    } catch (error) {
      throw new InternalServerErrorException();
    }
  }

  private async getModuleName(moduleId: number) {
    try {
      const moduleName = await this.modules.findAll({
        where: {
          id: moduleId,
          is_active: true,
        },
      });
      return moduleName[0].dataValues;
    } catch (error) {
      throw new InternalServerErrorException();
    }
  }

  private async getPermissionName(permissionId: number) {
    try {
      const permissionName = await this.permission.findAll({
        where: {
          id: permissionId,
          is_active: true,
        },
      });
      return permissionName[0].dataValues;
    } catch (error) {
      throw new InternalServerErrorException();
    }
  }

  public async getAllModulePermissionsByRole() {
    try {
      const query = `select distinct "m"."module_name" "module_name", 
      json_object_agg(
        lower("p"."permission_name"),
            case when lower("p"."permission_name")='create' then true
                 when lower("p"."permission_name")='read' then true
                 when lower("p"."permission_name")='write' then true  
                 when lower("p"."permission_name")='update' then true
                 when lower("p"."permission_name")='approve' then true
                 else false 
             end
         ) over (partition by "m"."module_name")
      "permissions" from ROLE_MODULE_PERMISSION "rmp"
      join MODULE_PERMISSION "mp" on "mp"."code" = "rmp"."module_permission_id"
      join PERMISSION "p" on "p"."id" = "mp"."permission_id"
      join MODULE "m" on "m"."id" = "mp"."module_id" and "m"."is_display" = true
      where "rmp"."is_active" = true
      group by "m"."module_name", "p"."permission_name"
      `;

      const [result, _] = await this.roleModulePermission.sequelize.query(
        query,
      );

      return result as ModulePermissionListDto[];
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }

  public async getModulePermissionForAuth(email: string) {
    try {
      const query = `select "m"."module_name" "module_name", array_agg(lower("p"."permission_name")) "scope" from ROLE_MODULE_PERMISSION "rmp"

      join MODULE_PERMISSION "mp" on "mp"."code" = "rmp"."module_permission_id"
      join PERMISSION "p" on "p"."id" = "mp"."permission_id"
      join MODULE "m" on "m"."id" = "mp"."module_id"
      where "rmp"."role" IN (
        select "ur"."role" from "USER_ROLE" "ur" where "ur"."is_active"= true and "ur"."user" ilike '%${email}'
      )
      group by "m"."module_name"`;

      const [result, _] = await this.roleModulePermission.sequelize.query(
        query,
      );
      if (result?.length == 0) {
        const employee = {
          module_name: 'Employee',
          scope: ['create', 'read', 'update'],
        };
        result.push(employee);
      }
      this.logger.debug('res', result, LOG_CONTEXT);

      return result as ModulePermissionListDto[];
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      console.error(err);
      throw new InternalServerErrorException();
    }
  }
}
