import axios from "axios";
import { ISetPublishedToMarketPlace } from "../../components/Marketplace/interfaces/marketplaceinterfaces";

const baseurl = process.env.REACT_APP_MARKETPLACE;

//Create interface for datatype ISetPublishedToMarketPlace
export const updatePublishedToMarketPlace = async (
  payload: ISetPublishedToMarketPlace[]
) => {
  try {
    return await axios
      .post(baseurl + "Project/SetPublishedToMarketPlace", payload)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw { err: error };
  }
};
