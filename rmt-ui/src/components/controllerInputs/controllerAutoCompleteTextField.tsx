import { Autocomplete, TextField } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";
import "./controllers.css";

const ControllerAutoCompleteTextField = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{ required: props.required === false ? props.required : true }}
      render={({ field }) => {
        return (
          <Autocomplete
            value={field.value}
            options={props.options}
            freeSolo={props.freeSolo}
            disabled={props.disabled}
            readOnly={props.isReadOnlyModeActive}
            sx={props.sx}
            // className={"input-field-group" + props.className}
            className={
              props.required
                ? "input-field-group required_field"
                : "input-field-group"
            }
            renderInput={(params) => (
              <TextField
                // sx={props.sx}
                onBlur={field.onBlur}
                label={props.label}
                className={
                  props.required
                    ? "input-field-group required_field"
                    : "input-field-group"
                }
                {...params}
                error={props.error}
                helperText={props.helperText}
              />
            )}
            onChange={(_, data) => {
              field.onChange(data);
              if (Object.keys(props).find((key) => key === "onChange")) {
                props.onChange(data);
              }
              // ?? props.onChange(data);
              // return data;
            }}
            onKeyDown={(e) => {
              if (Object.keys(props).find((key) => key === "onKeyDown")) {
                props?.onKeyDown(e);
              }
              return e;
            }}
          />
        );
      }}
    />
  );
};
export default ControllerAutoCompleteTextField;
