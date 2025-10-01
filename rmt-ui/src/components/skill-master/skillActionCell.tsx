import React from "react";
import { IconButton, Menu, MenuItem, Tooltip } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import VisibilityIcon from "@mui/icons-material/Visibility";
import PostAddIcon from "@mui/icons-material/PostAdd";
import * as constant from "../../components/activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import RemoveCircleIcon from "@mui/icons-material/RemoveCircle";
import ControlPointIcon from "@mui/icons-material/ControlPoint";
import { RolesListMaster } from "../../common/enums/ERoles";

const SkillActionCell = (props: any) => {
  const userContext = React.useContext(UserDetailsContext);
  const isAdmin =
    userContext.role?.filter(
      (role) =>
        role?.toLowerCase() === RolesListMaster.Admin.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.CEOCOO.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.SystemAdmin.toLowerCase() //SystemAdmin Change
    )?.length > 0;

  return (
    <>
      {props.data ? (
        <>
          {/* <AccessControl
            moduleName={MODULE_NAME_ENUM.Requisition}
            type={PERMISSION_TYPE.Read}
          > */}
          <Tooltip title="View Skill">
            <IconButton
              //disabled={!props.data?.isEnable}
              onClick={(e) => {
                props?.handleViewClick(props.data);
              }}
            >
              <PersonSearchIcon
                fontSize="small"
                sx={constant.DeleteIconSxProps}
              />
            </IconButton>
          </Tooltip>

          <Tooltip title="Edit Skill">
            <IconButton
              onClick={(e) => {
                props?.handleEditClick(props.data);
              }}
              disabled={!props.data?.isEnable || !isAdmin}
            >
              <RateReviewOutlinedIcon
                fontSize="small"
                sx={constant.DeleteIconSxProps}
              />
            </IconButton>
          </Tooltip>
          {/* </AccessControl> */}
          {/* {props.data?.isEnable ? (
            <Tooltip title="Disable Skill">
              <IconButton
                onClick={() => {
                  props?.handleStatusClick(props.data);
                }}
                disabled={!isAdmin}
              >
                <RemoveCircleIcon
                  fontSize="small"
                  sx={constant.DeleteIconSxProps}
                />
              </IconButton>
            </Tooltip>
          ) : (
            <Tooltip title="Enable Skill">
              <IconButton
                onClick={() => {
                  props?.handleStatusClick(props.data);
                }}
                disabled={!isAdmin}
              >
                <ControlPointIcon
                  fontSize="small"
                  sx={constant.DeleteIconSxProps}
                />
              </IconButton>
            </Tooltip>
          )} */}
        </>
      ) : (
        <></>
      )}
    </>
  );
};

export default SkillActionCell;
