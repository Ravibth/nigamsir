import { useEffect, useState } from "react";
import {
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  Typography,
} from "@mui/material";
import ControllerCalendar from "../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Legend,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
  Label,
} from "recharts";
import { ChartColors } from "../Reports/constant";
import { HorizontalCenterAlignSxProps } from "../common-allocation/user-info-timeline-group/style";
import RectangleIcon from "@mui/icons-material/Rectangle";

const calculateChartWidth = (dataLength) => {
  const barWidth = 60;
  const spacing = 20;
  const finalWidth =
    dataLength > 20 ? dataLength * (barWidth + spacing) + "px" : "100%";
  return finalWidth;
};

const BudgetAllocatedActualGraph = (props: any) => {
  const {
    formState: { errors },
    control,
    watch,
  } = useForm({
    mode: "onTouched",
  });
  const [selectedTimeOption, setSelectedTimeOption] = useState<string>("daily");
  const [graphData, setGraphData] = useState<any>([]);
  const [startDate, setStartDate] = useState<Date>(
    props?.projectDetails?.startDate
  );
  const [endDate, setEndDate] = useState<Date>(props?.projectDetails?.endDate);
  const [timeOptionStatus, setTimeOptionStatus] = useState<any>({
    daily: false,
    weekly: false,
    monthly: false,
    quarterly: false,
  });

  useEffect(() => {
    dataGenrate();
  }, [props]);

  const dataGenrate = () => {
    let data = [];
    const dataset = props.totalPlannedBudget;
    if (dataset?.length > 0) {
      data = dataset?.map((d) => [
        d.key,
        props?.toggleValue == "cost"
          ? d.totalAllocationCost
          : d.totalAllocationTime,
        props?.toggleValue == "cost"
          ? d.totalTimesheetCost
          : d.totalTimesheetTime,
      ]);
      props?.toggleValue == "cost"
        ? data.unshift(["Time Period", "Allocated Cost", "Actual Cost"])
        : data.unshift(["Time Period", "Allocated Time", "Actual Time"]);
    } else {
      props?.toggleValue == "cost"
        ? data.push(["Time Period", "Allocated Cost", "Actual Cost"])
        : data.push(["Time Period", "Allocated Time", "Actual Time"]);
    }

    if (dataset?.length < 5) {
      data.push([" ", 0, 0]);
      data.push([" ", 0, 0]);
      data.push([" ", 0, 0]);
      data.push([" ", 0, 0]);
      data.push([" ", 0, 0]);
    }
    setGraphData(dataset);
  };

  const updateTimeOption = (startDate, endDate) => {
    const oneDay = 24 * 60 * 60 * 1000;
    const diffDays = Math.round(Math.abs((endDate - startDate) / oneDay));
    const status = timeOptionStatus;
    if (diffDays < 7) {
      status["weekly"] = true;
      status["monthly"] = true;
      status["quarterly"] = true;
    } else if (diffDays >= 7 && diffDays <= 30) {
      status["weekly"] = false;
      status["monthly"] = true;
      status["quarterly"] = true;
    } else if (diffDays > 30 && diffDays < 90) {
      status["weekly"] = false;
      status["monthly"] = false;
      status["quarterly"] = true;
    } else {
      status["daily"] = false;
      status["weekly"] = false;
      status["monthly"] = false;
      status["quarterly"] = false;
    }
    setTimeOptionStatus(status);
  };

  const chartWidth = calculateChartWidth(graphData ? graphData.length : "100%");

  return (
    <div>
      <div className="budget-main-container">
        <div className="budget-heading">
          <span className="heading-weight"> Allocation/ Actual </span>
          <span className="budget-toggle"> </span>
        </div>
        <div className="budget-heading-control">
          <span className="budget-filter">
            {" "}
            <FormControl fullWidth className="day-selector">
              <InputLabel
                id="demo-simple-select-label"
                className="budget-timeoption"
              >
                Time
              </InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                // value={age}
                label="Time Period"
                defaultValue={"daily"}
                onChange={(e) => {
                  setSelectedTimeOption(e.target.value);
                  e.target.value == "quarterly"
                    ? props.setPlannedActualOption(
                        "quarterly",
                        startDate,
                        endDate
                      )
                    : props.setPlannedActualOption(
                        e.target.value,
                        startDate,
                        endDate
                      );
                }}
                // size="small"
              >
                <MenuItem value={"daily"} disabled={timeOptionStatus["daily"]}>
                  Daily
                </MenuItem>
                <MenuItem
                  value={"weekly"}
                  disabled={timeOptionStatus["weekly"]}
                >
                  Weekly
                </MenuItem>
                <MenuItem
                  value={"monthly"}
                  disabled={timeOptionStatus["monthly"]}
                >
                  Monthly
                </MenuItem>
                <MenuItem
                  value={"quarterly"}
                  disabled={timeOptionStatus["quarterly"]}
                >
                  Quarterly
                </MenuItem>
              </Select>
            </FormControl>
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
                    props.setPlannedActualOption(
                      selectedTimeOption,
                      date,
                      new Date(endDate)
                    );
                    updateTimeOption(new Date(date), new Date(endDate));
                  }}
                  value={startDate}
                />
              </span>
              <span className="date-selector">
                {" "}
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
                    props.setPlannedActualOption(
                      selectedTimeOption,
                      startDate,
                      new Date(date)
                    );
                    updateTimeOption(new Date(startDate), new Date(date));
                  }}
                />
              </span>
            </div>
          </span>
        </div>
      </div>

      <Grid container spacing={2}>
        <Grid
          item
          xs={0.5}
          sx={{
            transform: "rotate(270deg)",
            display: "flex",
            justifyContent: "left",
            alignItems: "center",
          }}
        >
          {props?.toggleValue === "cost" ? "Cost(INR)" : "Time(hrs)"}
        </Grid>
        <Grid item xs={11.5}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <div
                style={{
                  height: "450px",
                  overflowY: "auto",
                  overflowX: "auto",
                  whiteSpace: "nowrap",
                }}
              >
                <div
                  style={{ display: "inline-block", minWidth: `${chartWidth}` }}
                >
                  <ResponsiveContainer width="100%" height={420}>
                    <BarChart
                      data={graphData}
                      margin={{ top: 20, right: 18, left: 0, bottom: 20 }}
                    >
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis
                        dataKey="key"
                        angle={45}
                        dx={15}
                        dy={13}
                        minTickGap={-200}
                        fontFamily="GtWalsheimFont, sans-serif, Arial"
                        fontSize={"15px"}
                      ></XAxis>
                      <YAxis
                        fontFamily="GtWalsheimFont, sans-serif, Arial"
                        fontSize={"15px"}
                        dx={2}
                        width={90}
                      ></YAxis>
                      {graphData?.length ? <Tooltip /> : ""}
                      <Bar
                        dataKey={
                          props?.toggleValue === "cost"
                            ? "totalAllocationCost"
                            : "totalAllocationTime"
                        }
                        name={
                          props?.toggleValue === "cost"
                            ? "Allocation"
                            : "Allocation"
                        }
                        fill={ChartColors.Allocation} //"#4F2D7F"
                        barSize={20}
                      />
                      <Bar
                        dataKey={
                          props?.toggleValue === "cost"
                            ? "totalTimesheetCost"
                            : "totalTimesheetTime"
                        }
                        name={
                          props?.toggleValue === "cost" ? "Actual" : "Actual"
                        }
                        fill={ChartColors.Actual} //"#988ABD"
                        barSize={20}
                      />
                    </BarChart>
                  </ResponsiveContainer>
                </div>
              </div>
            </Grid>
          </Grid>
          <Grid container spacing={2}>
            <Grid
              item
              xs={12}
              sx={{ ...HorizontalCenterAlignSxProps, marginTop: "5px" }}
            >
              Time Period
            </Grid>
            <Grid
              item
              xs={12}
              sx={{ ...HorizontalCenterAlignSxProps, marginTop: "-15px" }}
            >
              <RectangleIcon
                sx={{
                  color: ChartColors.Allocation,
                  fontSize: "unset",
                  mt: "4px",
                  mr: "4px",
                }}
                fontSize="small"
              />
              <Typography sx={{ color: ChartColors.Allocation }}>
                Allocation
              </Typography>
              <RectangleIcon
                fontSize="small"
                sx={{
                  color: ChartColors.Actual,
                  fontSize: "unset",
                  mr: "4px",
                  mt: "4px",
                  ml: "10px",
                }}
              />
              <Typography sx={{ color: ChartColors.Actual }}>Actual</Typography>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};

export default BudgetAllocatedActualGraph;
