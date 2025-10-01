import {
  Box,
  Button,
  SxProps,
  TextField,
  Toolbar,
  Typography,
  IconButton,
} from "@mui/material";
import React, { useState } from "react";
import * as Constant from "./constant";
import "../currentdate/style.css";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import ArrowLeftIcon from "@mui/icons-material/ArrowLeft";
import ArrowRightIcon from "@mui/icons-material/ArrowRight";
import "./style.css";

const CurrentDateComp = (props: any) => {
  const [value, setValue] = useState(null);
  const currentDateHandleClick = () => {
    props.currentDateHandleClick();
  };

  const handleNextClick = () => {
    props.handleNextClick();
  };

  const handlePreviousClick = () => {
    props.handlePreviousClick();
  };
  return (
    <Box mt={2} id={"datepicker"}>
      <IconButton onClick={handlePreviousClick} className={props?.isLeftArrowDisabled ? "currentDateArrowHide" : ""}>
        <ArrowLeftIcon /> 
      </IconButton>
      <button className="currentdatebutton" onClick={currentDateHandleClick}>
       {props.label ? props.label : "Current Date"}
      </button>
      <IconButton onClick={handleNextClick} className={props?.isRightArrowDisabled ? "currentDateArrowHide" : ""}>
        <ArrowRightIcon />{" "}
      </IconButton>
    </Box>
  );
};

export default CurrentDateComp;
