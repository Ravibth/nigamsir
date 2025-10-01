import { Box, Button, Grid, Tooltip, Typography } from "@mui/material";
import React from "react";
import CloseIcon from "@mui/icons-material/Close";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";

const ApplyConfirmationModal = (props: any) => {
  const changeStatus = () => {
    props.afterStatusUpdate();
    props.handleClose();
  };

  const closeHandle = () => {
    props.handleClose();
  };
  return (
    <>
      <Typography
        className="assign-modal-header"
        id="modal-modal-title"
        variant="h5"
        component="h5"
      >
        {props.isUserDeactivated === true ? (
          <span className="modal-heading">Deactivate User?</span>
        ) : (
          <span className="modal-heading">Activate User?</span>
        )}
        <span>
          <Tooltip title={"Close"} placement="right">
            <CloseIcon
              onClick={() => {
                props.handleClose();
              }}
            />
          </Tooltip>
        </span>
      </Typography>
      <Box sx={{ mt: 2 }}>
        <div>
          <div>
            <span>
              {props.isUserDeactivated === true ? (
                <span>Are you sure you want to deactivate user? </span>
              ) : (
                <span>Are you sure you want to grant access to this user?</span>
              )}
            </span>
            <span>
              {props.isUserDeactivated === true ? (
                <span>
                  This will impact RMT access & tasks if any. Any draft
                  allocations for the employee will be deleted post employee
                  deactivation.
                </span>
              ) : (
                <span></span>
              )}
            </span>
          </div>
        </div>
        <Box sx={{ mt: 2 }} className="confrim-btn-main">
          <Grid item xs={1}>
            <BackActionButton label={"Cancel"} onClick={closeHandle} />
          </Grid>
          <Grid item xs={1} className="confrim-btn">
            <ActionButton
              label={"Confirm"}
              onClick={changeStatus}
              type={"button"}
              disabled={false}
            />
          </Grid>
        </Box>
      </Box>
    </>
  );
};

export default ApplyConfirmationModal;
