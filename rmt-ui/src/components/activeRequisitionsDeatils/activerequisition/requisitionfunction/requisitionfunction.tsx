import { Grid } from "@mui/material";
import React from "react";
import * as constant from "./constant";
import ActionButton from "../../../actionButton/actionButton";
import { useNavigate } from "react-router-dom";
import Requisitionfilter from "../requisitionFilter/requisitionfilter";
import CreateAccessControl, {
  PERMISSION_TYPE,
} from "../../../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../../../common/module-permission/module-permission";
import AccessControl from "../../../../common/access-control-guard/access-control";
import {
  IsPermissionExistForProject,
  IsProjectInActiveOrClosed,
  routeValueEncode,
} from "../../../../global/utils";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import moment from "moment";
import { EPipelineStatus } from "../../../../common/enums/EProject";
import { getDateWithoutTime } from "../../../../utils/date/dateHelper";

const Requisitionfunction = (props: any) => {
  // Getting end date to disable requisition creation for expired projects
  let projectEndDate = getDateWithoutTime(props?.projectDetails?.endDate);
  let currentDate = getDateWithoutTime(new Date());
  let dateDiff = currentDate <= projectEndDate;
  const isRequisitionCreationallowed =
    props?.projectDetails?.isRequisitionCreationallowed == true && dateDiff;
  const navigate = useNavigate();
  const openCreateRequisitionHandler = (event: any) => {
    event.preventDefault();
    navigate(
      `/create-requisition/${routeValueEncode(
        props.projectDetails.pipelineCode
      )}/${routeValueEncode(props.projectDetails.jobCode)}`
    );
  };
  const userContext = React.useContext(UserDetailsContext);

  return (
    <div>
      <Grid container sx={{ padding: "20px", ...constant.FunctionBarSXProps }}>
        <Grid item xs={6}>
          {/* <Requisitionfilter
            submittedFilterData={props.submittedFilterData}
            handleResetClick={props.handleResetClick}
            handleStartDateChange={props.handleStartDateChange}
            handleEndDateChange={props.handleEndDateChange}
            selectedDataByFilter={props.selectedDataByFilter}
          /> */}
        </Grid>
        <Grid
          item
          xs={6}
          style={{ display: "flex", justifyContent: "flex-end" }}
        >
          <div style={{ marginLeft: "59%" }}>
            {IsPermissionExistForProject(
              userContext.projectPermissionData?.permissions,
              MODULE_NAME_ENUM.Requisition,
              PERMISSION_TYPE.Create,
              userContext.role
            ) && (
              <ActionButton
                label={"Create Requisition"}
                type="submit"
                disabled={
                  !isRequisitionCreationallowed ||
                  props?.projectDetails?.pipelineStatus ===
                    EPipelineStatus.Suspended ||
                  props?.projectDetails?.pipelineStatus ===
                    EPipelineStatus.Lost ||
                  IsProjectInActiveOrClosed(props?.projectDetails)
                }
                onClick={(e: any) => {
                  openCreateRequisitionHandler(e);
                }}
              />
            )}
          </div>
        </Grid>
      </Grid>
    </div>
  );
};

export default Requisitionfunction;
