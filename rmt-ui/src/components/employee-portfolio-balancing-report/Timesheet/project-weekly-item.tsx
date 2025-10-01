import React from 'react';
import WeeklyPopover from '../../../common/popover/weeklypopover';
import { HR_ABBR, dateFormatyMd, dayNameUnicode, holidayShorthand, leaveShorthand } from '../../../global/constant';
import format from 'date-fns/format';
import { eachDayOfInterval, parseISO } from 'date-fns';
import { IEmployeeLeaveHolidayAndAvailablity } from '../../../common/interfaces/IEmployeeModel';


interface WeeklyItemProps {
  id: string;
  groupId: string;
  week: {
    totalEffort: number;
    start: Date;
    end: Date;
  };
  projectAllocation: any;
  employee_mid:any;
  filteredEmployees:IEmployeeLeaveHolidayAndAvailablity[];
}

const ProjectWeeklyItem: React.FC<WeeklyItemProps> = ({
  id,
  groupId,
  week,
  projectAllocation,
  employee_mid,
  filteredEmployees
}) => {
  const weekStart = week.start;
  const weekEnd = week.end;
  
  const weeklyBreakup: Record<string, string> = {
    Monday: "0",
    Tuesday: "0",
    Wednesday: "0",
    Thursday: "0",
    Friday: "0",
  };
  
  let total = 0;

  projectAllocation.resourceAllocationDays.forEach((dayAlloc: any) => {
    const allocDate = new Date(dayAlloc.allocationDate);
    if (allocDate >= weekStart && allocDate <= weekEnd) {      
      const day = allocDate.toLocaleDateString('en-US', { weekday: 'long' });
      weeklyBreakup[day] = dayAlloc.efforts;
      total += dayAlloc.efforts;
    }
  });  

  //To add Holidays and leaves in weekly popover
  // Generate all dates from start to end
  const allDates = eachDayOfInterval({ start: week.start, end: week.end });

  allDates.forEach((date) => {
    const dayName = format(date, dayNameUnicode);
    if (dayName in weeklyBreakup) {
      const workEntry = filteredEmployees.find(
        (emp) =>
          emp.employee_mid === employee_mid &&
          format(parseISO(emp.working_date.toString()), dateFormatyMd) === format(date, dateFormatyMd)
      );
      
      if (workEntry) {
        let dayAllocEfforts = parseInt(weeklyBreakup[dayName]);
        if (workEntry.leave_hrs == 8) {
          total -= dayAllocEfforts;
          weeklyBreakup[dayName] = leaveShorthand;
          return;
        }
        if (workEntry.holiday_hrs == 8) {
          total -= dayAllocEfforts;
          weeklyBreakup[dayName] = holidayShorthand;
          return;
        }
        if (workEntry.leave_hrs == 4) {
          if (dayAllocEfforts > 4) {
            let leaveHrs = dayAllocEfforts - 4;
            total -= leaveHrs;
            weeklyBreakup[dayName] = "4";
          }
          return;
        }
      }else {
        weeklyBreakup[dayName] = "";
      }
    }
  });

  return (
    <WeeklyPopover
      id={id}
      groupId={groupId}
      displayContent={`${total} ${HR_ABBR}`}
      popoverContent={{
        weeklyBreakup,
        total,        
        totalLabel: `Total Allocation(${HR_ABBR})`,
      }}
    />
  );
};

export default ProjectWeeklyItem;