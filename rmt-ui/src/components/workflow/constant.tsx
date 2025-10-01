import { getDateFormate, getEmailId, routeToEmployeeProfile } from "../../global/utils";
import { Chip } from "@mui/material";
import NavigationControl, {
  getLabelForNavigationMyApprovalPage,
} from "./navigation-control";
import { PROJECT_TAB_DETAILS } from "../../global/constant";
import { getEmployeeAllocationStatus } from "../../global/workflow/workflow-utils";
import { getDateWithoutTime } from "../../utils/date/dateHelper";
import { WORKFLOW_MODULE } from "../../global/workflow/workflow-constant";

export const getWorkflowType = (workflowModule = "") => {
  switch (workflowModule.toUpperCase().trim()) {
    case WORKFLOW_MODULE.EMPLOYEE_ALLOCATION.toUpperCase().trim():
      return "Allocation";
    case WORKFLOW_MODULE.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toUpperCase().trim():
      return "Skill";
    default:
      break;
  }
};
export const getTaskDescription = (task: any) => {
  switch (task?.module?.toUpperCase()?.trim()) {
    case WORKFLOW_MODULE.EMPLOYEE_ALLOCATION.toUpperCase().trim():
      return `${
        task?.entity_meta_data?.JobCode
          ? task?.entity_meta_data?.JobCode
          : task?.entity_meta_data?.PipelineCode
      } - ${task?.entity_meta_data?.EmpName}`;
    case WORKFLOW_MODULE.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.toUpperCase().trim():
      return `${task?.entity_meta_data?.SkillName} - ${task?.entity_meta_data?.Proficiency}`;
    default:
      break;
  }
};
export const getTaskRequestedBy = (task) => {
  let val = task?.updated_by ? task?.updated_by : task?.created_by;
  return val ? getEmailId(val?.toString()) : "";
};

const dateFilterParams = {
  comparator: (filterLocalDateAtMidnight, cellValue) => {
    if (!cellValue) return -1;
    var cellDateWithoutTime = getDateWithoutTime(cellValue);
    var filterLocalDateAtMidnightWithoutTime = getDateWithoutTime(
      filterLocalDateAtMidnight
    );
    if (
      filterLocalDateAtMidnightWithoutTime.getTime() ===
      cellDateWithoutTime.getTime()
    ) {
      return 0;
    }
    if (cellDateWithoutTime < filterLocalDateAtMidnight) {
      return -1;
    }
    if (cellDateWithoutTime > filterLocalDateAtMidnight) {
      return 1;
    }
    return 0;
  },
  suppressAndOrCondition: true,
  inRangeFloatingFilterDateFormat: "DD MMM YYYY",
};
export const columnDefs = [
  {
    headerName: "Task Type",
    field: "workflow.module",
    flex: 1.1,
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    rowGroup: true,
    hide: true,
    cellRenderer: function (params: any) {
      return (
        <div className="my-approval-label">
          {getWorkflowType(params?.value)}
        </div>
      );
    },
  },
  {
    headerName: "Task Description",
    field: "workflow",
    flex: 1.1,
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    valueGetter: (params) => {
      return getTaskDescription(params?.data?.workflow);
    },
    tooltipValueGetter: (params) => {
      return getTaskDescription(params?.data?.workflow);
    },
  },
  {
    headerName: "Title",
    field: "workflow.module",
    flex: 1.1,
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "workflow.module",
    cellRenderer: function (params: any) {
      return (
        <div className="my-approval-label">
          {params?.value
            ?.replace("WORKFLOW_MODULE_", "")
            ?.replace("WORKFLOW_SUB_MODULE_", "")
            ?.replace(/_/g, " ")}
        </div>
      );
    },
  },

  {
    headerName: "Task Id",
    field: "workflow.item_id",
    flex: 1.1,
    valueGetter: (params) => {
      console.log(params);
      return getLabelForNavigationMyApprovalPage(params);
    },
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipValueGetter: (params) => {
      return params?.value;
    },
    cellClass: "hyperlinks",
    cellRenderer: function (params: any) {
      return (
        <div>
          {params?.value ? (
            <NavigationControl
              label={params.value}
              params={params}
              state={{
                workflow_task_id: params.value,
                tab: PROJECT_TAB_DETAILS.Active_Allocation,
              }}
            ></NavigationControl>
          ) : (
            ""
          )}
        </div>
      );
    },
  },
  {
    headerName: "Requested By",
    field: "workflow",
    filter: "agTextColumnFilter",
    filterParams: dateFilterParams,
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    valueGetter: (params: any) => {
      return getTaskRequestedBy(params?.data?.workflow);
    },
    tooltipValueGetter: (params: any) => {
      return getTaskRequestedBy(params?.data?.workflow);
    },
    cellRenderer: (params: any) => {
      const email=params?.data?.workflow?.updated_by ? params?.data?.workflow?.updated_by : params?.data?.workflow?.created_by
            return (
              <span
                style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
                onClick={() => {routeToEmployeeProfile(`/employee-profile/${email}`);}}
              >
                {params.value}
              </span>
            );
          },
  },
  {
    headerName: "Request On",
    field: "created_at",
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipValueGetter: (params) => {
      return params?.value ? getDateFormate(params?.value) : null;
    },
    width: 200,
    valueFormatter: (params: any) => {
      return params?.value ? getDateFormate(params?.value) : null;
    },
  },
  {
    headerName: "Due Date",
    field: "due_date",
    flex: 1,
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    // tooltipField: "due_date",
    tooltipValueGetter: (params) => {
      return params?.value ? getDateFormate(params?.value) : null;
    },
    cellRenderer: (params: any) => {
      return params?.value ? getDateFormate(params?.value) : null;
    },
  },
  {
    headerName: "Status",
    field: "workflow.status",
    flex: 1.1,
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipValueGetter: (value: any) => {
      return getEmployeeAllocationStatus(
        value?.data?.workflow.status?.toString()
      )?.allocationStatus;
    },
    valueGetter: (value: any) => {
      return getEmployeeAllocationStatus(
        value?.data?.workflow.status?.toString()
      )?.allocationStatus;
    },
    cellRenderer: (value: any) => {
      const status = value?.data?.workflow.status;
      let color = "";
      let backgroundColor = "";
      let UIStatus = getEmployeeAllocationStatus(
        status?.toString()
      )?.allocationStatus;
      let uiBg = getEmployeeAllocationStatus(status?.toString())?.color;
      backgroundColor = uiBg;
      let fontColor = getEmployeeAllocationStatus(status?.toString())?.bgColor;
      color = fontColor;
      return status ? (
        <Chip
          label={UIStatus || ""}
          key={status || ""}
          style={{
            width: "100%",
            color: color,
            backgroundColor: backgroundColor,
          }}
        ></Chip>
      ) : (
        ""
      );
    },
  },
];

export const defaultColDef = {
  lockVisible: true,
  resizable: true,
};

export interface IGetTaskPayload {
  employeeEmail?: string;
  assigned_to?: string;
  outcome?: string;
  taskstatus?: string;
  module?: string;
  sub_module?: string;
  workflow_task_status?: string;
}

export interface IWorkflowUpdateTask {
  workflow_id?: string;
  item_id?: string;
  workflow_task_id?: string;
  assigned_to?: string;
  status?: string;
  remarks?: string;
  workflow_task_title?: string;
}
