import React from "react";
import { IconButton, Menu, MenuItem, styled, Tooltip } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import PersonSearchIcon from "@mui/icons-material/PersonSearch";
import VisibilityIcon from "@mui/icons-material/Visibility";
import PostAddIcon from "@mui/icons-material/PostAdd";
import * as constant from "../activeRequisitionsDeatils/activerequisition/requisitiontable/constant";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import RemoveCircleIcon from "@mui/icons-material/RemoveCircle";
import ControlPointIcon from "@mui/icons-material/ControlPoint";
import Switch, { SwitchProps } from "@mui/material/Switch";
import { RolesListMaster } from "../../common/enums/ERoles";

const SkillStatusActionCell = (props: any) => {
  const IOSSwitch = styled((props: SwitchProps) => (
    <Switch
      focusVisibleClassName=".Mui-focusVisible"
      disableRipple
      {...props}
    />
  ))(({ theme }) => ({
    width: 42,
    height: 26,
    padding: 0,
    "& .MuiSwitch-switchBase": {
      padding: 0,
      margin: 2,
      transitionDuration: "300ms",
      "&.Mui-checked": {
        transform: "translateX(16px)",
        color: "#fff",
        "& + .MuiSwitch-track": {
          backgroundColor:
            theme.palette.mode === "dark" ? "#2ECA45" : "#00a7b5 ",
          opacity: 1,
          border: 0,
        },
        "&.Mui-disabled + .MuiSwitch-track": {
          opacity: 0.5,
        },
      },
      "&.Mui-focusVisible .MuiSwitch-thumb": {
        color: "#33cf4d",
        border: "6px solid #fff",
      },
      "&.Mui-disabled .MuiSwitch-thumb": {
        color:
          theme.palette.mode === "light"
            ? theme.palette.grey[100]
            : theme.palette.grey[600],
      },
      "&.Mui-disabled + .MuiSwitch-track": {
        opacity: theme.palette.mode === "light" ? 0.5 : 0.3,
      },
    },
    "& .MuiSwitch-thumb": {
      boxSizing: "border-box",
      width: 22,
      height: 22,
    },
    "& .MuiSwitch-track": {
      borderRadius: 26 / 2,
      backgroundColor: theme.palette.mode === "light" ? "#787878" : "#39393D",
      opacity: 1,
      transition: theme.transitions.create(["background-color"], {
        duration: 500,
      }),
    },
  }));

  const userContext = React.useContext(UserDetailsContext);
  const isAdmin =
    userContext.role?.filter(
      (role) =>
        role?.toLowerCase() === RolesListMaster.Admin.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.CEOCOO.toLowerCase() ||
        role?.toLowerCase() === RolesListMaster.SystemAdmin.toLowerCase()
    )?.length > 0;

  return (
    <>
      {props.data ? (
        <>
          <Tooltip
            title={props.data?.isEnable ? "Disable Skill" : "Enable Skill"}
          >
            <IOSSwitch
              disabled={!isAdmin}
              inputProps={{ "aria-label": "controlled" }}
              checked={props.data?.isEnable}
              onChange={() => {
                props?.handleStatusClick(props.data);
              }}
            ></IOSSwitch>
          </Tooltip>
        </>
      ) : (
        <></>
      )}
    </>
  );
};

export default SkillStatusActionCell;
