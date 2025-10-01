import { Injectable, NotFoundException } from '@nestjs/common';
import { RoleService } from '../role/role.service';
import { DesignationRoleRepository } from './designationRole.repository';
import { AddDesignationMapping } from './dto/addDesignationMapping.dto';
import { FindDesignationDto } from './dto/findDesignationRole.dto';
import {
  UpdateDesignationMappingDto,
  UpdateFilterDto,
} from './dto/updateDesignationMapping.dto';
import { DesignationRoleModelSelect } from './interfaces';
import { DesignationRoleModel } from './models/designationRole.model';

@Injectable()
export class DesignationRoleService {
  constructor(
    private readonly repository: DesignationRoleRepository,
    private readonly roleService: RoleService,
  ) {}

  async validateRole(roleId: number): Promise<boolean> {
    try {
      const role = await this.roleService.getRolesByQuery({ id: roleId }, [
        'id',
      ]);

      if (role.length === 0) {
        throw new NotFoundException('Role not found for given id');
      }

      return true;
    } catch (err) {
      throw err;
    }
  }

  public async addDesignationMapping(
    params: AddDesignationMapping,
  ): Promise<DesignationRoleModel> {
    try {
      await this.validateRole(params.role_id);
      const roles = await this.repository.addDesignation(params);
      return roles;
    } catch (err) {
      throw err;
    }
  }

  public async getRolesByQuery(
    query: FindDesignationDto,
    select?: DesignationRoleModelSelect,
  ): Promise<DesignationRoleModel[]> {
    try {
      const roles = await this.repository.getRolesByQuery(query, select);
      return roles;
    } catch (err) {
      throw err;
    }
  }

  public async updateDesignationRoleMapping(
    filter: UpdateFilterDto,
    params?: UpdateDesignationMappingDto,
  ): Promise<DesignationRoleModel> {
    try {
      const roles = await this.repository.updateDesignationRole(filter, params);
      return roles;
    } catch (err) {
      throw err;
    }
  }
}
