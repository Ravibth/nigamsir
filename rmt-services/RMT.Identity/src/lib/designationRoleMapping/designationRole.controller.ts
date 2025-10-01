import {
  Body,
  Controller,
  Get,
  Post,
  Query,
  UseFilters,
  Param,
  Put,
} from '@nestjs/common';

import { DesignationRoleService } from './designationRole.service';
import { AddDesignationMapping } from './dto/addDesignationMapping.dto';
import { FindDesignationDto } from './dto/findDesignationRole.dto';
import {
  UpdateDesignationMappingDto,
  UpdateFilterDto,
} from './dto/updateDesignationMapping.dto';
import { DesignationRoleModel } from './models/designationRole.model';

@Controller('designationRole')
export class DesignationRoleController {
  constructor(private readonly roleService: DesignationRoleService) {}

  @Post('v1')
  async addDesignationMapping(
    @Body() params: AddDesignationMapping,
  ): Promise<DesignationRoleModel> {
    try {
      return await this.roleService.addDesignationMapping(params);
    } catch (err) {
      throw err;
    }
  }

  @Get('v1')
  async findDesignationMapping(
    @Query() params: FindDesignationDto,
  ): Promise<DesignationRoleModel[]> {
    try {
      return await this.roleService.getRolesByQuery(params);
    } catch (err) {
      throw err;
    }
  }

  @Put('v1/:id')
  async updateDesignationMapping(
    @Param() filter: UpdateFilterDto,
    @Body() params: UpdateDesignationMappingDto,
  ): Promise<DesignationRoleModel> {
    try {
      console.log(params, filter);
      return await this.roleService.updateDesignationRoleMapping(
        filter,
        params,
      );

      // return {} as any;
    } catch (err) {
      throw err;
    }
  }
}
