import { IsOptional, IsString } from "class-validator";

export class RefreshWorkflowTaskAssignment {
  @IsString()
  @IsOptional()
  previousAssignTo?: string;
  @IsString()
  @IsOptional()
  currentAssignTo?: string;
  @IsOptional()
  @IsString()
  employeeEmail?: string;
  @IsOptional()
  @IsString()
  pipelineCode?: string;
  @IsOptional()
  @IsString()
  jobCode?: string;
  @IsString()
  type: string;
}
