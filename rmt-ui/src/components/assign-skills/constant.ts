import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const AssignELSxProps: SxProps = {
  width: "100%",
  
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  "&:hover": {
    borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};
export const AutocompleteSxProps: SxProps = {
  marginTop:"24px",
};
