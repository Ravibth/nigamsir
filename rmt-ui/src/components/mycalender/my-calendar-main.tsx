import React, { useState } from "react";
import * as GC from "../../global/constant";
import MyCalender from "./my-calender";
import FunctionBarCalendar from "../calendar-function-bar/calendar-function-bar";
import MyCalendarFunctionBar from "./my-calendar-function-bar";
import "./style.css";

const MyCalendarMain = () => {
  const [currentView, setCurrentView] = useState("month");
  const [isClickToday, setIsClickToday] = useState(false);
  const [isClickPreviouorNext, setIsClickPreviouorNext] = useState("");
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [selectedPipelineCodes, setSelectedPipelineCodes] = useState([]);
  const [filterOptions, setFilterOptions] = useState([]);

  const handlePreviousClick = () => {
    // setIsClickToday(false);
    // setIsClickPreviouorNext(GC.NEXTPRE_CLICK.Pre);
    //setSelectedDate(new Date(selectedDate).getMonth)
    const nextDate = new Date(selectedDate);
    nextDate.setMonth(selectedDate.getMonth() - 1);
    setSelectedDate(nextDate);
  };

  const handleNextClick = () => {
    const nextDate = new Date(selectedDate);
    nextDate.setMonth(selectedDate.getMonth() + 1);
    setSelectedDate(nextDate);
  };

  const selectDateHandler = (date: Date) => {
    const currentDate = new Date();
    setSelectedDate(currentDate);
  };
  const handleSchedulerView = (view: any) => {
    setIsClickToday(false);
    setIsClickPreviouorNext(GC.NEXTPRE_CLICK.None);
    switch (view) {
      case GC.CALENDAR_VIEW_TYPE.TimeLineMonth:
        setCurrentView(GC.CALENDAR_VIEW.Month);
        break;

      case GC.CALENDAR_VIEW_TYPE.TimeLineToday:
        setIsClickToday(true);
        break;
      default:
        setCurrentView(GC.CALENDAR_VIEW.Year);
        break;
    }
  };
  return (
    <>
      <div className="my-calendar-main-container">
        <MyCalendarFunctionBar
          handleSchedulerView={handleSchedulerView}
          handlePreviousClick={handlePreviousClick}
          handleNextClick={handleNextClick}
          selectDateHandler={selectDateHandler}
          setSelectedPipelineCodes={setSelectedPipelineCodes}
          filterOptions={filterOptions}
        />
        {/* <FunctionBarCalendar
        handleSchedulerView={handleSchedulerView}
        handlePreviousClick={handlePreviousClick}
        handleNextClick={handleNextClick}
        selectDateHandler={selectDateHandler}
        setSelectedPipelineCodes={setSelectedPipelineCodes}
        filterOptions={filterOptions}
      /> */}
        <MyCalender
          currentView={currentView}
          clickToday={isClickToday}
          handlePreviousorNextClick={isClickPreviouorNext}
          setPreviousorNextNone={() => {
            setIsClickPreviouorNext(GC.NEXTPRE_CLICK.None);
          }}
          selectedPipelineCodes={selectedPipelineCodes}
          selectedDate={selectedDate}
          setFilterOptions={setFilterOptions}
        />
      </div>
    </>
  );
};

export default MyCalendarMain;
