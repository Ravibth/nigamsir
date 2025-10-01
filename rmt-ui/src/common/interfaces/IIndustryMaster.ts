export interface IIndustryMaster {
  id?: number;
  industry_id?: string;
  industry_name?: string;
  sub_industry_id?: string;
  sub_industry_name?: string;
}

export type IIndustryMasterList = IIndustryMaster[];
