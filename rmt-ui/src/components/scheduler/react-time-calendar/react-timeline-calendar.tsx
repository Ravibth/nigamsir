/* eslint-disable array-callback-return */
/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";
import {
  Grid,
  Tooltip,
  TooltipProps,
  styled,
  tooltipClasses,
} from "@mui/material";
import CircleIcon from "@mui/icons-material/Circle";
import "./timelinestyle.css";
import moment from "moment";
import "react-calendar-timeline/lib/Timeline.css";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import * as util from "./util";
import * as constant from "./constant";
import ResourceMenu from "../../scheduler-left/resource-menu/resources-menu";
import { IResourcesMenuProps } from "../../scheduler-left/resource-menu/IResourcesMenuProps";
import { GroupKey } from "./Idata";
import { IPaginationData, TimelineProps } from "../interface/timelineprops";
import { tcalendar } from "./timeline-calendar/tcalendar";
import * as GC from "../../../global/constant";
import {
  getProjectDetailsForEmployeeListingByPipelineCode,
  getAllProjectListByRequestorEmail,
} from "../../../services/project-list-services/project-list-services";
import { getProjectsOfEmployeeByEmail } from "../../../services/allocation/allocation.service";
import {
  TimelineGroupExt,
  TimelineItemBaseExt,
  TimelineSate,
} from "../interface/timelinestate";
import { getProjectsOfEmployeeByPipelineCode } from "../../../services/big-calendar-services/bigcalendar.service";
import {
  EProjectRequisitionAllocationStatus,
  RolesListMaster,
} from "../../../common/enums/ERoles";
import {
  getDateInMomentFormat,
  getDateWithEndHours,
  MomentToJSDate,
} from "../../../utils/date/dateHelper";
import IFilterQueryParameters, {
  IProjectList,
} from "../../../services/project-list-services/IProjectList";
import { GroupType, ItemType, debounceFetchSuggestion } from "./constant";
import { MODULE_NAME_ENUM } from "../../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../../common/access-control-guard/access-control";
import {
  GetProjectRoleAndPermission,
  IsPermissionExistForProject,
} from "../../../global/utils";
import { EPipelineStatus } from "../../../common/enums/EProject";
import MarketPlaceIcon from "../../../common/images/marketplace.png";

const LightTooltip = styled(({ className, ...props }: TooltipProps) => (
  <Tooltip {...props} arrow classes={{ popper: className }} />
))(({ theme }) => ({
  [`& .${tooltipClasses.tooltip}`]: {
    backgroundColor: theme.palette.common.white,
    color: "white",
    boxShadow: theme.shadows[1],
    fontSize: 11,
  },
  [`& .${tooltipClasses.arrow}`]: {
    color: theme.palette.common.white,
  },
}));

export default class ReacTimelineCalendar extends tcalendar {
  pageLimitConst = this.props.pageLimitConst;
  projectCount = 0;
  projType = "ALL";
  isDataLoading = false;

  // scrollEventAttached: boolean = false;
  public ctrlTimelineContainerRef: React.RefObject<any>;

