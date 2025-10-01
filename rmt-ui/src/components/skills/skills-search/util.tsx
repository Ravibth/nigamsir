import { IUserModelMaster } from "../../../common/interfaces/IUserModel";
import {
  getSearchSkillsService,
  getSkillMaster,
} from "../../../services/skills/skill.service";
import { GetAllUsersInfo } from "../../common-allocation/utils";

export interface IDropDownOptions {
  label: string;
  id: string;
  labelId: string;
  isActive: boolean;
  grade?: string;
}


export const fetchAllSkill = async () => {
  return Promise.all([getSkillMaster()])
    .then((response: any) => {
      return response[0]?.data;
    })
    .catch((ex) => {
      throw ex;
    });
};

export const getSkillsIDropdownOptions = (data: any) => {
  let _json: IDropDownOptions[] = [];
  if (data) {
    data
      .filter((a: any) => a.isEnable === true)
      .map((item: any) => {
        _json.push({
          id: item.skill_Id,
          label:
            item.skillCode && item.skillName
              ? `${item.skillName} (${item.skillCode})`
              : `${item.skillCode} (${item.skillCode})`,
          labelId: item.skillCode,
          isActive: item.isActive ? item.isActive : item.isactive,
        });
      });
  }
  return _json;
};

export const getAllUsers = async (emails: []): Promise<IUserModelMaster[]> => {
  return Promise.all([GetAllUsersInfo(emails)])
    .then((response: any) => {
      return response ? response[0] : response;
    })
    .catch((ex) => {
      throw ex;
    });
};

export const getSearchSkills = async (skills: any) => {
  const results = await getSearchSkillsService(skills);
  if (results.status === 200) {
    const emails = results.data.map((a) => a.email);
    const _employees = await getAllUsers(emails);
    const _mergeResults = mergeSearchResults(results.data, _employees);
    return _mergeResults;
  }
  return null;
};

export const mergeSearchResults = (userSkills: any[], employees: any[]) => {
  const mergedResults = userSkills.map((uSkills) => {
    const _users = employees.find((e) => e.email_id === uSkills.email);
    return { ...uSkills, ..._users };
  });
  return mergedResults;
};
