import { Logger, Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { RoleModule } from '../role/role.module';
import { UserModule } from '../user/user.module';
import { Modules } from './models/module.model';
import { ModulePermission } from './models/modulePermission.model';
import { Permission } from './models/permission.model';
import { RoleModulePermissionModel } from './models/roleModulePermission.model';
import { ModulePermissionController } from './modulePermission.controller';
import { ModulePermissionRepository } from './modulePermission.repository';
import { ModulePermissionService } from './modulePermission.service';
@Module({
  controllers: [ModulePermissionController],
  imports: [
    SequelizeModule.forFeature([
      Modules,
      ModulePermission,
      Permission,
      RoleModulePermissionModel,
    ]),
    UserModule,
    RoleModule,
  ],
  providers: [ModulePermissionService, ModulePermissionRepository, Logger],
  exports: [ModulePermissionService],
})
export class ModulePermissionModule {}
