import React, { useMemo, useState } from "react";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import moment from "moment";
import * as constant from "./constant";
import "./requisitionAggrid.css";
import { dateFilterParams } from "../../../../utils/date/dateHelper";
import RequisitionActionCell from "./requisitionActionCell";
import { GlobalConfigs } from "../../../../global/constant";
import { getEmailId, getGridToolTipValue } from "../../../../global/utils";

const RequisitionAggrid = (props: any) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>();
  const [ReqGridData, setReqGridData] = useState(null);
  const open = Boolean(anchorEl);
  const {
    requisitionsList,
    categoryValue,
    handleOpen,
    setRequisitionSelected,
    handleNavigationToUpdateRequisitionForm,
    projectDetails,
  } = props;
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
  // grid data logic starts here

  const skillName = function (params: any) {
    return params?.data?.requisitionSkill
      .map((item: any) => item.skillName)
      .join(", ");
  };

  const EndDate = (data: any) => {
    const inputDate = moment(data.value, "YYYY-MM-DD");
    if (inputDate.isValid()) {
      const endDate = inputDate.format(GlobalConfigs.dateFormat);
      return endDate;
    } else {
      return null;
    }
  };

  const StartDate = (data: any) => {
    const inputDate = moment(data.value, "YYYY-MM-DD");
    if (inputDate.isValid()) {
      const startDate = inputDate.format(GlobalConfigs.dateFormat);
      return startDate;
    } else {
      return null;
    }
  };
  const CreatedOn = (data: any) => {
    const inputDate = moment(data.value, "YYYY-MM-DD");
    if (inputDate.isValid()) {
      const createdDate = inputDate.format(GlobalConfigs.dateFormat);
      return createdDate;
    } else {
      return null;
    }
  };

  // grid data logic ends here

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
    // setReqGridData(params.data)
  };

  const handleClose = () => {
    setAnchorEl(null);
    setReqGridData(null);
  };

  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Actions",
        field: "actions",
        // flex: 0.7,
        suppressMenu: true,
        tooltipField: "action",
        cellRenderer: RequisitionActionCell,
        cellRendererParams: {
          handleNavigationToUpdateRequisitionForm,
          handleClick,
          open,
          anchorEl,
          handleClose,
          handleOpen,
          setRequisitionSelected,
          requisitionsList,
          projectDetails,
        },
      },
      {
        headerTooltip: "Designation",
        field: "designation",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "designation",
        menuTabs: ["filterMenuTab"],
        rowGroup: true,
        hide: true,
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
        //minWidth: 150,
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
      },
      {
        headerName: "No. of Hours",
        headerTooltip: "No. of Hours",
        field: "totalHours",
        // flex: 0.7,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "totalHours",
      },
      {
        headerName: "Grade",
        headerTooltip: "Grade",
        field: "grade",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "grade",
      },
      {
        headerName: "Skills",
        headerTooltip: "Skills",
        field: "Skills",
        // flex: 1.2,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
          textCustomComparator: function (
            filter: any,
            value: any,
            filterText: any
          ) {
            return value.some((skill: any) => skill.includes(filterText));
          },
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: skillName,
        valueGetter: skillName,
      },
      {
        headerName: "Business Unit",
        headerTooltip: "Business Unit",
        field: "businessUnit",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "businessUnit",
      },
      {
        headerName: "Offerings",
        headerTooltip: "Offerings",
        field: "offerings",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "offerings",
      },
      {
        headerName: "Solutions",
        headerTooltip: "Solutions",
        field: "solutions",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "solutions",
      },
      {
        headerName: "Competency",
        headerTooltip: "Competency",
        field: "competency",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "competency",
      },
      {
        headerName: "Created By",
        headerTooltip: "Created By",
        field: "createdBy",
        // flex: 0.9,
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
          }
          return params?.data?.createdBy;
        },
      },
      {
        headerName: "Created On",
        headerTooltip: "Created On",
        field: "createdAt",
        // flex: 0.9,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: CreatedOn,
        cellRenderer: CreatedOn,
      },
      {
        headerName: "Requisition Id",
        headerTooltip: "Requisition Id",
        field: "id",
        // flex: 0.7,
        filter: "agTextColumnFilter",
        filterParams: dateFilterParams,
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "id",
      },
    ],
    [requisitionsList, anchorEl, ReqGridData]
  );

  return (
    <div className="requisition-agGrid">
      <AgGridComponent
        ref={props.gridRef}
        rowData={requisitionsList}
        columnDefs={columnDefs}
        gridComponentRef={props.gridComponentRef}
        defaultColDef={constant.defaultColDef}
        tooltipShowDelay={0}
        tooltipHideDelay={2000}
        isPageination={true}
        pageSize={18}
        suppressCsvExport={true}
        suppressContextMenu={true}
        suppressExcelExport={true}
        isFilterVisible={true}
        hideExport={false}
        gridOptions={constant.gridOptions}
        suppressCellFocus={true}
        onGridReady={onGridReady}
        height={"59vh"}
      ></AgGridComponent>
    </div>
  );
};

export default RequisitionAggrid;
