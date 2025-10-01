import { useEffect, useState } from "react";
import ControllerCalendar from "../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import BudgetFilter from "./budget-filter/budgetFilter";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  ResponsiveContainer,
  Label,
} from "recharts";
import _ from "lodash";
import { ChartColors } from "../Reports/constant";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import { Grid, Tooltip as TooltipMui } from "@mui/material";

const BudgetResourceWiseGraph = (props: any) => {
  const {
    formState: { errors },
    control,
  } = useForm({
    mode: "onTouched",
  });

  const [identityData, setIdentityData] = useState<any>();
  const [startDate, setStartDate] = useState<Date>(
    props?.projectDetails?.startDate
  );
  const [endDate, setEndDate] = useState<Date>(props?.projectDetails?.endDate);

  const [allocatedData, setAllocatedData] = useState<any>();
  const [acutalData, setActualData] = useState<any>();
  const [chartHeight, setChartHeight] = useState<number>();

  useEffect(() => {
    const dataLength = allocatedData ? allocatedData.length : 1;
    const defaultHeight = 450;
    const barDistance = 75;
    const itemLimit = 7;
    let finalHeight =
      dataLength > itemLimit ? dataLength * barDistance : defaultHeight;
    finalHeight = finalHeight < defaultHeight ? defaultHeight : finalHeight;
    setChartHeight(finalHeight);
  }, [allocatedData]);

  useEffect(() => {
    if (props?.resourceWiseGraphData) {
      dataGenrate();
      let identityData = [];
      props?.resourceWiseGraphData.forEach((element) => {
        identityData.push(element?.identityUserResponse);
      });
      setIdentityData(identityData);
    }
  }, [props]);

  const dataGenrate = () => {
    let allocatedData = [];
    let acutalData = [];
    const dataset = props?.resourceWiseFilterData?.length
      ? props?.resourceWiseFilterData
      : props?.resourceWiseGraphData;
    dataset?.forEach((d) =>
      allocatedData.push({
        empName: d.empName,
        Allocation:
          props?.toggleValue == "cost"
            ? d.allocationtotalcost
            : d.allocationtotaltime,
        Actual:
          props?.toggleValue == "cost"
            ? d.timesheettotalCost
              ? d.timesheettotalCost
              : 0
            : d.timesheettotaltime
            ? d.timesheettotaltime
            : 0,
      })
    );

    setAllocatedData(allocatedData);
    setActualData(acutalData);
  };

  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);
  const checkIfIsFilterApplied = () => {
    if (
      props.appliedFilters &&
      Object.values(props.appliedFilters).find((v: any) => v && v.length > 0)
    ) {
      setIsFilterApplied(true);
      return true;
    } else {
      setIsFilterApplied(false);
      return false;
    }
  };

  useEffect(() => {
    checkIfIsFilterApplied();
  }, [props.appliedFilters]);

  return (
    <div id="budgetResourceGraph">
      <div className="budget-main-container">
        <div className="budget-heading">
          <span className="heading-weight">
            Resource wise Allocation/ Actual
          </span>
        </div>
        <div className="budget-heading-control">
          <span className="budget-filter">
            {isFilterApplied && (
              <div>
                <TooltipMui title="Filters applied">
                  <FilterAltIcon fontSize="large" />
                </TooltipMui>
              </div>
            )}
          </span>
          <span className="budget-filter">
            <BudgetFilter
              identityData={identityData}
              selectedDataByFilter={props.selectedDataByFilter}
            />
          </span>
          <span className="budget-toggle">
            <div className="budget-calander-container">
              <span className="date-selector">
                <ControllerCalendar
                  name="startDate"
                  control={control}
                  defaultValue={new Date(props?.projectDetails?.startDate)}
                  required={true}
                  minDate={new Date(props?.projectDetails?.startDate)}
                  maxDate={new Date(props?.projectDetails?.endDate)}
                  label={"Start Date"}
                  error={errors.startDate}
                  onChange={(date: any) => {
                    setStartDate(new Date(date));
                    props.dateRangeChange(date, new Date(endDate));
                  }}
                  value={startDate}
                  size="small"
                />
              </span>
              <span className="date-selector">
                <ControllerCalendar
                  name="endDate"
                  control={control}
                  defaultValue={new Date(props?.projectDetails?.endDate)}
                  required={true}
                  label={"End Date"}
                  minDate={new Date(props?.projectDetails?.startDate)}
                  maxDate={new Date(props?.projectDetails?.endDate)}
                  error={errors.endDate}
                  value={endDate}
                  onChange={(date: any) => {
                    setEndDate(new Date(date));
                    props.dateRangeChange(startDate, new Date(date));
                  }}
                  size="small"
                />
              </span>
            </div>
          </span>
        </div>
      </div>

      <div
        style={{
          height: "480px",
          overflowY: "auto",
          overflowX: "auto",
          whiteSpace: "nowrap",
        }}
      >
        {allocatedData?.length == 1 && acutalData?.length == 1 ? (
          <div> No Data</div>
        ) : (
          <div>
            <ResponsiveContainer height={chartHeight}>
              <BarChart
                data={allocatedData}
                layout="vertical"
                margin={{
                  top: 5,
                  right: 30,
                  left: 20,
                  bottom: 5,
                }}
                height={chartHeight}
              >
                <XAxis type="number">
                  <Label
                    style={{
                      textAnchor: "middle",
                      fontSize: "100%",
                      fill: "#666",
                      padding: "100px",
                    }}
                    value={
                      props?.toggleValue === "cost" ? "Cost(INR)" : "Time(hrs)"
                    }
                    angle={0}
                    position={"middle"}
                    dy={20}
                  />
                </XAxis>
                <YAxis type="category" dataKey="empName" width={90}>
                  <Label
                    style={{
                      textAnchor: "middle",
                      fontSize: "100%",
                      fill: "#666",
                      padding: "100px",
                    }}
                    value={"Resource"}
                    angle={-90}
                    position={"left"}
                  />
                </YAxis>
                {allocatedData?.length ? <Tooltip /> : ""}
                <Legend
                  wrapperStyle={{
                    paddingTop: "20px",
                  }}
                />
                <Bar
                  dataKey={"Allocation"}
                  fill={ChartColors.Allocation}
                  barSize={20}
                />
                <Bar
                  dataKey={"Actual"}
                  fill={ChartColors.Actual}
                  barSize={20}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        )}
      </div>
    </div>
  );
};
export default BudgetResourceWiseGraph;
