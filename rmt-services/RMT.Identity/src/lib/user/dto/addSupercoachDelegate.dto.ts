import { IsOptional, IsString } from 'class-validator';

export class AddSupercoachDelegateDto {
  @IsString()
  supercoach_mid: string;
  @IsString()
  supercoach_email: string;
  @IsString()
  @IsOptional()
  allocation_delegate_mid: string | null;
  @IsString()
  @IsOptional()
  allocation_delegate_email: string | null;
  @IsString()
  @IsOptional()
  allocation_delegate_name: string | null;
  @IsString()
  @IsOptional()
  skill_delegate_mid: string | null;
  @IsString()
  @IsOptional()
  skill_delegate_email: string | null;
  @IsString()
  @IsOptional()
  skill_delegate_name: string | null;
}
