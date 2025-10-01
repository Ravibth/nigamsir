import { Box, Button, ButtonGroup } from "@mui/material";
import React, { useState } from "react";
import * as constant from "./constant";
import * as GC from "../../global/constant";

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
  const handleHalfYearView = () => {
    setSelectedView(GC.CALENDAR_VIEW.Half);
    setSelectedButton(GC.CALENDAR_VIEW.Half);
    handleSchedulerViewChange(GC.CALENDAR_VIEW_TYPE.TimeLineHalfYear);
  };
  const handleYearView = () => {
    setSelectedView(GC.CALENDAR_VIEW.Year);
    setSelectedButton(GC.CALENDAR_VIEW.Year);
    handleSchedulerViewChange(GC.CALENDAR_VIEW_TYPE.TimeLineYear);
  };
  return (
    <>
      <Box mt={2} mb={2} ml={2} mr={2}>
        <ButtonGroup
          className="rmt-calenderview-buttongrp"
          variant="outlined"
          aria-label="outlined button group"
        >
          {/* <Button
            className="rmt-calenderview-button"
            onClick={handleDayView}
            sx={constant.GetSxPropsForButton(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Day
            )}
          >
            Days
          </Button> */}
          <Button
            className="rmt-calenderview-button"
            onClick={handleMonthView}
            sx={constant.GetSxPropsForButton(
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
            sx={constant.GetSxPropsForButton(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Quater
            )}
          >
            Quarter
          </Button>
          <Button
            className="rmt-calenderview-button"
            onClick={handleHalfYearView}
            sx={constant.GetSxPropsForButton(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Half
            )}
          >
            Half
          </Button>
          <Button
            className="rmt-calenderview-button"
            onClick={handleYearView}
            sx={constant.GetSxPropsForButton(
              selectedView,
              selectedButton,
              GC.CALENDAR_VIEW.Year
            )}
          >
            Year
          </Button>
        </ButtonGroup>
      </Box>
    </>
  );
};

export default DaysGrpBtn;
