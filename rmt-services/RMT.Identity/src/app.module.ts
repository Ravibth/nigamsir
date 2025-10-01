import { Module } from '@nestjs/common';
import { APP_FILTER } from '@nestjs/core';
import { SequelizeModule, SequelizeModuleOptions } from '@nestjs/sequelize';
import { AppController } from './app.controller';
import { HttpExceptionFilter } from './common/filters/http-exception.filter';
import { SecretManager } from './common/secretManager/secretManager';
import { DesignationRoleModule } from './lib/designationRoleMapping/designationRole.module';
import { ModulePermissionModule } from './lib/modulePermission/modulePermission.module';
import { RoleModule } from './lib/role/role.module';
import { UserModule } from './lib/user/user.module';
import { ErpConnectorModule } from './microserviceModules/erp-connector/erp.module';
import { WCGTModule } from './microserviceModules/Wcgt/Wcgt.module';

@Module({
  imports: [
    SequelizeModule.forRootAsync({
      useFactory: async (): Promise<SequelizeModuleOptions> => {
        const secretManager = SecretManager.getInstance();
        return secretManager.dbConfig;
      },
    }),
    RoleModule,
    UserModule,
    ModulePermissionModule,
    DesignationRoleModule,
    ErpConnectorModule,
    WCGTModule,
  ],

  controllers: [AppController],
  providers: [
    {
      provide: APP_FILTER,
      useClass: HttpExceptionFilter,
    },
  ],
})
export class AppModule {}
