import { Box, Grid, Modal, Tooltip, Typography } from "@mui/material";
import React from "react";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import "./style.css";
import CloseIcon from "@mui/icons-material/Close";
import { UpdateConfigurationType } from "./update-config-modal-constant";

export interface IUpdateConfigModal {
  open: boolean;
  handleCloseModal: Function;
  changeAcceptedHandler: Function;
  type?: "save" | "cancel";
  setIsDirty?: Function;
}

const UpdateConfigModal = (props: IUpdateConfigModal) => {
  const { open, handleCloseModal, changeAcceptedHandler, type, setIsDirty } =
    props;
  const handleChangeClick = (status: boolean) => {
    changeAcceptedHandler(status);
    handleCloseModal();
  };
  return (
    <div>
      <Modal
        open={open}
        onClose={() => {
          handleChangeClick(false);
        }}
      >
        <Box sx={UpdateConfigurationType}>
          <>
            <Typography
              className="assign-modal-header"
              id="modal-modal-title"
              variant="h5"
              component="h5"
            >
              <span className="modal-heading">
                {type === "save" && "Save Changes"}
                {type === "cancel" && "Cancel Changes"}
              </span>

              <span>
                <Tooltip title={"Close"}>
                  <CloseIcon
                    onClick={() => {
                      handleChangeClick(false);
                    }}
                  />
                </Tooltip>
              </span>
            </Typography>

            <Box sx={{ mt: 2 }}>
              <div>
                <div>
                  {type === "save" && (
                    <span>Are you sure you want to Save changes?</span>
                  )}
                  {type === "cancel" && (
                    <span>Are you sure you want to Cancel changes?</span>
                  )}
                </div>
              </div>

              <Box sx={{ mt: 2 }} className="confrim-btn-main">
                <Grid item xs={2}>
                  <BackActionButton
                    label={"No"}
                    onClick={() => {
                      handleChangeClick(false);
                    }}
                  />
                </Grid>

                <Grid item xs={2} className="confrim-btn">
                  <ActionButton
                    label={"Yes"}
                    onClick={() => {
                      handleChangeClick(true);
                      setIsDirty(false);
                    }}
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

export default UpdateConfigModal;
