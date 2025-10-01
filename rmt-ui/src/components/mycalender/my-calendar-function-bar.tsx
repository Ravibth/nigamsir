import { Autocomplete, Box, Grid, TextField } from "@mui/material";
import React from "react";
import CurrentDateComp from "../../common/currentdate/currentdate";
import { Controller, useForm } from "react-hook-form";
import * as constant from "../../global/constant";

const MyCalendarFunctionBar = (props: any) => {
  const { control, getValues } = useForm();
  const currentDateHandleClick = (date: any) => {
    props.selectDateHandler(date);
    props.handleSchedulerView(constant.CALENDAR_VIEW_TYPE.TimeLineToday);
  };
  const handlePreviousClick = () => {
    props.handlePreviousClick();
  };
  const handleNextClick = () => {
    props.handleNextClick();
  };
  const onChangeHandler = () => {
    props.setSelectedPipelineCodes(getValues("pipelineCode"));
  };
  return (
    <Box>
      <Grid
        container
        justifyContent="space-between"
        alignItems={"center"}
      >
        <Grid item>
          <CurrentDateComp
            currentDateHandleClick={currentDateHandleClick}
            handlePreviousClick={handlePreviousClick}
            handleNextClick={handleNextClick}
          />
        </Grid>
        <Grid
          item
          mt={2}
        >
          {/* {isEmployee && ( */}
          <Controller
            control={control}
            name="pipelineCode"
            render={({ field }) => {
              return (
                <Autocomplete
                  className="my-calendar-control"
                  sx={{ width: "350px" }}
                  multiple
                  id="tags-outlined"
                  // options={["PC101", "PC102", "PC103"]}
                  options={props.filterOptions}
                  //  getOptionLabel={(option) => option.label}
                  // defaultValue={}
                  filterSelectedOptions
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      label="Select Code"
                    />
                  )}
                  onChange={(_, data) => {
                    field.onChange(data);
                    onChangeHandler();
                  }}
                />
              );
            }}
          />
          {/* <GroupbtnCalendarlayout handleSchedulerViewChange={getCalendarCurrentView} /> */}
        </Grid>
      </Grid>
    </Box>
  );
};

export default MyCalendarFunctionBar;
