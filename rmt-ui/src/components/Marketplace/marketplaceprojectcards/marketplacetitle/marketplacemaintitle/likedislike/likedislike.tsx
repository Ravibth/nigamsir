import React, { useContext, useEffect, useState } from "react";
import { IconButton, Grid, Typography } from "@mui/material";
import ThumbUpAltIcon from "@mui/icons-material/ThumbUpAlt";
import ThumbUpOffAltIcon from "@mui/icons-material/ThumbUpOffAlt";
import { UpdateEmployeeProjectInterest } from "../../../../../../services/marketPlace/empProjectInterest";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../../../../../contexts/userDetailsContext";
import { createOrUpdateEmpProjectInterestScore } from "../../../../../../services/marketPlace/EmpProjectInterestScore";
import { GetAllRequisitionByProjectCodeFromMP } from "../../../../../../services/marketPlace/get-all-project-for-marketplace";
import {
  SnackbarContext,
  SnackbarSeverity,
} from "../../../../../../contexts/snackbarContext";
import { format } from "react-string-format";

function LikeButton(props: any) {
  const [isLiked, setIsLiked] = useState(false);
  const [likesCount, setLikesCount] = useState(0);
  const [allRequisition, setAllRequisition] = useState([] as any);
  const userDetailContext: IUserDetailsContext = useContext(UserDetailsContext);
  const [likeButtonDisable, setLikeButtonDisable] = useState(false);
  const snackbarContext: any = useContext(SnackbarContext);
  const [snackbarMsg, setSnackbarMsg] = useState("");
  const marketplace_interest_submitted_success =
    "Your interest in Project {0} has been submitted successfully.";

  useEffect(() => {
    if (props.projectDetail.jobCode) {
      setSnackbarMsg(
        format(
          marketplace_interest_submitted_success,
          props.projectDetail?.jobName
        )
      );
    } else {
      setSnackbarMsg(
        format(
          marketplace_interest_submitted_success,
          props.projectDetail?.pipelineName
        )
      );
    }

    setIsLiked(props.projectDetail.isInterested);
    setLikesCount(props.projectDetail.isInterested == true ? 1 : 0);
    isUserAndCreatorSame();
  }, [props.projectDetail]);

  const isUserAndCreatorSame = () => {
    if (userDetailContext.username === props.projectDetail.createdBy) {
      //Set Like button disbaled for user who has created the project
      setLikeButtonDisable(true);
    }
  };
  const handleLikeClick = async () => {
    if (isLiked) {
      setLikesCount(0);
    } else {
      setLikesCount(1);
    }

    setIsLiked((prevValue) => !prevValue);
    const update = await UpdateEmployeeProjectInterest({
      marketPlaceId: props.projectDetail.id,
      isInterested: !isLiked,
      interestDate: new Date(),
      empEmail: userDetailContext.username,
      empName: userDetailContext.name,
      isActive: true,
      createdBy: userDetailContext.username,
      modifiedBy: userDetailContext.username,
    });

    if (update.isInterested) {
      if (props.detailCardViewInfo.length == 0) {
        await GetAllRequisitionByProjectCodeInfo(update);
      } else {
        await CreateOrUpdateEmpProjectInterestScore(
          props.detailCardViewInfo,
          update
        );
      }
      snackbarContext.displaySnackbar(
        snackbarMsg,
        SnackbarSeverity.SUCCESS,
        6000
      );
    }

    props.likeButtonUpdateCallback(props.projectDetail.pipelineCode, !isLiked);
  };

  const GetAllRequisitionByProjectCodeInfo = async (update: any) => {
    if (allRequisition.length == 0) {
      const RequisitionData: any = await GetAllRequisitionByProjectCodeFromMP(
        props.projectDetail.id,
        userDetailContext.username,
        true
      );
      setAllRequisition(RequisitionData);
      CreateOrUpdateEmpProjectInterestScore(RequisitionData, update);
    } else {
      CreateOrUpdateEmpProjectInterestScore(allRequisition, update);
    }
  };

  const CreateOrUpdateEmpProjectInterestScore = async (
    requisitiondata: any,
    update: any
  ) => {
    let requistionList: any[] = [];
    let count = 0;
    for (let i = 0; i < requisitiondata.length; i++) {
      let obj = {} as any;
      obj.empProjectInterestId = update.id;
      obj.requisitionId = requisitiondata[i].id.toString();
      obj.requisitionDesignation = requisitiondata[i].designation;
      obj.requisitionGrade = requisitiondata[i].grade;
      obj.requisitionBU = requisitiondata[i].businessUnit;
      obj.requisitionOfferings = requisitiondata[i].offerings;
      obj.requisitionSolutions = requisitiondata[i].solutions;
      obj.requisitionCompetency = requisitiondata[i].competency;
      obj.requisionScore = requisitiondata[i].score;
      obj.suggestion = requisitiondata[i]?.suggestion;
      obj.requisitionParameters = requisitiondata[i]?.requisitionParameters;
      obj.isActive = requisitiondata[i].isActive;
      requistionList.push(obj);
      count++;
    }

    const RequisitionData: any = await createOrUpdateEmpProjectInterestScore(
      requistionList
    );
  };

  return (
    <Grid container alignItems="center" spacing={2} justifyContent="flex-end">
      <Grid item sx={{ marginRight: "8px" }}>
        <IconButton
          color="primary"
          onClick={handleLikeClick}
          // disabled={likeButtonDisable}
        >
          {isLiked ? (
            <ThumbUpAltIcon sx={{ color: "#4f2d7f", fontSize: "38px" }} />
          ) : (
            <ThumbUpOffAltIcon sx={{ color: "#4f2d7f", fontSize: "38px" }} />
          )}
        </IconButton>
      </Grid>
    </Grid>
  );
}

export default LikeButton;
