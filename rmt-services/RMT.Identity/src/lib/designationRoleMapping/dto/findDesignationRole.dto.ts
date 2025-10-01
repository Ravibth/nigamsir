import { IsNumberString, IsOptional, IsString } from 'class-validator';

export class FindDesignationDto {
  @IsOptional()
  @IsNumberString()
  role_id?: number;

  @IsOptional()
  @IsString()
  designation?: string;
}
