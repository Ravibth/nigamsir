import { Button, Grid, IconButton, Tooltip, Typography } from "@mui/material";
import React, { useState } from "react";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import ArrowDropUpIcon from "@mui/icons-material/ArrowDropUp";
import { ICustomPropsInterface } from "./ICustomAccordianProps";
import SaveAsIcon from "@mui/icons-material/SaveAs";
import "./style.css";
import InfoIcon from "@mui/icons-material/Info";
import Dropdown from "../../../common/images/down.png";
import Edit from "../../../common/images/edit form.png";

const CustomAccordian = (props: ICustomPropsInterface) => {
  const {
    isOpen,
    children,
    title,
    configNote,
    isEditable,
    hideEdit,
    handleAccordianCloseClick,
    handleAccordianOpenClick,
    handleCancelClick,
    handleEditClick,
    handleSaveClick,
  } = props;
  return (
    <div className="accordian-container">
      <div>
        <Grid container justifyContent={"space-between"} alignItems={"center"}>
          <Grid item>
            <div>
              <Typography
                variant="h6"
                className="accordian-title"
                component={"h6"}
              >
                {title}
                <Tooltip
                  sx={{ marginLeft: "5px" }}
                  className={"tool-requisition info-marketplace"}
                  title={configNote}
                  placement="bottom"
                >
                  <InfoIcon />
                </Tooltip>
              </Typography>
            </div>
          </Grid>
          <Grid item>
            {!props.isOpen && (
              <IconButton onClick={handleAccordianOpenClick}>
                <img src={Dropdown} alt="dropdown" />
              </IconButton>
            )}
            {isOpen && !isEditable && !hideEdit && (
              <Tooltip title={"Edit"} placement="bottom">
                <Button
                  className="edit-button"
                  onClick={handleEditClick}
                  variant="text"
                  startIcon={<img src={Edit} alt="edit" />}
                ></Button>
              </Tooltip>
            )}
            {isOpen && isEditable && !hideEdit && (
              <Button
                className="cancel-button"
                onClick={handleCancelClick}
                variant="outlined"
              >
                Cancel
              </Button>
            )}
            {isOpen && isEditable && !hideEdit && (
              <Button
                className="save-button rmt-action-button"
                type="submit"
                variant="contained"
              >
                Save
              </Button>
            )}
            {isOpen && (
              <IconButton onClick={handleAccordianCloseClick}>
                <img src={Dropdown} alt="dropdown" />
              </IconButton>
            )}
          </Grid>
        </Grid>
      </div>
      {isOpen && <>{children}</>}
    </div>
  );
};

export default CustomAccordian;
