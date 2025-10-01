import axios from "axios";

const baseurl = process.env.REACT_APP_MARKETPLACE;

export const getPublishedFieldByprojectCodeForMarketPlace = async (
  pipelineCode: any,
  jobCode: any
) => {
  try {
    return await axios
      .get(
        baseurl +
          `Project/GetPublishedFieldByprojectCodeForMarketPlace?pipelineCode=${encodeURIComponent(
            pipelineCode
          )}&jobCode=${encodeURIComponent(jobCode)}`
      )
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
