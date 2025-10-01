import axios from "axios";
import { IAddProjectToMarketPlace } from "../../components/Marketplace/interfaces/marketplaceinterfaces";

//todo: chnage local host url
const baseurl = process.env.REACT_APP_MARKETPLACE;

export const addProjectToMarketPlace = async (
  payload: IAddProjectToMarketPlace
) => {
  try {
    return await axios
      .post(baseurl + `MarketPlace/AddProjectToMarketPlace`, payload)
      .then((resp: any) => {
        return { result: resp.data, err: false };
      });
  } catch (error) {
    //throw(error)
    return { result: error, err: true };
  }
};
