import { getBarChartData } from "./service/bar_chart_service";
import { useContext, useEffect, useRef, useState } from "react";
import BarHoursCharts from "./bar_hours_chart";
import CostBarCharts from "./bar_cost_chart";
import "./bar_chart.scss";
import "../custom-tootlip/custom_tooltip.scss";
import AgGridComponent from "../../../../aggrid-component/aggrid-component";
import { AgGridReact } from "ag-grid-react";
import { CHART_VIEW_TYPE, CHARTTYPE, defaultColDef } from "./constant";
import {
  formateDateAfterRendering,
  sortdate,
  transformData,
  formateDateWithoutChangingData,
} from "../utils";
import CustomReportModal from "../modal/Modal";
import { TOGGLE_CONSTANTS } from "../../../constant";
import moment from "moment";
import { UserDetailsContext } from "../../../../../contexts/userDetailsContext";
import {
  GetFilterDefaultValueOnTheBasisOfRole,
  IsCeoCoo,
  IsEmployeeOnly,
  IsLeader,
  IsSystemAdmin,
} from "../../../util";
import {
  EReportDashboardFilterControl,
  IReportDashboardFilterControl,
} from "../../../Filters/uitls";
import {
  FormControlLabel,
  Grid,
  Switch,
  SwitchProps,
  Tooltip,
  styled,
} from "@mui/material";
import { LoaderContext } from "../../../../../contexts/loaderContext";
import { SnackbarContext } from "../../../../../contexts/snackbarContext";
import ControllerCalendar from "../../../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import ControllerAutoCompleteChipsSimple from "../../../../controllerInputs/controllerAutoCompleteChipsSimple";
import { CancelScheduleSendSharp } from "@mui/icons-material";
import { routeToEmployeeProfile } from "../../../../../global/utils";

