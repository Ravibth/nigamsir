import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const BtnMove: SxProps = {
  textTransform: "initial",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  fontSize: "17px",
};

export const Btncancel: SxProps = {
  textTransform: "initial",
  fontSize: "17px",
  paddingLeft: "200px",
  color: "grey",
  "&:hover": {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};
export const Textbox: SxProps = {
  width: "100px",
};

export const Text: SxProps = {
  fontSize: "18px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const Title: SxProps = {
  color: "grey",
  padding: "6px 0px",
  fontSize: "18px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const Divide: SxProps = {
  backgroundColor: "grey",
  height: 1,
  margin: "16px 0",
};

export const Card: SxProps = {
  backgroundColor: "#D4D4D4",
  minWidth: "539px",
  // height: "30px",
};

export const cardContent: SxProps = {
  color: "grey",
  fontSize: "18px",
  // fontFamily: "GT Walsheim Pro, Medium !important",
};

export const AutocompleteSxProps: SxProps = {
  backgroundColor: "#F2F5FF !important",
  width: 539,
};
