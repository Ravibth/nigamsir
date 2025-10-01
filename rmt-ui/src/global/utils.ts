import moment from "moment";
import * as gc from "./constant";
import {
  IUserDetailsContext,
  ProjectPermissionData,
} from "../contexts/userDetailsContext";
import { getModulePermissionsBasedOnRoleName } from "../services/role-permission-service/role-permission-service";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import {
  ResourceRequestorsList,
  RolesListMaster,
} from "../common/enums/ERoles";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { IBUTreeMappingListByMID } from "../common/interfaces/IBUTreeMappingListByMID";
import { GetProjectByCode } from "../services/project-list-services/project-list-services";
import { GetUserRoleCompeteInformation } from "../components/function-bar/util";
import _ from "lodash";
import { getMarketPlaceProjectDetail } from "../services/marketPlace/getProjectDetailsByPipelineCode";
import { dateFormatYMD } from "./constant";

export const isNull = (data: any) => {
  if (data == null || data === undefined) return true;
  else if (data.length === 0 || data === "0" || data === 0) return true;
  else return false;
};

export const getTimelineDateFormate = (data: any) => {
  if (!isNull(data)) {
    const dd = new Date(data);
    const month = dd.getMonth().toString();
    const day = dd.getDay().toString();
    return `${dd.getFullYear()}-${month.length === 1 ? "0" + month : month}-${
      day.length === 1 ? "0" + day : day
    }`;
  }
  return data;
};

export const reactCalendarHight = (itemCount: number) => {
  if (!isNull(itemCount)) {
    return 50 + itemCount * 10;
  } else {
    return 50;
  }
};

export const getHoursLabel = (hours: string) => {
  if (hours) {
    if (parseInt(hours) > 1) {
      return "hours";
    } else {
      return "hour";
    }
  }
  return "";
};

export const getDateFormate = (date: Date) => {
  if (!isNull(date)) {
    return moment(date).format(gc.dateFormate);
  }
  return date;
};

export const RolesListAllowedToAllocate: RolesListMaster[] = [
  RolesListMaster.ResourceRequestor,
  RolesListMaster.Resource_Requestor,
  RolesListMaster.Delegate,
  RolesListMaster.AdditionalEl,
  RolesListMaster.AdditionalDelegate,
  RolesListMaster.EngagementLeader,
  RolesListMaster.LeadGenerator,
  RolesListMaster.JobManager,
  RolesListMaster.ProposedEL,
  RolesListMaster.EO,
];

export const CheckUserHasRoleToAllocateOnProject = (
  projectRoles: any[],
  userApplicationRoles: string[]
) => {
  return (
    projectRoles?.some((role) => RolesListAllowedToAllocate.includes(role)) ||
    userApplicationRoles.includes(RolesListMaster.SystemAdmin)
  );
};

// method to check permission of user on projectCode
export const checkUserPermissionsForProject = async (
  pipelineCode: string,
  jobCode: string,
  userContext: IUserDetailsContext,
  moduleName: string,
  permName: string
) => {
  var hasPermission = false;
  if (pipelineCode && pipelineCode !== "") {
    var projectData = await GetProjectByCode(pipelineCode, jobCode);
    if (projectData.data) {
      hasPermission = await GetProjectRoleAndPermission(
        projectData?.data,
        userContext,
        pipelineCode,
        jobCode,
        moduleName,
        permName
      );
    } else {
      console.log("project data is not available");
    }
  }

  return hasPermission;
  // if (hasPermission) {
  //   //Current User has permssion to access the page
  // } else {
  //   //Show Unathorized access error for the page
  //   ShowUnAuthorizedView();
  // }
};

export const GetProjectRoleAndPermission = async (
  projectData,
  userContext: IUserDetailsContext,
  pipelineCode: string,
  jobCode: string,
  moduleName: string,
  permName: string
) => {
  var hasPermission = false;
  //projectRolesView change not required
  var currentProjRoles = (projectData?.projectRoles || [])
    ?.filter(
      (a) =>
        a.user.toLowerCase().trim() ===
        userContext.username.toLowerCase().trim()
    )
    .map((item) => item.role);
  //projectRolesView change
  var currentProjRolesView = (projectData?.projectRolesView || [])
    ?.filter(
      (a) =>
        a.user.toLowerCase().trim() ===
        userContext.username.toLowerCase().trim()
    )
    .map((item) => item.role);
  var currentApplicationRolesView = (projectData?.projectRolesView || [])
    ?.filter(
      (a) =>
        a.user.toLowerCase().trim() ===
        userContext.username.toLowerCase().trim()
    )
    .map((item) => item.applicationRole);
  const totalProjectRolesView: string[] = [
    ...currentProjRolesView,
    ...currentApplicationRolesView,
  ];
  const totalProjectRoles = [
    ...currentProjRoles,
    ...currentApplicationRolesView,
  ];
  // cons;
  if (currentProjRoles) {
    var modulePerm = await getModulePermissionsBasedOnRoleName([
      ...currentProjRolesView,
      ...currentApplicationRolesView,
    ]);
    var projectObj: ProjectPermissionData = {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      projectRoles: totalProjectRoles,
      projectRolesView: totalProjectRolesView,
      permissions: modulePerm?.data,
    };

    userContext.setProjectPermissionData(projectObj);
    hasPermission = IsPermissionExistForProject(
      modulePerm?.data,
      moduleName,
      permName,
      userContext.role,
      userContext?.buTreeMappingListByMID,
      projectData
    );
  }
  return hasPermission;
};

