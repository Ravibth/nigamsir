/* eslint-disable array-callback-return */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useEffect, useMemo, useRef, useState, useContext } from "react";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import moment from "moment";
import { Chip } from "@mui/material";
import { dateFilterParams } from "../../../utils/date/dateHelper";
import { IWorkflowUpdateTask } from "../../workflow/constant";
import AllocationActionCell from "./allocationActionCell";
import { getEmployeeAllocationStatus } from "../../../global/workflow/workflow-utils";
import { dateFormat, defaultColDef } from "./constant";
import { GlobalConfigs } from "../../../global/constant";
import { AgGridReact } from "ag-grid-react";
import { LoaderContext } from "../../../contexts/loaderContext";
import "./style.css";
import UpdateAllocationCommonScreen from "../../common-allocation/update-allocation-common-screeen/update-allocation-common-screeen";
import { ENavigatingFromToUpdateCommonScreen } from "../../common-allocation/enum";
import {
  getEmailId,
  getGridToolTipValue,
  IsProjectInActiveOrClosed,
  routeToEmployeeProfile,
} from "../../../global/utils";
import { EPipelineStatus } from "../../../common/enums/EProject";
import { WorkflowStatus } from "../../../global/workflow/workflow-constant";

const AllocationAggrid = (props: any) => {
  const {
    allocationList,
    handleClick,
    myPendingTasks,
    userEmailId,
    handleOpen,
    withdrawAllocation,
    terminateAllocation,
  } = props;
  const gridRef: any = useRef();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>();
  const open = Boolean(anchorEl);
  const [isUpdateAllocationOpen, setIsUpdateAllocationOpen] = useState(false);
  const [requisitionId, setRequisitionId] = useState<number | null>(null);
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const isSuspended =
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const loaderContext: any = useContext(LoaderContext);
  const [pendingTask, setPendingTask] = useState([]);
 
  useEffect(() => {
    if (gridRef) {
      loaderContext.open(true);
      setTimeout(() => {
        defaultSelectTask();
        loaderContext.open(false);
      }, 5000);
    }
    setPendingTask(myPendingTasks);
  }, [myPendingTasks]);

  const gridOptions = {
    suppressCellSelection: true,
    rowSelection: "multiple",
    isRowSelectable: (params: any) => {
      if (params && myPendingTasks) {
        const _mytask = myPendingTasks.filter(
          (a: any) =>
            a.workflow.item_id === params.data?.id &&
            (a?.isWithdrawl
              ? !(
                  a.workflow.status ===
                    WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE_PENDING_FOR_RR ||
                  a.workflow.status ===
                    WorkflowStatus.EMPLOYEE_ALLOCATION_REJECTED_BY_EMPLOYEE
                )
              : !(
                  a.title ===
                  "Employee Allocation Task For Resource Requestor After Employee Rejection For Termination"
                ))
        );
        return _mytask?.length > 0;
      } else {
        return false;
      }
    },
  };

  useEffect(() => {
    if (
      props.allocationList.length > 0 &&
      props.defaultSelectedNodes &&
      gridComponentRef?.current?.api
    ) {
      gridComponentRef?.current?.api.forEachNode((node: any) => {
        if (node.data.id === props.defaultSelectedNodes) {
          node.setSelected(true);
        }
      });
    }
  }, [props.allocationList, props.defaultSelectedNodes]);

  const onGridReady = (params: any) => {
    params.columnApi.autoSizeAllColumns();
    const gridColumnApi = params.columnApi;
    const allColumnIds: string[] = [];
    if (gridColumnApi !== undefined) {
      gridColumnApi.getColumns().forEach((column: any) => {
        allColumnIds.push(column.getId());
      });
      gridColumnApi.autoSizeColumns(allColumnIds);
      gridColumnApi.sizeColumnsToFit();
    }
  };

  const EndDate = (data: any) => {
    const endDate = moment(data.value, dateFormat).format(
      GlobalConfigs.dateFormat
    );
    return endDate;
  };

  const StartDate = (data: any) => {
    const startDate = moment(data.value, dateFormat).format(
      GlobalConfigs.dateFormat
    );
    return startDate;
  };

  const defaultSelectTask = () => {
    gridRef?.current?.gridComponentRef.current.api.forEachNode((node: any) => {
      // if (selectedFiles.find((item) => item.id === node.data.id)) {
      node.setSelected(true);
      // }
    });
  };
  const updateAllocation = (event: any, reqId: any) => {
    event.preventDefault();
    setRequisitionId(reqId);
    setIsUpdateAllocationOpen(true);
  };
  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Actions",
        field: "actions",
        // flex: 1,
        minWidth: 130,
        suppressMenu: true,
        tooltipField: "action",
        cellRenderer: AllocationActionCell,
        cellRendererParams: {
          handleClick,
          open,
          anchorEl,
          updateAllocation,
          // handleClose,
          handleOpen,
          myPendingTasks,
          withdrawAllocation,
          terminateAllocation,
          isSuspended,
          isProjectInActiveOrClosed: IsProjectInActiveOrClosed(
            props?.projectDetails
          ),
          projectDetails: props?.projectDetails,
        },
      },
      {
        headerName: "Employee Name",
        headerTooltip: "Employee Name",
        field: "empName",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "empName",
        checkboxSelection: true,
        showDisabledCheckboxes: true,
        cellRenderer: (params: any) => {
          return (
            <span
              style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
              title={params.value}
              onClick={() => {
                routeToEmployeeProfile(`/employee-profile/${params.data.empEmail}`);
              }}
            >
              {params.value}
            </span>
          );
        },
      },
      {
        headerName: "Designation",
        headerTooltip: "Designation",
        field: "requisition.designation",
        // flex: 0.8,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "requisition.designation",
        menuTabs: ["filterMenuTab"],
        width: 160,
      },
      {
        headerName: "Status",
        headerTooltip: "Status",
        field: "allocationStatus",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: (value: any) => {
          return getEmployeeAllocationStatus(value.value.toString())
            .allocationStatus;
        },
        valueGetter: (params: any) => {
          // Return the raw value of the cell
          return getEmployeeAllocationStatus(params.data.allocationStatus)
            .allocationStatus;
        },
        cellRenderer: (params: any) => {
          // Render the chip component based on the raw value
          const UIStatus = params.value;
          const uiBg = getEmployeeAllocationStatus(
            params.data.allocationStatus
          ).color;
          const fontColor = getEmployeeAllocationStatus(
            params.data.allocationStatus
          ).bgColor;
          return (
            <Chip
              label={UIStatus}
              key={params.value}
              style={{
                width: "100%",
                color: fontColor,
                backgroundColor: uiBg,
              }}
            />
          );
        },
      },
      {
        headerName: "Competency",
        headerTooltip: "Competency",
        field: "requisition.competency",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "requisition.competency",
      },
      {
        headerName: "Start Date",
        headerTooltip: "Start Date",
        field: "startDate",
        // flex: 0.7,
        filter: "agDateColumnFilter",
        filterParams: dateFilterParams,
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: StartDate,
        cellRenderer: StartDate,
        width: 160,
      },
      {
        headerName: "End Date",
        headerTooltip: "End Date",
        field: "endDate",
        // flex: 0.7,
        filter: "agDateColumnFilter",
        filterParams: dateFilterParams,
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: EndDate,
        cellRenderer: EndDate,
        width: 160,
      },
      {
        headerName: "No. of Hours",
        headerTooltip: "No. of Hours",
        field: "totalEffort",
        // flex: 0.8,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "totalEffort",
        width: 160,
      },
      {
        headerName: "Skills",
        headerTooltip: "Skills",
        field: "skills",
        // flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: function (params: any) {
          return params?.data?.skills
            ?.map((skillItem) => skillItem?.skillName)
            .join(", ");
        },
        valueGetter: function (params: any) {
          return params?.data?.skills
            ?.map((skillItem) => skillItem?.skillName)
            .join(", ");
        },
      },
      {
        headerName: "Allocated By",
        headerTooltip: "Allocated By",
        field: "createdBy",
        // flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: getGridToolTipValue,
        cellRenderer: function (params: any) {
          return <>{getEmailId(params.value)}</>;
        },
        valueGetter: function (params: any) {
          if (
            params?.data &&
            params?.data?.createdBy &&
            params?.data?.createdBy.indexOf("__") > -1
          ) {
            return params?.data?.createdBy.split("__")[1];
          } else return params?.data?.createdBy;
        },
      },
      {
        headerName: "Grade",
        headerTooltip: "Grade",
        field: "requisition.grade",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "requisition.grade",
        menuTabs: ["filterMenuTab"],
        width: 160,
      },

      {
        headerName: "Employee Email",
        headerTooltip: "Employee Email",
        field: "empEmail",
        // flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: getGridToolTipValue,
        cellRenderer: function (params: any) {
          return <>{getEmailId(params.value)}</>;
        },
        valueGetter: function (params: any) {
          if (
            params?.data &&
            params?.data?.empEmail &&
            params?.data?.empEmail.indexOf("__") > -1
          ) {
            return params?.data?.empEmail.split("__")[1];
          } else return params?.data?.empEmail;
        },
      },
    ],
    [allocationList, pendingTask]
  );
  function onSelectionChanged(e: any) {
    var selectedRows = e.api.getSelectedRows();
    const workflow_task_Update_payload: IWorkflowUpdateTask[] =
      [] as IWorkflowUpdateTask[];
    selectedRows.map((row: any) => {
      const _task = pendingTask.find((a: any) => a.workflow.item_id === row.id);
      if (_task) {
        workflow_task_Update_payload.push({
          workflow_id: _task.workflow_id,
          item_id: row.id,
          workflow_task_id: _task.id,
          assigned_to: userEmailId,
          status: "",
          remarks: "",
          workflow_task_title: _task.title,
        });
      }
    });
    props.actionButtonShowOrHide({
      showActionButton: selectedRows.length > 0,
      selectedTasks: workflow_task_Update_payload,
    });
  }

  return (
    <>
      {isUpdateAllocationOpen && requisitionId && (
        <UpdateAllocationCommonScreen
          isModalOpen={isUpdateAllocationOpen}
          handleCloseModal={function (): void {
            setRequisitionId(null);
            setIsUpdateAllocationOpen(false);
          }}
          projectInfo={props.projectDetails}
          requisitionIds={[requisitionId]}
          pipelineCode={props.projectDetails.pipelineCode}
          jobCode={props.projectDetails.jobCode}
          navigatingFrom={ENavigatingFromToUpdateCommonScreen.ALLOCATIONS_TAB}
        />
      )}

     

      <div className="allocation-agGrid">
        <AgGridComponent
          ref={gridRef}
          gridComponentRef={gridComponentRef}
          rowData={allocationList}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={true}
          suppressContextMenu={true}
          suppressExcelExport={true}
          isFilterVisible={true}
          hideExport={false}
          gridOptions={gridOptions}
          suppressCellFocus={true}
          onGridReady={onGridReady}
          onSelectionChanged={onSelectionChanged}
          rowSelection={"multiple"}
          rowMultiSelectWithClick={true}
          suppressRowClickSelection={true}
          height={"61.2vh"}
        ></AgGridComponent>
      </div>
    </>
  );
};

export default AllocationAggrid;
