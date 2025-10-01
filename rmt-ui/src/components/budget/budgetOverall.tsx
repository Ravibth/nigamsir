import React, { useEffect, useState } from "react";
import { ToggleButton, ToggleButtonGroup, Tooltip } from "@mui/material";
import ReactSpeedometer from "react-d3-speedometer";
import InfoIcon from "@mui/icons-material/Info";

const BudgetOverall = (props: any) => {
  const [gaugePercentage, setGaugePercentage] = useState<any>(0);
  const [maxGaugePercentage, setMaxGaugePercentage] = useState<any>(100);
  const [segments, setSegments] = useState<Array<number>>([0, 90, 100]);
  const [budgetStatus, setBudgetStatus] = useState<string>("In-Budget");
  const [gaugeToggleValue, setGaugeToggleValue] =
    useState<string>("allocation");

  const config = props?.budgetLimitConfig ? props?.budgetLimitConfig : 90;

  const getAllocatedBudgetPercentage = () => {
    const total =
      props?.toggleValue === "cost"
        ? props.overAllData.totalBudgetCost
        : props.overAllData.totalBudgetHrs;
    const allocated =
      props?.toggleValue === "cost"
        ? props.overAllData.totalAllocatedCost
        : props.overAllData.totalAllocatedhrs;
    const percentage = (allocated / total) * 100;
    return percentage;
  };

  const getActualBudgetPercentage = () => {
    const budget =
      props?.toggleValue === "cost"
        ? props.overAllData.totalBudgetCost
        : props.overAllData.totalBudgetHrs;
    const actual =
      props?.toggleValue === "cost"
        ? props.overAllData.totalTImesheetCost
        : props.overAllData.totalTimesheetHrs;

    const percentage = (actual / budget) * 100;
    return percentage;
  };

  const calculateGaugePercentage = (option: string) => {
    let percentage = 0;
    if (option === "allocation") {
      percentage = getAllocatedBudgetPercentage();
    } else {
      percentage = getActualBudgetPercentage();
    }
    const gauge =
      Math.round(percentage * 10) / 10 ? Math.round(percentage * 10) / 10 : 0;
    setGaugePercentage(gauge);

    if (gauge > 100) {
      const max = Math.ceil(gauge / 50) * 50;
      const seg = [0, config, 100, max];
      setSegments(seg);
      setMaxGaugePercentage(max);
      setBudgetStatus("Out-Budget");
    } else {
      const seg = [0, config, 100];
      setSegments(seg);
      setMaxGaugePercentage(100);
      setBudgetStatus("In-Budget");
    }
    if (gauge >= config && gauge <= 100) {
      setBudgetStatus("Amber");
    }
    if (gauge.toString() == "Infinity") {
      const seg = [0, config, 100];
      setSegments(seg);
      setMaxGaugePercentage(100);
    }
    return percentage;
  };

  useEffect(() => {
    calculateGaugePercentage(gaugeToggleValue);
  }, [gaugeToggleValue]);

  return (
    <>
      <div className="card-container">
        <div>
          <div className="gauge-toggle">
            <span>
              <ToggleButtonGroup
                value={gaugeToggleValue}
                exclusive
                onChange={(e: any) => {
                  setGaugeToggleValue(e.target?.value?.toString());
                }}
                aria-label="text alignment"
              >
                <ToggleButton value="allocation" aria-label="left aligned">
                  Allocation
                </ToggleButton>
                <ToggleButton value="timesheet" aria-label="centered">
                  Actual
                </ToggleButton>
              </ToggleButtonGroup>{" "}
            </span>

            <span>
              <Tooltip
                sx={{ marginLeft: "5px" }}
                className={"tool-requisition"}
                title={
                  gaugeToggleValue == "allocation"
                    ? `Allocation(%) = (Total Allocation / Total Budget) * 100`
                    : `Actual(%) = (Total Actual / Total Budget) * 100`
                }
                placement="right"
              >
                <InfoIcon />
              </Tooltip>
            </span>
          </div>

          <div className="budget-card-gauge">
            <ReactSpeedometer
              value={
                gaugePercentage == "Infinity"
                  ? 0
                  : Math.round(gaugePercentage * 10) / 10
              }
              segments={2}
              height={350}
              width={500}
              paddingVertical={1}
              maxValue={maxGaugePercentage}
              forceRender={true}
              customSegmentStops={segments}
              segmentColors={
                props?.isBudgetNotAllocated
                  ? ["#D3D3D3"]
                  : ["#5BE12C", "#F5CD19", "#EA4228"]
              }
              currentValueText={
                props?.isBudgetNotAllocated
                  ? props?.toggleValue === "cost"
                    ? "Budgeted Cost Not Allocated"
                    : "Budgeted Hours Not Allocated"
                  : props?.overAllData?.isBudgetNotAllocated
                  ? props?.toggleValue === "cost"
                    ? "Budgeted Cost Not Allocated"
                    : "Budgeted Hours Not Allocated"
                  : gaugePercentage == "Infinity"
                  ? props?.toggleValue === "cost"
                    ? "Budgeted Cost Not Allocated"
                    : "Budgeted Hours Not Allocated"
                  : budgetStatus +
                    ": " +
                    Math.round(gaugePercentage * 10) / 10 +
                    "%"
              }
            />
          </div>
        </div>
      </div>
    </>
  );
};

export default BudgetOverall;
