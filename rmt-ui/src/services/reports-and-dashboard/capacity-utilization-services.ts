import axios from "axios";

const baseurl = process.env.REACT_APP_PROJECT_MS;
export const getCapicityUtilizationOverviewChart = async (
  startDate: string,
  endDate: string,
  business_unit: string[],
  // expertise: string[],
  // smeg: string[],
  location: string[],
  designation: string[],
  competency: string[]
  // offering: string[],
  // solution: string[]
) => {
  try {
    const payload = {
      startDate: startDate,
      endDate: endDate,
      businessUnit: business_unit,
      // experties: expertise,
      // smeg: smeg,
      location: location,
      designation: designation,
      competency,
      // offering,
      // solution,
    };
    let url = `${baseurl}Reports/v1/capacity-utilization-overview-chart`;
    return await axios.post(url, payload);
  } catch (err) {
    throw err;
  }
};
