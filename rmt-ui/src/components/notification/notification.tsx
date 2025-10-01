import React, { useContext, useEffect, useState } from "react";
import { Popover } from "@mui/material";
import { Badge, IconButton } from "@mui/material";
import NotificationsIcon from "@mui/icons-material/Notifications";
import {
  INotificationContext,
  NotificationContext,
  NotificationSocketMethods,
} from "../../contexts/notificationContext";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../contexts/userDetailsContext";
import NotificationsPopOver from "./notifications-popover";
import "./notifications.css";
import { getMyNotificationsCountByApiService } from "./constant";

const Notifications: React.FC = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const notificationSocket: INotificationContext =
    useContext(NotificationContext);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const [notificationsCount, setNotificationsCount] = useState<number>(0);
  const [open, setOpen] = useState<boolean>(false);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
    setOpen(true);
  };

  const handleClose = () => {
    setAnchorEl(null);
    setOpen(false);
  };

  const id = open ? "notification-popover" : undefined;

  const getMyNotificationsCount = () => {
    notificationSocket.notificationSocketConnection?.send(
      NotificationSocketMethods.GetLoggedInUserAllNotificationsCount,
      userDetailsContext.username
    );
  };

  const updateNotificationCount = (count: number) => {
    setNotificationsCount(count);
  };

  useEffect(() => {
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsBySocket =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "false";
    if (runNotifications && runNotificationsBySocket) {
      if (notificationSocket.isConnected && userDetailsContext.username) {
        getMyNotificationsCount();
        notificationSocket.notificationSocketConnection?.on(
          NotificationSocketMethods.NotificationCount,
          (resp: number) => {
            updateNotificationCount(resp);
          }
        );
        notificationSocket.notificationSocketConnection?.on(
          NotificationSocketMethods.FoundNewNotification,
          (resp) => {
            getMyNotificationsCount();
          }
        );
      }
    }
  }, [notificationSocket.isConnected, open]);

  useEffect(() => {
    return () => {
      const runNotifications =
        process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
      const runNotificationsBySocket =
        process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" ===
        "false";
      if (runNotifications && runNotificationsBySocket) {
        notificationSocket.notificationSocketConnection?.off(
          NotificationSocketMethods.GetLoggedInUserAllNotificationsCount
        );
        notificationSocket.notificationSocketConnection?.off(
          NotificationSocketMethods.NotificationCount
        );
      }
    };
  }, []);

  useEffect(() => {
    const intervalValue: number = Number(
      process.env.REACT_APP_PUSH_NOTIFICATION_TIMEINTERVAL_VALUE
    );
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsByTimeInterval =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "true";
    if (runNotifications && runNotificationsByTimeInterval) {
      getMyNotificationsCountByApi();
      setInterval(() => {
        getMyNotificationsCountByApi();
      }, intervalValue);
    }
  }, []);

  const getMyNotificationsCountByApi = () => {
    return new Promise<boolean>((resolve, reject) => {
      getMyNotificationsCountByApiService()
        .then((resp) => {
          updateNotificationCount(resp);
          resolve(true);
        })
        .catch((err) => {
          resolve(true);
        });
    });
  };

  return (
    <div>
      <IconButton
        onClick={handleClick}
        size="large"
        aria-label="Show"
        color="inherit"
      >
        <Badge
          badgeContent={notificationsCount}
          color="secondary"
          invisible={notificationsCount > 0 ? false : true}
        >
          <NotificationsIcon />
        </Badge>
      </IconButton>
      <Popover
        id={id}
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
      >
        {open ? (
          <NotificationsPopOver
            handleClose={handleClose}
            getMyNotificationsCountByApi={getMyNotificationsCountByApi}
          />
        ) : (
          <></>
        )}
      </Popover>
    </div>
  );
};

export default Notifications;
