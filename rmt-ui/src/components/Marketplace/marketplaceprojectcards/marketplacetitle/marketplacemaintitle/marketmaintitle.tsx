import { Grid, Tooltip, Typography } from "@mui/material";
import React from "react";
import LikeDislikeButtons from "./likedislike/likedislike";
import Progressbar from "./progressbar/circularprogress";
import AccountTreeOutlinedIcon from "@mui/icons-material/AccountTreeOutlined";
import WorkOutlineOutlinedIcon from "@mui/icons-material/WorkOutlineOutlined";

const Marketmaintitle = (props: any) => {
  //console.log("line7", props.projectDetail);
  const JobCodePresent = props.projectDetail.jobCode;

  return (
    <div>
      <Grid container className="market-title-container">
        <Grid item xs={8}>
          <Typography className="card-title title-legend">
            {JobCodePresent ? (
              <Tooltip title={"Job"}>
                <WorkOutlineOutlinedIcon
                  sx={{ color: "green", marginRight: 1 }}
                />
              </Tooltip>
            ) : (
              <Tooltip title={"Pipeline"}>
                <AccountTreeOutlinedIcon
                  sx={{
                    color: "red",
                    marginRight: 1,
                  }}
                />
              </Tooltip>
            )}
            {props.projectDetail.jobCode &&
            props.projectDetail.jobCode.length > 1
              ? props.projectDetail.jobName
              : props.projectDetail.pipelineName}
          </Typography>
        </Grid>
        <Grid item xs={2} sx={{ paddingLeft: "100px" }}>
          {/* <Progressbar /> */}
        </Grid>
        <Grid item xs={2} container justifyContent="flex-end">
          <LikeDislikeButtons
            detailCardViewInfo={props.detailCardViewInfo}
            projectDetail={props.projectDetail}
            GetAllRequisitionByProjectCodeInfo={
              props.GetAllRequisitionByProjectCodeInfo
            }
            getProjectList={props.getProjectList}
            // fetchDetailsConfig = {props.fetchDetailsConfig}
            likeButtonUpdateCallback={props.likeButtonUpdateCallback}
          />
        </Grid>
      </Grid>
    </div>
  );
};

export default Marketmaintitle;
