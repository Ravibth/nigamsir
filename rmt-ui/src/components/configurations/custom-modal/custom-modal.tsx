import { Box, Grid, Modal, Tooltip, Typography } from "@mui/material";
import React from "react";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import CloseIcon from "@mui/icons-material/Close";
import { ChangeConfigurationType } from "./custom-modal-constant";
import "./style.css";

const CustomModal = (props: any) => {
  const { open, handleCloseModal, changeAcceptedHandler } = props;
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
        <Box sx={ChangeConfigurationType}>
          <>
            <Typography
              className="assign-modal-header"
              id="modal-modal-title"
              variant="h5"
              component="h5"
            >
              <span className="modal-heading">Change Configuration</span>

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
                  <span>
                    Configuration change may lead to information loss ?
                  </span>
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

export default CustomModal;
