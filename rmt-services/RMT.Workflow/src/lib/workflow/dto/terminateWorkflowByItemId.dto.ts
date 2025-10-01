import { IsIn, IsString } from "class-validator";
import { EEngagementStatus } from "../enum";

export class terminateWorkflowByItemIdDTO {
  @IsString()
  ItemId: string;
  @IsIn(Object.values(EEngagementStatus))
  WorkflowStatus: EEngagementStatus;
}
