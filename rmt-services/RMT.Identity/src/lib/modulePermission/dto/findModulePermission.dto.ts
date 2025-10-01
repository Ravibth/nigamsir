import { Transform } from 'class-transformer';
import {
  IsOptional,
  IsEmail,
  IsArray,
  IsNumberString,
  Matches,
} from 'class-validator';

export class FindModulePermissionDto {
  @IsOptional()
  @IsNumberString()
  roleId?: number;

  @IsOptional()
  @Transform(({ value }) => value.split(','))
  @IsArray()
  roleName?: string[];

  @IsOptional()
  @IsEmail()
  emailId?: string;
}
