/* eslint-disable no-useless-concat */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useState, useEffect } from "react";
import filter from "../../common/images/filter.png";
import Excel from "../../common/images/excel.png";
import { Tooltip } from "@mui/material";
import { Oval } from "react-loader-spinner";
import { GridProps } from "./Iaggrid-component";
import "./aggrid-component.scss";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-enterprise";
function AgGridComponent(props: GridProps) {
  // const gridComponentRef = useRef<AgGridReact | null>(null);
  const { gridComponentRef, exportedFileName } = props;
  const [showFilter, setShowFilter] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [selectedPagination, setSelectedPagination] = useState<number>(20);
  const paginationOption = [20, 50, 100];

  useEffect(() => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      gridComponentRef.current.api.showLoadingOverlay = showOverlay;
      gridComponentRef.current.api.hideOverlay = hideOverlay;
      gridComponentRef.current.api.setFilterModel = clearAllFilter;
      gridComponentRef.current.api.showNoRowsOverlay = showNoRowsOverlay;
      gridComponentRef.current.api.destroy = clearGrid;
    }
  }, []);
  const showOverlay = () => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      gridComponentRef.current.api.showLoadingOverlay();
    }
  };

  const hideOverlay = () => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      gridComponentRef.current.api.hideOverlay();
    }
  };

  const clearAllFilter = () => {
    if (gridComponentRef.current && gridComponentRef.current.api) {
      gridComponentRef.current.api.setFilterModel(null);
      setShowFilter(false);
    }
  };

  const showNoRowsOverlay = () => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      gridComponentRef.current.api.showNoRowsOverlay();
    }
  };

  const clearGrid = () => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      gridComponentRef.current.api.destroy();
    }
  };

  const onFilterChange = () => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      const savedFilterModel = gridComponentRef.current.api.getFilterModel();
      setShowFilter(Object.keys(savedFilterModel).length > 0);
    }
    if (Object.keys(props).includes("onFilterChanged")) {
      props?.onFilterChanged();
    }
  };

  const onGridReady = (params: any) => {
    if (
      gridComponentRef &&
      gridComponentRef.current &&
      gridComponentRef.current.api
    ) {
      // props?.getRef(gridComponentRef);
      params.columnApi.autoSizeAllColumns();
      const gridColumnApi = params.columnApi;
      const allColumnIds: string[] = [];
      if (gridColumnApi) {
        gridColumnApi.getAllColumns().forEach((column: any) => {
          allColumnIds.push(column.getColId());
        });
        gridColumnApi.autoSizeColumns(allColumnIds);
      }
    }
  };

  const paginationChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = parseInt(event.target.value);
    setSelectedPagination(selectedValue);
  };

  const downloadExcel = () => {
    if (
      gridComponentRef.current !== undefined &&
      gridComponentRef.current.api !== undefined
    ) {
      var filePreName = "";
      //file name chnage
      if (exportedFileName && exportedFileName.length > 0) {
        filePreName = exportedFileName;
      } else {
        filePreName = "export";
      }
      const filename = filePreName + "_" + new Date().toDateString() + ".xlsx";
      // Exclude "actions" col
      const allColumns = gridComponentRef.current.columnApi.getAllColumns();
      const columnsToExport = allColumns
        .filter(column => column.getColId() !== "actions")
        .map(column => column.getColId());

      const params = {
        sheetName: " OptiWise",
        fileName: filename,
        columnKeys: columnsToExport
      };
      gridComponentRef.current.api.exportDataAsExcel(params);
    }
  };

  const DefaultColDef = {
    resizable: true,
    lockVisible: true,
  };

  return (
    <div className="grid-wrapper">
      <div
        className="ag-theme-alpine"
        style={{ height: props?.height ? props.height : "86vh" }}
      >
        <AgGridReact
          onCellEditingStarted={props.onCellEditingStarted}
          rowData={props.rowData}
          columnDefs={props.columnDefs}
          pagination={props.isPageination}
          paginationPageSize={selectedPagination}
          ref={gridComponentRef}
          // ref={props.ref}
          getRowStyle={props.getRowStyle}
          defaultColDef={
            props.defaultColDef
              ? { ...DefaultColDef, ...props.defaultColDef }
              : DefaultColDef
          }
          autoGroupColumnDef={props.autoGroupColumnDef}
          enableBrowserTooltips={true}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          gridOptions={props.gridOptions}
          groupDefaultExpanded={1}
          onFirstDataRendered={props.firstDataRendered}
          overlayLoadingTemplate={
            '<span class="ag-overlay-loading-center">Please wait while your rows are loading</span>'
          }
          suppressColumnVirtualisation={true}
          onFilterChanged={onFilterChange}
          rowSelection={props?.rowSelection}
          onGridReady={onGridReady}
          suppressContextMenu={props.suppressContextMenu || false}
          getRowId={props.getRowId}
          onCellEditingStopped={props.onCellEditingStopped}
          onRowEditingStarted={props.onRowEditingStarted}
          onRowEditingStopped={props.onRowEditingStopped}
          onCellValueChanged={props.cellValueChanged}
          onCellKeyPress={props.onCellKeyPress}
          onCellKeyDown={props.onCellKeyDown}
          onSortChanged={props.onSortChanged}
          rowClassRules={props.rowClassRules}
          suppressMultiSort={true}
          stopEditingWhenCellsLoseFocus={
            props.stopEditingWhenCellsLoseFocus == false ? false : true
          }
          suppressCellFocus={true}
          onSelectionChanged={props.onSelectionChanged}
          masterDetail={props.masterDetail}
          detailCellRenderer={props.detailCellRenderer}
          enableGroupEdit={props.enableGroupEdit}
          detailCellRendererParams={props.detailCellRendererParams}
          suppressRowClickSelection={props.suppressRowClickSelection}
          isRowSelectable={props?.isRowSelectable}
          onCellEditRequest={props.onCellEditRequest}
        ></AgGridReact>

        <div className="download-icon">
          {props.rowData && props.rowData.length > 0 && !props.hideExport && (
            <>
              <Tooltip title="Export to excel">
                <div>
                  {" "}
                  <img
                    src={Excel}
                    alt="Excel"
                    width="25"
                    height="25"
                    onClick={() => downloadExcel()}
                  />
                </div>
              </Tooltip>
            </>
          )}
          {props.isFilterVisible && showFilter ? (
            <Tooltip title="Clear all filter">
              <div className="clear-filter">
                <img
                  src={filter}
                  height={25}
                  width={25}
                  alt="Clear Filter"
                  onClick={clearAllFilter}
                />
              </div>
            </Tooltip>
          ) : null}
        </div>

        {isLoading ? (
          <div className="loader">
            <Oval
              height={80}
              width={80}
              color="#4fa94d"
              visible={true}
              ariaLabel="oval-loading"
              secondaryColor="#4fa94d"
              strokeWidth={2}
              strokeWidthSecondary={2}
            />
          </div>
        ) : null}
      </div>
    </div>
  );
}

export default AgGridComponent;
