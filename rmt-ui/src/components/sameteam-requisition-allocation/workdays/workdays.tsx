import React, { useState } from "react";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import ArrowDropUpIcon from "@mui/icons-material/ArrowDropUp";
import { Grid, TextField } from "@mui/material";

const Workday = () => {
  const [numDays, setNumDays] = useState(1);

  const increaseDays = () => {
    setNumDays(numDays + 1);
  };

  const decreaseDays = () => {
    if (numDays > 1) {
      setNumDays(numDays - 1);
    }
  };

  return (
    <div>
      <TextField
        id="outlined-number"
        label="Number"
        type="number"
        inputProps={{
          min: 0,
        }}
      />
    </div>
  );
};

export default Workday;
