import axios from "axios";
import { createQueryUrl } from "../utils";

//todo: chnage local host url
const baseurl = process.env.REACT_APP_MARKETPLACE;

export interface IGetAllEmpProjectInterestScoreResponse {
  empProjectInterestId?: number;
  requisitionId?: string;
  requisitionDesignation?: string;
  requisitionGrade?: string;
  requisionScore?: string;
  suggestion?: string;
  isInterested?: boolean;
  empName?: string;
  empEmail?: string;
  isActive?: boolean;
}

export const getAllEmpProjectInterestScore = async (
  pipelineCode: string,
  jobCode: string
): Promise<IGetAllEmpProjectInterestScoreResponse[]> => {
  try {
    const url = createQueryUrl(
      baseurl + `MarketPlace/GetAllEmpProjectInterestScore`,
      { PipelineCode: pipelineCode, JobCode: jobCode }
    );
    return await axios.get(url).then((resp: any) => {
      return resp.data;
    });
  } catch (err) {
    throw err;
  }
};
