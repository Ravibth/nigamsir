import { Grid, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { WORKFLOW_ACTION_STATUS } from "../../../global/constant";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";

interface IConfirmationRejectApproveSkillModalProps {
  open: boolean;
  type: WORKFLOW_ACTION_STATUS;
  handleCloseModal: () => void;
  onConfirmClick: (e: string) => void;
}

const ConfirmationRejectApproveSkillModal = (
  props: IConfirmationRejectApproveSkillModalProps
) => {
  const [comments, setComments] = useState("");

  const getModalTitle = () => {
    switch (props.type) {
      case WORKFLOW_ACTION_STATUS.Approved:
        return " Approve Skill";
      case WORKFLOW_ACTION_STATUS.Rejected:
        return " Reject Skill";
      default:
        return "";
    }
  };

  const isCommentRequiredField = () => {
    switch (props.type) {
      case WORKFLOW_ACTION_STATUS.Approved:
        return false;
      case WORKFLOW_ACTION_STATUS.Rejected:
        return true;
      default:
        return false;
    }
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Typography
          className="assign-modal-header modal-heading"
          id="modal-modal-title"
          variant="h5"
          component="h5"
        >
          {getModalTitle()}
        </Typography>
      </Grid>
      <Grid item xs={12}>
        <TextField
          multiline
          sx={{ width: "100%" }}
          label="Remarks"
          className={
            isCommentRequiredField()
              ? "input-field-group required_field"
              : "input-field-group"
          }
          inputProps={{
            maxLength: 255,
            maxRows: 7,
          }}
          onChange={(e: any) => {
            setComments(e.target.value);
          }}
        />
      </Grid>
      <Grid item xs={2}></Grid>
      <Grid item xs={5}>
        <BackActionButton
          label={"Cancel"}
          onClick={function (e: any): void {
            props.handleCloseModal();
          }}
        />
      </Grid>
      <Grid item xs={5}>
        <ActionButton
          label={"Confirm"}
          onClick={() => {
            props.onConfirmClick(comments);
          }}
          type={"button"}
          disabled={isCommentRequiredField() && !comments ? true : false}
        />
      </Grid>
    </Grid>
  );
};
export default ConfirmationRejectApproveSkillModal;
