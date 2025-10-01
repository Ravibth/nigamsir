import moment from "moment";
import { IEvent } from "./IEvent";
import * as GC from "../../global/constant";
import { IGetUserAllocationCalanderFilterOptions } from "../../services/big-calendar-services/IGetUserAllocationCalanderFilterOptions";
import { ITimelineDisplayType, ITimelineType } from "../calendar/Utils";

export const GetEventsForEmployee = (employeesData: any) => {
  const employeeEvents: IEvent[] = [];
  employeesData.forEach((item: any) => {
    let employeeEvent: IEvent;

    //Todo: check the dateformat for time zone
    employeeEvent = {
      title: titleValue(item?.timeline_type, item),

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

export const GetDistinctPipelineCodesFromEvents = (events: any[]) => {
  const pipelineSet = new Set();
  events.forEach((data) => pipelineSet.add(data.pipelineCode));
  return Array.from(pipelineSet);
};

export const mapFilterParams = (filterParameters: any) => {
  const filterParams: IGetUserAllocationCalanderFilterOptions = {};
  if (filterParameters && filterParameters.length > 0) {
    const pipelineCodes = filterParameters.map(
      (pipeline) =>
        // pipeline.split("-")[0].trim()
        pipeline.id
    );
    filterParams.pipelineCodes = pipelineCodes;
  }
  return filterParams;
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
  switch (type.toLowerCase().trim()) {
    case ITimelineType.HOLIDAY.toLowerCase().trim():
      return "#ED5367";
      break;
    case ITimelineType.FULL_DAY_LEAVE.toLowerCase().trim():
      return "#ED5367";
      break;
    case ITimelineType.FIRST_HALF_LEAVE.toLowerCase().trim():
      return "#ED5367";
      break;
    case ITimelineType.SECOND_HALF_LEAVE.toLowerCase().trim():
      return "#ED5367";
      break;
    default:
      return "#66CAD3";
      break;
  }
};

export const titleValue = (type: string, item: any) => {
  switch (type.toLowerCase().trim()) {
    case ITimelineType.HOLIDAY.toLowerCase().trim():
      return ITimelineDisplayType.HOLIDAY;
      break;
    case ITimelineType.FULL_DAY_LEAVE.toLowerCase().trim():
      return ITimelineDisplayType.FULL_DAY_LEAVE;
      break;
    case ITimelineType.FIRST_HALF_LEAVE.toLowerCase().trim():
      return ITimelineDisplayType.FIRST_HALF_LEAVE;
      break;
    case ITimelineType.SECOND_HALF_LEAVE.toLowerCase().trim():
      return ITimelineDisplayType.SECOND_HALF_LEAVE;
      break;
    default:
      return `${item.jobCode ? item.jobName : item.pipelineName} (${
        item.jobCode ? item.jobCode : item.pipelineCode
      })`;
      break;
  }
};

export const getCalendarDropdownOptions = (options: any) => {
  const _options = [];
  options?.jobCodes?.forEach((option) => {
    _options.push({
      label: `${option.jobCode}-${option.jobName}`,
      id: option.jobCode,
    });
  });
  options?.pipelineCodes?.forEach((option) => {
    _options.push({
      label: `${option.pipelineCode}-${option.pipelineName}`,
      id: option.pipelineCode,
    });
  });
  return _options;
};
