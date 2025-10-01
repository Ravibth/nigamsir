import React, { Fragment, useState } from "react";
import Bigcalendar from "../../calendar/bigcalendar";
import { Container } from "@mui/material";
import FunctionBarCalendar from "../../calendar-function-bar/calendar-function-bar";
//import BackButton from '../backbutton/backbutton'
import * as GC from "../../../global/constant";
import "./requestor-calendar-view.css";

const RequestorCalendarView = (props: any) => {
  const [currentView, setCurrentView] = useState("month");
  const [isClickToday, setIsClickToday] = useState(false);
  const [isClickPreviouorNext, setIsClickPreviouorNext] = useState("");
  const [selectedDate, setSelectedDate] = useState(new Date());

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
    // console.log(date)
    const currentDate = new Date();
    //console.log("current date inside requestor ", currentDate);
    setSelectedDate(currentDate);
  };

  return (
    <Fragment>
      <Container className="main-calendar-container">
        {/* <BackButton /> */}
        <FunctionBarCalendar
          handleSchedulerView={handleSchedulerView}
          handlePreviousClick={handlePreviousClick}
          handleNextClick={handleNextClick}
          selectDateHandler={selectDateHandler}
        />
        <Bigcalendar
          currentView={currentView}
          clickToday={isClickToday}
          handlePreviousorNextClick={isClickPreviouorNext}
          setPreviousorNextNone={() => {
            setIsClickPreviouorNext(GC.NEXTPRE_CLICK.None);
          }}
          selectedPipelineCodes={[]}
          selectedDate={selectedDate}
        />
      </Container>
    </Fragment>
  );
};

export default RequestorCalendarView;
