import { Transform, Type } from 'class-transformer';
import {
  Allow,
  IsBoolean,
  IsNumberString,
  IsOptional,
  IsString,
} from 'class-validator';

export class UpdateDesignationMappingDto {
  @IsBoolean()
  @IsOptional()
  is_active: boolean;

  @IsNumberString()
  @IsOptional()
  role_id: number;

  @IsString()
  @IsOptional()
  @Transform(({ value }) => value.toLowerCase().trim())
  designation: string;
}

export class UpdateFilterDto {
  @Allow()
  @Type(() => Number)
  id: number;
}
