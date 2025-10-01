import { Injectable, InternalServerErrorException } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { IUser } from '../../common/decorators/user.decorator';
import { FindRoleDto } from './dto/findRole.dto';
import { RoleModelSelect } from './interfaces';
import { RoleModel } from './models/role.model';
import { Op, WhereOptions } from 'sequelize';

@Injectable()
export class RoleRepository {
  constructor(@InjectModel(RoleModel) private role: typeof RoleModel) {}

  public async getRoles(user: IUser): Promise<RoleModel[]> {
    try {
      const query: WhereOptions = {
        is_active: true,
        is_display: true,
      };

      const roles = await this.role.findAll({
        where: query,
        order: [['role_name', 'asc']],
      });

      return roles;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }

  public async getRolesByQuery(
    query: FindRoleDto,
    select: RoleModelSelect = [],
  ): Promise<RoleModel[]> {
    try {
      const finalQuery =
        select.length > 0
          ? {
              where: {
                ...query,
              },
              attributes: select,
            }
          : {
              where: {
                ...query,
              },
            };

      const roles = await this.role.findAll(finalQuery);
      return roles;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }
  // public async addRoleByUserEmailIfNotExist(
  //   userEmail: string,
  //   role: string,
  // ): Promise<any> {
  //   this.role.fin
  // }
}
