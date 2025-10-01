import { RolesListMaster } from "../../common/enums/ERoles";

export const ResourceRequestorsList = [
  RolesListMaster.EngagementLeader.toString(),
  RolesListMaster.EO.toString(),
  RolesListMaster.JobManager.toString(),
  RolesListMaster.ProposedEL.toString(),
  RolesListMaster.ResourceRequestor.toString(),
  RolesListMaster.Resource_Requestor.toString(),
];
export const hasPermissionForChange = (userRoles: string[]) => {
  const isResourceRequestor = userRoles?.some((role) =>
    ResourceRequestorsList.includes(role)
  );
  if (isResourceRequestor) {
    return true;
  }
  return false;
};
