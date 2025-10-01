import { IBUTreeMapping } from "../../common/interfaces/IBuTreeMapping";
import { ISkillsMaster } from "../../common/interfaces/ISkillsMaster";
import { getAllBusinessMaster } from "../../services/configuration-services/configuration.service";
import { getSkillMaster } from "../../services/skills/skill.service";
import {
  AddUpdateMySkills,
  IAddUpdateMySkillsRequest,
} from "../../services/skills/userSkills.service";
import { getAllCompetency } from "../../services/wcgt-master-services/wcgt-master-services";

export const AddUpdateMySkillsUtils = (
  payload: IAddUpdateMySkillsRequest[]
) => {
  return new Promise((resolve, reject) => {
    AddUpdateMySkills(payload)
      .then((resp) => {
        resolve(resp);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

export const GetAllSkillsMaster = (): Promise<ISkillsMaster[]> => {
  return new Promise<ISkillsMaster[]>((resolve, reject) => {
    getSkillMaster()
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

export const GetBusinessUnitsMaster = (): Promise<IBUTreeMapping[]> => {
  return new Promise<IBUTreeMapping[]>((resolve, reject) => {
    getAllBusinessMaster()
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((err) => {
        reject(err);
      });
  });
};
export const GetCompetencyMaster = async () => {
  try {
    return await getAllCompetency();
  } catch (err) {}
};

export const GetDistinctBUOptionValues = (buTreeMapping: IBUTreeMapping[]) => {
  const distinctBuTreeMapping = Array.from(
    new Set(buTreeMapping.map((item) => item.bu)).values()
  );
  const filteredBu = distinctBuTreeMapping
    .map((item) => buTreeMapping.find((bu) => bu.bu === item))
    .map((bu) => bu.bu);
  return filteredBu;
};
