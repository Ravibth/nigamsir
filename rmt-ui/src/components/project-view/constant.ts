import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";
import { RolesListMaster } from "../../common/enums/ERoles";
export const ViewMoreDetialSxProps: SxProps = {
  textDecoration: "none",
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export const CloseButtonSxProps: SxProps = {
  position: "absolute",
  right: "10px",
  top: "-4px",
};

export const CloseIconSxProps: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
};

export interface ProjectViewDetails {
  startDate: Date | null;
  endDate: Date | null;
  clientGroup: string | null;
  clientName: string | null;
  jobId: string | null;
  pipelineCode: string | null;
  bu: string | null;
  buId: string | null;
  offerings: string | null;
  solutions: string | null;
  offeringsId: string | null;
  solutionsId: string | null;
  expertise: string | null; //Recheck
  sme: string | null; //Recheck
  smeg: string | null; //Recheck
  revenueUnit: string | null; //Recheck
  csp: string | null; //unable_to_fetch
  el: string | null; //unable_to_fetch
  smegLeader: string | null; //unable_to_fetch
  jobManager: string | null; //unable_to_fetch
  eo: string | null; //unable_to_fetch
  proposedCsp: string | null; //unable_to
  projectRoles: any[] | null;
  projectRolesView: any[] | null;
  budgetStatus: string | null;
  projectRequisitionAllocations: any;
}

export enum ProjectViewDetailsTitle {
  START_DATE = "Start Date",
  END_DATE = "End Date",
  CLIENT_GROUP = "Client Group",
  CLIENT_NAME = "Client Name",
  JOB_ID = "Job ID",
  BUSINESS_UNITS = "Business Unit",
  OFFERING = "Offering",
  SOLUTION = "Solution",
  EXPERTISE = "Expertise",
  SME = "SME",
  SMEG = "SMEG",
  REVENUE_UNT = "Revenue Unit",
  CSP = "CSP",
  EL = "EL",
  SMEG_LEADER = "SMEG Leader",
  JOB_PARTNER = "Job Partner",
  JOB_MANAGER = "Job Manager",
  EO = "EO",
  PROPOSED_CSP = "Proposed CSP",
  PIPELINE_CODE = "Pipeline Code",
  BUDGET = "Budget",
  ALLOCATION_STATUS = "Allocation Status",
}

export const GetPropertyValueByTitle = (
  title: string,
  ProjectViewDetails: ProjectViewDetails
) => {
  switch (title) {
    case ProjectViewDetailsTitle.START_DATE:
      return ProjectViewDetails?.startDate ?? null;

    case ProjectViewDetailsTitle.END_DATE:
      return ProjectViewDetails?.endDate ?? null;

    case ProjectViewDetailsTitle.CLIENT_GROUP:
      return ProjectViewDetails?.clientGroup ?? null;

    case ProjectViewDetailsTitle.CLIENT_NAME:
      return ProjectViewDetails?.clientName ?? null;

    case ProjectViewDetailsTitle.JOB_ID:
      return ProjectViewDetails?.jobId ?? null;

    case ProjectViewDetailsTitle.PIPELINE_CODE:
      return ProjectViewDetails?.jobId
        ? null
        : ProjectViewDetails?.pipelineCode ?? null;

    case ProjectViewDetailsTitle.BUSINESS_UNITS:
      return ProjectViewDetails?.bu ?? null;

    case ProjectViewDetailsTitle.OFFERING:
      return ProjectViewDetails?.offerings ?? null;

    case ProjectViewDetailsTitle.SOLUTION:
      return ProjectViewDetails?.solutions ?? null;

    case ProjectViewDetailsTitle.EXPERTISE:
      return ProjectViewDetails?.expertise ?? null;

    case ProjectViewDetailsTitle?.SME: //Recheck
      return ProjectViewDetails?.sme ?? null;

    case ProjectViewDetailsTitle?.SMEG: //Recheck
      return ProjectViewDetails?.smeg ?? null;

    case ProjectViewDetailsTitle.REVENUE_UNT: //Recheck
      return ProjectViewDetails?.revenueUnit ?? null;

    case ProjectViewDetailsTitle.CSP:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.CSP.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.EL:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.EngagementLeader.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.SMEG_LEADER:
    case ProjectViewDetailsTitle.JOB_PARTNER:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.SMEGLeader.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.JOB_MANAGER:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.JobManager.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.EO:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.EO.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.PROPOSED_CSP:
      return (
        //projectRolesView change
        ProjectViewDetails?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.ProposedCSP.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() ?? null
      );

    case ProjectViewDetailsTitle.BUDGET:
      return ProjectViewDetails?.budgetStatus ?? null;

    case ProjectViewDetailsTitle.ALLOCATION_STATUS:
      return ProjectViewDetails?.projectRequisitionAllocations?.status ?? null;

    default:
      break;
  }
};
