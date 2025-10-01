import { Box, Button, ButtonGroup } from "@mui/material";
import React, { useState } from "react";
import * as constant from "../util/constant";
import * as GC from "../../../global/constant";

const DaysGrpBtn = (props: any) => {
  const [selectedView, setSelectedView] = useState(GC.CALENDAR_VIEW.Month);
  const [selectedButton, setSelectedButton] = useState(GC.CALENDAR_VIEW.Month);
  const { handleSchedulerViewChange } = props;
  const handleDayView = () => {
    setSelectedView(GC.CALENDAR_VIEW.Day);
    setSelectedButton(GC.CALENDAR_VIEW.Day);
    handleSchedulerViewChange(GC.CALENDAR_VIEW_TYPE.TimeLineDay);
  };
  const handleMonthView = () => {    
    setSelectedView(GC.CALENDAR_VIEW.Month);
    setSelectedButton(GC.CALENDAR_VIEW.Month);
    handleSchedulerViewChange(GC.CALENDAR_VIEW_TYPE.TimeLineMonth);
  };
  const handleQuaterView = () => {
    setSelectedView(GC.CALENDAR_VIEW.Quater);
    setSelectedButton(GC.CALENDAR_VIEW.Quater);
    handleSchedulerViewChange(GC.CALENDAR_VIEW_TYPE.TimeLineQuater);
  };

  return (
    <>
      <Box mt={2} mb={2} ml={2} mr={2}>
        <ButtonGroup
          className="rmt-calenderview-buttongrp"
          variant="outlined"
          aria-label="outlined button group"
        >
          <Button
            className="rmt-calenderview-button"
            onClick={handleMonthView}
            sx={constant.GetSxPropsForButtonPortfolioBalancing(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Month
            )}
          >
            Months
          </Button>
          <Button
            className="rmt-calenderview-button"
            onClick={handleQuaterView}
            sx={constant.GetSxPropsForButtonPortfolioBalancing(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Quater
            )}
          >
            Quarter
          </Button>
        </ButtonGroup>
      </Box>
    </>
  );
};

export default DaysGrpBtn;
