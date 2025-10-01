import axios from "axios";
const baseurl = process.env.REACT_APP_PROJECT_MS;
export const getBarChartData = async (
  startDate: string,
  endDate: string,
  businessUnit: string[],
  location: string[],
  designation: string[],
  offering: string[],
  solution: string[],
  competency: string[],
  reportType: string,
  emailId?: string,
) => {
  const payload = {
    startDate,
    endDate,
    businessUnit,
    location,
    designation,
    offering,
    solution,
    competency,
    emailId,
    reportType
  };
  //const result = '?' + new URLSearchParams(data).toString();
  // console.log("getBarChartData", payload);
  try {
    let url = `${baseurl}Reports/v1/scheduled-vs-variance-chart`;
    return await axios.post(url, payload);
  } catch (err) {
    throw err;
  }
};
