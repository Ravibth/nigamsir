import { Autocomplete, TextField } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";
import "./controllers.css";

const ControllerAutoCompleteTextFieldWithGetOptionsLabel = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{
        required: props.required === false ? props.required : true,
        validate: !props.disabled && props.validate,
      }}
      render={({ field }) => {
        return (
          <Autocomplete
            value={field.value}
            options={props.options}
            freeSolo={props.freeSolo}
            disabled={props.disabled}
            readOnly={props.isReadOnlyModeActive}
            sx={props.sx}
            filterSelectedOptions={
              props.filterSelectedOptions ?? props.filterSelectedOptions
            }
            isOptionEqualToValue={(option, value) => {
              if (
                Object.keys(props).includes("isOptionEqualToValue") &&
                typeof props.isOptionEqualToValue === "function"
              ) {
                return props.isOptionEqualToValue(option, value);
              } else {
                return option === value;
              }
            }}
            getOptionDisabled={props?.getOptionDisabled}
            getOptionLabel={(option) =>
              Object.keys(props).includes("getOptionLabel") &&
              props?.getOptionLabel(option)
                ? props.getOptionLabel(option)
                : option
            }
            // className={"input-field-group" + props.className}
            className={
              props.required
                ? "input-field-group required_field"
                : "input-field-group"
            }
            groupBy={props.groupBy ?? props.groupBy}
            renderInput={(params) => (
              <div style={props.helperTextStyle}>
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
              </div>
            )}
            onChange={(_, data) => {
              field.onChange(data);
              if (Object.keys(props).find((key) => key === "onChange")) {
                props.onChange(data);
              }
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
export default ControllerAutoCompleteTextFieldWithGetOptionsLabel;
