import { useEffect, useMemo, useRef } from "react";
import AgGridComponent from "../../../aggrid-component/aggrid-component";
import {
  IRequisitionMaster,
  IRequisitionParametersMaster,
} from "../../../../common/interfaces/IRequisition";
import {
  IFetchDetailsConfig,
  IScoreBreakup,
  ISystemSuggestions,
} from "../../interfaces";
import { capitalizeFirstLetter, routeToEmployeeProfile } from "../../../../global/utils";

interface ISuggestionsGridView {
  requisitionId: string;
  suggestionsSelected: Array<ISystemSuggestions>;
  setSuggestionsSelected: any;
  setShowAvailability: any;
  fetchDetailsConfig: IFetchDetailsConfig;
  setFetchDetailsConfig: any;
  userSuggestions: Array<ISystemSuggestions>;
  setUserSuggestions: any;
  requisitionDetails: IRequisitionMaster;
  isLoading: boolean;
  setIsLoading: any;
  fetchSystemSuggestions: any;
  updateSelections: any;
  list_ended: boolean;
  setUserSuggestionsFiltered: React.Dispatch<
    React.SetStateAction<ISystemSuggestions[]>
  >;
  setFilterStateOfGrid: any;
  filterStateOfGrid: any;
}

export enum ERequisitionParameters {
  competency = "competency",
  Same_client = "Same_client",
  Industry = "Industry",
  Sub_Industry = "Sub_Industry",
  Skills = "Skills",
  offerings = "offerings",
  solutions = "solutions",
  Location = "Location",
}

export enum ESkillType {
  Mandatory = "mandatory",
  Optional = "optional",
}

interface IParameterScoreAndValue {
  score: number | string;
  value: string;
}

export const getParameterScoreAndValue = (
  rowData: ISystemSuggestions,
  requiredParameter: ERequisitionParameters,
  requisitionParameters: IRequisitionParametersMaster[]
): IParameterScoreAndValue => {
  const isParameterChecked = checkIfRequisitionContainedSpecifiedParameter(
    requiredParameter,
    requisitionParameters
  );
  if (isParameterChecked) {
    const scoreBreakupItemFound: IScoreBreakup = rowData?.score_breakup?.find(
      (row) =>
        row?.parameter?.toLowerCase() === requiredParameter?.toLowerCase()
    );

    if (scoreBreakupItemFound) {
      return {
        score: scoreBreakupItemFound?.value,
        value: capitalizeFirstLetter(scoreBreakupItemFound.matching_value),
      };
    }
  }
  return {
    score: "-",
    value: "-",
  };
};

export const getSkillsValue = (
  rowData: ISystemSuggestions,
  requiredType: ESkillType
) => {
  const skillItemsFound = rowData?.skill?.filter(
    (skillItem) => skillItem?.type === requiredType
  );
  if (skillItemsFound && skillItemsFound.length > 0) {
    return skillItemsFound?.map((skillItem) => skillItem?.skillName).join(", ");
  }
  return "-";
};

export const checkIfRequisitionContainedSpecifiedParameter = (
  parameterToCheck: ERequisitionParameters,
  requisitionParameters: IRequisitionParametersMaster[]
) => {
  const parametersPresentInRequisition: string[] = requisitionParameters
    ?.filter(
      (item) => item?.category && item?.requisitionWeight > 0 && item.isChecked
    )
    ?.map((item) => item?.category);

  return (
    parametersPresentInRequisition &&
    parametersPresentInRequisition.length &&
    parametersPresentInRequisition.includes(parameterToCheck)
  );
};

