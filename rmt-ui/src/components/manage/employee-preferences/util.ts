import {
  getEmployeePreferenceByEmail,
  getPreferenceMaster,
} from "../../../services/employee-preference/employee-preference.service";
import { MasterData, PREFERENCE_CATEGORY } from "./constant";

export const GetAllSelectedLocation = (
  selectedPreferences: any[],
  selectedCategory: string
) => {
  // const categorizedData = {};
  // categorizedData[PREFERENCE_CATEGORY.LOCATION.toString().toUpperCase()] = [];
  return selectedPreferences.filter((pref) => {
    return (
      pref?.category.toString().toUpperCase() ===
      selectedCategory?.toUpperCase()
    );
  });
};

export const GetAllMasterLocation = (master: any[]) => {
  return master.filter((data) => {
    return (
      data.category.toString().toUpperCase() ===
      PREFERENCE_CATEGORY.LOCATION.toString().toUpperCase()
    );
  });
};

export const GetEmployeePreferenceData = (employeeEmail: string) => {
  return new Promise((resolve, reject) => {
    getEmployeePreferenceByEmail(employeeEmail)
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((ex) => {
        console.log(ex);
      });
  });
};

export const GetMasterPreferenceData = () => {
  return new Promise((resolve, reject) => {
    getPreferenceMaster()
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((ex) => {
        console.log(ex);
      });
  });
};

export const EmployeePreferenceStructuredData = (
  EmployeePreferenceDBData: any[]
) => {
  return EmployeePreferenceDBData.map((data) => {
    return {
      id: data.id,
      employeeEmail: data.employeeEmail,
      preferedValue: data.preferedValue,
      label: data.preferenceMaster.name,
      preferenceOrder: data.preferenceOrder,
      category: data.category,
      isActive: data.isActive,
    };
  });
};

export const PreferenceMasterStructuredData = (
  PreferenceMasterDBData: any[]
) => {
  return PreferenceMasterDBData.map((data) => {
    return {
      id: data.preferenceMasterId,
      label: data.name,
      category: data.category,
    };
  });
};

export const GetBUMasterData = (
  buTreeMappingData: any[],
  category: string
): MasterData[] => {
  let dataSet;
  let data: MasterData[] = [];
  // console.log(buTreeMappingData);
  switch (category.toLowerCase().trim()) {
    case PREFERENCE_CATEGORY.BUISNESS_UNIT.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.bu_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.bu_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.bu,
          category: PREFERENCE_CATEGORY.BUISNESS_UNIT,
          id: dataFound.bu_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.REVENUE_UNIT.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.revenue_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.revenue_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.ru_name,
          category: PREFERENCE_CATEGORY.REVENUE_UNIT,
          id: dataFound.revenue_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.EXPERTISE.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.expertise_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.expertise_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.expertise,
          category: PREFERENCE_CATEGORY.EXPERTISE,
          id: dataFound.expertise_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.SMEG.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.sme_group_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.sme_group_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.sme_group,
          category: PREFERENCE_CATEGORY.SMEG,
          id: dataFound.sme_group_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.LOCATION.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.location_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.location_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.location_name,
          category: PREFERENCE_CATEGORY.LOCATION,
          id: dataFound.location_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.INDUSTRY.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.industry_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find((x) => x.industry_id === d);
        // console.log(dataFound);
        return {
          label: dataFound.industry_name,
          category: PREFERENCE_CATEGORY.INDUSTRY,
          id: dataFound.industry_id,
        };
      });
      return data;
      break;
    case PREFERENCE_CATEGORY.SUB_INDUSTRY.toLowerCase().trim():
      dataSet = new Set(buTreeMappingData.map((d) => d.sub_industry_id));
      // console.log(dataSet);
      data = Array.from(dataSet).map((d) => {
        const dataFound = buTreeMappingData.find(
          (x) => x.sub_industry_id === d
        );
        // console.log(dataFound);
        return {
          label: dataFound.sub_industry_name,
          category: PREFERENCE_CATEGORY.SUB_INDUSTRY,
          id: dataFound.sub_industry_id,
        };
      });
      return data;
      break;
    default:
      break;
  }
  return data;
};
