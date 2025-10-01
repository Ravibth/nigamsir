import * as React from "react";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import { Autocomplete, TextField, Typography } from "@mui/material";
import { Controller, useController, useForm } from "react-hook-form";
import CalendarNextPrevious from "../calendar-next-previous/calendar-next-prev";
import DaysGrpBtn from "../daysgroup/days-groups";
import CurrentDateComp from "../../common/currentdate/currentdate";
import ViewSlider from "../view-slider-bar/viewslider";
import Groupbtnsliderlayout from "../groupbtn-slider-layout/groupbtn-slider-layout";
import { addMonths, subMonths, format } from "date-fns";
import * as constant from "../../global/constant";
import GroupbtnCalendarlayout from "../groupbtn-slider-layout/groupbtn-calendar-layout";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { useContext } from "react";

const FunctionBarCalendar = (props: any) => {
  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const getCalendarCurrentView = (event: any) => {
    //console.log(event);
    props.getCalendarCurrentView(event);
  };
  const currentDateHandleClick = (date: any) => {
    // console.log("currentDateHandleClick", date);
    props.selectDateHandler(date);
    props.handleSchedulerView(constant.CALENDAR_VIEW_TYPE.TimeLineToday);
  };
  const handlePreviousClick = () => {
    // console.log(props);
    props.handlePreviousClick();
  };
  const handleNextClick = () => {
    props.handleNextClick();
  };
  const onChangeHandler = () => {
    // console.log(getValues("pipelineCode"));
    props.setSelectedPipelineCodes(getValues("pipelineCode"));
  };
  const { control, getValues } = useForm();
  return (
    <Box>
      <Grid container justifyContent="space-between">
        <Grid item>
          <CurrentDateComp
            currentDateHandleClick={currentDateHandleClick}
            handlePreviousClick={handlePreviousClick}
            handleNextClick={handleNextClick}
          />
        </Grid>
        <Grid item>
          {isEmployee && (
            <Controller
              control={control}
              name="pipelineCode"
              render={({ field }) => {
                return (
                  <Autocomplete
                    sx={{ width: "250px" }}
                    multiple
                    id="tags-outlined"
                    // options={["PC101", "PC102", "PC103"]}
                    options={props.filterOptions}
                    // defaultValue={}
                    filterSelectedOptions
                    renderInput={(params) => (
                      <TextField {...params} label="Select Pipeline Code" />
                    )}
                    onChange={(_, data) => {
                      field.onChange(data);
                      onChangeHandler();
                    }}
                  />
                );
              }}
            />
          )}
          {/* <GroupbtnCalendarlayout handleSchedulerViewChange={getCalendarCurrentView} /> */}
        </Grid>
      </Grid>
    </Box>
  );
};

export default FunctionBarCalendar;
