import React from "react";
import * as constant from "./constant";
import moment from "moment";
import { IRequisitions } from "./Idata";
import { Box, Grid, Tooltip } from "@mui/material";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import { IResourcesMenuProps } from "../../scheduler-left/resource-menu/IResourcesMenuProps";
import EmployeeContextMenu from "../../scheduler-left/employee-context-menu/employee-context-menu";
import { getEmployeeAllocationStatus } from "../../../global/workflow/workflow-utils";
import {
  EMPLOYEE_VIEW_ROLES,
  GT_DESIGN_PARAMETERS,
  REQUESTOR_VIEW_ROLES,
  RESPONSE_STATUS,
} from "../../../global/constant";

import "./style.css";
import IFilterQueryParameters, {
  IProjectList,
} from "../../../services/project-list-services/IProjectList";
import { getAllProjectListByRequestorEmail } from "../../../services/project-list-services/project-list-services";
import { getAllFilterParameters } from "../../../services/project-list-services/get-master-services";
import {
  getAllocatedHoursRatioByPipelineCode,
  getProjectsOfEmployeeByEmailAndPipelineCode,
} from "../../../services/big-calendar-services/bigcalendar.service";
import { EPipelineStatus } from "../../../common/enums/EProject";
import { getEmailId, routeToEmployeeProfile } from "../../../global/utils";
import { ITimelineDisplayType } from "../../calendar/Utils";

export const getChildTilte = (
  pipelineCode: string,
  title: string,
  status: string,
  employeeInfo: any,
  contextMenuClickHandler: any,
  project: any
) => {
  const ResourcesMenuData: IResourcesMenuProps = {
    projectId: pipelineCode,
    employeeInfo: employeeInfo,
    project: project,
    contextMenuClickHandler: contextMenuClickHandler,
  };
  const allocationStatus = getEmployeeAllocationStatus(status).allocationStatus;

  return (
    <>
      <Grid container className={`react-employee-title`}>
        <Grid item xs={2} sx={constant.VerticalCenterAlignSxProps}>
          <PersonOutlineOutlinedIcon
            sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }}
          />
        </Grid>
        <Grid item xs={7} sx={constant.VerticalCenterAlignSxProps}>
          <Grid container>
            <Grid
              item
              xs={7}
              style={{ font: "caption" }}
              sx={constant.VerticalCenterAlignSxProps}
            >
              <span onClick={()=>routeToEmployeeProfile(`/employee-profile/${employeeInfo.empEmail}`)} 
              title={getEmailId(employeeInfo.empEmail)}
              style={{ cursor: "pointer" }}>{title}</span>
              <span style={{ display: "none" }}>{employeeInfo.listId}</span>
            </Grid>
            {allocationStatus && allocationStatus.length > 0 && (
              <Tooltip
                title={getEmployeeAllocationStatus(status).allocationStatus}
              >
                <Grid
                  item
                  xs={12}
                  style={{
                    font: "small-caption",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                  }}
                >
                  {getEmployeeAllocationStatus(status).allocationStatus}
                </Grid>
              </Tooltip>
            )}
          </Grid>
        </Grid>
        <Grid
          item
          xs={1}
          sx={constant.VerticalCenterAlignSxProps}
          className="employee-context-menu"
        >
          <EmployeeContextMenu {...ResourcesMenuData} />
        </Grid>
      </Grid>
    </>
  );
};

export const handlePreorNextClick = (
  state: any,
  ClickEventType: constant.CLICKEVENT_TYPE
) => {
  const addValue = constant.CLICKEVENT_TYPE.Next === ClickEventType ? 1 : -1;
  switch (state.currentView) {
    case constant.CALENDAR_VIEW.Month.toString():
      return {
        startDate: moment(state.startDate)
          .add(addValue, "month")
          .startOf("month"),
        endDate: moment(state.endDate).add(addValue, "month").endOf("month"),
      };
    case constant.CALENDAR_VIEW.Day.toString():
      return {
        startDate: moment(state.startDate).add(addValue, "day").startOf("day"),
        endDate: moment(state.endDate).add(addValue, "day").endOf("day"),
      };
    case constant.CALENDAR_VIEW.Quater.toString():
      return {
        startDate: moment(state.startDate)
          .add(addValue, "quarter")
          .startOf("quarter"),
        endDate: moment(state.endDate)
          .add(addValue, "quarter")
          .endOf("quarter"),
      };
    case constant.CALENDAR_VIEW.Year.toString():
      return {
        startDate: moment(state.startDate)
          .add(addValue, "year")
          .startOf("year"),
        endDate: moment(state.endDate).add(addValue, "year").endOf("year"),
      };

    case constant.CALENDAR_VIEW.Half.toString():
      return getHalfViewData(state, ClickEventType);
    default:
      return {
        startDate: null,
        endDate: null,
      };
  }
};

