import { Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { RoleModule } from '../role/role.module';
import { DesignationRoleController } from './designationRole.controller';
import { DesignationRoleRepository } from './designationRole.repository';
import { DesignationRoleService } from './designationRole.service';
import { DesignationRoleModel } from './models/designationRole.model';

@Module({
  controllers: [DesignationRoleController],
  imports: [SequelizeModule.forFeature([DesignationRoleModel]), RoleModule],
  providers: [DesignationRoleRepository, DesignationRoleService],
  exports: [DesignationRoleService],
})
export class DesignationRoleModule {}
