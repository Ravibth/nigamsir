import { SxProps } from "@mui/material";
import { IAllocationDetail } from "../allocation-details/IAllocationDetail";

export const allocationData: IAllocationDetail[] = [
  { startDate: "1 July,2023", endDate: "15 July,2023", allocationHours: "4" },
  { startDate: "22 July,2023", endDate: "15 Aug,2023", allocationHours: "4" },
];

export const supervisorsDetails = [
  // { InputLable: "Client Service Partner", Value: "Nabeel Ahmed - GTAR" },
  // { InputLable: "Legal Entity", Value: "Grant thorton bharat LLP" },
  // { InputLable: "Market", Value: "Cross Entity Billing Markets" },
  // { InputLable: "Submarket", Value: "Billing" },
  // { InputLable: "GT Reference Country", Value: "Country" },
];

export const content = `
This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax 
estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and 
values strong internal controls. A person should be able to do Audit and do some finances also and have hands on experience on Legal 
work. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income 
tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, 
and values strong internal controls. A person should be able to do Audit and do some finances also and have hands on experience on 
Legal work
`;

export enum ROLE_TYPES {
  DELEGATE_ROLE = "Delegate",
  ADDITIONAL_EL_ROLE = "AdditionalEL",
}

export const AutocompleteSxProps: SxProps = {
  mt: 1,
  mb: 1,
};
