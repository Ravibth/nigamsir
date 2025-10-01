import { IsArray, IsEmail, IsNotEmpty } from 'class-validator';

export class AddUserRoles {
  @IsNotEmpty()
  @IsEmail()
  email_id: string;
  @IsNotEmpty()
  @IsArray()
  roles: string[];
}
