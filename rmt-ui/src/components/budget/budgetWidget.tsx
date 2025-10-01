import React, { useContext, useEffect, useRef, useState } from "react";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import {
  Avatar,
  Box,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  ToggleButton,
  ToggleButtonGroup,
  Typography,
} from "@mui/material";
import AttachMoneyIcon from "@mui/icons-material/AttachMoney";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import AccessTimeOutlinedIcon from "@mui/icons-material/AccessTimeOutlined";
import CurrencyRupeeOutlinedIcon from "@mui/icons-material/CurrencyRupeeOutlined";

const BudgetWidget = (props: any) => {
  const [allocatedTextColor, setAllocatedTextColor] =
    useState<string>("#059862");
  const [timesheetTextColor, setTimesheetTextColor] =
    useState<string>("#059862");
  enum budgetStatusCodes {
    InBudget = "#059862",
    Amber = "#FFBF00",
    OutBudget = "#FF0000",
  }

  const setTextColor = (percentage: number) => {
    if (percentage < props?.budgetLimitConfig) {
      return budgetStatusCodes.InBudget;
    } else if (percentage >= props?.budgetLimitConfig && percentage <= 100) {
      return budgetStatusCodes.Amber;
    } else if (percentage > 100) {
      return budgetStatusCodes.OutBudget;
    }
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

  const calculateGauge = () => {
    const percentage = getActualBudgetPercentage();
    const allocationPercentage = getAllocatedBudgetPercentage();
    setTimesheetTextColor(setTextColor(percentage));
    setAllocatedTextColor(setTextColor(allocationPercentage));
  };
  useEffect(() => {
    calculateGauge();
  }, [props]);
  return (
    <>
      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                Job Fee
              </Typography>
              <Typography sx={{ fontSize: 30 }} color="text.primary">
                {props?.jobFee?.toLocaleString()}
                {/* need to get from wcgt */}
              </Typography>
            </span>
            <Avatar className="jobFee-card-icons">
              <CurrencyRupeeOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>

      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                No. of Resources
              </Typography>
              <Typography sx={{ fontSize: 30 }} color="text.primary">
                {props?.totalNoOfResource?.toLocaleString()}
              </Typography>
            </span>
            <Avatar className="resources-card-icons">
              <PersonOutlineOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>

      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                Original Budget
              </Typography>
              <Typography sx={{ fontSize: 30 }} color="text.primary">
                {props?.isBudgetNotAllocated
                  ? "-"
                  : props?.toggleValue === "cost"
                  ? props.overAllData?.totalOriginalBudgetCost?.toLocaleString() ||
                    ""
                  : props.overAllData?.totalOriginalBudgetHrs?.toLocaleString() ||
                    ""}
              </Typography>
            </span>
            <Avatar className="budgeted-card-icons">
              <CurrencyRupeeOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>

      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                Revised Budget
              </Typography>
              <Typography sx={{ fontSize: 30 }} color="text.primary">
                {props?.isBudgetNotAllocated
                  ? "-"
                  : props?.toggleValue === "cost"
                  ? props.overAllData?.totalBudgetCost?.toLocaleString() ===
                    props.overAllData?.totalOriginalBudgetCost?.toLocaleString()
                    ? "-"
                    : props.overAllData?.totalBudgetCost?.toLocaleString()
                  : props.overAllData?.totalBudgetHrs?.toLocaleString() ===
                    props.overAllData?.totalOriginalBudgetHrs?.toLocaleString()
                  ? "-"
                  : props.overAllData?.totalBudgetHrs?.toLocaleString()}
              </Typography>
            </span>
            <Avatar className="budgeted-card-icons">
              <CurrencyRupeeOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>

      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                Allocations
              </Typography>
              <Typography sx={{ fontSize: 30 }} color={allocatedTextColor}>
                {props?.toggleValue === "cost"
                  ? props.overAllData.totalAllocatedCost?.toLocaleString()
                  : props.overAllData.totalAllocatedhrs?.toLocaleString()}
              </Typography>
            </span>
            <Avatar className="allocation-card-icons-v1">
              <AccessTimeOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>

      <div className="budget-card">
        {" "}
        <Card sx={{ minWidth: 275 }}>
          <CardContent className="card-content-container">
            <span>
              <Typography
                sx={{ mb: 0, fontSize: 16 }}
                color="text.secondary"
                gutterBottom
              >
                Actual
              </Typography>
              <Typography sx={{ fontSize: 30 }} color={timesheetTextColor}>
                {props?.toggleValue === "cost"
                  ? props.overAllData.totalTImesheetCost?.toLocaleString()
                  : props.overAllData.totalTimesheetHrs?.toLocaleString()}
              </Typography>
            </span>
            <Avatar className="actual-card-icons-v1">
              <PersonOutlineOutlinedIcon />
            </Avatar>
          </CardContent>
        </Card>{" "}
      </div>
    </>
  );
};

export default BudgetWidget;
