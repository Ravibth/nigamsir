import {
  IBuOption,
  IOfferingOption,
  ISolutionOption,
} from "./employee-preferences/area-of-experties/interface";
import {
  IIndustryOption,
  ISubIndustryOption,
} from "./employee-preferences/industry/interface";
import { ILocationOption } from "./employee-preferences/location/interface";

export interface IEmployeePreference {
  id: number;
  employeeEmail: string;
  preferenceName?: string;
  preferenceId?: string;
  category: string;
  preferenceInfo: string; // JSON stored as a string
  preferenceOrder: number;
  createdAt: Date;
  modifiedAt?: Date;
  createdBy: string;
  modifiedBy?: string;
  isActive: boolean;
  preferenceDetails: PreferenceDetails;
}

export interface PreferenceDetails {
  businessUnit?: IBuOption | null;
  offering?: IOfferingOption | null;
  solution?: ISolutionOption | null;
  location?: ILocationOption | null;
  industry?: IIndustryOption | null;
  subIndustry?: ISubIndustryOption | null;
  year_of_experience?:number;
  description:string;
}

export type IEmployeePreferenceList = IEmployeePreference[];
