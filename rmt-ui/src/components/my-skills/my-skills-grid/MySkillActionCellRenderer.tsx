import { IconButton, Tooltip } from "@mui/material";
import { CheckIfUserSkillEntryIsEditable, EditIconSxProps } from "./utils";
import {
  IMySkillGridContextProps,
  MySkillsGridContext,
} from "./mySkillsGridContext/mySkillsGridContext";
import { useContext } from "react";
import RateReviewOutlinedIcon from "@mui/icons-material/RateReviewOutlined";

const MySkillActionCellRenderer = (props) => {
  const mySkillGridContext: IMySkillGridContextProps =
    useContext(MySkillsGridContext);

  return (
    <>
      <IconButton
        className="edit-icon"
        disabled={
          !CheckIfUserSkillEntryIsEditable(props.data) ||
          mySkillGridContext.currentEditingField ||
          !props.data.isEnabled
            ? true
            : false
        }
        onClick={(e) => {
          mySkillGridContext.setCurrentEditingField(props.data);
        }}
      >
        <Tooltip title="Edit Skill" placement="bottom">
          <RateReviewOutlinedIcon fontSize="small" sx={EditIconSxProps} />
        </Tooltip>
      </IconButton>
      {/* <Tooltip title="Delete Skill" placement="bottom">
        <IconButton
          className="edit-icon"
          disabled={
            !CheckIfUserSkillEntryIsEditable(props.data) ||
            mySkillGridContext.currentEditingField
              ? true
              : false
          }
          onClick={(e) => {
            mySkillGridContext.setCurrentEditingField(props.data);
          }}
        >
          <DeleteIcon fontSize="small" sx={EditIconSxProps} />
        </IconButton>
      </Tooltip> */}
    </>
  );
};
export default MySkillActionCellRenderer;
