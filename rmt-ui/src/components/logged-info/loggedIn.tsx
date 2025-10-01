import * as React from "react";
import Avatar from "@mui/material/Avatar";
import LogoutIcon from "@mui/icons-material/Logout";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import Stack from "@mui/material/Stack";
import * as constant from "./constant";
import { useContext } from "react";
import {
  UserDetailsContext,
  IUserDetailsContext,
} from "../../contexts/userDetailsContext";
import { ListItemIcon, Menu, MenuItem, IconButton } from "@mui/material";
import { SnackbarContext } from "../../contexts/snackbarContext";
import { signout } from "../../auth/authService";
import { useMsal } from "@azure/msal-react";

function stringAvatar(name: string) {
  return {
    sx: {
      fontSize: "14px",
      height: "30px",
      width: "30px",
      bgcolor: "#9581b2",
      color: "#fff",
      // fontFamily: "GT Walsheim Pro, Medium",
    },
    children: name
      ? name
          .split(" ")
          .filter((item, index) => index < 2)
          .map((item) => item.slice(0, 1))
          .join("")
          .toUpperCase()
      : "",
  };
}

export default function BackgroundLetterAvatars() {
  const { instance } = useMsal();
  const currentAcc = instance.getActiveAccount();
  const snackbarContext: any = useContext(SnackbarContext);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const logout = async () => {
    snackbarContext.displaySnackbar(
      "You are being logged out of OptiWise..",
      "success",
      6000
    );
    setTimeout(() => {
      signout();
    }, 5000);
  };
  const menuItems = [
    // {
    //   label: "Profile",
    //   icon: <AccountCircleIcon fontSize="small" />,
    //   onClick: handleClose,
    // },
    {
      label: "Logout",
      icon: <LogoutIcon fontSize="small" />,
      onClick: logout,
    },
  ];

  return (
    <Stack
      className="user-container"
      direction="row"
      spacing={2}
      sx={constant.StackSxProps}
    >
      <Avatar
        {...stringAvatar(
          userDetailsContext?.name ? userDetailsContext?.name : ""
        )}
      />
      {/* <Stack
        sx={constant.StackSecondSxProps}
        onClick={handleClick}
        direction="row"
        spacing={2}
      > */}
      <IconButton
        sx={constant.StackSecondSxProps}
        onClick={handleClick}
      >
        {userDetailsContext?.name ? userDetailsContext?.name : ""}
      </IconButton>

      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={anchorEl ? true : false}
        onClose={handleClose}
        onClick={handleClose}
      >
        {menuItems.map((item, index) => (
          <MenuItem
            key={index}
            onClick={item.onClick}
          >
            <ListItemIcon>{item.icon}</ListItemIcon>
            {item?.label}
          </MenuItem>
        ))}
      </Menu>
      {/* </Stack> */}
    </Stack>
  );
}
