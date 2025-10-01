import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
  Label,
} from "recharts";
import { useState } from "react";

import "./bar_chart.scss";
import "../custom-tootlip/custom_tooltip.scss";
import { CustomTooltip } from "../custom-tootlip/custom_tooltip";
import { Card, CardContent } from "@mui/material";
import { IndianNumberFormatter } from "../../../../../utils/date/dateHelper";
import { ChartColors } from "../../../constant";

//Scheduled vs Variance Chart Cost Chart
const CostBarCharts = (props) => {
  const [xaxisTitle, setxAxisTitle] = useState("Cost");
  const [yaxisTitle, setyAxisTitle] = useState("Months");
  // };
  const bar_data_coolor = {
    actual: ChartColors.Actual,
    allocated: ChartColors.Allocation,
    capacity: ChartColors.Capacity,
  };

  const legendFormater = (value: any) => {
    value = value.replaceAll("_", " ");
    return (
      <span className="legend-text-color">
        {value.charAt(0).toUpperCase() + value.slice(1)}
      </span>
    );
  };

  const handleBarClick = (event: any, dataKeySelected: string, index: any) => {
    props.handleBarClick(event, dataKeySelected, index, event.payload);
  };
  const CustomXAxisTick = (props) => {
    const { x, y, payload } = props;
    return (
      <g transform={`translate(${x},${y})`}>
        <text
          x={0}
          y={0}
          dy={16}
          textAnchor="middle"
          fill="#666"
          transform="rotate(0)"
        >
          {payload.value ? IndianNumberFormatter(payload.value) : ""}
        </text>
      </g>
    );
  };

  return (
    <div className="main-reports-graph-container">
      <Card>
        <CardContent>
          <div className="question">
            <div className="question-container">
              <ResponsiveContainer width="90%" height="100%">
                <BarChart
                  data={props.data}
                  layout="vertical"
                  margin={{ top: 5, right: 30, left: 50, bottom: 20 }}
                  barCategoryGap={4}
                  barGap={20}
                >
                  <XAxis type="number" tick={<CustomXAxisTick />}>
                    <Label
                      style={{
                        textAnchor: "middle",
                        fontSize: "80%",
                        fill: "black",
                      }}
                      value={xaxisTitle}
                      position={"center"}
                      // position={"bottom"}
                      // dx={20}
                      dy={25}
                    />
                  </XAxis>
                  <YAxis type="category" dataKey="date" width={90}>
                    <Label
                      style={{
                        textAnchor: "middle",
                        fontSize: "80%",
                        fill: "black",
                        padding: "100px",
                      }}
                      value={yaxisTitle}
                      angle={-90}
                      position={"left"}
                    />
                  </YAxis>
                  <Tooltip
                    content={<CustomTooltip toggleValue={props.chartType} />}
                  />
                  <CartesianGrid strokeDasharray="3 3" />
                  <Legend
                    formatter={(value) => legendFormater(value)}
                    verticalAlign="top"
                    height={36}
                  />

                  <Bar
                    dataKey="capacity_cost"
                    fill={bar_data_coolor.capacity}
                    onClick={(event, index) => {
                      handleBarClick(event, "capacity_cost", index);
                    }}
                  />
                  <Bar
                    dataKey="allocated_cost"
                    fill={bar_data_coolor.allocated}
                    onClick={(event, index) => {
                      handleBarClick(event, "allocated_cost", index);
                    }}
                  ></Bar>
                  <Bar
                    dataKey="actual_cost"
                    fill={bar_data_coolor.actual}
                    onClick={(event, index) => {
                      handleBarClick(event, "actual_cost", index);
                    }}
                  ></Bar>
                </BarChart>
              </ResponsiveContainer>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default CostBarCharts;
//Scheduled vs Variance Chart Cost Chart
