import { AgGridReact } from "ag-grid-react";
import React, { useState } from "react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import CustomHeader from "./custom-header";

const RoleName = (props: any) => {
  const onRoleSelection = (params: any) => {
    const selectedRole = props.roleRowData.find((role: any) => {
      return role.role_name === params;
    });
    props.isAnyRoleSelected(selectedRole?.role_name);
  };

  const cellClicked = (params: any) => {
    if (params.node.selected === true) {
      params.node.setSelected(false);
    } else {
      params.node.setSelected(true);
    }
  };

  const columnDefs: any = [
    {
      headerName: "Role Name",
      field: "display",
      suppressMenu: true,
    },
    {
      headerClass: "header-custom",
      headerComponent: CustomHeader,
      flex: 1,
      headerComponentParams: {
        clearRoleNameSelcted: async function () {
          if (
            props.gridRefRoles.current.api.selectionService.lastSelectedNode
              ?.selected
          ) {
            props.gridRefRoles.current.api.selectionService.lastSelectedNode.setSelected(
              false
            );
            await props.resetUsersData();
            props.setRoleIdSelected("");
          }
        },
        isRoleSelected: props.roleIdSelected,
      },
    },
  ];

  const defaultColDef = {
    lockVisible: true,
    flex: 1,
    suppressMovable: true,
  };

  //console.log("props role 57 line:", props);

  return (
    <div className="ag-theme-alpine role-aggrid" style={{ height: "100%" }}>
      <AgGridReact
        onRowClicked={(e) => {
          onRoleSelection(e.data.role_name);
        }}
        onCellClicked={cellClicked}
        rowData={props.roleRowData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        ref={props.gridRefRoles}
        suppressCellFocus={true}
      />
    </div>
  );
};

export default RoleName;
