import { useContext } from "react";
import {
  IUserSelectedAllocationContext,
  UserSelectedAllocationContext,
} from "../context/users-selected-allocation";
import { Checkbox, Grid, IconButton, Typography } from "@mui/material";
import {
  PersonRemoveSharpIconSxProps,
  VerticalCenterAlignSxProps,
} from "./style";
import { IAllUserAllocationEntries } from "../interface";
import { Tooltip, TooltipProps, styled, tooltipClasses } from "@mui/material";
import UserInfoTooltip from "../../system-suggestions/availability-view/userInfoTooltip";
import { EAllocationType } from "../enum";
import { MatchScoreAvailabilitySxProps } from "../../system-suggestions/availability-view/constants";
import ScoreBreakupTooltip from "../../system-suggestions/availability-view/scoreBreakupTooltip";
import PersonRemoveSharpIcon from "@mui/icons-material/PersonRemoveSharp";
import {
  EReturnTypeForCheckingUserErrors,
  checkIfUserHasAnyErrors,
} from "../commonAllocationWrapper";
import MarketPlaceIcon from "../../../common/images/marketplace.png";
import { checkIfPrevAllocIsSameAsNewAllocation } from "./utils";

export const LightTooltip = styled(({ className, ...props }: TooltipProps) => (
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

export interface IUserInfoTimelineGroup {
  userAllocationEntry: IAllUserAllocationEntries;
  index: number;
}

const UserInfoTimelineGroup = (props: IUserInfoTimelineGroup) => {
  const usersSelectedAllocationContext: IUserSelectedAllocationContext =
    useContext(UserSelectedAllocationContext);

  const isUserSelected = () => {
    return usersSelectedAllocationContext.usersSelected.find(
      (item) =>
        item.email.toLowerCase().trim() ===
        props.userAllocationEntry.email.toLowerCase().trim()
    )
      ? true
      : false;
  };

  const isUserAvailable = () => {
    return usersSelectedAllocationContext.allUserSelectionEntries.find(
      (item) =>
        item.email.toLowerCase().trim() ===
        props.userAllocationEntry.email.toLowerCase().trim()
    )?.available;
  };

  const updateUsersForAllocation = (
    checked: boolean,
    user: IAllUserAllocationEntries
  ) => {
    let tempSelections = usersSelectedAllocationContext.usersSelected
      ? usersSelectedAllocationContext.usersSelected
      : [];
    if (checked) {
      const userDetails =
        usersSelectedAllocationContext.allUserSelectionEntries.find(
          (item) => item.email === user.email
        );
      if (userDetails) {
        tempSelections = [...tempSelections, userDetails];
      }
    } else {
      tempSelections = tempSelections.filter(
        (userItem: IAllUserAllocationEntries) => userItem.email !== user.email
      );
    }
    usersSelectedAllocationContext.setUsersSelected(tempSelections);
  };

  const isUserDisabledForSelection = (): boolean => {
    if (props.userAllocationEntry.isUpdateAllowed) {
      if (
        props.userAllocationEntry.type === EAllocationType.UPDATE_ALLOCATION &&
        !props.userAllocationEntry.isPreviouslyDraft
      ) {
        const entryToCheck =
          usersSelectedAllocationContext.allUserSelectionEntries.find(
            (m) => m.email === props.userAllocationEntry.email
          );
        const isDisabled = checkIfPrevAllocIsSameAsNewAllocation(
          entryToCheck,
          usersSelectedAllocationContext
        );

        if (isDisabled) {
          const isAlreadyChecked =
            usersSelectedAllocationContext.usersSelected.find(
              (item) => item.email === props.userAllocationEntry.email
            );
          if (isAlreadyChecked) {
            const filterOutThisItem =
              usersSelectedAllocationContext.usersSelected.filter(
                (item) => item.email !== props.userAllocationEntry.email
              );
            usersSelectedAllocationContext.setUsersSelected(filterOutThisItem);
          }
        }
        return isDisabled;
      }
      return false;
    }
    return true;
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={8} sx={VerticalCenterAlignSxProps}>
        <Checkbox
          checked={isUserSelected()}          
           disabled={props.userAllocationEntry.isUpdateAllowed ? false : isUserDisabledForSelection()}
          // disabled={!props.userAllocationEntry.isUpdateAllowed}
          // disabled={
          //   usersSelectedAllocationContext?.projectInfo?.isRollover ||
          //   (!isUserSelected() && !isUserAvailable())
          // }
          onChange={(e) => {
            updateUsersForAllocation(
              e.target.checked,
              props.userAllocationEntry
            );
          }}
        />
        <IconButton
          onClick={() => {
            usersSelectedAllocationContext.setRemoveUserFromTimeline(
              props.userAllocationEntry.email
            );
          }}
        >
          <Tooltip title="Remove User">
            <PersonRemoveSharpIcon
              fontSize="small"
              sx={PersonRemoveSharpIconSxProps}
            />
          </Tooltip>
        </IconButton>
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
                userInfo={props.userAllocationEntry.userInfo}
                requisitionParameters={props.userAllocationEntry.requisition}
              />
            }
          >
            <span>{props.userAllocationEntry.userInfo.empName}</span>
          </LightTooltip>
          {checkIfUserHasAnyErrors(
            usersSelectedAllocationContext,
            props.userAllocationEntry.email,
            EReturnTypeForCheckingUserErrors.TooltipJSXElement
          )}
        </span>
      </Grid>
      <Grid item xs={1} sx={VerticalCenterAlignSxProps}>
        {props.userAllocationEntry?.interested ? (
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
      <Grid item xs={2.5} className="tooltip-custom-container">
        {props.userAllocationEntry.type ===
          EAllocationType.SYSTEM_SUGGESTED_ALLOCATION && (
          <LightTooltip
            arrow
            className="custom-system-suggestion-tooltip"
            title={
              <ScoreBreakupTooltip
                userInfo={props.userAllocationEntry.userInfo}
                requisitionParameters={
                  props.userAllocationEntry?.requisition?.requisitionParameters
                }
              />
            }
          >
            <Typography sx={MatchScoreAvailabilitySxProps}>
              <span>{props.userAllocationEntry.userInfo.match_score}%</span>
            </Typography>
          </LightTooltip>
        )}
      </Grid>
      {/* {props.userAllocationEntry.type ===
        EAllocationType.SYSTEM_SUGGESTED_ALLOCATION && (
        <Grid item xs={1} sx={VerticalCenterAlignSxProps}>
          <Typography component={"span"}>
            <KeyboardArrowRightIcon fontSize="medium" />
          </Typography>
        </Grid>
      )} */}
    </Grid>
  );
};
export default UserInfoTimelineGroup;
