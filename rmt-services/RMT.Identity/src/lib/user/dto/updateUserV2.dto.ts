import { IsBoolean, IsOptional, IsString, IsArray } from 'class-validator';
import { OmitType } from '@nestjs/mapped-types';
import { UpdateUserDto } from './updateUser.dto';

export class UpdateUserV2Dto extends OmitType(UpdateUserDto, [
  'roles',
] as const) {
  @IsOptional()
  @IsArray()
  roles: string[];
}
