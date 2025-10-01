import { ButtonPropsVariantOverrides, SxProps } from "@mui/material";
import { WorkflowStatus } from "./workflow/workflow-constant";

export const CALENDAR_VIEW_TYPE = {
  TimeLineDay: "TIMELINEDAY",
  TimeLineMonth: "TIMELINEMONTH",
  TimeLineQuater: "TIMELINEQUATER",
  TimeLineHalfYear: "TIMELINEHALFYEAR",
  TimeLineToday: "TimeLineToday",
  TimeLineYear: "TIMELINEYEAR",
};

export const VARIANT_TYPE: ButtonPropsVariantOverrides = {
  outlined: "outlined",
};
export const GT_DESIGN_PARAMETERS = {
  // GtPrimaryColor: "#4f2d7f",
  GtPrimaryColor: "#f26416",
  GtPrimaryColorPurple: "#725799",
  GtFontFamily: "GT Walsheim Pro, Medium",
  OnHoverGtPrimaryColorPurple: "#4f2d7f",
  GTTealColor: "#00A7B5",
  GtColorPurple2: "#AB54DB",
  GtLightPurpleColor: "#AB54DB26",
  GtLightPurpleColor2: "#f6f2fc",
  GtRedColor: "#C24200",
};

export enum CALENDAR_VIEW {
  Day = "day",
  Month = "month",
  Quater = "quater",
  Half = "half",
  Year = "year",
  Today = "today",
}

export const NEXTPRE_CLICK = {
  Next: "Next",
  Pre: "Pre",
  None: "None",
};

export const dateFormate = "DD-MM-YYYY";
export const dateFormatYMD = "YYYY-MM-DD";
export const dateFormatyMd = "yyyy-MM-dd";
export const dayNameUnicode = "EEEE";
export const leaveShorthand = "L";
export const holidayShorthand = "H";

export enum ROLE_TYPE {
  engagementLeader = "EngagementLeader",
  delegateUser = "Delegate",
  enguiryOwner = "EO",
  designation = "Designation",
  skill = "Skill",
}

export const deleteEffortSXProps: SxProps = {
  display: "flex",
  alignItems: "center",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  mt: 5,
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GtPrimaryColor,
  },
};

export const APP_CONFIG = {
  add_more_limit: 5,
};

export const DEFAULT_SYSTEM_ACCOUNT_NAME = "System";

export const ERROR_MSG = {
  vaild_allocation_date: "Enter valid allocation date",
  vaild_allocation_min_hours: "Effort hours should between  1-8.",
  total_effort_error_msg: "Total Effort should not be greater than {0} hours.",
  timesheet_no_record_found: "No data was found for the selection criteria."
};

export const DEFAULT_ALLOCATION_HOUR = {
  min: 1,
  max: 8,
};

export const RESPONSE_STATUS = {
  Success: 200,
};

export const ROUTE_URL = {
  projectDetails: "/project-details",
  skillReview: "/skill-review",
};

export const PROJECT_TAB_DETAILS = {
  Detail_View: 0,
  Calendar_View: 1,
  Budget_Details: 2,
  Active_Allocation: 3,
  Active_Requisition: 4,
};

export const BUTTON_BACKGROUND_COLOR = {
  approved: "green",
  rejected: "red",
};

export enum WorkflowModule {
  ENGAGEMENT = "engagement",
  FILE = "file",
  ONBOARDING = "onboarding",
  KPI = "kpi",
  EMPLOYEE_ALLOCATION = "Employee Allocation",
}

export enum WorkflowSubModule {
  ENGAGEMENT = "engagement-workflow",
  FILE_UPLOAD = "file-upload",
  FILE_WORKFLOW = "file-workflow",
  FILE_SANITY = "file-sanity",
  FILE_CLEANSING = "file-cleansing",
  FILE_INGESTION = "file-ingestion",
  FILE_MERGE = "file-merge",
  ONBOARDING = "onboarding-approval-workflow",
  KPI_WORKFLOW = "kpi-workflow",
  KPI_EXECUTION_WORKFLOW = "kpi-execution-workflow",
  EMPLOYEE_ALLOCATION = "Employee_Allocation_workflow",
}

