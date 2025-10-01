import { Controller } from '@nestjs/common';
import { MessagePattern, Payload } from '@nestjs/microservices';
import { IUser } from '../../common/decorators/user.decorator';
import { FindUserDto } from './dto/findUserList.dto';
import { UserListDto } from './dto/userList.dto';
import { UserModel } from './models/users.model';
import { EUserCommandNames } from './user.constant';
import { UserService } from './user.service';

@Controller()
export class UserTransportController {
  constructor(private readonly userService: UserService) {}

  @MessagePattern({ cmd: EUserCommandNames.GET_ALL_USERS })
  async getAllUsers(
    @Payload('params') params: FindUserDto,
    @Payload('user') user: IUser,
  ): Promise<UserModel[]> {
    try {
      console.log(params);
      const users = await this.userService.getAllUsers(params, user);
      console.log(users.rows);
      return users.rows || [];
    } catch (error) {
      throw error;
    }
  }

  @MessagePattern({ cmd: EUserCommandNames.GET_ALL_USER_DETAILS })
  async getAllUserDetails(
    @Payload('params') params: FindUserDto,
  ): Promise<UserModel[]> {
    try {
      const users = await this.userService.getUserByEmailForMicro(params);
      return users || [];
    } catch (error) {
      throw error;
    }
  }
}
