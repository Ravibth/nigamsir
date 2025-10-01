import React from "react";
import { Stack, Typography } from "@mui/material";
import * as constant from "./constant";
import StarIcon from "@mui/icons-material/Star";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import LocalMallIcon from "@mui/icons-material/LocalMall";
import LocationOnOutlinedIcon from "@mui/icons-material/LocationOnOutlined";

const Marketsubtitle = (props: any) => {
  return (
    <div>
      <Stack direction="row" spacing={2}>
        {props.data.map((item: any, index: any) => (
          <div key={index}>
            <div style={{ display: "flex", alignItems: "center" }}>
              {item.icon}
              <Typography sx={constant.subtitle} variant="body1">
                <span style={{ fontWeight: "bold" }}>{item?.label}</span>
                <span className="market-card-value">{item?.value}</span>
              </Typography>
            </div>
          </div>
        ))}
      </Stack>
    </div>
  );
};

export default Marketsubtitle;
