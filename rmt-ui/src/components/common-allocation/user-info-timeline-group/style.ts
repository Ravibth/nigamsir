import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const VerticalCenterAlignSxProps: SxProps = {
  display: "flex !important",
  alignItems: "center !important",
};

export const HorizontalCenterAlignSxProps: SxProps = {
  display: "flex !important",
  justifyContent: "center !important",
};

export const HorizontalRightAlignSxProps: SxProps = {
  display: "flex !important",
  justifyContent: "right !important",
};

export const PersonRemoveSharpIconSxProps: SxProps = {
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  ":disabled": { color: "#c5c2c2 !important" },
};
