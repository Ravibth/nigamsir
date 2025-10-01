export const addWorkingDays = (date: Date, noOfDays: number, Holidays: any) => {
  for (let i = 0; i < noOfDays; ) {
    if (date.getDay() !== 0 && date.getDay() !== 6) {
      i++;
    }
    date.setDate(date.getDate() + 1);
  }
  return date;
};

export interface IUserSkillsWorkflowCommentDTO {
  created_by: string;
  created_at: Date;
  comment: string;
  name?: string;
}

export const getUserSkillWorkflowComment = (
  oldComments: string,
  newComment: string,
  created_by: string,
  created_at: Date,
  user: string
): string => {
  const allComments: IUserSkillsWorkflowCommentDTO[] = [];
  if (oldComments) {
    allComments.push(...JSON.parse(oldComments));
  }
  if (newComment) {
    allComments.push({
      created_by: created_by,
      created_at: created_at,
      comment: newComment,
      name: user,
    });
  }
  return JSON.stringify(allComments);
};

export const GetUnique = (input: string[]) => {
  return [...new Set(input)];
};
