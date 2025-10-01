/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect, useContext, memo } from "react";
import SystemSuggestionCardView from "./system-suggestions-card-view";
import {
  ISystemSuggestions,
  IFetchDetailsConfig,
  SortingCardsBy,
} from "./interfaces";
import { fetchSystemSuggestionsByReqId } from "../../services/allocation/allocation.service";
import { SnackbarContext } from "../../contexts/snackbarContext";
import {
  Autocomplete,
  Grid,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import ActionButton from "../actionButton/actionButton";
import BackActionButton from "../actionButton/backactionButton";
import { AllocateEmployeesState } from "../../contexts/allocateEmployeesContext";
import InfoIcon from "@mui/icons-material/Info";
import SystemSuggestionCardLegends from "./systemSuggestionCardLegends";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";
import "./style.css";
import CommonAllocationWrapper from "../common-allocation/commonAllocationWrapper";
import { EAllocationType } from "../common-allocation/enum";
import SystemSuggestionsFilter from "./system-suggestions-filter/system-suggestions-filter";
import KeyboardArrowUpOutlinedIcon from "@mui/icons-material/KeyboardArrowUpOutlined";
import KeyboardArrowDownOutlinedIcon from "@mui/icons-material/KeyboardArrowDownOutlined";
import { SortingCardsLabelSxProps, SortingIconsSxProps } from "./constants";
import { IRequisitionMaster } from "../../common/interfaces/IRequisition";
import { IProjectMaster } from "../../common/interfaces/IProject";
import SuggestionsGridView from "./requisition-form-system-suggestions/suggestions-grid-view/suggestions-grid-view";
import ReorderOutlinedIcon from "@mui/icons-material/ReorderOutlined";
import ViewAgendaOutlinedIcon from "@mui/icons-material/ViewAgendaOutlined";
import BackDropModal from "../../common/back-drop-modal/backDropModal";
import filterImage from "../../common/images/filter.png";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import FilterAltIcon from "@mui/icons-material/FilterAlt";

const defaultFetchConfig = {
  limit: 50,
  pagination: 1,
};

enum SortValues {
  DESC = "DESC",
  ASC = "ASC",
}

export enum ViewTypeOfSuggestions {
  GridView = "GridView",
  CardsView = "CardsView",
}

interface ISystemSuggestionLayout {
  requisitionId: string;
  projectInfo: IProjectMaster;
  requisitionDetails: IRequisitionMaster;
  showBackActionButton: boolean;
  resetActiveRequisitions: () => void;
  baseViewType: ViewTypeOfSuggestions;
  showSuggestionsViewToggle: boolean;
  openCommonAllocationInPopup: boolean;
  useCustomFilter: boolean;
  useGridFilter: boolean;
  useLazyLoading: boolean;
}

const SystemSuggestionLayout = (props: ISystemSuggestionLayout) => {
  const snackbarContext: any = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [isFilterOpen, setOpenFilter] = useState<boolean>(false);
  const [filterValues, setFilterValues] = useState(null);
  const [sortingBy, setSortingBy] = useState<SortingCardsBy | null>({
    type: "Score",
    value: SortValues.DESC,
  });
  const [filterPayload, setFilterPayload] = useState<string[] | null>([]);
  const [showAvailability, setShowAvailability] = useState<boolean>(false);
  const [userSuggestions, setUserSuggestions] = useState<
    Array<ISystemSuggestions>
  >([]);
  const [userSuggestionsFiltered, setUserSuggestionsFiltered] = useState<
    Array<ISystemSuggestions>
  >([]);
  const [filterStateOfGrid, setFilterStateOfGrid] = useState<any>(null);
  const [suggestionsSelected, setSuggestionsSelected] = useState<
    Array<ISystemSuggestions>
  >([]);
  const [fetchDetailsConfig, setFetchDetailsConfig] =
    useState<IFetchDetailsConfig>(defaultFetchConfig);

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [list_ended, setListEnded] = useState<boolean>(false);
  const [selectedViewOfSuggestions, setSelectedViewOfSuggestions] =
    useState<ViewTypeOfSuggestions>(ViewTypeOfSuggestions.GridView);
  // const [filtersApplied, setFiltersApplied] = useState();

  const updateSelections = (selections: Array<ISystemSuggestions>) => {
    setSuggestionsSelected([]);
    setSuggestionsSelected(selections);
  };

  const openCloseLoader = (startLoader: boolean = false) => {
    if (props.baseViewType === ViewTypeOfSuggestions.GridView) {
      loaderContext.open(startLoader);
    } else {
      setIsLoading(startLoader);
    }
  };

  const fetchSystemSuggestions = async (
    requisitionId: string,
    pagination: number
  ): Promise<string> => {
    return new Promise<string>((resolve, reject) => {
      fetchSystemSuggestionsByReqId(
        requisitionId,
        props.useLazyLoading ? fetchDetailsConfig.limit : 1000,
        pagination,
        filterPayload
      )
        .then((suggestionsResp) => {
          if (suggestionsResp.data.length > 0) {
            setUserSuggestions((prev: ISystemSuggestions[]) => [
              ...prev,
              ...suggestionsResp.data,
            ]);
            setUserSuggestionsFiltered((prev: ISystemSuggestions[]) => [
              ...prev,
              ...suggestionsResp.data,
            ]);
          } else {
            setListEnded(true);
          }
          resolve("");
        })
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error Fetching suggestions",
            "error",
            6000
          );
          reject(err);
        });
    });
  };

  const initialFetch = () => {
    openCloseLoader(true);
    Promise.all([
      fetchSystemSuggestions(
        props.requisitionId,
        fetchDetailsConfig.pagination
      ),
    ])
      .then(() => {
        openCloseLoader(false);
      })
      .catch((err) => {
        openCloseLoader(false);
      });
  };

  const refreshAllSuggestionsForFilterOrSort = () => {
    setFetchDetailsConfig(defaultFetchConfig);
    setUserSuggestions([]);
    setListEnded(false);
    openCloseLoader(true);
    setTimeout(() => {
      initialFetch();
    }, 500);
  };

  const sortHandlerUp = () => {
    setSortingBy((prev) => ({ ...prev, value: SortValues.ASC }));
  };

  const sortHandlerDown = () => {
    setSortingBy((prev) => ({ ...prev, value: SortValues.DESC }));
  };

  useEffect(() => {
    const finalFiltersPayload: string[] = [];
    if (filterValues) {
      Object.keys(filterValues).forEach((item) => {
        if (filterValues[item]) {
          finalFiltersPayload.push(`${item},${filterValues[item]}`);
        }
      });
    }
    if (sortingBy?.value) {
      finalFiltersPayload.push(`sorting,${sortingBy?.value}`);
    }
    setFilterPayload(finalFiltersPayload);
  }, [filterValues, sortingBy]);

  useEffect(() => {
    if (filterPayload && filterPayload.length > 0) {
      refreshAllSuggestionsForFilterOrSort();
    }
  }, [filterPayload]);

  useEffect(() => {
    setSelectedViewOfSuggestions(props.baseViewType);
  }, [props.baseViewType]);

  return (
    <>
      {!showAvailability ? (
        <>
          <Grid container spacing={2}>
            {props.useCustomFilter && (
              <>
                <Grid item xs={0.45}>
                  <SystemSuggestionsFilter
                    isFilterOpen={isFilterOpen}
                    setOpenFilter={setOpenFilter}
                    requisitionDetails={props?.requisitionDetails}
                    setFilterValues={setFilterValues}
                    filterValues={filterValues}
                  />
                </Grid>
                <Grid item xs={0.25} alignItems="center">
                  {selectedViewOfSuggestions ===
                    ViewTypeOfSuggestions.CardsView &&
                    !props.useGridFilter &&
                    filterValues &&
                    Object.keys(filterValues).length > 0 && (
                      <>
                        <Typography component="span">
                          <Tooltip title="Filters applied">
                            <FilterAltIcon fontSize="large" />
                          </Tooltip>
                        </Typography>
                      </>
                    )}
                </Grid>

                <Grid item xs={0.2} className="gridOne" alignItems="center">
                  <Tooltip title={SortValues.ASC}>
                    <KeyboardArrowUpOutlinedIcon
                      className="sortIcon-btn"
                      onClick={sortHandlerUp}
                      sx={{
                        ...SortingIconsSxProps,
                        fontSize:
                          (sortingBy?.value === SortValues.ASC
                            ? "large"
                            : "large") + "!important",
                      }}
                    />
                  </Tooltip>
                  <Tooltip title={SortValues.DESC}>
                    <KeyboardArrowDownOutlinedIcon
                      className="sortIcon-btn"
                      onClick={sortHandlerDown}
                      sx={{
                        ...SortingIconsSxProps,
                        fontSize:
                          (sortingBy?.value === SortValues.DESC
                            ? "large"
                            : "large") + "!important",
                      }}
                    />
                  </Tooltip>
                </Grid>
                <Grid
                  item
                  xs={0.5}
                  className="gridOne gridCenterAlign"
                  alignItems="center"
                  sx={SortingCardsLabelSxProps}
                >
                  <Typography component={"span"}>Sort By:</Typography>
                </Grid>
                <Grid
                  item
                  xs={1.8}
                  className="gridOne gridCenterAlign select-control-market"
                  alignItems="center"
                >
                  <Autocomplete
                    value={sortingBy?.type}
                    options={["Score"]}
                    onChange={(e) => {}}
                    id="flat-demo"
                    size="small"
                    disableClearable
                    renderInput={(params) => (
                      <TextField
                        style={{ width: "200px" }}
                        {...params}
                        label=""
                      />
                    )}
                  />
                </Grid>
              </>
            )}

            <Grid
              item
              xs={props.showSuggestionsViewToggle ? 1.5 : 3.5}
              className="gridCenterAlign headerItem"
            >
              {props.requisitionDetails?.demands?.allResourcesHaveSameDetails
                ? props.requisitionDetails?.demands?.pendingDemands
                : 1}{" "}
              {props?.requisitionDetails?.designation
                ? props?.requisitionDetails?.designation
                : ""}{" "}
              Required
            </Grid>
            {props.showSuggestionsViewToggle && (
              <Grid item xs={2} className="gridCenterAlign headerItem">
                <Tooltip placement="top" title={"Grid View"}>
                  <ReorderOutlinedIcon
                    fontSize="large"
                    style={{
                      border:
                        selectedViewOfSuggestions ===
                        ViewTypeOfSuggestions.GridView
                          ? `2px solid ${GT_DESIGN_PARAMETERS.GtPrimaryColorPurple}`
                          : "none",
                    }}
                    onClick={() => {
                      setSelectedViewOfSuggestions(
                        ViewTypeOfSuggestions.GridView
                      );
                    }}
                  />
                </Tooltip>
                <Tooltip placement="top" title={"Cards View"}>
                  <ViewAgendaOutlinedIcon
                    fontSize="large"
                    style={{
                      border:
                        selectedViewOfSuggestions ===
                        ViewTypeOfSuggestions.CardsView
                          ? `2px solid ${GT_DESIGN_PARAMETERS.GtPrimaryColorPurple}`
                          : "none",
                    }}
                    onClick={() => {
                      setSelectedViewOfSuggestions(
                        ViewTypeOfSuggestions.CardsView
                      );
                    }}
                  />
                </Tooltip>

                <Tooltip
                  placement="top"
                  title={
                    "Please note filters applied on search results in the table in  will be applicable in the card view. You may click on the Clear Filters button to clear any filters that may have been applied. To change any filter options please use the filter functionality in the table."
                  }
                >
                  <InfoIcon
                    sx={{
                      color: GT_DESIGN_PARAMETERS.GTTealColor,
                    }}
                  />
                </Tooltip>
              </Grid>
            )}
            {!props.useCustomFilter && <Grid item xs={3} />}

            <Grid
              item
              xs={props.showBackActionButton ? 2.8 : 3.8}
              className="gridCenterAlign gridJustifyEnd"
            >
              {selectedViewOfSuggestions === ViewTypeOfSuggestions.CardsView &&
                props.useGridFilter &&
                filterStateOfGrid &&
                Object.keys(filterStateOfGrid).length > 0 && (
                  <>
                    <Typography component="span">
                      <Tooltip title="Clear all filter">
                        <img
                          src={filterImage}
                          height={25}
                          width={25}
                          alt="Clear Filter"
                          onClick={() => {
                            setFilterStateOfGrid(null);
                            setUserSuggestionsFiltered(userSuggestions);
                          }}
                        />
                      </Tooltip>
                    </Typography>
                  </>
                )}
            </Grid>
            <Grid item xs={0.2} className="gridCenterAlign gridJustifyEnd">
              {selectedViewOfSuggestions ===
                ViewTypeOfSuggestions.CardsView && (
                <Typography component="span">
                  <Tooltip
                    placement="top"
                    title={<SystemSuggestionCardLegends />}
                  >
                    <InfoIcon
                      sx={{
                        color: GT_DESIGN_PARAMETERS.GTTealColor,
                      }}
                    />
                  </Tooltip>
                </Typography>
              )}
            </Grid>

            {props.showBackActionButton && (
              <Grid item xs={0.9}>
                <BackActionButton
                  label={"Cancel"}
                  onClick={function (e: any): void {
                    props.resetActiveRequisitions();
                  }}
                />
              </Grid>
            )}

            <Grid item xs={1.4}>
              <ActionButton
                label={"View Calendar"}
                onClick={function (e: any): void {
                  setShowAvailability(true);
                }}
                disabled={suggestionsSelected.length === 0}
                type={"button"}
              />
            </Grid>
            {selectedViewOfSuggestions === ViewTypeOfSuggestions.CardsView && (
              <Grid item xs={12}>
                <SystemSuggestionCardView
                  suggestionsSelected={suggestionsSelected}
                  requisitionId={props.requisitionId}
                  updateSelections={updateSelections}
                  fetchDetailsConfig={fetchDetailsConfig}
                  setFetchDetailsConfig={setFetchDetailsConfig}
                  userSuggestions={
                    props.useLazyLoading
                      ? userSuggestions
                      : userSuggestionsFiltered
                  }
                  requisitionDetails={props.requisitionDetails}
                  isLoading={isLoading}
                  setIsLoading={setIsLoading}
                  fetchSystemSuggestions={fetchSystemSuggestions}
                  list_ended={list_ended}
                />
              </Grid>
            )}
            {selectedViewOfSuggestions === ViewTypeOfSuggestions.GridView && (
              <Grid item xs={12}>
                <SuggestionsGridView
                  suggestionsSelected={suggestionsSelected}
                  requisitionId={props.requisitionId}
                  setSuggestionsSelected={setSuggestionsSelected}
                  updateSelections={updateSelections}
                  setShowAvailability={setShowAvailability}
                  fetchDetailsConfig={fetchDetailsConfig}
                  setFetchDetailsConfig={setFetchDetailsConfig}
                  userSuggestions={userSuggestions}
                  setUserSuggestions={setUserSuggestions}
                  requisitionDetails={props.requisitionDetails}
                  isLoading={isLoading}
                  setIsLoading={setIsLoading}
                  fetchSystemSuggestions={fetchSystemSuggestions}
                  list_ended={list_ended}
                  setUserSuggestionsFiltered={setUserSuggestionsFiltered}
                  filterStateOfGrid={filterStateOfGrid}
                  setFilterStateOfGrid={setFilterStateOfGrid}
                />
              </Grid>
            )}
          </Grid>
        </>
      ) : (
        <>
          {props.openCommonAllocationInPopup ? (
            <BackDropModal
              open={showAvailability}
              onclose={() => {
                setShowAvailability(false);
                return;
              }}
              restrictOnClose={true}
              style={{
                width: "95%",
                height: "90vh",
                borderRadius: "15px",
              }}
            >
              <Typography component="div">
                <AllocateEmployeesState>
                  <CommonAllocationWrapper
                    requisition={props.requisitionDetails}
                    suggestionsSelected={suggestionsSelected}
                    back={function (): {} {
                      setShowAvailability(false);
                      return;
                    }}
                    projectInfo={props.projectInfo}
                    baseType={EAllocationType.SYSTEM_SUGGESTED_ALLOCATION}
                  />
                </AllocateEmployeesState>
              </Typography>
            </BackDropModal>
          ) : (
            <AllocateEmployeesState>
              <CommonAllocationWrapper
                requisition={props.requisitionDetails}
                suggestionsSelected={suggestionsSelected}
                back={function (): {} {
                  setShowAvailability(false);
                  return;
                }}
                projectInfo={props.projectInfo}
                baseType={EAllocationType.SYSTEM_SUGGESTED_ALLOCATION}
              />
            </AllocateEmployeesState>
          )}
        </>
      )}
    </>
  );
};
export default memo(SystemSuggestionLayout);