const SuggestionsGridView = (props: ISuggestionsGridView) => {
  const suggestionsGridRef: any = useRef();

  const isRowSelectable = (params: any): boolean => {
    return !!params?.data?.available;
  };

  const columnDefs = useMemo(() => {
    return [
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
        checkboxSelection: isRowSelectable,
        showDisabledCheckboxes: true,
        cellRenderer: (params: any) => {
          return (
            <span
              style={{ cursor: "pointer", color: "blue" }}
              onClick={(e) => {
                e.stopPropagation();
                routeToEmployeeProfile(
                  `/employee-profile/${params.data.email}`
                );
              }}
            >
              {params.value}
            </span>
          );
        },
      },
      {
        headerName: "Availability",
        headerTooltip: "Availability",
        field: "available",
        filter: true,
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowData) =>
          rowData?.data?.available ? "Available" : "Not Available",
        tooltipValueGetter: (rowData) => rowData?.value || "-",
      },
      {
        headerName: "Marketplace Interest",
        headerTooltip: "Marketplace Interest",
        field: "interested",
        filter: true,
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowData) =>
          rowData?.data?.interested ? "Interested" : "Not Interested",
        tooltipValueGetter: (rowData) => rowData?.value || "-",
      },
      {
        headerName: "BU",
        headerTooltip: "BU",
        field: "business_unit",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "business_unit",
        menuTabs: ["filterMenuTab"],
      },
      {
        headerName: "Skill",
        headerTooltip: "Skill",
        field: "skill",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        valueGetter: (rowItem) => {
          return getSkillsValue(rowItem.data, ESkillType.Mandatory);
        },
        tooltipValueGetter: (rowData) => rowData?.value || "-",
      },
      {
        headerName: "Overall Score",
        headerTooltip: "Overall Score",
        field: "score",
        filter: "agNumberColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "score",
        menuTabs: ["filterMenuTab"],
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
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.competency,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.competency,
            props.requisitionDetails?.requisitionParameters
          )?.value;
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.competency,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.competency,
          props.requisitionDetails?.requisitionParameters
        ),
      },
      {
        headerName: "Offering",
        headerTooltip: "Offering",
        field: "offering",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "offering",
        menuTabs: ["filterMenuTab"],
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.offerings,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.offerings,
            props.requisitionDetails?.requisitionParameters
          )?.value;
        },
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.offerings,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.offerings,
          props.requisitionDetails?.requisitionParameters
        ),
      },
      {
        headerName: "Solution",
        headerTooltip: "Solution",
        field: "solutions",
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "solutions",
        menuTabs: ["filterMenuTab"],
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.solutions,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.solutions,
            props.requisitionDetails?.requisitionParameters
          )?.value;
        },
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.solutions,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.solutions,
          props.requisitionDetails?.requisitionParameters
        ),
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
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Location,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Location,
            props.requisitionDetails?.requisitionParameters
          )?.value;
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Location,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Location,
          props.requisitionDetails?.requisitionParameters
        ),
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
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Industry,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Industry,
            props.requisitionDetails?.requisitionParameters
          )?.value;
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Industry,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Industry,
          props.requisitionDetails?.requisitionParameters
        ),
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
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Sub_Industry,
          props.requisitionDetails?.requisitionParameters
        ),
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Sub_Industry,
            props.requisitionDetails?.requisitionParameters
          )?.value;
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Sub_Industry,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Sub_Industry,
          props.requisitionDetails?.requisitionParameters
        ),
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
          return getSkillsValue(rowItem.data, ESkillType.Optional);
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
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Skills,
            props.requisitionDetails?.requisitionParameters
          )?.score;
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
        valueGetter: (rowItem) => {
          return getParameterScoreAndValue(
            rowItem.data,
            ERequisitionParameters.Same_client,
            props.requisitionDetails?.requisitionParameters
          )?.score;
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: (rowData) => rowData?.value,
        hide: !checkIfRequisitionContainedSpecifiedParameter(
          ERequisitionParameters.Same_client,
          props.requisitionDetails?.requisitionParameters
        ),
      },
    ];
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [
    props.requisitionDetails,
    props.userSuggestions,
    props.suggestionsSelected,
  ]);

  const onSelectionChanged = (e) => {
    if (suggestionsGridRef) {
      const selectedRows = suggestionsGridRef?.current?.api?.getSelectedRows();
      props.updateSelections(selectedRows);
    }
  };

  const onFilterChanged = () => {
    const filteredNodes = [];
    suggestionsGridRef.current.api.forEachNodeAfterFilter((node) => {
      filteredNodes.push(node.data);
    });
    props.setUserSuggestionsFiltered(filteredNodes);
    const filterState = suggestionsGridRef?.current?.api?.getFilterModel();
    props.setFilterStateOfGrid(filterState);
  };

  useEffect(() => {
    if (suggestionsGridRef?.current) {
      suggestionsGridRef?.current?.api?.refreshCells();
    }
  }, [
    props.requisitionDetails,
    props.userSuggestions,
    props.suggestionsSelected,
  ]);

  useEffect(() => {
    setTimeout(() => {
      if (
        props.suggestionsSelected &&
        suggestionsGridRef &&
        suggestionsGridRef?.current?.api
      ) {
        suggestionsGridRef?.current?.api?.deselectAll();
        suggestionsGridRef?.current?.api?.forEachNode((node: any) => {
          if (
            props.suggestionsSelected.find(
              (item) => item.email === node.data.email
            )
          )
            node.setSelected(true);
        });
        suggestionsGridRef?.current?.api?.setFilterModel(
          props.filterStateOfGrid
        );
      }
    }, 100);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const gridOptions = useMemo(() => {
    return {
      suppressCellSelection: true,
      rowSelection: "multiple",
      isRowSelectable: isRowSelectable,
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.suggestionsSelected, props.updateSelections]);

  return (
    <AgGridComponent
      columnDefs={columnDefs}
      rowData={props.userSuggestions}
      isPageination={true}
      onSelectionChanged={onSelectionChanged}
      rowSelection={"multiple"}
      onFilterChanged={onFilterChanged}
      gridOptions={gridOptions}
      gridComponentRef={suggestionsGridRef}
      pageSize={40}
      isFilterVisible={true}
    />
  );
};
export default SuggestionsGridView;
