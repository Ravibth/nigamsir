import React, { useState } from "react";
import { Box, IconButton, Modal, Tooltip } from "@mui/material";
import HighlightOffOutlinedIcon from "@mui/icons-material/HighlightOffOutlined";
import { IBackDropModalProps } from "./interface";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "auto",
  height: "auto",
  bgcolor: "background.paper",
  padding: "30px",
  borderRadius: "15px",
};

const BackDropModal = (props) => {
  const [openBackPromptDialog, setOpenBackPromptDialog] =
    useState<boolean>(false);

  const checkForClose = () => {
    if (props.restrictOnClose) {
      setOpenBackPromptDialog(true);
    } else {
      props.onclose();
    }
  };

  return (
    <>
      {openBackPromptDialog && (
        <DialogBox
          showDialog={openBackPromptDialog}
          confirmNavigation={() => {
            setOpenBackPromptDialog(false);
            props.onclose();
          }}
          cancelNavigation={() => {
            setOpenBackPromptDialog(false);
          }}
        />
      )}
      <Modal
        open={props.open}
        onClose={(e, reason) => {
          if (reason !== "backdropClick") {
            checkForClose();
          }
        }}
      >
        <Box sx={props?.style ? { ...style, ...props.style } : { ...style }}>
          <Box>
            <IconButton
              sx={{
                position: "absolute",
                top: 0,
                right: 0,
                zIndex: 1,
              }}
              onClick={() => checkForClose()}
            >
              <Tooltip title="Close">
                <HighlightOffOutlinedIcon />
              </Tooltip>
            </IconButton>
            {props.children}
          </Box>
        </Box>
      </Modal>
    </>
  );
};

export default BackDropModal;
