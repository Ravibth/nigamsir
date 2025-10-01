import axios from "axios";

const baseurl = process.env.REACT_APP_ALLOCATION;
export const GetResourceNameService = async (payload: any) => {
  try {
    return await axios.get(baseurl + "identity/user/v5/" + payload);
  } catch (error) {
    throw error;
  }
};
