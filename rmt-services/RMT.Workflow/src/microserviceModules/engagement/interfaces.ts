import { UpdateEngagementByCEODto } from "../../lib/workflow/dto/updateWorkflow.dto";

export interface IEngagementUpdate {
  id: string;

  toUpdate?: Record<string, any>;

  coe_acceptance_meta?: UpdateEngagementByCEODto;
}

export interface IKpiExecutionUpdate {
  id: string;

  status: string;
}