//Schedule Variance Chart
const BarCharts = (props) => {
  const {
    control,
    setValue,
    getValues,
    trigger,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });

  const { chartType, filterParameters } = props;
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [selectedChartData, setSelectedChartData] = useState(null);
  const [chartData, setChartData] = useState<string[]>([]);
  const [modalRowData, setModalRowData] = useState<any[]>([]);
  const [rowData, setRowData] = useState<any[]>([]);
  const [colDef, setColDef] = useState<any[]>([]);
  const userDetails = useContext(UserDetailsContext);
  const [isEmployeeModeOn, setIsEmployeeModeOn] = useState<boolean>(
    IsEmployeeOnly(userDetails)
  );
  const [isLeader, setIsLeader] = useState<boolean>(IsLeader(userDetails));
  const [isCeoCoo, setIsCeoCoo] = useState<boolean>(IsCeoCoo(userDetails));
  const [isSystemAdmin, setIsSystemAdmin] = useState<boolean>(
    IsSystemAdmin(userDetails)
  );
  const [isPageLoaded, setIsPageLoaded] = useState<Boolean>(false);
  const gridRef: any = useRef();
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const [gridData, setGridData] = useState<any[]>();
  const [chartViewoptions, setChartViewoptions] = useState<any[]>();
  const [selectedChartViewType,SetSelectedChartViewType] = useState(CHARTTYPE.EmployeeView);

  useEffect(() => {
    // Below Code Commented Due to multiple api calls on state change
    // var flag = IsEmployeeOnly(userDetails);
    // setIsEmployeeModeOn(flag);

    // flag = IsLeader(userDetails);
    // setIsLeader(flag);
    if (isPageLoaded) {
      // const filterDefaultValue = GetFilterDefaultValueOnTheBasisOfRole();
      loadDataFromService(filterParameters);
    }
  }, [filterParameters, userDetails]);

  useEffect(()=>{
    getAvailableChartViews(props?.currentUserRoleView);
  },[props.currentUserRoleView]);

  const getAvailableChartViews = ( 
    userRoles: string[]
  ) => {
    const chartview =  CHART_VIEW_TYPE
    .filter(view => view.role.some(role => userRoles.includes(role)))
    .map(view => view.title);
    const defaultView = CHART_VIEW_TYPE
    .filter(view => view.defaultSelect == true)
    .map(view => view.title);
    setValue("report_role_based_view",defaultView);
    setChartViewoptions(chartview);
  };


  const IOSSwitch = styled((props: SwitchProps) => (
    <Switch
      focusVisibleClassName=".Mui-focusVisible"
      disableRipple
      {...props}
    />
  ))(({ theme }) => ({
    width: 42,
    height: 26,
    padding: 0,
    "& .MuiSwitch-switchBase": {
      padding: 0,
      margin: 2,
      transitionDuration: "300ms",
      "&.Mui-checked": {
        transform: "translateX(16px)",
        color: "#fff",
        "& + .MuiSwitch-track": {
          backgroundColor:
            theme.palette.mode === "dark" ? "#2ECA45" : "#00a7b5 ",
          opacity: 1,
          border: 0,
        },
        "&.Mui-disabled + .MuiSwitch-track": {
          opacity: 0.5,
        },
      },
      "&.Mui-focusVisible .MuiSwitch-thumb": {
        color: "#33cf4d",
        border: "6px solid #fff",
      },
      "&.Mui-disabled .MuiSwitch-thumb": {
        color:
          theme.palette.mode === "light"
            ? theme.palette.grey[100]
            : theme.palette.grey[600],
      },
      "&.Mui-disabled + .MuiSwitch-track": {
        opacity: theme.palette.mode === "light" ? 0.5 : 0.3,
      },
    },
    "& .MuiSwitch-thumb": {
      boxSizing: "border-box",
      width: 22,
      height: 22,
    },
    "& .MuiSwitch-track": {
      borderRadius: 26 / 2,
      backgroundColor: theme.palette.mode === "light" ? "#787878" : "#39393D",
      opacity: 1,
      transition: theme.transitions.create(["background-color"], {
        duration: 500,
      }),
    },
  }));

  useEffect(() => {
    if (props.chartType) {
      if (
        props.chartType.toLowerCase().trim() ===
        TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
      ) {
        const timeValue = GetAgGridColumnDefForTime();
        setColDef(timeValue);
      } else {
        const costValue = GetAgGridColumnDefForCost();
        setColDef(costValue);
      }
    }
  }, [props.chartType]);

  useEffect(() => {
    if (!isPageLoaded) {
      //reset the filter values to default values
      const filterDefaultData = GetFilterDefaultValueOnTheBasisOfRole();
      props.setFilterParameters(filterDefaultData);
      setIsPageLoaded(true);
    }
  }, [selectedChartViewType]);

  const applyFilter = (filterData) => {
    loadDataFromService(filterData);
  };

  const loadDataFromService = (fltData: IReportDashboardFilterControl) => {
    loaderContext.open(true);
    const start_date: string = moment(
      props.filterParameters[EReportDashboardFilterControl.start_date]
    ).format("YYYY-MM-DD");
    const end_date: string = moment(
      props.filterParameters[EReportDashboardFilterControl.end_date]
    ).format("YYYY-MM-DD");
    const filterEmail = isEmployeeModeOn ? userDetails.username : null;
    if (CancelScheduleSendSharp)
    return new Promise((resolve, reject) => {
      getBarChartData(
        start_date,
        end_date,
        props.filterParameters[EReportDashboardFilterControl.businessUnit],
        props.filterParameters[EReportDashboardFilterControl.location],
        props.filterParameters[EReportDashboardFilterControl.designation],
        props.filterParameters[EReportDashboardFilterControl.offering],
        props.filterParameters[EReportDashboardFilterControl.solution],
        props.filterParameters[EReportDashboardFilterControl.competency],
        selectedChartViewType,
        filterEmail
      )
        .then((resp) => {
          if (
            props.isFilterApplied &&
            resp?.data?.length > process.env.REACT_APP_CHART_DATA_LIMIT
          ) {
            setChartData([]);
            setRowData([]);
            setGridData([]);
            loaderContext.open(false);
            props.setisFilterApplied(false);
            snackbarContext.displaySnackbar(
              "Please apply additional filters as the result includes records greater then 10K",
              "error"
            );
          } else {
            loaderContext.open(true);

            const responseData = resp.data;
            const formattedRespData =
            formateDateWithoutChangingData(responseData);
            setRowData(formattedRespData);
            setGridData(formattedRespData);
            setChartDataOrGridData(responseData);
            loaderContext.open(false);
            resolve(resp.data);
          }
        })
        .catch((ex) => {
          console.log(ex);
          loaderContext.open(false);
        });
    });
  };

  const setChartDataOrGridData = (data: any)=> {
    const formattedRespData =
    formateDateWithoutChangingData(data);
    const res = transformData(data);
    let sortedData = sortdate(res);
    sortedData = formateDateAfterRendering(sortedData);
    // const gridRes = transformDataForGrid(resp.data);
    setChartData(res);
  }

  const handleBarClick = (
    event: any,
    dataKeySelected: string,
    index: any,
    data
  ) => {
    setIsOpen(true);
    // console.log(event, index, dataKeySelected, data);
    setModalRowData(rowData.filter((row) => row.newDate === event.date));
    const { name } = event;
  };

  const gridOptions = {
    suppressCellSelection: true,
    rowSelection: "multiple",
  };
  const onGridReady = (params: any) => {
    params.columnApi.autoSizeAllColumns();
    const gridColumnApi = params.columnApi;
    const allColumnIds: string[] = [];
    if (gridColumnApi !== undefined) {
      gridColumnApi.getColumns().forEach((column: any) => {
        allColumnIds.push(column.getId());
      });
      gridColumnApi.autoSizeColumns(allColumnIds);
      gridColumnApi.sizeColumnsToFit();
    }
  };

  function onSelectionChanged(e: any) {
    var selectedRows = e.api.getSelectedRows();
  }
  useEffect(() => {
    props.setIsEmployeeViewGraph(true);
  }, []);
  const switchChangeHandler = (event, value) => {
    // if (!value) {
    //   props.setIsEmployeeViewGraph(true);
    // } else {
    //   props.setIsEmployeeViewGraph(false);
    // }
    setIsPageLoaded(false);
    setIsEmployeeModeOn(!isEmployeeModeOn);
    const chartype = CHART_VIEW_TYPE.find(view=> view.title == event);
    if (chartType.key == CHARTTYPE.EmployeeView) {
      props.setIsEmployeeViewGraph(true);
    }else {
      props.setIsEmployeeViewGraph(false);
    }
    SetSelectedChartViewType(chartype ? chartype.key : CHARTTYPE.EmployeeView);    
    props.setSchedularViewChartType(event);
    props.setisFilterApplied(false);
  };

  useEffect(() => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  }, [props.filterParameters]);

  console.log(gridComponentRef);
  console.log(gridRef);

  const onFilterChanged = () => {
    loaderContext.open(true);
    const api = gridComponentRef?.current?.api;
    const filteredData: any[] = [];  
    api?.forEachNodeAfterFilter((node) => {
      if (node.data) {
        const rowCopy = { ...node.data };
    if (rowCopy.date && typeof rowCopy.date === "string") {
      rowCopy.date = `${rowCopy.year}-${rowCopy.month}`;
    }
    filteredData.push(rowCopy);
      }
    });  
    setChartDataOrGridData(filteredData);
    loaderContext.open(false);
  }; 
  
  return (
    <>
      <div className="question">
        <div className="main-toggle-div"></div>
        <div>
          <Grid container spacing={2}>
            <Grid item xs={2}>
              <ControllerCalendar
                name={EReportDashboardFilterControl.start_date}
                control={control}
                defaultValue={null}
                label={"Start Date"}
                maxDate={moment(
                  getValues(EReportDashboardFilterControl.end_date)
                ).toDate()}
                error={errors?.start_date ? true : false}
                minDate={moment(
                  getValues(EReportDashboardFilterControl.end_date)
                )
                  .add(-1, "year")
                  .toDate()}
                onChange={(date: any) => {
                  trigger(EReportDashboardFilterControl.end_date);
                  props.setFilterParameters(getValues());
                }}
              />
            </Grid>
            <Grid item xs={2}>
              <ControllerCalendar
                name={EReportDashboardFilterControl.end_date}
                control={control}
                defaultValue={null}
                label={"End Date"}
                maxDate={moment(
                  getValues(EReportDashboardFilterControl.start_date)
                )
                  .add(1, "year")
                  .toDate()}
                minDate={moment(
                  getValues(EReportDashboardFilterControl.start_date)
                ).toDate()}
                error={errors?.end_date ? true : false}
                onChange={(date: any) => {
                  props.setFilterParameters(getValues());
                  trigger(EReportDashboardFilterControl.start_date);
                }}
              />
            </Grid>
            <Grid item xs={4}>              
                <>
                   <ControllerAutoCompleteChipsSimple
                  name={"report_role_based_view"}
                  control={control}
                  freeSolo={false}
                  defaultValue={[]}
                  label={"Report View"}
                  options={chartViewoptions || []}                  
                  onChange={switchChangeHandler}
                />
                </>
            </Grid> 
          </Grid>
        </div>

        <div className="question-container">
          {chartType === TOGGLE_CONSTANTS.TIME ? (
            <BarHoursCharts
              data={chartData}
              gridData={gridData}
              chartType={chartType}
              handleBarClick={handleBarClick}
              setRowData={setRowData}
              setModalRowData={setModalRowData}
              rowData={rowData}
              // agColumns={agColumns}
              // setagColumns={setagColumns}
              filterParameters={filterParameters}
              applyFilter={applyFilter}
              setIsOpen={setIsOpen}
              selectedChartData={selectedChartData}
            />
          ) : (
            <CostBarCharts
              data={chartData}
              chartType={chartType}
              handleBarClick={handleBarClick}
              gridData={gridData}
              setRowData={setRowData}
              setModalRowData={setModalRowData}
              rowData={rowData}
              // agColumns={agColumns}
              // setagColumns={setagColumns}
              filterParameters={filterParameters}
              applyFilter={applyFilter}
              setIsOpen={setIsOpen}
              selectedChartData={selectedChartData}
            />
          )}
        </div>
      </div>

      <CustomReportModal
        isOpen={isOpen}
        setIsOpen={setIsOpen}
        modalRowData={modalRowData}
        colDef={colDef}
        selectedChartData={selectedChartData}
      />
      <div className="chart-tabular-data">
        <p></p>
      </div>
      <div style={{ margin: "15px" }}>
        <AgGridComponent
          ref={gridRef}
          gridComponentRef={gridComponentRef}
          rowData={rowData}
          columnDefs={colDef}
          defaultColDef={defaultColDef}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={true}
          suppressContextMenu={true}
          suppressExcelExport={true}
          isFilterVisible={true}
          hideExport={false}
          gridOptions={gridOptions}
          suppressCellFocus={true}
          onGridReady={onGridReady}
          onSelectionChanged={onSelectionChanged}
          rowSelection={"multiple"}
          rowMultiSelectWithClick={true}
          suppressRowClickSelection={true}
          height={"61.2vh"}
          exportedFileName={"ScheduledVsVarianceReport"}
          onFilterChanged= {onFilterChanged}
        ></AgGridComponent>
      </div>
    </>
  );
};

