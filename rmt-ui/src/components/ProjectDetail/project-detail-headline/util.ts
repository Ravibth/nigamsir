import {
  ResourceRequestorsList,
  ReviewersList,
  RolesListMaster,
} from "../../../common/enums/ERoles";
import { IBUTreeMappingListByMID } from "../../../common/interfaces/IBUTreeMappingListByMID";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { isUserLeaderForCurrentProject } from "../../../global/utils";
import {
  PROJECT_CHARGE_TYPE,
  ProjectChargeableType,
} from "../../project-types/constant";
import { ProjectDetailsHeadline } from "./Constant";

export const GetProjectHeadlineTitle = (
  currentUserApplicationRole: string[],
  currentUserProjectRoles: string[],
  projectType: string,
  pipelineCode: string | null,
  jobCode: string | null,
  userBuTreeMapping: IBUTreeMappingListByMID,
  projectData: IProjectMaster
) => {
  let basicTitles = [
    {
      Name: ProjectDetailsHeadline.BUSINESS_UNITS,
      Order: 11,
    }, //1.
    { Name: ProjectDetailsHeadline.OFFERING, Order: 21 }, //2.
    { Name: ProjectDetailsHeadline.SOLUTION, Order: 31 }, //3.
    { Name: ProjectDetailsHeadline.CLIENT_GROUP, Order: 32 }, //6.
    { Name: ProjectDetailsHeadline.CLIENT_NAME, Order: 42 }, //7.
    { Name: ProjectDetailsHeadline.START_DATE, Order: 13 }, //9.
    { Name: ProjectDetailsHeadline.END_DATE, Order: 23 }, //10.
    { Name: ProjectDetailsHeadline.PROJECT_CATEGORY, Order: 51 }, //C , NC 12.
    { Name: ProjectDetailsHeadline.PROJECT_TYPE, Order: 41 }, //R ,NR 13.
    { Name: ProjectDetailsHeadline.PROJECT_STATUS, Order: 33 },
    { Name: ProjectDetailsHeadline.INDUSTRY, Order: 12 }, //8.
    { Name: ProjectDetailsHeadline.SUB_INDUSTRY, Order: 22 },
  ];
  if (
    jobCode &&
    jobCode !== "undefined" &&
    jobCode.length > 0 &&
    projectType.toLowerCase().trim() ===
      PROJECT_CHARGE_TYPE.CHARGABLE.toLowerCase().trim()
  ) {
    //For Chargeable Jobs
    basicTitles = basicTitles.concat([
      { Name: ProjectDetailsHeadline.LEGAL_ENTITY, Order: 71 }, //11.
      { Name: ProjectDetailsHeadline.CSP, Order: 52 }, //14
      { Name: ProjectDetailsHeadline.EL, Order: 62 }, //16
      { Name: ProjectDetailsHeadline.JOB_LOCATION, Order: 63 }, //17
      { Name: ProjectDetailsHeadline.DELIVERY_LOCATION, Order: 73 }, //
      { Name: ProjectDetailsHeadline.PIPELINE_ID, Order: 61 },
      { Name: ProjectDetailsHeadline.CONFIDENTIAL, Order: 74 }, //

    ]);
    if (
      currentUserProjectRoles.some((role) =>
        ResourceRequestorsList.map((e) => e?.toLowerCase()?.trim())?.includes(
          role?.toLowerCase()?.trim()
        )
      )
    ) {
      //RR
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        ReviewersList.map((e) => e?.toLowerCase()?.trim())?.includes(
          role?.toLowerCase().trim()
        )
      ) ||
      currentUserApplicationRole.some((role) =>
        [
          RolesListMaster.Admin,
          RolesListMaster.SystemAdmin,
          RolesListMaster.CEOCOO,
        ]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      )
    ) {
      //Reviewers
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        [
          RolesListMaster.Delegate,
          RolesListMaster.AdditionalEl,
          RolesListMaster.AdditionalDelegate,
        ]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      )
    ) {
      //AddEl , AddDele , Delegate
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.EMPTY, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserApplicationRole.some((role) =>
        [RolesListMaster.Leaders]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      ) &&
      isUserLeaderForCurrentProject(userBuTreeMapping, projectData)
    ) {
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else {
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    }
  } else if (
    jobCode !== "undefined" &&
    jobCode &&
    jobCode.length > 0 &&
    projectType.toLowerCase().trim() ===
      PROJECT_CHARGE_TYPE.NON_CHARGABLE.toLowerCase().trim()
  ) {
    //For Non Chargable Jobs
    basicTitles = basicTitles.concat([
      { Name: ProjectDetailsHeadline.LEGAL_ENTITY, Order: 61 }, //11.
      // { Name: ProjectDetailsHeadline.SUB_INDUSTRY, Order: 5 },
      { Name: ProjectDetailsHeadline.JOB_PARTNER, Order: 52 },
      { Name: ProjectDetailsHeadline.ASSIGNMENT_INCHARGE, Order: 81 },
      { Name: ProjectDetailsHeadline.JOB_MANAGER, Order: 62 },
      { Name: ProjectDetailsHeadline.JOB_LOCATION, Order: 63 },
      // ProjectDetailsHeadline.PROJECT_ACTIVATION_AND_CLOSURE_STATUS,
    ]);
    if (
      currentUserProjectRoles.some((role) =>
        ResourceRequestorsList.map((e) => e.toLowerCase().trim()).includes(
          role.toLowerCase().trim()
        )
      )
    ) {
      //RR
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        ReviewersList.map((e) => e.toLowerCase().trim()).includes(
          role.toLowerCase().trim()
        )
      )
    ) {
      //Reviewers
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        [
          RolesListMaster.Delegate,
          RolesListMaster.AdditionalEl,
          RolesListMaster.AdditionalDelegate,
        ]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      )
    ) {
      //AddEl , AddDele , Delegate
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.EMPTY, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserApplicationRole.some((role) =>
        [RolesListMaster.Leaders]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      ) &&
      isUserLeaderForCurrentProject(userBuTreeMapping, projectData)
    ) {
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else {
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    }
  } else {
    //For Pipelines
    basicTitles = basicTitles.concat([
      { Name: ProjectDetailsHeadline.DELIVERY_LOCATION, Order: 73 }, //
      { Name: ProjectDetailsHeadline.EO, Order: 81 }, //
      { Name: ProjectDetailsHeadline.PROPOSED_CSP, Order: 52 }, //}
      { Name: ProjectDetailsHeadline.PROPOSED_EL, Order: 62 }, //}
      // ProjectDetailsHeadline.PROJECT_STATUS,
      // { Name: ProjectDetailsHeadline.FINDING_PARTNER_1, Order: 26 }, //}
      // { Name: ProjectDetailsHeadline.FINDING_PARTNER_2, Order: 27 }, //}
    ]);
    if (
      currentUserProjectRoles.some((role) =>
        ResourceRequestorsList.map((e) => e.toLowerCase().trim()).includes(
          role.toLowerCase().trim()
        )
      )
    ) {
      //RR
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        ReviewersList.map((e) => e.toLowerCase().trim()).includes(
          role.toLowerCase().trim()
        )
      )
    ) {
      //Reviewers
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.BUDGET, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else if (
      currentUserProjectRoles.some((role) =>
        [
          RolesListMaster.Delegate,
          RolesListMaster.AdditionalEl,
          RolesListMaster.AdditionalDelegate,
        ]
          .map((e) => e.toLowerCase().trim())
          .includes(role.toLowerCase().trim())
      )
    ) {
      //AddEl , AddDele , Delegate
      basicTitles = basicTitles.concat([
        { Name: ProjectDetailsHeadline.EMPTY, Order: 53 },
        { Name: ProjectDetailsHeadline.ALLOCATION_STATUS, Order: 43 },
      ]);
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    } else {
      basicTitles.sort((a, b) => a.Order - b.Order);
      return basicTitles.map((title) => title.Name);
    }
  }
};
