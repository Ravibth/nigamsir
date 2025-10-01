import axios from "axios";

const baseurl = process.env.REACT_APP_BASEAPIURL;

export const getSummaryStatisticsViewReport = async (data: any) => {
  try {
    let url = `${baseurl}Reports/v1/summary-statistics-chart`;
    return await axios.post(url, data);
  } catch (error) {
    console.log(error); //throw error;
  }
};
