import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import {
  Autocomplete,
  Divider,
  Grid,
  IconButton,
  Paper,
  TextField,
  Tooltip,
} from "@mui/material";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import CloseIcon from "@mui/icons-material/Close";
import { DeleteRequisitionModalSXProps } from "../constant";

import "./delete-requisition-modal.css";
import ActionButton from "../../../../actionButton/actionButton";
import BackActionButton from "../../../../actionButton/backactionButton";
import { useContext } from "react";
import { SnackbarContext } from "../../../../../contexts/snackbarContext";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../../../contexts/loaderContext";

export default function DeleteRequisitionModal(props: any) {
  const snackbarContext: any = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const deleteRequisition = () => {
    loaderContext.open(true);
    Promise.all([props.deleteRequisition({ id: props.selectedRequisitionId })])
      .then((values) => {
        if (values[0] === 200) {
          const updatedRequisitionList: any = props.requisitionList.filter(
            (e: any) => e.requisionId.toString() !== props.selectedRequisitionId
          );
          props.setRequisitionsList(updatedRequisitionList);
        }
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Requisition deleted successfully",
          "success"
        );
      })
      .catch((err) => {
        loaderContext.open(false);
        snackbarContext.displaySnackbar("Requisition deletion failed", "error");
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
              <span className="modal-heading">Delete Requisition</span>

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
                  <span>Do you want to delete this requisition?</span>
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
                    onClick={deleteRequisition}
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
