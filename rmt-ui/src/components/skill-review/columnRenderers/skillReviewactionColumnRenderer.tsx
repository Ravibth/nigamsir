import { IconButton, Tooltip } from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import BlockIcon from "@mui/icons-material/Block";
import { EditIconSxProps } from "../../my-skills/my-skills-grid/utils";
import { WORKFLOW_ACTION_STATUS } from "../../../global/constant";

const SkillReviewActionColumnRenderers = (props) => {
  return (
    <>
      <IconButton
        className="edit-icon"
        disabled={false}
        onClick={(e) => {
          props.setItemToApproveReject(props.data);
          props.setOpenRejectApproveConfirmationModal(
            WORKFLOW_ACTION_STATUS.Approved
          );
        }}
      >
        <Tooltip title="Approve" placement="bottom">
          <CheckCircleIcon fontSize="small" sx={EditIconSxProps} />
        </Tooltip>
      </IconButton>
      <IconButton
        className="edit-icon"
        disabled={false}
        onClick={(e) => {
          props.setItemToApproveReject(props.data);
          props.setOpenRejectApproveConfirmationModal(
            WORKFLOW_ACTION_STATUS.Rejected
          );
        }}
      >
        <Tooltip title="Reject" placement="bottom">
          <BlockIcon fontSize="small" sx={EditIconSxProps} />
        </Tooltip>
      </IconButton>
    </>
  );
};

export default SkillReviewActionColumnRenderers;
