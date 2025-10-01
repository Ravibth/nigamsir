import { IsBoolean, IsOptional, IsString } from 'class-validator';

export class UpdateUserDto {
  @IsOptional()
  @IsString()
  roles: string;

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
