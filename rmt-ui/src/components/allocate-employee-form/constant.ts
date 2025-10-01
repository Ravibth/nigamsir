import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";
import { IAllocation } from "../update-allocation/entity/IAllocations";

export const BtnAllocate: SxProps = {
  textTransform: "initial",
  fontSize: "16px",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const Btncancel: SxProps = {
  textTransform: "initial",
  // paddingLeft: "200px",
  fontSize: "16px",
  color: "grey",
  "&:hover": {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};
export const Textbox: SxProps = {
  width: "100px",
};

export const BtnAddMore: SxProps = {
  textTransform: "initial",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const Title: SxProps = {
  fontSize: "20px",
  color: "grey",
};

export const TextboxDescription: SxProps = {
  width: "100%",
};

export interface allocationDetails {
  allocationType: string;
  description: string;
  totalEfforts: number;
  skills: [];
  allocation: allocation[];
}
export interface allocation {
  startDate: Date;
  endDate: Date;
  effortsPerDay: number;
}

export const initialEntry: IAllocation = {
  confirmedAllocationStartDate: undefined,
  confirmedAllocationEndDate: undefined,
  confirmedPerDayHours: 0,
  totalWorkingDays: 0,
  isactive: true,
  id: 0,
  index: 0,
  isPerDayHourAllocation: false,
};
