import { Slider } from "@mui/material";
import React from "react";
import { Controller } from "react-hook-form";

const ControllerSlider = (props: any) => {
  return (
    <Controller
      name={props.name}
      control={props.control}
      render={({ field }) => {
        return (
          <Slider
            value={
              field.value ||
              (props?.defaultMinValue === 0 ? props.defaultMinValue : 1)
            }
            track={props.track ?? "inverted"}
            valueLabelDisplay="auto"
            step={props.step}
            sx={props.sx}
            disabled={props.disabled ?? false}
            size={props.size}
            // marks
            min={props.min}
            max={props.max}
            onChange={(_, newValue) => {
              field.onChange(newValue);
              props.onChange(newValue);
            }}
          />
        );
      }}
    />
  );
};

export default ControllerSlider;
