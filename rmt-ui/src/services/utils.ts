import moment from "moment";
import { GetDateAsIsoString } from "../utils/date/dateHelper";

export const createQueryUrl = (path: string, paramsData: any): string => {
  const keys = Object.keys(paramsData);
  let query = path + "?";
  keys.forEach((paramsKey) => {
    if (paramsData[paramsKey]) {
      if (typeof paramsData[paramsKey] === "number") {
        query = query + paramsKey + "=" + paramsData[paramsKey] + "&";
      } else if (typeof paramsData[paramsKey] === "object") {
        if (Array.isArray(paramsData[paramsKey])) {
          paramsData[paramsKey]?.forEach((item: any) => {
            if (typeof item === "string" || typeof item === "number") {
              query = query + paramsKey + "=" + item + "&";
            } else if (moment(paramsData[paramsKey]).isValid()) {
              const finalDate = GetDateAsIsoString(paramsData[paramsKey]);
              query = query + paramsKey + "=" + finalDate + "&";
            } else {
              paramsData[paramsKey]?.forEach((item: any) => {
                query = query + paramsKey + "=" + item + "&";
              });
            }
          });
        } else if (moment(paramsData[paramsKey]).isValid()) {
          const finalDate = GetDateAsIsoString(paramsData[paramsKey]);
          query = query + paramsKey + "=" + finalDate + "&";
        } else {
          paramsData[paramsKey]?.forEach((item: any) => {
            query = query + paramsKey + "=" + item + "&";
          });
        }
      } else {
        query = query + paramsKey + "=" + paramsData[paramsKey] + "&";
      }
    }
  });
  return query.substring(0, query.length - 1);
};
