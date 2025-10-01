import { SxProps } from "@mui/material";
import { HeadlineInterface } from "./IHeadline";
import { RolesListMaster } from "../../../common/enums/ERoles";
import { IProjectRequisitionAllocation } from "../../../common/interfaces/IProject";
import { ProjectChargeableType } from "../../project-types/constant";
import { ProjectCategories, Role } from "../../../global/constant";

export const HeadlinesData: HeadlineInterface[] = [
  // {
  //   Key: 1,
  //   title: "Start date",
  //   value: new Date().toLocaleString("en-US", {
  //     day: "numeric",
  //     month: "short",
  //     year: "numeric",
  //   }),
  // },
  // {
  //   Key: 2,
  //   title: "End date",
  //   value: new Date().toLocaleString("en-US", {
  //     day: "numeric",
  //     month: "short",
  //     year: "numeric",
  //   }),
  // },
  // { Key: 3, title: "Project Category", value: "Chargable" },
  // { Key: 4, title: "Job/Pipeline Code", value: "048665" },
  // { Key: 5, title: "Client", value: "Adani Enterprises Ltd" },
  // { Key: 6, title: "Expertise", value: "Audit" },
  // { Key: 7, title: "SME", value: "Audit Support" },
  // { Key: 8, title: "Location", value: "New Delhi" },
  // { Key: 9, title: "Revenue Unit", value: "organic North" },
  // { Key: 10, title: "Project Type", value: "Recurring" },
];

export const getProjectHeadlineSxProps = () => {
  const sxProps: SxProps = {
    margin: "1px 0px",
    padding: "1px 10px",
    borderRadius: "4px",
    background: "#f2f5ff", //"#f6f2fc" ,
  };
  return sxProps;
};

export const ProjectHeaders: SxProps = {
  color: "#4f2d7f",
  fontWeight: "bold",
  textTransform: "none",
  fontSize: "17px",
  background: "#f6f2fc",
};

export interface ProjectHeadlinesView {
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
  budget: string | null; //unable_to_fetch
  csp: string | null; //unable_to_fetch
  el: string | null; //unable_to_fetch
  smegLeader: string | null; //unable_to_fetch
  jobManager: string | null; //unable_to_fetch
  eo: string | null; //unable_to_fetch
  proposedCsp: string | null; //unable_to    lead_generator   ,
  projectType: string | null;
  chargableType: string | null;
  industry: string | null;
  subindustry: string | null;
  gtRefferenceCountry: string | null;
  jobLocation: string | null;
  legalEntity: string | null;
  isBudgetRequired: boolean | null;
  viewType: string | null;
  deliveryLocation: string | null;
  projectRoles: any[] | null;
  projectRolesView: any[] | null;
  pipelineStatus: string | null;
  projectRequisitionAllocations: IProjectRequisitionAllocation | null;
  projectActivationAndClosureState: string | null;
  budgetStatus: string | null;
  isConfidential?: boolean | false;
}
export enum ProjectDetailsHeadline {
  START_DATE = "Start Date",
  END_DATE = "End Date",
  CLIENT_GROUP = "Client Group",
  CLIENT_NAME = "Client Name",
  JOB_ID = "Job ID",
  BUSINESS_UNITS = "Business Unit",
  OFFERINGS = "Offering",
  SOLUTIONS = "Solution",
  // EXPERTISE = "Expertise", //Recheck
  // SME = "SME", //Recheck
  // SMEG = "SMEG", //Recheck
  BUDGET = "Budget",
  // REVENUE_UNT = "Revenue Unit", //Recheck
  PROJECT_TYPE = "Project Type",
  PROJECT_CATEGORY = "Project Category",
  CSP = "CSP",
  EL = "EL",
  LEAD_GENERATOR = "Lead Generator",
  ASSIGNMENT_INCHARGE = "Assignment Incharge",
  CSL = "CSL",
  SMEG_LEADER = "SMEG Leader",
  JOB_PARTNER = "Job Partner",
  JOB_MANAGER = "Job Manager",
  EO = "EO",
  PROPOSED_CSP = "Proposed CSP",
  PROPOSED_EL = "Proposed EL",
  PIPELINE_CODE = "Pipeline Code",
  LEGAL_ENTITY = "Legal Entity",
  GT_REFFERENCE_COUNTRY = "GT Refference Country",
  JOB_LOCATION = "Job Location",
  DELIVERY_LOCATION = "Delivery Location",
  FINDING_PARTNER = "Finding Partner",
  INDUSTRY = "Industry",
  ALLOCATION_STATUS = "Allocation Status",
  PROJECT_STATUS = "Status",
  SUB_INDUSTRY = "Sub Industry",
  PROJECT_ACTIVATION_AND_CLOSURE_STATUS = "Project Activation Status",
  PIPELINE_ID = "Pipeline ID",
  FINDING_PARTNER_1 = "Finding Partner 1",
  FINDING_PARTNER_2 = "Finding Partner 2",
  OFFERING = "Offering",
  SOLUTION = "Solution",
  EMPTY = "",
  CONFIDENTIAL= "Confidential",
  
}

