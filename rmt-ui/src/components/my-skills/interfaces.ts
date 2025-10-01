import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { ISkillsMaster } from "../../common/interfaces/ISkillsMaster";

export interface IMySkillsForm {
  skillName: ISkillsMaster;
  proficiency: string;
  comments: string;
  expertise?: string;
  // businessUnit?: string;
  competency?: ICompetencyMaster;
}
export interface IBusinessUnitExpertiseOptions {
  businessUnitOptions: string[];
  competencyOptions: ICompetencyMaster[];
}
