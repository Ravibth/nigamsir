import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const BtnYes: SxProps = {
  backgroundColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  // marginLeft: "40px",
  textTransform: "initial",
  "&:hover": {
    borderColor: "#4f2d7f !important",
    backgroundColor: "#4f2d7f !important",
    color: "#fff !important",
  },
  fontFamily: GlobalConstant.GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
  marginLeft: "10px",
  color: "white",
  fontSize: "17px",
  border: "1px solid #725799",
};

export const BtnNo: SxProps = {
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  marginLeft: "20px",
  textTransform: "initial",
  "&:hover": {
    borderColor: "#4f2d7f !important",
    backgroundColor: "#fff !important",
    color: "#4f2d7f !important",
  },
  fontFamily: GlobalConstant.GT_DESIGN_PARAMETERS.GtFontFamily,
  borderRadius: "10px !important",
};

export const modalContentSxProps: SxProps = {
  fontFamily: GlobalConstant.GT_DESIGN_PARAMETERS.GtFontFamily,
  fontSize: "20px",
};

export const modalTitleSxProps: SxProps = {
  fontFamily: GlobalConstant.GT_DESIGN_PARAMETERS.GtFontFamily,
  fontSize: "25px",
  fontWeight: 600,
};
