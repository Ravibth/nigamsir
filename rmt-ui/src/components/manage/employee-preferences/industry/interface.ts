import { IEmployeePreferenceList } from "../../interface";

export interface IIndustryOption {
  industry_id: string;
  industry_name: string;
  year_of_experience: number;
  description: string;
}

export interface IIndustryListOption {
  industry_id: string;
  industry_name: string;
}

export type IIndustryOptionList = IIndustryListOption[];

export interface ISubIndustryOption {
  sub_industry_id: string;
  sub_industry_name: string;
}

export type ISubIndustryOptionList = ISubIndustryOption[];

export interface IIndustryMappingPreferenceOptions {
  industryOptions?: IIndustryOptionList;
  subIndustryOptions?: ISubIndustryOptionList;
}
export interface IIndustryMappingPreference {
  id?: number;
  employeeEmail: string;
  preferenceOrder: number;
  isActive?: boolean;
  category?: string;
  industry?: IIndustryOption;
  subIndustry?: ISubIndustryOption;
}
export type IIndustryMappingPreferenceList = IIndustryMappingPreference[];

export interface IIndustryPreferenceProps {
  employeePreference: IEmployeePreferenceList;
  rowData: IIndustryMappingPreferenceList;
  maxNumberOfPreference: number;
  setRowData: React.Dispatch<
    React.SetStateAction<IIndustryMappingPreferenceList>
  >;
}
