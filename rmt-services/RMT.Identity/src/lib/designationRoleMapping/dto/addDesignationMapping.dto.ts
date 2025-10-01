import { Transform } from 'class-transformer';
import { IsNotEmpty, IsNumberString, IsString } from 'class-validator';

export class AddDesignationMapping {
  @IsString()
  @IsNotEmpty()
  @Transform(({ value }) => value.toLowerCase().trim())
  designation: string;

  @IsNumberString()
  @IsNotEmpty()
  role_id: number;
}
