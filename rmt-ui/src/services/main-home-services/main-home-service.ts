import axios from "axios";
import { getLoggedInUserInfo } from "../../auth/authService";
import { createQueryUrl } from "../utils";
const baseurl = process.env.REACT_APP_BASEAPIURL;

export async function getModuleListByUser() {
  const details = await getLoggedInUserInfo();
  if (details?.username) {
    try {
      const url = createQueryUrl(`${baseurl}identity/modulePermission/role`, {
        emailId: details.username,
      });
      const res = await axios.get(url);
      return res?.data;
    } catch (err) {
      throw new Error("Not logged in");
    }
  } else {
    throw new Error("Not logged in");
  }
}
