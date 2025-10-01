import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../global/constant";

export const ModalBoxSxProps: SxProps = {
  bgcolor: "#F8F7FA",
  width: "90%",
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  //boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.9)",
  boxShadow: '0px 5px 15px rgba(0, 0, 0, 0.9)',
  // height: '112px'
};

export const MenuIconSxProps: SxProps = {
  mr: 1,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};
