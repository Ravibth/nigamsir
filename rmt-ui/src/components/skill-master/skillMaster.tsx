import React, { useContext, useEffect, useRef, useState } from "react";
import ActionButton from "../actionButton/actionButton";

import "./skill.css";
import AgGridComponent from "../aggrid-component/aggrid-component";
import SkillActionCell from "./skillActionCell";
import { Typography } from "@mui/material";
import { RequisitionHeaderSxProps } from "../create-requisition-main/constant";
import {
  changeSkillStatus,
  getSkillMaster,
} from "../../services/skills/skill.service";
import { SnackbarContext } from "../../contexts/snackbarContext";
import { LoaderContext } from "../../contexts/loaderContext";
import { ICellRendererParams } from "ag-grid-community";
import { ISkillStatusUpdate } from "./Interface/ISkillStatusUpdate";
import ConfirmationDialog from "../../common/confirmation-dialog/confirmation-dialog";
import SkillStatusActionCell from "./skillStatusActionCell";

const SkillMaster = (props: any) => {
  const gridRef: any = useRef();
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [rowData, setRowData] = useState([]);
  const [isConfirmationModal, setIsConfirmationModal] =
    useState<boolean>(false);
  const [statusEvent, setStatusEvent] = useState<any>();
  const handleViewClick = (event: any) => {
    props?.navigateGrid(false, event?.skillName, true, false);
  };

  const handleEditClick = (event: any) => {
    props?.navigateGrid(false, event?.skillName, false, true);
  };

  const updateStatusFlag = (event: any) => {
    setIsConfirmationModal(false);
    const payload: ISkillStatusUpdate = {
      isEnable: !event?.isEnable,
      modifyBy: props?.userName,
      //skillName: event?.skillName,
      skillCode: event?.skillCode,
    };
    updateStatus(payload);
  };
  const handleStatusClick = (event: any) => {
    setStatusEvent(event);
    setIsConfirmationModal(true);
  };
  // Column Definitions: Defines & controls grid columns.
  const [colDefs, setColDefs] = useState([
    {
      field: "Action",
      flex: 1,
      suppressMenu: true,
      cellRenderer: SkillActionCell,
      valueGetter: (params: ICellRendererParams) => params,
      cellRendererParams: {
        handleViewClick,
        handleEditClick,
        handleStatusClick,
      },
      headerTooltip: "Skill Code",
      headerName: "Skill Code",
    },
    {
      field: "skillCode",
      flex: 1,
      filter: "agTextColumnFilter",
      sortable: true,
      unSortIcon: true,
      tooltipField: "skillCode",
      headerTooltip: "Skill Code",
      headerName: "Skill Code",
    },
    {
      field: "skillName",
      flex: 1,
      filter: "agTextColumnFilter",
      sortable: true,
      unSortIcon: true,
      tooltipField: "skillName",
      headerTooltip: "Skill Name",
      headerName: "Skill Name",
    },
    {
      field: "skillCategory",
      flex: 1,
      filter: "agTextColumnFilter",
      sortable: true,
      unSortIcon: true,
      tooltipField: "skillCategory",
      headerTooltip: "Skill Category",
      headerName: "Skill Category",
    },
    {
      field: "description",
      flex: 3,
      filter: "agTextColumnFilter",
      sortable: true,
      unSortIcon: true,
      tooltipField: "description",
      headerTooltip: "Description",
      headerName: "Description",
    },
    {
      field: "Status",
      flex: 1,
      cellRenderer: SkillStatusActionCell,
      valueGetter: (params: any) => {
        return params.data?.isEnable ? "Active" : "Inactive";
      },
      cellRendererParams: {
        handleStatusClick,
      },
      filter: true,
      menuTabs: ["filterMenuTab"],
      filterParams: {
        suppressAndOrCondition: true,
        refreshValuesOnOpen: true,
      },
      sortable: true,
      unSortIcon: true,
      headerTooltip: "Status",
      headerName: "Status",
    },
  ]);

  const gridOptions = {
    suppressCellSelection: true,
    autoSizeStrategy: {
      type: "fitGridWidth",
      defaultMinWidth: 100,
    },

    // other grid options ...
  };

  const fetchAllSkill = async () => {
    loaderContext.open(true);
    Promise.all([getSkillMaster()])
      .then((response: any) => {
        setRowData(response[0]?.data);
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Something went Wrong.", "error");
        loaderContext.open(false);
      });
  };

  const updateStatus = async (payload: ISkillStatusUpdate) => {
    loaderContext.open(true);
    Promise.all([changeSkillStatus(payload)])
      .then((response: any) => {
        payload.isEnable
          ? snackbarContext.displaySnackbar("Skill Enabled.", "success")
          : snackbarContext.displaySnackbar("Skill Disabled.", "success");
        fetchAllSkill();
        gridRef?.current?.api?.refreshCells();
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Something went Wrong.", "error");
        loaderContext.open(false);
      });
  };

  useEffect(() => {
    fetchAllSkill();
  }, []);

  const defaultColDef = {
    lockVisible: true,
    resizable: true,
    cellStyle: function (params: any) {
      if (params.data?.isEnable === false) {
        return { color: "gray" };
      }
    },
  };

  return (
    <>
      <div className="skill-master-heading requisition-header-title">
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          Skill Master
        </Typography>
      </div>
      {props?.isAdmin && (
        <div className="skill-master-add">
          <ActionButton
            label={"Add New Skill"}
            type="submit"
            disabled={false}
            onClick={(e: any) => {
              props?.navigateGrid(false, "", false, false);
            }}
          />
        </div>
      )}
      <div>
        <ConfirmationDialog
          title="Confirm!"
          content="Are you sure you want to proceed?"
          noBtnLabel="No"
          yesBtnLabel="Yes"
          open={isConfirmationModal}
          onConfirmationPopClose={() => {
            setIsConfirmationModal(false);
          }}
          handleYesClick={() => {
            updateStatusFlag(statusEvent);
          }}
        />
      </div>
      <div className="skill-master-grid">
        <AgGridComponent
          gridComponentRef={gridRef}
          rowData={rowData}
          columnDefs={colDefs}
          defaultColDef={defaultColDef}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={true}
          suppressContextMenu={true}
          suppressExcelExport={true}
          isFilterVisible={true}
          hideExport={true}
          suppressCellFocus={true}
          gridOptions={gridOptions}
          height={"720px"}
        ></AgGridComponent>
      </div>
    </>
  );
};

export default SkillMaster;
