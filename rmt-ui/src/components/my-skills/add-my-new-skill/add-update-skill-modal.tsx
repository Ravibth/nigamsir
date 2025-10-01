import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import { IGetAllMySkillsResponse } from "../../../services/skills/userSkills.service";
import { ESkillPopupType } from "../enums";
import { IMySkillsForm } from "../interfaces";
import AddUpdateSkill from "./add-update-skill";

export interface IAddUpdateMySkillModalProps {
  openDialogToAddUpdateSkill: ESkillPopupType;
  onCloseModal: () => void;
  submitSkill: (
    e: IMySkillsForm,
    closeModalAfterSubmit: boolean
  ) => Promise<boolean>;
  existingSkills: IGetAllMySkillsResponse[];
}

const AddUpdateMySkillModal = (props: IAddUpdateMySkillModalProps) => {
  return (
    <BackDropModal
      open={props.openDialogToAddUpdateSkill ? true : false}
      onclose={props.onCloseModal}
      style={{ width: "auto", maxWidth: "35%" }}
    >
      <AddUpdateSkill
        closeModal={props.onCloseModal}
        openDialogToAddUpdateSkill={props.openDialogToAddUpdateSkill}
        submitSkill={props.submitSkill}
        existingSkills={props.existingSkills}
      />
    </BackDropModal>
  );
};
export default AddUpdateMySkillModal;
