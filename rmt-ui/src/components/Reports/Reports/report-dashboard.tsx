import React, { useContext, useEffect, useRef, useState } from "react";
import "../report.scss";
import {
  Avatar,
  Card,
  CardContent,
  Grid,
  ToggleButton,
  ToggleButtonGroup,
  Typography,
} from "@mui/material";
import ReactSpeedometer from "react-d3-speedometer";
import DonutChart from "../Charts/Donut-Chart/donut-chart";
import { LoaderContext } from "../../../contexts/loaderContext";
import { SnackbarContext } from "../../../contexts/snackbarContext";
import { errorMessage } from "../../../services/log-services/log-service";
import { getSummaryStatisticsViewReport } from "../../../services/reports/reports.service";
import { report } from "process";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import { IEmployeeReportGraph } from "../IEmployeeReportGraph";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { formateDate } from "../Charts/Bar-Chart/utils";
import {
  EReportDashboardFilterControl,
  IReportDashboardFilterControl,
} from "../Filters/uitls";
import { GetFilterDefaultValueOnTheBasisOfRole } from "../util";
import moment from "moment";
import { ChartColors, REPORT_TYPE, TOGGLE_CONSTANTS } from "../constant";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import { AgGridReact } from "ag-grid-react";
import { defaultColDef } from "../Charts/Bar-Chart/scheduled_vs_variance/constant";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import AccessTimeOutlinedIcon from "@mui/icons-material/AccessTimeOutlined";
import { RolesListMaster } from "../../../common/enums/ERoles";

interface ISummaryStatesDTO {
  chargeAbleActualCost: number;
  chargeAbleActualHrs: number;
  chargeableAllocatedCost: number;
  chargeableAllocatedHrs: number;
  jobAllocationsHrs: number;
  nonChargeableActualCost: number;
  nonChargeableActualHrs: number;
  nonChargeableAllocatedCost: number;
  nonChargeableAllocatedHrs: number;
  futureChargeableAllocatedHrs: number;
  futureChargeableAllocatedCost: number;
  futureNonChargeableAllocatedHrs: number;
  futureNonChargeableAllocatedCost: number;
  pipelineAllocationsHrs: number;
  summaryStatisticsData: [];
  totalActualCost: number;
  totalActualHrs: number;
  totalAllocatedCost: number;
  totalAllocatedHrs: number;
  totalCapacityCost: number;
  totalCapacityHrs: number;
  totalProjectCount: number;
  futureAllocationHrs: number;

  chargeableAllocatedHrsCurrent: number;
  chargeableAllocatedCostCurrent: number;
  nonChargeableAllocatedHrsCurrent: number;
  nonChargeableAllocatedCostCurrent: number;
  chargeAbleActualHrsPrevious: number;
  chargeAbleActualCostPrevious: number;
  nonChargeableActualHrsPrevious: number;
  nonChargeableActualCostPrevious: number;

  totalAllocatedHrsCurrent: number;
  totalAllocatedCostCurrent: number;
  totalCapacityHrsCurrent: number;
  totalCapacityCostCurrent: number;
  totalActualHrsPrevious: number;
  totalActualCostPrevious: number;
  totalCapacityHrsPrevious: number;
  totalCapacityCostPrevious: number;
}

