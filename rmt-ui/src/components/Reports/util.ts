import moment from "moment";
import { IReportDashboardFilterControl } from "./Filters/uitls";
import { RolesListMaster } from "../../common/enums/ERoles";
import { IUserDetailsContext } from "../../contexts/userDetailsContext";
import { REPORT_TYPE } from "./constant";

export const GetFilterDefaultValueOnTheBasisOfRole = (
  chartName?: string
): IReportDashboardFilterControl => {
  switch (chartName) {
    case REPORT_TYPE.STAT_CHART:
      return {
        businessUnit: [],
        expertise: [],
        smeg: [],
        location: [],
        designation: [],
        startDate: moment(new Date()).add(-1, "months").toDate(),
        endDate: moment(new Date()).add(1, "months").toDate(),
      };
    default:
      return {
        businessUnit: [],
        expertise: [],
        smeg: [],
        location: [],
        designation: [],
        startDate: moment(new Date()).add(-1, "months").toDate(),
        endDate: moment(new Date()).add(1, "months").toDate(),
      };
  }
};

export const IsEmployeeOnly = (userDetailsContext: IUserDetailsContext) => {
  const isEmp = userDetailsContext.role.includes(RolesListMaster.Employee); //&&
  return isEmp;
};

export const IsLeader = (userDetailsContext: IUserDetailsContext) => {
  const isFlag = userDetailsContext.role.includes(RolesListMaster.Leaders);
  return isFlag;
};

export const IsCeoCoo = (userDetailsContext: IUserDetailsContext) => {
  const isFlag = userDetailsContext.role.includes(RolesListMaster.CEOCOO);
  return isFlag;
};

export const IsSystemAdmin = (userDetailsContext: IUserDetailsContext) => {
  const isFlag = userDetailsContext.role.includes(RolesListMaster.SystemAdmin);
  return isFlag;
};