export const handleSelectedDateClick = (
  selectedDate: Date,
  currentView: string
) => {
  switch (currentView.toUpperCase()) {
    case constant.CALENDAR_VIEW.Month.toString().toUpperCase():
      return {
        startDate: moment(selectedDate).startOf("month"),
        endDate: moment(selectedDate).endOf("month"),
      };
    case constant.CALENDAR_VIEW.Day.toString().toUpperCase():
      return {
        startDate: moment(selectedDate).startOf("day"),
        endDate: moment(selectedDate).endOf("day"),
      };
    case constant.CALENDAR_VIEW.Quater.toString().toUpperCase():
      return {
        startDate: moment(selectedDate).startOf("quarter"),
        endDate: moment(selectedDate).endOf("quarter"),
      };
    case constant.CALENDAR_VIEW.Year.toString().toUpperCase():
      return {
        startDate: moment(selectedDate).startOf("year"),
        endDate: moment(selectedDate).endOf("year"),
      };
    case constant.CALENDAR_VIEW.Half.toString().toUpperCase():
      return getHalfYearOfDate(selectedDate);
    default:
      return {
        startDate: null,
        endDate: null,
      };
  }
};

export const handleTodayClick = (
  state: any,
  ClickEventType: constant.CLICKEVENT_TYPE
) => {
  switch (state.currentView) {
    case constant.CALENDAR_VIEW.Month.toString():
      return {
        startDate: moment().startOf("month"),
        endDate: moment().endOf("month"),
      };
    case constant.CALENDAR_VIEW.Day.toString():
      return {
        startDate: moment().startOf("day"),
        endDate: moment().endOf("day"),
      };
    case constant.CALENDAR_VIEW.Quater.toString():
      return {
        startDate: moment().startOf("quarter"),
        endDate: moment().endOf("quarter"),
      };
    case constant.CALENDAR_VIEW.Year.toString():
      return {
        startDate: moment().startOf("year"),
        endDate: moment().endOf("year"),
      };

    case constant.CALENDAR_VIEW.Half.toString():
      return getHalfYearOfDate();
    default:
      return {
        startDate: null,
        endDate: null,
      };
  }
};

const getHalfViewData = (
  state: any,
  ClickEventType: constant.CLICKEVENT_TYPE
) => {
  const month = moment(state.startDate).month();
  const year = moment(state.startDate).year();
  if (constant.CLICKEVENT_TYPE.Next === ClickEventType) {
    if (month < 6) {
      return {
        startDate: moment(new Date(year, 6, 1)),
        endDate: moment(new Date(year, 11, 31)),
      };
    } else {
      return {
        startDate: moment(new Date(year + 1, 0, 1)),
        endDate: moment(new Date(year + 1, 5, 30)),
      };
    }
  } else {
    if (month >= 6) {
      return {
        startDate: moment(new Date(year, 0, 1)),
        endDate: moment(new Date(year, 5, 30)),
      };
    } else {
      return {
        startDate: moment(new Date(year - 1, 6, 1)),
        endDate: moment(new Date(year - 1, 11, 31)),
      };
    }
  }
};

const getHalfYearOfDate = (selectedDate: Date = new Date()) => {
  const month = moment(selectedDate).month();
  const year = moment(selectedDate).year();
  if (month < 6) {
    return {
      startDate: moment(new Date(year, 0, 1)),
      endDate: moment(new Date(year, 5, 30)),
    };
  } else {
    return {
      startDate: moment(new Date(year, 6, 1)),
      endDate: moment(new Date(year, 11, 31)),
    };
  }
};

export const upateItemDateTime = (items: any, itemId: any, time: any) => {
  // const updatedItems = items.map((item: any) => {
  //   if (item.id === itemId) {
  //     item.start_time = time;
  //   }
  //   return item;
  // });
};

