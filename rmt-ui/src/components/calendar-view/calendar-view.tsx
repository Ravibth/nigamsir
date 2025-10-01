import React, { Fragment, useState } from "react";
import Bigcalendar from "../calendar/bigcalendar";
import { Container } from "@mui/material";
//import BackButton from '../backbutton/backbutton'
import FunctionBarCalendar from "../calendar-function-bar/calendar-function-bar";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import BackButton from "../backbutton/backbutton";
import * as GC from "../../global/constant";

const CalendarView = () => {
  const [currentView, setCurrentView] = useState("month");
  const [isClickToday, setIsClickToday] = useState(false);
  const [isClickPreviouorNext, setIsClickPreviouorNext] = useState("");
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [selectedPipelineCodes, setSelectedPipelineCodes] = useState([]);
  const [filterOptions, setFilterOptions] = useState([]);
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
    setSelectedDate(currentDate);
  };

  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  return (
    <Fragment>
      <Container maxWidth="xl">
        {isEmployee && <BackButton />}
        <FunctionBarCalendar
          handleSchedulerView={handleSchedulerView}
          handlePreviousClick={handlePreviousClick}
          handleNextClick={handleNextClick}
          selectDateHandler={selectDateHandler}
          setSelectedPipelineCodes={setSelectedPipelineCodes}
          filterOptions={filterOptions}
        />
        <Bigcalendar
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
      </Container>
    </Fragment>
  );
};

export default CalendarView;