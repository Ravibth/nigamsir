import { AppBar, Box, Button, ButtonGroup, Grid, IconButton, Toolbar, Typography } from "@mui/material";
import React, { useState } from "react";
import ArrowLeftIcon from "@mui/icons-material/ArrowLeft";
import ArrowRightIcon from "@mui/icons-material/ArrowRight";
import { addMonths, subMonths, format } from "date-fns";

const CalendarNextPrevious = (props: any) => {
  const [currentMonth, setCurrentMonth] = useState(new Date());

  const handleNextMonth = () => {
    setCurrentMonth(addMonths(currentMonth, 1));
  };

  const handlePrevMonth = () => {
    setCurrentMonth(subMonths(currentMonth, 1));
  };
  return (
    <Box sx={{ marginBottom: "3px" }}>
      <Grid container justifyContent="space-between">
        <Grid item sx={{ marginBottom: "6px" }}>
          <Box mt={2} mb={2} ml={2} mr={2}>
            <IconButton onClick={handlePrevMonth}>
              {" "}
              <ArrowLeftIcon sx={{ fontSize: "38px" }} />
            </IconButton>
            <IconButton onClick={handleNextMonth}>
              {" "}
              <ArrowRightIcon sx={{ fontSize: "38px" }} />
            </IconButton>
          </Box>
        </Grid>
        <Grid item>
          <Box mt={2} mb={2} ml={2} mr={2}>
            <Typography variant="h6" sx={{ fontSize: "28px" }}>
              {format(currentMonth, "MMMM yyyy")}
            </Typography>
          </Box>
        </Grid>
      </Grid>
    </Box>
  );
};

export default CalendarNextPrevious;
