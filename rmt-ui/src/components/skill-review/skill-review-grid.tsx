/* eslint-disable react-hooks/exhaustive-deps */
import { Typography } from "@mui/material";
import { LoaderContextProps } from "../../contexts/loaderContext";
import { SnackbarContextProps } from "../../contexts/snackbarContext";
import AgGridComponent from "../aggrid-component/aggrid-component";
import { useMemo } from "react";
import SkillReviewActionColumnRenderers from "./columnRenderers/skillReviewactionColumnRenderer";
import {
  getSkillProfeciencyDescriptionMapping,
  ISkillReviewGridData,
} from "./utils";
import { WORKFLOW_ACTION_STATUS } from "../../global/constant";
import { getGridToolTipValue, routeToEmployeeProfile } from "../../global/utils";
import { EProficiencyLevels } from "../my-skills/enums";

export interface ISkillReviewGridProps {
  mySkillReviewGridRef: any;
  gridRowsData: any[];
  setGridRowsData: React.Dispatch<React.SetStateAction<any[]>>;
  loaderContext: LoaderContextProps;
  snackbarContext: SnackbarContextProps;
  selectedNodes: ISkillReviewGridData[];
  setSelectedNodes: React.Dispatch<
    React.SetStateAction<ISkillReviewGridData[]>
  >;
  setItemToApproveReject: React.Dispatch<
    React.SetStateAction<ISkillReviewGridData>
  >;
  setOpenRejectApproveConfirmationModal: React.Dispatch<
    React.SetStateAction<WORKFLOW_ACTION_STATUS>
  >;
}

const SkillReviewGrid = (props: ISkillReviewGridProps) => {
  console.log(props);
  const MySkillsReviewAgGridColumnDefs = useMemo(
    () => [
      {
        headerName: "Action",
        field: "action",
        flex: 0.5,
        filter: false,
        sortable: false,
        unSortIcon: false,
        cellRenderer: SkillReviewActionColumnRenderers,
        cellRendererParams: {
          setItemToApproveReject: props.setItemToApproveReject,
          setOpenRejectApproveConfirmationModal:
            props.setOpenRejectApproveConfirmationModal,
        },
        suppressMenu: true,
      },
      {
        checkboxSelection: true,
        headerCheckboxSelection: true,
        headerName: "Employee",
        field: "meta.entity_meta_data.Name",
        flex: 1,
        filter: "agTextColumnFilter",
        sortable: true,
        tooltipValueGetter: getGridToolTipValue,
        unSortIcon: true,
        cellRenderer: (params) => {
          return (
            <span
              style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
              title={params.value}
              onClick={() => {                
                routeToEmployeeProfile(`/employee-profile/${params.data?.meta?.entity_meta_data?.Email}`);
              }}
            >
              {params.value}
            </span>
          );
        },
      },
      {
        headerName: "Skill Code",
        field: "skillCode",
        flex: 0.8,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillCode",
      },
      {
        headerName: "Skill Name",
        field: "skillName",
        flex: 0.8,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillName",
      },
      {
        headerName: "Skill Description",
        field: "skillMaster.description",
        flex: 0.8,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "skillMaster.description",
        cellRendererParams: (props) => {},
      },

      {
        headerName: "Prior Proficiency",
        field: "priorGrading",
        flex: 0.5,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "priorGrading",
      },
      {
        headerName: "Prior Proficiency Description",
        field: "priorGrading",
        flex: 0.5,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "priorGrading",
        cellRenderer: (params) => {
          let description = getSkillProfeciencyDescriptionMapping(params);
          return <span>{description}</span>;
        },
      },
      {
        headerName: "New Proficiency",
        field: "newGrading",
        flex: 0.5,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "newGrading",
      },
      {
        headerName: "New Proficiency Description",
        field: "newGrading",
        flex: 0.5,
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "newGrading",
        cellRenderer: (params) => {
          let description = getSkillProfeciencyDescriptionMapping(params);
          return <span>{description}</span>;
        },
      },
      {
        headerName: "Comments",
        field: "comments",
        flex: 1,
        filter: true,
        sortable: true,
        tooltipField: "comments",
        unSortIcon: true,
      },
    ],
    [props.gridRowsData]
  );

  const defaultColDef = {
    lockVisible: true,
  };
  const gridOptions = {
    suppressCellSelection: true,
  };

  const onSelectionChanged = () => {
    if (
      props?.mySkillReviewGridRef?.current &&
      props?.mySkillReviewGridRef?.current?.api
    ) {
      const selectedItems =
        props?.mySkillReviewGridRef?.current?.api?.getSelectedRows();
      props.setSelectedNodes(selectedItems);
    }
  };
  console.log();
  return (
    <Typography component={"div"} className="my-skills-ag-grid">
      <AgGridComponent
        columnDefs={MySkillsReviewAgGridColumnDefs}
        rowData={props.gridRowsData}
        isPageination={true}
        gridComponentRef={props?.mySkillReviewGridRef}
        onSelectionChanged={onSelectionChanged}
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
        rowSelection={"multiple"}
        suppressRowClickSelection={true}
      />
    </Typography>
  );
};
export default SkillReviewGrid;