export default BarCharts;
export const GetAgGridColumnDefForTime = () => {
  const columnDefs: any = [
    {
      headerName: "Month",
      field: "newDate",
      suppressMenu: true,
      tooltipField: "action",
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {dayjs(new Date(params?.data?.date))
      //         .format(DATETIME_FORMAT)
      //         .toUpperCase()}
      //     </span>
      //   );
      // },
    },
    {
      headerName: "Employee",
      headerTooltip: "Employee",
      field: "employee_name",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
      cellRenderer: (params: any) => {
        return (
          <span
            style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
            onClick={() => {routeToEmployeeProfile(`/employee-profile/${params.data.employee_mid}__${params.data.email_id}`);}}
          >
            {params.value}
          </span>
        );
      },
    },
    {
      headerName: "Designation",
      headerTooltip: "Designation",
      field: "designation_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Grade",
      headerTooltip: "Grade",
      field: "grade",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
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
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },

    {
      headerName: "BU",
      headerTooltip: "BU",
      field: "business_unit",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Competency",
      headerTooltip: "Competency",
      field: "competency",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    // {
    //   headerName: "Expertise",
    //   headerTooltip: "Expertise",
    //   field: "expertise",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    // {
    //   headerName: "SMEG",
    //   headerTooltip: "SMEG",
    //   field: "sme_group_name",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    {
      headerName: "Net Capacity (Hrs)",
      headerTooltip: "Net Capacity (Hrs)",
      field: "capacity",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Leave Hours",
      headerTooltip: "Leave Hours",
      field: "net_leaves",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "net_leaves",
    },
    {
      headerName: "Available Hours",
      headerTooltip: "Available Hours",
      field: "availability",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Allocation Hours",
      field: "allocation_hours",
      headerTooltip: "Allocation Hours",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Actual Hours",
      field: "actual_log_hours",
      headerTooltip: "Actual Hours",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Allocation %",
      headerTooltip: "Allocation %",
      field: "allocation_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {params?.data?.capacity > 0
      //         ? (
      //             ((params?.data?.job_chargeable_hours +
      //               params?.data?.job_non_chargeable_hours) /
      //               params?.data?.capacity) *
      //             100
      //           ).toFixed(2)
      //         : 0}
      //     </span>
      //   );
      // },
      tooltipField: "allocation_percent",
    },
    {
      headerName: "Allocation Job Chargeable %",
      headerTooltip: "Allocation Job Chargeable %",
      field: "job_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.allocated_chargable_hr /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Allocation Job non-chargeable %",
      headerTooltip: "Allocation Job non-chargeable %",
      field: "job_non_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.allocated_non_chargable_hr /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Actual Job Chargeable %",
      headerTooltip: "Actual Job Chargeable %",
      field: "actual_job_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.job_chargeable_hours /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Actual Job non-chargeable %",
      headerTooltip: "Actual Job non-chargeable %",
      field: "actual_job_non_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.job_non_chargeable_hours /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Allocation Chargeability %",
      headerTooltip: "Allocation Chargeability %",
      field: "allocation_chargability_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {params?.data?.capacity > 0
      //         ? (
      //             (params?.data?.job_chargeable_hours /
      //               params?.data?.capacity) *
      //             100
      //           ).toFixed(2)
      //         : 0}
      //     </span>
      //   );
      // },
      tooltipField: "allocation_chargability_percent",
    },
    {
      headerName: "Actual Chargeability %",
      headerTooltip: "Actual Chargeability %",
      field: "actual_chargability_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {params?.data?.capacity > 0
      //         ? (
      //             (params?.data?.job_chargeable_hours /
      //               params?.data?.capacity) *
      //             100
      //           ).toFixed(2)
      //         : 0}
      //     </span>
      //   );
      // },
      tooltipField: "actual_chargability_percent",
    },
    {
      headerName: "Actual%",
      headerTooltip: "Actual%",
      field: "actual_percentage",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.actual_log_hours / // DONE as PER SHEET Provided BY BA Allocation hours/ Capacity Hours *100
                    params?.data?.capacity) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Supercoach",
      headerTooltip: "Supercoach",
      field: "supercoach_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      }
    },
    {
      headerName: "Co Super Coach",
      headerTooltip: "Co Super Coach",
      field: "csc_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      }
    }
  ];
  return columnDefs;
};

