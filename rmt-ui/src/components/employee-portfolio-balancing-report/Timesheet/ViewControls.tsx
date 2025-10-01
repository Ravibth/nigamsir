import { Box, Grid } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import CurrentDateComp from "../../../common/currentdate/currentdate";
import DaysGrpBtn from "./days-groups-portfolio";
import ActionButton from "../../actionButton/actionButton";
import { Control } from "react-hook-form"; // 👈 import Control type
import { useEffect, useState } from "react";
import { CALENDAR_VIEW } from "../../../global/constant";
import { endOfQuarter, startOfQuarter } from "date-fns";
import { getQuarterEndMonth } from "../util/util";

type Props = {
  control: Control<any>;
  handleCurrentDateClick: () => void;
  handlePreviousClick: () => void;
  handleNextClick: () => void;
  handleSchedulerView?: (viewType: string) => void;
  startDate?: Date,
  endDate?: Date,
  selectedDate?: Date,
  currentView?: string,
  isLeftArrowDisabled?: boolean,
  isRightArrowDisabled?: boolean
};

export const ViewControls = ({
  control,  
  startDate,
  endDate,
  selectedDate,
  handleCurrentDateClick,
  handlePreviousClick,
  handleNextClick,
  handleSchedulerView,
  currentView,
  isLeftArrowDisabled,
  isRightArrowDisabled
}: Props) => {
  

  return (
    <>
      <Grid item xs />
      <Grid item xs="auto">
        <Box display="flex">
          <CurrentDateComp
            currentDateHandleClick={handleCurrentDateClick}
            handlePreviousClick={handlePreviousClick}
            handleNextClick={handleNextClick} 
            control = {control}
            isLeftArrowDisabled ={isLeftArrowDisabled}
            isRightArrowDisabled={isRightArrowDisabled}
            label="First Month"
          />
          <DaysGrpBtn handleSchedulerViewChange={handleSchedulerView} />
        </Box>
      </Grid>
    </>
  );
};
