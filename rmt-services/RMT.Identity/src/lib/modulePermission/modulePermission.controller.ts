import {
  Controller,
  Get,
  Param,
  UseFilters,
  Query,
  Logger,
} from '@nestjs/common';
import { IUser, User } from '../../common/decorators/user.decorator';
import { HttpExceptionFilter } from '../../common/filters/http-exception.filter';
import { FindModulePermissionDto } from './dto/findModulePermission.dto';
import { ModulePermissionListDto } from './dto/modulePermissionList.dto';
import { ModulePermissionService } from './modulePermission.service';

const LOG_CONTEXT = 'identity-module-service';
@Controller('modulePermission')
export class ModulePermissionController {
  constructor(
    private readonly modulePermissionService: ModulePermissionService,
    private readonly logger: Logger,
  ) {}

  @Get('/role')
  async getRoleModulePermissions(
    @Query() param: FindModulePermissionDto,
    @User() user: IUser,
  ): Promise<ModulePermissionListDto[]> {
    try {
      debugger;
      const roles = await this.modulePermissionService.getRoleModulePermissions(
        param,
        user,
      );
      this.logger.debug('role ', roles, LOG_CONTEXT);
      return roles;
    } catch (err) {
      this.logger.log(err, LOG_CONTEXT);
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }

  @Get('/id')
  async getPermission(
    @Query() query: FindModulePermissionDto,
    @User() user: IUser,
  ): Promise<ModulePermissionListDto[]> {
    try {
      const roles = await this.modulePermissionService.getRoleModulePermissions(
        { emailId: query.emailId },
        user,
      );
      return roles;
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }

  @Get('/all')
  async getAllModulePermissionsByRole(): Promise<ModulePermissionListDto[]> {
    try {
      const roles =
        await this.modulePermissionService.getAllModulePermissionsByRole();
      return roles;
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }

  @Get('/permissionMappings')
  async getAllModulePermissionMapping(
    @User() user: IUser,
  ): Promise<ModulePermissionListDto[]> {
    try {
      const roles = await this.modulePermissionService.getAllPermissionMapping(
        user,
      );
      return roles;
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }

  @Get('query/v1')
  async getModulePermissionForAuth(
    @Query() params: any,
  ): Promise<ModulePermissionListDto[]> {
    try {
      const roles =
        await this.modulePermissionService.getModulePermissionForAuth(
          params.email_id,
        );
      return roles;
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }
}
