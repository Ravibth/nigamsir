import axios from "axios";
import { createQueryUrl } from "../utils";
import { IWorkflowModelMaster } from "../../common/interfaces/IWorkflowmodel";
import { IUserSkillsMaster } from "../../common/interfaces/IUserSkills";

const baseurl = process.env.REACT_APP_BASEAPIURL;

export interface IGetAllMySkillsResponse {
  id: string;
  skillName: string;
  skillCode: string;
  proficiency: string;
  status: string;
  email: string;
  name: string;
  empId: string;
  isActive: boolean;
  isEnabled?: boolean;
  createdAt: Date;
  createdBy: string;
  modifiedAt: Date;
  modifiedBy: string;
  comments?: IGetWorkflowCommentsByItemIdResponseTaskList[];
  commentsFetched?: boolean;
  approvedOn?: Date;
  approver?: string;
}

export const GetAllMySkills = async (
  approvals?: boolean
): Promise<IGetAllMySkillsResponse[]> => {
  try {
    const url: string = createQueryUrl(
      baseurl + "UserSkills/GetUserSkillsByEmail",
      { approvals: approvals }
    );
    return (await axios.get(url)).data;
  } catch (error) {
    throw error;
  }
};

export interface IAddUpdateMySkillsRequest {
  skillName: string;
  skillCode: string;
  proficiency: string;
  comments: string;
}

export const AddUpdateMySkills = async (
  payload: IAddUpdateMySkillsRequest[]
): Promise<IGetAllMySkillsResponse[]> => {
  try {
    const url = baseurl + "UserSkills/AddUpdateUserSkill";
    return await axios.post(url, payload);
  } catch (error) {
    throw error;
  }
};

export const getApprovedSkillByEmail = async (email: Array<string>) => {
  try {
    return await axios.post(
      baseurl + "Skill/GetUserApprovedSkillByEmail",
      email
    );
  } catch (err) {
    throw err;
  }
};

export interface IGetWorkflowCommentsByItemIdResponseTaskList {
  comment?: string;
}
export interface IGetWorkflowCommentsByItemIdResponse {
  item_id: string;
  task_list: IGetWorkflowCommentsByItemIdResponseTaskList[];
}
export const getWorkflowCommentsByItemId = async (
  itemId: Array<string>
): Promise<IGetWorkflowCommentsByItemIdResponse[]> => {
  try {
    const url = createQueryUrl(baseurl + "workflow/v1/getCommentsByItemId", {
      itemId: itemId,
    });
    return (await axios.get(url)).data;
  } catch (err) {
    throw err;
  }
};

export const getMySkillTasksAssigned = async (): Promise<
  IWorkflowModelMaster[]
> => {
  try {
    return (await axios.get(baseurl + "workflow/v1/getMySkillTasksAssigned"))
      .data;
  } catch (err) {
    throw err;
  }
};

export const GetUserSkillsWithProficiency = async (
  email: string
): Promise<IUserSkillsMaster[]> => {
  try {
    const url: string = createQueryUrl(
      baseurl + "UserSkills/GetUserSkillsWithProficiency",
      { email: email }
    );
    return (await axios.get(url)).data;
  } catch (err) {
    throw err;
  }
};
