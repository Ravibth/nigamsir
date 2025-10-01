import { resolve } from "path";
import { data } from "../assign-resources/constant";
import { rejects } from "assert";
import { updateEmployeePreference } from "../../services/employee-preference/employee-preference.service";
import { PreferencesLength } from "./constant";
import {
  EmployeePreferenceData,
  PREFERENCE_CATEGORY,
} from "./employee-preferences/constant";
import { IEmployeePreferenceList } from "./interface";

export const GetAllMasterPreferenceData = () => {};
export const GetDataToUpdate = (
  initialPreference: any[],
  updatedPreference: any[]
) => {
  const a1 = updatedPreference.map((data) => data.preferedValue);
  const a2 = initialPreference.map((data) => data.preferedValue);
  const p1 = initialPreference
    // .filter((data: any) => a1.includes(data.preferedValue))
    .map((val: any) => {
      if (a1.includes(val.preferedValue)) {
        return {
          id: val.id,
          name: val.preferenceMaster.name,
          employeeEmail: val.employeeEmail,
          preferedValue: val.preferedValue,
          category: val.category,
          isActive: a1.length === 0 ? false : true,
        };
      } else {
        return {
          id: val.id,
          name: val.preferenceMaster.name,
          employeeEmail: val.employeeEmail,
          preferedValue: val.preferedValue,
          category: val.category,
          isActive: false,
        };
      }
    });
  const p3 = updatedPreference
    .filter((data: any) => !a2.includes(data.preferedValue))
    .map((val: any) => {
      return {
        id: 0,
        name: val.label,
        employeeEmail: val.employeeEmail,
        preferedValue: val.preferedValue,
        category: val.category,
        isActive: true,
      };
    });
  return [...p1, ...p3];
};
// export const EmployeePreferenceStructuredData = (
//   EmployeePreferenceDBData: any[]
// ): IEmployeePreferenceList[] => {
//   return EmployeePreferenceDBData.map((data) => {
//     return {
//       id: data.id,
//       employeeEmail: data.employeeEmail,
//       preferenceName: data.preferenceName,
//       preferenceId: data.preferenceId,
//       label: data.preferenceName,
//       category: data.category,
//       isActive: data.isActive,
//       preferenceOrder: data.preferenceOrder,
//     };
//   });
// };
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

export const checkUpdatePayloadValidity = (UpdatePayload: any[]) => {
  const categorySet = new Set();
  let hasError = false;
  let prefWithError: any = [];
  const ListOfAllPreferences = Object.keys(PREFERENCE_CATEGORY);
  let ListOfPrefNotAvailable: any = [];
  UpdatePayload.forEach((updateData) => {
    categorySet.add(updateData.category);
  });
  console.log(ListOfAllPreferences);
  let ListOfPrefAvailable = Array.from(categorySet);
  ListOfPrefNotAvailable = ListOfAllPreferences.filter(
    (d) => !ListOfPrefAvailable.includes(d)
  );
  if (categorySet.size === PreferencesLength) {
    hasError = false;
  } else {
    hasError = true;
    prefWithError = ListOfPrefNotAvailable;
  }
  return {
    hasError,
    prefWithError,
  };
};
// export const UpdateEmployeePreference = (data : any) => {
//   return new Promise((resolve , rejects) => {
//     updateEmployeePreference(data).then((resp) => {
//       resolve(resp);
//     }).catch((err) => {
//       console.log(err)
//     })
//   })
// }
