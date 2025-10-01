import React, { useState } from "react";
import { IconButton, Tooltip } from "@mui/material";
import * as constant from "./constant";
import PostAddIcon from "@mui/icons-material/PostAdd";
import PersonRemoveSharpIcon from "@mui/icons-material/PersonRemoveSharp";
import GppBadIcon from "@mui/icons-material/GppBad";
import NotInterestedIcon from "@mui/icons-material/NotInterested";
import {
  DraftStatus,
  WORKFLOW_WITHDRAW_STATUS,
} from "../../../global/constant";
import AccessControl, {
  PERMISSION_TYPE,
} from "../../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../../common/module-permission/module-permission";
import { IsPermissionExistForProject } from "../../../global/utils";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import {
  isReleaseResourceDisabled,
  isUpdateAllocationDisabled,
} from "../utils";
import ConfirmationDialog from "../../../common/confirmation-dialog/confirmation-dialog";

const AllocationActionCell = (props: any) => {
  const {
    updateAllocation,
    data,
    handleOpen,
    myPendingTasks,
    withdrawAllocation,
    terminateAllocation,
    projectDetails
  } = props;
  const userContext = React.useContext(UserDetailsContext);
  const [openModal, setOpenModal] = useState(false);

  const isEnableWithdrawIcon = (guid: any) => {
    const _mytask = myPendingTasks.filter(
      (a: any) =>
        a?.isWithdrawl &&
        a.workflow.item_id == guid &&
        WORKFLOW_WITHDRAW_STATUS.includes(a.workflow.status)
    );
    return _mytask?.length > 0;
  };
  const IsTerminationIconVisible = (guid: any) => {
    const _currentTask = myPendingTasks.filter(
      (data) =>
        data.workflow.item_id === guid &&
        data.title ===
          "Employee Allocation Task For Resource Requestor After Employee Rejection For Termination"
    );

    return _currentTask?.length > 0;
  };
  console.log(
    "THIS IS ALLOCATION DISABLED CAUSES",
    props.data,
    !props.data.hasPermisssionToUpdateAllocation,
    isUpdateAllocationDisabled(
      new Date(props.projectDetails.endDate),
      props.data.allocationStatus,
      props.data?.isUpdated
    ),
    props?.isSuspended,
    props.data.isUpdated,
    props?.isProjectInActiveOrClosed
  );
  return (
    <>
      <ConfirmationDialog
        title="Confirm!"
        content="Do you want to terminate this resource?"
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openModal}
        onConfirmationPopClose={() => {
          setOpenModal(false);
        }}
        handleYesClick={(e) => {
          terminateAllocation(e, data?.id);
          setOpenModal(false);
        }}
      ></ConfirmationDialog>

      {IsPermissionExistForProject(
        userContext.projectPermissionData?.permissions,
        MODULE_NAME_ENUM.Allocation,
        PERMISSION_TYPE.Create,
        userContext.role
      ) && (
        <Tooltip title="Update Allocation">
          <IconButton
            onClick={(e) => {
              updateAllocation(e, data.requisitionId);
            }}
            disabled={
              !props.data.hasPermisssionToUpdateAllocation ||
              isUpdateAllocationDisabled(
                new Date(props.projectDetails.endDate),
                props.data.allocationStatus,
                props.data?.isUpdated
              ) ||
              props?.isSuspended ||
              props.data.isUpdated ||
              props?.isProjectInActiveOrClosed
            }
          >
            <PostAddIcon fontSize="small" sx={constant.IconSxProps} />
          </IconButton>
        </Tooltip>
      )}
      <AccessControl
        moduleName={MODULE_NAME_ENUM.Allocation}
        type={PERMISSION_TYPE.Read}
      >
        <Tooltip title="Release Resource">
          <IconButton
            disabled={
              !props.data.hasPermissionToReleaseAllocation ||
              isReleaseResourceDisabled(
                new Date(data.endDate),
                props.data.allocationStatus,
                props.data?.isUpdated
              ) ||
              props?.isSuspended ||
              props.data?.isUpdated ||
              props?.isProjectInActiveOrClosed
            }
            onClick={() => {
              handleOpen(data?.id?.toString());
            }}
          >
            <PersonRemoveSharpIcon fontSize="small" sx={constant.IconSxProps} />
          </IconButton>
        </Tooltip>
      </AccessControl>
      {isEnableWithdrawIcon(data?.id) && (
        <Tooltip title="Withdraw Rejection">
          <IconButton
            disabled={props?.isSuspended || props?.isProjectInActiveOrClosed}
            onClick={(e) => {
              withdrawAllocation(e, data?.id);
            }}
          >
            <GppBadIcon fontSize="small" sx={constant.IconSxProps} />
          </IconButton>
        </Tooltip>
      )}
      {IsTerminationIconVisible(data?.id) && (
        <Tooltip title="Terminate Allocation">
          <IconButton
            disabled={props?.isSuspended || props?.isProjectInActiveOrClosed}
            onClick={(e) => {
              setOpenModal(true);
              // IsTerminationIconVisible();
            }}
          >
            <NotInterestedIcon fontSize="small" sx={constant.IconSxProps} />
          </IconButton>
        </Tooltip>
      )}
    </>
  );
};

export default AllocationActionCell;
