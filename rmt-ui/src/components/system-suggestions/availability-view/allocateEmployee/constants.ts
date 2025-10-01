import { SxProps } from "@mui/material";
import * as globalconstants from "../../../../global/constant";

export interface IAllocations {
  id: number;
  start_date: Date | null;
  end_date: Date | null;
  efforts: number | null;
}
export interface IAllocationsEntries {
  startDate: Date | null;
  endDate: Date | null;
  effort: number | null;
}
export const TooltipBoxsxprop: SxProps = {
  position: "fixed",
  top: "30%",
  left: "25%",
  width: "50%",
  height: "50%",
  color: "white",
  zIndex: 47,
  marginTop: "-20px",
  display: "inline-flex",
};
export const TooltipNavigationTypographysxprop: SxProps = {
  zIndex: 45,
  marginRight: "-10px",
};
export const TooltipNavigationsxprop: SxProps = {
  //   color: "white",
  marginTop: "40px",
  transform: "rotate(-90deg)",
};
export const TooltipDetailsSxProps: SxProps = {
  backgroundColor: "white",
  color: "black",
  border: "1px solid gray",
  padding: "20px",
  width: "100%",
  height: "100%",
};
export const CloseIconSxProps: SxProps = {
  color: globalconstants.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
export const CloseButtonSxProps: SxProps = {
  display: "flex",
  justifyContent: "flex-end",
};
export const allocateDetails = {
  padding: "20px 0px",
  color: globalconstants.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  fontSize: "20px",
};
export interface ISubmitAllocations {
  startDate: Date;
  endDate: Date;
  effort: number;
}
