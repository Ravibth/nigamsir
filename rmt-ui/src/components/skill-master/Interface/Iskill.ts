import { IskillMapping } from "./IskillMapping";

export interface Iskill {
  skillCode?: string;
  skillName: string;
  skillCategory: string;
  basic: string;
  intermediate: string;
  advanced: string;
  expert: string;
  description: string;
  createdBy: string;
  isEnable: boolean;
  mapping: Array<IskillMapping>;
}
