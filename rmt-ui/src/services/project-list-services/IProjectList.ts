export interface IProjectList {
  userEmail: string;
  limit: number;
  pagination: number;
  searchQuery?: string;
  searchRoles?: string[];
  orderBy?: string;
  filterQueryParameters: IFilterQueryParameters;
}

export default interface IFilterQueryParameters {
  Bu?: Array<string>;
  Expertises?: Array<string>;
  Offerings?: Array<string>;
  Solutions?: Array<string>;
  Smes?: Array<string>;
  Industry?: Array<string>;
  SubIndustry?: Array<string>;
  RevenueUnit?: Array<string>;
  ClientNames?: Array<string>;
  PipelineCodes?: Array<string>;
  JobCodes?: Array<string>;
  JobName?: Array<string>;
  ProjectStatus?: Array<string>;
  ProjectType?: string;
  MarketPlace?: boolean;
  ProjectChargeType?: string;
  IsAllocatedHoursRequired?: boolean;
}
