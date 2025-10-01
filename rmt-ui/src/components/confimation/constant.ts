import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const BtnYes: SxProps = {
  textTransform: "initial",
  marginLeft: "1px",
  color:"white",
  fontSize: "17px",
  backgroundColor:GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const BtnNo: SxProps = {
  textTransform: "initial",
  fontSize: "17px",
  marginLeft: "190px",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  border:"1px solid #4f2d7f"
};