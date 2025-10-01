import { IsArray, IsEmail, IsNotEmpty } from 'class-validator';

export class RemoveUserRoleByEmailDto {
  @IsNotEmpty()
  @IsEmail()
  email_id: string;
  @IsNotEmpty()
  @IsArray()
  roles: string[];
}
