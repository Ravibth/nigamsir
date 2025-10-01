import moment from "moment";

export const isDate = (str: any) => {
  return isValidDate(str);
};

/**  Below Function validate date format.
 @param {*} str : date in string format
@returns { isDateValid, formateDAte }
**/
export function isValidDate(str: any) {
  const dateformat = ["YYYY-MM-DD", "DD-MM-YYYY", "DD-MMM-YY", "D-M-YY"];
  let dateCount = 0;
  let formateDAte;
  dateformat.forEach((item) => {
    const d2: any = moment(str, item, true);
    if (d2 != null && d2.isValid()) {
      formateDAte = moment(str, item).format("YYYY-MM-DD");
      dateCount = 1;
    }
  });

  if (dateCount) {
    return { isDateValid: true, formateDAte: formateDAte };
  } else {
    return { isDateValid: false, formateDAte: "" };
  }
}

export const getCurrentTimeZoneDate = (date: Date): Date => {
  const storedDate = new Date(date);
  const offsetInMinutes = new Date().getTimezoneOffset();
  const localDate = new Date(
    storedDate.getTime() - offsetInMinutes * 60 * 1000
  );
  return localDate;
};

export const dateFilterParams = {
  suppressAndOrCondition: true,
  comparator: (filterLocalDateAtMidnight: any, cellValue: any) => {
    const dateAsString = cellValue;
    const changedCreatedDate = moment(dateAsString, "YYYY-MM-DD").format(
      "DD-MM-YYYY"
    );
    if (dateAsString == null) {
      return 0;
    }
    const dateParts = changedCreatedDate.split("-");
    const year = Number(dateParts[2]);
    const month = Number(dateParts[1]) - 1;
    const day = Number(dateParts[0]);
    const cellDate = new Date(year, month, day);
    if (cellDate < filterLocalDateAtMidnight) {
      return -1;
    } else if (cellDate > filterLocalDateAtMidnight) {
      return 1;
    }
    return 0;
  },
  browserDatePicker: true,
};

export const calculateDaysBetweenTwoDates = (
  start_date: Date,
  end_date: Date
) => {
  const start: any = moment(new Date(start_date).setHours(0, 0, 0, 0));
  const end: any = moment(new Date(end_date).setHours(0, 0, 0, 0));
  var totalDays = end.diff(start, "days") + 1;

  // const totalDays = Math.abs(end.diff(start, "days")) + 1;
  let weekdaysCount = 0;
  let startingDate = moment(new Date(start_date));

  for (let i = 0; i < totalDays; i++) {
    const dayOfWeek = startingDate.weekday();
    if (dayOfWeek !== 0 && dayOfWeek !== 6) {
      weekdaysCount++;
    }
    startingDate.add(1, "day");
  }
  return weekdaysCount;
};

export const getDateInMomentFormat = (
  date: Date,
  dateformat: string = "YYYY-MM-DD"
) => {
  return moment(new Date(date)).format(dateformat);
};

export const getDateAfterAddingHoursAndMinInMomentFormat = (
  date: Date,
  hours: any,
  dateformat: string = "YYYY-MM-DD"
) => {
  return moment(new Date(date)).format(dateformat);
};

export const getDateWithEndHours = (
  date: Date,
  dateformat: string = "YYYY-MM-DD"
) => {
  let _dt = moment(getDateInMomentFormat(date, dateformat))
    .add(23, "hours")
    .add(59, "minutes")
    .add(55, "second");
  return _dt;
};
// moment(getDateInMomentFormat(new Date(item.endDate)))
//             .add(23, "hours")
//             .add(59, "minutes")
//             .add(55, "second"),

export const getDateWithoutTime = (inputDate: Date) => {
  var date = new Date(inputDate);
  date.setHours(0, 0, 0, 0);
  return date;
};

export const getLocalDateInMomentFormat = (
  date: Date,
  dateformat: string = "YYYY-MM-DD"
) => {
  const nextDay = moment(date).add(1, "day");
  return nextDay.format(dateformat);
};

export const GetNewDateWithNoonTimeZone = (
  date?: Date | string | number
): Date => {
  if (date != null && moment(new Date(date)).isValid) {
    const today = new Date(date);
    today.setHours(12, 0, 0, 0);
    return today;
  } else {
    const today = new Date();
    today.setHours(12, 0, 0, 0);
    return today;
  }
};

