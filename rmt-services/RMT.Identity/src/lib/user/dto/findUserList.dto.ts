/* eslint-disable @typescript-eslint/no-inferrable-types */
import { Transform, Type } from 'class-transformer';
import {
  IsBoolean,
  IsNumberString,
  IsOptional,
  IsString,
  IsArray,
  IsAlpha,
} from 'class-validator';

export class FindUserDto {
  @IsOptional()
  @Transform(({ value }) => value * 1)
  @IsNumberString()
  limit: number | null = null;

  @IsOptional()
  @Transform(({ value }) => value * 1)
  @IsNumberString()
  skip: number | null = null;

  @IsOptional()
  @IsString()
  emp_code?: string;

  @IsOptional()
  @IsString()
  @Transform(({ value }) => `%${value}%`)
  name?: string;

  @IsOptional()
  @IsArray()
  @Transform(({ value }) => value.split(','))
  roles?: string[];

  @IsOptional()
  @IsString()
  location?: string;

  @IsOptional()
  @IsString()
  service_line?: string;

  @IsOptional()
  @Transform(({ value }) => value === 'true')
  @IsBoolean()
  status?: boolean;

  @IsOptional()
  @IsArray()
  @Transform(({ value }) => value.split(','))
  email_id?: string[];

  @IsOptional()
  @IsString()
  include_sa?: string; //Include Super admin ?

  @IsOptional()
  @Transform(({ value }) =>
    typeof value === 'string' ? value.split(',') : value,
  )
  @IsArray()
  designation?: string[];
}
