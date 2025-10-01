import * as React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { Grid, Tooltip } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import "./release-resource-modal.css";
import { useContext } from "react";
import { SnackbarContext } from "../../../../contexts/snackbarContext";
import { DeleteRequisitionModalSXProps } from "../../../activeRequisitionsDeatils/activerequisition/active-requisition-modal/constant";
import BackActionButton from "../../../actionButton/backactionButton";
import ActionButton from "../../../actionButton/actionButton";

export default function ReleaseResourceModal(props: any) {
  const snackbarContext: any = useContext(SnackbarContext);

  const deleteAllocationResource = () => {
    Promise.all([
      props.releaseAllocationResource({ guid: props.selectedAllocationGuid }),
    ])
      .then((values) => {
        // if (values[0] === 200) {
        const updatedAllocationList: any = props.allocationList.filter(
          (e: any) => {
            return e.id.toString() !== props.selectedAllocationGuid;
          }
        );
        props.setAllocationList(updatedAllocationList);
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
}
