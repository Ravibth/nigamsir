import {
  Box,
  Grid,
  Modal,
  Tooltip,
  Typography,
  TextField,
} from "@mui/material";
import React, { useState } from "react";
import BackActionButton from "../../../actionButton/backactionButton";
import ActionButton from "../../../actionButton/actionButton";
import CloseIcon from "@mui/icons-material/Close";
import { DeleteRequisitionModalSXProps } from "../../../activeRequisitionsDeatils/activerequisition/active-requisition-modal/constant";
import "./reject-allocation-modal.css";

const RejectAllocationModal = (props: any) => {
  const [comments, setComments] = useState("");

  return (
    <div>
      <Modal open={props.open} onClose={props.handleCloseModal}>
        <Box sx={DeleteRequisitionModalSXProps}>
          <>
            <Typography
              className="assign-modal-header"
              id="modal-modal-title"
              variant="h5"
              component="h5"
            >
              <span className="modal-heading">
                {" "}
                Reason For Rejection{" "}
                <span className="reject-eng-astrix"> * </span>
              </span>

              <span>
                <Tooltip title={"Close"}>
                  <CloseIcon
                    onClick={() => {
                      setComments("");
                      props.handleCloseModal();
                    }}
                  />
                </Tooltip>
              </span>
            </Typography>
            <Box sx={{ mt: 2 }}>
              <div className="reason-message-textfield">
                <TextField
                  className="reason-meesage-text"
                  multiline
                  rows={5}
                  inputProps={{
                    maxLength: 255,
                  }}
                  onChange={(e: any) => {
                    setComments(e.target.value);
                  }}
                />
              </div>
              <Box sx={{ mt: 2 }} className="confrim-btn-main">
                <Grid item xs={2}>
                  <BackActionButton
                    label={"Cancel"}
                    onClick={() => {
                      setComments("");
                      props.handleCloseModal();
                    }}
                  />
                </Grid>
                <Grid item xs={0.5}></Grid>
                <Grid item xs={3} className="confrim-btn">
                  <ActionButton
                    label={"Confirm"}
                    onClick={() => {
                      props.onConfirmClick(comments);
                    }}
                    type={"button"}
                    disabled={comments ? false : true}
                  />
                </Grid>
              </Box>
            </Box>
          </>
        </Box>
      </Modal>
    </div>
  );
};

export default RejectAllocationModal;
