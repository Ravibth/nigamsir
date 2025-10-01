import { useMemo, useRef, useState } from "react";
// import AgGridComponent from '../../../aggrid-component/aggrid-component';
import { AgGridReact } from "ag-grid-react";
import AgGridComponent from "../../../../aggrid-component/aggrid-component";

const TabularChart = (props: any) => {
  const { colDef } = props;
  const gridRef: any = useRef();
  // const [colDef, setColDef] = useState<any[]>([]);
  // const [rowData, setRowData] = useState<[]>([]);

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
    <>
      <AgGridComponent
        ref={gridRef}
        gridComponentRef={gridComponentRef}
        rowData={props.rowData}
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
        height={"61.2vh"}
        exportedFileName={"ScheduledVsVarianceReport"}
      ></AgGridComponent>
    </>
  );
};

export default TabularChart;

// AVAILABLE
