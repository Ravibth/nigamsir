import InfoIcon from "@mui/icons-material/Info";
import { SxProps } from "@mui/material";
import Tooltip from "@mui/material/Tooltip";

enum ITooltipPlacement {
  Bottom = "bottom",
  Left = "left",
  Right = "right",
  Top = "top",
  BottomEnd = "bottom-end",
  BottomStart = "bottom-start",
  LeftEnd = "left-end",
  LeftStart = "left-start",
  RightEnd = "right-end",
  RightStart = "right-start",
  TopEnd = "top-end",
  TopStart = "top-start",
}

export interface IInfoIconWithTooltipProps {
  title: string;
  placement?: ITooltipPlacement;
  sx?: SxProps;
}
const InfoIconWithTooltip = (props: IInfoIconWithTooltipProps) => {
  return (
    <Tooltip
      sx={props.sx}
      className="infoIconStyle"
      title={props.title}
      placement={props.placement ?? ITooltipPlacement.Top}
    >
      <InfoIcon />
    </Tooltip>
  );
};

export default InfoIconWithTooltip;
