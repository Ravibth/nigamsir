import { getModuleListByUser } from "../../services/main-home-services/main-home-service";

export enum MODULE_NAME_ENUM {
  Dashboard = "Dashboard",
  Reports = "Reports",
  // Project_Listing = "Project",
  Pending_Requests = "Pending Requests",
  Configurations = "Configurations",
  Marketplace = "Marketplace",
  // My_View = "My View",
  Roles_and_Permissions = "Roles and Permissions",
  My_Preferences = "My Preferences",
  Profile = "Profile",
  Project_Calender = "Project Calender",
  Project_Delegation = "Project Delegation",
  Project_Details = "Project Details",
  Project_Budget = "Project Budget",
  Requisition = "Requisition",
  Allocation = "Allocation",
  Marketplace_Interests = "Marketplace Interests",
  Project_Listing = "Project Listing",
  Assign_Additional_EL = "Assign Additional EL",
  Assign_Additional_Delegate = "Assign Additional Delegate",
  Skill_Master = "Skill Master",
}

const setModulePermissionsForLoggedInUser = (modulePermissions: any) => {
  const accessedPermissionList = (modulePermissions || []).filter(
    (modulePermission: any) => modulePermission.is_assigned
  );

  const modulePermissionFinalList = (accessedPermissionList || []).reduce(
    (acc: any, row: any) => {
      acc[row.module_name] = row.permissions;
      return acc;
    },
    {}
  );
  return modulePermissionFinalList;
};
export const getTheListOfModulePermissionsMappingToUser = async () => {
  let response;
  await getModuleListByUser().then(async (resp: any) => {
    if (resp !== "") {
      const finalList = setModulePermissionsForLoggedInUser(resp);
      response = finalList;
    }
  });
  return response;
};
