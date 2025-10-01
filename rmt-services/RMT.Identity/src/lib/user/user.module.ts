import { Logger, Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { DesignationRoleModule } from '../designationRoleMapping/designationRole.module';

import { RoleModule } from '../role/role.module';
import { UserModel } from './models/users.model';
import { UserController } from './user.controller';
import { UserRepository } from './repository/user.repository';
import { UserService } from './user.service';
import { UserRoleModel } from './models/userRole.model';
import { UserTransportController } from './userTransport.controller';
import { ErpConnectorModule } from '../../microserviceModules/erp-connector/erp.module';
import { WCGTModule } from 'src/microserviceModules/Wcgt/Wcgt.module';
import { ModulePermissionService } from '../modulePermission/modulePermission.service';
import { ModulePermissionRepository } from '../modulePermission/modulePermission.repository';
import { Modules } from '../modulePermission/models/module.model';
import { ModulePermission } from '../modulePermission/models/modulePermission.model';
import { Permission } from '../modulePermission/models/permission.model';
import { RoleModulePermissionModel } from '../modulePermission/models/roleModulePermission.model';
import { SupercoachDelegateModel } from './models/supercoachDelegate.model';

@Module({
  controllers: [UserController, UserTransportController],
  imports: [
    SequelizeModule.forFeature([
      UserModel,
      UserRoleModel,
      SupercoachDelegateModel,
      Modules,
      ModulePermission,
      Permission,
      RoleModulePermissionModel,
    ]),
    RoleModule,
    DesignationRoleModule,
    ErpConnectorModule,
    WCGTModule,
    RoleModule,
  ],
  providers: [
    UserRepository,
    UserService,
    ModulePermissionService,
    ModulePermissionRepository,
    Logger,
  ],
  exports: [UserService, ModulePermissionService],
})
export class UserModule {}