export const getRequisitionsReducedByDesignation = (
  requisitions: IRequisitions[]
) => {
  const designationBasedObj: any = {};

  requisitions.reduce((t, v) => {
    if (designationBasedObj[v.designation]) {
      designationBasedObj[v.designation].push(v);
    } else {
      designationBasedObj[v.designation] = [v];
    }
    return t;
  }, {});
  return designationBasedObj;
};

export const childRequisitionTimeline = (
  item: IRequisitions,
  propsdata: any
) => {
  return (
    <Grid container>
      <Grid item xs={2}></Grid>
      <Grid item xs={8} sx={constant.VerticalCenterAlignSxProps}>
        <Box>{item.designation + " " + item.resource_number}</Box>
      </Grid>
      {/* <Grid item xs={2} sx={constant.VerticalCenterAlignSxProps}>
        <RequeisitionAllocatorMenu {...item} {...propsdata} />
      </Grid> */}
    </Grid>
  );
};

export const getProjectTitle = (data: any) => {
  // let projectList: any[];
  // data.map((project : any) => {
  // count = 1;
  // projectList.push({
  //   id: count,
  //   isHeader: false,
  //   defaultExpanded: false,
  //   details: [],
  //   allocation_status: '',
  //   job_id:
  // })
  // return data
  // })
};
/*
  IT TAKES THE ARRAY OF DATA AND FIND OUT THE UNIQUES CLIENT NAME , EXPERTISE etc FOR THE FILTERS 
*/
interface ProjectFilterExpertise {
  bu: string;
  name: string;
}
interface ProjectFilterSmeg {
  expertise: string;
  name: string;
}
interface ProjectFilterOffering {
  bu: string;
  name: string;
}
interface ProjectFilterSolution {
  offerings: string;
  name: string;
}
export interface IFilterOptions {
  distinctClientNames: string[];
  distinctBUSet: string[];
  distinctOfferings: ProjectFilterOffering[];
  distinctSolutions: ProjectFilterSolution[];
  distinctExpertises: ProjectFilterExpertise[];
  distinctSmes: ProjectFilterSmeg[];
  distinctPipelines: string[];
  distinctJobs: string[];
  distinctJobNames: string[];
  status: string[];
  projectType: string[];
  marketPlaceType: string[];
  distinctIndustry: string[];
  distinctSubIndustry: string[];
  revenueUnit: string[];
}
export const getFilterOptionData = async () => {
  try {
    let obj: IFilterOptions = await getAllFilterParameters();
    // console.log(obj);
    if (obj && Object.keys(obj).length > 0) {
      Object.keys(obj).map((item) => {
        // console.log(obj[item]);
        // obj[item] = obj[item]?.map((x) => (x == null ? "" : x));
        obj[item] = obj[item]?.filter((x) => {
          if (typeof x === "string") {
            return x ? true : false;
          } else {
            if (x && x.name) {
              return true;
            } else {
              return false;
            }
          }
        });
      });
    }
    // console.log(obj);
    return obj;
  } catch (err) {
    throw err;
  }
};

