import { Controller, Get, Logger, UseFilters } from '@nestjs/common';
import { IUser, User } from '../../common/decorators/user.decorator';
import { HttpExceptionFilter } from '../../common/filters/http-exception.filter';
import { RoleModel } from './models/role.model';
import { RoleService } from './role.service';

const LOG_CONTEXT = 'identity-role-service';

@Controller('role')
export class RoleController {
  constructor(
    private readonly roleService: RoleService,
    private readonly logger: Logger,
  ) {}

  @Get()
  async getRoles(@User() user: IUser): Promise<RoleModel[]> {
    try {
      return await this.roleService.getRoles(user);
    } catch (err) {
      this.logger.error(err, LOG_CONTEXT);
      throw err;
    }
  }
}
