import React, { useContext, useEffect, useState } from "react";
import ReacTimelineCalendar from "../../../components/scheduler/react-time-calendar/react-timeline-calendar";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { IPaginationData } from "../../scheduler/interface/timelineprops";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import { ItemType } from "../../scheduler/react-time-calendar/constant";
import { IWeeklyBreakupProps } from "../../calendar/Utils";
import WeeklyBreakupPopover from "../../common-allocation/weeklyBreakup/weeklyBreakupPopover";

function RequestorProjectList(props: any) {
  const pageLimitConst = props.pageLimitConst;
  const [currentView, setCurrentView] = useState("month");
  const userDetails = useContext(UserDetailsContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [isEmployee, setIsEmployee] = useState<boolean>(false);
  const [showWeeklyData, setShowWeeklyData] = useState<boolean>(false);
  const [weeklyData, setWeeklyData] = useState<IWeeklyBreakupProps>(null);
  const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);

  const [paginationData, setPaginationData] = useState<IPaginationData>({
    limit: pageLimitConst,
    currentPageNumber: 1,
    nextPageNumber: 2,
  });
  const [projectSearchText, setProjectSearchText] = useState("");

  const getPaginationDataFunc = () => {
    return paginationData;
  };
  const setPaginationDataFunc = (data: IPaginationData) => {
    if (props.searchQuery === undefined || props.searchQuery == null) {
      return setPaginationData(data);
    }
  };

  useEffect(() => {
    if (userDetails) {
      setIsEmployee(userDetails.isEmployee);
    }
  }, [userDetails?.isEmployee]);

  useEffect(() => {
    setCurrentView(props.currentView);
  }, [props.currentView]);

  useEffect(() => {
    loaderContext.open(true);
    if (props.searchQuery) {
      setPaginationData({
        limit: pageLimitConst,
        currentPageNumber: 1,
        nextPageNumber: 2,
      });
      setProjectSearchText(props.searchQuery);
    } else {
      setProjectSearchText("");
    }
  }, [props.searchQuery]);
  const onInitialPaginationData = (e: boolean) => {
    if (e) {
      setPaginationData({
        limit: pageLimitConst,
        currentPageNumber: 1,
        nextPageNumber: 2,
      });
    }
  };

  const handlePopoverClose = () => {
    setAnchorEl(null);
  };

  function IsWeeklyBreakUpAllowed(item: any) {
    return (
      item.RowType?.toLowerCase() === ItemType.Allocation?.toLowerCase() ||
      // item.RowType?.toLowerCase() === ItemType.Leave?.toLowerCase() ||
      item.RowType?.toLowerCase() === ItemType.FULL_DAY_LEAVE?.toLowerCase() ||
      item.RowType?.toLowerCase() ===
        ItemType.FIRST_HALF_LEAVE?.toLowerCase() ||
      item.RowType?.toLowerCase() ===
        ItemType.SECOND_HALF_LEAVE?.toLowerCase() ||
      item.RowType?.toLowerCase() === ItemType.Holiday?.toLowerCase()
    );
  }

  const onMouseEnterHandler = (e, a, item: any) => {
    // console.log("onMouseEnterHandler e", e);
    if (IsWeeklyBreakUpAllowed(item)) {
      setShowWeeklyData(true);
      var data: IWeeklyBreakupProps = {
        breakup: item.weeklyBreakup,
        weeklyTotal: item.weeklyTotal,
      };
      setWeeklyData(data);
      setAnchorEl(e.target);
    }
  };

  const onMouseLeaveHandler = (e, a, item: any) => {
    // console.log("onMouseLeaveHandler e", e);
    if (IsWeeklyBreakUpAllowed(item)) {
      setShowWeeklyData(false);
      setWeeklyData(null);
      setAnchorEl(null);
    }
  };

  const CustomItemRenderer = ({
    item,
    itemContext,
    getItemProps,
    getResizeProps,
  }) => {
    const { left: leftResizeProps, right: rightResizeProps } = getResizeProps();
    return (
      <>
        <div
          onMouseEnter={(e, a) => onMouseEnterHandler(e, a, item)}
          onMouseLeave={(e, a) => onMouseLeaveHandler(e, a, item)}
          {...getItemProps(item.itemProps)}
        >
          {itemContext.useResizeHandle ? <div {...leftResizeProps} /> : ""}
          <div
            className="rct-item-content"
            style={{ maxHeight: `${itemContext.dimensions.height}` }}
          >
            {item.title}
          </div>
          {itemContext.useResizeHandle ? <div {...rightResizeProps} /> : ""}
        </div>
      </>
    );
  };

  return (
    <div className="div-ReacTimelineCalendar-container">
      {showWeeklyData === true && (
        <WeeklyBreakupPopover
          anchorEl={anchorEl}
          handlePopoverClose={handlePopoverClose}
          weeklyData={weeklyData}
          totalPlaceHolderText={"Allocation Total (hrs) - "}
        ></WeeklyBreakupPopover>
      )}
      <ReacTimelineCalendar
        selectedDate={props.selectedDate}
        SetupFilterDefaultValue={props.SetupFilterDefaultValue}
        submittedFilterData={props.submittedFilterData}
        handleFilterData={props.handleFilterData}
        isEmployee={isEmployee}
        currentView={currentView}
        {...props}
        userDetails={userDetails}
        searchRoles={props.searchRoles}
        getPaginationDataFunc={getPaginationDataFunc}
        setPaginationDataFunc={setPaginationDataFunc}
        searchQuery={projectSearchText}
        onInitialPaginationData={(e) => {
          onInitialPaginationData(e);
        }}
        pageLimitConst={pageLimitConst}
        loaderContext={loaderContext}
        itemRenderer={CustomItemRenderer}
      />
    </div>
  );
}
export default RequestorProjectList;
