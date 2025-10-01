import axios from "axios";
import { createQueryUrl } from "../utils";

const baseurl = process.env.REACT_APP_ALLOCATION;

export const getResourceAllocationDayGroup = async (
  pipelineCode: string,
  jobCode: string,
  timeOption: string,
  startDate: string,
  endDate: string
) => {
  try {
    const url = createQueryUrl(baseurl + `Budget/AllcoatedActualGraph`, {
      PipelineCode: pipelineCode,
      JobCode: jobCode,
      TimeOption: timeOption,
      StartDate: startDate,
      EndDate: endDate,
    });
    return await axios.get(url);
  } catch (error) {
    console.log(error); //throw error;
  }
};

export const getActualTimesheet = async (
  jobCode: string,
  timeOption: string
) => {
  try {
    const url = createQueryUrl(baseurl + `WcgtData/GetTimesheetGroup`, {
      JobCode: jobCode,
      TimeOption: timeOption,
    });
    return await axios.get(url);
  } catch (error) {
    console.log(error); //throw error;
  }
};
export const getBudgetDesignationWise = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    const url = createQueryUrl(baseurl + `Budget/BudgetDesignationWise`, {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
    });
    return await axios.get(url);
  } catch (error) {
    console.log(error); //throw error;
  }
};

export const getBudgetOverview = async (overviewRequest: any) => {
  try {
    const url = createQueryUrl(baseurl + `Budget/BudgetOverview`, "");
    return await axios.post(url, overviewRequest);
  } catch (error) {
    console.log(error); //throw error;
  }
};
export const resourceActualPlannedGraph = async (
  pipelineCode: string,
  jobCode: string,
  start_date: string,
  end_date: string
) => {
  try {
    const url = createQueryUrl(baseurl + `Budget/ActualPlannedResourceView`, {
      PipelineCode: pipelineCode,
      JobCode: jobCode,
      StartDate: start_date,
      EndDate: end_date,
    });
    return await axios.get(url);
  } catch (error) {
    console.log(error); //throw error; //todo: Rahul
  }
};

export const getProjectBudget = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    const url = createQueryUrl(baseurl + `Project/GetProjectBudget`, {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
    });
    return await axios.get(url);
  } catch (error) {
    console.log(error); //throw error;
  }
};