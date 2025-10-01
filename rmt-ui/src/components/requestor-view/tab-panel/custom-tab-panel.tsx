import { Box, Typography } from "@mui/material";
import React from "react";
import { TabPanelProps } from "./ITabPanelProps";

const CustomTabPanel = (props: TabPanelProps) => {
  const { children, value, index, className, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index || props?.hidden}
      id={`tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box className={className}>
          <Typography component={"div"}>{children}</Typography>
        </Box>
      )}
    </div>
  );
};

export default CustomTabPanel;
