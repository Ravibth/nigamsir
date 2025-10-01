import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
  LabelList,
  Label,
} from "recharts";
import { useEffect, useMemo, useState } from "react";

import "./bar_chart.scss";
import "../custom-tootlip/custom_tooltip.scss";

import { CustomTooltip } from "../custom-tootlip/custom_tooltip";
import { Card, CardContent } from "@mui/material";
import { ChartColors } from "../../../constant";

//Scheduled vs Variance Chart Hour Chart
const BarHoursCharts = (props) => {
  //console.log("props ===>", props);
  const [xaxisTitle, setxAxisTitle] = useState("Resource Details");
  const [yaxisTitle, setyAxisTitle] = useState("Months");
  const [chartData, setChartData] = useState<string[]>([]);
  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Date",
        field: "date",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Actual Log Hours",
        field: "actual_log_hours",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Allocation Log Hours",
        field: "allocation_hours",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },

      {
        headerName: "Capacity",
        field: "capacity",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Business Unit",
        field: "business_unit",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Email Id",
        field: "email_id",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },

      {
        headerName: "Job Chargeable",
        field: "job_chargeable",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Job Non Chargeable",
        field: "job_non_chargeable",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      // {
      //     headerName: 'expertise',
      //     field: 'expertise',
      //     flex: 1,
      //     suppressMenu: true,
      //     tooltipField: 'action',
      // },
    ],
    [props.data]
  );
  useEffect(() => {
    props.setagColumns(columnDefs);
  }, [props.data]);

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
    props.setRowData([event.payload]);
    const { name } = event;
  };

  return (
    <div className="main-reports-graph-container">
      <Card>
        <CardContent>
          <div className="question">
            <div className="question-container">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart
                  data={props.data}
                  layout="vertical"
                  margin={{ top: 5, right: 30, left: 50, bottom: 20 }}
                  barCategoryGap={2}
                  barGap={20}
                >
                  <XAxis type="number">
                    <Label
                      style={{
                        textAnchor: "middle",
                        fontSize: "80%",
                        fill: "black",
                      }}
                      value={xaxisTitle}
                      position={"bottom"}
                    />
                  </XAxis>
                  <YAxis type="category" dataKey="date" tickMargin={20}>
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
                  <Tooltip content={<CustomTooltip />} />
                  <CartesianGrid strokeDasharray="3 3" />
                  <Legend
                    formatter={(value) => legendFormater(value)}
                    verticalAlign="top"
                    height={36}
                  />
                  <Bar
                    dataKey="actual_log_hours"
                    fill={bar_data_coolor.actual}
                    onClick={(event, index) => {
                      handleBarClick(event, "actual_log_hours", index);
                    }}
                  >
                    <LabelList
                      dataKey="actual_percentage"
                      position="right"
                      style={{ fill: "black", fontWeight: 500 }}
                      className="label-text"
                      formatter={(value) => {
                        return value + "%";
                      }}
                    />
                  </Bar>
                  <Bar
                    dataKey="allocation_hours"
                    fill={bar_data_coolor.allocated}
                    onClick={(event, index) => {
                      handleBarClick(event, "allocation_hours", index);
                    }}
                  >
                    <LabelList
                      dataKey="allocation_percentage"
                      position="right"
                      formatter={(value) => {
                        return value + "%";
                      }}
                      style={{ fill: "black", fontWeight: 500 }}
                      className="label-text"
                    />
                  </Bar>
                  <Bar
                    dataKey="capacity"
                    fill={bar_data_coolor.capacity}
                    onClick={(event, index) => {
                      handleBarClick(event, "capacity", index);
                    }}
                  />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default BarHoursCharts;
//Scheduled vs Variance Chart Hour Chart
