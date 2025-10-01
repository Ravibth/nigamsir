import React from "react";
import {
  Box,
  LinearProgress,
  linearProgressClasses,
  styled,
  Typography,
} from "@mui/material";
import "./progress-bar-display.css";

const BorderLinearProgress = styled(LinearProgress)(({ theme }) => ({
  height: 15,
  borderRadius: 7.5,
  [`&.${linearProgressClasses.colorPrimary}`]: {
    backgroundColor:
      theme.palette.grey[theme.palette.mode === "light" ? 200 : 800],
  },
  [`& .${linearProgressClasses.bar}`]: {
    borderRadius: 5,
    backgroundColor: theme.palette.mode === "light" ? "#1a90ff" : "#308fe8",
  },
}));

const ProgressBarDisplay = (props: any) => {
  const { value } = props;

  return (
    <Box sx={{ flexGrow: 1, position: "relative" }}>
      <BorderLinearProgress variant="determinate" value={value} />
      <Typography variant="body2" component="div" className="progress-bar">
        {`${value}%`}
      </Typography>
    </Box>
  );
};

export default ProgressBarDisplay;
