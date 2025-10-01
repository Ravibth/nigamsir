import { IsArray, IsIn, IsOptional } from "class-validator";
import { WorkflowOutCome } from "../enum";
import { Transform } from "class-transformer";

export class getMultipleTasksByByQueryDto {
  @IsArray()
  @Transform(({ value }) => (Array.isArray(value) ? value : value.split(",")))
  item_id: string[];

  @IsIn(Object.values(WorkflowOutCome))
  @IsOptional()
  outcome?: WorkflowOutCome;
}