/*
  Used To Filter PROJECTLIST on the basis of FILTERPARAMETERS PROVIDED
  For Both Requestor And Employee Type
*/
export const mapFilterParam = (filterParameters: any) => {
  const filterParam: IFilterQueryParameters = {};
  if (filterParameters?.bu && filterParameters.bu.length) {
    filterParam.Bu = filterParameters?.bu;
  }
  if (filterParameters?.experties && filterParameters.experties.length) {
    filterParam.Expertises = filterParameters?.experties;
  }
  if (filterParameters?.offering && filterParameters?.offering.length) {
    filterParam.Offerings = filterParameters?.offering;
  }
  if (filterParameters?.solution && filterParameters?.solution.length) {
    filterParam.Solutions = filterParameters?.solution;
  }
  if (filterParameters?.sme && filterParameters.sme.length) {
    filterParam.Smes = filterParameters?.sme;
  }
  if (filterParameters?.industry && filterParameters.industry.length) {
    filterParam.Industry = filterParameters?.industry;
  }
  if (filterParameters?.subIndustry && filterParameters.subIndustry.length) {
    filterParam.SubIndustry = filterParameters?.subIndustry;
  }
  if (filterParameters?.revenueUnit && filterParameters.revenueUnit.length) {
    filterParam.RevenueUnit = filterParameters?.revenueUnit;
  }
  if (filterParameters?.clientName && filterParameters.clientName.length) {
    filterParam.ClientNames = filterParameters?.clientName;
  }
  if (filterParameters?.job && filterParameters.job.length) {
    filterParam.JobCodes = filterParameters?.job;
  }
  if (filterParameters?.jobName && filterParameters.jobName.length) {
    filterParam.JobName = filterParameters?.jobName;
  }
  if (filterParameters?.pipeline && filterParameters.pipeline.length) {
    const pipelineCode = filterParameters?.pipeline?.map((pipeline) =>
      pipeline.split("-")[0].trim()
    );
    filterParam.PipelineCodes = pipelineCode;
  }
  if (
    filterParameters?.marketPlaceType &&
    filterParameters?.marketPlaceType?.length
  ) {
    filterParam.MarketPlace =
      filterParameters?.marketPlaceType === "Yes" ? true : false;
  }
  if (filterParameters?.status && filterParameters?.status.length) {
    filterParam.ProjectStatus = filterParameters?.status;
  }
  if (filterParameters?.projectType && filterParameters?.projectType.length) {
    filterParam.ProjectChargeType = filterParameters?.projectType;
  }
  return filterParam;
};

export const filterProjectData = (
  projectList: any[],
  filterParameters: any,
  projType: any,
  searchQuery: any,
  searchRoles: string[],
  isEmployee: boolean,
  userEmail: string,
  limit?: number,
  pageNumber?: number
) => {
  const params: IFilterQueryParameters = mapFilterParam(filterParameters);
  //params.ProjectChargeType = chargeType === "ALL" ? null : chargeType;
  params.ProjectType = projType === "ALL" ? null : projType;
  const request: IProjectList = {
    userEmail: userEmail,
    pagination: pageNumber,
    limit: limit,
    filterQueryParameters: params,
    searchRoles: searchRoles && searchRoles.length > 0 ? searchRoles : null,
    searchQuery: searchQuery,
  };
  return Promise.all([getAllProjectListByRequestorEmail(request)]).then(
    (resp) => {
      if (resp.length > 0 && resp[0].status === RESPONSE_STATUS.Success) {
        return resp[0].data;
      } else {
        return [];
      }
    }
  );
};

