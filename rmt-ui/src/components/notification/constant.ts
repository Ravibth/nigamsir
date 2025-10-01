import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";
import axios from "axios";
import { createQueryUrl } from "../../services/utils";
const baseurl = process.env.REACT_APP_PROJECT_MS;

export const Divide: SxProps = {
  backgroundColor: "grey",
  height: 1,
  margin: "16px 0",
};

export const Button: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  textTransform: "initial",
  fontSize: "17px",
  fontWeight: "20px",
  paddingLeft: "100px",
};

export const Day: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export interface IPushNotifications {
  createdBy: string;
  createdDate: Date;
  email: string;
  employee_id: string;
  id: string;
  isRead: boolean;
  message: string;
  meta: any;
  notification_id: string;
  type: string;
  link: string;
}
export const NotificationSxProps: SxProps = {
  padding: "4px",
  cursor: "pointer",
};
export const ZeroNotificationSxProps: SxProps = {
  display: "flex",
  justifyContent: "space-evenly",
  height: "3vh",
  flexDirection: "column",
  alignItems: "center",
};

export enum ENotificationsFilter {
  ALL = "All",
  UNREAD = "Unread",
}

export const getNotificationsByApiService = async (
  limit: number,
  pagination: number
): Promise<any[]> => {
  const url = createQueryUrl(
    baseurl + "Notification/GetLoggedInUserNotifications",
    {
      limit: limit,
      pagination: pagination,
    }
  );
  return (await axios.get(url)).data;
};

export const getMyNotificationsCountByApiService =
  async (): Promise<number> => {
    return (
      await axios.get(
        baseurl + "Notification/GetLoggedInUserAllNotificationsCount"
      )
    ).data;
  };

export const MarkNotificationsAsReadByApiService = async (
  id: string[]
): Promise<void> => {
  const url = createQueryUrl(baseurl + "Notification/MarkNotificationsAsRead", {
    id: id,
  });
  return await axios.get(url);
};

export const MarkAllNotificationsAsReadByApiService =
  async (): Promise<void> => {
    const url = baseurl + "Notification/MarkAllNotificationsAsRead";
    return await axios.get(url);
  };
