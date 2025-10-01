import { useEffect, useState } from "react";
import * as constant from "./bulk-upload-constant";
import "./bulk-upload-main.css";
import CancelIcon from "@mui/icons-material/Cancel";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import AgGridComponent from "../aggrid-component/aggrid-component";

const UploadAgGrid = (props: any) => {
  const { onGridReady, errors, dataAfterValidation } = props;
  const [newColDef, setNewColDef] = useState<any[]>([]);

  useEffect(() => {
    if (dataAfterValidation?.length > 0) {
      let columnDefs = [];

      if (props.selectedRadioValue === "requisition") {
        columnDefs = constant.requisitionColumnDefs;
      } else {
        columnDefs = constant.allocationColumnDefs;
      }
      const newColDefData = columnDefs?.map((colDef) => {
        if (
          colDef.headerName.toLowerCase().trim() === "status" &&
          colDef.headerName.toLowerCase().trim() !== null
        ) {
          return {
            ...colDef,
            cellRenderer: (params: any) => {
              return (
                <>
                  {params?.data?.status ? (
                    <span className="success-status-icon">
                      <CheckCircleIcon />
                    </span>
                  ) : params?.data?.status ? (
                    <span className="error-status-icon">
                      <CancelIcon />
                    </span>
                  ) : (
                    <span className="error-status-icon">
                      <CancelIcon />
                    </span>
                  )}
                </>
              );
            },
          };
        } else if (colDef.headerName.toLowerCase().trim() === "comments") {
          return {
            ...colDef,
            cellRenderer: (params: any) => {
              const comments = (params?.data?.comments || "").toString();
              const commentsArray = comments
                .split(",")
                .filter((comment) => comment.trim() !== "");
              const commentsWithCount = commentsArray
                .map((comment, index) => `${index + 1}. ${comment}`)
                .join(", ");
              return <span>{commentsWithCount}</span>;
            },
            tooltipValueGetter: (params: any) => {
              const comments = (params?.data?.comments || "").toString();
              const commentsArray = comments
                .split(",")
                .filter((comment) => comment.trim() !== "");
              const commentsWithCount = commentsArray
                .map((comment, index) => `${index + 1}. ${comment}`)
                .join(", ");
              return commentsWithCount;
            },
          };
        } else {
          return colDef;
        }
      });
      setNewColDef(newColDefData);
    }
  }, [
    constant.requisitionColumnDefs,
    constant.allocationColumnDefs,
    dataAfterValidation,
  ]);

  useEffect(() => {}, [dataAfterValidation]);
  return (
    <div>
      <div className="ag-theme-alpine aggrid-invalid-fileupload">
        <AgGridComponent
          ref={props.gridRef}
          rowData={dataAfterValidation}
          columnDefs={newColDef}
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
          height={"61vh"}
        ></AgGridComponent>
      </div>
    </div>
  );
};

export default UploadAgGrid;
