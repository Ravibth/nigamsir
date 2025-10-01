import { Injectable } from '@nestjs/common';
import { IUser } from '../../common/decorators/user.decorator';
import { FindRoleDto } from './dto/findRole.dto';
import { RoleModelSelect } from './interfaces';
import { RoleModel } from './models/role.model';
import { RoleRepository } from './role.repository';

@Injectable()
export class RoleService {
  constructor(private readonly roleRepository: RoleRepository) {}

  public async getRoles(user?: IUser): Promise<RoleModel[]> {
    try {
      const roles = await this.roleRepository.getRoles(user);
      return roles;
    } catch (err) {
      throw err;
    }
  }

  public async getRolesByQuery(
    query: FindRoleDto,
    select?: RoleModelSelect,
  ): Promise<RoleModel[]> {
    try {
      const roles = await this.roleRepository.getRolesByQuery(query, select);
      return roles;
    } catch (err) {
      throw err;
    }
  }
}
