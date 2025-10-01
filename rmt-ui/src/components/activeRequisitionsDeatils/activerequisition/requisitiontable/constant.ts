import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../../global/constant";

export const Headings: SxProps = {
  color: "grey",
  alignItems: "center",
  fontWeight: "bold",
  fontSize: "16px",
  // fontFamily: "GT Walsheim Pro ",
};

export const DataRows: SxProps = {
  alignItems: "center",
  color: "black",
  fontWeight: "bold",
  fontSize: "17px",
  // fontFamily: "GT Walsheim Pro ",
};

export const MenuRow: SxProps = {
  // fontFamily: "GT Walsheim Pro ",
  fontSize: "17px",
};

export const SkillsChip: SxProps = {
  borderRadius: "10px", // Adjust the border radius as needed
  backgroundColor: "#ebeaf3", // Change the background color as needed
  border: "1px solid #4f2d7f",
  color: "#4f2d7f",
  fontWeight: "bold",
  margin: "4px",
};

export const SystemSuggestionSxProps: SxProps = {
  mr: 0.5,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GTTealColor,
};

export const DeleteIconSxProps: SxProps = {
  mr: 0.5,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtRedColor,
};

export const UpdateIconSxProps: SxProps = {
  mr: 0.5,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
};

export const AccordionDetailProps: SxProps = {
  padding: "0px !important",
};

export const defaultColDef = {
  lockVisible: true,
  resizable: true,
};

export const gridOptions = {
  suppressCellSelection: true,
};
