import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const GetSxPropsForButton = (
  selectedView: string,
  selectedButton: string,
  view: string
) => {
  const sxProps: SxProps = {
    // backgroundColor: selectedView === view ? "#4f2d7f" : "",
    backgroundColor:
      selectedView === view ? GT_DESIGN_PARAMETERS.GtPrimaryColorPurple : "",
    // color: selectedButton === view ? "white" : "#4f2d7f",
    color:
      selectedButton === view
        ? "white"
        : GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    fontSize: "14px",
    // fontFamily: "GT Walsheim Pro",
    textTransform: "initial",
    borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
    borderRadius: view === "month" || view === "year" ? "30px" : "0px",
    "&:hover": {
      color: "#000",
      borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
      backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor,
    },
  };
  return sxProps;
};
