import { SxProps } from "@mui/material";
// import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const TabLabelSXProps: SxProps = {
  borderBottom: 1, 
  borderColor: "divider", 
  width: "100%"
};

export const tabs:SxProps={
  fontWeight: "bold",
  textTransform: "none",
  fontSize: "17px",
  fontFamily: "GT Walsheim Pro",
  backgroundColor: "transparent", 
  "&.Mui-selected": {
    color: "#4f2d7f",
    backgroundColor: "transparent",
  },
}