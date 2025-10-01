import * as React from "react";
import { Controller } from "react-hook-form";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import "./controllers.css";
import moment from "moment";
import { GlobalConfigs } from "../../global/constant";
import {
  disableWeekends,
  getDateInMomentFormat,
} from "../../utils/date/dateHelper";

export default function ControllerCalendar(props: any) {
  const adapter = new AdapterDayjs();

  const minDate =
    props.minDate && (adapter.date(props.minDate) as unknown as Date);
  const maxDate =
    props.maxDate && (adapter.date(props.maxDate) as unknown as Date);
  return (
    <Controller
      name={props.name} //startDate
      control={props.control}
      rules={{
        required: props.required ?? true,
        validate: !props.disabled && props.validate,
      }}
      render={({ field }) => {
        const eDate = field.value
          ? (adapter.date(
              props.value ? props.value : field.value
            ) as unknown as Date)
          : field.value;

        const defaultDate = props.defaultValue
          ? (adapter.date(
              props.defaultValue ? props.defaultValue : field.value
            ) as unknown as Date)
          : props.defaultValue;
        return (
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DatePicker
              disabled={props.disabled}
              shouldDisableDate={(e) => {
                return moment(getDateInMomentFormat(new Date(e))).isBefore(
                  moment(getDateInMomentFormat(new Date(minDate)))
                ) ||
                  moment(getDateInMomentFormat(new Date(e))).isAfter(
                    moment(getDateInMomentFormat(new Date(maxDate)))
                  ) ||
                  props.shouldDisableWeekends
                  ? disableWeekends(new Date(e))
                  : false;
              }}
              format={GlobalConfigs.dateFormat}
              onAccept={(newValue: any) => {
                var dateOnly = new Date(newValue);
                if (props?.ignoreTimeZone) {
                  dateOnly.setHours(12, 0, 0, 0);
                }
                field.onChange(dateOnly);
                props.onChange(dateOnly);
              }}
              slotProps={{
                actionBar: {
                  actions: props.actions,
                },
                textField: {
                  label: props.label,
                  error: props.error || props.isSelectedDateInvalid == true,
                  helperText: props.helperText,
                  disabled: props.disabled,
                  onBlur: (e) => {
                    var selectedDateOnly = moment(
                      e.target.value,
                      GlobalConfigs.dateFormat
                    )?.toDate();
                    if (props?.ignoreTimeZone) {
                      selectedDateOnly?.setHours(12, 0, 0, 0);
                    }
                    var currFieldDateOnly = moment(
                      field.value,
                      GlobalConfigs.dateFormat
                    )?.toDate();
                    if (
                      selectedDateOnly?.toDateString() !==
                      currFieldDateOnly?.toDateString()
                    ) {
                      field.onChange(selectedDateOnly);
                      props.onChange(selectedDateOnly);
                    } else {
                      //Do nothing
                    }
                  },
                  autoComplete: "off",
                  style: { width: "100%" },
                  className: props.required
                    ? "input-field-group required_field"
                    : "input-field-group",
                },
              }}
              // value={props.value ? eDate : field.value}
              defaultValue={defaultDate}
              value={eDate}
              readOnly={props.readOnly}
              minDate={minDate}
              maxDate={maxDate}
            ></DatePicker>
          </LocalizationProvider>
        );
      }}
    />
  );
}
