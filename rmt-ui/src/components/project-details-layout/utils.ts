import * as service from "../../services/project-employee-list";
import * as master_service from "../../services/project-list-services/get-master-services";
import * as gc from "../../global/constant";
import moment from "moment";
import { isNull } from "lodash";
import { debug } from "console";
import * as config_service from "../../services/configuration-services/configuration.service";
import { RolesListMaster } from "../../common/enums/ERoles";
import _ from "lodash";
export const getProjectDetails = async (pipelineCode: string, jobCode) => {
  const projectDetails = await service.getProjectDetailsByCode(
    pipelineCode,
    jobCode
  );
  return projectDetails;
};

export const getProjectDetailHeadlineEmployee = (projectDetails: any) => {
  const data: any[] = [
    {
      Key: 1,
      title: "Start date",
      value: moment(projectDetails?.startDate).format(gc.dateFormate),
    },
    { Key: 17, title: "BU", value: "Audit" },
    { Key: 7, title: "SME", value: projectDetails?.sme },
    { Key: 16, title: "Sector", value: "Auto Components" },
    { Key: 12, title: "Market", value: "Cross Entity Billing Markets" },
    { Key: 8, title: "Location", value: projectDetails?.location },
    { Key: 5, title: "Client", value: projectDetails?.clientName },
    { Key: 3, title: "Project Category", value: projectDetails?.chargableType },
    { Key: 4, title: "Pipeline Code", value: projectDetails?.pipelineCode },

    {
      Key: 2,
      title: "End date",
      value: moment(projectDetails?.endDate).format(gc.dateFormate),
    },
    { Key: 6, title: "Expertise", value: projectDetails?.expertise },
    { Key: 9, title: "Revenue Unit", value: projectDetails?.revenueUnit },
    {
      Key: 15,
      title: "Sub Industry",
      value: "Automobile parts  equipment manufacturers",
    },
    { Key: 13, title: "Submarket", value: "Billing" },
    { Key: 14, title: "GT Reference Country", value: "Country" },
    { Key: 11, title: "Legal Entity", value: "Grant thorton bharat LLP" },
    {
      Key: 10,
      title: "Recurring/Non-recurring",
      value: projectDetails?.projectType,
    },
    {
      Key: 10,
      title: "Budget Status",
      value: projectDetails.budgetStatus,
    },
  ];
  return data;
};

export const getProjectSkills = (projectDetails: any) => {
  const skills: any = [];
  projectDetails &&
    projectDetails.projectSkills?.map((item: any) => {
      skills.push({
        id: item.id,
        label: item.skillName,
        isactive: true,
      });
    });
  return skills;
};
export const getProjectDesignations = (projectDetails: any) => {
  const designations: any = [];
  projectDetails?.projectDemands &&
    projectDetails?.projectDemands?.map((item: any) => {
      designations.push({
        id: item.id,
        label: item.designation,
        noOfResources: item.noOfResources,
        projectDemandSkills: item.projectDemandSkills,
        isactive: true,
      });
    });
  return designations;
};

export const getAllDesignations = (masterList: any) => {
  const designations: any = [];
  masterList
    ?.filter(
      (ELItem: any) => ELItem.recordType == gc.ROLE_TYPE.designation.toString()
    )
    .map((item: any) => {
      designations.push({
        id: item.id,
        label: item.recordDisplayName,
        role: gc.ROLE_TYPE.designation.toString(),
        internalName: item.recordInternalName,
        isactive: true,
      });
    });
  return designations;
};

export const getEngagementLeaders = (masterList: any) => {
  const EngamentLeadersData: any = [];
  masterList
    ?.filter(
      (ELItem: any) =>
        ELItem.recordType == gc.ROLE_TYPE.engagementLeader.toString()
    )
    .map((item: any) => {
      EngamentLeadersData.push({
        id: item.id,
        label: item.recordDisplayName,
        role: gc.ROLE_TYPE.engagementLeader.toString(),
        internalName: item.recordInternalName,
        isactive: true,
        modifiedBy: item.modifiedBy,
      });
    });
  return EngamentLeadersData;
};

export const GetDelegateLeaders = (masterList: any) => {
  const DelegateLeadersData: any = [];
  masterList
    ?.filter(
      (ELItem: any) => ELItem.recordType == gc.ROLE_TYPE.delegateUser.toString()
    )
    .map((item: any) => {
      DelegateLeadersData.push({
        id: item.id,
        label: item.recordDisplayName,
        role: gc.ROLE_TYPE.delegateUser.toString(),
        internalName: item.recordInternalName,
        isactive: true,
      });
    });
  return DelegateLeadersData;
};

