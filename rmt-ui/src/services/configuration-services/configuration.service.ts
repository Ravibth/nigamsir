import axios from "axios";
import { CONFIGURATION_SERVICES } from "../../global/service-constant";
import { createQueryUrl } from "../utils";
import { ILogger } from "./ILogger";
import { IConfigurationMainBreakupMetaValues } from "../../common/interfaces/IConfigurationMaster";

const baseUrl = process.env.REACT_APP_CONFIGURATION;

export enum EConfigurationConfigGroup {
  RequisitionForm = "Requisition_form",
}

export const getProjectConfigurationByConfigGroupAndConfigType = async (
  configGroup: string,
  configType: string
) => {
  try {
    const url = createQueryUrl(
      baseUrl +
        `Configuration/GetProjectConfigurationByConfigGroupAndConfigType`,
      { ConfigGroup: configGroup, ConfigType: configType }
    );

    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};
export const getGonfigurationGroupByConfigGroupAndConfigType = async (
  configGroup: string,
  configType: string
) => {
  try {
    const url = createQueryUrl(
      baseUrl + `Configuration/GetConfigurationGroupByGroupNameAndConfigType`,
      { groupName: configGroup, ConfigType: configType }
    );

    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};

export const UpdateConfiguration = async (updateConfiguration: any) => {
  try {
    return await axios.post(
      baseUrl + `Configuration/UpdateConfiguration`,
      updateConfiguration
    );
  } catch (err) {
    throw err;
  }
};

export const getDesignationList = async () => {
  try {
    return await axios.get(baseUrl + CONFIGURATION_SERVICES.getDesignationList);
  } catch (err) {
    throw err;
  }
};

export const getLocationList = async () => {
  try {
    return await axios.get(baseUrl + CONFIGURATION_SERVICES.getLocationList);
  } catch (err) {
    throw err;
  }
};

export const getSectorIndustryList = async () => {
  try {
    return await axios.get(
      baseUrl + CONFIGURATION_SERVICES.getSectorIndustryList
    );
  } catch (err) {
    throw err;
  }
};

export const getAllBusinessMaster = async () => {
  try {
    return await axios.get(
      baseUrl + CONFIGURATION_SERVICES.getAllBusinessMaster
    );
  } catch (err) {
    throw err;
  }
};

export const getBUTreeMappingListByMID = async (employee_mid: string) => {
  try {
    return await axios.get(
      baseUrl + "WcgtData/GetBUTreeMappingListByMID?mid=" + employee_mid
    );
  } catch (err) {
    throw err;
  }
};

export const getEmployeesSuperCoachOrCSCByMID = async (employee_mid: string) => {
  try {
    return await axios.get(
      baseUrl + "WcgtData/GetEmployeeBySuperCoachOrCSC?emp_mid=" + employee_mid
    );
  } catch (err) {
    throw err;
  }
};
 

export const GetExpertiesConfigurationByExpertiesNameAndConfigGroup = async (
  expertiesName: string,
  groupName: string
) => {
  try {
    const url = createQueryUrl(
      baseUrl +
        `Configuration/GetExpertiesConfigurationByExpertiesNameAndConfigGroup`,
      {
        expertiesName: encodeURIComponent(expertiesName),
        configurationGroup: encodeURIComponent(groupName),
      }
    );
    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};

export const LogContentService = async (logObject: ILogger) => {
  try {
    return await axios.post(baseUrl + `Configuration/LogContent`, logObject);
  } catch (err) {
    throw err;
  }
};

export const GetConfigurationMasterList = async () => {
  try {
    return (await axios.get(baseUrl + `Configuration/GetConfigurationMaster`))
      .data;
  } catch (err) {
    throw err;
  }
};

export const GetApplicationLevelSettings = async (key?: string) => {
  try {
    let url = baseUrl + `Configuration/GetApplicationLevelSettings`;
    if (key) {
     // url = createQueryUrl(url, { keys: `${key}` });
     url = baseUrl + `Configuration/GetApplicationLevelSettings?keys=${key}` ;
    }
    return (await axios.get(url)).data;
  } catch (err) {
    throw err;
  }
};

export interface IAddUpdateConfigurationMasterListRequestPayload {
  configurationMasterId: string;
  keySelector: string;
  configurationMetaValues: IConfigurationMainBreakupMetaValues[];
  isActive: boolean;
}

export const AddUpdateConfigurationMasterList = async (
  payload: IAddUpdateConfigurationMasterListRequestPayload[]
) => {
  try {
    return (
      await axios.post(
        baseUrl + `Configuration/UpdateConfigurationBreakup`,
        payload
      )
    ).data;
  } catch (err) {
    throw err;
  }
};
