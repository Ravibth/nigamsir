import {
  IsDateString,
  IsIn,
  IsOptional,
  IsString,
  IsUUID,
} from "class-validator";
import { EEngagementStatus } from "../enum";

export class updateAllocationStatusDTO {
  @IsUUID()
  @IsOptional()
  Guid: string;
  @IsOptional()
  @IsIn(Object.values(EEngagementStatus))
  AllocationStatus: EEngagementStatus;
}
