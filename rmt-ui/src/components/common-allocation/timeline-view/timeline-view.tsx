import React, { useEffect, useState } from "react";
import Timeline, {
  TimelineHeaders,
  SidebarHeader,
  DateHeader,
} from "react-calendar-timeline";
import { ITimelineViewProps } from "./interface";
import { Checkbox, Grid, Tooltip, Typography } from "@mui/material";
import { TimelineSxProps } from "../../system-suggestions/availability-view/constants";
import "./style.css";
import { IWeeklyBreakupProps } from "../../calendar/Utils";
import WeeklyBreakupPopover from "../weeklyBreakup/weeklyBreakupPopover";
import * as util from "./../../../components/scheduler/react-time-calendar/util";
import moment from "moment";
import CurrentDateComp from "../../../common/currentdate/currentdate";

const TimelineView = (props: ITimelineViewProps) => {
  const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);
  const [showWeeklyData, setShowWeeklyData] = useState<boolean>(false);
  const [weeklyData, setWeeklyData] = useState<IWeeklyBreakupProps>(null);

  const [timelineStart, setTimelineStart] = useState<Date | moment.Moment>(
    props?.defaultTimeStart
  );
  const [timelineEnd, setTimelineEnd] = useState<Date | moment.Moment>(
    props?.defaultTimeEnd
  );
  const timelineDefaultUnit = "month";
  const [dateHeaderUnit, setDateHeaderUnit] =
    useState<any>(timelineDefaultUnit);

  useEffect(() => {
    currentDateHandleClick();
  }, [props?.defaultTimeStart, props?.defaultTimeEnd]);

  const handlePopoverClose = () => {
    setAnchorEl(null);
  };

  const onMouseEnterHandler = (e, a, item: any) => {
    setShowWeeklyData(true);
    var data: IWeeklyBreakupProps = {
      breakup: item?.meta?.weeklyBreakup,
      weeklyTotal: item?.meta?.weeklyTotal,
    };
    setWeeklyData(data);
    setAnchorEl(e.target);
  };

  const onMouseLeaveHandler = (e, a, item: any) => {
    setShowWeeklyData(false);
    setWeeklyData(null);
    setAnchorEl(null);
  };

  const itemRenderer = ({
    item,
    itemContext,
    getItemProps,
    getResizeProps,
  }) => {
    const { left: leftResizeProps, right: rightResizeProps } = getResizeProps();
    const { title, ...allItemProps } = getItemProps(item.itemProps);
    return (
      <div
        onMouseEnter={(e, a) => onMouseEnterHandler(e, a, item)}
        onMouseLeave={(e, a) => onMouseLeaveHandler(e, a, item)}
        key={allItemProps?.key}
        {...allItemProps}
      >
        {itemContext.useResizeHandle && <div {...leftResizeProps} />}

        <div
          className="rct-item-content"
          style={{
            maxHeight: `${itemContext.dimensions.height}`,
          }}
        >
          {item.title}
        </div>

        {itemContext.useResizeHandle && <div {...rightResizeProps} />}
      </div>
    );
  };

  const handleTimeChange = (
    visibleTimeStart,
    visibleTimeEnd,
    updateScrollCanvas,
    unit
  ) => {
    const vStart = moment(visibleTimeStart).format("DD-MM-YYYY");
    const vEnd = moment(visibleTimeEnd).add(-1, "second").format("DD-MM-YYYY");
    const invalidUnits = ["second", "minute", "hour", "day"];
    if (vStart !== vEnd && !invalidUnits.some((a) => a == unit)) {
      updateScrollCanvas(visibleTimeStart, visibleTimeEnd);
    } else if (vStart !== vEnd && invalidUnits.some((a) => a == unit)) {
      setDateHeaderUnit(timelineDefaultUnit);
      updateScrollCanvas(timelineStart.valueOf(), timelineEnd.valueOf());
    } else {
      return;
    }
  };

  const currentDateHandleClick = () => {
    const currentDate = new Date();
    const { startDate, endDate } = util.handleSelectedDateClick(
      currentDate,
      timelineDefaultUnit
    );
    setTimelineStart(startDate);
    setTimelineEnd(endDate);
  };

  const onPrevClick = () => {
    const zoom =
      moment(timelineEnd).valueOf() - moment(timelineStart).valueOf();
    setTimelineStart(moment(moment(timelineStart).valueOf() - zoom));
    setTimelineEnd(moment(moment(timelineEnd).valueOf() - zoom));
  };

  const onNextClick = () => {
    const zoom =
      moment(timelineEnd).valueOf() - moment(timelineStart).valueOf();
    setTimelineStart(moment(moment(timelineStart).valueOf() + zoom));
    setTimelineEnd(moment(moment(timelineEnd).valueOf() + zoom));
  };

  useEffect(() => {
    if (props.defaultTimeStart) {
      const { startDate, endDate } = util.handleSelectedDateClick(
        props.defaultTimeStart as Date,
        timelineDefaultUnit
      );
      setTimelineStart(startDate);
      setTimelineEnd(endDate);
    }
  }, [props.defaultTimeStart]);

  return (
    <Typography
      sx={TimelineSxProps}
      className="common-screen-timeline-view"
      component={"div"}
    >
      {showWeeklyData === true && (
        <WeeklyBreakupPopover
          anchorEl={anchorEl}
          handlePopoverClose={handlePopoverClose}
          weeklyData={weeklyData}
          totalPlaceHolderText={"Available Total (hrs) - "}
        ></WeeklyBreakupPopover>
      )}
      <Grid
        container
        direction="column"
        alignItems="center"
        justifyContent="center"
      >
        <Grid item xs={12} paddingBottom={1}>
          <CurrentDateComp
            currentDateHandleClick={currentDateHandleClick}
            handlePreviousClick={onPrevClick}
            handleNextClick={onNextClick}
          />
        </Grid>
      </Grid>
      <Timeline
        canResize="both"
        groups={props.timelineGroups}
        items={props.timelineItems}
        stackItems={true}
        itemHeightRatio={0.75}
        sidebarWidth={310}
        buffer={1}
        onItemDoubleClick={(item: any, e: any, time: any) => {
          props.onItemClick(item, e, time);
        }}
        itemRenderer={itemRenderer}
        defaultTimeStart={timelineStart}
        // defaultTimeEnd={timelineEnd}
        visibleTimeStart={timelineStart}
        visibleTimeEnd={timelineEnd}
        onTimeChange={handleTimeChange}
      >
        <TimelineHeaders className={"sticky"}>
          <SidebarHeader>
            {({ getRootProps }) => {
              return (
                <div {...getRootProps()}>
                  {props.showCheckbox && (
                    <div style={{ marginTop: "12px" }}>
                      <Tooltip
                        title={
                          props.allUsersSelected ? "Deselect all" : "Select all"
                        }
                      >
                        <Checkbox
                          checked={props.allUsersSelected}
                          disabled={props.timelineGroups.length === 0}
                          onClick={(e) => {
                            props.onCheckBoxChange(e);
                          }}
                        />
                      </Tooltip>
                    </div>
                  )}
                </div>
              );
            }}
          </SidebarHeader>
          <DateHeader unit={dateHeaderUnit} />
          <DateHeader />
        </TimelineHeaders>
      </Timeline>
    </Typography>
  );
};
export default TimelineView;
