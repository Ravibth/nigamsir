import { Transform } from 'class-transformer';
import { IsBoolean, IsOptional, IsString, IsArray } from 'class-validator';

export class UpdateUserDto {
  @IsOptional()
  @Transform(({ value }) => value.split(','))
  @IsArray()
  roles: string[];

  @IsOptional()
  @IsString()
  role_ids: string;

  // @IsOptional()
  // @IsString()
  // emp_id: string;

  @IsOptional()
  @IsString()
  emp_code: string;

  @IsOptional()
  @IsBoolean()
  status: boolean;
}
