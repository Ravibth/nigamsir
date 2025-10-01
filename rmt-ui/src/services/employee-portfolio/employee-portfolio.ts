import axios from "axios";
import {
  IEmployeeLeaveHolidayAndAvailablity,
  IEmployeeModel,
} from "../../common/interfaces/IEmployeeModel";

const baseurl = process.env.REACT_APP_BASEAPIURL;

// interface EmployeePortfolioResponse {
//   employees: IEmployeeModel[];
//   role: string[];
//   emp_mid: string;
// }

export const EmployeePortfolio = async (
  startDate: string,
  endDate: string
): Promise<IEmployeeLeaveHolidayAndAvailablity[]> => {
  try {
    const response = await axios.get<IEmployeeLeaveHolidayAndAvailablity[]>(
      `${baseurl}WcgtData/GetEmployeesForPortfolio?startDate=${startDate}&endDate=${endDate}`
    );

    return response.data; // contains both employees and logged in user role
  } catch (error) {
    throw error;
  }
};
