import React from "react";
import { AggregatedEmployee, IEmployeeItemRow } from "../Type/type";
import { eachDayOfInterval, format, parseISO } from "date-fns";
import {
  dateFormatyMd,
  dayNameUnicode,
  leaveShorthand,
  holidayShorthand,
  workinghours,
  HR_ABBR,
  CALENDAR_VIEW,
} from "../../../global/constant";
import WeeklyPopover from "../../../common/popover/weeklypopover";
import { getAvailablityPercentage } from "../util/util";

const EmployeeItemRow = ({
  employee,
  filteredEmployees,
  currentView,
}: IEmployeeItemRow) => {
  const startDate = new Date(employee.start);
  const endDate = new Date(employee.end);
  let totalTimelineAvailablity = 0;
  let net_availablity_hrs =
    employee.availablity_hrs - (employee.leave_hrs + employee.holiday_hrs);
  let allocation_pct =
    net_availablity_hrs > 0
      ? (employee.allocation_hrs / net_availablity_hrs) * 100
      : 0;

  let available_hrs_show = net_availablity_hrs - employee.allocation_hrs;
  available_hrs_show = available_hrs_show > 0 ? available_hrs_show : 0;
  let availability_pct = getAvailablityPercentage(
    employee.availablity_hrs,
    employee.allocation_hrs,
    employee.leave_hrs,
    employee.holiday_hrs
  );

  const weeklyBreakup: Record<string, string> = {
    Monday: "0",
    Tuesday: "0",
    Wednesday: "0",
    Thursday: "0",
    Friday: "0",
  };

  // Generate all dates from start to end
  const allDates = eachDayOfInterval({ start: startDate, end: endDate });

  allDates.forEach((date) => {
    const dayName = format(date, dayNameUnicode);
    if (dayName in weeklyBreakup) {
      const workEntry = filteredEmployees.find(
        (emp) =>
          emp.employee_mid === employee.employee_mid &&
          format(parseISO(emp.working_date.toString()), dateFormatyMd) ===
            format(date, dateFormatyMd)
      );

      if (workEntry) {
        if (workEntry.leave_hrs == 8) {
          weeklyBreakup[dayName] = leaveShorthand;
          return;
        }
        if (workEntry.holiday_hrs == 8) {
          weeklyBreakup[dayName] = holidayShorthand;
          return;
        }
        let totalHours = Math.max(
          0,
          workinghours -
            workEntry.leave_hrs -
            workEntry.holiday_hrs -
            workEntry.allocation_hrs
        );
        weeklyBreakup[dayName] = totalHours.toString();
        totalTimelineAvailablity += totalHours;
      } else {
        weeklyBreakup[dayName] = "";
      }
    }
  });

  return (
    <WeeklyPopover
      id={`employee-${employee.employee_mid}`}
      displayContent={`${available_hrs_show} ${HR_ABBR} - ${availability_pct}%`}
      popoverContent={
        currentView == CALENDAR_VIEW.Quater
          ? null
          : {
              weeklyBreakup,
              total: totalTimelineAvailablity,
              totalLabel: `Total Available(${HR_ABBR})`,
            }
      }
    />
  );
};

export default EmployeeItemRow;
