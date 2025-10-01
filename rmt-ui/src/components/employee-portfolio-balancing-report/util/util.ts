import {
  addDays,
  addMonths,
  addWeeks,
  endOfDay,
  endOfWeek,
  startOfWeek,
} from "date-fns";
import {
  endOfMonth,
  endOfQuarter,
  startOfMonth,
  startOfQuarter,
} from "date-fns";
import moment from "moment";
import * as GC from "../../../global/constant";
import {
  getBU_Exp_SME_RUFromWcgt,
  getAllCompetency,
  get_Portfolio_Filters_Options,
  get_EmployeePortfolio,
  downloadPortfolioReport
} from "../../../services/wcgt-master-services/wcgt-master-services";
import { IBUTreeMapping } from "../../../common/interfaces/IBuTreeMapping";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import { IEmployeeLeaveHolidayAndAvailablity } from "../../../common/interfaces/IEmployeeModel";
import { AggregatedEmployee } from "../Type/type";
import { employeeName } from "../../activeallocation/AllocationFilter/constant";
import { IPortfolioFiltersOptions } from "../../../common/interfaces/IPortfolioFiltersOptions";
import { EmployeePortfolio } from "../../../services/employee-portfolio/employee-portfolio";
import { PortfolioReportHeader, PortfolioReportHeaderKey } from "./constant";

