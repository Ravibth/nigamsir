/* eslint-disable react-hooks/exhaustive-deps */
import { useContext, useMemo, useRef, useState } from "react";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import { Typography } from "@mui/material";
import { IGetAllMySkillsResponse } from "../../../services/skills/userSkills.service";
import MySkillActionCellRenderer from "./MySkillActionCellRenderer";
import "./style.css";
import {
  MySkillsGridContext,
  IMySkillGridContextProps,
} from "./mySkillsGridContext/mySkillsGridContext";
import { LoaderContextProps } from "../../../contexts/loaderContext";
import { SnackbarContextProps } from "../../../contexts/snackbarContext";
import MySkillStatusCommentRenderer from "./MySkillStatusCommentRenderer";
import {
  DateComparatorForSorting,
  getDateInMomentFormat,
  dateFilterParams,
} from "../../../utils/date/dateHelper";

export interface IMySkillsAgGridProps {
  gridRowsData: IGetAllMySkillsResponse[];
  setGridRowsData: React.Dispatch<
    React.SetStateAction<IGetAllMySkillsResponse[]>
  >;
  fetchUserSkills: () => Promise<boolean>;
  loaderContext: LoaderContextProps;
  snackbarContext: SnackbarContextProps;
}
const MySkillsAgGrid = (props: IMySkillsAgGridProps) => {
  const mySkillGridRef = useRef();
  const mySkillGridContext: IMySkillGridContextProps =
    useContext(MySkillsGridContext);
  const [proficiencyOptions, setProficiencyOptions] = useState<string[]>([
    "Basic",
    "Intermediate",
    "Advanced",
    "Expert",
  ]);

  const MySkillsAgGridColumnDefs = useMemo(
    () => [
      {
        headerName: "Action",
        field: "action",
        flex: 0.5,
        filter: false,
        sortable: false,
        unSortIcon: false,
        suppressMenu: true,
        cellRenderer: MySkillActionCellRenderer,
        cellRendererParams: {},
      },
      {
        headerName: "Skill Code",
        field: "skillCode",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillCode",
      },
      {
        headerName: "Skill Name",
        field: "skillName",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillName",
      },
      {
        headerName: "Skill Description",
        field: "description",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillDescription",
      },
      {
        headerName: "Proficiency",
        field: "proficiency",
        flex: 1,
        filter: true,
        sortable: true,
        unSortIcon: true,
        // menuTabs: ["filterMenuTab"],
        tooltipField: "proficiency",
      },
      {
        headerName: "Status",
        field: "status",
        flex: 1,
        filter: true,
        sortable: true,
        // sort: "asc",
        unSortIcon: true,
        // menuTabs: ["filterMenuTab"],
        cellRenderer: MySkillStatusCommentRenderer,
        cellRendererParams: {
          setGridRowsData: props.setGridRowsData,
          gridRowsData: props.gridRowsData,
        },
      },
      {
        headerName: "Approver",
        field: "approver",
        flex: 1,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "approver",
      },
      {
        headerName: "Approved/Rejected On",
        field: "approvedOn",
        flex: 1,
        filter: "agDateColumnFilter",
        comparator: DateComparatorForSorting,
        sortable: true,
        unSortIcon: true,
        valueGetter: function (params) {
          if (params?.data?.approvedOn) {
            return getDateInMomentFormat(params.data.approvedOn, "DD-MM-YYYY");
          }
        },
        tooltipValueGetter: function (params) {
          if (params?.data?.approvedOn) {
            return getDateInMomentFormat(params.data.approvedOn, "DD-MM-YYYY");
          }
        },
        filterParams: dateFilterParams,
      },
    ],
    [
      mySkillGridContext.currentEditingField,
      proficiencyOptions,
      props.gridRowsData,
    ]
  );

  const defaultColDef = {
    lockVisible: true,
  };
  const gridOptions = {
    suppressCellSelection: true,
  };

  return (
    <Typography component={"div"} className="my-skills-ag-grid">
      <AgGridComponent
        columnDefs={MySkillsAgGridColumnDefs}
        rowData={props.gridRowsData}
        isPageination={true}
        gridComponentRef={mySkillGridRef}
        height={"76vh"}
        tooltipShowDelay={0}
        tooltipHideDelay={2000}
        pageSize={18}
        suppressCellFocus={true}
        hideExport={true}
        suppressContextMenu={true}
        isFilterVisible={true}
        defaultColDef={defaultColDef}
        gridOptions={gridOptions}
      />
    </Typography>
  );
};
export default MySkillsAgGrid;
