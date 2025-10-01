import { ColDef } from "ag-grid-community";
import { useMemo } from "react";
import { EmployeeProfile, EmployeeQualification } from "../../interfaces/employeeProfile";

export const defaultColDef : ColDef<EmployeeQualification> = {
    flex: 1,
    minWidth: 50,
    resizable: true,
    sortable: true,
}
