import axios from "axios";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { IBuOfferingsMasterList } from "../../common/interfaces/IBuOfferingsMaster";
import BuTreeMapping from "./buTreeMapping.json";
import { IPortfolioFiltersOptions } from "../../common/interfaces/IPortfolioFiltersOptions";
import { IEmployeeLeaveHolidayAndAvailablity, IEmployeeModel } from "../../common/interfaces/IEmployeeModel";
import { IClient } from "../../common/interfaces/IClient";
import { IPortfolioFiltersReport } from "../../common/interfaces/IPortfolioFiltersReport";

const baseurl = process.env.REACT_APP_WCGT;
//  GET DESIGNATION MASTER
export const getDesignationFromWcgt = async () => {
  try {
    return await axios.get(baseurl + `WcgtData/GetDesignationList`);
  } catch (error) {
    throw error;
  }
};
// GET BU-TREE-MAPPING
export const getBU_Exp_SME_RUFromWcgt = async () => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetBUTreeMappingList")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
// GET LOCATION MASTER
export const getLocationMasterFromWCGT = async () => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetLocationList")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
export const getIndustryMasterFromWCGT = async () => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetSectorIndustryList")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const getPipelineData = async () => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetPipelineList")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const getDesignationList = async () => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetDesignationList")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const getWCGTJobByJobCode = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    const url =
      baseurl +
      "WcgtData/GetJobByJobCode?pipelineCode=" +
      pipelineCode +
      "&jobCode=" +
      jobCode;
    return await axios.get(url).then((resp: any) => {
      return resp.data;
    });
  } catch (error) {
    throw error;
  }
};
//COMPETENCY MASTER DATA
export const getAllCompetency = async (): Promise<ICompetencyMaster[]> => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetCompetencyList")
      .then((response) => {
        const modifiedCompetencyMaster: ICompetencyMaster[] = response.data.map(
          (item) => {
            const finalResp: ICompetencyMaster = {
              ...item,
              competencyId: item.competencyId,
              competency: item.competencyName,
              isActive: item.isactive,
            };
            return finalResp;
          }
        );
        return modifiedCompetencyMaster;
      });
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const getAllCompetencyByMID = async (
  mid: string
): Promise<ICompetencyMaster[]> => {
  try {
    return await axios
      .get(baseurl + "WcgtData/GetCompetencyList?competency_leader_mid=" + mid)
      .then((response) => {
        const modifiedCompetencyMaster: ICompetencyMaster[] = response.data.map(
          (item) => {
            const finalResp: ICompetencyMaster = {
              ...item,
              competencyId: item.competencyId,
              competency: item.competencyName,
              isActive: item.isactive,
              buId: item.buId,
              competencyLeaderMID: item.competencyLeaderMID,
            };
            return finalResp;
          }
        );
        return modifiedCompetencyMaster;
      });
  } catch (error) {
    console.log(error);
    throw error;
  }
};

//BU_OFFERINGS MASTER DATA
export const getAllBuOfferings = async (): Promise<IBuOfferingsMasterList> => {
  try {
    // return BuTreeMapping;
    const resp = await axios.get(baseurl + "WcgtData/GetBUTreeMappingList");
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

//GET PORTFOLIO FILTERS OPTIONS  
export const get_Portfolio_Filters_Options = async (payload: any): Promise<IPortfolioFiltersOptions> => {
  try {
    // return BuTreeMapping;
    var resp = await axios.post(
      baseurl + "WcgtData/GetPortfolioFiltersOptions",
      payload
    );
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const downloadPortfolioReport = async (payload: any): Promise<IPortfolioFiltersReport> => {
  try {
    // return BuTreeMapping;
    var resp = await axios.post(
      baseurl + "WcgtData/GetEmployeesForPortfolioReport",
      payload
    );
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};


export const get_EmployeePortfolio = async (payload: any): Promise<IEmployeeLeaveHolidayAndAvailablity[]> => {
  try {
    var resp = await axios.post(
      baseurl + "WcgtData/GetEmployeesForPortfolio",
      payload
    );
    return resp.data;
  } catch (err) {
    throw err;
  }
};

export const getEmployeeLists = async (): Promise<IEmployeeModel[]> => { 
  try {
    // return BuTreeMapping;
    const resp = await axios.get(baseurl + "WcgtData/GetEmployeeList");
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const getAllSupercoachLists = async (): Promise<IEmployeeModel[]> => { 
  try {
    // return BuTreeMapping;
    const resp = await axios.get(baseurl + "WcgtData/GetAllSuperCoach");
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

export const getClientList = async (): Promise<IClient[]> => {
  try {
    // return BuTreeMapping;
    const resp = await axios.get(baseurl + "WcgtData/GetClientList");
    return resp.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
  
};