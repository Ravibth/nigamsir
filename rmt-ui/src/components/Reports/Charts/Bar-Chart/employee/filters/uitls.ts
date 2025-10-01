import { SxProps } from "@mui/material";
import moment from "moment";
export enum EReportDashboardFilterControl {
  businessUnit = "businessUnit",
  expertise = "expertise",
  smeg = "smeg",
  location = "location",
  start_date = "startDate",
  end_date = "endDate",
}

export interface IReportDashboardFilterOptions {
  businessUnit: string[];
  expertise: string[];
  smeg: string[];
  location: string[];
  // start_date?: Date | null;
}
export interface IReportDashboardFilterControl {
  [EReportDashboardFilterControl.businessUnit]: string[];
  [EReportDashboardFilterControl.expertise]: string[];
  [EReportDashboardFilterControl.smeg]: string[];
  [EReportDashboardFilterControl.location]: string[];
  [EReportDashboardFilterControl.start_date]?: Date | null; //should not be null
  [EReportDashboardFilterControl.end_date]?: Date | null; //should not be null
}

export const filterIconButton: SxProps = {
  color: "#4f2d7f",
  fontSize: "14px",
  textTransform: "initial",
  borderRadius: "40px",
  borderColor: "#B8B8B8",
};

export const DividerSxProps: SxProps = {
  borderBottomWidth: 2,
  margin: "10px",
};

export const GetFilterDefaultValueOnTheBasisOfRole = (
  userRole: string = "Admin"
): IReportDashboardFilterControl => {
  //Get complete user
  //find BU , Exp ,SMEG and set value
  switch (userRole) {
    case "Admin":
      return {
        businessUnit: [],
        expertise: [],
        smeg: [],
        location: [],
        startDate: moment(new Date()).add(-3, "months").toDate(),
        endDate: moment(new Date()).toDate(),
      };
    default:
      return {
        businessUnit: [],
        expertise: [],
        smeg: [],
        location: [],
        startDate: moment(new Date()).add(-3, "months").toDate(),
        endDate: moment(new Date()).toDate(),
      };
  }
};