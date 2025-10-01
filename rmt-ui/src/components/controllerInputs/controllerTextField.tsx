import { TextField } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";
import "./controllers.css";

const ControllerTextField = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      defaultValue={props.defaultValue}
      rules={{
        required: props.required ?? true,
        pattern: props.pattern ?? props.pattern,
      }}
      render={({ field }) => (
        <TextField
          sx={props.sx}
          className={
            props.required
              ? "input-field-group required_field"
              : "input-field-group"
          }
          label={props.label }
          {...field}
          disabled={props.disabled}
          autoComplete="off"
          error={props.error}
          multiline={props.multiline || true}
          fullWidth={props.fullWidth || true}
          onBlur={() => {
            if (props?.onBlur) {
              props?.onBlur();
            }
            field.onBlur();
          }}
          onChange={(e) => {
            field.onChange(e);
            props.onChange(e) ?? props.onChange(e);
          }}
          maxRows={props?.maxRows}
          rows={props.rows}
          helperText={props.helperText}
          inputProps={{
            maxLength: props.maxLength,
            readOnly: props.isReadOnlyModeActive,
          }}
        />
      )}
    />
  );
};
export default ControllerTextField;
