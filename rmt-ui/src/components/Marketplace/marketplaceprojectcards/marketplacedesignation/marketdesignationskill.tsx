import { useContext, useEffect, useRef, useState } from "react";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import { LoaderContext } from "../../../../contexts/loaderContext";

const Marketdesignationskill = (props: any) => {
  const gridRef: any = useRef();
  const [rowData, setRowData] = useState([]);
  const loaderContext: any = useContext(LoaderContext);

  const matchingScoreData = () => {
    loaderContext.open(true);
    setRowData(props?.detailCardViewInfo);
    loaderContext.open(false);
  };

  useEffect(() => {
    matchingScoreData();
  }, [props?.detailCardViewInfo]);
  const columnDefs: any[] = [
    {
      headerName: "Designations",
      headerTooltip: "Designations",
      field: "designation",
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      tooltipField: "designation",
      menuTabs: ["filterMenuTab"],
    },
    {
      headerName: "Competency",
      headerTooltip: "Competency",
      field: "competency",
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      tooltipField: "competency",
      menuTabs: ["filterMenuTab"],
    },
    {
      headerName: "Skills",
      headerTooltip: "Skills",
      field: "Skills",
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      cellRenderer: function (params: any) {
        return params?.data?.requisitionSkill.map((a) => a.skillName).join(",");
      },
      tooltipValueGetter: function (params: any) {
        return params?.data?.requisitionSkill.map((a) => a.skillName).join(",");
      },
      menuTabs: ["filterMenuTab"],
    },
    {
      headerName: "Match%",
      headerTooltip: "Match%",
      field: "score",
      flex: 1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      tooltipField: "score",
      menuTabs: ["filterMenuTab"],
    },
  ];

  const defaultColDef = {
    lockVisible: true,
    suppressMenu: true,
  };

  return (
    <div style={{ marginTop: "30px" }}>
      <div className="marketplace-designation-grid">
        <AgGridComponent
          gridComponentRef={gridRef}
          rowData={rowData}
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
          hideExport={true}
          suppressCellFocus={true}
          height={"320px"}
        ></AgGridComponent>
      </div>
    </div>
  );
};

export default Marketdesignationskill;
