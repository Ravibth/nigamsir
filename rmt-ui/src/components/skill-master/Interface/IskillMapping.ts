import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";

export interface IskillMapping {
  // businessUnit: string;
  competency: ICompetencyMaster;
  designation: Array<string>;
}
