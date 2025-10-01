import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../global/constant";

export const title: SxProps = {
  // fontFamily: "GT Walsheim Pro",
  // color: "var(--gt-darker-black, #000)",
  color: "#000",
  fontSize: "12px",
  fontStyle: "normal",
  fontWeight: "500",
  opacity: 0.65,
};
export enum viewItemTitle {
  BUDGET = "Budget",
  START_DATE = "Start Date",
  END_DATE = "End Date",
}

export const data: SxProps = {
  // fontFamily: "GT Walsheim Pro",
  color: "#4F2D7F",
  fontSize: "16px",
  fontStyle: "normal",
  fontWeight: "600",
  letterSpacing: "0.8px",
  backgroundColor: "#F2F5FF !important",
};
