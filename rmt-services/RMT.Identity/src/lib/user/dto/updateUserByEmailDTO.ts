import { IsEmail, IsOptional, IsString } from 'class-validator';

export class UpdateUserByEmailDTO {
  @IsString()
  @IsOptional()
  emailId: string;
}
