import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../global/constant";

export const calendarButtonSxProps: SxProps = {
  borderRadius: "40px",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
export const calendarIconSxProps: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
export const UpdateDetailsSxProps: SxProps = {
  textTransform: "initial",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  mr: 1,
};
export const MoveToMarketPlaceSxProps: SxProps = {
  backgroundColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  ml: 1,
  "&:hover": {
    backgroundColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};
export const heading: SxProps = {
  fontSize: "40px",
  // fontFamily: "GT Walsheim Pro",
};

export const projectDetailModal: SxProps = {
  backgroundColor: "#e6e8ec",
  position: "absolute",
  top: " 50%",
  left: "50%",
  maxWidth: "90%",
  width: "auto",
  maxHeight: "99vh",
  height: "auto",
  margin: "auto",
  transform: "translate(-50%, -50%)",
  boxShadow: "0px 2px 4px rgba(0.9, 0.9, 0.9, 0.9)",
};

export const BtnYes: SxProps = {
  textTransform: "initial",
  marginLeft: "10px",
  color: "white",
  fontSize: "17px",
  backgroundColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const BtnNo: SxProps = {
  textTransform: "initial",
  fontSize: "17px",
  //  marginLeft: "250px",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  border: "1px solid #4f2d7f",
};

export const MoveMarketPlaceHeader: SxProps = {
  paddingBottom: "30px",
  fontSize: "20px",
  fontStyle: "bold",
  // fontFamily: "GT Walsheim Pro !important",
};
