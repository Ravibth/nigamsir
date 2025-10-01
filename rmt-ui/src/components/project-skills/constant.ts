import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const AddSkillsSxProps: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderRadius: "40px",
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  m: 1,
  pl: 1,
  pr: 1,
};
export const ChipSxProps: SxProps = {
  mr: 1,
  p: 1,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor
};
export const CancelIconSxProps: SxProps = {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor.toString(),
    // backgroundColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor
}
export const CloseIconSxProps: SxProps ={
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor
}