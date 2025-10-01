import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { Grid, Paper, TextField } from "@mui/material";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { useState } from "react";
import * as constant from "./constant";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import ControllerTextField from "../../controllerInputs/controllerTextField";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";

const Allocationdate = (props: any) => {
  return (
    <div>
      <Grid container spacing={2} sx={{ padding: "17px" }}>
        <Grid item xs={4}>
          <Paper>
            {" "}
            <ControllerCalendar
              name="startDate"
              control={props.control}
              defaultValue={""}
              required={true}
              label={"Start Date"}
              error={props.errors.startDate}
              onChange={(e: any) => {}}
            />
          </Paper>
        </Grid>
        <Grid item xs={4}>
          <Paper>
            {" "}
            <ControllerCalendar
              name="endDate"
              control={props.control}
              defaultValue={""}
              required={true}
              label={"End Date"}
              error={props.errors.endDate}
              onChange={(e: any) => {}}
            />
          </Paper>
        </Grid>
        <Grid item xs={4}>
          <Paper>
            {" "}
            <ControllerNumberTextField
                  name="effortsPerDay"
                 
                  control={props.control}
                  defaultValue={""}
                  required={true}
                  label={"Efforts(hr/day)"}
                  error={props.errors.effortsPerDay}
                  onChange={(e: any) => {}}
                  // validate={() => {
                  //   return getValues("effortsPerDay") > 0 && getValues("effortsPerDay") < 9 ? true : false;
                  // }}
                />
            {/* <TextField id="outlined-basic" variant="outlined" size="small" /> */}
          </Paper>
        </Grid>
      </Grid>
    </div>
  );
};

export default Allocationdate;
