import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const TabPanelSxProps: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const TabLabelSXProps: SxProps = {
  color: "#4f2d7f !important",
  fontWeight: "bold",
  textTransform: "none",
  fontSize: "17px",
  // fontFamily: "GT Walsheim Pro",
};

export const TabLabelHideSXProps: SxProps = {
  display: "none",
};

export enum TabsTitleEnum {
  DetailView = 0,
  CalenderView = 1,
  BudgetDetails = 2,
  Allocations = 3,
  Requisitions = 4,
  MarketplaceInterests = 5,
}
