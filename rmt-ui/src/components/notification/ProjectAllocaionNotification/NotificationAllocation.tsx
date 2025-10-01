import React from "react";
import { Typography } from "@mui/material";
import { differenceInDays } from "date-fns";
import * as constant from "./constant";
interface AllocationNotificationProps {
  content: string;
  isRead: boolean;
  receivedAt: Date;
}

const AllocationNotification: React.FC<AllocationNotificationProps> = ({
  content,
  isRead,
  receivedAt,
}) => {
  const daysElapsed = differenceInDays(new Date(), new Date(receivedAt));
  return (
    <>
      <Typography variant="caption" sx={constant.Day}>
        {daysElapsed === 0
          ? "Today"
          : daysElapsed === 1
          ? "1 day ago"
          : `${daysElapsed} days ago`}
      </Typography>
      <Typography variant="body2">{content}</Typography>
    </>
  );
};

export default AllocationNotification;
