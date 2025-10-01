import axios from "axios";
import { createQueryUrl } from "../utils";

const baseurl = process.env.REACT_APP_EMPLOYEE_PREFERENCE;

export const getEmployeePreferenceByEmail = async (employeeEmail: string) => {
  try {
    const url = createQueryUrl(
      baseurl + `Employee/GetEmployeePreferenceByEmail`,
      { EmployeeEmail: employeeEmail }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getPreferenceMaster = async () => {
  try {
    return await axios.get(baseurl + `Employee/GetAllPreferenceMasteer`);
  } catch (error) {
    throw error;
  }
};

export const updateEmployeePreference = async (employeePreferences: any) => {
  try {
    return await axios.post(
      baseurl + `Employee/UpdateEmployeePreference`,
      employeePreferences
    );
  } catch (error) {
    throw error;
  }
};
