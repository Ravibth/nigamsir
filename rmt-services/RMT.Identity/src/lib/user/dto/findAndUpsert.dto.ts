import { IsEmail, IsIn, IsOptional, IsString } from 'class-validator';

export class FindAndUpsertUserDto {
  @IsEmail()
  emailId: string;

  @IsIn(['true', 'false'])
  @IsOptional()
  @IsString()
  upsert?: string;
}
