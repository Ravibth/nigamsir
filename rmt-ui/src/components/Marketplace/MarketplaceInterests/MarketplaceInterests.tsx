/* eslint-disable array-callback-return */
/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import { useEffect, useMemo, useRef, useState, useContext } from "react";
import {
  getAllEmpProjectInterestScore,
  IGetAllEmpProjectInterestScoreResponse,
} from "../../../services/marketPlace/getAllEmpProjectInterestScore";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import { AgGridReact } from "ag-grid-react";
import {
  ERequisitionParameters,
  ESkillType,
  getSkillsValue,
  getParameterScoreAndValue,
} from "../../system-suggestions/requisition-form-system-suggestions/suggestions-grid-view/suggestions-grid-view";
import ActionButton from "../../actionButton/actionButton";
import { Grid, Tooltip } from "@mui/material";
import MarketplaceInterestCommonAllocation from "./marketplaceinterest-common-allocation";
import InfoIcon from "@mui/icons-material/Info";
import { CheckUserHasRoleToAllocateOnProject } from "../../../global/utils";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import Radio from "@mui/material/Radio";

const MarketplaceInterests = (props: any) => {
  const { projectDetails } = props;
  const userContext = useContext(UserDetailsContext);
  const gridRef: any = useRef();
  const [rowData, setRowData] = useState<any[]>([]);
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const [selectedUserRow, setSelectedUserRow] = useState<
    IGetAllEmpProjectInterestScoreResponse[]
  >([]);
  const [showAvailabilityCommonScreen, setShowAvailabilityCommonScreen] =
    useState<boolean>(false);

  const isRowSelectable = (params: any): boolean => {
    //Do not select row if project is published in marketplace
    const allocationPermission = CheckUserHasRoleToAllocateOnProject(
      userContext?.projectPermissionData?.projectRoles || [],
      userContext.role
    );
    const flag =
      !params.node.group &&
      !projectDetails.isPublishedToMarketPlace &&
      allocationPermission;
    return flag;
  };

  const columnDefs = useMemo(
    () => [
      {
        headerName: "Employee Name",
        headerTooltip: "Employee Name",
        field: "empName",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "empName",
        menuTabs: ["filterMenuTab"],
        cellRenderer: (cellRendererProps) => {
          return (
            !cellRendererProps.node.group &&
            cellRendererProps.getValue() && (
              <>
                {isRowSelectable(cellRendererProps) && (
                  <span>
                    <Radio
                      checked={
                        selectedUserRow &&
                        selectedUserRow.length > 0 &&
                        selectedUserRow[0]?.empEmail ===
                          cellRendererProps?.node?.data?.empEmail &&
                        selectedUserRow[0]?.empProjectInterestId ===
                          cellRendererProps?.node?.data?.empProjectInterestId
                      }
                      value={cellRendererProps.node.data}
                      onChange={(e) =>
                        handleRadioButtonChange(cellRendererProps.node.data)
                      }
                    />
                  </span>
                )}
                <span>{cellRendererProps.getValue()}</span>
              </>
            )
          );
        },
      },
      {
        headerName: "Mandatory Skills",
        field: "skills",
        filter: "agTextColumnFilter",
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getSkillsValue(
              rowItem?.data?.suggestion,
              ESkillType.Mandatory
            );
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value || "-",
      },
      {
        headerName: "Employee BU",
        field: "suggestion.business_unit",
        filter: true,
        sortable: true,
        unSortIcon: true,
        tooltipField: "businessUnit",
      },
      {
        headerName: "Match%",
        field: "requisionScore",
        filter: true,
        sortable: true,
        tooltipField: "requisionScore",
        unSortIcon: true,
      },
      {
        headerName: "Offering Score",
        headerTooltip: "Offering Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.offerings,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Solution Score",
        headerTooltip: "Solution Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.solutions,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Competency",
        headerTooltip: "Competency",
        field: "competency",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "competency",
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.competency,
              rowItem.data?.requisitionParameters
            )?.value;
          } else {
            return "";
          }
        },
      },
      {
        headerName: "Competency Score",
        headerTooltip: "Competency Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.competency,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Location",
        headerTooltip: "Location",
        field: "location",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "location",
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Location,
              rowItem.data?.requisitionParameters
            )?.value;
          } else {
            return "";
          }
        },
      },
      {
        headerName: "Location Score",
        headerTooltip: "Location Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Location,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Industry",
        headerTooltip: "Industry",
        field: "industry",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "industry",
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Industry,
              rowItem.data?.requisitionParameters
            )?.value;
          } else {
            return "";
          }
        },
      },
      {
        headerName: "Industry Score",
        headerTooltip: "Industry Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Industry,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Sub-Industry ",
        headerTooltip: "Sub-Industry ",
        field: "sub_industry",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "sub_industry",
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Sub_Industry,
              rowItem.data?.requisitionParameters
            )?.value;
          } else {
            return "";
          }
        },
      },
      {
        headerName: "Sub-Industry Score",
        headerTooltip: "Sub-Industry Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Sub_Industry,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Additional Skill",
        headerTooltip: "Additional Skill",
        field: "",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getSkillsValue(
              rowItem.data?.suggestion,
              ESkillType.Optional
            );
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value || "-",
      },
      {
        headerName: "Additional Skill Score",
        headerTooltip: "Additional Skill Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Skills,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Exp working with same client Score",
        headerTooltip: "Exp working with same client Score",
        field: "",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          if (rowItem?.data) {
            return getParameterScoreAndValue(
              rowItem.data?.suggestion,
              ERequisitionParameters.Same_client,
              rowItem.data?.requisitionParameters
            )?.score;
          } else {
            return "";
          }
        },
        tooltipValueGetter: (rowData) => rowData?.value,
      },
      {
        headerName: "Designation - Grade",
        field: "designation-grade",
        filter: true,
        sortable: true,
        unSortIcon: true,
        suppressMenu: true,
        rowGroup: true,
        hide: false,
        valueGetter: (params) => {
          if (params?.data) {
            return `${params?.data?.requisitionDesignation} - ${params?.data?.requisitionGrade}`;
          } else {
            return "";
          }
        },
      },
      {
        headerName: "Requisition Id",
        field: "requisitionId",
        filter: "agTextColumnFilter",
        sortable: true,
        unSortIcon: true,
        rowGroup: true,
        hide: false,
        tooltipField: "requisitionId",
      },
    ],
    [userContext, selectedUserRow]
  );

  useEffect(() => {
    GetAllEmpProjectInterestScore(
      projectDetails.pipelineCode,
      projectDetails.jobCode
    );
  }, []);

  const GetAllEmpProjectInterestScore = async (
    pipelineCode: string,
    jobCode: string
  ) => {
    const responseObj: IGetAllEmpProjectInterestScoreResponse[] =
      await getAllEmpProjectInterestScore(pipelineCode, jobCode);

    const sortedResponse = responseObj.sort((a, b) =>
      a.requisitionId.localeCompare(b.requisitionId)
    );
    setRowData(sortedResponse);
  };

  const handleRadioButtonChange = (e) => {
    setSelectedUserRow([e]);
  };

  return (
    <>
      {showAvailabilityCommonScreen &&
        selectedUserRow &&
        selectedUserRow.length && (
          <MarketplaceInterestCommonAllocation
            requisitionId={selectedUserRow[0].requisitionId}
            emailId={selectedUserRow[0].empEmail}
            projectInfo={projectDetails}
            back={function (): void {
              setShowAvailabilityCommonScreen(false);
            }}
          />
        )}
      <Grid container spacing={2}>
        <Grid item xs={10.2} />
        <Grid item xs={0.3}>
          {selectedUserRow && selectedUserRow.length > 0 && (
            <Tooltip
              className="infoIconStyle"
              title={
                "Selected Resource is to be allocated via name allocation; hence requisition will continue to exist."
              }
              placement="right"
            >
              <InfoIcon />
            </Tooltip>
          )}
        </Grid>
        <Grid item xs={1.5}>
          {!projectDetails.isPublishedToMarketPlace && (
            <ActionButton
              label={"View Calendar"}
              onClick={function (e: any): void {
                setShowAvailabilityCommonScreen(true);
              }}
              disabled={!selectedUserRow || selectedUserRow.length === 0}
              type={"button"}
            />
          )}
        </Grid>
        <Grid item xs={12}>
          <div className="marketplace-interest-grid">
            <AgGridComponent
              ref={gridRef}
              rowData={rowData}
              columnDefs={columnDefs}
              gridComponentRef={gridComponentRef}
              tooltipShowDelay={0}
              tooltipHideDelay={2000}
              isPageination={true}
              pageSize={18}
              suppressCsvExport={true}
              suppressContextMenu={true}
              suppressExcelExport={true}
              isFilterVisible={true}
              hideExport={false}
              suppressCellFocus={true}
              height={"65.5vh"}
            ></AgGridComponent>
          </div>
        </Grid>
      </Grid>
    </>
  );
};

export default MarketplaceInterests;
