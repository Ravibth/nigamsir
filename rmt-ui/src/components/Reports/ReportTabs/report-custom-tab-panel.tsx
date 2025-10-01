import { Box, Typography } from "@mui/material";
import React from "react";

// const ReportCustomTabPanel = () => {
//   return;
// };
interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
  hidden?: boolean;
}
const ReportCustomTabPanel = (props: TabPanelProps) => {
  const { children, value, index, hidden, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index || hidden}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
};
export default ReportCustomTabPanel;
