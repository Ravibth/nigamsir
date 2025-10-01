import {
  Chip,
  Tooltip,
  TooltipProps,
  styled,
  tooltipClasses,
} from "@mui/material";
import MySkillCommentRenderer from "./commentsRenderer";
import { getEmployeeAllocationStatus } from "../../../global/workflow/workflow-utils";

const MySkillStatusCommentRenderer = (props) => {
  const LightTooltip = styled(({ className, ...props }: TooltipProps) => (
    <Tooltip {...props} arrow classes={{ popper: className }} />
  ))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
      backgroundColor: theme.palette.common.white,
      color: "rgba(0, 0, 0, 0.87)",
      boxShadow: theme.shadows[1],
      fontSize: 11,
    },
    [`& .${tooltipClasses.arrow}`]: {
      color: theme.palette.common.white,
    },
  }));
  const getChip = (params) => {
    const UIStatus = params;
    const uiBg = getEmployeeAllocationStatus(params).color;
    const fontColor = getEmployeeAllocationStatus(params).bgColor;
    return (
      <Chip
        label={UIStatus}
        style={{
          width: "100%",
          color: fontColor,
          backgroundColor: uiBg,
        }}
      />
    );
  };
  return (
    <>
      <LightTooltip
        arrow
        className="custom-system-suggestion-tooltip"
        title={
          <MySkillCommentRenderer
            nodeData={props?.data}
            gridRowsData={props?.gridRowsData}
            setGridRowsData={props?.setGridRowsData}
          />
        }
      >
        <span>{getChip(props?.value)}</span>
      </LightTooltip>
    </>
  );
};

export default MySkillStatusCommentRenderer;
