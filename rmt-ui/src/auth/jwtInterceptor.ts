import axios from "axios";
import { getAccessToken, getToken } from "./authService";

export function jwtInterceptor() {
  axios.interceptors.request.use(async (req: any) => {
    const token = await getToken();
    const accessToken = await getAccessToken();

    if (token) {
      req.headers!.Authorization = `Bearer ${token}`;
      req.headers!.access_Token = `Bearer ${accessToken}`;
      req.headers["access-token"] = `Bearer ${accessToken}`;
    }
    return req;
  });
}
