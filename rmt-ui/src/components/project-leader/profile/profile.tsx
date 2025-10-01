import React from "react";
import { Avatar, Box, CssBaseline, Stack, Typography } from "@mui/material";
import { IProfileProps } from "./IProfileProps";
import "./style.css";
const LeaderProfile = (props: IProfileProps) => {
  const { label: name, index } = props;
  return (
    <div className="profileName-aligment">
      <Box>
        <Stack direction={"row"} spacing={1}>
          {name && (
            <CssBaseline>
              {/* <Avatar sx={{ width: 20, height: 20 }} alt={name} src="/static/images/avatar/1.jpg" /> */}
              <Box className="profileName" sx={{ whiteSpace: "nowrap" }}>
                <Typography className="name">
                  {" "}
                  {index ? "," : ""} {name}
                </Typography>
              </Box>
            </CssBaseline>
          )}
          {!name && <CssBaseline>{/* <Box>-</Box> */}</CssBaseline>}
        </Stack>
      </Box>
    </div>
  );
};

export default LeaderProfile;