export const GetPropertyValueByTitle = (
  title: string,
  ProjectHeadlinesView: ProjectHeadlinesView
) => {
  switch (title) {
    case ProjectDetailsHeadline.START_DATE:
      return ProjectHeadlinesView?.startDate; //?? null;
      break;
    case ProjectDetailsHeadline.END_DATE:
      return ProjectHeadlinesView?.endDate; //?? null;
      break;
    case ProjectDetailsHeadline.CLIENT_GROUP:
      return ProjectHeadlinesView?.clientGroup; //?? null;
      break;
    case ProjectDetailsHeadline.CLIENT_NAME:
      return ProjectHeadlinesView?.clientName; //?? null;
      break;
    case ProjectDetailsHeadline.JOB_ID:
      return ProjectHeadlinesView?.jobId; //?? null;
      break;
    case ProjectDetailsHeadline.PIPELINE_CODE:
      return ProjectHeadlinesView?.pipelineCode;
      break;
    case ProjectDetailsHeadline.PIPELINE_ID:
      return ProjectHeadlinesView?.pipelineCode;
      break;
    case ProjectDetailsHeadline.BUSINESS_UNITS:
      return ProjectHeadlinesView?.bu; //?? null;
      break;
    case ProjectDetailsHeadline.OFFERING:
      return ProjectHeadlinesView?.offerings ?? null;
      break;
    case ProjectDetailsHeadline.SOLUTION:
      return ProjectHeadlinesView?.solutions ?? null;
      break;
    case ProjectDetailsHeadline.SOLUTIONS:
      return ProjectHeadlinesView?.solutions; //?? null;
      break;
    case ProjectDetailsHeadline.PROJECT_CATEGORY:
      return ProjectHeadlinesView?.chargableType; //?? null;
      break;
    case ProjectDetailsHeadline.PROJECT_TYPE:
      return ProjectHeadlinesView?.projectType; //?? null;
      break;
    case ProjectDetailsHeadline.BUDGET:
      const budgetStatus = ProjectHeadlinesView?.budgetStatus;
      if (budgetStatus === ProjectCategories.InBudget) {
        return ProjectCategories.InBudget;
      } else if (budgetStatus === ProjectCategories.OverBudget) {
        return ProjectCategories.OverBudget;
      } else {
        return budgetStatus;
      }
      break;
    case ProjectDetailsHeadline.PROJECT_ACTIVATION_AND_CLOSURE_STATUS:
      return ProjectHeadlinesView?.projectActivationAndClosureState; //?? null;
      break;
    case ProjectDetailsHeadline.CSP:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.CSP.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    // eslint-disable-next-line no-duplicate-case
    case ProjectDetailsHeadline.EL:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.EngagementLeader.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.LEAD_GENERATOR:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.LeadGenerator.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.ASSIGNMENT_INCHARGE:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.AssignmentIncharge.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.CSL:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.CSL.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.SMEG_LEADER:
    case ProjectDetailsHeadline.JOB_PARTNER:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.SMEGLeader.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.FINDING_PARTNER:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.FindingPartner.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.JOB_MANAGER:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.JobManager.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.PROPOSED_EL:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.ProposedEL.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.EO:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.EO.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.PROPOSED_CSP:
      return (
        //projectRolesView change
        ProjectHeadlinesView?.projectRolesView
          ?.filter(
            (d) =>
              d.role.toLowerCase().trim() ===
              RolesListMaster.ProposedCSP.toLowerCase().trim()
          )
          ?.map((x) => x.userName)
          .join() //?? null
      );
      break;
    case ProjectDetailsHeadline.LEGAL_ENTITY:
      return ProjectHeadlinesView?.legalEntity; //?? null;
      break;
    case ProjectDetailsHeadline.JOB_LOCATION:
      return ProjectHeadlinesView?.jobLocation; //?? null;
      break;
    case ProjectDetailsHeadline.DELIVERY_LOCATION:
      return ProjectHeadlinesView?.deliveryLocation; //?? null;
      break;
    case ProjectDetailsHeadline.GT_REFFERENCE_COUNTRY:
      return ProjectHeadlinesView?.gtRefferenceCountry; //?? null;
      break;
    case ProjectDetailsHeadline.INDUSTRY:
      return ProjectHeadlinesView?.industry; //?? null;
      break;
    case ProjectDetailsHeadline.SUB_INDUSTRY:
      return ProjectHeadlinesView?.subindustry; // ?? null;
      break;
    case ProjectDetailsHeadline.PROJECT_STATUS:
      return ProjectHeadlinesView?.pipelineStatus; //?? null;
      break;
    case ProjectDetailsHeadline.ALLOCATION_STATUS:
      return ProjectHeadlinesView?.projectRequisitionAllocations?.status; //?? null
    case ProjectDetailsHeadline.CONFIDENTIAL:
      return ProjectHeadlinesView?.isConfidential; //?? null
    default:
      break;
  }
};
