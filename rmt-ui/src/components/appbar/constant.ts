import { SxProps } from "@mui/material";

export const AppBarMainBox: SxProps = {
  flexGrow: 1,
  // height: "5vh",
  position: "sticky",
};
export const AppbarSxProps: SxProps = {
  background: "#4f2d7f",
  // height: "60px",
};

export const BoxOneSxProps: SxProps = {
  flexGrow: 1,
  display: { xs: "flex", md: "none" },
};

export const DividerSxProps: SxProps = {
  backgroundColor: "white",
  marginRight: "1rem",
  marginLeft: "1rem",
  // height: "45px",
  marginTop: "7px",
  marginBotton: "4px",
};

export const APPBAR_CONFIG = {
  Top: "top",
  Right: "right",
  Error: "error",
  TextAlign: "textAlign",
};
const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;

export const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
  getContentAnchorEl: null,
  anchorOrigin: {
    vertical: "bottom",
    horizontal: "center",
  },
  transformOrigin: {
    vertical: "top",
    horizontal: "center",
  },
  variant: "menu",
};
