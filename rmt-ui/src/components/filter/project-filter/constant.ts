import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const DrawerSxProps: SxProps = {
  // height: "calc(100% - 64px)",
  top: 64,
  overflowY: "scroll",
};

export const CloseButtonSxProps: SxProps = {
  borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  marginLeft: "20px",
  textTransform: "initial",
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  },
  fontFamily: GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
  padding: "4px 25px !important",
};

export const ApplyFilterButtonSxProps: SxProps = {
  backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  // marginLeft: "40px",
  textTransform: "initial",
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
    backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
  fontFamily: GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
  padding: "0px 25px !important",
};

export const DividerSxProps: SxProps = {
  borderBottomWidth: 2,
  margin: "10px",
};

export const TypographySxProps: SxProps = {
  color: "black",
  fontSize: "14px",
  paddingBottom: "10px",
};

export const AutocompleteSxProps: SxProps = {
  width: "300px",
  backgroundColor: "#F2F5FF !important",
};

export const filterIconButton: SxProps = () => {
  return {
    color: GT_DESIGN_PARAMETERS.GTTealColor,
    fontSize: "14px",
    marginTop: "15px",
    // fontFamily: "GT Walsheim Pro",
    textTransform: "initial",
    borderRadius: "40px",
    border: "2px solid",
    borderColor: GT_DESIGN_PARAMETERS.GTTealColor,
    "&:hover": {
      borderColor: GT_DESIGN_PARAMETERS.GTTealColor,
      border: "2px solid",
      backgroundColor: "#E9F7FB",
    },
    marginLeft: "5px !important",
  };
};