export const OUTCOME = {
  inprogress: "inprogress",
};

export const WORKFLOW_TASK_STATUS = {
  pending: "pending",
};

export enum WORKFLOW_ACTION_STATUS {
  Approved = "approved",
  Rejected = "rejected",
  Withdraw = "withdraw",
}

export const WORKFLOW_TASK_TITLE = {
  withdraw: "Employee Allocation Withdrawl Task For Employee",
};

export enum DraftStatus {
  DRAFT = "DRAFT",
}

export const GlobalConfigs = {
  dateFormat: "DD-MM-YYYY",
};

export const MenuIconSxProps: SxProps = {
  mr: 1,
  color: GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
};

export const WORKFLOW_WITHDRAW_STATUS = [
  WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR,
  WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE,
];

export const maxPriorityValue = 10;
export const EMPLOYEE_VIEW_ROLES = ["Employee"];
export const REQUESTOR_VIEW_ROLES = ["ResourceRequestor"]; // Subscription Enum
export enum SUBSCRIPTION_MODULES {
  MARKETPLACE = "MarketPlace",
}

export enum SUBSCRIPTION_ROLES {
  MARKETPLACE_LISTING = "MarketPlaceListing",
}

export const message = {
  error: {
    workflow_running_msg:
      "The selected allocation is under workflow. This cannot be updated.",
    requisition_date_expire_msg:
      "Requisition dates have lapsed, allocation not allowed on expired requisition. Please select another requisition.",
    project_date_expire_msg:
      "Project end date has expired. Allocation has not allow to update.",
  },
};

export const AutocompleteSxProps: SxProps = {
  width: "300px",
  backgroundColor: "rgb(246, 243, 243)",
};

export const NO_OF_RESOURCE_CONFIG_VALUE = {
  min: 1,
  max: 10,
};

export const CloseButtonSxProps: SxProps = {
  borderColor: " #4f2d7f",
  color: "#b5a7c9",
  marginLeft: " 40px",
  textTransform: "initial",
};
export const ApplyFilterButtonSxProps: SxProps = {
  backgroundColor: "#4f2d7f",
  marginLeft: "40px",
  textTransform: "initial",
};

export const ApplyTextTransform: SxProps = {
  textTransform: "initial !important",
};

export const DATE_TIME_INITIAL_VALUE = "January 1, 1970";

export const Project_Type = {
  Pipeline: "Pipeline",
  Job: "Job",
};

export const ProjectCategories = {
  Chargeable: "Chargeable",
  NonChargeable: "NonChargeable",
  Pipeline: "Pipeline",
  InBudget: "In-Budget",
  OverBudget: "Over-Budget",
  OutBudget: "Out-Budget",
  ReachingThresholdBudget: "Reaching your Budget threshold",
  WithinBudget: "Within Budget",
  BudgetExceeded: "Budget Exceeded",
};

export const Role = {
  Employee: "Employee",
  Delegate: "Delegate",
  ResourceRequestor: "ResourceRequestor",
  Admin: "Admin",
  CEOCOO: "CEOCOO",
  Reviewer: "Reviewer",
  Leaders: "Leaders",
  EngagementLeader: "EngagementLeader",
  CSP: "CSP",
  AdditionalDelegate: "AdditionalDelegate",
  SuperCoach:"SuperCoach",
  //NOT IN USE
  SystemAdmin: "SystemAdmin",
};

export const RoleDisplayNames = {
  SuperCoach:"Super Coach",
  CSC:"Co Super Coach",
}

export const AllocationColors = {
  ZERO: "#F6A9B3",       
  LOW: "#FBD4D9",        
  MEDIUM_LOW: "#D7EFAD", 
  MEDIUM: "#C3E784",     
  HIGH: "#AFDF5B",       
};

export const workinghours = 8;
export const HR_ABBR = "hrs"; 
export const PORTFOLIO_MAX_DATERANGE_DAYS = 90;
export const ALLOCATION_BREAKUP_MAX_LENGTH = 50;

export const CONFIDENTIAL_APPLICATION_SETTING_KEY_FIELD  = "ProjectMaskingColumn";