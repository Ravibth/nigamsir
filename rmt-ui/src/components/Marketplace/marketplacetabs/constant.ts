import { makeStyles, SxProps } from "@mui/material";

export const TabLabelSXProps: SxProps = {
  borderBottom: 1,
  borderColor: "divider",
  width: "100%",
};

export const tabs: SxProps = {
  fontWeight: "bold",
  textTransform: "none",
  fontSize: "17px",
  // fontFamily: "GT Walsheim Pro",
  backgroundColor: "transparent",
  "&.Mui-selected": {
    color: "#4f2d7f",
    backgroundColor: "transparent",
  },
};

export const mainTab: SxProps = {
  height: "90vh",
  overflowX: "hidden",
  overflowY: "auto",
};

export const TooltipDetailsSxProps: SxProps = {
  padding: "10px 10px 10px 10px",
  width: "100%",
};
