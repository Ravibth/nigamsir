import { Button, SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";

export interface IActionButtonProps {
  label: string;
  onClick: (e: any) => void;
  startIcon?: any;
  className?: string;
}

const NavigateActionButton = (props: IActionButtonProps) => {
  const ButtonSxProps: SxProps = {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    width: "5%",
    // fontFamily: "GT Walsheim Pro, Medium",
    // border: `2px solid ${GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple}`,
  };
  return (
    <Button
      className={props.className}
      sx={ButtonSxProps}
      onClick={props.onClick}
      startIcon={
        <ArrowBackIosNewIcon
          className="backarrow"
          sx={{
            color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
          }}
        />
      }
    >
      {props.label ? props.label : "Back"}
    </Button>
  );
};

export default NavigateActionButton;