export const loadBUTreeMappingData = () => {
  return new Promise<IBUTreeMapping[]>((resolve, reject) => {
    getBU_Exp_SME_RUFromWcgt()
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

export const loadCompetencyMappingData = () => {
  return new Promise<ICompetencyMaster[]>((resolve, reject) => {
    getAllCompetency()
      .then((response) => {
        resolve(response);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

export const currentBuLeaderMaster = (
  buTreeMapping: any[],
  currentUserLeaderRole: any
) => {
  let result = [];

  if (currentUserLeaderRole && currentUserLeaderRole.bu) {
    const currentUserBUs = Object.keys(currentUserLeaderRole.bu);
    const absentBu = currentUserBUs.filter(
      (bu) =>
        !result.some(
          (r) => r.bu_id?.toLowerCase().trim() === bu.toLowerCase().trim()
        )
    );
    const busToAdd = buTreeMapping.filter((mapping) =>
      absentBu.includes(mapping.bu_id)
    );
    result = [...result, ...busToAdd];
  }
  if (currentUserLeaderRole && currentUserLeaderRole.offerings) {
    const currentUserOfferings = Object.keys(currentUserLeaderRole.offerings);
    const absentOffering = currentUserOfferings.filter(
      (offering) =>
        !result.some(
          (r) =>
            r.offering_id?.toLowerCase().trim() ===
            offering.toLowerCase().trim()
        )
    );
    const offeringToAdd = buTreeMapping.filter((mapping) =>
      absentOffering.includes(mapping.offering_id)
    );
    result = [...result, ...offeringToAdd];
  }
  if (currentUserLeaderRole && currentUserLeaderRole.solutions) {
    const currentUserSolutions = Object.keys(currentUserLeaderRole.solutions);
    const absentSolutions = currentUserSolutions.filter(
      (solution) =>
        !result.some(
          (r) =>
            r.solution_id?.toLowerCase().trim() ===
            solution.toLowerCase().trim()
        )
    );
    const solutionToAdd = buTreeMapping.filter((mapping) =>
      absentSolutions.includes(mapping.solution_id)
    );

    result = [...result, ...solutionToAdd];
  }
  return result;
};

export const currentCompetencyLeaderMaster = (
  competencyMasterList: ICompetencyMaster[],
  currentUserLeaderRole: any,
  competencyLeaderRole: ICompetencyMaster[],
  buTreeMappingMaster: IBUTreeMapping[]
) => {
  let result: ICompetencyMaster[] = [];
  if (currentUserLeaderRole && currentUserLeaderRole.bu) {
    const currentUserBUs = Object.keys(currentUserLeaderRole.bu);
    const absentBu = currentUserBUs.filter(
      (bu) =>
        !result.some(
          (r) => r.buId.toLowerCase().trim() === bu.toLowerCase().trim()
        )
    );
    const buToAdd = competencyMasterList.filter((m) =>
      absentBu.includes(m.buId)
    );
    result = [...result, ...buToAdd];
  }
  if (competencyLeaderRole && competencyLeaderRole.length > 0) {
    const absentCompetencies = competencyLeaderRole.filter(
      (cl) =>
        !result.some(
          (r) =>
            r.competencyId.toLowerCase().trim() ===
            cl.competencyId.toLowerCase().trim()
        )
    );
    result = [...result, ...absentCompetencies];
  }
  return result;
};
 
export const generateTimelineData = (
  view: string,
  startDate: Date = new Date(),
  endDate: Date = new Date()
) => {
  let start, end;

  if (view === GC.CALENDAR_VIEW.Month) {
    start = startOfMonth(startDate);
    end = endOfMonth(startDate);
  } else {
    start = startOfQuarter(startDate);
    end = endOfQuarter(startDate);
  }

  return {
    start,
    end,
    visibleTimeStart: moment(start).startOf("day").valueOf(),
    visibleTimeEnd: moment(end).endOf("day").valueOf(),
  };
};

export const aggregatedEmployeeInformation = (
  employees: IEmployeeLeaveHolidayAndAvailablity[],
  view: "month" | "quarter" | string,
  timeRange: { start: Date; end: Date }
): Map<string, AggregatedEmployee[]> => {
  const aggregatedData = new Map<string, AggregatedEmployee[]>();
  const timelineAggregatedData = new Map<string, AggregatedEmployee>();
  let employeeGroup: Record<string, IEmployeeLeaveHolidayAndAvailablity[]> = {};
  employees.map((e) => {
    if (!employeeGroup[e.employee_mid]) {
      employeeGroup[e.employee_mid] = [];
    }
    employeeGroup[e.employee_mid].push(e);
  });
  let currentWeek =
    view === "month"
      ? startOfWeek(timeRange.start, { weekStartsOn: 1 })
      : startOfMonth(timeRange.start);

  while (currentWeek <= timeRange.end) {
    const weekStart =
      view === "month"
        ? startOfWeek(currentWeek, { weekStartsOn: 1 })
        : startOfMonth(currentWeek);
    const weekEnd =
      view === "month"
        ? endOfDay(addDays(weekStart, 4))
        : endOfMonth(currentWeek);
    let totalAllocation = 0;
    let totalAvailablity = 0;
    let totalLeaveHrs = 0;
    let totalHolidayHrs = 0;
    let employee_mid = "";
    let employee_email = "";
    let email_id_uid = "";
    let employee_name = "";
    Object.keys(employeeGroup).forEach((e) => {
      const employeeCurrentWeekData = employeeGroup[e].filter(
        (x) =>
          new Date(moment(x.working_date).format("YYYY-MM-DD")) >= weekStart &&
          new Date(moment(x.working_date).format("YYYY-MM-DD")) <= weekEnd
      );
      employeeCurrentWeekData.forEach((x) => {
        totalAllocation += x.allocation_hrs;
        totalAvailablity += x.available_hrs;
        totalLeaveHrs += x.leave_hrs;
        totalHolidayHrs += x.holiday_hrs;
        employee_mid = x.employee_mid;
        employee_email = x.email_id;
        email_id_uid = x.email_id_uid;
        employee_name = x.name;
      });
      if (employeeCurrentWeekData.length > 0) {
        if (!timelineAggregatedData.has(employee_mid)) {
          timelineAggregatedData.set(employee_mid, {
            start: timeRange.start,
            end: timeRange.end,
            allocation_hrs: 0,
            availablity_hrs: 0,
            leave_hrs: 0,
            holiday_hrs: 0,
            employee_mid: employee_mid,
            employee_email: employee_email,
            employee_email_uid: email_id_uid,
            employee_name: employee_name,
            totalTimelineAllocationHrs: 0,
            totalTimelineLeaveHrs: 0,
            totalTimelineHolidayHrs: 0,
            totalTimelineAvailablity: 0,
          });
        }

        timelineAggregatedData.set(employee_mid, {
          ...timelineAggregatedData.get(employee_mid),
          allocation_hrs:
            timelineAggregatedData.get(employee_mid).allocation_hrs +
            totalAllocation,
          availablity_hrs:
            timelineAggregatedData.get(employee_mid).availablity_hrs +
            totalAvailablity,
          leave_hrs:
            timelineAggregatedData.get(employee_mid).leave_hrs + totalLeaveHrs,
          holiday_hrs:
            timelineAggregatedData.get(employee_mid).holiday_hrs +
            totalHolidayHrs,
          totalTimelineAllocationHrs:
            timelineAggregatedData.get(employee_mid).allocation_hrs +
            totalAllocation,
          totalTimelineLeaveHrs:
            timelineAggregatedData.get(employee_mid).leave_hrs + totalLeaveHrs,
          totalTimelineHolidayHrs:
            timelineAggregatedData.get(employee_mid).holiday_hrs +
            totalHolidayHrs,
          totalTimelineAvailablity:
            timelineAggregatedData.get(employee_mid).availablity_hrs +
            totalAvailablity,
        });

        if (!aggregatedData.has(e)) {
          aggregatedData.set(e, []);
        }

        aggregatedData.set(e, [
          ...(aggregatedData.get(e) || []),
          {
            start: weekStart,
            end: weekEnd,
            allocation_hrs: totalAllocation,
            leave_hrs: totalLeaveHrs,
            availablity_hrs: totalAvailablity,
            holiday_hrs: totalHolidayHrs,
            employee_mid: employee_mid,
            employee_email: employee_email,
            employee_email_uid: email_id_uid,
            employee_name: employee_name,
            totalTimelineAllocationHrs:
              timelineAggregatedData.get(employee_mid).allocation_hrs,
            totalTimelineLeaveHrs:
              timelineAggregatedData.get(employee_mid).leave_hrs,
            totalTimelineHolidayHrs:
              timelineAggregatedData.get(employee_mid).holiday_hrs,
            totalTimelineAvailablity:
              timelineAggregatedData.get(employee_mid).availablity_hrs,
          },
        ]);
      }
      totalAllocation = 0;
      totalAvailablity = 0;
      totalLeaveHrs = 0;
      totalHolidayHrs = 0;
      employee_mid = "";
      employee_email = "";
      email_id_uid = "";
      employee_name = "";
    });    
    currentWeek =
      view === "month"
        ? startOfWeek(addWeeks(currentWeek, 1), { weekStartsOn: 1 })
        : addMonths(currentWeek, 1);
  }
  // } else if (view === "quarterly") {
  // }
  Array.from(aggregatedData.values()).forEach((value) => {
    value.forEach((v) => {
      if (timelineAggregatedData.has(v.employee_mid)) {
        v.totalTimelineAllocationHrs = timelineAggregatedData.get(
          v.employee_mid
        ).allocation_hrs;
        v.totalTimelineLeaveHrs = timelineAggregatedData.get(
          v.employee_mid
        ).leave_hrs;
        v.totalTimelineHolidayHrs = timelineAggregatedData.get(
          v.employee_mid
        ).holiday_hrs;
        v.totalTimelineAvailablity = timelineAggregatedData.get(
          v.employee_mid
        ).availablity_hrs;
      }
    });
  });
  return aggregatedData;
};

export const getEmployeeItemBackgroundColor = (employee: AggregatedEmployee) => 
{
  //let net_availablity_hrs = employee.availablity_hrs - (employee.leave_hrs + employee.holiday_hrs);
  //let allocation_pct = net_availablity_hrs > 0 ? (employee.allocation_hrs / net_availablity_hrs) * 100 : 100; //if net_availablity_hrs is 0 then alloc pct 100 to get 0 avail bcz of holidays
  let availability_pct = Number(getAvailablityPercentage(employee.availablity_hrs,employee.allocation_hrs,employee.leave_hrs,employee.holiday_hrs))

  if (availability_pct === 0) {
    return GC.AllocationColors.ZERO;
  }
  if (availability_pct <= 20) {
    return GC.AllocationColors.LOW;
  }
  if (availability_pct <= 40) {
    return GC.AllocationColors.MEDIUM_LOW;
  }
  if (availability_pct <= 70) {
    return GC.AllocationColors.MEDIUM;
  }
  return GC.AllocationColors.HIGH;
};

export function calculateStartAndEnd(view: string, selectedDate: Date) {
  if (view === GC.CALENDAR_VIEW.Month) {
    return {
      startDate: startOfMonth(selectedDate),
      endDate: endOfMonth(selectedDate),
    };
  } else if (view === GC.CALENDAR_VIEW.Quater) {
    return {
      startDate: startOfQuarter(selectedDate),
      endDate: endOfQuarter(selectedDate),
    };
  } else {
    throw new Error("Unsupported view type");
  }
}



export function calculateNextDates(
  currentView: string,
  selectedDate: Date
): { newSelectedDate: Date; newStartDate: Date; newEndDate: Date } {
  const nextDate = new Date(selectedDate);
  let newStartDate: Date;
  let newEndDate: Date;

  if (currentView === GC.CALENDAR_VIEW.Month) {
    nextDate.setMonth(nextDate?.getMonth() + 1);
    newStartDate = new Date(nextDate.getFullYear(), nextDate?.getMonth(), 1);
    newEndDate = new Date(nextDate.getFullYear(), nextDate?.getMonth() + 1, 0);
  } else if (currentView === GC.CALENDAR_VIEW.Quater) {
    const currentMonth = selectedDate?.getMonth();
    const currentQuarter = Math.floor(currentMonth / 3);
    const nextQuarter = currentQuarter + 1;

    const nextQuarterYear =
      nextQuarter >= 4
        ? selectedDate.getFullYear() + 1
        : selectedDate.getFullYear();
    const nextQuarterStartMonth = (nextQuarter % 4) * 3;

    newStartDate = new Date(nextQuarterYear, nextQuarterStartMonth, 1);
    newEndDate = new Date(nextQuarterYear, nextQuarterStartMonth + 3, 0);
    nextDate.setFullYear(
      newStartDate?.getFullYear(),
      newStartDate?.getMonth(),
      1
    ); // move selectedDate to the next quarter start
  } else {
    throw new Error("Unsupported view type");
  }

  return { newSelectedDate: nextDate, newStartDate, newEndDate };
}

export function calculatePreviousDates(
  currentView: string,
  selectedDate: Date
): { newSelectedDate: Date; newStartDate: Date; newEndDate: Date } {
  const previousDate = new Date(selectedDate);
  let newStartDate: Date;
  let newEndDate: Date;

  if (currentView === GC.CALENDAR_VIEW.Month) {
    previousDate.setMonth(previousDate?.getMonth() - 1);
    newStartDate = new Date(
      previousDate?.getFullYear(),
      previousDate?.getMonth(),
      1
    );
    newEndDate = new Date(
      previousDate?.getFullYear(),
      previousDate?.getMonth() + 1,
      0
    );
  } else if (currentView === GC.CALENDAR_VIEW.Quater) {
    const currentMonth = selectedDate?.getMonth();
    const currentQuarter = Math.floor(currentMonth / 3);
    const previousQuarter = currentQuarter - 1;

    const previousQuarterYear =
      previousQuarter < 0
        ? selectedDate?.getFullYear() - 1
        : selectedDate?.getFullYear();
    const previousQuarterStartMonth = ((previousQuarter + 4) % 4) * 3;

    newStartDate = new Date(previousQuarterYear, previousQuarterStartMonth, 1);
    newEndDate = new Date(
      previousQuarterYear,
      previousQuarterStartMonth + 3,
      0
    );
    previousDate.setFullYear(
      newStartDate?.getFullYear(),
      newStartDate?.getMonth(),
      1
    ); // move selectedDate to the previous quarter start
  } else {
    throw new Error("Unsupported view type");
  }

  return { newSelectedDate: previousDate, newStartDate, newEndDate };
}

// Utility function to get Monday of the week
export const getMonday = (date: Date): Date => {
  const day = date.getDay();
  const diffToMonday = day === 0 ? -6 : 1 - day;
  const monday = new Date(date);
  monday.setDate(date.getDate() + diffToMonday);
  monday.setHours(0, 0, 0, 0);
  return monday;
};

// Utility function to get Friday of the week
export const getFriday = (monday: Date): Date => {
  const friday = new Date(monday);
  friday.setDate(monday.getDate() + 4);
  friday.setHours(23, 59, 59, 999);
  return friday;
};

// Utility function to create weekly item
// export const createWeeklyItem = (
//   itemIdOffset: string,
//   groupId: string,
//   week: any
// ): any => {
//   return {
//     id: itemIdOffset,
//     group: groupId,
//     title: `${week.totalEffort}h`,
//     start_time: week.start,
//     end_time: week.end,
//     effort: week.totalEffort,
//     innerHeight: 500,
//     outerHeight: 500,
//     itemProps: {
//       style: {
//         background: "#4caf50",
//         color: "white",
//         borderRadius: "4px",
//         padding: "1px 8px",
//       },
//     },
//   };
// };

export const getAvailablityPercentage = (
  totalAvailablity: number,
  totalAllocation: number,
  totalLeaveHrs: number,
  totalHolidayHrs: number
) => {
  if (totalAvailablity === 0) {
    return 0;
  }
  let net_availablity =
    Math.max(0,totalAvailablity - (totalAllocation + totalLeaveHrs + totalHolidayHrs));
  return ((net_availablity / totalAvailablity) * 100).toFixed(2);
};
export async function getEmployeeProfileData(
  startDate: any,
  endDate: any,
  submittedFilterData: any,
  bu_id: string[] | null,
  competency_ids: string[] | null,
  pageNumber: number,
  pageSize: number,
  roles: string[] | null
) {
  const payload = {
    StartDate: startDate,
    EndDate: endDate,
    availablity: parseInt(submittedFilterData?.availability) || 0,
    grade: submittedFilterData?.grade || null,
    designation: submittedFilterData?.designation?.map((a) => a.id) || null,
    employeename: submittedFilterData?.employeename?.map((a) => a.id) || null,
    location: submittedFilterData?.location?.map((a) => a.id) || null,
    supercoach: submittedFilterData?.supercoach?.map((a) => a.id) || null,
    cosupercoach: submittedFilterData?.cosupercoach?.map((a) => a.id) || null,
    clientname: submittedFilterData?.clientname?.map((a) => a.id) || null,
    clientgroupname:
      submittedFilterData?.clientgroupname?.map((a) => a.id) || null,
    business_unit_ids: bu_id,
    competency_ids: competency_ids,
    PageSize: pageSize,
    PageNumber: pageNumber,
    roles: roles,
  };
  const data = await get_EmployeePortfolio(payload);
  return data;
}

export const getPortfolioFiltersOptions = async (
  startDate: any,
  endDate: any,
  bu_id: string[] | null,
  competency_ids: string[] | null,
  roles: string[] | null
) => {
  const payload = {
    StartDate: startDate,
    EndDate: endDate,
    business_unit_ids: bu_id,
    competency_ids: competency_ids,
    availablity: null,
    grade: null,
    designation: null,
    employeename: null,
    location: null,
    supercoach: null,
    cosupercoach: null,
    clientname: null,
    clientgroupname: null,
    roles: roles
  };
  const data = await get_Portfolio_Filters_Options(payload);
  return data;
};

export const downloadEmployeesForPortfolioReport = async (
  startDate: any,
  endDate: any,
  submittedFilterData: any,
  bu_id: string[] | null,
  competency_ids: string[] | null,
  roles: string[] | null,
  currentView: string
) => {
  const payload = {
    StartDate: startDate,
    EndDate: endDate,
    availablity: parseInt(submittedFilterData?.availability) || 0,
    grade: submittedFilterData?.grade || null,
    designation: submittedFilterData?.designation?.map((a) => a.id) || null,
    employeename: submittedFilterData?.employeename?.map((a) => a.id) || null,
    location: submittedFilterData?.location?.map((a) => a.id) || null,
    supercoach: submittedFilterData?.supercoach?.map((a) => a.id) || null,
    cosupercoach: submittedFilterData?.cosupercoach?.map((a) => a.id) || null,
    clientname: submittedFilterData?.clientname?.map((a) => a.id) || null,
    clientgroupname:
      submittedFilterData?.clientgroupname?.map((a) => a.id) || null,
    business_unit_ids: bu_id,
    competency_ids: competency_ids,   
    roles: roles,
    periodType: currentView  =='quater' ? 1 : 0
  };
  const data = await downloadPortfolioReport(payload);
  return data;
};

export const getQuarterEndMonth = (date) => {
  const month = date.getMonth(); // 0-based: Jan=0, Dec=11
  if (month <= 2) return 2; // Q1 ends in March
  if (month <= 5) return 5; // Q2 ends in June
  if (month <= 8) return 8; // Q3 ends in September
  return 11; // Q4 ends in December
};


export const createWeeklyItem = (
  itemIdOffset: string,
  groupId: string,
  week: any,
  projectAllocation: any,
  filteredEmployees:IEmployeeLeaveHolidayAndAvailablity[],
  employee_mid:any
): any => {
  return {
    week:week,
    projectAllocation:projectAllocation,
    filteredEmployees:filteredEmployees,
    employee_mid:employee_mid,
    type: "PROJECT_ITEM",
    id: itemIdOffset,
    group: groupId,
    start_time: week.start,
    end_time: week.end,
    effort: week.totalEffort,
    innerHeight: 500,
    outerHeight: 500,
    itemProps: {
      style: {
        background: "#D8BFD8",
        color: "black",
        borderRadius: "4px",
        padding: "1px 8px",          
      },
    },            
  };
};

const getHeader = (data) => {
  const header = PortfolioReportHeader;
  const weekHeaders = [];

  data.forEach(item => {
    if (item.weeklyHours) {
      Object.keys(item.weeklyHours).forEach(week => {
        if (!weekHeaders.includes(week)) {
          weekHeaders.push(week);
        }
      });
    }
  });

  const sortedWeeks = weekHeaders.sort();

  // ✅ Modify the first week header (e.g., rename it to "First Week")
 

  return [...header, ...sortedWeeks];
};


export const convertToCSV = (data, startDate) => {
  if (!data || data.length === 0) return '';
  
  // Get all possible week headers from all records
  const allWeekHeaders = [];
  data.forEach(item => {
    if (item.weeklyHours) {
      Object.keys(item.weeklyHours).forEach(week => {
        if (!allWeekHeaders.includes(week)) {
          allWeekHeaders.push(week);
        }
      });
    }
  });
  //allWeekHeaders.sort(); // Optional: sort the weeks
  const allWeekHeadersKey = [...allWeekHeaders];

   if (allWeekHeaders.length > 0) {
    allWeekHeaders[0] = `${allWeekHeaders[0]}-${startDate}`;
  }

  const headers = [...PortfolioReportHeader, ...allWeekHeaders];
  
  const csvRows = [];
  csvRows.push(headers.join(','));
  
  for (const row of data) {
    const values = PortfolioReportHeaderKey.map(headerKey => {
      const escaped = ('' + (row[headerKey] || '')).replace(/"/g, '""');
      return `"${escaped}"`;
    });
    
    // Add weekly hours values in the same order as the headers
    const weeklyValues = allWeekHeadersKey.map(week => {
      const hours = row.weeklyHours ? row.weeklyHours[week] || 0 : 0;
      return `"${hours}"`;
    });
    
    csvRows.push([...values, ...weeklyValues].join(','));
  }
  
  return csvRows.join('\n');
};

export const convertToCSV01 = (data, startDate, periodType) => {
  if (!data || data.length === 0) return '';
  
  // Get all possible period headers (W1/W2 or M1/M2) from all records
  const allPeriodHeaders = [];
  data.forEach(item => {
    if (item.weeklyHours) {
      Object.keys(item.weeklyHours).forEach(period => {
        if (!allPeriodHeaders.includes(period)) {
          allPeriodHeaders.push(period);
        }
      });
    }
  });
  
  // Sort periods numerically (W1, W2 or M1, M2)
  allPeriodHeaders.sort((a, b) => {
    const numA = parseInt(a.substring(1));
    const numB = parseInt(b.substring(1));
    return numA - numB;
  });

  // Create headers with period labels
  const periodHeaders = allPeriodHeaders.map(period => {
    return `${periodType === 'Monthly' ? 'Month' : 'Week'} ${period.substring(1)}`;
  });

  const headers = [...PortfolioReportHeader, ...periodHeaders];
  
  const csvRows = [];
  csvRows.push(headers.join(','));
  
  for (const row of data) {
    const values = PortfolioReportHeaderKey.map(headerKey => {
      const escaped = ('' + (row[headerKey] || '')).replace(/"/g, '""');
      return `"${escaped}"`;
    });
    
    // Add period hours values in the same order as the headers
    const periodValues = allPeriodHeaders.map(period => {
      const hours = row.periodData ? row.periodData[period] || 0 : 0;
      return `"${hours}"`;
    });
    
    csvRows.push([...values, ...periodValues].join(','));
  }
  
  return csvRows.join('\n');
};