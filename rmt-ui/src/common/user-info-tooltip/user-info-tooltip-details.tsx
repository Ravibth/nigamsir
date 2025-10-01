import { useEffect, useState } from "react";
import UserInfoTooltip from "../../components/system-suggestions/availability-view/userInfoTooltip";
import { Tooltip, TooltipProps, styled, tooltipClasses } from "@mui/material";

export default function CommonnUserInfoTooltip(props: any) {
  const { emailId } = props;
  const [userInfo, setUserInfo] = useState<any[]>([]);

  const LightTooltip = styled(({ className, ...props }: TooltipProps) => (
    <Tooltip
      {...props}
      arrow
      classes={{ popper: className }}
    />
  ))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
      backgroundColor: theme.palette.common.white,
      color: "rgba(0, 0, 0, 0.87)",
      boxShadow: theme.shadows[1],
      fontSize: 11,
    },
    [`& .${tooltipClasses.arrow}`]: {
      color: theme.palette.common.white,
    },
  }));

  useEffect(() => {
    if (emailId) {
    }
  }, []);

  return (
    <>
      <div>
        <span
          style={{
            marginLeft: "5px",
            display: "inline-block",
            position: "relative",
            cursor: "pointer",
          }}
        >
         
        </span>
      </div>
    </>
  );
}
