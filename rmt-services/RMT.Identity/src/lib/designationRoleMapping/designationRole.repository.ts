import {
  BadRequestException,
  HttpException,
  Injectable,
  InternalServerErrorException,
} from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { WhereOptions } from 'sequelize';
import { HasMany, HasOne, Sequelize } from 'sequelize-typescript';
import { AddDesignationMapping } from './dto/addDesignationMapping.dto';
import { FindDesignationDto } from './dto/findDesignationRole.dto';
import {
  UpdateDesignationMappingDto,
  UpdateFilterDto,
} from './dto/updateDesignationMapping.dto';
import { DesignationRoleModelSelect } from './interfaces';
import { RoleModel } from '../role/models/role.model';
import { DesignationRoleModel } from './models/designationRole.model';

@Injectable()
export class DesignationRoleRepository {
  constructor(
    @InjectModel(DesignationRoleModel)
    private role: typeof DesignationRoleModel,
  ) {}

  public async addDesignation(
    params: AddDesignationMapping,
  ): Promise<DesignationRoleModel> {
    try {
      const checkExistingMapping = await this.role.findOne({
        where: {
          designation: params.designation,
          role_id: params.role_id,
          is_active: true,
        },
        attributes: ['id'],
      });

      if (checkExistingMapping) {
        throw new BadRequestException('Already exist');
      }

      const roles = await this.role.create({ ...params });
      return roles;
    } catch (err) {
      console.error(err);
      if (err instanceof HttpException) {
        throw err;
      }

      throw new InternalServerErrorException();
    }
  }

  public async getRolesByQuery(
    query: FindDesignationDto,
    select: DesignationRoleModelSelect = [],
  ): Promise<DesignationRoleModel[]> {
    try {
      const finalQuery: WhereOptions =
        select.length > 0
          ? {
              where: {
                ...query,
              },
              attributes: {
                include: select,
              },
            }
          : {
              where: {
                ...query,
              },
            };

      finalQuery.where.is_active = true;

      const roles = await this.role.scope('role_details').findAll(finalQuery);
      return roles;
    } catch (err) {
      console.error(err);
      throw new InternalServerErrorException();
    }
  }

  public async updateDesignationRole(
    filter: UpdateFilterDto,
    params: UpdateDesignationMappingDto,
  ): Promise<DesignationRoleModel> {
    try {
      const users = await this.role.update(params, {
        where: { ...filter },
      });

      return await this.role.findOne({
        where: { ...filter },
      });
    } catch (error) {
      console.error(error);
      throw new InternalServerErrorException(error.message);
    }
  }
}