export const isUserLeaderForCurrentProject = (
  buTreeMappingListByMID: IBUTreeMappingListByMID,
  projectDetails: any
): boolean => {
  const projectDetailsLower = {
    bu: projectDetails?.bu?.toLowerCase(),
    offering: projectDetails?.offerings?.toLowerCase(),
    solution: projectDetails?.solutions?.toLowerCase(),
  };

  const buValuesLower = Object.values(buTreeMappingListByMID?.bu).map(
    (value: string) => value.toLowerCase()
  );
  const offeringValuesLower = Object.values(
    buTreeMappingListByMID?.offerings
  ).map((value: string) => value.toLowerCase());
  const solutionValuesLower = Object.values(
    buTreeMappingListByMID?.solutions
  ).map((value: string) => value.toLowerCase());

  const finalPermission =
    buValuesLower.includes(projectDetailsLower?.bu) ||
    offeringValuesLower.includes(projectDetailsLower?.offering) ||
    solutionValuesLower.includes(projectDetailsLower?.solution);

  return finalPermission;
};

//check permissions based on project role wise permission
export const IsPermissionExistForProject = (
  permissionData: any,
  moduleName: string,
  permissionName: string,
  loggedInUserRoles: string[],
  buTreeMappingListByMID?: IBUTreeMappingListByMID | null,
  projectDetails?: any | null,
  projectRoles?: any[] | null
): boolean => {
  var currentPerm = permissionData;
  var hasAccess =
    currentPerm &&
    currentPerm.length > 0 &&
    currentPerm.filter(
      (f) =>
        f.is_assigned === true &&
        f.module_name === moduleName &&
        f.permissions[permissionName] === true
    ).length > 0;

  if (hasAccess) {
    return hasAccess;
  } else if (loggedInUserRoles.includes(RolesListMaster.SystemAdmin)) {
    return true;
  } else if (
    permissionName === PERMISSION_TYPE.Read &&
    (loggedInUserRoles.includes(RolesListMaster.Admin) ||
      loggedInUserRoles.includes(RolesListMaster.CEOCOO) ||
      loggedInUserRoles.includes(RolesListMaster.Leaders) ||
      loggedInUserRoles.includes(RolesListMaster.SystemAdmin) ||
      loggedInUserRoles.includes(RolesListMaster.Delegate))
  ) {
    if (
      moduleName === MODULE_NAME_ENUM.Project_Budget &&
      projectDetails &&
      buTreeMappingListByMID
    ) {
      if (loggedInUserRoles.includes(RolesListMaster.Delegate)) {
        if (projectRoles && projectRoles.length > 0) {
          if (projectRoles.includes(RolesListMaster.Delegate)) {
            return true;
          }
        }
      }
      if (
        loggedInUserRoles.includes(RolesListMaster.Leaders) &&
        !loggedInUserRoles.includes(RolesListMaster.Admin) &&
        !loggedInUserRoles.includes(RolesListMaster.CEOCOO) &&
        !loggedInUserRoles.includes(RolesListMaster.SystemAdmin)
      ) {
        return isUserLeaderForCurrentProject(
          buTreeMappingListByMID,
          projectDetails
        );
      } else {
        return true;
      }
    }
  }
};

//check permissions based on module permission state
export const IsPermissionExistForApp = (
  permissionData: any,
  moduleName: string,
  permissionName: string
) => {
  var currentPerm = permissionData;
  var hasAccess =
    currentPerm &&
    currentPerm[moduleName] &&
    currentPerm[moduleName] !== undefined &&
    currentPerm[moduleName][permissionName] === true;
  return hasAccess ? hasAccess : false;
};

export const ShowUnAuthorizedView = (navigate: any) => {
  console.log("Show UnAuthorized Access error for the page!");
};

