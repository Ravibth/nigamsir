import { useEffect, useState, useContext } from "react";
import { getCapicityUtilizationOverviewChart } from "../../../../services/reports-and-dashboard/capacity-utilization-services";
import {
  ICapacityUtilizationChart,
  ICapacityUtilizationColumnBarChart,
} from "./interface";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Label,
  Legend,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import _ from "lodash";
import {
  EReportDashboardFilterControl,
  EXAxisGroupKey,
  IReportDashboardFilterControl,
} from "../../Filters/uitls";
import moment from "moment";
import { CustomTooltip } from "./custom-tooltip";
import { GetCapacityPercentage, GetChargablityPercentage } from "./util";
import { ChartColors, TOGGLE_CONSTANTS } from "../../constant";
import ColumnChartCard from "./Cards/column-chart-card";
import "./column-chart.scss";
import { LoaderContext } from "../../../../contexts/loaderContext";
import { SnackbarContext } from "../../../../contexts/snackbarContext";
import { IndianNumberFormatter } from "../../../../utils/date/dateHelper";
import { useForm } from "react-hook-form";
import { Grid } from "@mui/material";
import ControllerCalendar from "../../../controllerInputs/controlerCalendar";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../../../contexts/userDetailsContext";
import { RolesListMaster } from "../../../../common/enums/ERoles";
import { routeToEmployeeProfile } from "../../../../global/utils";

const calculateChartWidth = (dataLength) => {
  const barWidth = 60;
  const spacing = 20;
  const finalWidth =
    dataLength > 10 ? dataLength * (barWidth + spacing) + "px" : "100%";
  // return "100%";
  return finalWidth;
};

