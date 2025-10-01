import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const UserDetailsHeaderSxProps: SxProps = {
  display: "flex",
  alignItems: "center",
};
export const DataGridSxProps: SxProps = {
  width: "100%",
  //   height: "20vh",
};
export const filterIconButtonSystemSuggestions: SxProps = {
  color: "#4f2d7f",
  fontSize: "14px",
  textTransform: "initial",
  borderRadius: "20px",
  borderColor: "#B8B8B8",
  height: "35px",
};
export const categoryCardSxProps: SxProps = {
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  fontWeight: 500,
  textTransform: "capitalize",
  fontSize: "14px !important",
};

export const categoryValueCardSxProps: SxProps = {
  color: GT_DESIGN_PARAMETERS.GTTealColor,
  fontWeight: "600 !important",
  textTransform: "capitalize",
  fontSize: "16px !important",
  display: "flex",
  justifyContent: "flex-end",
  whiteSpace: "nowrap",
  overFlow: "hidden",
  textOverflow: "ellipsis",
};

export const scoreCategoryCardSxProps: SxProps = {
  // border: "1px solid gray",
  width: "100%",
  borderRadius: "10px",
  padding: "2px",
  backgroundColor: "#f6f2fc !important",
  display: "inline-flex",
  justifyContent: "center",
  color: "#000 !important",
};
export const SortingCardsLabelSxProps: SxProps = {
  color: "#00a7b5",
  fontWeight: "bold",
  fontSize: "16px",
};

export const SortingIconsSxProps: SxProps = {
  ":hover": {
    cursor: "pointer",
  },
};