const _monthToNum = (date) => {
  if (date === undefined || date === null || date.length !== 10) {
    return null;
  }
  const yearNumber = date.substring(6, 10);
  const monthNumber = date.substring(3, 5);
  const dayNumber = date.substring(0, 2);
  const result = yearNumber * 10000 + monthNumber * 100 + dayNumber;
  return result;
};

export const DateComparatorForSorting = (date1, date2) => {
  const date1Number = _monthToNum(date1);
  const date2Number = _monthToNum(date2);

  if (date1Number === null && date2Number === null) {
    return 0;
  }
  if (date1Number === null) {
    return -1;
  }
  if (date2Number === null) {
    return 1;
  }

  return date1Number - date2Number;
};

export const GetDateAsIsoString = (payload?: Date): string => {
  if (payload && moment(payload).isValid()) {
    const date = new Date(payload);
    date.setHours(12, 0, 0, 0);
    return new Date(date).toISOString();
  } else {
    return new Date().toISOString();
  }
};

export const IndianNumberFormatter = (number: number): string => {
  const formatter = new Intl.NumberFormat("en-IN");
  return formatter.format(number);
};

export const SmallDateFormatter = (date: Date): string => {
  const formatter = new Intl.DateTimeFormat("en-US", {
    month: "long",
    year: "numeric",
  });
  return formatter.format(new Date(date));
};

export const disableWeekends = (date) => {
  const day = date.getDay();
  return day === 0 || day === 6;
};

//Convert string date using moment date object
export const ConvertToMomentDate = (strDate: string, addDays: number) => {
  var _dt = new Date(moment(strDate).add(addDays, "day").toDate());
  return _dt;
};

//Convert string date using moment with format
export const ConvertToMomentDateUsingFormat = (
  strDate: string,
  format: string,
  addDays: number
) => {
  var _dt = new Date(
    moment(strDate, "DD-MM-YYYY HH:mm:ss").add(addDays, "day").format("")
  );
  return _dt;
};

//Convert string date using moment utc object
export const ConvertToMomentUTCDate = (strDate: any) => {
  var _dt = new Date(moment.utc(strDate, "DD-MM-YYYY HH:mm:ss").format(""));
  return _dt;
};

//Convert to Date Object using local date string
export const ConvertToDateLocalDateString = (strDate: string) => {
  var _dt = new Date(strDate).toLocaleDateString("en-US", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });
  return _dt;
};

export const ConvertProjectDate = (strDate: string) => {
  var _dt = moment(strDate).format();
  return _dt;
};

export const ConvertDateByFormat = (strDate: string, _format: string) => {
  var _dt = moment(strDate, _format).format();
  return _dt;
};

export const ConvertAllocationDate = (strDate: string) => {
  var _dt = moment(strDate, "DD-MM-YYYY hh:mm:ss a").format();
  return _dt;
};

export const ConvertTimeLineDate = (strDate: any) => {
  var _dt = moment(strDate).format();
  return _dt;
};

export const MomentAddDays = (strDate: string | Date, addDays: number) => {
  var _dt = moment(strDate).add(addDays, "day").format();
  return _dt;
};

export const MomentToJSDate = (strDate: string) => {
  var _dt = new Date(moment(strDate, "YYYY-MM-DD hh:mm:ss").toDate());
  return _dt;
};

export const ConvertMomentToFormat = (strDate: string, _format: string) => {
  var _dt = moment.utc(strDate).format();
  return _dt;
};

export const GetPrevCurrentValidDate = () => {
  const currentDate = new Date();
  if (currentDate.getDay() === 6) {
    return GetNewDateWithNoonTimeZone(new Date(MomentAddDays(currentDate, -1)));
  }
  if (currentDate.getDay() === 0) {
    return GetNewDateWithNoonTimeZone(new Date(MomentAddDays(currentDate, -2)));
  }
  return GetNewDateWithNoonTimeZone(currentDate);
};

export const GetNextCurrentValidDate = (date?: Date | string) => {
  const currentDate = date ? new Date(date) : new Date();
  if (currentDate.getDay() === 6) {
    return GetNewDateWithNoonTimeZone(new Date(MomentAddDays(currentDate, 2)));
  }
  if (currentDate.getDay() === 0) {
    return GetNewDateWithNoonTimeZone(new Date(MomentAddDays(currentDate, 1)));
  }
  return GetNewDateWithNoonTimeZone(currentDate);
};