export const fiterRequestorProjectList = (
  projectList: any[],
  filterParameters: any,
  chargeType: any,
  isEmployee: boolean
) => {
  if (
    filterParameters &&
    (filterParameters.bu === undefined || filterParameters.bu.length === 0) &&
    (filterParameters?.experties === undefined ||
      filterParameters.experties.length === 0) &&
    (filterParameters?.sme === undefined ||
      filterParameters?.sme?.length === 0) &&
    (filterParameters?.industry === undefined ||
      filterParameters?.industry?.length === 0) &&
    (filterParameters?.subIndustry === undefined ||
      filterParameters?.subIndustry?.length === 0) &&
    (filterParameters?.clientName === undefined ||
      filterParameters?.clientName?.length === 0) &&
    (filterParameters?.pipeline === undefined ||
      filterParameters?.pipeline?.length === 0) &&
    (filterParameters?.job === undefined ||
      filterParameters?.job?.length === 0) &&
    (filterParameters?.status === undefined ||
      filterParameters?.status?.length === 0) &&
    (filterParameters?.projectType === undefined ||
      filterParameters?.projectType?.length === 0) &&
    (filterParameters?.marketPlaceType === undefined ||
      filterParameters?.marketPlaceType?.length === 0)
  ) {
    if (
      chargeType.toUpperCase() ===
      constant.PROJECT_CHARGE_TYPE.All.toString().toUpperCase()
    ) {
      return projectList;
    } else if (
      chargeType.toUpperCase() ===
      constant.PROJECT_CHARGE_TYPE.Chargable.toString().toUpperCase()
    ) {
      return projectList.filter(
        (data: any) =>
          data.chargableType.toUpperCase() ===
          constant.PROJECT_CHARGE_TYPE.Chargable.toString()
      );
    } else {
      return projectList.filter(
        (data: any) =>
          data.chargableType.toUpperCase() ===
          constant.PROJECT_CHARGE_TYPE.NonChargable.toString()
      );
    }
  }
  let projectListWithChargeType = projectList;
  if (
    chargeType.toUpperCase() !==
    constant.PROJECT_CHARGE_TYPE.All.toString().toUpperCase()
  ) {
    projectListWithChargeType = projectList.filter(
      (data: any) =>
        data.chargableType.toUpperCase() === chargeType.toUpperCase()
    );
  }
  const list = projectListWithChargeType.filter((data) => {
    if (
      (filterParameters.bu && filterParameters.bu.includes(data.bu)) ||
      (filterParameters.experties &&
        filterParameters.experties.includes(data.expertise)) ||
      (filterParameters.sme && filterParameters.sme.includes(data.sme)) ||
      (filterParameters.industry &&
        filterParameters.industry.includes(data.industry)) ||
      (filterParameters.subIndustry &&
        filterParameters.subIndustry.includes(data.subIndustry)) ||
      (filterParameters.clientName &&
        filterParameters.clientName.includes(data.clientName)) ||
      (filterParameters.pipeline &&
        filterParameters.pipeline.includes(
          data.pipelineCode + " - " + data.pipelineName
        )) ||
      (filterParameters.job &&
        filterParameters.job.includes(
          !isEmployee ? data.jobCode : data.jobCode
          // !isEmployee ? data.projectCode : data.jobCode
        )) ||
      (filterParameters.status &&
        filterParameters.status.includes(data.pipelineStatus)) ||
      (filterParameters.projectType &&
        filterParameters.projectType.includes("Active") &&
        new Date(data.endDate) >= new Date() &&
        (data.pipelineStatus !== EPipelineStatus.Suspended ||
          data.pipelineStatus !== EPipelineStatus.Lost)) ||
      (filterParameters.projectType &&
        filterParameters.projectType.includes("Closed") &&
        new Date(data.endDate) < new Date() &&
        (data.pipelineStatus !== EPipelineStatus.Suspended ||
          data.pipelineStatus !== EPipelineStatus.Lost)) ||
      (filterParameters.marketPlaceType &&
        data.isPublishedToMarketPlace ===
          (filterParameters.marketPlaceType === "Yes"))
    ) {
      return true;
    } else {
      return false;
    }
  });
  return list;
};

export const GetDistinctPipelineCodeForEmployees = (
  employeeProjectList: any[]
) => {
  const pipelineSet = new Set<string>();
  employeeProjectList.forEach((data: any) =>
    pipelineSet.add(data.pipelineCode)
  );
  return Array.from(pipelineSet);
};

export const ProjectCountByDistinctPipelineCode = (projectData: any[]) => {
  return GetDistinctPipelineCodeForEmployees(projectData).length;
};

export const getProjectByCode = (
  projectsListData: any[],
  pipelineCode: string,
  jobCode: string
) => {
  return projectsListData.filter(
    (a) => a.pipelineCode === pipelineCode && a.jobCode === jobCode
  );
};
export const isEmployeeRoles = (projectRoleList: any, emailId: string) => {
  if (projectRoleList) {
    //projectRolesView change
    const _role = projectRoleList.projectRolesView
      ?.filter((b) => (b.user = emailId))
      .map((a) => {
        return (
          a.role.includes(EMPLOYEE_VIEW_ROLES) &&
          !a.role.includes(REQUESTOR_VIEW_ROLES)
        );
      });
    return _role?.length > 0 && _role.indexOf(true) !== -1;
  } else {
    return false;
  }
};