  constructor(props: TimelineProps) {
    super(props);
    this.ctrlTimelineContainerRef = React.createRef();
    this.state = {
      isCalendarRefresh: true,
      isEmployee: this.props.isEmployee,
      projectListDataEmployee: [],
      projectChargeType: "All",
      submittedFilterData: this.props.submittedFilterData,
      projectsData: [],
      groups: [],
      startDate: moment()
        .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Months))
        .valueOf(),
      endDate: moment()
        .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Months))
        .valueOf(),
      currentView: constant.CALENDAR_VIEW.Month.toString(),
      items: [],
      currentProjectData: [],
      projectsListData: [],
      itemsListData: [],
      isAllocationEmployeeShow: false,
      pipelineCode: "",
      jobCode: "",
      itemId: 0,
      currentRecordPipelineCode: "",
      currentRecordJobCode: "",
      serviceResponseData: [] as any,
    };
  }
  GetAllEmployeeProjectListFromDB(
    email: string,
    limit: number,
    pageNumber: number,
    searchQuery: string,
    searchRoles: string[]
  ): Promise<any> {
    return new Promise((resolve, reject) => {
      if (!this.state.isEmployee) {
        const paramsFilter: IFilterQueryParameters = util.mapFilterParam(
          this.props.submittedFilterData
        );
        paramsFilter.ProjectType =
          this.projType === "ALL" ? null : this.projType;

        const request: IProjectList = {
          userEmail: email,
          limit: limit,
          pagination: pageNumber,
          searchQuery: searchQuery,
          searchRoles: searchRoles,
          filterQueryParameters: paramsFilter,
        };
        getAllProjectListByRequestorEmail(request)
          .then((resp) => {
            resolve(resp.data);
          })
          .catch((ex) => {});
      } else {
        getProjectsOfEmployeeByEmail(email)
          .then((resp) => {
            resolve(resp.data);
          })
          .catch((ex) => {});
      }
    });
  }

  onItemClick = async (itemId: any) => {
    const currentItem = this.state.items.find((a: any) => a.id === itemId);
    const projectInfo = this.state.groups.find(
      (project) => project.id === currentItem?.group
    );
    if (projectInfo) {
      setTimeout(() => {
        if (
          IsPermissionExistForProject(
            this.props.userDetails?.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Allocation,
            PERMISSION_TYPE.Create,
            this.props.userDetails.role
          )
        ) {
          if (
            currentItem &&
            currentItem.isEmployeeItem &&
            currentItem["timeline_type"]?.toLowerCase().trim() ===
              ItemType.Allocation?.toLocaleLowerCase().trim()
          ) {
            this.contextMenuClickHandler(
              currentItem.pipelineCode,
              currentItem.jobCode,
              currentItem
            );
            this.setState({
              isAllocationEmployeeShow: true,
              itemId: currentItem.requisitionId,
              pipelineCode: currentItem.pipelineCode,
              jobCode: currentItem.jobCode,
            });
          }
        } else {
          return;
        }
      }, 200);
    }
  };

  public onCloseAllocationModel = async (itemId: any) => {
    const projectData = await this.getProjectListData();
    const currentItem: any = this.state.items.find(
      (a: any) => a.requisitionId === itemId
    );
    this.onClickAccordian(
      currentItem,
      currentItem.pipelineCode,
      currentItem.jobCode,
      false
    );

    this.setState({
      itemId: 0,
    });
  };

  GetProjectsOfEmployeeByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    emailId?: string,
    isAllocationDetailsFilterByUserRoles: boolean = false
  ) {
    return new Promise((resolve, reject) => {
      this.props.loaderContext.open(true);
      getProjectsOfEmployeeByPipelineCode(
        pipelineCode,
        jobCode,
        emailId,
        isAllocationDetailsFilterByUserRoles
      )
        .then((resp) => {
          this.props.loaderContext.open(false);
          resolve(resp.data);
        })
        .catch((ex) => {
          this.props.loaderContext.open(false);
        });
    });
  }

  public handleScroll = () => {
    const element: any =
      this.ctrlTimelineContainerRef.current.childNodes[0].childNodes[1];
    let _pageObj: IPaginationData = this.props.getPaginationDataFunc();
    let totalCurrentRecordCount = this.state.currentProjectData.length;
    let scrollPosition = element.scrollHeight - element.scrollTop;
    const scrollMargin: number = 5;
    if (
      !(totalCurrentRecordCount < _pageObj.limit) &&
      totalCurrentRecordCount >=
        _pageObj.limit * (_pageObj.currentPageNumber - 1) &&
      scrollPosition <= element.clientHeight + scrollMargin &&
      scrollPosition >= element.clientHeight - scrollMargin // keeping margin of 10 px for scroll
    ) {
      this.handleLoadMoreDataClick();
    }
  };

  public handleLoadMoreDataClick = (isFirstLoad?: boolean) => {
    let _pageObj: IPaginationData = this.props.getPaginationDataFunc();
    if (!this.isDataLoading) {
      this.props.loaderContext.open(true);
      this.isDataLoading = true;
      let pageNumber =
        isFirstLoad === true
          ? _pageObj.currentPageNumber
          : _pageObj.nextPageNumber;

      this.loadProjectMoreData(
        _pageObj.limit,
        pageNumber,
        this.props.searchRoles,
        this.props.searchQuery
      ).then(() => {
        _pageObj.currentPageNumber = pageNumber;
        _pageObj.nextPageNumber = pageNumber + 1;
        this.props.setPaginationDataFunc(_pageObj);
        this.isDataLoading = false;
      });
    } else {
    }
  };

  public loadProjectMoreData = (
    limit: number,
    pageNumber: number,
    searchRoles: string[],
    searchQuery?: string
  ) => {
    if (pageNumber > 0 && limit > 0) {
      return Promise.all([
        this.GetAllEmployeeProjectListFromDB(
          this.props.userDetails.username,
          limit,
          pageNumber,
          searchQuery,
          searchRoles
        ),
      ])
        .then(async (values) => {
          let oldServiceResponsData =
            pageNumber === 1 ? ([] as any) : this.state.serviceResponseData;
          let _obj = values[0];
          oldServiceResponsData.push(..._obj);
          this.setState({ serviceResponseData: oldServiceResponsData });
          let processedData = await this.getTimeLineFormatData(
            oldServiceResponsData
          );
          if (processedData !== undefined) {
            this.setState(processedData);
          }
          this.props.loaderContext.open(false);
        })
        .then((data) => {
          return true;
        });
    }
  };

  componentDidMount(): void {
    this.getProjectListData();
    var _this = this;
    document.addEventListener("named-closed-event", function (e: any) {
      _this.refreshCalendar(
        e.detail.projectInfo,
        e.detail?.pipelineCode,
        e.detail?.jobCode
      );
    });
  }

  refreshCalendar(projectInfo: any, pipelineCode: string, jobCode: string) {
    this.getProjectListData();
    this.onClickAccordian(projectInfo, pipelineCode, jobCode, false);
  }

  getProjectListData = () => {
    this.handleLoadMoreDataClick(true);
  };

  private async getTimeLineFormatData(values: any) {
    let stateData = undefined;
    let newProjectList: GroupKey[] = [];
    let newProjectListForEmployee: GroupKey[] = [];
    let projectsDataByDb = values;
    if (this.state.isEmployee) {
      newProjectListForEmployee = this.setEmployeeData(projectsDataByDb);
      const distinctPipelineCodes: string[] =
        util.GetDistinctPipelineCodeForEmployees(newProjectListForEmployee);
      let projectDetailsMap = new Map();
      getProjectDetailsForEmployeeListingByPipelineCode(distinctPipelineCodes)
        .then(async (res) => {
          res.data.forEach((d: any) => {
            projectDetailsMap.set(d.name, d.value);
          });
          newProjectListForEmployee = newProjectListForEmployee.map((data) => {
            return {
              ...data,
              projectAllocationStatus: projectDetailsMap.get(data.pipelineCode)
                ?.projectAllocationStatus,
              chargableType: projectDetailsMap.get(data.pipelineCode)
                ?.chargableType,
              sme: projectDetailsMap.get(data.pipelineCode)?.sme,
              clientName: projectDetailsMap.get(data.pipelineCode)?.clientName,
              expertise: projectDetailsMap.get(data.pipelineCode)?.expertise,
            };
          });
          let data = this.getGroupsForEmployee(newProjectListForEmployee);
          const distinctPipeCode = util.GetDistinctPipelineCodeForEmployees(
            newProjectListForEmployee
          );
          this.projectCount = distinctPipeCode.length;
          this.getProjectCount(distinctPipeCode.length);
          this.getFilterDataAndPopulate();
          stateData = {
            projectListDataEmployee: newProjectListForEmployee,
            projectsListData: newProjectList,
            groups: data.projectsList,
            projectsData: constant.projectsList,
            items: data.itemsList,
          };
        })
        .catch((err) => {});
    } else {
      newProjectList = this.setProjectData(projectsDataByDb);
      let data;
      data = this.getGroups(newProjectList);
      this.projectCount = newProjectList.length;
      this.getProjectCount(newProjectList.length);
      this.getFilterDataAndPopulate();
      stateData = {
        currentProjectData: newProjectList,
        projectListDataEmployee: newProjectListForEmployee,
        projectsListData: newProjectList,
        groups: data.projectsList,
        projectsData: constant.projectsList,
        items: data.itemsList,
      };
    }
    return stateData;
  }

  //Seperate function created for parellel call to get filtersdata async
  async getFilterDataAndPopulate() {
    util.getFilterOptionData().then((data: util.IFilterOptions) => {
      this.props.handleFilterData(data);
    });
  }

  async componentDidUpdate(
    prevProps: Readonly<TimelineProps>,
    prevState: Readonly<TimelineSate>
  ): Promise<void> {
    // if (this.scrollEventAttached != true) {
    const timelineRefParentElement =
      this.ctrlTimelineContainerRef?.current?.childNodes;
    if (
      timelineRefParentElement &&
      timelineRefParentElement.length > 0 &&
      timelineRefParentElement[0].childNodes &&
      timelineRefParentElement[0].childNodes.length > 1
    ) {
      const element: any = timelineRefParentElement[0].childNodes[1];

      const debounceScroll = debounceFetchSuggestion(this.handleScroll, 300);
      if (element) {
        // Add a scroll event listener to the specific component
        element.removeEventListener("scroll", debounceScroll);
        element.addEventListener("scroll", debounceScroll);
        // this.scrollEventAttached = true;
      }
    }
    if (prevProps.searchQuery !== this.props.searchQuery) {
      this.setState({ serviceResponseData: [] as any });
      this.loadProjectMoreData(
        this.pageLimitConst,
        1,
        this.props.searchRoles,
        this.props?.searchQuery
      );
      if (this.props?.searchQuery === "") {
        this.props.onInitialPaginationData(true);
      }
      timelineRefParentElement[0].childNodes[1].scrollTo(0, 0);
    }
    if (prevProps.searchRoles !== this.props.searchRoles) {
      if (this.props.searchRoles && this.props.searchRoles.length === 0) {
        this.loadProjectMoreData(
          this.pageLimitConst,
          1,
          null,
          this.props?.searchQuery
        );
      } else {
        this.loadProjectMoreData(
          this.pageLimitConst,
          1,
          this.props.searchRoles,
          this.props?.searchQuery
        );
      }
      this.props.onInitialPaginationData(true);
      timelineRefParentElement[0].childNodes[1].scrollTo(0, 0);
    }
    if (prevProps.currentView !== this.props.currentView) {
      this.setState({
        currentView: this.props.currentView,
      });
      this.calendarViewByCurrentView();
    }

    if (prevProps.selectedDate !== this.props.selectedDate) {
      this.selectedDateClick(this.props.selectedDate, this.state.currentView);
    }
    if (
      prevProps.handlePreviousorNextClick !==
      this.props.handlePreviousorNextClick
    ) {
      switch (this.props.handlePreviousorNextClick) {
        case GC.NEXTPRE_CLICK.Next:
          this.handleNextClick();
          break;
        case GC.NEXTPRE_CLICK.Pre:
          this.handlePreviousClick();
          break;
      }
      this.props.setPreviousorNextNone();
    }

    if (prevProps.submittedFilterData !== this.props.submittedFilterData) {
      this.props.loaderContext.open(true);
      let data;
      let _pageObj: IPaginationData = this.props.getPaginationDataFunc();
      _pageObj.currentPageNumber = 1; //reset page numberwhen filtering data
      _pageObj.nextPageNumber = 2; //reset page numberwhen filtering data
      const selectedProjectListData = this.state.isEmployee
        ? this.state.projectListDataEmployee
        : this.state.projectsListData;

      const updatedProjectsListData = await util.filterProjectData(
        selectedProjectListData,
        this.props.submittedFilterData,
        this.projType,
        this.props.searchQuery,
        this.props.searchRoles,
        this.state.isEmployee,
        this.props?.userDetails?.username,
        _pageObj.limit,
        _pageObj.currentPageNumber
      );

      let oldServiceResponsData =
        _pageObj.currentPageNumber === 1
          ? ([] as any)
          : this.state.serviceResponseData;
      let _obj = updatedProjectsListData;
      oldServiceResponsData.push(..._obj);
      this.setState({ serviceResponseData: oldServiceResponsData });

      let dataLength = updatedProjectsListData.length;
      this.getProjectCount(dataLength);
      if (
        this.projType.toUpperCase() ===
        constant.PROJECT_TYPE.OPEN.toString().toUpperCase()
      ) {
        this.getChargeType(1);
      } else if (
        this.projType.toUpperCase() ===
        constant.PROJECT_TYPE.CLOSE.toString().toUpperCase()
      ) {
        this.getChargeType(2);
      } else {
        this.getChargeType(0);
      }
      this.props.SetupFilterDefaultValue(this.props.submittedFilterData);
      data = this.state.isEmployee
        ? this.getGroupsForEmployee(updatedProjectsListData)
        : this.getGroups(updatedProjectsListData);

      this.setState({
        currentProjectData: updatedProjectsListData,
        groups: data.projectsList,
        items: data.itemsList,
      });
      this.props.loaderContext.open(false);
    }

    if (
      this.state.currentRecordPipelineCode !== "" &&
      this.state.currentRecordJobCode !== ""
    ) {
      let data1 = this.state.isEmployee
        ? this.getGroupsForEmployee(this.state.projectListDataEmployee)
        : this.getGroups(this.state.serviceResponseData);

      this.setState({
        groups: data1.projectsList,
        currentRecordPipelineCode: "",
        currentRecordJobCode: "",
      });
    }
  }

  getProjectTitle = (item: any, status: any) => {
    let projectAllocationStatus = "";
    let circleColor = "white";
    let projectStatus = util.getProjectStateByActivationAndClosureState(
      item.projectClosureState,
      item.projectActivationStatus
    );
    if (
      item.pipelineStatus === EPipelineStatus.Suspended ||
      item.pipelineStatus === EPipelineStatus.Lost
    ) {
      projectAllocationStatus = "";
      circleColor = "white";
    } else if (!item?.projectRequisitionAllocations) {
      projectAllocationStatus = EProjectRequisitionAllocationStatus.ToBeStarted;
      circleColor = "#ff5149";
    } else {
      switch (item?.projectRequisitionAllocations?.status.toString().trim()) {
        case EProjectRequisitionAllocationStatus.Completed.toString().trim():
          projectAllocationStatus =
            EProjectRequisitionAllocationStatus.Completed;
          circleColor = "#00a4b3";
          break;
        case EProjectRequisitionAllocationStatus.PENDING.toString().trim():
          projectAllocationStatus = EProjectRequisitionAllocationStatus.PENDING;
          circleColor = "#ffc23d";
          break;
        case EProjectRequisitionAllocationStatus.ToBeStarted.toString().trim():
          projectAllocationStatus =
            EProjectRequisitionAllocationStatus.ToBeStarted;
          circleColor = "#ff5149";
          break;
      }
    }

    const ResourcesMenuData: IResourcesMenuProps = {
      projectId: item.pipelineCode,
      project: item,
      contextMenuClickHandler: this.contextMenuClickHandler,
    };

    let elementClass = "elemProjectitem-" + item.pipelineCode;
    let selectedClass = "elemProjectitem-" + item.pipelineCode;

    if (
      item.pipelineCode?.toLowerCase() ===
        this.state.currentRecordPipelineCode?.toLowerCase() &&
      item.jobCode?.toLowerCase() ===
        this.state.currentRecordJobCode?.toLowerCase()
    ) {
      selectedClass = "selected-ProjectItem";
    } else {
      selectedClass = "";
    }

    const userHasMoreAccessThanEmployee = () => {
      //projectRolesView change

      if (
        this.props?.userDetails?.role?.includes(RolesListMaster.SystemAdmin)
      ) {
        return true;
      }

      const roles = item.projectRolesView
        .filter(
          (m) =>
            m.user.trim().toLowerCase() ===
            this.props.userDetails.username.trim().toLowerCase()
        )
        //ApplicationRole not required here only employee role is checked
        .map((m) => m.role.toLowerCase());
      if (
        roles.includes(RolesListMaster.Employee.toLowerCase()) &&
        roles.length === 1
      ) {
        return false;
      } else if (roles.length >= 1) {
        return true;
      }
    };
    return (
      <>
        {!item.isHeader ? (
          <Grid container className={elementClass + " " + selectedClass}>
            <Grid item xs={0.1} />
            <Grid item xs={1} sx={constant.VerticalCenterAlignSxProps}>
              {userHasMoreAccessThanEmployee() ? (
                <LightTooltip title={projectAllocationStatus}>
                  <CircleIcon sx={{ color: circleColor, fontSize: "14px" }} />
                </LightTooltip>
              ) : (
                <></>
              )}
            </Grid>
            <Grid item xs={7} sx={constant.VerticalCenterAlignSxProps}>
              <Grid container>
                <Grid
                  item
                  xs={12}
                  style={{
                    font: "caption",
                    width: "100%",
                    whiteSpace: "normal",
                    lineHeight: "1.5",
                    minHeight: "24px",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                  }}
                >
                  <Tooltip title={util.getPipelinNameOrJobName(item)}>
                    <span>{util.getPipelinNameOrJobName(item)}</span>
                  </Tooltip>
                </Grid>
                <Grid
                  item
                  xs={12}
                  style={{
                    font: "small-caption",
                    color: GC.GT_DESIGN_PARAMETERS.GtPrimaryColor,
                    padding: "3px 0px",
                  }}
                >
                  {projectStatus}
                </Grid>
              </Grid>
            </Grid>
            <Grid item xs={1}>
              {userHasMoreAccessThanEmployee() &&
                item?.isPublishedToMarketPlace &&
                (!item?.marketPlaceExpirationDate ||
                  moment(item?.marketPlaceExpirationDate).format(
                    GC.dateFormatYMD
                  ) >= moment(new Date()).format(GC.dateFormatYMD)) && (
                  <Tooltip title="Listed in Marketplace" placement="right">
                    <img
                      src={MarketPlaceIcon}
                      alt="upload"
                      style={{
                        height: "22px",
                        width: "22px",
                        marginTop: "5px",
                        display: "flex",
                      }}
                    />
                  </Tooltip>
                )}
            </Grid>
            <Grid item xs={2} sx={{ mt: "12px" }}>
              <Grid container>
                <Grid item xs={6} sx={constant.VerticalCenterAlignSxProps}>
                  {ResourcesMenuData?.project?.jobCode != null && (
                    <ResourceMenu
                      {...ResourcesMenuData}
                      pipelineCode={ResourcesMenuData?.project?.pipelineCode}
                      jobCode={ResourcesMenuData?.project?.jobCode}
                    />
                  )}
                </Grid>
                {item.jobCode != null && (
                  <Grid
                    item
                    xs={6}
                    ml={-2}
                    sx={constant.VerticalCenterAlignSxProps}
                  >
                    {!item.defaultExpanded
                      ? !this.props.isEmployee && (
                          <KeyboardArrowRightIcon
                            fontSize="small"
                            className="project-arrow-right"
                            style={constant.inlineStyles}
                            onClick={(e: any) => {
                              this.onClickAccordian(
                                item,
                                item.pipelineCode,
                                item.jobCode,
                                false
                              );
                            }}
                          />
                        )
                      : !this.props.isEmployee && (
                          <KeyboardArrowDownIcon
                            style={constant.inlineStyles}
                            fontSize="small"
                            className="project-arrow-down"
                            onClick={(e: any) => {
                              this.onClickAccordian(
                                item,
                                item.pipelineCode,
                                item.jobCode,
                                true
                              );
                            }}
                          />
                        )}
                  </Grid>
                )}
              </Grid>
            </Grid>
          </Grid>
        ) : (
          <div className="">{util.getPipelinNameOrJobName(item)}</div>
        )}
      </>
    );
  };

  //Get Timeline Bar color based on Type

  private getGroupsForEmployee = (projectData: any) => {
    const projectsList: TimelineGroupExt[] = [];
    const itemsList: TimelineItemBaseExt[] = [];
    let itemCount = 0;
    let count = 0;
    let projectFreqMap = new Map();
    count = 1;
    itemCount = 1;
    const jobProjects = projectData.filter(
      (projectItem: any) => projectItem?.jobCode?.length > 0
    );
    if (jobProjects?.length > 0) {
      projectsList.push({
        id: count,
        title: (
          <div
            className={"job-header"}
            style={{ backgroundColor: "#f0f0f0", fontWeight: "bold" }}
          >
            Jobs
          </div>
        ),
        // height: 50,
        RowType: GroupType.Job,
      });
      count++;
      jobProjects.forEach((item: any) => {
        if (!projectFreqMap.has(item.pipelineCode + ";#" + item.jobCode)) {
          projectFreqMap.set(item.pipelineCode + ";#" + item.jobCode, count);
          projectsList.push({
            ...item,
            id: count,
            title: this.getProjectTitle(item, item?.pipelineStatus),
            //  height: 50,
            RowType: GroupType.Job,
          });
          count++;
        }

        itemsList.push({
          id: itemCount,
          title: util.GetTitleOfRow(item?.timeline_type, item.allocationHours),
          group: projectFreqMap.get(item.pipelineCode + ";#" + item.jobCode),
          start_time: MomentToJSDate(
            getDateInMomentFormat(new Date(item.startDate))
          ),
          end_time: getDateWithEndHours(new Date(item.endDate)),
          innerHeight: 500,
          outerHeight: 500,
          className: util.getTimelineBarColor(
            item.timeline_type,
            item.pipelineStatus
          ),
          RowType: item.timeline_type,
        });
        itemCount++;
      });
    }
    const pipeProjects = projectData.filter(
      (projectItem: any) => projectItem?.jobCode?.length === 0
    );
    if (pipeProjects?.length > 0) {
      projectsList.push({
        id: count,
        title: (
          <div
            className={"job-header"}
            style={{ backgroundColor: "#f0f0f0", fontWeight: "bold" }}
          >
            Pipeline
          </div>
        ),
        height: 50,
        RowType: GroupType.Pipeline,
      });
      count++;
      pipeProjects.forEach((item: any) => {
        if (!projectFreqMap.has(item.pipelineCode + ";#" + item.jobCode)) {
          projectFreqMap.set(item.pipelineCode + ";#" + item.jobCode, count);
          projectsList.push({
            ...item,
            id: count,
            title: this.getProjectTitle(item, item.pipelineStatus),
            height: 50,
            RowType: GroupType.Pipeline,
          });
          count++;
        }
        itemsList.push({
          id: itemCount,
          title: util.GetTitleOfRow(item?.timeline_type, item.allocationHours),
          group: projectFreqMap.get(item.pipelineCode + ";#" + item.jobCode),
          start_time: MomentToJSDate(
            getDateInMomentFormat(new Date(item.startDate))
          ),
          end_time: getDateWithEndHours(new Date(item.endDate)),
          innerHeight: 500,
          outerHeight: 500,
          className: util.getTimelineBarColor(
            item.timeline_type,
            item.pipelineStatus
          ),
          RowType: item.timeline_type,
        });
        itemCount++;
      });
    }
    return {
      projectsList,
      itemsList,
    };
  };

  private contextMenuClickHandler = (
    recordPipelineCode: any,
    recordJobCode: any,
    obj: any
  ) => {
    //highlight the color basedon current pipline code

    this.setState({
      currentRecordPipelineCode: recordPipelineCode,
      currentRecordJobCode: recordJobCode,
    });
  };

  private getGroups = (projectsData: any) => {
    const projectsList: TimelineGroupExt[] = [];
    const itemsList: TimelineItemBaseExt[] = [];
    let itemCount = 0;
    let projectFreqMap = new Map();
    let count = 1;
    const jobProjects = projectsData.filter((a) => a.jobCode != null);

    if (jobProjects.length > 0) {
      projectsList.push({
        id: count,
        title: (
          <div className={"job-header"} style={{ backgroundColor: "#f0f0f0" }}>
            Job
          </div>
        ),
        height: 50,
        RowType: GroupType.Job,
      });
      count++;
      jobProjects.forEach((item: any) => {
        if (!projectFreqMap.has(item.pipelineCode + ";#" + item.jobCode)) {
          projectFreqMap.set(item.pipelineCode + ";#" + item.jobCode, count);
        }
        this.getProjectTitle(item, item.pipelineStatus);
        projectsList.push({
          ...item,
          id: count,
          title: this.getProjectTitle(item, item.pipelineStatus),
          // height: 50,
          stackItems: true,
          innerHeight: 500,
          RowType: GroupType.Job,
        });

        // implement moment formate.
        let projectDurationText = "";              

        if (item.projectAllocatedHoursRatio) {
          const isProjectRolesAllEmployee = item.projectRolesView?.length > 0 && item.projectRolesView?.every(x => 
            x.applicationRole?.toLowerCase().includes(RolesListMaster.Employee.toLowerCase())
          );
          
          const isSingleEmployeeSearch = this.props.searchRoles?.length === 1 && 
            this.props.searchRoles.includes('Employee');
          
          if (!(isProjectRolesAllEmployee || isSingleEmployeeSearch)) {
            projectDurationText = ` ${item.projectAllocatedHoursRatio.allocatedTotalHours}/ ${item.projectAllocatedHoursRatio.requistionTotalHours} allocated hrs`;
          }
        }

        itemsList.push({
          ...item,
          listId: item.id,
          id: itemCount,
          title: projectDurationText,
          group: projectFreqMap.get(item.pipelineCode + ";#" + item.jobCode),
          start_time: MomentToJSDate(
            getDateInMomentFormat(new Date(item.startDate))
          ),

          end_time: getDateWithEndHours(new Date(item.endDate)),

          innerHeight: 500,
          outerHeight: 500,
          className: util.getTimelineBarColor(
            ItemType.JobDuration,
            item.pipelineStatus
          ),
          RowType: ItemType.JobDuration,
        });
        itemCount++;

        count++;
        if (item.defaultExpanded && item.details.length > 0) {
          //To resolve 7289 - Sort desc added (if there's a leave or any break in allocation it was picking last date of first chunk)
          const sortedDetails = item?.details?.sort((a, b) => {
            return new Date(b.endDate).getTime() - new Date(a.endDate).getTime();
          });
          sortedDetails?.forEach((detail: any) => {
            if (
              !projectFreqMap.has(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id
              )
            ) {
              projectFreqMap.set(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id,
                count
              );
              let d1 = {
                ...detail,
                listId: detail.id, //requsitionid
                id: itemCount,
                title: detail.confirmedPerDayHours + " hours",
                group: projectFreqMap.get(
                  detail.empEmail +
                    detail.pipelineCode +
                    ";#" +
                    detail.jobCode +
                    detail.id
                ),
                start_time: moment(detail.startDate, GC.dateFormatYMD),
                end_time: moment(detail.endDate, GC.dateFormatYMD),
                innerHeight: 500,
                outerHeight: 500,
                className: util.getTimelineBarColor(
                  GroupType.Employee,
                  detail.pipelineStatus
                ),
                RowType: GroupType.Employee,
              };
              projectsList.push({
                ...item,
                id: count,
                title: util.getChildTilte(
                  detail.pipelineCode,
                  detail.empName,
                  detail.allocationStatus,
                  d1,
                  this.contextMenuClickHandler,
                  detail
                ),
                stackItems: true,
                innerHeight: 500,
                height: 50,
                isEmployeeRow: true,
              });
              count++;
            }

            itemsList.push({
              ...detail,
              //stackItems: true,
              listId: detail.id,
              id: itemCount,
              title: util.GetTitleOfRow(
                detail?.timeline_type,
                detail.confirmedPerDayHours
              ),
              group: projectFreqMap.get(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id
              ),
              start_time: moment(detail.startDate, GC.dateFormatYMD),
              end_time: moment(detail.endDate, GC.dateFormatYMD).add(1, "day"),
              innerHeight: 500,
              outerHeight: 500,
              className: util.getTimelineBarColor(
                detail.timeline_type,
                detail.pipelineStatus
              ),
              isEmployeeItem: true,
              RowType: detail.timeline_type,
            });
            itemCount++;
          });
        }
      });
    }

    const pipelineProjects = projectsData.filter(
      (a) => a.pipelineCode != null && a.jobCode == null
    );
    if (pipelineProjects.length > 0) {
      projectsList.push({
        id: count,
        title: (
          <div className={"job-header"} style={{ backgroundColor: "#f0f0f0" }}>
            Pipeline
          </div>
        ),
        height: 50,
        RowType: GroupType.Pipeline,
      });
      count++;
      pipelineProjects.forEach((pipelineProject: any) => {
        if (
          !projectFreqMap.has(
            pipelineProject.pipelineCode + ";#" + pipelineProject.jobCode
          )
        ) {
          projectFreqMap.set(
            pipelineProject.pipelineCode + ";#" + pipelineProject.jobCode,
            count
          );
        }
        projectsList.push({
          ...pipelineProject,
          id: count,
          title: this.getProjectTitle(
            pipelineProject,
            pipelineProject.pipelineStatus === EPipelineStatus.Suspended
              ? "Suspended"
              : pipelineProject.pipelineStatus === EPipelineStatus.Lost
              ? "Lost"
              : pipelineProject.pipelineStatus
          ),
          stackItems: true,
          innerHeight: 500,
          RowType: GroupType.Pipeline,
        });
        let projectDurationText = "";
        if (
          pipelineProject.projectAllocatedHoursRatio !== undefined &&
          pipelineProject.projectAllocatedHoursRatio != null &&
          pipelineProject.projectAllocatedHoursRatio
        ) {
          projectDurationText = `${pipelineProject.projectAllocatedHoursRatio.allocatedTotalHours}/ ${pipelineProject.projectAllocatedHoursRatio.requistionTotalHours} allocated hrs`;
        }
        //pipeline item
        itemsList.push({
          id: itemCount,
          group: projectFreqMap.get(
            pipelineProject.pipelineCode + ";#" + pipelineProject.jobCode
          ),
          title: projectDurationText,
          start_time: MomentToJSDate(
            getDateInMomentFormat(new Date(pipelineProject.startDate))
          ),
          end_time: getDateWithEndHours(new Date(pipelineProject.endDate)),
          innerHeight: 500,
          outerHeight: 500,
          className: util.getTimelineBarColor(
            ItemType.PipelineDuration,
            pipelineProject.pipelineStatus
          ),
          RowType: ItemType.PipelineDuration,
        });
        itemCount++;
        count++;

        //code added to show allocated employee for jobs expanded section- Start

        if (
          pipelineProject.defaultExpanded &&
          pipelineProject.details.length > 0
        ) {
          let itemDetailsLastOccuranceIndx = pipelineProject.details.length - 1;
          pipelineProject?.details?.forEach((detail: any) => {
            if (
              !projectFreqMap.has(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id
              )
            ) {
              projectFreqMap.set(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id,
                count
              );
              let d1 = {
                ...detail,
                listId: detail.id,
                id: itemCount,
                title: detail.confirmedPerDayHours + " hours",
                group: projectFreqMap.get(
                  detail.empEmail +
                    detail.pipelineCode +
                    ";#" +
                    detail.jobCode +
                    detail.id
                ),
                start_time: moment(detail.startDate, GC.dateFormatYMD),
                end_time: moment(detail.endDate, GC.dateFormatYMD),
                innerHeight: 500,
                outerHeight: 500,
                className: util.getTimelineBarColor(
                  GroupType.Employee,
                  detail.pipelineStatus
                ),
                RowType: GroupType.Employee,
              };
              projectsList.push({
                ...pipelineProject,
                id: count,
                title: util.getChildTilte(
                  detail.pipelineCode,
                  detail.empName,
                  detail.allocationStatus,
                  d1,
                  this.contextMenuClickHandler,
                  detail
                ),
                stackItems: true,
                innerHeight: 500,
                // height: 50,
              });
              count++;
            }

            itemsList.push({
              ...detail,
              //stackItems: true,
              listId: detail.id,
              id: itemCount,
              title: util.GetTitleOfRow(
                detail?.timeline_type,
                detail?.confirmedPerDayHours
              ),
              group: projectFreqMap.get(
                detail.empEmail +
                  detail.pipelineCode +
                  ";#" +
                  detail.jobCode +
                  detail.id
              ),
              start_time: moment(detail.startDate, GC.dateFormatYMD),
              end_time: moment(detail.endDate, GC.dateFormatYMD).add(1, "day"),
              innerHeight: 500,
              outerHeight: 500,
              className: util.getTimelineBarColor(
                detail.timeline_type,
                detail.pipelineStatus
              ),
              isEmployeeItem: true,
              RowType: detail.timeline_type,
            });
            itemCount++;
          });
        }
        //code added to show allocated employee for jobs expanded section- End
      });
    }

    projectsList.map((a: any) => {
      const groupsItems = itemsList.filter((item: any) => item.group === a.id);
      //a.height = 50;
      if (a.isEmployeeRow) return;
      const nameLength = util.getPipelinNameOrJobName(a)?.length || 0;
      a.height = 50 + Math.floor(nameLength / 10) * 12;
      //  a.height = Math.min(
      //    100,
      //    50 + Math.floor(nameLength / 10) * 10
      //  );
      //Todo: Don't remove this code.
      // if (groupsItems.length < 2) {
      //   a.height = 50;
      // } else {
      //   a.height = 0;
      // }
    });

    return {
      projectsList,
      itemsList,
    };
  };
  private setEmployeeData = (EmployeeDbData: any) => {
    let newProjectListForEmployee: GroupKey[] = [];
    EmployeeDbData.map((projectData: any) => {
      let item: GroupKey = {
        id: projectData.id,
        isHeader: false,
        defaultExpanded: false,
        projectName: projectData.pipelineName || projectData.jobName,
        details: [],
        pipelineName: projectData.pipelineName,
        startDate: projectData.startDate,
        endDate: projectData.endDate,
        jobCode: projectData.jobCode,
        pipelineCode: projectData.pipelineCode,
        projectAllocationStatus: projectData.projectAllocationStatus,
        project_status: "Ongoing",
        allocationHours: projectData.confirmedPerDayHours,
        requisitions: [],
      };
      newProjectListForEmployee.push(item);
    });
    return newProjectListForEmployee;
  };

  private setProjectData = (RequestorDbData: any) => {
    let newProjectList: GroupKey[] = [];

    RequestorDbData.map((projectData: any) => {
      let item: GroupKey = {
        ...projectData,
        defaultExpanded: false,
        requisitions: [],
        project_status: "Ongoing",
        details: [],
      };
      newProjectList.push(item);
    });
    return newProjectList;
  };

  onClickAccordian = async (
    projectInfo: any,
    pipelineCode: string,
    jobCode: string,
    isOpen: boolean
  ) => {
    //service data
    if (pipelineCode && !isOpen) {
      let _isEmployee = false;
      const _lstProjectByProjectCode = util.getProjectByCode(
        this.state.projectsListData,
        pipelineCode,
        jobCode
      );
      if (_lstProjectByProjectCode && _lstProjectByProjectCode.length > 0) {
        _isEmployee = util.isEmployeeRoles(
          _lstProjectByProjectCode[0],
          this.props.userDetails.username
        );
      }

      await GetProjectRoleAndPermission(
        projectInfo,
        this.props.userDetails,
        pipelineCode,
        jobCode,
        MODULE_NAME_ENUM.Requisition,
        PERMISSION_TYPE.Create
      );
      const isSearchRoleEmployee =
        this.props.searchRoles.indexOf("Employee") > -1 ? true : false;
      Promise.all([
        _isEmployee
          ? this.GetProjectsOfEmployeeByPipelineCode(
              pipelineCode,
              jobCode,
              this.props.userDetails.username,
              isSearchRoleEmployee
            )
          : this.GetProjectsOfEmployeeByPipelineCode(pipelineCode, jobCode),
      ]).then((values) => {
        let employees = values[0];

        const AllValues = this.state.projectsListData.map((item: any) => {
          if (
            item.pipelineCode?.toString().toUpperCase() ===
              pipelineCode?.toUpperCase() &&
            item.jobCode?.toString().toUpperCase() === jobCode?.toUpperCase()
          ) {
            return {
              ...item,
              defaultExpanded: true,
              details: employees,
            };
          } else {
            return item;
          }
        });
        const ListingValues = this.state.currentProjectData.map((item: any) => {
          if (
            item.pipelineCode?.toString().toUpperCase() ===
              pipelineCode?.toUpperCase() &&
            item.jobCode?.toString().toUpperCase() === jobCode?.toUpperCase()
          ) {
            return {
              ...item,
              defaultExpanded: true,
              details: employees,
            };
          } else {
            return item;
          }
        });
        const data = this.getGroups(ListingValues);
        this.setAccordianState(ListingValues, AllValues, data);
      });
    } else if (pipelineCode && isOpen) {
      const AllValue = this.state.projectsListData.map((item: any) => {
        if (
          item.pipelineCode?.toString().toUpperCase() ===
          pipelineCode?.toUpperCase()
        ) {
          return {
            ...item,
            defaultExpanded: false,
            details: [],
          };
        } else {
          return item;
        }
      });
      const ListingValue = this.state.currentProjectData.map((item: any) => {
        if (
          item.pipelineCode?.toString().toUpperCase() ===
          pipelineCode?.toUpperCase()
        ) {
          return {
            ...item,
            defaultExpanded: false,
            details: [],
          };
        } else {
          return item;
        }
      });
      const data = this.getGroups(ListingValue);
      this.setAccordianState(ListingValue, AllValue, data);
    }
  };

  handleNextClick = () => {
    const { startDate, endDate } = util.handlePreorNextClick(
      this.state,
      constant.CLICKEVENT_TYPE.Next
    );
    this.setState({
      startDate: startDate?.valueOf(),
      endDate: endDate?.valueOf(),
    });
  };
  selectedDateClick = (selectedDate: Date, currentView: string) => {
    const { startDate, endDate } = util.handleSelectedDateClick(
      selectedDate,
      currentView
    );
    this.setState({
      startDate: startDate?.valueOf(),
      endDate: endDate?.valueOf(),
    });
  };

  handlePreviousClick = () => {
    const { startDate, endDate } = util.handlePreorNextClick(
      this.state,
      constant.CLICKEVENT_TYPE.Previous
    );
    this.setState({
      startDate: startDate?.valueOf(),
      endDate: endDate?.valueOf(),
    });
  };

  handleDayClick = (selectedDate: Date) => {
    this.setState({
      isCalendarRefresh: true,
      currentView: constant.CALENDAR_VIEW.Day.toString(),
      startDate: moment(selectedDate)
        .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Day))
        .valueOf(),
      endDate: moment(selectedDate)
        .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Day))
        .valueOf(),
    });

    this.render();
  };
  handleMonthClick = (selectedDate: Date) => {
    this.setState({
      currentView: constant.CALENDAR_VIEW.Month.toString(),
      startDate: moment(selectedDate)
        .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Months))
        .valueOf(),
      endDate: moment(selectedDate)
        .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Months))
        .valueOf(),
    });
  };

  handleQuaterClick = (selectedDate: Date) => {
    this.setState({
      currentView: constant.CALENDAR_VIEW.Quater.toString(),
      startDate: moment(selectedDate)
        .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Quarter))
        .valueOf(),
      endDate: moment(selectedDate)
        .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Quarter))
        .valueOf(),
    });
  };

  handleHalfClick = (selectedDate: Date) => {
    const monthNumber = moment(selectedDate).month();
    const year = moment(selectedDate).year();

    if (monthNumber >= 6) {
      this.setState({
        currentView: constant.CALENDAR_VIEW.Half.toString(),
        startDate: moment(new Date(year, 6, 1)).valueOf(),
        endDate: moment(selectedDate)
          .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Year))
          .valueOf(),
      });
    } else {
      this.setState({
        currentView: constant.CALENDAR_VIEW.Half.toString(),
        startDate: moment(selectedDate)
          .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Year))
          .valueOf(),
        endDate: moment(new Date(year, 5, 30)).valueOf(),
      });
    }
  };

  handleYearClick = (selectedDate: Date) => {
    this.setState({
      currentView: constant.CALENDAR_VIEW.Year.toString(),
      startDate: moment(selectedDate)
        .startOf(constant.convertToDuration(constant.MOMENT_UNIT.Year))
        .valueOf(),
      endDate: moment(selectedDate)
        .endOf(constant.convertToDuration(constant.MOMENT_UNIT.Year))
        .valueOf(),
    });
  };

  handleItemResize = (itemId: any, time: any, edge: any) => {
    const updatedItems = this.state.items.map((item: any) => {
      if (item.id === itemId) {
        item.start_time =
          edge === constant.edge.Left.toString() ? time : item.start_time;
        item.end_time =
          edge === constant.edge.Right.toString() ? time : item.end_time;
      }
      return item;
    });
    this.setState({
      items: updatedItems,
    });
  };
  handleTodayClick = () => {
    const { startDate, endDate } = util.handleTodayClick(
      this.state,
      constant.CLICKEVENT_TYPE.Previous
    );
    this.setState({
      startDate: startDate,
      endDate: endDate,
    });
  };
  handleProjectTypeClick = async (projectType: string) => {
    this.props.loaderContext.open(true);
    this.projType = projectType;
    let _pageObj: IPaginationData = this.props.getPaginationDataFunc();
    _pageObj.currentPageNumber = 1; //reset page number when filtering data
    _pageObj.nextPageNumber = 2; //reset page number when filtering data
    const selectedProjectListData = this.state.isEmployee
      ? this.state.projectListDataEmployee
      : this.state.projectsListData;
    const updatedProjectsListData = await util.filterProjectData(
      selectedProjectListData,
      this.props.submittedFilterData,
      projectType,
      this.props.searchQuery,
      this.props.searchRoles,
      this.state.isEmployee,
      this.props?.userDetails?.username,
      _pageObj.limit,
      _pageObj.currentPageNumber
    );

    let oldServiceResponsData =
      _pageObj.currentPageNumber === 1
        ? ([] as any)
        : this.state.serviceResponseData;
    let _obj = updatedProjectsListData;
    oldServiceResponsData.push(..._obj);
    this.setState({ serviceResponseData: oldServiceResponsData });

    let dataLength = updatedProjectsListData.length;
    this.getProjectCount(dataLength);
    if (
      projectType.toUpperCase() ===
      constant.PROJECT_TYPE.OPEN.toString().toUpperCase()
    ) {
      this.getChargeType(1);
    } else if (
      projectType.toUpperCase() ===
      constant.PROJECT_TYPE.CLOSE.toString().toUpperCase()
    ) {
      this.getChargeType(2);
    } else {
      this.getChargeType(0);
    }
    this.props.SetupFilterDefaultValue(this.props.submittedFilterData);
    const data = this.state.isEmployee
      ? this.getGroupsForEmployee(updatedProjectsListData)
      : this.getGroups(updatedProjectsListData);

    this.setState({
      currentProjectData: updatedProjectsListData,
      groups: data.projectsList,
      items: data.itemsList,
    });
    this.props.loaderContext.open(false);
  };

  private setAccordianState(
    ListingValues: any,
    AllValues: any,
    data: { projectsList: TimelineGroupExt[]; itemsList: TimelineItemBaseExt[] }
  ) {
    this.setState({ isCalendarRefresh: false }, () => {
      this.setState({
        ...this.state,
        currentProjectData: ListingValues,
        projectsListData: AllValues,
        groups: data.projectsList,
        items: data.itemsList,
        isCalendarRefresh: true,
      });
    });
  }

  private calendarViewByCurrentView() {
    switch (this.props.currentView) {
      case constant.CALENDAR_VIEW.Month:
        this.handleMonthClick(this.props.selectedDate);
        break;
      case constant.CALENDAR_VIEW.Day:
        this.handleDayClick(this.props.selectedDate);
        break;
      case constant.CALENDAR_VIEW.Quater:
        this.handleQuaterClick(this.props.selectedDate);
        break;
      case constant.CALENDAR_VIEW.Half:
        this.handleHalfClick(this.props.selectedDate);
        break;
      case constant.CALENDAR_VIEW.Year:
        this.handleYearClick(this.props.selectedDate);
        break;
      case constant.CALENDAR_VIEW.Today:
        this.handleTodayClick();
        break;
    }
  }
}
