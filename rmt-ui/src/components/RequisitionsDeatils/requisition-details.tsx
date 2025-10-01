import React, { useEffect, useState } from "react";
import {
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Typography,
  Grid,
} from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import moment from "moment";
import { getHoursLabel } from "../../global/utils";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import "./style.css";
import { GlobalConfigs } from "../../global/constant";

export default function RequisitionDetails(props: any) {
  const [expanded, setExpanded] = useState(false);
  const handleAccordionClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Accordion expanded={expanded} onChange={handleAccordionClick}>
      <div onClick={handleAccordionClick} className="abx">
        <AccordionSummary
          expandIcon={<KeyboardArrowDownIcon className="req-expand-icon" />}
          aria-controls="panel-content"
          id="panel-header"
          sx={{ backgroundColor: "#f9f5ff", color: "000" }}
        >
          {/* {!expanded && <KeyboardArrowRightIcon />}
          {expanded && <KeyboardArrowDownIcon />} */}
          <Typography variant="h6">Requisition Detail</Typography>
        </AccordionSummary>
      </div>
      <AccordionDetails>
        <Grid container spacing={2}>
          <Grid item xs={6}>
            Designation : {props.requisitionDetail?.designation}
          </Grid>
          <Grid item xs={6}>
            {`Total Effort : ${
              props.requisitionDetail?.totalHours
            } ${getHoursLabel(props.requisitionDetail?.totalHours)}`}
          </Grid>
          <Grid item xs={6}>
            Start Date :{" "}
            {moment(props.requisitionDetail.startDate).format(
              GlobalConfigs.dateFormat
            )}
          </Grid>
          <Grid item xs={6}>
            End Date :{" "}
            {moment(props.requisitionDetail?.endDate).format(
              GlobalConfigs.dateFormat
            )}
          </Grid>
        </Grid>
      </AccordionDetails>
    </Accordion>
  );
}
