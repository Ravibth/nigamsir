import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const datepicker_style: SxProps = {
  borderRadius: "40px",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const CurrentDateButtonSxProps: SxProps = {
  borderRadius: "40px",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  "&:hover": {
    color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};

export const sxPropsForIcon: SxProps = {
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
