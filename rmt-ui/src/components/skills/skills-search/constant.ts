import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";

export const SkillsContainerSxProps: SxProps = {
  padding: "5px 10px",
};

export const defaultSortModel = [{ colId: "designation", sort: "asc" }];

// export const autoGroupColumnDef = {
//   cellRendererParams: {
//     suppressCount: true,
//     colDef: {
//       rowGroup: true,
//       cellRendererParams: {
//         suppressCount: false,
//         checkbox: false,
//       },
//     },
//   },
// };

export const groupValueFormatter = (data) => {
  // console.log("valueFormatter", data);
  data.node.allChildrenCount =
    data?.node?.childrenAfterGroup?.length > 0
      ? data?.node?.childrenAfterGroup?.length
      : "";
  return data.value; //
};

export const columnDefs = [
  {
    headerName: "Designation",
    valueFormatter: groupValueFormatter,
    field: "designation",
    flex: 1.1,
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    menuTabs: ["filterMenuTab"],
    tooltipField: "designation",
    rowGroup: true,
    hide: true,
    sortable: true,
    unSortIcon: true,
  },
  {
    headerName: "Employee Name",
    field: "name",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    hide: true,
    rowGroup: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "name",
    flex: 1,
  },
  {
    headerName: "Skill Name",
    field: "skillName",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "skillName",
    flex: 1,
  },
  {
    headerName: "Proficiency",
    field: "proficiency",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "proficiency",
    flex: 1,
  },
  {
    headerName: "Business Unit",
    field: "business_unit",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "business_unit",
    flex: 1,
  },
  {
    headerName: "Competency",
    field: "competency",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "proficiency",
    flex: 1,
  },
  {
    headerName: "Location",
    field: "location",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "location",
    flex: 1,
  },
];

const gridOptions = {
  suppressCellSelection: true,
};

export const defaultColDef = {
  lockVisible: true,
  resizable: true,
};

export const SkillSearchHeader: SxProps = {
  // color: "#5A5A5A",
  fontSize: "20px",
  marginLeft: "20px",
  fontWeight: "550",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
};

export const groupDisplayType = "multipleColumns";
