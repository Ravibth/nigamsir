export interface ISkillsMaster {
  skill_Id?: number;
  skillName?: string;
  skillCode?: string;
  skillCategory?: string;
  description?: string;
  basic?: string;
  intermediate?: string;
  advanced?: string;
  expert?: string;
  type?: string;
  skill_Mapping?: ISkillMapping[];
  isActive?: boolean;
  isEnable?: boolean;
  createDate?: Date;
  modifieDate?: Date;
  createdBy?: string;
  modifiedBy?: string;
}

export interface ISkillMapping {
  id: number;
  skill_Id: number;
  skill: ISkillsMaster;
  businessUnit: string;
  competency: string;
  competencyId: string;
  // experties: string;
  designation: string[];
  isActive: boolean;
  createDate: Date;
  modifieDate: Date;
  createdBy: string;
  modifiedBy: string;
}
