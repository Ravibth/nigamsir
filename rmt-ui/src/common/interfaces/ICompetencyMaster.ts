export interface ICompetencyMaster {
  competencyId: string;
  competency: string;
  isActive?: boolean;
  createdat?: Date;
  modifiedat?: Date;
  createdby?: string;
  modifiedby?: string;
  // competencyMID?: string;
  competencyLeaderMID?: string;
  buId?: string;
}
