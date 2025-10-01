import { IsOptional, IsString } from 'class-validator';

export class SupercoachDelegateDto {
  @IsString()
  supercoach_mid: string;
  @IsString()
  email_id: string;
  @IsString()
  name: string;
  @IsString()
  @IsOptional()
  designation: string | null;
  @IsString()
  @IsOptional()
  location: string | null;
  @IsString()
  @IsOptional()
  business_unit: string | null;
  @IsString()
  @IsOptional()
  competencyId: string | null;
  @IsString()
  @IsOptional()
  grade: string | null;
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
