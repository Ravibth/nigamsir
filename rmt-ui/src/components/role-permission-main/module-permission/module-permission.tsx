import { AgGridReact } from "ag-grid-react";
import React, { useCallback, useState } from "react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { Button, Checkbox } from "@mui/material";

const ModulePermission = (props: any) => {
  const permissions = ["create", "read", "update", "delete"];
  // console.log(props);
  const generateCheckboxCellRenderer = (permission: any) => {
    return function CheckboxCellRenderer(params: any) {
      if (params) {
        return (
          <span>
            <Checkbox
              className="checkColor"
              checked={
                params.data.is_assigned && params.data.permissions[permission]
              }
            />
          </span>
        );
      } else {
        return (
          <span>
            <Checkbox readOnly disabled />
          </span>
        );
      }
    };
  };
  // console.log(permissions)
  const columnDefs: any = [
    {
      headerName: "Module Name",
      field: "module_display",
      tooltipField: "module_display",
      flex: 1.9,
      suppressMovable: true,
    },
    ...permissions.map((permission) => ({
      headerName: permission.charAt(0).toUpperCase(),
      field: permission,
      headerTooltip: permission.charAt(0).toUpperCase() + permission.slice(1),
      flex: 1,
      cellStyle: () => ({
        display: "flex",
        alignItems: "start",
        justifyContent: "start",
        marginLeft: "-16px",
      }),
      cellRenderer: generateCheckboxCellRenderer(permission),
    })),
  ];
  console.log(columnDefs);
  const defaultColDef = {
    lockVisible: true,
    flex: 1,
    suppressMenu: true,
  };

  // const onBtnExport = useCallback(() => {
  //   if (props.gridRefModulePermission.current) {
  //     props.gridRefModulePermission.current.api.exportDataAsCsv();
  //   }
  // }, [props.gridRefModulePermission]);

  const onBtnExport = useCallback(() => {
    if (props.gridRefModulePermission.current) {
      const gridApi = props.gridRefModulePermission.current.api;
      const rowData = gridApi.getModel().rowsToDisplay;
      const headerRow = ["Module Name", ...permissions];
      const csvContent = rowData
        .map((row: any) => {
          const data = [
            row.data.module_name,
            ...permissions.map((permission) =>
              row.data.permissions[permission] ? "true" : ""
            ),
          ];
          return data.join(",");
        })
        .join("\n");

      const csvData = [headerRow.join(","), csvContent].join("\n");
      const blob = new Blob([csvData], { type: "text/csv;charset=utf-8;" });
      const url = URL.createObjectURL(blob);

      const a = document.createElement("a");
      a.style.display = "none";
      a.href = url;
      a.download = "exported_data.csv";
      document.body.appendChild(a);
      a.click();

      URL.revokeObjectURL(url);
      document.body.removeChild(a);
    }
  }, [props.gridRefModulePermission]);

  return (
    <div className="ag-theme-alpine module-aggrid" style={{ height: "54vh" }}>
      {/* <Button onClick={onBtnExport}>click</Button> */}
      <AgGridReact
        ref={props.gridRefModulePermission}
        rowData={props.modulePermissionRowData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        suppressCellFocus={true}
      />
    </div>
  );
};

export default ModulePermission;
