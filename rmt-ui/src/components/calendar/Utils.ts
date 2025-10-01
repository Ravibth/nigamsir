import moment from "moment";
import { IEvent } from "./IEvent";
import * as GC from "../../global/constant";

export enum ITimelineType {
  // LEAVE = "leave",
  FULL_DAY_LEAVE = "FULL_DAY_LEAVE",
  FIRST_HALF_LEAVE = "FIRST_HALF_LEAVE",
  SECOND_HALF_LEAVE = "SECOND_HALF_LEAVE",
  HOLIDAY = "Holiday",
  ALLOCATION = "Allocation",
  AVAILABLE = "Available",
}

export enum ITimelineDisplayType {
  // LEAVE = "Leave",
  FULL_DAY_LEAVE = "Leave",
  FIRST_HALF_LEAVE = "Half Day Leave",
  SECOND_HALF_LEAVE = "Half Day Leave",
  HOLIDAY = "Holiday",
  ALLOCATION = "Allocation",
}

export interface IWeeklyBreakup {
  mon: string;
  tue: string;
  wed: string;
  thu: string;
  fri: string;
}

export interface IWeeklyBreakupProps {
  breakup: IWeeklyBreakup;
  weeklyTotal: number;
}

export const GetEventsForEmployee = (employeesData: any) => {
  const employeeEvents: IEvent[] = [];
  employeesData.forEach((item: any) => {
    let employeeEvent: IEvent;

    //Todo: check the dateformat for time zone
    employeeEvent = {
      title: titleValue(item.timeline_type, item),
      start: moment(item.startDate, GC.dateFormatYMD).toDate(),
      // start: moment(),
      // end: moment().add(2,'day'),
      pipelineCode: item.pipelineCode,
      end: moment(item.endDate, GC.dateFormatYMD).add(1, "day").toDate(),
      color: "red",
      type: item?.timeline_type,
    };
    employeeEvents.push(employeeEvent);
  });
  return employeeEvents;
};
export const GetEventsFilterByPipelineCodes = (
  events: any[],
  pipelineCodes: any[]
) => {
  if (pipelineCodes.length === 0) {
    return events;
  }
  return events.filter((data) => pipelineCodes.includes(data.pipelineCode));
};
export const GetDistinctPipelineCodesFromEvents = (events: any[]) => {
  const pipelineSet = new Set();
  events.forEach((data) => pipelineSet.add(data.pipelineCode));
  return Array.from(pipelineSet);
};

export const getStyleDetils = (type: string) => {
  // if (
  //   type.toLowerCase().trim() ===
  //     ITimelineType.LEAVE.toString().toLowerCase().trim() ||
  //   type.toLowerCase().trim() ===
  //     ITimelineType.HOLIDAY.toString().toLowerCase().trim()
  // ) {
  //   return "#ED5367";
  // } else {
  //   return "#66CAD3";
  // }
  switch (type?.toLowerCase()?.trim()) {
    case ITimelineType.HOLIDAY.toLowerCase().trim():
      return "#ED5367";
      break;
    case ITimelineType.FULL_DAY_LEAVE.toLowerCase().trim():
      return "#ED5367";
      break;
    case ITimelineType.FIRST_HALF_LEAVE.toLowerCase().trim():
      return "#66CAD3";
      break;
    case ITimelineType.SECOND_HALF_LEAVE.toLowerCase().trim():
      return "#66CAD3";
      break;
    default:
      return "#66CAD3";
      break;
  }
};

export const titleValue = (type: string, item: any) => {
  switch (type?.toLowerCase()?.trim()) {
    case ITimelineType.HOLIDAY.toLowerCase().trim():
      return `${item.empName}(${ITimelineDisplayType.HOLIDAY})`;
      break;
    case ITimelineType.FULL_DAY_LEAVE.toLowerCase().trim():
      return `${item.empName}(${ITimelineDisplayType.FULL_DAY_LEAVE})`;
      break;
    case ITimelineType.FIRST_HALF_LEAVE.toLowerCase().trim():
      return `${item.empName}(${ITimelineDisplayType.FIRST_HALF_LEAVE})`;
      break;
    case ITimelineType.SECOND_HALF_LEAVE.toLowerCase().trim():
      return `${item.empName}(${ITimelineDisplayType.SECOND_HALF_LEAVE})`;
      break;
    default:
      return `${item.empName}`;
      break;
  }
};
