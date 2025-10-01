import { Box, Typography, Tooltip } from "@mui/material";
import React from "react";
import { ISectionHeaderProps } from "./interface";
import "./style.css";
import InfoIcon from "@mui/icons-material/Info";

const SectionHeader = (props: ISectionHeaderProps) => {
  return (
    <Box sx={{ mb: 4 }}>
      <Typography variant="h5" component="h2">
        <span className="title-container">{props.title}</span>
        <span>
          {props.tooltip && (
            <Tooltip title={props.tooltip} placement="right">
              <InfoIcon className="preference-info-icon" />
            </Tooltip>
          )}
        </span>
      </Typography>
    </Box>
  );
};

export default SectionHeader;
