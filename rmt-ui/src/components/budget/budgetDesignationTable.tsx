import React, { useRef } from "react";
import AgGridComponent from "../aggrid-component/aggrid-component";

const BudgetDesignationTable = (props: any) => {
  const gridRef: any = useRef();

  const gridOptions = {
    getRowStyle: (params) => {
      if (params?.node?.data?.budgetHrs === 0) {
        return { color: "#f26416" };
      }
    },
  };
  return (
    <div className="budget-grid">
      <AgGridComponent
        gridComponentRef={gridRef}
        rowData={props?.designationData}
        columnDefs={props.coldef}
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
        height={"378px"}
        gridOptions={gridOptions}
      ></AgGridComponent>
    </div>
  );
};

export default BudgetDesignationTable;
