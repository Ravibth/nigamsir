import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Put,
  Query,
  Post,
  Request,
  UseFilters,
  UseGuards,
  UsePipes,
  Logger,
} from '@nestjs/common';
import { ValidatePayloadExistsPipe } from '../../common/validationPipes/payload.pipe';
import { HttpExceptionFilter } from '../../common/filters/http-exception.filter';
import { UpdateUserDto } from './dto/updateUser.dto';
import { UserService } from './user.service';
import { UserModel } from './models/users.model';
import { UserListDto } from './dto/userList.dto';
import { FindUserDto } from './dto/findUserList.dto';
import { IUser, User } from '../../common/decorators/user.decorator';
import { FindAndUpsertUserDto } from './dto/findAndUpsert.dto';
import { AddUserDto, getUserByNameOrEmailV6Request } from './dto/addUser.dto';
import { UpdateUserV2Dto } from './dto/updateUserV2.dto';
import { MessagePattern, Payload } from '@nestjs/microservices';
import { EMicroservicesCommand, EMicroServicesNames } from 'src/common/enum';
import { IUserQueryCommand } from 'src/user.command';
import { UserDetailsWCGTDto } from './dto/userDetailWCGT.dto';
import { UserInfoDTO } from './dto/userInfo.dto';
import { UpdateUserByEmailDTO } from './dto/updateUserByEmailDTO';
import { IsPositive } from 'class-validator';
import { AddUserRoles } from './dto/addUserRoles.dto';
import { FindModulePermissionDto } from '../modulePermission/dto/findModulePermission.dto';
import { ModulePermissionService } from '../modulePermission/modulePermission.service';
import { UserDTO } from './dto/user.dto';
import { RemoveUserRoleByEmailDto } from './dto/removeUserRoleByEmailDTO.dto';
import { AddSupercoachDelegateDto } from './dto/addSupercoachDelegate.dto';
import { SuperCoachDelegateListDto } from './dto/supercoachDelegateList.dto';

const LOG_CONTEXT = 'identity-user-service';

@Controller('user')
export class UserController {
  constructor(
    private readonly userService: UserService,
    private readonly modulePermissionService: ModulePermissionService,
    private readonly logger: Logger,
  ) {}

