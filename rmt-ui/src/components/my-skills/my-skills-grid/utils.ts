import { SxProps } from "@mui/material";
import { IGetAllMySkillsResponse } from "../../../services/skills/userSkills.service";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export enum EUserSkillStatus {
  PENDING = "Pending",
  PENDING_APPROVAL = "Pending Approval",
  APPROVED = "Approved",
  REJECTED = "Rejected",
}

export const CheckIfUserSkillEntryIsEditable = (
  node: IGetAllMySkillsResponse
): boolean => {
  if (node.status.toLowerCase() === EUserSkillStatus.APPROVED.toLowerCase()) {
    return true;
  } else {
    return false;
  }
};
export const EditIconSxProps: SxProps = {
  mr: 0.5,
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const commentsHoverSxProps: SxProps = {
  minWidth: "100px",
  padding: "10px",
};
export const ListCommentsHoverSxProps: SxProps = {
  marginTop: "-10px",
  width: "100%",
  maxWidth: 560,
  maxHeight: "50vh",
  overflow: "auto",
};
