import axios from "axios";

const baseurl = process.env.REACT_APP_PROJECT_MS;

export const getAllFilterParameters = async () => {
  try {
    const url = `Project/GetProjectFilterOptionsQuery`;
    return await axios
      .get(baseurl + url)
      .then((resp: any) => {
        return resp.data;
      })
      .catch((error) => {
        throw error;
      });
  } catch (error) {}
};

export const getProjectRolesByEmailId = async (emailsIds: string[]) => {
  try {
    const url = `Project/GetProjectRolesByEmails`;
    return await axios
      .post(baseurl + url, emailsIds)
      .then((resp: any) => {
        return resp.data;
      })
      .catch((error) => {
        throw error;
      });
  } catch (error) {}
};
