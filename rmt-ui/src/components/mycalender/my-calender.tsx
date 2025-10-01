/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useEffect, useState } from "react";
import { Calendar, dateFnsLocalizer } from "react-big-calendar";
import format from "date-fns/format";
import parse from "date-fns/parse";
import startOfWeek from "date-fns/esm/fp/startOfWeek";
import getDay from "date-fns/getDay";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import {
  getCurrentUserAllocationCalander,
  getCurrentUserAllocationCalanderFilterOptions,
  getProjectsOfEmployeeByEmailAndPipelineCode,
} from "../../services/big-calendar-services/bigcalendar.service";
import moment from "moment";
import * as GC from "../../global/constant";
import {
  GetDistinctPipelineCodesFromEvents,
  GetEventsForEmployee,
  getCalendarDropdownOptions,
  getStyleDetils,
  mapFilterParams,
} from "./util";
import { IGetUserAllocationCalander } from "../../services/big-calendar-services/IGetUserAllocationCalanderFilterOptions";
import { IEvent } from "./IEvent";

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

export interface IGetUserAllocationCalanderFilterOptions {
  pipelineCodes: string[] | null;
  jobCodes: string[] | null;
}

const MyCalender = (props: any) => {
  const { selectedPipelineCodes } = props;
  const [currentEvent, setCurrentEvent] = useState<any>([]);
  const userDetails = React.useContext(UserDetailsContext);
  const [modalOpen, setModalOpen] = useState(false);
  const [modalData, setModalData] = useState([]);

  const GetUserAllocations = (employeeEmail: string) => {
    getProjectsOfEmployeeByEmailAndPipelineCode(employeeEmail)
      .then((resp) => {
        console.log(resp.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const handleEventClick = (event: any) => {
    //event.preventDefault();
    setModalData(event); // Store the selected date
    setModalOpen(true); // Open the modal
  };

  const GetUserAllocationCalander = (
    startDate: string,
    endDate: string,
    selectedFilterOptions: any[]
  ) => {
    return new Promise((resolve, reject) => {
      const paramsFilter = mapFilterParams(selectedFilterOptions);
      const request: IGetUserAllocationCalander = {
        startDate: startDate,
        endDate: endDate,
        filterParameters: paramsFilter,
      };
      getCurrentUserAllocationCalander(request)
        .then((resp) => {
          resp.data.sort((a, b) => {
            return a.id - b.id;
          });
          resolve(resp.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };
  const GetUserAllocationCalanderFilterOptions = () => {
    return new Promise((resolve, reject) => {
      getCurrentUserAllocationCalanderFilterOptions()
        .then((resp) => {
          resolve(resp.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };
  useEffect(() => {
    if (userDetails.username) {
      const startDate = moment(props.selectedDate)
        .add(-1, "month")
        .format(GC.dateFormatYMD);
      const endDate = moment(props.selectedDate)
        .add(1, "month")
        .format(GC.dateFormatYMD);
      Promise.all([
        GetUserAllocationCalander(startDate, endDate, selectedPipelineCodes),
      ])
        .then((values) => {
          const dbData = values[0];
          const employeeEventData = GetEventsForEmployee(dbData);
          setCurrentEvent((prevState: any) => [...employeeEventData]);
        })
        .catch((err) => {
          throw err;
        });
    }
  }, [userDetails, props.selectedDate, selectedPipelineCodes]);

  useEffect(() => {
    if (userDetails.username) {
      Promise.all([GetUserAllocationCalanderFilterOptions()])
        .then((values) => {
          const filterDbData: IGetUserAllocationCalanderFilterOptions =
            values[0] as IGetUserAllocationCalanderFilterOptions;
          const options = getCalendarDropdownOptions(values[0]);
          props.setFilterOptions(options);
          // props.setFilterOptions(filterDbData.pipelineCodes);
        })
        .catch((err) => {
          throw err;
        });
    }
  }, [userDetails]);

  const eventStyleGetter = (
    event: IEvent,
    start: any,
    end: any,
    isSelected: any
  ) => {
    //console.log(event);
    const pipelineCodes = GetDistinctPipelineCodesFromEvents(currentEvent);

    const pipelineIndex = pipelineCodes.findIndex(
      (code: string) => code === event.pipelineCode
    );

    const opacityVal = 1 - (pipelineIndex / pipelineCodes.length) * 0.5;

    var _style = {
      borderRadius: "0px",
      color: "black",
      border: "0px",
      backgroundColor: getStyleDetils(event.type),
      opacity: opacityVal, // Opacity of the background
      display: "block",
    };
    return {
      style: _style,
    };
  };

  return (
    <Calendar
      className="bigCalendarmain"
      date={props.selectedDate}
      localizer={localizer}
      events={currentEvent}
      startAccessor="start"
      endAccessor="end"
      popup={true}
      view={props.calendarCurrentView}
      onShowMore={handleEventClick}
      eventPropGetter={eventStyleGetter}
      style={{ height: 700, width: "100%", borderRadius: "0px" }}
    />
  );
};

export default MyCalender;
