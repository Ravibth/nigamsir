import React, { useContext } from "react";
import ActionButton from "../../../actionButton/actionButton";
import { Box, Grid, Modal, SxProps, Tooltip, Typography } from "@mui/material";
import BackActionButton from "../../../actionButton/backactionButton";
import CloseIcon from "@mui/icons-material/Close";
import "./style.css";
import { SnackbarContext } from "../../../../contexts/snackbarContext";

const DeleteRequisitionModalSXProps: SxProps = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "25%",
  bgcolor: "#fff",
  boxShadow: 24,
  p: 3,
  borderRadius: "15px",
};

const ResourceReleaseModal = (props: any) => {
  const snackbarContext: any = useContext(SnackbarContext);

  const deleteAllocationResource = () => {
    Promise.all([
      props.releaseAllocationResource({ guid: props.releaseResource }),
    ])
      .then((values) => {
        // if (values[0] === 200) {
        // }
        snackbarContext.displaySnackbar(
          "Resource released successfully",
          "success"
        );
      })
      .catch((err) => {
        snackbarContext.displaySnackbar("Resource release failed", "error");
      });

    props.handleCloseModal();
  };

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
              <span className="modal-heading">Release Resource</span>
              <span>
                <Tooltip title={"Close"}>
                  <CloseIcon
                    onClick={() => {
                      props.handleCloseModal();
                    }}
                  />
                </Tooltip>
              </span>
            </Typography>
            <Box sx={{ mt: 2 }}>
              <div>
                <div>
                  <span>Do you want to release this resource?</span>
                </div>
              </div>
              <Box sx={{ mt: 2 }} className="confrim-btn-main">
                <Grid item xs={2}>
                  <BackActionButton
                    label={"No"}
                    onClick={props.handleCloseModal}
                  />
                </Grid>
                <Grid item xs={2} className="confrim-btn">
                  <ActionButton
                    label={"Yes"}
                    onClick={deleteAllocationResource}
                    type={"button"}
                    disabled={false}
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

export default ResourceReleaseModal;
