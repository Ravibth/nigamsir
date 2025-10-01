import {
  IsBoolean,
  IsDateString,
  IsEnum,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import {
  WorkflowOutCome,
  WorkflowStatus,
  WorklFlowModule,
  WorklFlowSubModule,
} from "../enum";
export class GetHolidaysDto {
  @IsOptional()
  @IsString()
  holiday_name: string;
  @IsOptional()
  @IsString()
  holiday_type: string;
  @IsOptional()
  @IsString()
  location_id: string;
  @IsOptional()
  @IsString()
  location_name: string;
  @IsOptional()
  @IsDateString()
  holiday_date: Date;
  @IsOptional()
  @IsDateString()
  cr_date: Date;
  @IsOptional()
  @IsBoolean()
  isactive: boolean;
  @IsOptional()
  @IsDateString()
  createdat: Date;
  @IsOptional()
  @IsDateString()
  modifiedat: Date;
  @IsOptional()
  @IsString()
  createdby: string;
  @IsOptional()
  @IsString()
  modifiedby: string;
}
