import moment from "moment";
import {
  EAllocationStatuses,
  ERequisitionDesignations,
  GroupKey,
} from "./Idata";
import { SxProps } from "@mui/system";
import { EProjectRequisitionAllocationStatus } from "../../../common/enums/ERoles";
import { EPipelineStatus } from "../../../common/enums/EProject";

export const projectsList: GroupKey[] = [];

// export const items = [
//   {
//     id: 1,
//     group: 2,
//     title: "Child",
//     start_time: moment().toDate(),
//     end_time: moment().add(7, "day").toDate(),
//     innerHeight: 500,
//     outerHeight: 500,
//     className: "item-color",
//   },
//   {
//     id: 3,
//     group: 1.1,
//     title: "item 3",
//     start_time: moment().add(-1, "day").toDate(),
//     end_time: moment().add(5, "day").toDate(),
//   },
//   {
//     id: 4,
//     group: 1.2,
//     title: "item 4",
//     start_time: moment().add(-1, "day").toDate(),
//     end_time: moment().add(5, "day").toDate(),
//   },

//   {
//     id: 2,
//     title: "Project name 2",
//     defaultExpanded: false,
//     details: [
//       {
//         id: 2.1,
//         title: "Rohit",
//       },
//     ],
//   },
//   {
//     id: 3,
//     title: "Project name 3",
//     defaultExpanded: false,
//     details: [],
//   },
// ];

export const inlineStyles = {
  marginLeft: 40,
};

export enum CALENDAR_VIEW {
  Day = "day",
  Month = "month",
  Quater = "quater",
  Half = "half",
  Year = "year",
  Today = "today",
}
export enum PROJECT_CHARGE_TYPE {
  Chargable = "CHARGEABLE",
  NonChargable = "NONCHARGEABLE",
  All = "ALL",
}

export enum PROJECT_TYPE {
  OPEN = "Open",
  CLOSE = "Closed",
  All = "ALL",
}

export enum CLICKEVENT_TYPE {
  Previous = "Previous",
  Next = "Next",
}

export enum edge {
  Left = "left",
  Right = "right",
}

export enum fontSize {
  small = "small",
  inherit = "inherit",
  medium = "medium",
  large = "large",
}
export const tooltip: SxProps = {
  backgroundColor: "white",
  color: "black",
  border: "1px solid gray",
  padding: "20px",
  width: "100%",
  height: "100%",
};
export const convertToDuration = (
  unit: string
): moment.unitOfTime.DurationConstructor => {
  if (
    unit === "month" ||
    unit === "hour" ||
    unit === "week" ||
    unit === "seconds" ||
    unit === "minutes" ||
    unit === "hours" ||
    unit === "days" ||
    unit === "weeks" ||
    unit === "months" ||
    unit === "year" ||
    unit === "quarter" ||
    unit === "day"
  ) {
    return unit;
  } // Default unit is weeks
  else return "weeks";
};

export const MOMENT_UNIT = {
  Months: "months",
  // Month: "month",
  // Week: "week",
  Weeks: "weeks",
  // Days: "days",
  Day: "day",
  // Hours: "hours",
  // Hour: "hour",
  // Minutes: "minutes",
  // Seconds: "seconds",
  Year: "year",
  Quarter: "quarter",
  // Half: "half",
};

export const Calendar_Config = {
  Sticky: "sticky",
};
export const VerticalCenterAlignSxProps: SxProps = {
  display: "flex !important",
  alignItems: "center !important",
  // paddingLeft: "4px",
};
export const HorizontalFlexEnd: SxProps = {
  display: "flex",
  justifyContent: "flex-end",
};

export const canResizeType: any = {
  Both: "both",
};

export const primaryHeader = "primaryHeader";

export enum ItemType {
  Default = "default", //
  PipelineDuration = "pipelineduration", //
  JobDuration = "jobduration", //
  Allocation = "allocation", //
  Available = "available", //
  Rollover = "rollover", //
  Holiday = "holiday",
  // Leave = "leave", //
  FULL_DAY_LEAVE = "FULL_DAY_LEAVE",
  FIRST_HALF_LEAVE = "FIRST_HALF_LEAVE",
  SECOND_HALF_LEAVE = "SECOND_HALF_LEAVE",
  // RolloverForward = "rolloverforward", //
  Suspended = "Suspended",
  Lost = "Lost",
  DisplayWon = "WON",
  won = "won",
}

export enum GroupType {
  Pipeline = "pipeline", //
  Job = "job", //
  Employee = "employee", //
}

export const debounceFetchSuggestion = (fn: any, delay: number) => {
  let timeout: any;
  return (...args: any) => {
    clearTimeout(timeout);
    timeout = setTimeout(() => {
      fn(...args);
    }, delay);
  };
};

export const getProjectTitle = (item: any, status: any) => {
  let projectAllocationStatus = "";
  let circleColor = "white";
  if (
    item.pipelineStatus === EPipelineStatus.Suspended ||
    item.pipelineStatus === EPipelineStatus.Lost
  ) {
    projectAllocationStatus = "";
    circleColor = "white";
  } else if (!item?.projectRequisitionAllocations) {
    projectAllocationStatus = EProjectRequisitionAllocationStatus.ToBeStarted;
    circleColor = "red";
  } else {
    switch (item?.projectRequisitionAllocations?.status.toString().trim()) {
      case EProjectRequisitionAllocationStatus.Completed.toString().trim():
        projectAllocationStatus = EProjectRequisitionAllocationStatus.Completed;
        circleColor = "green";
        break;
      case EProjectRequisitionAllocationStatus.PENDING.toString().trim():
        projectAllocationStatus = EProjectRequisitionAllocationStatus.PENDING;
        circleColor = "yellow";
        break;
      case EProjectRequisitionAllocationStatus.ToBeStarted.toString().trim():
        projectAllocationStatus =
          EProjectRequisitionAllocationStatus.ToBeStarted;
        circleColor = "red";
        break;
    }
  }
};
