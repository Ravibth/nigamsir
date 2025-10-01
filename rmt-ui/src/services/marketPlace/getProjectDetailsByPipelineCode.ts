import axios from "axios";
import { createQueryUrl } from "../utils";
import { IGetAllEmpProjectInterestScoreResponse } from "./getAllEmpProjectInterestScore";

//todo: chnage local host url
const baseurl = process.env.REACT_APP_MARKETPLACE;

export interface MarketPlaceProjectDetailReponse {
  ID?: number; // Int64? in C#
  JobCode?: string;
  JobName?: string;
  PipelineCode?: string;
  PipelineName?: string;
  ClientName?: string;
  ClientGroup?: string;
  StartDate?: Date; // DateTime? as ISO string
  EndDate?: Date; // DateTime? as ISO string
  Description?: string;
  IsPublishedToMarketPlace?: boolean;
  MarketPlacePublishDate?: Date; // DateTime? as ISO string
  CreatedDate?: Date; // DateTime? as ISO string
  CreatedBy?: string;
  ModifiedDate?: Date; // DateTime? as ISO string
  ModifiedBy?: string;
  IsActive?: boolean;
  JsonData?: string;
  ChargableType?: string;
  Location?: string;
  BusinessUnit?: string;
  BUId?: string;
  Offerings?: string;
  Solutions?: string;
  OfferingsId?: string;
  SolutionsId?: string;
  Industry?: string;
  Subindustry?: string;
  Csp?: string;
  ProposedCsp?: string;
  ElForJob?: string;
  ElForPipeLine?: string;
  JobManager?: string;
  IsInterested?: boolean;
  MarketPlaceExpirationDate?: Date; // DateTime? as ISO string
  IspipeLine?: boolean;
}

export const getMarketPlaceProjectDetail = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    const url = createQueryUrl(
      baseurl + `MarketPlace/GetProjectDetailsByPipelineCode`,
      {
        PipelineCode: encodeURIComponent(pipelineCode),
        JobCode: encodeURIComponent(jobCode),
      }
    );
    return await axios.get(url).then((resp: any) => {
      return resp.data;
    });
  } catch (err) {
    throw err;
  }
};