  @Get('health')
  health(): { status: string } {
    try {
      return { status: 'OK' };
    } catch (error) {
      throw error;
    }
  }
  @Get('supercoach-user-list-by-allocation-supercoach-delegate')
  async GetSupercoachUserListByAllocationSupercoachDelegate(
    @Query('email') email: string,
  ) {
    try {
      var resp =
        await this.userService.GetSupercoachUserListByAllocationSupercoachDelegate(
          email,
        );
      return resp;
    } catch (error) {}
  }
  @Post('supercoach-and-delegates-list')
  async getSupercoachAndDelegatesList(
    @Body() params: SuperCoachDelegateListDto,
  ) {
    try {
      return await this.userService.getSupercoachAndDelegatesList(params);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Post('add-supercoach-delegate')
  async addSupercoachDelegate(
    @Body() param: AddSupercoachDelegateDto,
    @User() user: IUser,
  ) {
    try {
      return await this.userService.addSupercoachDelegate(param, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Get('email/:emailId/:upsert?')
  async getUserByEmail(
    @Param() param: FindAndUpsertUserDto,
    @User() loggedINUser: IUser,
  ): Promise<UserModel> {
    try {
      const user = await this.userService.getUserByEmail(
        param,
        [],
        loggedINUser,
      );

      return user;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Post('GetUsersByEmails')
  async GetUsersByEmails(@Body() param: string[]) {
    try {
      const users = await this.userService.getUsersByUserEmails(param);
      return users;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Post('GetUsersBySuperCoachEmails')
  async GetUsersBySuperCoachEmails(@Body() param: string[]) {
    try {
      const users = await this.userService.getUsersBySuperCoachEmails(param);
      return users;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Get('status/v1')
  async getUserByQuery(
    @Query() param: any,
    @User() loggedINUser: IUser,
  ): Promise<UserModel> {
    try {
      const roles = await this.userService.getUserForAuth(param, loggedINUser);

      return roles;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('status/v2')
  async getUserWithModulePermissionByQuery(
    @Query() param: any,
    @User() loggedINUser: IUser,
  ): Promise<UserModel> {
    try {
      let userInfo: any;
      if (
        param.email_id != undefined &&
        param.email_id !== '' &&
        param.email_id !== null
      ) {
        userInfo = await this.userService.getUserForAuth(param, loggedINUser);

        if (userInfo) {
          const param2: FindModulePermissionDto = {
            emailId: userInfo.uemail_id,
            roleName: userInfo.roles,
          };
          const permissions =
            await this.modulePermissionService.getRoleModulePermissionsV2(
              param2,
              loggedINUser,
            );
          userInfo['app_permissions'] = permissions;
        } else {
          userInfo['app_permissions'] = [];
        }
      }

      return userInfo;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Get('supercoach-delegate-by-supercoach-mid')
  async getSuperCoachAndDelegateBySupercoachMid(
    @Query('supercoach_mid') supercoach_mid,
  ) {
    try {
      return this.userService.getSuperCoachAndDelegateBySupercoachMid(
        supercoach_mid,
      );
    } catch (error) {
      throw error;
    }
  }

  @Get('multiuser/v1')
  async getUserByEmailForMicro(@Query() query): Promise<UserModel[]> {
    try {
      return await this.userService.getUserByEmailForMicro(query);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Post('v1')
  async addUser(
    @Body() param: AddUserDto,
    @User() loggedINUser: IUser,
  ): Promise<UserModel> {
    try {
      const user = await this.userService.addUser(param, loggedINUser);

      return user;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Put('/update')
  @UsePipes(new ValidatePayloadExistsPipe())
  async updateUser(
    @Query() param: UpdateUserByEmailDTO,
    @Body() params: UpdateUserDto,
    @User() user: IUser,
  ): Promise<UserModel> {
    try {
      return await this.userService.updateUserRoles(
        param.emailId,
        params,
        user,
      );
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Put('v2/:emailId')
  @UsePipes(new ValidatePayloadExistsPipe())
  async updateUserV2(
    @Param() param,
    @Body() params: UpdateUserV2Dto,
    @User() user: IUser,
  ): Promise<UserDTO> {
    try {
      return await this.userService.updateUser(param.emailId, params, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Post('v1/bulkAddUserRoles')
  async addUserRolesBulk(@Body() params: AddUserRoles[], @User() user: IUser) {
    try {
      return await this.userService.bulkUpdateRole(params, user);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Delete('/:roleUserId')
  async deleteUser(@Param() param) {
    try {
      const roles = await this.userService.deleteUser(param.roleUserId);
      return roles;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('role/:roleId')
  async getUsersByRole(
    @Param() param,
    @User() user: IUser,
  ): Promise<UserModel[]> {
    try {
      const roles = await this.userService.getUsersByRole(param.roleId, user);
      return roles;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('users/:roleName')
  async getUsersByRoleV2(
    @Param() param,
    @User() user: IUser,
  ): Promise<UserModel[]> {
    try {
      const roles = await this.userService.getUsersByRoleV2(
        param.roleName,
        user,
      );
      return roles;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('all')
  async getAllUsers(
    @User() user: IUser,
    @Query() param: FindUserDto,
  ): Promise<UserListDto> {
    try {
      // const param1: FindUserDto = new FindUserDto();
      // param1.skip = 0;
      // param1.limit = 25;
      //param1.name = 'Saif';
      //param1.email_id =['saif@email.com'];
      //param1.roles = ['Admin', 'Approvers'];

      const users = await this.userService.getAllUsers(param, user);
      return users;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('list')
  async getAllUsersList(@User() user: IUser): Promise<any> {
    try {
      const users = await this.userService.getAllUsersList(user);
      return users;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Get('allusersList')
  async getListOfAllUser(): Promise<any> {
    try {
      const users = await this.userService.getListOfAllUser();
      return users;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
    }
  }

  ///Get user details from WCGT
  @Get('GetEmpDetailsFromWCGT')
  async getEmpDetailsFromWCGT(
    @Query('emailId') param: string,
    @User() user: IUser, //todo: get from req header
  ): Promise<UserDetailsWCGTDto> {
    try {
      const userDetail = this.userService.getEmpDetailsFromWCGT(param);
      return userDetail;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  ///Update user for active inactive flag
  @Put('v3/:emailId')
  @UsePipes(new ValidatePayloadExistsPipe())
  async updateUserStatus(
    @Param() param,
    @Body() params: any,
    // @User() user: IUser,
  ): Promise<boolean> {
    try {
      let user: IUser; //todo: remove Temporary object get from req header
      return await this.userService.updateUserStatus(
        param.emailId,
        params.isActive,
        user,
      );
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
  @Post('AddUserRoleIfNotExist')
  async addUserRoleIfNotExist(): Promise<void> {
    try {
      //WIP
    } catch (error) {}
  }

  //check if user present in rmt or wcgt,then return it from get api
  @Get('v4/:emailId/:upsert?')
  async getandcheckUserByEmail(
    @Param() param: FindAndUpsertUserDto,
    @User() loggedINUser: IUser,
  ): Promise<UserInfoDTO> {
    try {
      const user = await this.userService.getandcheckUserByEmail(
        param,
        [],
        loggedINUser,
      );

      return user;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Get('v5/:name')
  async getUserByNameOrEmail(@Param() param: any): Promise<Array<any>> {
    try {
      const user = await this.userService.getUserByNameOrEmail(param, []);

      return user;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Post('v6')
  async getUserByNameOrEmailV6(
    @Body() param: getUserByNameOrEmailV6Request,
  ): Promise<Array<any>> {
    try {
      const user = await this.userService.getUserByNameOrEmail(param, []);

      return user;
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }

  @Put('removeUserRoleByEmail')
  async removeUserRoleByEmail(@Body() params: RemoveUserRoleByEmailDto[]) {
    try {
      return await this.userService.removeUserRoleByEmail(params);
    } catch (error) {
      this.logger.error(error, LOG_CONTEXT);
      throw error;
    }
  }
}
