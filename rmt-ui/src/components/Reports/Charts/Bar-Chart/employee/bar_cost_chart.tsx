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
import { CustomTooltip } from "../custom-tootlip/custom_tooltip";

import "./bar_chart.scss";
import "../custom-tootlip/custom_tooltip.scss";
import { Card, CardContent } from "@mui/material";
import { ChartColors } from "../../../constant";

//Scheduled vs Variance Chart Cost Chart
const CostBarCharts = (props) => {
  //console.log("CostBarCharts", props);
  const [xaxisTitle, setxAxisTitle] = useState("Resource Details");
  const [yaxisTitle, setyAxisTitle] = useState("Cost Details");

  const bar_data_coolor = {
    actual: ChartColors.Actual,
    allocated: ChartColors.Allocation,
    capacity: ChartColors.Capacity,
  };
  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Actual Cost",
        field: "actual_cost",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "Allocated Cost",
        field: "allocated_cost",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },

      {
        headerName: "Capapcity Cost",
        field: "capacity_cost",
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
        headerName: "Email Id",
        field: "email_id",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "job_chargeable",
        field: "job_chargeable",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      {
        headerName: "job_non_chargeable",
        field: "job_non_chargeable",
        flex: 1,
        suppressMenu: true,
        tooltipField: "action",
      },
      // {
      //   headerName: "expertise",
      //   field: "expertise",
      //   flex: 1,
      //   suppressMenu: true,
      //   tooltipField: "action",
      // },
    ],
    [props.data]
  );
  useEffect(() => {
    console.log(props.data);
    //props.setRowData(props.data);
    props.setagColumns(columnDefs);
  });

  const legendFormater = (value: any) => {
    value = value.replaceAll("_", " ");
    return (
      <Card>
        <CardContent>
          <div className="question">
            <div className="question-container">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart
                  data={props.data}
                  layout="vertical"
                  margin={{ top: 5, right: 30, left: 50, bottom: 10 }}
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
                      dx={20}
                      dy={25}
                    />
                  </XAxis>
                  <YAxis type="category" dataKey="date">
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
                    dataKey="actual_cost"
                    fill={bar_data_coolor.actual}
                    onClick={(event, index) => {
                      handleBarClick(event, "actual_cost", index);
                    }}
                  >
                    <LabelList
                      dataKey="actual_cost_percentage"
                      position="right"
                      style={{ fill: "black", fontWeight: 500 }}
                      className="label-text"
                      formatter={(value) => {
                        return value + "%";
                      }}
                    />
                  </Bar>
                  <Bar
                    dataKey="allocated_cost"
                    fill={bar_data_coolor.allocated}
                    onClick={(event, index) => {
                      handleBarClick(event, "allocated_cost", index);
                    }}
                  >
                    <LabelList
                      dataKey="allocation_cost_percentage"
                      position="right"
                      formatter={(value) => {
                        return value + "%";
                      }}
                      style={{ fill: "black", fontWeight: 500 }}
                      className="label-text"
                    />
                  </Bar>
                  <Bar
                    dataKey="capacity_cost"
                    fill={bar_data_coolor.capacity}
                    onClick={(event, index) => {
                      handleBarClick(event, "capacity_cost", index);
                    }}
                  />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </div>
        </CardContent>
      </Card>
    );
  };

  const handleBarClick = (event: any, dataKeySelected: string, index: any) => {
    // props.handleBarClick(event, dataKeySelected, index, 'test');
    props.setRowData([event.payload]);
  };

  return (
    <Card>
      <CardContent>
        <div className="question">
          <div className="question-container">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart
                data={props.data}
                layout="vertical"
                margin={{ top: 5, right: 30, left: 50, bottom: 10 }}
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
                    dx={20}
                    dy={25}
                  />
                </XAxis>
                <YAxis type="category" dataKey="date">
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
                  dataKey="actual_cost"
                  fill={bar_data_coolor.actual}
                  onClick={(event, index) => {
                    handleBarClick(event, "actual_cost", index);
                  }}
                >
                  <LabelList
                    dataKey="actual_cost_percentage"
                    position="right"
                    style={{ fill: "black", fontWeight: 500 }}
                    className="label-text"
                    formatter={(value) => {
                      return value + "%";
                    }}
                  />
                </Bar>
                <Bar
                  dataKey="allocated_cost"
                  fill={bar_data_coolor.allocated}
                  onClick={(event, index) => {
                    handleBarClick(event, "allocated_cost", index);
                  }}
                >
                  <LabelList
                    dataKey="allocation_cost_percentage"
                    position="right"
                    formatter={(value) => {
                      return value + "%";
                    }}
                    style={{ fill: "black", fontWeight: 500 }}
                    className="label-text"
                  />
                </Bar>
                <Bar
                  dataKey="capacity_cost"
                  fill={bar_data_coolor.capacity}
                  onClick={(event, index) => {
                    handleBarClick(event, "capacity_cost", index);
                  }}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

export default CostBarCharts;
//Scheduled vs Variance Chart Cost Chart
