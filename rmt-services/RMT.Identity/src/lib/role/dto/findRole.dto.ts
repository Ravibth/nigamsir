import { IsArray, IsBoolean, IsNumberString } from 'class-validator';

export class FindRoleDto {
  role_name?: string[];

  is_display?: false;

  id?: number;
}
