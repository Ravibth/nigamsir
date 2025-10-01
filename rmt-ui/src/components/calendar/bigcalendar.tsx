import React, { useContext, useEffect, useState } from "react";
import { Calendar, dateFnsLocalizer } from "react-big-calendar";
import format from "date-fns/format";
import parse from "date-fns/parse";
import startOfWeek from "date-fns/esm/fp/startOfWeek";
import getDay from "date-fns/getDay";
import "react-big-calendar/lib/css/react-big-calendar.css";
import DatePicker from "react-date-picker";
import "./styles.css";
import * as Utils from "./Utils";
import { Box, Container, Divider, Grid, Modal, Stack } from "@mui/material";
import moment from "moment";
import {
  getProjectsOfEmployeeByEmailAndPipelineCode,
  getProjectsOfEmployeeByPipelineCode,
} from "../../services/big-calendar-services/bigcalendar.service";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { IEvent } from "./IEvent";
import { getCurrentTimeZoneDate } from "../../utils/date/dateHelper";
import * as constant from "./constant";
import { useParams } from "react-router";
import { getStyleDetils } from "./Utils";

const locales = {
  "en-US": require("date-fns/locale/en-US"),
};

const localizer = dateFnsLocalizer({
  format,
  parse,

  startOfWeek,
  getDay,
  locales,
});

const Bigcalendar = (props: any) => {
  const { selectedPipelineCodes, setFilterOptions } = props;
  const [allEvents, setAllEvents] = useState<any>([]);
  const { projectDetails } = props;
  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const [currentEvent, setCurrentEvent] = useState<any>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [modalData, setModalData] = useState([]);
  const { pipelineCode, jobCode } = useParams();
  const userDetails = React.useContext(UserDetailsContext);
  const handleEventClick = (event: any) => {
    //event.preventDefault();
    setModalData(event); // Store the selected date
    setModalOpen(true); // Open the modal
  };
  const GetEventsFromDB = (
    employeeEmail: string = "",
    pipelineCode: string,
    jobCode: string
  ) => {
    return new Promise((resolve, reject) => {
      if (isEmployee) {
        getProjectsOfEmployeeByEmailAndPipelineCode(employeeEmail, "")
          .then((resp) => {
            resolve(resp.data);
          })
          .catch((err) => {
            // console.log(err);
          });
      } else {
        getProjectsOfEmployeeByPipelineCode(pipelineCode, jobCode)
          .then((resp) => {
            resolve(resp.data);
          })
          .catch((err) => {
            // console.log(err);
          });
      }
    });
  };
  useEffect(() => {
    Promise.all([
      GetEventsFromDB(userDetails.username, pipelineCode + "", jobCode),
    ]).then((values) => {
      const dbData = values[0];
      const employeeEventData = Utils.GetEventsForEmployee(dbData);
      setAllEvents((prevState: any) => [...employeeEventData]);
      setCurrentEvent((prevState: any) => [...employeeEventData]);
      if (isEmployee) {
        const pipelineCodes =
          Utils.GetDistinctPipelineCodesFromEvents(employeeEventData);
        // console.log(pipelineCodes);
        setFilterOptions(pipelineCodes);
      }
    });
  }, []);
  useEffect(() => {
    const newcurrentEvents = Utils.GetEventsFilterByPipelineCodes(
      allEvents,
      selectedPipelineCodes
    );
    setCurrentEvent(newcurrentEvents);
  }, [selectedPipelineCodes]);

  const eventStyleGetter = (
    event: any,
    start: any,
    end: any,
    isSelected: any
  ) => {
    const pipelineCodes =
      Utils.GetDistinctPipelineCodesFromEvents(currentEvent);
    var opacityVal = currentEvent.findIndex(
      (x: any) => x.pipelineCode === event.pipelineCode
    );

    opacityVal = (opacityVal + 1) / pipelineCodes.length;
    //rgb(102, 202, 211,.5)

    var _style = {
      borderRadius: "0px",
      color: "black",
      border: "0px",
      backgroundColor: getStyleDetils(event.type),
    };
    // console.log(_style);
    return {
      style: _style,
    };
  };

  return (
    <>
      <div style={{ height: "900px" }}>
        <Calendar
          // className="bigCalendarmain"
          date={props.selectedDate}
          localizer={localizer}
          events={currentEvent}
          startAccessor="start"
          endAccessor="end"
          popup={true}
          view={props.calendarCurrentView}
          // onShowMore={handleEventClick}
          // showAllEvents={false}
          eventPropGetter={eventStyleGetter}
          style={{ width: "100%", borderRadius: "0px" }}
        />
      </div>
    </>
  );
};

export default Bigcalendar;
