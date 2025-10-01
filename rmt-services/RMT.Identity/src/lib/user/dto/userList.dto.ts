import { IsArray, IsNumberString } from 'class-validator';
import { UserModel } from '../models/users.model';

export class UserListDto {
  @IsArray()
  rows: UserModel[];

  @IsNumberString()
  count: number;
}
