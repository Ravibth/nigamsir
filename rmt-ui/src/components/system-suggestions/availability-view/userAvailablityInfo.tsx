import Checkbox from "@mui/material/Checkbox";
import Grid from "@mui/material/Grid";
import PersonIcon from "@mui/icons-material/Person";
import Typography from "@mui/material/Typography";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import UserInfoTooltip from "./userInfoTooltip";
import ScoreBreakupTooltip from "./scoreBreakupTooltip";
import { VerticalCenterAlignSxProps } from "../../scheduler/react-time-calendar/constant";
import { IUserInfo, MatchScoreAvailabilitySxProps } from "./constants";
import { useContext, memo } from "react";
import { AllocateEmployeesContext } from "../../../contexts/allocateEmployeesContext";
import StarBorderPurple500Icon from "@mui/icons-material/StarBorderPurple500";
import { Tooltip, TooltipProps, styled, tooltipClasses } from "@mui/material";
import MarketPlaceIcon from "../../../common/images/marketplace.png";

const UserAvailablityInfo = (props: any) => {
  const allocateEmployeesContext: any = useContext(AllocateEmployeesContext);
  const isScoreVisible = props.isScoreVisible == false ? false : true;
  const isCheckBoxVisible = props.isCheckBoxVisible == false ? false : true;
  const isSameTeam = props.isSameTeam === true ? false : true;

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

  const updateUsersForAllocation = (checked: boolean, user: IUserInfo) => {
    let tempSelections = allocateEmployeesContext.selectedUsers
      ? allocateEmployeesContext.selectedUsers
      : [];
    if (checked) {
      tempSelections = [...tempSelections, user];
    } else {
      tempSelections = tempSelections.filter(
        (userItem: IUserInfo) => userItem.email !== user.email
      );
    }
    allocateEmployeesContext.setSelectedUsers(tempSelections);
    (Object.keys(props).includes("userSelectionChange") &&
      props?.userSelectionChange(checked)) ??
      props?.userSelectionChange(checked);
  };

  return (
    <Grid container className="userAvailablityInfo-custom-container">
      <Grid item xs={8} sx={VerticalCenterAlignSxProps}>
        {isCheckBoxVisible ? (
          <Checkbox
            checked={
              allocateEmployeesContext.selectedUsers?.find(
                (item: any) => item.email === props.user.email
              )
                ? true && !props.isRolledOver
                : false
            }
            onChange={(e: any) => {
              updateUsersForAllocation(e.target.checked, props.user);
            }}
            disabled={
              props.isRolledOver ||
              (isSameTeam &&
                !allocateEmployeesContext.selectedUsers?.find(
                  (item: any) => item.email === props.user.email
                ) &&
                props.isRolledOver &&
                allocateEmployeesContext.selectedUsers?.length ===
                  (props.requisitionDetails?.demands
                    ?.allResourcesHaveSameDetails
                    ? props.requisitionDetails?.demands?.pendingDemands
                    : 1)) ||
              (props.isSystemSuggestions &&
                !allocateEmployeesContext.selectedUsers?.find(
                  (item: any) => item.email === props.user.email
                ) &&
                props.isSystemSuggestions &&
                allocateEmployeesContext.selectedUsers?.length ===
                  (props.requisitionDetails?.demands
                    ?.allResourcesHaveSameDetails
                    ? props.requisitionDetails?.demands?.pendingDemands
                    : 1))
            }
          />
        ) : (
          <></>
        )}

        <PersonIcon />
        <span
          style={{
            marginLeft: "5px",
            display: "inline-block",
            position: "relative",
            cursor: "pointer",
          }}
        >
          <LightTooltip
            arrow
            className="custom-system-suggestion-tooltip"
            title={
              <UserInfoTooltip
                userInfo={props.user}
                requisitionParameters={
                  props.requisitionDetails?.requisitionParameters
                }
              />
            }
          >
            <span>{props.user.empName}</span>
          </LightTooltip>
        </span>
      </Grid>
      <Grid item xs={1} sx={VerticalCenterAlignSxProps}>
        {props.user.interested ? (
          <img
            src={MarketPlaceIcon}
            alt="upload"
            style={{
              height: "22px",
              width: "22px",
            }}
          />
        ) : (
          <></>
        )}
      </Grid>
      {isScoreVisible && props?.isSystemSuggestions ? (
        <Grid item xs={2} className="tooltip-custom-container">
          <LightTooltip
            arrow
            className="custom-system-suggestion-tooltip"
            title={
              <ScoreBreakupTooltip
                userInfo={props.user}
                requisitionParameters={
                  props.requisitionDetails?.requisitionParameters
                }
              />
            }
          >
            <Typography sx={MatchScoreAvailabilitySxProps}>
              <span>{props.user.score}%</span>
            </Typography>
          </LightTooltip>
        </Grid>
      ) : (
        <></>
      )}
      {isScoreVisible && props?.isSystemSuggestions ? (
        <Grid item xs={1} sx={VerticalCenterAlignSxProps}>
          <Typography component={"span"}>
            <KeyboardArrowRightIcon fontSize="medium" />
          </Typography>
        </Grid>
      ) : (
        <></>
      )}
    </Grid>
  );
};
export default memo(UserAvailablityInfo);
