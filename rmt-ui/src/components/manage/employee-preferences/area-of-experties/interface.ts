import { IEmployeePreferenceList } from "../../interface";

export interface IBuOption {
  buId: string;
  buName: string;
  buMid?: string;
}

export type IBuOptionList = IBuOption[];

export interface IOfferingOption {
  offeringId: string;
  offeringName: string;
  offeringMid: string;
}

export type IOfferingOptionList = IOfferingOption[];

export interface ISolutionOption {
  solutionId: string;
  solutionName: string;
  solutionMid: string;
}

export type ISolutionOptionList = ISolutionOption[];

export interface IBuMappingPreferenceOptions {
  buOptions?: IBuOptionList;
  offeringOptions?: IOfferingOptionList;
  solutionOptions?: ISolutionOptionList;
}

export interface IBuMappingPreference {
  id?: number;
  employeeEmail: string;
  preferenceOrder: number;
  isActive?: boolean;
  category?: string;
  businessUnit?: IBuOption;
  offering?: IOfferingOption;
  solution?: ISolutionOption;
}

export type IBuMappingPreferenceList = IBuMappingPreference[];

export interface IBuMappingPreferenceProps {
  employeePreference: IEmployeePreferenceList;
  rowData: IBuMappingPreferenceList;
  maxNumberOfPreference: number;
  setRowData: React.Dispatch<React.SetStateAction<IBuMappingPreferenceList>>;
}
