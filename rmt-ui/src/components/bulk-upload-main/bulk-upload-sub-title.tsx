import React from "react";
import { Grid, Typography } from "@mui/material";
import { requistion_label } from "../create-requisition-main/constant";
import { getDateFormate } from "../../global/utils";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined";
import UploadFileIcon from "@mui/icons-material/UploadFile";

const BulkUploadSubTitle = (props: any) => {
  const { projectDetails, fileSelected, aggridDisplay, showBrowseComponent } =
    props;

  return (
    <div>
      {aggridDisplay && (
        <Grid container>
          <Grid item xs={3}>
            <span className="fw-600 data-container bulkUploadInfo uploadedFile">
              <UploadFileIcon className="icon-requisition-location" />
              {`${fileSelected} uploaded`}
            </span>
          </Grid>
          <Grid item xs={3}>
            <Typography component="div">
              <span className="fw-600 data-container bulkUploadInfo">
                <CalendarTodayIcon className="icon-requisition" />
                {`${requistion_label.start_date}: ${getDateFormate(
                  projectDetails?.startDate
                )}`}
              </span>
            </Typography>
          </Grid>
          <Grid item xs={3}>
            <Typography component="div">
              <span className="fw-600 data-container bulkUploadInfo">
                <CalendarTodayIcon className="icon-requisition" />
                {`${requistion_label.end_date}: ${getDateFormate(
                  projectDetails?.endDate
                )}`}
              </span>
            </Typography>
          </Grid>
          <Grid item xs={3}>
            <Typography component="div">
              <span className="fw-600 data-container bulkUploadInfo">
                <FmdGoodOutlinedIcon className="icon-requisition-location" />
                {`${requistion_label.location}: ${projectDetails?.location}`}
              </span>
            </Typography>
          </Grid>
        </Grid>
      )}
      {showBrowseComponent && (
        <Grid container>
          <Grid item xs={3} />
          <Grid item xs={3}>
            <Typography component="div">
              <span className="fw-600 data-container bulkUploadInfo">
                <CalendarTodayIcon className="icon-requisition" />
                {`${requistion_label.start_date}: ${getDateFormate(
                  projectDetails?.startDate
                )}`}
              </span>
            </Typography>
          </Grid>
          <Grid item xs={3}>
            <Typography component="div">
              <span className="fw-600 data-container bulkUploadInfo">
                <CalendarTodayIcon className="icon-requisition" />
                {`${requistion_label.end_date}: ${getDateFormate(
                  projectDetails?.endDate
                )}`}
              </span>
            </Typography>
          </Grid>
          <Grid item xs={3} />
        </Grid>
      )}
    </div>
  );
};

export default BulkUploadSubTitle;