export const getDesignation = async () => {
  const delegateLeadersData: any = [];
  return config_service.getDesignationList().then((response: any) => {
    if (response && response.data) {
      response.data.map((item: any) => {
        delegateLeadersData.push({
          label: item.designation_name,
          id: item.id,
          labelId: item.designation_id,
          internalName: item.designation_name,
          isactive: true,
        });
      });
    }
    return delegateLeadersData;
  });
};

export const getAllSkills = (masterList: any) => {
  const DelegateLeadersData: any = [];
  masterList
    ?.filter(
      (ELItem: any) => ELItem.recordType == gc.ROLE_TYPE.skill.toString()
    )
    .map((item: any) => {
      DelegateLeadersData.push({
        id: item.id,
        label: item.recordDisplayName,
        role: item.role,
        internalName: item.recordInternalName,
        isactive: true,
      });
    });
  return DelegateLeadersData;
};

export const getProjectResourceRequestors = (projectDetails: any) => {
  const userData: any[] = [];
  projectDetails.resourceRequestorNames &&
    projectDetails.resourceRequestorNames.map((item: any) => {
      userData.push({
        id: "",
        label: item,
        user: item,
        isactive: true,
      });
    });

  let uniqueData = _.uniqBy(userData, "label");
  // let uniqueData = [...new Set(userData.map((element: { label: any; }) => element.label))];
  return uniqueData;
};

export const getProjectEngagementorDLLeaders = (
  projectDetails: any,
  roleType: gc.ROLE_TYPE
) => {
  const userData: any = [];
  //projectRolesView change
  projectDetails.projectRolesView &&
    projectDetails.projectRolesView
      .filter(
        (ELItem) =>
          ELItem.role.toLowerCase() == roleType.toString().toLowerCase()
      )
      .map((item: any) => {
        userData.push({
          id: item.id,
          label: item.userName,
          user: item.user,
          role: item.role,
          isactive: true,
          modifiedBy: item.modifiedBy,
        });
      });
  return userData;
};

export const getProjectEnquireOwnerLeaders = (projectDetails: any) => {
  const userData: any = [];
  //projectRolesView change
  projectDetails.projectRolesView &&
    projectDetails.projectRolesView
      .filter(
        (ELItem: any) =>
          ELItem.role.toString() == gc.ROLE_TYPE.enguiryOwner.toString()
      )
      .map((item: any) => {
        userData.push({
          id: item.id,
          label: item.userName,
          user: item.user,
          role: item.role,
          isactive: item.isactive,
          modifiedBy: item.modifiedBy,
        });
      });
  return userData;
};

export const ResourceRequestorsList = [
  RolesListMaster.EngagementLeader.toString(),
  RolesListMaster.EO.toString(),
  RolesListMaster.JobManager.toString(),
  RolesListMaster.ProposedEL.toString(),
  RolesListMaster.ResourceRequestor.toString(),
  RolesListMaster.Resource_Requestor.toString(),
];
export const DelegateList = [RolesListMaster.Delegate.toString()];

export const AdditionalElList = [RolesListMaster.AdditionalEl.toString()];

export const AdditionalDelegateList = [
  RolesListMaster.AdditionalDelegate.toString(),
];

export const hasPermissionForAddAndDeleteButton = (
  userRoles: string[]
): boolean => {
  const isResourceRequestor = userRoles?.some((role) =>
    ResourceRequestorsList.includes(role)
  );
  const isDelegate = userRoles?.some((role) => DelegateList.includes(role));
  if (isResourceRequestor || isDelegate) {
    return true;
  }
  return false;
};

export const hasPermissionForAdditionalDelegateChange = (
  userRoles: string[],
  userEmail: string,
  rowInfo: any
) => {
  // const isAdditionalDelegate = userRoles?.some((role) =>
  //   AdditionalElList.includes(role)
  // );
  // if (isAdditionalDelegate) {
  //   return true;
  // }
  const isAdditionalEl = userRoles?.some((role) =>
    AdditionalElList.includes(role)
  );
  if (
    isAdditionalEl &&
    rowInfo.data.additionalElEmail.toLowerCase().trim() ===
      userEmail.toLowerCase().trim()
  ) {
    return true;
  }
  return false;
};
export const hasPermissionForAdditionalElChange = (
  userRoles: string[],
  userEmail: string,
  rowInfo: any
) => {
  const isResourceRequestor = userRoles?.some((role) =>
    ResourceRequestorsList.includes(role)
  );
  const isDelegate = userRoles?.some((role) => DelegateList.includes(role));
  if (isResourceRequestor || isDelegate) {
    return true;
  }
  const isAdditionalEl = userRoles?.some((role) =>
    AdditionalElList.includes(role)
  );
  // if (
  //   isAdditionalEl &&
  //   rowInfo.data.additionalElEmail.toLowerCase().trim() ===
  //     userEmail.toLowerCase().trim()
  // ) {
  //   return true;
  // }
  return false;
};
