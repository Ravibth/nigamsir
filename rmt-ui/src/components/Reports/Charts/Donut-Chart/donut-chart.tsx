import React, { useState } from "react";
import { PieChart, Pie, Legend, Tooltip, Label } from "recharts";

const DonutChart = (props) => {
  // console.log(props.data);
  // console.log(props);
  const { data, width, height, labelDisplayText, label } = props;

  const CustomLabel = ({ viewBox, labelText, value }) => {
    const { cx, cy } = viewBox;
    return (
      <g>
        <text
          x={cx}
          y={cy}
          className="recharts-text recharts-label"
          textAnchor="middle"
          dominantBaseline="central"
          alignmentBaseline="middle"
          fontSize="15"
        >
          {labelText}
        </text>
        <text
          x={cx}
          y={cy + 20}
          className="recharts-text recharts-label"
          textAnchor="middle"
          dominantBaseline="central"
          alignmentBaseline="middle"
          fontSize="15"
        >
          {value}
        </text>
      </g>
    );
  };

  return (
    <>
      <PieChart width={width} height={height}>
        <Legend
          height={36}
          iconType="circle"
          layout="horizontal"
          verticalAlign="bottom"
          iconSize={10}
          //formatter={renderColorfulLegendText}
        />
        <Pie
          data={data}
          cx={"50%"}
          cy={"50%"}
          innerRadius={60}
          outerRadius={80}
          fill="#8884d8"
          paddingAngle={0}
          dataKey="value"
        >
          <Label
            content={
              <CustomLabel
                labelText={labelDisplayText}
                value={label}
                viewBox={"sasas"}
              />
            }
            // value={10}
            position="centerBottom"
            className="label-top"
            fontSize="22px"
          />
        </Pie>
        <Tooltip />
      </PieChart>
    </>
  );
};

export default DonutChart;
