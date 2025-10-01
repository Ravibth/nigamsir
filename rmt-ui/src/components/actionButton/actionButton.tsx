import { Button, SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export interface IActionButtonProps {
  label: string;
  onClick: (e: any) => void;
  disabled: boolean;
  type: any;
  backgroundColor?: string;
  textTransform?: string;
}

const ActionButton = (props: IActionButtonProps) => {
  const ButtonSxProps: SxProps = {
    backgroundColor: props.backgroundColor
      ? props.backgroundColor
      : GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    color: "white",
    border: `2px solid ${GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple}`,
    width: "100%",
    ":hover": {
      backgroundColor:
        GlobalConstant.GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
      color: "white",
      border: `2px solid ${GlobalConstant.GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple}`,
    },
    ":disabled": {
      backgroundColor: "gray !important",
      border: "2px solid gray",
      color: "white",
    },
    // fontFamily: "GT Walsheim Pro, Medium",
    textTransform: props.textTransform ? props.textTransform : "capitalize",
    borderRadius: "10px !important",
    // fontWeight: "bold",
    fontSize: "16px !important",
  };
  return (
    <Button
      sx={ButtonSxProps}
      className={props.disabled ? "rmt-action-disabled" : "rmt-action-button"}
      onClick={props.onClick}
      disabled={props.disabled}
      type={props.type ? props.type : "button"}
    >
      {props.label ? props.label : "Continue"}
    </Button>
  );
};

export default ActionButton;
