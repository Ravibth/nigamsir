import { Checkbox, FormControlLabel, Grid } from "@mui/material";
import * as constant from "./constant";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import DeleteRoundedIcon from "@mui/icons-material/DeleteRounded";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import * as GC from "../../../global/constant";
import React from "react";

const Allocationdate = (props: any) => {
  const {
    entry,
    index,
    isPerDayHourAllocation,
    requisitions,
    isDeleteBtnEnabale,
    isViewMode,
  } = props;
  const {
    control,
    formState: { errors },
    setValue,
  } = useForm({ mode: "onTouched" });

  useEffect(() => {
    setValue("confirmedPerDayHours", entry.confirmedPerDayHours);
    setValue(
      "confirmedAllocationStartDate",
      entry.confirmedAllocationStartDate
    );
    setValue("confirmedAllocationEndDate", entry.confirmedAllocationEndDate);
  }, [entry, entry.confirmedPerDayHours]);
  const effortValidate = () => {
    if (entry.isPerDayHourAllocation) {
      return !(
        entry.confirmedPerDayHours < GC.DEFAULT_ALLOCATION_HOUR.min ||
        entry.confirmedPerDayHours > GC.DEFAULT_ALLOCATION_HOUR.max
      );
    }
    return true;
  };
  return (
    <Grid container spacing={2}>
      <Grid item xs={3}>
        <ControllerCalendar
          name={`confirmedAllocationStartDate`}
          control={control}
          required={true}
          label={"Start Date"}
          error={errors.confirmedAllocationStartDate}
          minDate={new Date(requisitions?.startDate)}
          onChange={(date: any) =>
            props.handleStartDateChange(entry.index, date)
          }
          defaultValue={entry?.confirmedAllocationStartDate}
          maxDate={entry?.confirmedAllocationEndDate}
          disabled={isViewMode}
        />
      </Grid>
      <Grid item xs={3}>
        <ControllerCalendar
          name={`confirmedAllocationEndDate`}
          control={control}
          required={true}
          label={"End Date"}
          error={errors.confirmedAllocationEndDate}
          onChange={(date: any) => props.handleEndDateChange(entry.index, date)}
          defaultValue={entry?.confirmedAllocationEndDate}
          minDate={entry?.confirmedAllocationStartDate}
          maxDate={new Date(requisitions?.endDate)}
          disabled={isViewMode}
        />
        {/* </Paper> */}
      </Grid>
      <Grid item xs={3}>
        <ControllerNumberTextField
          name={`confirmedPerDayHours`}
          sx={constant.Textbox}
          control={control}
          required={true}
          label={"Efforts(hr)"}
          min={GC.DEFAULT_ALLOCATION_HOUR.min}
          max={isPerDayHourAllocation ?? GC.DEFAULT_ALLOCATION_HOUR.max}
          error={errors.confirmedPerDayHours ? true : false}
          validate={() => effortValidate()}
          defaultValue={entry.confirmedPerDayHours}
          onChange={(e: any) => props.handleEffortChange(index, e.target.value)}
          disabled={isViewMode}
        />
      </Grid>
      <Grid item xs={2.2}>
        <FormControlLabel
          sx={constant.PerDayHourAllocation}
          key={"Hours/Day"}
          control={
            <Checkbox
              checked={entry.isPerDayHourAllocation}
              disabled={isViewMode}
              onChange={(date: any) =>
                props.handleChangeHoursPerDay(entry.index, date)
              }
              name="gilad"
            />
          }
          label={"Hours/Day"}
        />
      </Grid>
      <Grid item xs={0.8}>
        {isDeleteBtnEnabale && !isViewMode && (
          <DeleteRoundedIcon
            sx={constant.deleteIcon}
            fontSize="small"
            onClick={() => {
              props.handleDeleteEntry(index);
            }}
          />
        )}
      </Grid>
      <Grid item xs={12}>
        <span className="error_msg">
          {entry.error_vaild_date_Msg ? entry.error_vaild_date_Msg : ""}
        </span>{" "}
        <span className="error_msg">
          {entry.error_total_hours_Msg ? entry.error_total_hours_Msg : ""}
        </span>
      </Grid>
    </Grid>
  );
};
export default React.memo(Allocationdate);
