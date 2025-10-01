import { PERMISSION_TYPE } from "../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../common/module-permission/module-permission";
import { IUserDetailsContext } from "../../contexts/userDetailsContext";
import { IsPermissionExistForProject } from "../../global/utils";
import { TabLabelHideSXProps, TabLabelSXProps } from "./constant";

export const isShowHideProjectDetailsTab = (
  userContext: IUserDetailsContext,
  modalName: MODULE_NAME_ENUM,
  permission_type: PERMISSION_TYPE = PERMISSION_TYPE.Read,
  projectInfo?: any
) => {
  let flag: boolean = false;
  switch (true) {
    case modalName === MODULE_NAME_ENUM.Project_Budget &&
      IsPermissionExistForProject(
        userContext.projectPermissionData?.permissions,
        modalName,
        permission_type,
        userContext.role,
        userContext?.buTreeMappingListByMID,
        projectInfo,
        userContext.projectPermissionData?.projectRoles
      ):
      flag = true;
      break;

    case IsPermissionExistForProject(
      userContext.projectPermissionData?.permissions,
      modalName,
      permission_type,
      userContext.role,
      userContext?.buTreeMappingListByMID,
      projectInfo
    ):
      flag = true;
      break;

    default:
      break;
  }
  return getShowHideTabClass(flag);
};

const getShowHideTabClass = (flag: boolean) => {
  return flag ? TabLabelSXProps : TabLabelHideSXProps;
};
