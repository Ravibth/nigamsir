import { SxProps } from "@mui/material";
import { ISkillsMaster } from "../../../common/interfaces/ISkillsMaster";
import { IGetAllMySkillsResponse } from "../../../services/skills/userSkills.service";
import { ESkillPopupType, EUserSkillsTypes } from "../enums";
import { IMySkillsForm } from "../interfaces";
import { IUserDetailsContext } from "../../../contexts/userDetailsContext";

export const MySkillFormDefaultValues: IMySkillsForm = {
  skillName: null,
  competency: null,
  proficiency: "",
  comments: "",
  // businessUnit: "",
  expertise: "",
};

export interface IAddUpdateSkillProps {
  closeModal: () => void;
  openDialogToAddUpdateSkill: ESkillPopupType;
  submitSkill: (
    e: IMySkillsForm,
    closeModalAfterSubmit: boolean
  ) => Promise<boolean>;
  existingSkills: IGetAllMySkillsResponse[];
}

export const updateSkillsFilterOptionsUtils = (
  skillsMaster: ISkillsMaster[],
  allValues: IMySkillsForm
) => {
  const competency =
    allValues.competency &&
    typeof allValues.competency.competencyId === "string"
      ? [allValues.competency.competencyId]
      : allValues.competency && allValues.competency.competencyId
      ? Array.from(allValues.competency.competencyId)
      : [];

  if (competency.length === 0) {
    return skillsMaster;
  } else if (true) {
    const tempSkillOptions = skillsMaster.filter((skillItem) => {
      if (competency.length > 0) {
        const matchedByCompetency = skillItem.skill_Mapping.filter((compItem) =>
          competency.includes(compItem.competencyId)
        );
        if (matchedByCompetency.length > 0) {
          return true;
        }
      } else {
        return true;
      }
    });
    console.log(tempSkillOptions);
    return tempSkillOptions;
  }
};

export const AddUpdateSkillFilterLabel: SxProps = {
  display: "flex",
  alignItems: "center",
};

export const GetOptionsSorted = (
  skillsFilteredValues: ISkillsMaster[],
  userDetailsContext: IUserDetailsContext
) => {
  const skillInCompetencyOfUser = skillsFilteredValues
    .filter((sk) =>
      sk?.skill_Mapping?.find(
        (sm) => sm.competencyId === userDetailsContext.competencyId
      )
    )
    .map((e) => {
      return { ...e, type: EUserSkillsTypes.MatchedCompetency };
    });
  const skillNotInCompetencyOfUser = skillsFilteredValues
    .filter(
      (sk) =>
        !sk?.skill_Mapping?.find(
          (sm) => sm.competencyId === userDetailsContext.competencyId
        )
    )
    .map((e) => {
      return { ...e, type: EUserSkillsTypes.UnMatchedCompetency };
    });
  const startSkills = skillInCompetencyOfUser.sort((a, b) => {
    return a.skillName.localeCompare(b.skillName);
  });
  const endSkills = skillNotInCompetencyOfUser.sort((a, b) => {
    return a.skillName.localeCompare(b.skillName);
  });
  console.log(startSkills, endSkills);
  return [...startSkills, ...endSkills];
};