//Capacity Utilization chart report
const ColumnChart = (props) => {
  const {
    control,
    setValue,
    getValues,
    trigger,
    formState: { errors, isDirty },
  } = useForm({
    mode: "onTouched",
  });

  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);

  const chart_data_color = {
    availability: ChartColors.Availability,
    allocated: ChartColors.Allocation,
    capacity: ChartColors.Capacity,
  };

  useEffect(() => {
    Object.keys(props.filterParameters).forEach((key: any) => {
      setValue(key, props.filterParameters[key]);
    });
  }, [props.filterParameters]);

  const [capacityUtilizationData, setCapacityUtilizationData] = useState<
    ICapacityUtilizationChart[]
  >([]);
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);

  const [columnBarChart, setColumnBarChart] = useState<
    ICapacityUtilizationColumnBarChart[]
  >([]);
  const [columnChartCardData, setColumnChartCardData] = useState<any>();

  let buLeaderData = Object.values(
    props.userLeaderRoles
      ? props.userLeaderRoles["bu"]
        ? props.userLeaderRoles["bu"]
        : {}
      : {}
  );
 
  const loadDataFromServices = (
    startDate: string,
    endDate: string,
    business_unit: any[],
    // expertise: any[],
    // smeg: any[],
    location: string[],
    designation: string[],
    competency: string[]
    // offering: string[],
    // solution: string[]
  ) => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      getCapicityUtilizationOverviewChart(
        startDate,
        endDate,
        business_unit,
        // expertise,
        // smeg,
        location,
        designation,
        competency
        // offering,
        // solution
      )
        .then((response) => {
          if (
            props.isFilterApplied &&
            response.data.length > process.env.REACT_APP_CHART_DATA_LIMIT
          ) {
            props.setisFilterApplied(false);
            resolve([]);
            loaderContext.open(false);
            snackbarContext.displaySnackbar(
              "Please apply additional filters as the result includes records greater then 10K",
              "error"
            );
          } else {
            resolve(response.data);
            loaderContext.open(false);
          }
        })
        .catch((err) => {
          reject(err);
          loaderContext.open(false);
        });
    });
  };
  useEffect(() => {
    props.setIsEmployeeViewGraph(false);
  }, []);

  //Scheduled variance chart
  const transformData = (
    capacityUtilizationData: ICapacityUtilizationChart[],
    groupKey: string
  ) => {
    loaderContext.open(true);
    let finalTransformData: ICapacityUtilizationColumnBarChart[] = [];

    const groupedData = _.groupBy(capacityUtilizationData, groupKey);
    const summedData = _.mapValues(groupedData, (group) => {
      return {
        capacity: _.sumBy(group, "capacity"),
        availability: _.sumBy(group, "availability"),
        allocation_hours: _.sumBy(group, "allocation_hours"),
        allocated_chargable_hr: _.sumBy(group, "allocated_chargable_hr"),
        allocated_chargable_cost: _.sumBy(group, "allocated_chargable_cost"),
        allocated_non_chargable_hr: _.sumBy(
          group,
          "allocated_non_chargable_hr"
        ),
        allocated_non_chargable_cost: _.sumBy(
          group,
          "allocated_non_chargable_cost"
        ),
        actual_log_hours: _.sumBy(group, "actual_log_hours"),
        job_chargeable_cost: _.sumBy(group, "job_chargeable_cost"),
        job_non_chargeable_cost: _.sumBy(group, "job_non_chargeable_cost"),
        job_chargeable_hours: _.sumBy(group, "job_chargeable_hours"),
        job_non_chargeable_hours: _.sumBy(group, "job_non_chargeable_hours"),
        allocated_cost: _.sumBy(group, "allocated_cost"),
        actual_cost: _.sumBy(group, "actual_cost"),
        capacity_cost: _.sumBy(group, "capacity_cost"),
        availability_cost: _.sumBy(group, "availability_cost"),
        allocation_percent: _.sumBy(group, "allocation_percent"),
        allocation_chargability_percent: _.sumBy(
          group,
          "allocation_chargability_percent"
        ),
        actual_chargability_percent: _.sumBy(
          group,
          "actual_chargability_percent"
        ),
      };
    });
    Object.keys(summedData).forEach((data) => {
      if (props.toggleValue === TOGGLE_CONSTANTS.COST) {
        finalTransformData.push({
          name: data,
          allocation: summedData[data].allocation_hours,
          allocated_chargable_hr: summedData[data].allocated_chargable_hr,
          allocated_chargable_cost: summedData[data].allocated_chargable_cost,
          allocated_non_chargable_hr:
            summedData[data].allocated_non_chargable_hr,
          allocated_non_chargable_cost:
            summedData[data].allocated_non_chargable_cost,

          availability: summedData[data].availability,
          actual_log_hours: summedData[data].actual_log_hours,
          capacity: summedData[data].capacity,
          chargability_percentage: GetChargablityPercentage(
            summedData[data].capacity_cost,
            summedData[data].job_chargeable_cost
          ),
          job_chargeable_cost: summedData[data].job_chargeable_cost,
          job_non_chargeable_cost: summedData[data].job_non_chargeable_cost,
          job_chargeable_hours: summedData[data].job_chargeable_hours,
          job_non_chargeable_hours: summedData[data].job_non_chargeable_hours,
          capacity_percentage: GetCapacityPercentage(
            summedData[data].capacity_cost,
            summedData[data].job_chargeable_cost,
            summedData[data].job_non_chargeable_cost
          ),
          allocated_cost: summedData[data].allocated_cost,
          actual_cost: summedData[data].actual_cost,
          capacity_cost: summedData[data].capacity_cost,
          availability_cost: summedData[data].availability_cost,
          allocation_percent: summedData[data].allocation_percent,
          allocation_chargability_percent:
            summedData[data].allocation_chargability_percent,
          actual_chargability_percent:
            summedData[data].actual_chargability_percent,
        } as ICapacityUtilizationColumnBarChart);
      } else {
        finalTransformData.push({
          name: data,
          allocation: summedData[data].allocation_hours,
          allocated_chargable_hr: summedData[data].allocated_chargable_hr,
          allocated_chargable_cost: summedData[data].allocated_chargable_cost,
          allocated_non_chargable_hr:
            summedData[data].allocated_non_chargable_hr,
          allocated_non_chargable_cost:
            summedData[data].allocated_non_chargable_cost,

          availability: summedData[data].availability,
          actual_log_hours: summedData[data].actual_log_hours,
          capacity: summedData[data].capacity,
          chargability_percentage: GetChargablityPercentage(
            summedData[data].capacity,
            summedData[data].job_chargeable_hours
          ),
          job_chargeable_hours: summedData[data].job_chargeable_hours,
          job_non_chargeable_hours: summedData[data].job_non_chargeable_hours,
          job_chargeable_cost: summedData[data].job_chargeable_cost,
          job_non_chargeable_cost: summedData[data].job_non_chargeable_cost,
          capacity_percentage: GetCapacityPercentage(
            summedData[data].capacity,
            summedData[data].job_chargeable_hours,
            summedData[data].job_non_chargeable_hours
          ),
          allocated_cost: summedData[data].allocated_cost, //allocation
          actual_cost: summedData[data].actual_cost, //actual -- > show not
          capacity_cost: summedData[data].capacity_cost, //
          availability_cost: summedData[data].availability_cost, //
          allocation_percent: summedData[data].allocation_percent,
          allocation_chargability_percent:
            summedData[data].allocation_chargability_percent,
          actual_chargability_percent:
            summedData[data].actual_chargability_percent,
        } as ICapacityUtilizationColumnBarChart);
      }
    });
    loaderContext.open(false);
    return finalTransformData;
  };
  const determineXAxisTypeNew = () => {
    const isCEOCOO = userDetailsContext.role.includes(RolesListMaster.CEOCOO);
    const isSystemAdmin = userDetailsContext.role.includes(
      RolesListMaster.SystemAdmin
    );
    const isBuLeader = Object.keys(props.userLeaderRoles["bu"]).length > 0;
    const isCompetencyLeader = props?.competencyLeaderRoles?.length > 0;
    if ((isBuLeader && isCompetencyLeader) || isCEOCOO || isSystemAdmin) {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    } else if (isBuLeader && !isCompetencyLeader) {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    } else if (!isBuLeader && isCompetencyLeader) {
      return { value: EXAxisGroupKey.competency, label: "Competency" };
    } else {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    }
  };
  const determineXAxisType = (
    filterParameters: IReportDashboardFilterControl
  ) => {
    if (
      filterParameters &&
      filterParameters[EReportDashboardFilterControl.competency] &&
      filterParameters[EReportDashboardFilterControl.competency].length > 0
    ) {
      return { value: EXAxisGroupKey.competency, label: "Competency" };
    } else if (
      filterParameters &&
      filterParameters[EReportDashboardFilterControl.smeg] &&
      filterParameters[EReportDashboardFilterControl.smeg].length > 0
    ) {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    } else if (
      filterParameters &&
      filterParameters[EReportDashboardFilterControl.expertise] &&
      filterParameters[EReportDashboardFilterControl.expertise].length > 0
    ) {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    } else if (
      filterParameters &&
      filterParameters[EReportDashboardFilterControl.businessUnit] &&
      filterParameters[EReportDashboardFilterControl.businessUnit].length > 0
    ) {
      return { value: EXAxisGroupKey.competency, label: "Competency" };
    } else {
      return { value: EXAxisGroupKey.businessUnit, label: "BU" };
    }
  };
  useEffect(() => {
    if (props.toggleValue) {
      props.toggleValue === TOGGLE_CONSTANTS.TIME
        ? props.setColDef(GetAgGridColumnDefForTime())
        : props.setColDef(GetAgGridColumnDefForCost());
      props.setRowData(capacityUtilizationData);
      props.toggleValue === TOGGLE_CONSTANTS.TIME
        ? props.setColDef(GetAgGridColumnDefForTime())
        : props.setColDef(GetAgGridColumnDefForCost());
      props.setRowTabularData(capacityUtilizationData);
    }
  }, [props.toggleValue]);

  useEffect(() => {
    // Done For Leader roles
    let filterDataByRole = [];
    if (props.filterParameters) {
      const start_date: string = moment(
        props.filterParameters[EReportDashboardFilterControl.start_date]
      ).format("YYYY-MM-DD");
      const end_date: string = moment(
        props.filterParameters[EReportDashboardFilterControl.end_date]
      ).format("YYYY-MM-DD");
      loadDataFromServices(
        start_date,
        end_date,
        props.filterParameters[EReportDashboardFilterControl.businessUnit],
        props.filterParameters[EReportDashboardFilterControl.location],
        props.filterParameters[EReportDashboardFilterControl.designation],
        props.filterParameters[EReportDashboardFilterControl.competency]
      )
        .then((response) => {
          let capacityUtilizationData: ICapacityUtilizationChart[] =
            response as ICapacityUtilizationChart[];
          if (
            userDetailsContext.role.find(
              (m) =>
                m.toLowerCase().trim() ===
                  RolesListMaster.CEOCOO.toLowerCase().trim() ||
                m.toLowerCase().trim() ===
                  RolesListMaster.SystemAdmin.toLowerCase().trim()
            )
          ) {
            filterDataByRole = capacityUtilizationData;
          } else {
            if (buLeaderData && buLeaderData.length > 0) {
              const bufilterData = capacityUtilizationData.filter((item) =>
                buLeaderData.includes(item.business_unit)
              );
              filterDataByRole = [...bufilterData];
            } else if (
              props.competencyLeaderRoles &&
              props.competencyLeaderRoles.length > 0
            ) {
              const compfilterData = capacityUtilizationData.filter((item) =>
                props.competencyLeaderRoles
                  .map((a) => a.competency)
                  .includes(item.competency)
              );
              filterDataByRole = [...compfilterData];
            }

          }
          setCapacityUtilizationData(filterDataByRole);
          let XAxisType = "";
          if (props.isFilterApplied) {
            XAxisType = determineXAxisType(props.filterParameters).value;
          } else {
            XAxisType = determineXAxisTypeNew().value;
          }
          // const XAxisType = determineXAxisType(props.filterParameters).value;
          const transformedData = transformData(filterDataByRole, XAxisType);
          props.toggleValue === TOGGLE_CONSTANTS.TIME
            ? props.setColDef(GetAgGridColumnDefForTime())
            : props.setColDef(GetAgGridColumnDefForCost());
          props.setRowData(capacityUtilizationData);
          props.toggleValue === TOGGLE_CONSTANTS.TIME
            ? props.setColDef(GetAgGridColumnDefForTime())
            : props.setColDef(GetAgGridColumnDefForCost());
          props.setRowTabularData(capacityUtilizationData);
          setColumnChartCardData({
            TOTAL_CAPACITY: _.sumBy(transformedData, "capacity"),
            TOTAL_ALLOCATION: _.sumBy(transformedData, "allocation"),
            TOTAL_ACTUAL_HOURS: _.sumBy(transformedData, "actual_log_hours"),
            TOTAL_AVAILABILITY: _.sumBy(transformedData, "availability"),
            TOTAL_ALLOCATED_COST: _.sumBy(transformedData, "allocated_cost"),
            TOTAL_ACTUAL_COST: _.sumBy(transformedData, "actual_cost"),
            TOTAL_AVAILABLE_COST: _.sumBy(transformedData, "availability_cost"),
            TOTAL_CAPACITY_COST: _.sumBy(transformedData, "capacity_cost"),
          });
          // TOTAL_AVAILABLE_COST: _.sumBy(transformedData, "availability_cost"),

          setColumnBarChart(transformedData);
        })
        .catch((error) => {});
    }
  }, [props.filterParameters]);

  const getBarLabel = () => {
    let XAxisType;
    if (props.isFilterApplied) {
      XAxisType = determineXAxisType(props.filterParameters).label;
    } else {
      XAxisType = determineXAxisTypeNew().label;
    }
    return XAxisType;
  };
  const getBarLabelValues = () => {
    let XAxisType;
    if (props.isFilterApplied) {
      XAxisType = determineXAxisType(props.filterParameters).value;
    } else {
      XAxisType = determineXAxisTypeNew().value;
    }
    return XAxisType;
  };
  const handleBarClick = (
    event: any,
    dataKeySelected: string,
    XAxisLabelValue: string,
    index: any
  ) => {
    const { name } = event;
    props.toggleValue === TOGGLE_CONSTANTS.TIME
      ? props.setColDef(GetAgGridColumnDefForTime())
      : props.setColDef(GetAgGridColumnDefForCost());
    const filterData = capacityUtilizationData.filter(
      (data) => data[XAxisLabelValue] === name
    );
    props.setRowData(filterData);

    props.setSelectedChartData({
      chartName: props.chartTitle,
      rowName: name,
    });
    props.setIsOpen(true);
  };
  const legendFormater = (value: any) => {
    value = value.replaceAll("_", " ");
    return (
      <span className="legend-text-color">
        {value.charAt(0).toUpperCase() + value.slice(1)}
      </span>
    );
  };
  const CustomYAxisTick = (props) => {
    const { x, y, payload } = props;
    return (
      <g transform={`translate(${x},${y})`}>
        <text
          x={-55}
          y={0}
          dy={10}
          textAnchor="middle"
          fill="#666"
          transform="rotate(0)"
        >
          {payload.value ? IndianNumberFormatter(payload.value) : ""}
        </text>
      </g>
    );
  };
  const CustomXAxisTick = (props) => {
    const { x, y, payload } = props;
    return (
      <g transform={`translate(${x},${y})`}>
        <text
          x={-25}
          y={0}
          dy={10}
          textAnchor="middle"
          fill="#666"
          transform="rotate(0)"
        >
          {payload.value.toLocaleString()} {/* format value with commas */}
        </text>
      </g>
    );
  };
  const chartWidth = calculateChartWidth(
    columnBarChart ? columnBarChart.length : "100%"
  );

  return (
    <>
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
            minDate={moment(getValues(EReportDashboardFilterControl.end_date))
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
            maxDate={moment(getValues(EReportDashboardFilterControl.start_date))
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
      <div className="question-col-chart">
        <Grid container spacing={2}>
          <Grid item xs={3} />
          <Grid item xs={9}>
            <ColumnChartCard
              TOTAL_CAPACITY={
                props.toggleValue === TOGGLE_CONSTANTS.TIME
                  ? columnChartCardData?.TOTAL_CAPACITY
                  : columnChartCardData?.TOTAL_CAPACITY_COST
              }
              TOTAL_ALLOCATION={
                props.toggleValue === TOGGLE_CONSTANTS.TIME
                  ? columnChartCardData?.TOTAL_ALLOCATION
                  : columnChartCardData?.TOTAL_ALLOCATED_COST
              }
              TOTAL_AVAILABILITY={
                props.toggleValue === TOGGLE_CONSTANTS.TIME
                  ? columnChartCardData?.TOTAL_AVAILABILITY
                  : columnChartCardData?.TOTAL_AVAILABLE_COST
              }
              TOTAL_ACTUAL={
                props.toggleValue === TOGGLE_CONSTANTS.TIME
                  ? columnChartCardData?.TOTAL_ACTUAL_HOURS
                  : columnChartCardData?.TOTAL_ACTUAL_COST
              }
            />
            <Grid item xs={12}>
              <div className="question-container-col-chart">
                <div
                  style={{
                    height: "530px",
                    overflowY: "hidden",
                    overflowX: "auto",
                    whiteSpace: "nowrap",
                  }}
                >
                  <div
                    style={{
                      display: "inline-block",
                      minWidth: `${chartWidth}`,
                    }}
                  >
                    <ResponsiveContainer width="100%" height={500}>
                      <BarChart
                        // width={500}
                        // height={500}
                        data={columnBarChart}
                        margin={{
                          top: 15,
                          right: 35,
                          left: 55,
                          bottom: 5,
                        }}
                      >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="name" tick={<CustomXAxisTick />}>
                          <Label
                            style={{
                              textAnchor: "middle",
                              fontSize: "80%",
                              fill: "black",
                            }}
                            value={getBarLabel()}
                            position={"center"}
                            // padding="no-gap"
                            // dx={100}
                            dy={16}
                          />
                        </XAxis>

                        <YAxis width={150} tick={<CustomYAxisTick />}>
                          <Label
                            style={{
                              textAnchor: "middle",
                              fontSize: "100%",
                              fill: "black",
                              padding: "100px",
                            }}
                            value={props.toggleValue}
                            angle={-90}
                            position={"left"}
                          />
                        </YAxis>
                        <Tooltip
                          content={
                            <CustomTooltip toggleValue={props.toggleValue} />
                          }
                        />
                        <Legend
                          formatter={(value) => legendFormater(value)}
                          verticalAlign="top"
                          height={50}
                        />
                        <Bar
                          dataKey={
                            props.toggleValue === TOGGLE_CONSTANTS.TIME
                              ? "allocation"
                              : "allocated_cost"
                          }
                          barSize={20}
                          stackId="a"
                          fill={chart_data_color.allocated}
                          onClick={(event, index) => {
                            handleBarClick(
                              event,
                              props.toggleValue === TOGGLE_CONSTANTS.TIME
                                ? "allocation"
                                : "allocated_cost",
                              getBarLabelValues(),
                              index
                            );
                          }}
                        >
                          <Label
                            value={
                              props.toggleValue === TOGGLE_CONSTANTS.TIME
                                ? "Allocation"
                                : "Allocation"
                            }
                          ></Label>
                        </Bar>
                        <Bar
                          dataKey={
                            props.toggleValue === TOGGLE_CONSTANTS.TIME
                              ? "availability"
                              : "availability_cost"
                          }
                          barSize={20}
                          stackId="a"
                          fill={chart_data_color.availability}
                          onClick={(event, index) => {
                            handleBarClick(
                              event,
                              props.toggleValue === TOGGLE_CONSTANTS.TIME
                                ? "availability"
                                : "availability_cost",
                              getBarLabelValues(),
                              index
                            );
                          }}
                          label={{
                            value:
                              props.toggleValue === TOGGLE_CONSTANTS.TIME
                                ? "capacity"
                                : "capacity_cost",
                            position: "top",
                            height: "4px",
                            formatter: (value) => {
                              return value ? value.toLocaleString() : "";
                            },
                          }}
                        >
                          <Label
                            value={
                              props.toggleValue === TOGGLE_CONSTANTS.TIME
                                ? "Availability"
                                : "Availability"
                            }
                          ></Label>
                        </Bar>
                      </BarChart>
                    </ResponsiveContainer>
                  </div>
                </div>
              </div>
            </Grid>
          </Grid>
        </Grid>
      </div>
    </>
  );
};

const GetAgGridColumnDefForTime = () => {
  const columnDefs: any = [
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
      tooltipField: "employee_name",
      cellRendererFramework: (params: any) => {
        if (!params.value) return null;
        const email = params.data?.employee_mid+"__"+params.data?.email_id;

        return (
          <span
            style={{
              cursor: "pointer",
              color: "#1976d2",
              textDecoration: "underline",
            }}
            onClick={(e) => {
              e.stopPropagation();
              routeToEmployeeProfile(`/employee-profile/${email}`);
            }}
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
      headerName: "Leave hours",
      headerTooltip: "Leave hours",
      field: "net_leaves",
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
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "available_hr",
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
      headerName: "Allocation %",
      headerTooltip: "Allocation%",
      field: "allocation_percent",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
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
      tooltipField: "actual_chargability_percent",
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
    },
    {
      headerName: "Skills",
      headerTooltip: "Skills",
      field: "skills",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      tooltipField:"skills",
    }
  ];
  return columnDefs;
};

const GetAgGridColumnDefForCost = () => {
  const columnDefs: any = [
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
    },
    {
      headerName: "Designation",
      headerTooltip: "Designation",
      field: "designation_name",
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
      headerName: "Net Capacity",
      headerTooltip: "Net Capacity",
      field: "capacity_cost",
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
      headerName: "Availability Cost",
      headerTooltip: "Availability Cost",
      field: "availability_cost",
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
      headerName: "Allocation Cost",
      headerTooltip: "Allocation Cost",
      field: "allocated_cost",
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
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "allocation_percent",
    },
    {
      headerName: "Allocation Job Chargeable %",
      headerTooltip: "Job Chargeable %",
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
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "actual_chargability_percent",
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
    },
    {
      headerName: "Skills",
      headerTooltip: "Skills",
      field: "skills",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      tooltipField:"skills",
    }
  ];
  return columnDefs;
};

export default ColumnChart;
