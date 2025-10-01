import { RolesListMaster } from "../../common/enums/ERoles";
import { IRoleOptions } from "./interface";

export const GetUserRoleCompeteInformation = (roles: string[]) => {
  const result: IRoleOptions = [];
  roles.forEach((role) => {
    let roleName = role;
    let roleDisplayName = "";
    switch (role.toLowerCase().trim()) {
      case RolesListMaster.Admin.toLowerCase().trim():
        roleDisplayName = "Admin";
        break;
      case RolesListMaster.SystemAdmin.toLowerCase().trim():
        roleDisplayName = "System Admin";
        break;
      case RolesListMaster.AdditionalEl.toLowerCase().trim():
        roleDisplayName = "Additional EL";
        break;
      case RolesListMaster.AdditionalDelegate.toLowerCase().trim():
        roleDisplayName = "Additional Delegate";
        break;
      case RolesListMaster.ResourceRequestor.toLowerCase().trim():
        roleDisplayName = "Resource Requestor";
        break;
      case RolesListMaster.Reviewer.toLowerCase().trim():
        roleDisplayName = "Reviewer";
        break;
      case RolesListMaster.Employee.toLowerCase().trim():
        roleDisplayName = "Employee";
        break;
      case RolesListMaster.CEOCOO.toLowerCase().trim():
        roleDisplayName = "CEO-COO";
        break;
      default:
        roleDisplayName = role;
        break;
    }
    result.push({
      roleName: roleName,
      roleDisplayName: roleDisplayName,
    });
  });

  return result;
};
