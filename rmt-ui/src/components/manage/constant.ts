import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const TabLabelSXProps: SxProps = {
  color: "#4f2d7f !important",
  fontWeight: "bold",
  textTransform: "none",
  fontSize: "17px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const SaveButtonSxProps: SxProps = {
  backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  color: "white",
  width: "120px",
  fontFamily: "GT Walsheim Pro !important",
  fontSize: "16px !important",
  "&:hover": {
    backgroundColor: GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
    color: "white",
  },
  borderRadius: "10px !important",
  textTransform: "capitalize !important",
};
export const PreferencesLength = 8;

// width: 185px;
// padding: 12px 10px;
export const EmployeePreferencesHeaderSxProps: SxProps = {
  // color: "#5A5A5A",
  fontSize: "20px",
  // marginLeft: "20px",
  fontWeight: "550",
  color: GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
};

export const PreferenceInfoIcon: SxProps = {
  marginTop: "6px",
  marginLeft: "10px",
};

export const PreferenceCategories = {
  LOCATION: "LOCATION",
  BU_TREE_MAPPING: "BU_TREE_MAPPING",
  INDUSTRY_MAPPING: "INDUSTRY_MAPPING",
};
export enum ConfigurationParameterConstants {
  MAX_NUMBER_OF_PREFERENCES = "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen",
  CONFIG_TYPE_GLOBAL = "GLOBAL",
  CONFIG_TYPE = "EXPERTISE",
}
