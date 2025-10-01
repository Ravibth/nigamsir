import { IsOptional, IsString } from "class-validator";

export class terminateWorkflowByPipelineCodeAndJobCodeDTO {
  @IsString()
  PipelineCode: string;
  @IsString()
  @IsOptional()
  JobCode?: string;
}
