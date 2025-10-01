import * as React from "react";
import Box from "@mui/material/Box";
import Slider from "@mui/material/Slider";
import * as constant from "./constant";

export default function ViewSlider() {
  return (
    <Box mt={2} mb={2} ml={2} mr={2} sx={constant.BoxrSxProps}>
      <Slider
        sx={constant.SliderSxProps}
        aria-label="Temperature"
        defaultValue={41}
        step={null}
        marks={constant.marks}
      />
    </Box>
  );
}
