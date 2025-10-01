import axios from "axios";

import * as ServiceConstant from "../../global/service-constant";

const baseurl = process.env.REACT_APP_PROJECT_MS;

export const GetAllRequisitionByProjectCode = async (
  pipelineCode: any,
  jobCode: any,
  username: any,
  ScoreCalculationForRequisitionIdsAllowed: any,
  IsRequsitionFilterByProjectRoles: any = false
) => {
  try {
    const limit = 100;
    const pagination = 1;

    return await axios
      .get(
        `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.GetAllRequisitionByProjectCode}${pipelineCode}&JobCode=${jobCode}&limit=${limit}
        &pagination=${pagination}&currentEmp=${username}&ScoreCalculationForRequisitionIdsAllowed=${ScoreCalculationForRequisitionIdsAllowed}
        &IsRequsitionFilterByProjectRoles=${IsRequsitionFilterByProjectRoles}`
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
export const IsReqistionExistsInProject = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    return await axios
      .get(
        `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.IsRequisitionExistsInProjectCode}${pipelineCode}&JobCode=${jobCode}`
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
