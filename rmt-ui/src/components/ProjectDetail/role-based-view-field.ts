import { ProjectCategories, Role } from "../../global/constant";
import { jsonValuesToArray } from "../../global/utils";
import { ProjectDetailsHeadline } from "./project-detail-headline/Constant";

export function getProjectViewFieldsJson(role: string, projectType: string) {
  let dynamicJSON = {};
  dynamicJSON = generateSubJSON(role, projectType);
  return jsonValuesToArray(dynamicJSON);
}

function generateSubJSON(role, chargeType) {
  //const RR_DL_View_Role = Request_View_Role.concat(Delegate_View_Role);
  const staticFields = [
    ProjectDetailsHeadline.PIPELINE_CODE,
    ProjectDetailsHeadline.BUSINESS_UNITS,
    ProjectDetailsHeadline.OFFERING,
    ProjectDetailsHeadline.SOLUTION,
    ProjectDetailsHeadline.CLIENT_GROUP,
    ProjectDetailsHeadline.CLIENT_NAME,
    ProjectDetailsHeadline.START_DATE,
    ProjectDetailsHeadline.END_DATE,
  ];
  let subJSON = {};
  staticFields.forEach((field) => {
    subJSON[field] =
      field === ProjectDetailsHeadline.PIPELINE_CODE
        ? ProjectDetailsHeadline.JOB_ID
        : field;
  });

  if (chargeType === ProjectCategories.Pipeline) {
    subJSON[ProjectDetailsHeadline.PIPELINE_CODE] =
      ProjectDetailsHeadline.PIPELINE_CODE;
  }
  if (
    chargeType === ProjectCategories.Pipeline //&&
    // RR_DL_View_Role.indexOf(role) > -1
  ) {
    // subJSON["Budget"] = "Budget";
    subJSON[ProjectDetailsHeadline.PROPOSED_CSP] =
      ProjectDetailsHeadline.PROPOSED_CSP;
    subJSON[ProjectDetailsHeadline.EO] = ProjectDetailsHeadline.EO;
  } else if (
    chargeType === ProjectCategories.Chargeable //&&
    // RR_DL_View_Role.indexOf(role) > -1
  ) {
    subJSON[ProjectDetailsHeadline.CSP] = ProjectDetailsHeadline.CSP;
    subJSON[ProjectDetailsHeadline.EL] = ProjectDetailsHeadline.EL;
  } else if (
    chargeType === ProjectCategories.NonChargeable //&&
    // RR_DL_View_Role.indexOf(role) > -1
  ) {
    subJSON[ProjectDetailsHeadline.SMEG_LEADER] =
      ProjectDetailsHeadline.JOB_PARTNER;
    subJSON[ProjectDetailsHeadline.JOB_MANAGER] =
      ProjectDetailsHeadline.JOB_MANAGER;
  }

  if (Request_View_Role.indexOf(role) > -1) {
    subJSON[ProjectDetailsHeadline.BUDGET] = ProjectDetailsHeadline.BUDGET;
  }
  return subJSON;
}

export const Request_View_Role = [
  Role.ResourceRequestor,
  Role.Leaders,
  Role.Reviewer,
  Role.EngagementLeader,
  Role.CSP,
  Role.AdditionalDelegate,
];

export const Delegate_View_Role = [Role.Delegate, Role.Admin, Role.CEOCOO];

export const Employee_View_Role = [Role.Employee];
