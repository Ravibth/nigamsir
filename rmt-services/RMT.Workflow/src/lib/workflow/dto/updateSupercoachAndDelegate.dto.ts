import { IsOptional, IsString } from "class-validator";

export class UpdateSupercoachAndDelegateDto {
  @IsString()
  supercoach_mid: string;
  @IsString()
  supercoach_email: string;
  @IsOptional()
  @IsString()
  prev_allocation_delegate_email: string;
  @IsOptional()
  @IsString()
  prev_allocation_delegate_mid: string;
  @IsOptional()
  @IsString()
  new_allocation_delegate_email: string;
  @IsOptional()
  @IsString()
  new_allocation_delegate_mid: string;
  @IsOptional()
  @IsString()
  new_allocation_delegate_name: string;
  @IsOptional()
  @IsString()
  prev_skill_delegate_email: string;
  @IsOptional()
  @IsString()
  prev_skill_delegate_mid: string;
  @IsOptional()
  @IsString()
  new_skill_delegate_email: string;
  @IsOptional()
  @IsString()
  new_skill_delegate_mid: string;
  @IsOptional()
  @IsString()
  new_skill_delegate_name: string;
  @IsString()
  status: string;
  @IsString()
  outcome: string;
}
