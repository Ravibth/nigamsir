import { Button, SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export interface IActionButtonProps {
  label: string;
  onClick: (e: any) => void;
  textTransform?: string;
}

const BackActionButton = (props: IActionButtonProps) => {
  const ButtonSxProps: SxProps = {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    backgroundColor: "white",
    border: `2px solid ${GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple}`,
    width: "100%",
    ":hover": {
      color: GlobalConstant.GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
      backgroundColor: "white",
      border: `2px solid ${GlobalConstant.GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple}`,
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
      className="rmt-back-button"
      onClick={props.onClick}
    >
      {props.label ? props.label : "Back"}
    </Button>
  );
};

export default BackActionButton;