//Summary Statistics chart report
//const ReportDashboard = (props: IStatesDashboardChart) => {
const ReportDashboard = (props) => {
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [reportData, setReportData] = useState<any>({
    netCapacity: 0,
    totalActual: 0,
    totalAllocated: 0,
    chargeableAllocated: 0,
    nonChargeableAllocated: 0,
    chargeableActual: 0,
    nonChargeableActual: 0,
    chargiablityAllocatedPercent: 0,
    chargeablityActualPercent: 0,
    allocationPercent: 0,
    actualPercent: 0,
    totalCapacityHrs: 0,
    totalAllocatedHrs: 0,
    totalActualHrs: 0,
    nonChargeableActualHrs: 0,
    totalCapacityCost: 0,
    totalAllocatedCost: 0,
    totalActualCost: 0,
    chargeableAllocatedCost: 0,
    nonChargeableAllocatedCost: 0,
    chargeAbleActualCost: 0,
    nonChargeableActualCost: 0,
  });

  const [segments, setSegments] = useState<Array<number>>([0, 0, 100]);
  const [actualGraphSegments, setActualGraphSegments] = useState<Array<number>>(
    [0, 0, 100]
  );
  const gridRef: any = useRef();
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const [colDef, setColDef] = useState<any[]>([]);
  const [rowData, setRowData] = useState<string[]>([]);

  const [chargeNonChargeAllocated, setChargeNonChargeAllocated] = useState([]);
  const [chargeNonChargeActual, setChargeNonChargeActual] = useState([]);
  const [totalProjects, setTotalProjects] = useState<number>(0);
  const [futureAllocationHrs, setFutureAllocationHrs] = useState<number>(0);
  const [futureChargeableAllocatedHrs, setFutureChargeableAllocatedHrs] =
    useState<number>(0);
  const [futureNonChargeableAllocatedHrs, setFutureNonChargeableAllocatedHrs] =
    useState<number>(0);
  const [netCapacityCurrent, setNetCapacityCurrent] = useState<number>(100);
  const [netCapacityPrevious, setNetCapacityPrevious] = useState<number>(100);
  const [reportGraph, setReportGraph] = useState<IEmployeeReportGraph>();
  const [isPageLoaded, setIsPageLoaded] = useState<Boolean>(false);
  const [isEmployee, setIsEmployee] = useState<Boolean>(false);
  const userDetailsContext: any = useContext(UserDetailsContext);

  const {
    control,
    setValue,
    getValues,
    trigger,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });

  const calculateSpeedoMeterDataWrapper = (_chartType, report_data) => {
    if (_chartType) {
      if (
        _chartType.toLowerCase().trim() ===
        TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
      ) {
        calculateSpeedoMeterData(
          TOGGLE_CONSTANTS.TIME.toLowerCase(),
          report_data
        );
        const timeValue = GetAgGridColumnDefForTime();
        setColDef(timeValue);
      } else {
        calculateSpeedoMeterData(
          TOGGLE_CONSTANTS.COST.toLowerCase(),
          report_data
        );
        const costValue = GetAgGridColumnDefForCost();
        setColDef(costValue);
      }
    }
  };

  var statisticsChartType = TOGGLE_CONSTANTS.TIME;
  var statisticsIsFilterApplied = false; //props.isFilterApplied;

  useEffect(() => {
    if (isPageLoaded) {
      calculateSpeedoMeterDataWrapper(statisticsChartType, reportData);
    }
  }, [props]);

  useEffect(() => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  }, [props.filterParameters]);

  useEffect(() => {
    if (!isPageLoaded) {
      props.setFilterParameters(
        GetFilterDefaultValueOnTheBasisOfRole(REPORT_TYPE.STAT_CHART)
      );
      setIsPageLoaded(true);
    }
  }, []);

  useEffect(() => {
    if (isPageLoaded) {
      const isEmployee =
        userDetailsContext?.role?.filter(
          (role) =>
            role?.toLowerCase() === RolesListMaster.Employee.toLowerCase()
        )?.length > 0;
      if (isEmployee) {
        setIsEmployee(true);
      }
      generateReportData(props.filterParameters);
    }
  }, [props.filterParameters, userDetailsContext?.role]);

  const calculateSpeedoMeterData = (toggleValue, reportData) => {
    let calculatedData: IEmployeeReportGraph = {
      netCapacityCurrent: 0,
      netCapacityPrevious: 0,
      totalActual: 0,
      totalAllocated: 0,
      chargeableAllocated: 0,
      nonChargeableAllocated: 0,
      chargeableActual: 0,
      nonChargeableActual: 0,
      chargiablityAllocatedPercent: 0,
      chargeablityActualPercent: 0,
      allocationPercent: 0,
      actualPercent: 0,
    };
    if (toggleValue.toLowerCase() == TOGGLE_CONSTANTS.TIME.toLowerCase()) {
      calculatedData.netCapacityCurrent = reportData?.totalCapacityHrsCurrent;
      calculatedData.netCapacityPrevious = reportData?.totalCapacityHrsPrevious;
      calculatedData.totalAllocated = reportData?.totalAllocatedHrsCurrent;
      calculatedData.totalActual = reportData?.totalActualHrsPrevious;

      calculatedData.chargeableAllocated =
        reportData?.chargeableAllocatedHrsCurrent;

      calculatedData.nonChargeableAllocated =
        reportData?.nonChargeableAllocatedHrsCurrent;

      calculatedData.chargeableActual = reportData?.chargeAbleActualHrsPrevious;

      calculatedData.nonChargeableActual =
        reportData?.nonChargeableActualHrsPrevious;
    } else {
      calculatedData.netCapacityCurrent = reportData?.totalCapacityCostCurrent;
      calculatedData.netCapacityPrevious =
        reportData?.totalCapacityCostPrevious; //
      calculatedData.totalAllocated = reportData?.totalAllocatedCostCurrent;
      calculatedData.totalActual = reportData?.totalActualCostPrevious;

      calculatedData.chargeableAllocated =
        reportData?.chargeableAllocatedCostCurrent;

      calculatedData.nonChargeableAllocated =
        reportData?.nonChargeableAllocatedCostCurrent;

      calculatedData.chargeableActual =
        reportData?.chargeAbleActualCostPrevious;
      calculatedData.nonChargeableActual =
        reportData?.nonChargeableActualCostPrevious;
    }

    calculatedData.allocationPercent =
      (calculatedData.chargeableAllocated / netCapacityCurrent) * 100;

    calculatedData.actualPercent =
      (calculatedData.chargeableActual / netCapacityPrevious) * 100;

    if (calculatedData.netCapacityCurrent == 0) {
      calculatedData.chargiablityAllocatedPercent = 0;
    } else {
      calculatedData.chargiablityAllocatedPercent = percent(
        calculatedData.chargeableAllocated,
        calculatedData.netCapacityCurrent
      );
    }

    if (calculatedData.netCapacityPrevious == 0) {
      calculatedData.chargeablityActualPercent = 0;
    } else {
      calculatedData.chargeablityActualPercent = percent(
        calculatedData.chargeableActual,
        calculatedData.netCapacityPrevious
      );
    }

    if (isNaN(calculatedData.chargiablityAllocatedPercent)) {
      calculatedData.chargiablityAllocatedPercent = 0;
    }
    if (isNaN(calculatedData.chargeablityActualPercent)) {
      calculatedData.chargeablityActualPercent = 0;
    }

    setReportGraph(calculatedData);

    if (calculatedData.netCapacityCurrent > 0) {
      setSegments([
        0,
        calculatedData.totalAllocated,
        maxValue(
          calculatedData.totalAllocated,
          calculatedData.netCapacityCurrent
        ),
      ]);
    } else {
      setSegments([0, 0, 100]);
    }

    if (calculatedData.netCapacityPrevious > 0) {
      setActualGraphSegments([
        0,
        calculatedData.totalActual,
        maxValue(
          calculatedData.totalActual,
          calculatedData.netCapacityPrevious
        ),
      ]);
    } else {
      setActualGraphSegments([0, 0, 100]);
    }
    const actualGraph = [
      {
        name: "Chargeable",
        value: calculatedData.chargeableActual,
        fill: ChartColors.ActualChargable,
      },
      {
        name: "NonChargeable",
        value: calculatedData.nonChargeableActual,
        fill: ChartColors.ActualNonChargable,
      },
    ];
    const allocatedGraph = [
      {
        name: "Chargeable",
        value: calculatedData.chargeableAllocated,
        fill: ChartColors.AllocChargable,
      },
      {
        name: "NonChargeable",
        value: calculatedData.nonChargeableAllocated,
        fill: ChartColors.AllocNonChargable,
      },
    ];
    setChargeNonChargeActual(actualGraph);
    setChargeNonChargeAllocated(allocatedGraph);
  };

  const generateReportData = async (filterData) => {
    filterData["emailId"] = userDetailsContext.username;
    const response = await fetchReportData(filterData);
    if (response) {
      setReportData(response);
      setTotalProjects(response?.totalProject);
      setFutureAllocationHrs(response?.futureAllocationHrs); //future allocation
      setFutureChargeableAllocatedHrs(response?.futureChargeableAllocatedHrs); //future chargeable allocation
      setFutureNonChargeableAllocatedHrs(
        response?.futureNonChargeableAllocatedHrs
      ); //future nonchargeable allocation
      setNetCapacityCurrent(response?.totalCapacityHrsCurrent);
      setNetCapacityPrevious(response?.totalCapacityHrsPrevious);

      calculateSpeedoMeterDataWrapper(statisticsChartType, response);
    }
  };

  const fetchReportData = (fltData): Promise<any> => {
    loaderContext.open(true);
    const serviceData = { ...fltData };
    serviceData["startDate"] = formateDate(serviceData["startDate"]);
    serviceData["endDate"] = formateDate(serviceData["endDate"]);

    return new Promise((resolve, reject) => {
      getSummaryStatisticsViewReport(serviceData)
        .then((_data: any) => {
          if (
            statisticsIsFilterApplied &&
            _data.data.length > process.env.REACT_APP_CHART_DATA_LIMIT
          ) {
            loaderContext.open(false);
            props.setisFilterApplied(false);
            snackbarContext.displaySnackbar(
              "Please apply additional filters as the result includes records greater then 10K",
              "error"
            );
            setRowData([]);
            return;
          } else {
            loaderContext.open(false);

            var responseData: ISummaryStatesDTO =
              _data.data as ISummaryStatesDTO;
            var aggridData = responseData.summaryStatisticsData;
            setRowData(aggridData);
            const dataService = {
              totalProject: responseData.totalProjectCount,
              futureAllocationHrs: responseData.futureAllocationHrs,
              jobAllocations: responseData.jobAllocationsHrs,
              pipelineAllocations: responseData.pipelineAllocationsHrs,

              chargeableAllocatedHrsCurrent:
                responseData.chargeableAllocatedHrsCurrent,
              chargeableAllocatedCostCurrent:
                responseData.chargeableAllocatedCostCurrent,

              nonChargeableAllocatedHrsCurrent:
                responseData.nonChargeableAllocatedHrsCurrent,
              nonChargeableAllocatedCostCurrent:
                responseData.nonChargeableAllocatedCostCurrent,

              chargeAbleActualHrsPrevious:
                responseData.chargeAbleActualHrsPrevious,
              chargeAbleActualCostPrevious:
                responseData.chargeAbleActualCostPrevious,

              nonChargeableActualHrsPrevious:
                responseData.nonChargeableActualHrsPrevious,
              nonChargeableActualCostPrevious:
                responseData.nonChargeableActualCostPrevious,

              totalAllocatedHrsCurrent: responseData.totalAllocatedHrsCurrent,
              totalAllocatedCostCurrent: responseData.totalAllocatedCostCurrent,
              totalCapacityHrsCurrent: responseData.totalCapacityHrsCurrent,
              totalCapacityCostCurrent: responseData.totalCapacityCostCurrent,
              totalActualHrsPrevious: responseData.totalActualHrsPrevious,
              totalActualCostPrevious: responseData.totalActualCostPrevious,
              totalCapacityHrsPrevious: responseData.totalCapacityHrsPrevious,
              totalCapacityCostPrevious: responseData.totalCapacityCostPrevious,

              totalCapacityHrs: responseData.totalCapacityHrs,
              totalAllocatedHrs: responseData.totalAllocatedHrs,
              chargeableAllocatedHrs: responseData.chargeableAllocatedHrs,
              nonChargeableAllocatedHrs: responseData.nonChargeableAllocatedHrs,
              totalActualHrs: responseData.totalActualHrs,
              chargeAbleActualHrs: responseData.chargeAbleActualHrs,
              nonChargeableActualHrs: responseData.nonChargeableActualHrs,
              totalCapacityCost: responseData.totalCapacityCost,
              totalAllocatedCost: responseData.totalAllocatedCost,
              chargeableAllocatedCost: responseData.chargeableAllocatedCost,
              nonChargeableAllocatedCost:
                responseData.nonChargeableAllocatedCost,
              futureChargeableAllocatedHrs:
                responseData.futureChargeableAllocatedHrs,
              futureChargeableAllocatedCost:
                responseData.futureChargeableAllocatedCost,
              futureNonChargeableAllocatedHrs:
                responseData.futureNonChargeableAllocatedHrs,
              futureNonChargeableAllocatedCost:
                responseData.futureNonChargeableAllocatedCost,
              totalActualCost: responseData.totalActualCost,
              chargeAbleActualCost: responseData.chargeAbleActualCost,
              nonChargeableActualCost: responseData.nonChargeableActualCost,
            };
            resolve(dataService);
          }
        })
        .catch((ex) => {
          console.log(ex);
          snackbarContext?.displaySnackbar(
            "Error fetching data! Please re-try after some time. Please contact System Administrator if issue persists.",
            "error"
          );
          errorMessage("Report", "fetchReportData", ex);
          loaderContext.open(false);
        });
    });
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

  const maxValue = (value, capacity) => {
    if (capacity > value) {
      return capacity;
    }
    return value;
  };

  const percent = (value, total) => {
    if (total > 0) {
      const percentage = (value / total) * 100;
      return Math.round(percentage * 10) / 10;
    }
    return 0;
  };
  return (
    <>
      {isEmployee && !loaderContext.isOpen ? (
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
          </Grid>
          <div className="report-dashboard-header"></div>
          <div style={{ display: "none" }}>
            <div style={{ backgroundColor: ChartColors.Capacity }}>
              Capacity
            </div>
            <div style={{ backgroundColor: ChartColors.Actual }}>Actual</div>
            <div style={{ backgroundColor: ChartColors.Allocation }}>
              Allocation
            </div>
            <div style={{ backgroundColor: ChartColors.Availability }}>
              Availability
            </div>
            <div style={{ backgroundColor: ChartColors.ActualChargable }}>
              Actual-Charg
            </div>
            <div style={{ backgroundColor: ChartColors.ActualNonChargable }}>
              Actual-NonCharg
            </div>
            <div style={{ backgroundColor: ChartColors.AllocChargable }}>
              Aloc-Charg
            </div>
            <div style={{ backgroundColor: ChartColors.AllocNonChargable }}>
              Aloc-NonCharg
            </div>
          </div>
          <div className="report-dashboard-container">
            <Card>
              <CardContent>
                <div className="report-tabs">
                  <div>
                    <div className="budget-card report-card">
                      {" "}
                      <Card sx={{ minWidth: 275 }}>
                        <CardContent className="report-card-content">
                          <span>
                            <Typography
                              sx={{ mb: 0, fontSize: 16 }}
                              color="text.secondary"
                            >
                              Total Project
                            </Typography>
                            <Typography
                              sx={{ fontSize: 22 }}
                              color="text.primary"
                            >
                              {totalProjects}
                            </Typography>
                          </span>
                          <Avatar>
                            <PersonOutlineOutlinedIcon />
                          </Avatar>
                        </CardContent>
                      </Card>{" "}
                    </div>
                  </div>
                  <div className="budget-card report-card">
                    {" "}
                    <Card sx={{ minWidth: 275 }}>
                      <CardContent className="report-card-content">
                        <span>
                          <Typography
                            sx={{ mb: 0, fontSize: 16 }}
                            color="text.secondary"
                          >
                            Allocation
                          </Typography>
                          <Typography
                            sx={{ fontSize: 22 }}
                            color="text.primary"
                          >
                            {futureAllocationHrs} hours
                          </Typography>
                        </span>
                        <Avatar className="allocation-card-icons-v1">
                          <AccessTimeOutlinedIcon />
                        </Avatar>
                      </CardContent>
                    </Card>{" "}
                  </div>

                  <div className="budget-card report-card">
                    {" "}
                    <Card sx={{ minWidth: 275 }}>
                      <CardContent className="report-card-content">
                        <span>
                          <Typography
                            sx={{ mb: 0, fontSize: 16 }}
                            color="text.secondary"
                          >
                            Chargeable Allocation
                          </Typography>
                          <Typography
                            sx={{ fontSize: 22 }}
                            color="text.primary"
                          >
                            {/* {jobAllocations} */}
                            {futureChargeableAllocatedHrs} hours
                          </Typography>
                        </span>
                        <Avatar className="chargable-allocation-icons-v1">
                          <PersonOutlineOutlinedIcon />
                        </Avatar>
                      </CardContent>
                    </Card>{" "}
                  </div>

                  <div className="budget-card report-card">
                    <Card sx={{ minWidth: 275 }}>
                      <CardContent className="report-card-content">
                        <span>
                          <Typography
                            sx={{ mb: 0, fontSize: 16 }}
                            color="text.secondary"
                          >
                            NC Allocation
                          </Typography>
                          <Typography
                            sx={{ fontSize: 22 }}
                            color="text.primary"
                          >
                            {futureNonChargeableAllocatedHrs} hours
                          </Typography>
                        </span>
                        <Avatar className="non-chargable-allocation-icons-v1">
                          <PersonOutlineOutlinedIcon />
                        </Avatar>
                      </CardContent>
                    </Card>
                  </div>
                </div>
                <div className="report-graph-container">
                  <div className="report-graphs">
                    <div className="report-card-gauge">
                      <div className="report-heading">
                        <Typography
                          sx={{ mb: 0, fontSize: 16, fontWeight: 600 }}
                          color="text.secondary"
                        >
                          Allocation vs Capacity (hours)
                        </Typography>
                      </div>
                      <div className="speedometer-style-main">
                        {reportGraph && (
                          <ReactSpeedometer
                            value={reportGraph?.totalAllocated}
                            segments={2}
                            height={350}
                            width={304}
                            paddingVertical={1}
                            maxValue={
                              reportGraph?.netCapacityCurrent
                                ? maxValue(
                                    reportGraph?.totalAllocated,
                                    reportGraph?.netCapacityCurrent
                                  )
                                : 100
                            }
                            forceRender={true}
                            customSegmentStops={segments}
                            segmentColors={[
                              ChartColors.Allocation,
                              ChartColors.Capacity,
                              ChartColors.Red,
                            ]}
                            // segmentColors={["#0088FE" blue , "# gray", "#EA4228" red]}
                            currentValueText={
                              percent(
                                reportGraph?.totalAllocated,
                                reportGraph?.netCapacityCurrent
                              ) + "%"
                            }

                            // startColor will be ignored
                            // endColor will be ignored
                          />
                        )}
                      </div>
                    </div>
                    <div className="report-card-gauge">
                      <div className="report-heading">
                        <Typography
                          sx={{ mb: 0, fontSize: 16, fontWeight: 600 }}
                          color="text.secondary"
                        >
                          Allocation Hours
                        </Typography>
                      </div>
                      <DonutChart
                        data={chargeNonChargeAllocated}
                        width={304}
                        height={300}
                        labelDisplayText={"Chargeability"}
                        label={reportGraph?.chargiablityAllocatedPercent + "%"}
                      />
                    </div>
                  </div>
                  <div className="report-graphs">
                    <div className="report-card-gauge">
                      <div className="report-heading">
                        <Typography
                          sx={{ mb: 0, fontSize: 16, fontWeight: 600 }}
                          color="text.secondary"
                        >
                          Actuals vs Capacity (hours)
                        </Typography>
                      </div>
                      <div className="speedometer-style-main">
                        {reportGraph && (
                          <ReactSpeedometer
                            value={reportGraph?.totalActual}
                            segments={2}
                            height={350}
                            width={304}
                            paddingVertical={10}
                            maxValue={
                              reportGraph?.netCapacityPrevious
                                ? maxValue(
                                    reportGraph?.totalActual,
                                    reportGraph?.netCapacityPrevious
                                  )
                                : 100
                            }
                            currentValueText={
                              percent(
                                reportGraph?.totalActual,
                                reportGraph?.netCapacityPrevious
                              ) + "%"
                            }
                            forceRender={true}
                            customSegmentStops={actualGraphSegments}
                            segmentColors={[
                              ChartColors.Actual,
                              ChartColors.Capacity,
                              ChartColors.Red,
                            ]}
                            // segmentColors={["#5BE12C" gree, "#D3D3D3" gray, "#EA4228" red]}
                            // startColor will be ignored
                            // endColor will be ignored
                          />
                        )}
                      </div>
                    </div>
                    <div className="report-card-gauge">
                      <div className="report-heading">
                        <Typography
                          sx={{ mb: 0, fontSize: 16, fontWeight: 600 }}
                          color="text.secondary"
                        >
                          Actual Hours
                        </Typography>
                      </div>
                      {reportGraph && (
                        <DonutChart
                          data={chargeNonChargeActual}
                          width={304}
                          height={300}
                          label={reportGraph?.chargeablityActualPercent + "%"}
                        />
                      )}
                    </div>
                  </div>
                </div>
              </CardContent>
            </Card>
          </div>
          {/* AGGrid component all Columns needs to be checked with backend  */}
          {false && (
            <>
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
                  rowSelection={"multiple"}
                  rowMultiSelectWithClick={true}
                  suppressRowClickSelection={true}
                  height={"61.2vh"}
                  exportedFileName={"StatisticsReport"}
                ></AgGridComponent>
              </div>
            </>
          )}
        </div>
      ) : (
        <div className="center">
          {!loaderContext.isOpen && (
            <> You do not have permission to view this page!</>
          )}
        </div>
      )}
    </>
  );
};

