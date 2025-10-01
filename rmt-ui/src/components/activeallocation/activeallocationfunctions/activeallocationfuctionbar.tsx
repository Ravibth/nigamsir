import { Autocomplete, Box, FormControl, Grid, TextField } from "@mui/material";
import React, { useEffect, useState } from "react";
import Allocationfilter from "../AllocationFilter/allocationfilter";
import ActionButton from "../../actionButton/actionButton";
import BackActionButton from "../../actionButton/backactionButton";
import { ButtonGridSXProps } from "./constant";
import RejectAllocationModal from "../active-allocation-modal/reject-allocation-modal/reject-allocation-modal";
import { WORKFLOW_TASK_TITLE } from "../../../global/constant";
import "./styles.css";

const Activeallocationfuctionbar = (props: any) => {
  const [open, setOpen] = useState(false);
  const [isWithdrawBtnShow, setIsWithdrawBtnShow] = useState(false);
  const { selectedTasks } = props;
  const handleOpen = () => {
    setOpen(true);
  };
  const handleCloseModal = () => setOpen(false);
  useEffect(() => {
    let _flag = false;
    if (selectedTasks && selectedTasks.length > 0) {
      _flag =
        selectedTasks.filter(
          (a: any) => a.workflow_task_title === WORKFLOW_TASK_TITLE.withdraw
        ).length > 0;
    }
    setIsWithdrawBtnShow(_flag);
  }, [selectedTasks]);
  return (
    <div>
      <>
        {open && (
          <RejectAllocationModal
            open={open}
            handleOpen={handleOpen}
            handleCloseModal={handleCloseModal}
            onConfirmClick={(e: string) => {
              setOpen(false);
              props.rejectBtnClick(e);
            }}
          />
        )}
      </>

      <Grid item xs={6}>
        {/* <Allocationfilter
            selectedDataByFilter={props.selectedDataByFilter}
            submittedFilterData={props.submittedFilterData}
            handleResetClick={props.handleResetClick}
            handleStartDateChange={props.handleStartDateChange}
            handleEndDateChange={props.handleEndDateChange}
          /> */}
      </Grid>

      {!isWithdrawBtnShow && (
        <Grid item xs={6} sx={ButtonGridSXProps} className="action-button">
          <BackActionButton
            label={"Reject"}
            onClick={() => {
              handleOpen();
            }}
          />
          <span style={{ marginLeft: "10px" }}></span>
          <ActionButton
            label={"Accept"}
            onClick={() => {
              //
              props.ApprovedBtnClick();
            }}
            type={"button"}
            disabled={false}
          />
        </Grid>
      )}
      {isWithdrawBtnShow && (
        <Grid item xs={6} sx={ButtonGridSXProps} className="action-button">
          <ActionButton
            label={"Withdraw"}
            onClick={() => {
              //
              props.WithdrawBtnClick();
            }}
            type={"button"}
            disabled={false}
          />
          <span style={{ marginRight: "10px" }}></span>
        </Grid>
      )}
    </div>
  );
};

export default Activeallocationfuctionbar;
