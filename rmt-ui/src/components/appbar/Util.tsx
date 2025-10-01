import axios from "axios";
import { RoleMenuInternalNameMaster } from "../../common/enums/ENavVar";
import { createQueryUrl } from "../../services/utils";

export interface ITopNavMenuItem {
  id: number;
  internalName: string;
  displayName: string;
  parentId: string;
  order: number;
  menuType: string;
  path: string;
  description: string;
  is_Expandable: boolean;
  isActive: boolean;
  createdAt: string;
  modifiedAt: string;
  createdBy: string;
  modifiedBy: string;
  pageName: string;
  children?: ITopNavMenuItem[];
}

export interface IContextMenuItem {
  id: number;
  internalName: string;
  displayName: string;
  order: number;
  description: string;
  isActive: boolean;
  createdAt: string;
  modifiedAt: string;
  createdBy: string;
  modifiedBy: string;
}

const baseurl = process.env.REACT_APP_CONFIGURATION;

export const getMenuByRoles = async (payload: any) => {
  try {
    return await axios.post(baseurl + "NavigationMenu/GetMenuByRoles", payload);
  } catch (err) {
    throw err;
  }
};

export const getContextMenuByRoles = async (payload: any) => {
  try {
    return await axios.post(
      baseurl + "NavigationMenu/GetContextMenuByRoles",
      payload
    );
  } catch (err) {
    throw err;
  }
};

export const getNavigationPaths = (basePaths: ITopNavMenuItem[]) => {
  // let tempPaths = basePaths;
  let finalPaths = basePaths
    .map((path) => {
      return {
        ...path,
        children: childItems(path.id, basePaths),
      };
    })
    .filter(
      (child) =>
        !child.parentId &&
        child.internalName.toLowerCase().trim() !==
          RoleMenuInternalNameMaster.AdminSettings.toLowerCase().trim()
    );
  return finalPaths;
};

const childItems = (parentId, basePaths: ITopNavMenuItem[]) => {
  const childPaths = basePaths
    .filter((item) => item.parentId?.toString() === parentId?.toString())
    .map((path) => {
      return {
        ...path,
        children: childItems(path.id, basePaths),
      };
    });
  return childPaths;
};

export const getAdminSettings = (paths: ITopNavMenuItem[]) => {
  let addminSettingsData = paths.filter((path) => {
    if (
      path.internalName.toLowerCase().trim() ===
      RoleMenuInternalNameMaster.AdminSettings.toLowerCase().trim()
    ) {
      const childPaths = childItems(path.id, paths);
      path.children = childPaths;
      return path;
    }
  });
  return addminSettingsData;
};

export const getEmployeeTaskCountByApiService = async (
  payload: any
): Promise<number> => {
  try {
    const url = createQueryUrl(
      baseurl + "Workflow/v1/GetEmployeeTaskCountByQuery",
      payload
    );
    return (await axios.get(url)).data;
  } catch (error) {
    throw error;
  }
};

export const IsContextItemExist = (contextMenuItems: IContextMenuItem[] ,itemInternalName: string): boolean => {
  const itemIndex = contextMenuItems.findIndex(
    (el: IContextMenuItem) => el.internalName === itemInternalName
  );

  return itemIndex > -1 ? true : false;
};