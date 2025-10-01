import { IRequisitionMaster } from "../../common/interfaces/IRequisition";

export interface IPropsCardView {
  requisitionId: string;
  suggestionsSelected: Array<ISystemSuggestions>;
  fetchDetailsConfig: IFetchDetailsConfig;
  setFetchDetailsConfig: any;
  userSuggestions: Array<ISystemSuggestions>;
  requisitionDetails: IRequisitionMaster;
  isLoading: boolean;
  setIsLoading: any;
  fetchSystemSuggestions: any;
  updateSelections: any;
  list_ended: boolean;
}

export interface IFetchDetailsConfig {
  limit: number;
  pagination: number;
}

export interface IScoreBreakup {
  matched_type?: string;
  matching_value?: string;
  parameter?: string;
  value?: number;
}

export interface ISystemSuggestions {
  empName?: string;
  email?: string;
  designation?: string;
  grade?: string;
  location?: string;
  supercoach?: string;
  score?: string;
  score_breakup?: IScoreBreakup[];
  allocations?: string;
  revenue_unit?: string;
  business_unit?: string;
  sector?: string;
  industry?: string;
  skill?: ISkillEntities[];
  interested?: boolean;
  available?: boolean;
  competency?: string;
  competencyId?: string;
}

export enum ERequisitionParametersEnum {
  SAME_CLIENT = "Same_client",
  SKILLS = "Skills",
  SKILL = "Skill",
}

export interface SortingCardsBy {
  type: string;
  value: string;
}

export interface ISkillEntities {
  skillName: string;
  skillCode: string;
  type?: string;
}
