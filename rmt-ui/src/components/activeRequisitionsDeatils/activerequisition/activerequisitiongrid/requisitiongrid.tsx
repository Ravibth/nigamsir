import { Grid } from "@mui/material";
import React from "react";
import Requisitionfunction from "../requisitionfunction/requisitionfunction";
import Requisitiontable from "../requisitiontable/requisitiontable";

const RequisitionGrid = (props: any) => {
  return (
    <div>
      {/* <Grid container spacing={2}> */}
      <Requisitionfunction
        submittedFilterData={props.submittedFilterData}
        handleResetClick={props.handleResetClick}
        handleStartDateChange={props.handleStartDateChange}
        handleEndDateChange={props.handleEndDateChange}
        selectedDataByFilter={props.selectedDataByFilter}
        // projectCode={props.projectCode}
        pipelineCode={props.pipelineCode}
        jobCode={props.jobCode}
        projectDetails={props.projectDetails}
      />
      <Requisitiontable
        requisitionsList={props.requisitionsList}
        setRequisitionSelected={props.setRequisitionSelected}
        deleteRequisition={props.deleteRequisition}
        setRequisitionsList={props.setRequisitionsList}
        projectDetails={props.projectDetails}
      />
      {/* </Grid> */}
    </div>
  );
};

export default RequisitionGrid;
