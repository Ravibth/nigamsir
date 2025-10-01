// import { Button, Modal } from "react-bootstrap";
import React from "react";
import "./DialogBoxComponent.scss";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import { Grid, Typography } from "@mui/material";
import BackActionButton from "../../../components/actionButton/backactionButton";
import ActionButton from "../../../components/actionButton/actionButton";

interface DialogBoxProps {
  showDialog: boolean;
  cancelNavigation: any;
  confirmNavigation: any;
}
const DialogBox: React.FC<DialogBoxProps> = ({
  showDialog,
  cancelNavigation,
  confirmNavigation,
}) => {
  return (
    <BackDropModal
      open={showDialog}
      onclose={cancelNavigation}
      style={{ borderRadius: "10px", position: "fixed", top: "90px" }}
    >
      <Typography component={"div"} className="router-guard-modal">
        <Typography variant="h6" component="h6" className="dialog-box-header">
          <Typography component={"span"}>
            There might be some unsaved changes
          </Typography>
          <Typography component={"span"}>
            Are you sure you want to navigate?
          </Typography>
        </Typography>
        <Grid container spacing={2}>
          <Grid item xs={3} />
          <Grid item xs={3}>
            <BackActionButton
              label={"No"}
              onClick={function (e: any): void {
                cancelNavigation(e);
              }}
            />
          </Grid>
          <Grid item xs={3}>
            <ActionButton
              label={"Yes"}
              onClick={function (e: any): void {
                confirmNavigation();
              }}
              disabled={false}
              type={"button"}
            />
          </Grid>
          <Grid item xs={3} />
        </Grid>
      </Typography>
    </BackDropModal>
  );
};
export default DialogBox;
