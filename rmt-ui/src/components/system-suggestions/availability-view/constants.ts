import { SxProps } from "@mui/system";
import { ITimelineType, IWeeklyBreakup } from "../../calendar/Utils";

export const TooltipDetailsSxProps: SxProps = {
  padding: "10px 10px 10px 10px",
  width: "100%",
};

export interface IUserTimelineInner {
  start: Date;
  end: Date;
  timeline_type: string;
  timeline_display_text: string;
  pipelineCode?: string;
  weeklyBreakup: IWeeklyBreakup;
  weeklyTotal: number;
}
export interface IUsersTimelines {
  email: string;
  usersTimelines: IUserTimelineInner[];
}

export const TextWrapSxProps: SxProps = {
  textOverflow: "ellipsis",
  overflow: "hidden",
  whiteSpace: "nowrap",
};

export interface IUserInfo {
  id?: number;
  empName: string;
  email: string;
  designation?: string;
  grade?: string;
  location?: string;
  supercoach?: string;
  sme_group?: string;
  match_score?: string;
  interested?: boolean;
  businessUnit?: string;
  industry?: string;
  competency?: string;
  competencyId?: string;
}

export const MatchScoreSXProps: SxProps = {
  border: "1px solid gray",
  padding: "5px",
};

export const DataGridSxProps: SxProps = {
  width: "100%",
  height: "40vh",
};

export const TimelineSxProps: SxProps = {
  width: "100%",
  overflow: "auto",
  border: "1px solid gray",
  height: "68vh",
};

export const MatchScoreAvailabilitySxProps: SxProps = {
  width: "100%",
  borderRadius: "10px",
  padding: "10px",
  display: "inline-flex",
  justifyContent: "center",
  color: "#000 !important",
  fontSize: "20px",
  fontWeight: "600 !important",
};

export const UserInfoTooltipCategories = [
  "Email",
  "Designation",
  "Location",
  "Supercoach",
  "Business Unit",
  "Competency",
];
export const categoryLabels = (category: string) => {
  const value = category.split("_").join(" ");
  switch (value.toLowerCase().trim()) {
    case "same client":
      return "Same Client Exp.";
    case "Competency":
      return "Competency";
    case "location":
      return "Location";
    case "subindustry" || "sub industry":
      return "Sub Industry";
    case "revenue unit":
      return "Revenue Unit";
    case "business unit":
      return "Business Unit";
    case "solutions":
      return "Solutions";
    case "offerings":
      return "Offerings";
    case "competency":
      return "Competency";
    default:
      return value;
  }
};

export const getCustomItemPropsClass = (type: any) => {
  switch (type.toLowerCase().trim()) {
    // case ITimelineType.LEAVE:
    //   return "item-leave";
    case ITimelineType.FULL_DAY_LEAVE.toLowerCase().trim():
      return "item-leave";
    case ITimelineType.FIRST_HALF_LEAVE.toLowerCase().trim():
    case ITimelineType.SECOND_HALF_LEAVE.toLowerCase().trim():
    case ITimelineType.ALLOCATION.toLowerCase().trim():
      return "item-allocation";
    case ITimelineType.AVAILABLE.toLowerCase().trim():
      return "item-available";
    case ITimelineType.HOLIDAY.toLowerCase().trim():
      return "item-holiday";
    default:
      return "item-allocation";
  }
};

export const UserInfoTooltipColumnsSxProps: SxProps = {
  whiteSpace: "nowrap",
  textOverflow: "ellipsis",
  overflow: "hidden",
  color: "black",
  fontWeight: "500",
  fontSize: "18px",
};
export const UserInfoTooltipValuesSxProps: SxProps = {
  color: "black",
  whiteSpace: "nowrap",
  textOverflow: "ellipsis",
  overflow: "hidden",
};