export default ReportDashboard;
//Summary Statistics Chart Report

export const GetAgGridColumnDefForTime = () => {
  const columnDefs: any = [
    {
      headerName: "Date",
      field: "date",
      suppressMenu: true,
      tooltipField: "action",
    },
    {
      headerName: "Employee",
      headerTooltip: "Employee",
      field: "email_id",
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
      headerName: "Business Unit",
      headerTooltip: "Business Unit",
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
      headerName: "Capacity",
      headerTooltip: "Capacity",
      field: "capacity",
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
      headerName: "Job Chargeable Hours %",
      headerTooltip: "Job Chargeable Hours %",
      field: "job_chargeable_hours_pct",
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
            {params?.data?.capacity > 0
              ? (
                  (params?.data?.job_chargeable_hours /
                    params?.data?.capacity) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "job_chargeable_hours_pct",
    },
    {
      headerName: "Job Non Chargeable Hours %",
      headerTooltip: "Job Non Chargeable Hours %",
      field: "job_non_chargeable_hours_pct",
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
            {params?.data?.capacity > 0
              ? (
                  (params?.data?.job_non_chargeable_hours /
                    params?.data?.capacity) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "job_non_chargeable_hours_pct",
    },
    {
      headerName: "Allocation %",
      headerTooltip: "Allocation %",
      field: "allocation_pct",
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
            {params?.data?.capacity > 0
              ? (
                  ((params?.data?.job_chargeable_hours +
                    params?.data?.job_non_chargeable_hours) /
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
      headerName: "Chargeability",
      headerTooltip: "Chargeability",
      field: "chargeability",
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
            {params?.data?.capacity > 0
              ? (
                  (params?.data?.job_chargeable_hours /
                    params?.data?.capacity) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
  ];
  return columnDefs;
};

export const GetAgGridColumnDefForCost = () => {
  const columnDefs: any = [
    {
      headerName: "Date",
      field: "date",
      suppressMenu: true,
      tooltipField: "action",
    },
    {
      headerName: "Employee Email",
      headerTooltip: "Employee Email",
      field: "email_id",
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
      headerName: "Business Unit",
      headerTooltip: "Business Unit",
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
    // Actual Cost
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
    // Allocated Cost
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
    {
      headerName: "Capacity Cost",
      headerTooltip: "Capacity Cost",
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
      headerName: "Job Chargeable Cost %",
      headerTooltip: "Job Chargeable Cost %",
      field: "job_chargeable_cost_pct",
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
            {params?.data?.capacity_cost > 0
              ? (
                  (params?.data?.job_chargeable_cost /
                    params?.data?.capacity_cost) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "job_chargeable_cost_pct",
    },
    {
      headerName: "Job Non Chargeable Cost %",
      headerTooltip: "Job Non Chargeable Cost %",
      field: "job_non_chargeable_cost_pct",
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
            {params?.data?.capacity_cost > 0
              ? (
                  (params?.data?.job_non_chargeable_cost /
                    params?.data?.capacity_cost) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "job_non_chargeable_cost_pct",
    },
    {
      headerName: "Allocation %",
      headerTooltip: "Allocation %",
      field: "allocation_pct",
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
            {params?.data?.capacity_cost > 0
              ? (
                  ((params?.data?.job_chargeable_cost +
                    params?.data?.job_non_chargeable_cost) /
                    params?.data?.capacity_cost) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
    {
      headerName: "Chargeability",
      headerTooltip: "Chargeability",
      field: "chargeability",
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
            {params?.data?.capacity_cost > 0
              ? (
                  (params?.data?.job_chargeable_cost /
                    params?.data?.capacity_cost) *
                  100
                ).toFixed(2)
              : 0}
          </span>
        );
      },
      tooltipField: "",
    },
  ];
  return columnDefs;
};
