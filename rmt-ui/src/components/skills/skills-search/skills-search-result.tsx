import { useRef } from "react";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import { AgGridReact } from "ag-grid-react";

export default function SkillsSearchResult(props: any) {
  const { rowsData, columnDefs, defaultColDef, autoGroupColumnDef } = props;
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const gridRef: any = useRef();
  const gridOptions = {
    autoSizeStrategy: {
      type: "fitGridWidth",
      defaultMinWidth: 100,
    },
    enableSorting: true,
    defaultColDef: {
      sortable: true,
    },
    sortModel: [
      { colId: "designation", sort: "asc" }, // Default sorting by designation in ascending order
    ],
  };

  return (
    <>
      <AgGridComponent
        ref={gridRef}
        getRef={props.getRef}
        gridComponentRef={gridComponentRef}
        rowData={rowsData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        autoGroupColumnDef={autoGroupColumnDef}
        tooltipShowDelay={0}
        tooltipHideDelay={2000}
        isPageination={true}
        pageSize={18}
        suppressCsvExport={false}
        suppressContextMenu={false}
        suppressExcelExport={false}
        isFilterVisible={true}
        hideExport={false}
        height={"680px"}
        gridOptions={gridOptions}
        suppressCellFocus={true}

        // onGridReady={onGridReady}
      ></AgGridComponent>
    </>
  );
}
