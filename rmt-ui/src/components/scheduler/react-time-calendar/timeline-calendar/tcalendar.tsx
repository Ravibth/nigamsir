import { Divider, Grid } from "@mui/material";
import React from "react";
import Timeline, {
  TimelineHeaders,
  SidebarHeader,
  DateHeader,
} from "react-calendar-timeline";
import "react-calendar-timeline/lib/Timeline.css";
import { TimelineProps } from "../../interface/timelineprops";
import { TimelineSate } from "../../interface/timelinestate";
import * as constant from "../constant";
import "../timelinestyle.css";
import ProjectTypesComp from "../../../project-types/projecttypescomp";
import Allproject from "../../../allprojects/allproject";
import "./styles.css";
import UpdateAllocationCommonScreen from "../../../common-allocation/update-allocation-common-screeen/update-allocation-common-screeen";
import { ENavigatingFromToUpdateCommonScreen } from "../../../common-allocation/enum";

export class tcalendar extends React.Component<TimelineProps, TimelineSate> {
  public ctrlTimelineContainerRef: React.RefObject<any>;

  handlePreviousClick = (e: any) => {};
  public projectsCount = 0;
  public selectCount = 0;
  public handleNextClick = () => {};
  public handleItemResize = (itemId: any, time: any, edge: any) => {};
  public handleProjectTypeClick = (projectType: string) => {};
  public getProjectCount = (projectCount: number) => {
    this.projectsCount = projectCount;
  };
  public getChargeType = (selectCount: number) => {
    this.selectCount = selectCount;
  };
  public onItemClick = (itemId: any, e: any, time: any) => {
    this.props.onItemClick(itemId, true);
  };
  public onCloseAllocationModel = (itemId: number) => {};

  public handleLoadMoreDataClick = () => {
    // console.log("handleLoadMoreDataClick--tcal");
  };

  render() {
    return (
      <>
        {this.state.groups.length === 0 &&
          this.props.loaderContext.isOpen === false && (
            <div
              style={{
                width: "100%",
                textAlign: "center",
                fontSize: "16px",
                fontWeight: "600",
                justifyContent: "center",
              }}
            >
              No records to display!
            </div>
          )}
        <span
          onClick={this.handleLoadMoreDataClick}
          style={{ cursor: "pointer", display: "none" }}
        >
          Load More
        </span>
        <div
          id="timelineCalendar"
          className="time-calender-main div-Timeline-container"
          ref={this.ctrlTimelineContainerRef}
        >
          {this.state.isCalendarRefresh && (
            <Timeline
              // canResize={"both"}
              groups={this.state.groups}
              items={this.state.items}
              visibleTimeStart={this.state.startDate}
              visibleTimeEnd={this.state.endDate}
              stackItems
              //itemHeightRatio={0.75}
              sidebarWidth={310}
              onItemResize={this.handleItemResize}
              itemRenderer={this.props.itemRenderer}
              buffer={1}
              onItemDoubleClick={(item: any, e: any, time: any) => {
                this.onItemClick(item, e, time);
              }}
            >
              <TimelineHeaders className="sticky">
                <SidebarHeader>
                  {({ getRootProps }) => {
                    return (
                      <div className="time-calender-main" {...getRootProps()}>
                        <Grid
                          container
                          rowSpacing={1}
                          columnSpacing={{ xs: 1, sm: 2, md: 3 }}
                        >
                          <Grid item xs={12}>
                            <ProjectTypesComp
                              className={"project-type-main"}
                              handleProjectTypeClick={
                                this.handleProjectTypeClick
                              }
                              selectProjectType={this.selectCount}
                            />
                            <Divider sx={{ margin: "-5px" }} />
                          </Grid>
                          <Grid item xs={12}>
                            <Allproject projectCount={this.projectsCount} />
                          </Grid>
                        </Grid>
                      </div>
                    );
                  }}
                </SidebarHeader>
                <DateHeader unit={constant.primaryHeader} />
                <DateHeader />
              </TimelineHeaders>
            </Timeline>
          )}
          {this.state.itemId !== 0 && this.state.isAllocationEmployeeShow && (
            <UpdateAllocationCommonScreen
              isModalOpen={true}
              handleCloseModal={(itemId: number) => {
                this.onCloseAllocationModel(itemId);
              }}
              projectInfo={null}
              requisitionIds={[this.state.itemId]}
              pipelineCode={this.state.pipelineCode}
              jobCode={this.state.jobCode}
              navigatingFrom={
                ENavigatingFromToUpdateCommonScreen.PROJECT_LISTING
              }
            />
          )}
        </div>
      </>
    );
  }
}
