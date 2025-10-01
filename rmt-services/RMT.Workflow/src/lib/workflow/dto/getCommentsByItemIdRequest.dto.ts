import { IsArray } from "class-validator";
import { Transform } from "class-transformer";

export class GetCommentsByItemIdRequest {
  @IsArray()
  @Transform(({ value }) => (Array.isArray(value) ? value : value.split(",")))
  itemId: string[];
}

export class GetCommentsByItemIdResponse {
  itemId: string;
  task_list: [];
}
