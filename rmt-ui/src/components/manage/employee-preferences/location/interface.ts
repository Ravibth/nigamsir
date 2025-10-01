import { IEmployeePreferenceList } from "../../interface";
import { IBuMappingPreferenceList } from "../area-of-experties/interface";

export interface ILocationPreferenceProps {
  employeePreference: IEmployeePreferenceList;
  rowData: ILocationPreferenceList;
  maxNumberOfPreference: number;
  setRowData: React.Dispatch<React.SetStateAction<ILocationPreferenceList>>;
}

export interface ILocationOption {
  location_id?: string;
  location_mid?: string;
  location_name?: string;
  region_name?: string;
}

export type ILocationOptionList = ILocationOption[];

export interface ILocationPreferenceOptions {
  locationOptions?: ILocationOptionList;
}

export interface ILocationPreference {
  id?: number;
  employeeEmail: string;
  preferenceOrder: number;
  isActive?: boolean;
  category?: string;
  location?: ILocationOption;
}

export type ILocationPreferenceList = ILocationPreference[];