export const GetAgGridColumnDefForCost = () => {
  const columnDefs: any = [
    {
      headerName: "Month",
      field: "newDate",
      suppressMenu: true,
      tooltipField: "action",
    },
    {
      headerName: "Employee",
      headerTooltip: "Employee",
      field: "employee_name",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
      cellRenderer: (params: any) => {
        return (
          <span
            style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
            onClick={() => {routeToEmployeeProfile(`/employee-profile/${params.data.employee_mid}__${params.data.email_id}`);}}
          >
            {params.value}
          </span>
        );
      },
    },
    {
      headerName: "Designation",
      headerTooltip: "Designation",
      field: "designation_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Grade",
      headerTooltip: "Grade",
      field: "grade",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
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
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "BU",
      headerTooltip: "BU",
      field: "business_unit",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Competency",
      headerTooltip: "Competency",
      field: "competency",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
   
    {
      headerName: "Net Capacity (Cost)",
      headerTooltip: "Net Capacity (Cost)",
      field: "capacity_cost",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Leave Hours",
      headerTooltip: "Leave",
      field: "net_leaves",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "net_leaves",
    },
    {
      headerName: "Available Hours",
      headerTooltip: "Available Hours",
      field: "availability",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Allocation Hours",
      headerTooltip: "Allocation Hours",
      field: "allocation_hours",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Actual Hours",
      field: "actual_log_hours",
      headerTooltip: "Actual Hours",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Allocation %",
      headerTooltip: "Allocation %",
      field: "allocation_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {params?.data?.capacity_cost > 0
      //         ? (
      //             ((params?.data?.job_chargeable_cost +
      //               params?.data?.job_non_chargeable_cost) /
      //               params?.data?.capacity_cost) *
      //             100
      //           ).toFixed(2)
      //         : 0}
      //     </span>
      //   );
      // },
      tooltipField: "allocation_percent",
    },
    {
      headerName: "Allocation Job Chargeable %",
      headerTooltip: "Job Chargeable %",
      field: "job_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.allocated_chargable_hr /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Allocation Job non-chargeable %",
      headerTooltip: "Allocation Job non-chargeable %",
      field: "job_non_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.allocated_non_chargable_hr /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Actual Job Chargeable %",
      headerTooltip: "Actual Job Chargeable %",
      field: "actual_job_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.job_chargeable_hours /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Actual Job non-chargeable %",
      headerTooltip: "Actual Job non-chargeable %",
      field: "actual_job_non_chargeable_hours_pct",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.job_non_chargeable_hours /
                    params?.data?.allocation_hours) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Allocation Chargeability %",
      headerTooltip: "Allocation Chargeability %",
      field: "allocation_chargability_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "allocation_chargability_percent",
    },
    {
      headerName: "Actual Chargeability %",
      headerTooltip: "Actual Chargeability %",
      field: "actual_chargability_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      // cellRenderer: (params: any) => {
      //   return (
      //     <span>
      //       {params?.data?.capacity > 0
      //         ? (
      //             (params?.data?.job_chargeable_hours /
      //               params?.data?.capacity) *
      //             100
      //           ).toFixed(2)
      //         : 0}
      //     </span>
      //   );
      // },
      tooltipField: "actual_chargability_percent",
    },
    {
      headerName: "Actual%",
      headerTooltip: "Actual%",
      field: "actual_percentage",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      cellRenderer: (params: any) => {
        return (
          <span>
            {params?.data?.allocation_hours > 0
              ? (
                  (params?.data?.actual_cost / // DONE as PER SHEET Provided BY BA Allocation hours/ Capacity Hours *100
                    params?.data?.capacity) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Actual Cost",
      field: "actual_cost",
      headerTooltip: "Actual Cost",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Allocated Cost",
      field: "allocated_cost",
      headerTooltip: "Allocated Cost",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
  ];
  return columnDefs;
};
 
