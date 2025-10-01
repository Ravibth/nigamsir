export enum EMySkillsForm {
  skillName = "skillName",
  proficiency = "proficiency",
  comments = "comments",
  expertise = "expertise",
  businessUnit = "businessUnit",
  competency = "competency",
}

export enum EProficiencyLevels {
  Basic = "Starting",
  Intermediate = "Building",
  Professional = "Skilled",
  Expert = "Excelled",
}

export enum EUserSkillsTypes {
  MatchedCompetency = "Competency Skill",
  UnMatchedCompetency = "Non - Competency Skills",
}

export const ProficiencyLevelOptions: string[] = [
  EProficiencyLevels.Basic,
  EProficiencyLevels.Intermediate,
  EProficiencyLevels.Professional,
  EProficiencyLevels.Expert,
];
export enum ESkillPopupType {
  NEW = "New",
  UPDATE = "Update",
}

export const ProficiencyLevelOptionsKeyPair = [
  {
    key: "Starting",
    value: "basic",
  },
  { key: "Building", value: "intermediate" },
  { key: "Skilled", value: "advanced" },
  { key: "Excelled", value: "expert" },
];
