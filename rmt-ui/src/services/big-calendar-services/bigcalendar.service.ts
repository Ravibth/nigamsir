import axios from "axios";
import { IGetUserAllocationCalander } from "./IGetUserAllocationCalanderFilterOptions";
import { createQueryUrl } from "../utils";

const baseurl = process.env.REACT_APP_ALLOCATION;

export const getProjectsOfEmployeeByEmailAndPipelineCode = async (
  employeeEmail: string,
  pipelineCode?: string
) => {
  try {
    const url = createQueryUrl(
      baseurl + `ResourceAllocation/GetProjectsByEmployeeEmailAndPipelineCode`,
      {
        email: employeeEmail,
        pipelineCode: pipelineCode,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getProjectsOfEmployeeByPipelineCode = async (
  pipelineCode: string,
  jobCode: string,
  emailId?: string,
  isAllocationDetailsFilterByUserRoles: boolean = false
) => {
  try {
    const url = createQueryUrl(
      //baseurl + `ResourceAllocation/GetProjectsByEmployeeEmailAndPipelineCode`,
      // { email: emailId, pipelineCode: pipelineCode, jobCode: jobCode }

      baseurl + `ResourceAllocation/v2/GetProjectsByPipelineCode`,
      { pipelineCode: pipelineCode, jobCode: jobCode, emailId: emailId, isAllocationDetailsFilterByUserRoles: isAllocationDetailsFilterByUserRoles}
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getAllocatedHoursRatioByPipelineCode = async (
  pipelineCode: string
) => {
  try {
    const url = createQueryUrl(
      baseurl + `ResourceAllocation/GetAllocatedHoursRatioByPipelineCode`,
      { pipelineCode: pipelineCode }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getCurrentUserAllocationCalander = async (
  request: IGetUserAllocationCalander
) => {
  try {
    return await axios.post(
      baseurl + `ResourceAllocation/GetCurrentUserAllocationCalander`,
      request
    );
  } catch (err) {
    throw err;
  }
};
// GetCurrentUserAllocationCalanderFilterOptions;
export const getCurrentUserAllocationCalanderFilterOptions = async () => {
  try {
    return await axios.get(
      baseurl +
        `ResourceAllocation/GetCurrentUserAllocationCalanderFilterOptions`
    );
  } catch (err) {
    throw err;
  }
};
