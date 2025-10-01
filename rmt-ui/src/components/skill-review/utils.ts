import { ISkillsMaster } from "../../common/interfaces/ISkillsMaster";
import { IWorkflowModelMaster } from "../../common/interfaces/IWorkflowmodel";
import { EProficiencyLevels } from "../my-skills/enums";

export interface ISkillReviewGridData {
  user?: string;
  skillCode?: string;
  skillName?: string;
  priorGrading?: string;
  newGrading?: string;
  comments?: string;
  skillMaster?: ISkillsMaster;
  meta: IWorkflowModelMaster;
}

export const ModifyWorkflowModelMasterToSkillReviewGridData = (
  workflows: IWorkflowModelMaster[]
): ISkillReviewGridData[] => {
  return workflows.map((workflowModel) => {
    return {
      user: workflowModel.entity_meta_data?.Email || "",
      skillName: workflowModel.entity_meta_data?.SkillName || "",
      skillCode: workflowModel.entity_meta_data?.SkillCode || "",
      priorGrading: workflowModel.entity_meta_data?.previousUpdatedLevel || "",
      newGrading: workflowModel.entity_meta_data?.Proficiency || "",
      comments: ModifyCommentsForGridListingUserSkillsWorkflow(
        workflowModel.task_list
          .filter((task) => task.comment)
          .map((task) => task.comment)
      ),
      meta: workflowModel,
    };
  });
};

export interface IUserSkillsWorkflowCommentDTO {
  created_by: string;
  created_at: Date;
  comment: string;
  name?: string;
}

export const ModifyCommentsForGridListingUserSkillsWorkflow = (
  comments: string[]
): string => {
  const allComments: IUserSkillsWorkflowCommentDTO[] = [];
  comments.forEach((item) => allComments.push(...JSON.parse(item)));
  return allComments.map((comment) => comment.comment).join(";") || "";
};

export const getSkillProfeciencyDescriptionMapping = (params: any) => {
  const grade = params?.value?.toLowerCase()?.trim();
  let description = "";
  if (grade === EProficiencyLevels.Basic.toLocaleLowerCase().trim())
    description = params?.data?.skillMaster?.basic;
  else if (grade === EProficiencyLevels.Intermediate.toLocaleLowerCase().trim())
    description = params?.data?.skillMaster?.intermediate;
  else if (grade === EProficiencyLevels.Professional.toLocaleLowerCase().trim())
    description = params?.data.skillMaster?.advanced;
  else if (grade === EProficiencyLevels.Expert.toLocaleLowerCase().trim())
    description = params?.data?.skillMaster?.expert;
  else description = "";
  return description;
};
