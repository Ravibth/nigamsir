import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const GetSxPropsForButton = (
  projectTypeNumber: number,
  viewNumber: number
) => {
  const sxProps: SxProps = {
    backgroundColor:
      projectTypeNumber === viewNumber
        ? GT_DESIGN_PARAMETERS.GtPrimaryColorPurple
        : "",
    color:
      projectTypeNumber === viewNumber
        ? "white"
        : GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    // fontFamily: "GT Walsheim Pro",
    fontWeight: projectTypeNumber === viewNumber ? "550" : "500",
    fontSize: "12px",
    textTransform: "initial",

    borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
    borderRadius: "30px",
    "&:hover": {
      color: "#000",
      borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
      backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor,
    },
  };
  return sxProps;
};

// export const GetSxPropsFo
export const PROJECT_CHARGE_TYPE = {
  CHARGABLE: "CHARGEABLE",
  NON_CHARGABLE: "NONCHARGEABLE",
  ALL: "ALL",
};

export const PROJECT_TYPE = {
  OPEN: "Open",
  CLOSE: "Closed",
  ALL: "ALL",
};

//check for these duplicate values with correct and wrong spelling both
export enum ProjectChargeableType {
  Chargable = "Chargeable",
  NonChargable = "NonChargeable",
  All = "All",
}
