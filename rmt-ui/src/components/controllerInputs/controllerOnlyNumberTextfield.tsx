import { TextField } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";
import "./controllers.css";
import "./controllerNumberTextfieldStyle.css";

const ControllerOnlyNumberTextField = (props: any) => {
  let classes = props.required
    ? "input-field-group required_field "
    : "input-field-group ";
  classes += props.isUpDownNeeded ? "" : "up-down-remove";

  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{
        required: props.required ? props.required : false,
        validate: props.validate,
      }}
      render={({ field }) => (
        <TextField
          sx={props.sx}
          size={props.size}
          className={classes}
          label={props.label}
          {...field}
          disabled={props.disabled}
          autoComplete="off"
          error={props.error}
          value={
            field.value
            // < 10 ? "0" + field.value : field.value
          }
          onBlur={field.onBlur}
          onChange={(data) => {
            field.onChange(data);
            props.onChange(data);
            //  ?? props.onChange(data);
          }}
          style={{ width: "100%" }}
          helperText={props.helperText}
          inputProps={{
            min: props.min,
            max: props.max,
            readOnly: props.readOnly,
          }}
          onKeyDown={(e) => {
            if (
              e.key === "e" ||
              e.key === "E" ||
              e.key === "-" ||
              e.key === "+" ||
              e.key === "."
            ) {
              e.preventDefault();
            }
          }}
          type="number"
          // value={props.value ? props.value : field.value}
        />
      )}
    />
  );
};
export default ControllerOnlyNumberTextField;