export const getTimelineBarColor = (type: string, projectStatus?: string) => {
  let colorClass = "";
  if (
    projectStatus &&
    (projectStatus.toLocaleLowerCase() ===
      constant.ItemType.Suspended.toLocaleLowerCase() ||
      projectStatus.toLocaleLowerCase() ===
        EPipelineStatus.Lost.toLocaleLowerCase())
  ) {
    return "item-suspended";
  }
  switch (type) {
    case constant.GroupType.Job:
      colorClass = "item-job";
      break;
    case constant.GroupType.Pipeline:
      colorClass = "item-pipeline";
      break;
    case constant.GroupType.Employee:
      colorClass = "item-employee";
      break;
    case constant.ItemType.Allocation:
      colorClass = "item-allocation";
      break;
    case constant.ItemType.JobDuration:
      colorClass = "item-jobduration";
      break;
    case constant.ItemType.PipelineDuration:
      colorClass = "item-pipelineduration";
      break;
    case constant.ItemType.Available:
      colorClass = "item-available";
      break;
    // case constant.ItemType.Leave:
    //   colorClass = "item-leave";
    //   break;
    case constant.ItemType.Holiday:
      colorClass = "item-holiday";
      break;
    case constant.ItemType.Rollover:
      colorClass = "item-gray";
      break;
    // case constant.ItemType.RolloverForward:
    //   colorClass = "item-rolloverproposed";
    //   break;
    case constant.ItemType.FULL_DAY_LEAVE:
      colorClass = "item-leave";
      break;
    case constant.ItemType.FIRST_HALF_LEAVE:
      colorClass = "item-first-half-leave";
      break;
    case constant.ItemType.SECOND_HALF_LEAVE:
      colorClass = "item-second-half-leave";
      break;
    default:
      colorClass = "item-color";
      break;
  }

  return colorClass;
};

export const GetTitleOfRow = (timeline_type: string, hours: any) => {
  if (
    timeline_type?.toLowerCase()?.trim() ===
      constant.ItemType.Allocation.toLowerCase().trim() ||
    timeline_type?.toLowerCase()?.trim() ===
      constant.ItemType.FIRST_HALF_LEAVE.toLowerCase().trim() ||
    timeline_type?.toLowerCase()?.trim() ===
      constant.ItemType.SECOND_HALF_LEAVE.toLowerCase().trim()
  ) {
    return hours + " hours";
  } else if (
    timeline_type?.toLowerCase()?.trim() ===
    constant.ItemType.FULL_DAY_LEAVE.toLowerCase().trim()
  ) {
    return ITimelineDisplayType.FULL_DAY_LEAVE;
  } else if (
    timeline_type?.toLowerCase()?.trim() ===
    constant.ItemType.Holiday.toLowerCase().trim()
  ) {
    return ITimelineDisplayType.HOLIDAY;
  } else {
    return ITimelineDisplayType.FULL_DAY_LEAVE;
  }
  // timeline_type?.toLowerCase().trim() ===
  //               ItemType.Allocation?.toLowerCase().trim()
  //                 ? detail.confirmedPerDayHours + " hours"
  //                 : detail.timeline_type?.toLowerCase().trim() ===
  //                   ItemType.Holiday
  //                 ? ItemType.Holiday
  //                 : ItemType.Leave,
};

export const getPipelinNameOrJobName = (item: any) => {
  if (item.jobCode == null) {
    return item.pipelineName;
  } else {
    return item.jobName;
  }
};

export const GetProjectsOfEmployeeByEmailAndPipelineCode = (
  emailId: string,
  pipelineCode: string
) => {
  return new Promise((resolve, reject) => {
    getProjectsOfEmployeeByEmailAndPipelineCode(emailId, pipelineCode)
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((ex) => {});
  });
};

export const GetAllocatedHoursRatioByPipelineCode = (pipelineCode: string) => {
  return new Promise((resolve, reject) => {
    getAllocatedHoursRatioByPipelineCode(pipelineCode)
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((ex) => {});
  });
};

enum ProjectActivationStatus {
  ACTIVE = "Active",
  IN_ACTIVE = "In Active",
}

enum ProjectClosureStatus {
  OPEN = "Open",
  CLOSED = "Closed",
}

const projectStatusSeparator = "-";

export function getProjectStateByActivationAndClosureState(
  projectClosureState: boolean | null | undefined,
  projectActivationState: boolean | null | undefined
): string | null {
  if (projectClosureState == null && projectActivationState == null) {
    return null;
  } else if (projectClosureState == null && projectActivationState != null) {
    return projectActivationState
      ? ProjectActivationStatus.ACTIVE
      : ProjectActivationStatus.IN_ACTIVE;
  } else if (projectClosureState != null && projectActivationState == null) {
    return !projectClosureState
      ? ProjectClosureStatus.OPEN
      : ProjectClosureStatus.CLOSED;
  } else {
    return (
      (!projectClosureState
        ? ProjectClosureStatus.OPEN
        : ProjectClosureStatus.CLOSED) +
      projectStatusSeparator +
      (projectActivationState
        ? ProjectActivationStatus.ACTIVE
        : ProjectActivationStatus.IN_ACTIVE)
    );
  }
}
