import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const typographyHeading: SxProps = {
  fontWeight: "bold",
  fontSize: "22px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const typographySubtitle: SxProps = {
  fontWeight: "bold",
  fontSize: "12px",
  marginTop: "30px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
  color: "rgba(0, 0, 0, 0.5)",
};

export const label: SxProps = {
  color: "rgba(0, 0, 0, 0.6)",
  fontWeight: "bold",
  fontSize: "16px",
  marginTop: "22px",
  marginRight: "20px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const MarketfilterIconButton: SxProps = () => {
  return {
    color: GT_DESIGN_PARAMETERS.GTTealColor,
    fontSize: "14px",
    // marginTop: "15px",
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
    marginLeft: "16px !important",
  };
};
