import { useRef, useState } from "react";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import { AgGridReact } from "ag-grid-react";

//Capacity chart report
const TabularChart = (props: any) => {
  const { rowData, colDef, setColDef, setRowData } = props;
  const gridRef: any = useRef();

  const defaultColDef = {
    lockVisible: true,
    resizable: true,
  };
  const gridOptions = {
    suppressCellSelection: true,
  };
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

  const gridComponentRef = useRef<AgGridReact | null>(null);

  return (
      <div className="ag-theme-alpine user-ag-grid" style={{ height: "100%" }}>
      <AgGridComponent
        ref={gridRef}
        gridComponentRef={gridComponentRef}
        rowData={rowData}
        columnDefs={colDef}
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
        rowMultiSelectWithClick={true}
        suppressRowClickSelection={true}
        height={props.height?props.height:"61.2vh"}
        exportedFileName={"SC Delegate Allocation"}
      ></AgGridComponent>
    </div>
  );
};

export default TabularChart;

// AVAILABLE
