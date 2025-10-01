import { Logger, Module } from '@nestjs/common';
import { SequelizeModule } from '@nestjs/sequelize';
import { RoleModel } from './models/role.model';
import { RoleController } from './role.controller';
import { RoleRepository } from './role.repository';
import { RoleService } from './role.service';

@Module({
  controllers: [RoleController],
  imports: [SequelizeModule.forFeature([RoleModel])],
  providers: [RoleRepository, RoleService, Logger],
  exports: [RoleService],
})
export class RoleModule {}
