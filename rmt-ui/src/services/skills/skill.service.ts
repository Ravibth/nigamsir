import axios from "axios";
import { createQueryUrl } from "../utils";
import { Iskill } from "../../components/skill-master/Interface/Iskill";
import { ISkillStatusUpdate } from "../../components/skill-master/Interface/ISkillStatusUpdate";

const baseurl = process.env.REACT_APP_BASEAPIURL;

export const getSkillMaster = async () => {
  try {
    return await axios.get(baseurl + "Skill/GetAllSkill");
  } catch (error) {
    throw error;
  }
};

export const getSkillCategoryMaster = async () => {
  try {
    return await axios.get(baseurl + "Skill/GetSkiilCategoryMaster");
  } catch (error) {
    throw error;
  }
};

export const getSkillByName = async (skill: string) => {
  try {
    const skillName = encodeURIComponent(skill);
    return await axios.get(
      createQueryUrl(baseurl + "Skill/GetSkillByName", { skillName })
    );
  } catch (error) {
    throw error;
  }
};

export const addNewSkill = async (payload: Iskill) => {
  try {
    return await axios.post(baseurl + "Skill/SubmitSKill", payload);
  } catch (err) {
    throw err;
  }
};
export const changeSkillStatus = async (payload: ISkillStatusUpdate) => {
  try {
    return await axios.put(baseurl + "Skill/UpdateSkillStatus", payload);
  } catch (err) {
    throw err;
  }
};

export const updateSkill = async (payload: Iskill) => {
  try {
    return await axios.put(baseurl + "Skill/UpdateSkill", payload);
  } catch (err) {
    throw err;
  }
};

export const getSearchSkillsService = async (payload: string[]) => {
  try {
    return await axios.post(
      baseurl + "Skill/SearchSkillsByNameOrCode",
      payload
    );
  } catch (error) {
    throw error;
  }
};

export const getSkillsByCodeOrName = async (payload: string[]) => {
  try {
    let resp = await axios.post(
      baseurl + "Skill/GetAllSkillBySkillCodeOrName",
      payload
    );
    return resp.data;
  } catch (error) {
    throw error;
  }
};
