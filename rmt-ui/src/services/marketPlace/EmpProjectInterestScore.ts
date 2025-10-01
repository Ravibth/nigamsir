import axios from "axios";

//todo: chnage local host url
const baseurl = process.env.REACT_APP_MARKETPLACE;

export const createOrUpdateEmpProjectInterestScore = async (payload: any) => {
  try {
    return await axios
      .post(
        baseurl + `MarketPlace/CreateOrUpdateEmpProjectInterestScore`,
        payload
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
