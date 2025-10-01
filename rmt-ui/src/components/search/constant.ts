import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const BoxSxProps: SxProps = {
  marginBottom: "1px",
  marginTop: "15px",
};

export const FormControlSxProps: SxProps = {
  paddingTop: "none",
};

export const BoxSecondSxProps: SxProps = {
  display: "flex",
  alignItems: "flex-end",
  paddingBottom: "none",
  borderRadius: "10px",
};

export const SearchIconSxProps: SxProps = {
  color: GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
  mr: 1,
  my: 0.5,
  fontSize: "20px",
};

export const AutocompleteSxProps: SxProps = {
  width: "100% !important",
};

export const TextfieldSxProps: SxProps = {
  borderRadius: "8px !important",
};
