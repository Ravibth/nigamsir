/* eslint-disable react-hooks/exhaustive-deps */
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import React, { createContext, useContext, useEffect, useState } from "react";
import { getToken } from "../auth/authService";
import { IUserDetailsContext, UserDetailsContext } from "./userDetailsContext";

export interface INotificationContext {
  notificationSocketConnection: HubConnection | null;
  isConnected: boolean;
}

export enum NotificationSocketMethods {
  GetLoggedInUserNotifications = "GetLoggedInUserNotifications",
  GetLoggedInUserAllNotifications = "GetLoggedInUserAllNotifications",
  FoundNewNotification = "FoundNewNotification",
  JoinMasterGroup = "JoinMasterGroup",
  LeaveMasterGroup = "LeaveMasterGroup",
  MarkNotificationsAsRead = "MarkNotificationsAsRead",
  NotificationCount = "NotificationCount",
  GetLoggedInUserAllNotificationsCount = "GetLoggedInUserAllNotificationsCount",
}

const initialState: INotificationContext = {
  notificationSocketConnection: null,
  isConnected: false,
};
export const NotificationContext =
  createContext<INotificationContext>(initialState);

export const NotificationContextState: React.FC<{
  children: React.ReactNode;
}> = ({ children }) => {
  const [notificationSocketConnection, setNotificationSocketConnection] =
    useState(initialState.notificationSocketConnection);
  const [isConnected, setIsConnected] = useState(initialState.isConnected);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);

  //refresh token
  const refreshTokenSocket = async () => {
    const token = await getToken();
    if (token) {
      const socketConnection = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_BASEAPIURL + "hub", {
          //TODO check the transport type
          transport: HttpTransportType.WebSockets,
          accessTokenFactory: async () => {
            return token;
          },
          headers: { Authorization: token },
        })
        .configureLogging(LogLevel.Warning)
        .withAutomaticReconnect()
        .build();
      //todo changed due to docker issue
      //commented as discussed with aayush
      await socketConnection
        ?.start()
        .then(() => {
          setTimeout(() => {
            socketConnection.send(
              NotificationSocketMethods.JoinMasterGroup,
              userDetailsContext.username
            );
          }, 10);
          onConnect();
        })
        .catch((error) => {
          onDisconnect();
        });
      setNotificationSocketConnection(socketConnection);
    }
  };

  const stopTheSocketConnection = async () => {
    Object.keys(NotificationSocketMethods).forEach((item) => {
      notificationSocketConnection?.off(item);
    });
    await notificationSocketConnection?.stop();
  };

  const restartSocketConnection = async () => {
    await stopTheSocketConnection();
    onDisconnect();
    refreshTokenSocket();
  };
  const onConnect = () => {
    setIsConnected(true);
  };

  const onDisconnect = () => {
    setIsConnected(false);
  };

  useEffect(() => {
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsBySocket =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "false";
    if (runNotifications && runNotificationsBySocket) {
      if (userDetailsContext.username) {
        restartSocketConnection();
        setInterval(() => {
          restartSocketConnection();
        }, 200000);
      }
    }
    return () => {
      const runNotifications =
        process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
      const runNotificationsBySocket =
        process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" ===
        "false";
      if (runNotifications && runNotificationsBySocket) {
        stopTheSocketConnection();
      }
    };
  }, [userDetailsContext.username]);

  return (
    <NotificationContext.Provider
      value={{ notificationSocketConnection, isConnected }}
    >
      {children}
    </NotificationContext.Provider>
  );
};
