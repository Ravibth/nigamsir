import axios from "axios";
import { ICreateOrUpdatePublishedFieldForMarketPlace } from "../../components/Marketplace/interfaces/marketplaceinterfaces";

const baseurl = process.env.REACT_APP_MARKETPLACE;

export const createOrUpdatePublishedFieldForMarketPlace = async (
  // projectID: any,
  pipelineCode: string,
  jobCode: string,
  load: any
) => {
  try {
    const l1: string[] = [];
    const l2: boolean[] = [];
    for (let key in load) {
      l1.push(key);
      l2.push(load[key]);
    }

    //Create interface for datatype ICreateOrUpdatePublishedFieldForMarketPlace
    const payload: any = {
      fieldNameList: l1,
      isActiveList: l2,
      // projectCode: projectID,
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      
    };
    return await axios
      .post(
        baseurl + "Project/CreateOrUpdatePublishedFieldForMarketPlace",
        payload
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