export const getDistinctOptions = (data: any[]) => {
  return Array.from(new Set(data.map((item) => item.bu)).values());
};

export const capitalizeFirstLetter = (str: string) => {
  return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
};

export const routeValueEncode = (value: any) => {
  let encodedStr = value?.replaceAll("/", "%2F");
  return encodedStr;
};

export function jsonValuesToArray(json) {
  const valuesArray = [];

  // Iterate over each key in the JSON object
  for (const key in json) {
    if (json.hasOwnProperty(key)) {
      const value = json[key];

      // Check if the value is an object
      if (typeof value === "object") {
        // Recursively call the function for nested objects
        const nestedArray = jsonValuesToArray(value);
        valuesArray.push(...nestedArray);
      } else {
        // Push the value to the array
        valuesArray.push(value);
      }
    }
  }

  return valuesArray;
}

export const getEmailId = (emailId: string) => {
  if (emailId && emailId.indexOf("__") > -1) {
    return emailId.split("__")[1];
  }
  return emailId;
};

export const getGridToolTipValue = (params: any) => {
  if (params.value) {
    const value = getEmailId(params.value);
    if (typeof value === "object" && value !== null) {
      // If it's an object, convert it to a string
      return JSON.stringify(value);
    } else {
      // Otherwise, return the original value as string
      return String(value);
    }
  }
};

export const setTimeToEndOfDay = (date) => {
  const newDate = new Date(date);
  newDate.setHours(23, 59, 59, 999); // Set time to 23:59:59:999
  return newDate;
};

export const isEmptyString = (value: string) => {
  if (value == "null" || value == "" || value == undefined) return true;
  if (value) {
    if (value.length == 0) return true;
    return false;
  }
};

export const sortCategories = (
  categories: string[],
  preferenceOrder: string[]
) => {
  // Create a map for quick lookup of predefined order
  const orderMap = preferenceOrder.reduce((map, category, index) => {
    map[category] = index;
    return map;
  }, {});

  // Separate categories into those that are in the predefined order and those that are not
  const inOrder = [];
  const notInOrder = [];

  categories.forEach((category) => {
    if (orderMap.hasOwnProperty(category)) {
      inOrder.push(category);
    } else {
      notInOrder.push(category);
    }
  });

  // Sort categories that are in predefined order based on their index
  inOrder.sort((a, b) => orderMap[a] - orderMap[b]);

  // Combine sorted categories with those that are not in the predefined order
  return [...inOrder, ...notInOrder];
};

export const IsProjectInActiveOrClosed = (projectDetails) => {
  if (
    projectDetails &&
    projectDetails.projectActivationStatus !== null &&
    projectDetails.projectActivationStatus === false
  ) {
    return true;
  }
  if (
    projectDetails &&
    projectDetails.projectClosureState !== null &&
    projectDetails.projectClosureState === true
  ) {
    return true;
  }
  return false;
};

export const ToCheckMarketPlaceExpirationDate = async (projectDetails) => {
  if (projectDetails) {
    const result = await getMarketPlaceProjectDetail(projectDetails?.pipelineCode,projectDetails?.jobCode);
    if (result && result?.marketPlaceExpirationDate) {
      const expirationDate = moment(result?.marketPlaceExpirationDate).format(
        dateFormatYMD
      );
      const currentDate = moment(new Date()).format(dateFormatYMD);
      // Check if the expiration date is in the past
      return expirationDate >= currentDate;
    }
    return false;
  }
};

export const GetUserRoleOptions = (userContext: IUserDetailsContext) => {
  if (userContext && userContext.role && userContext.role.length > 0) {
    const uniqRoles = _.uniq(userContext.role);
    const modifiedUniqRoles = uniqRoles.filter(
      (role) =>
        ![RolesListMaster.SuperCoach.toString(),RolesListMaster.SkillSuperCoach.toString()].includes(role)
    );
    const uniqOptions = GetUserRoleCompeteInformation(modifiedUniqRoles);
    return uniqOptions;
  }
};

export const GetUserRoleOptionsPortfolio = (
  userContext: IUserDetailsContext
) => {
  if (userContext && userContext.role && userContext.role.length > 0) {
    const uniqRoles = _.uniq(userContext.role);
    // const modifiedUniqRoles = uniqRoles.filter(
    //   (role) => ![RolesListMaster.SuperCoach.toString()].includes(role)
    // );
    const uniqOptions = GetUserRoleCompeteInformation(uniqRoles);
    // console.log(uniqOptions);
    return uniqOptions;
  }
};

export const routeToEmployeeProfile=(route:string)=>{
  window.open(route, "_blank");
}