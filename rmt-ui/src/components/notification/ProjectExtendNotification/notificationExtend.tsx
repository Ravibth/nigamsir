import React from "react";
import { Button, Typography } from "@mui/material";
import { differenceInDays } from "date-fns";
import * as constant from "./constant";
import { useNavigate } from "react-router-dom";
import ActionButton from "../../actionButton/actionButton";

interface ExtensionNotificationProps {
  content: string;
  receivedAt: Date;
  isRead: boolean;
}

const ExtensionNotification: React.FC<ExtensionNotificationProps> = ({
  content,
  receivedAt,
  isRead,
}) => {
  const daysElapsed = differenceInDays(new Date(), receivedAt);
  const navigate = useNavigate();
  const navigateToAlerts = () => navigate(`/alerts`);
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
      <ActionButton
        label={"Take Action"}
        type="submit"
        disabled={false}
        onClick={(e: any) => {
          navigateToAlerts();
        }}
      />
    </>
  );
};

export default ExtensionNotification;
