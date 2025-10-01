import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const AssignELSxProps: SxProps = {
  width: "100%",
  mt: 1,
  mb: 1,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  "&:hover": {
    borderColor: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};
export const AutocompleteSxProps: SxProps = {
  mt: 1,
  mb: 1,
};

export const BoxSxProps: SxProps = {
  border: "1px solid back",
};

export const ADDITIONAL_EL_ROLE = "AdditionalEl";
