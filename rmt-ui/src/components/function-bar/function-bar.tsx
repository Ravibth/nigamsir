import React, { useEffect, useState } from "react";
import { Grid, Tooltip } from "@mui/material";
import CurrentDateComp from "../../common/currentdate/currentdate";
import FilterComp from "../filter/project-filter/filter";
import Groupbtnsliderlayout from "../groupbtn-slider-layout/groupbtn-slider-layout";
import * as constant from "../../global/constant";
import HomeSearch from "./home-search";
import RoleSearch from "./role-search";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import { VerticalCenterAlignSxProps } from "../common-allocation/user-info-timeline-group/style";

const FunctionBar = (props: any) => {
  const [openDrawer, setOpenDrawer] = useState(false);

  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);
  const checkIfIsFilterApplied = () => {
    if (
      props.submittedFilterData &&
      Object.values(props.submittedFilterData).find(
        (v: any) => v && v.length > 0
      )
    ) {
      setIsFilterApplied(true);
      return true;
    } else {
      setIsFilterApplied(false);
      return false;
    }
  };

  useEffect(() => {
    checkIfIsFilterApplied();
  }, [openDrawer]);

  const handleSchedulerViewChange = (schedulerView: any) => {
    props.handleSchedulerView(schedulerView);
  };
  const currentDateHandleClick = (date: any) => {
    props.selectDateHandler(date);
    props.handleSchedulerView(constant.CALENDAR_VIEW_TYPE.TimeLineToday);
  };
  const handlePreviousClick = () => {
    props.handlePreviousClick();
  };
  const handleNextClick = () => {
    props.handleNextClick();
  };

  const searchQueryHandle = (data: any) => {
    props.searchQueryHandle(data);
  };
  return (
    <Grid container>
      <Grid item xs={8} md={8}>
        <Grid container spacing={{ xs: 1, md: 1 }}>
          <Grid item>
            <FilterComp
              filterData={props.filterData}
              handleResetClick={props.handleResetClick}
              defaultValue={props.defaultValue}
              openDrawer={openDrawer}
              setOpenDrawer={setOpenDrawer}
              selectedDataByFilter={(e) => props.selectedDataByFilter(e)}
            />
          </Grid>
          {/* {isFilterApplied && (
            <Grid item sx={VerticalCenterAlignSxProps}>
              <Tooltip title="Filters applied"> 
                <FilterAltIcon style={{ marginTop: "8px" }} fontSize="large" />
                
              </Tooltip>
            </Grid>
          )} */}

          <Grid item>
            <HomeSearch
              searchQueryHandle={(e) => {
                searchQueryHandle(e);
              }}
            />
          </Grid>
          <Grid item>
            <RoleSearch setSearchRoles={props.setSearchRoles} />
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={4} md={4}>
        <Grid container spacing={{ xs: 1, sm: 1 }} justifyContent={"flex-end"}>
          <Grid item>
            <CurrentDateComp
              currentDateHandleClick={currentDateHandleClick}
              handlePreviousClick={handlePreviousClick}
              handleNextClick={handleNextClick}
            />
          </Grid>
          <Grid item>
            <Groupbtnsliderlayout
              handleSchedulerViewChange={handleSchedulerViewChange}
            />
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
};
export default FunctionBar;
