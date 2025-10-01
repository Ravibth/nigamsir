import React from "react";
import { IconButton, Tooltip } from "@mui/material";
import * as constant from "./constant";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";
import { PERMISSION_TYPE } from "../../../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../../../common/module-permission/module-permission";
import {
  IsPermissionExistForProject,
  IsProjectInActiveOrClosed,
} from "../../../../global/utils";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import DeleteOutlinedIcon from "@mui/icons-material/DeleteOutlined";
import PersonSearchOutlinedIcon from "@mui/icons-material/PersonSearchOutlined";
import { GetNewDateWithNoonTimeZone } from "../../../../utils/date/dateHelper";

const RequisitionActionCell = (props: any) => {
  const {
    handleNavigationToUpdateRequisitionForm,
    handleOpen,
    setRequisitionSelected,
    projectDetails,
  } = props;
  const userContext = React.useContext(UserDetailsContext);
  return (
    <>
      {props.data ? (
        <>
          {IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Requisition,
            PERMISSION_TYPE.Read,
            userContext.role
          ) && (
            <Tooltip title="View-update requisition">
              <IconButton
                onClick={(e) => {
                  handleNavigationToUpdateRequisitionForm(e, props.data);
                }}
              >
                <RateReviewOutlinedIcon
                  fontSize="small"
                  sx={constant.UpdateIconSxProps}
                />
              </IconButton>
            </Tooltip>
          )}
          {IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Requisition,
            PERMISSION_TYPE.Create,
            userContext.role
          ) && (
            <Tooltip title="View system suggestions">
              <IconButton
                disabled={
                IsProjectInActiveOrClosed(projectDetails) ||
                !props?.data?.hasPermissionToDelete ||
                projectDetails?.isPublishedToMarketPlace === true ||
                  (props?.data?.endDate
                    ? GetNewDateWithNoonTimeZone(props?.data?.endDate) <
                      GetNewDateWithNoonTimeZone()
                    : false)
                    ? true
                    : props.data?.requisitionStatus?.toLowerCase() !== "pending"
                    ? true
                    : false
                }
                onClick={() => setRequisitionSelected(props.data)}
              >
                <PersonSearchOutlinedIcon
                  fontSize="small"
                  sx={constant.SystemSuggestionSxProps}
                />
              </IconButton>
            </Tooltip>
          )}
          {IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Requisition,
            PERMISSION_TYPE.Delete,
            userContext.role
          ) && (
            //check if project is published to market place
            <Tooltip title="Delete requisition">
              <IconButton
                disabled={
                  !props?.data?.hasPermissionToDelete ||
                  projectDetails?.isPublishedToMarketPlace === true ||
                  IsProjectInActiveOrClosed(projectDetails)
                }
                onClick={() => {
                  handleOpen(props.data?.id?.toString());
                }}
              >
                <DeleteOutlinedIcon
                  fontSize="small"
                  sx={constant.DeleteIconSxProps}
                />
              </IconButton>
            </Tooltip>
          )}
        </>
      ) : (
        <></>
      )}
    </>
  );
};

export default RequisitionActionCell;
