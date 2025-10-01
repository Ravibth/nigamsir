import React from "react";
import { Stack, Typography } from "@mui/material";
import * as constant from "./constant";
import "./style.css";
import BusinessIcon from "@mui/icons-material/Business";
import { titleIcons } from "../marketplacetitle/marketplacesubtitle/constant";
import WorkOutlineOutlinedIcon from "@mui/icons-material/WorkOutlineOutlined";
import DonutSmallOutlinedIcon from "@mui/icons-material/DonutSmallOutlined";
import AccountBalanceWalletOutlinedIcon from "@mui/icons-material/AccountBalanceWalletOutlined";
import FactoryOutlinedIcon from "@mui/icons-material/FactoryOutlined";
import WarehouseOutlinedIcon from "@mui/icons-material/WarehouseOutlined";

const Marketprojectdetails = (props: any) => {
  const data = [
    {
      label: "Business Unit",
      value: props.projectdetails.businessUnit,
      icon: <BusinessIcon sx={titleIcons} />,
    },
    {
      label: "Offerings",
      value: props.projectdetails.offerings,
      icon: <WorkOutlineOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "Solutions",
      value: props.projectdetails.solutions,
      icon: <WorkOutlineOutlinedIcon sx={titleIcons} />,
    },
    // {
    //   label: "Expertise",
    //   value: props.projectdetails.expertise,
    //   icon: <WorkOutlineOutlinedIcon sx={titleIcons} />,
    // },
    // {
    //   label: "SMEG",
    //   value: props.projectdetails.smeg,
    //   icon: <DonutSmallOutlinedIcon sx={titleIcons} />,
    // },
    // {
    //   label: "SME",
    //   value: props.projectdetails.sme,
    //   icon: <DonutSmallOutlinedIcon sx={titleIcons} />,
    // },
    // {
    //   label: "RU",
    //   value: props.projectdetails.revenueUnit,
    //   icon: <AccountBalanceWalletOutlinedIcon sx={titleIcons} />,
    // },
    {
      label: "Industry",
      value: props.projectdetails.industry,
      icon: <FactoryOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "Sub-Industry",
      value: props.projectdetails.subindustry,
      icon: <WarehouseOutlinedIcon sx={titleIcons} />,
    },
  ];

  return (
    <div className="MarketprojectdetailsMainHeading">
      <Stack direction="row" spacing={2}>
        {data.map((item, index) => (
          <div key={index}>
            <div className="MarketprojectdetailsSubHeading">
              <span className="icon-container-marketplace">{item.icon}</span>
              <Typography sx={constant.subtitle} variant="body1">
                <span className="subtitle2">
                  {item?.label} {":"}
                </span>{" "}
                <span className="subtitle3">{item?.value}</span>
              </Typography>
            </div>
          </div>
        ))}
      </Stack>
    </div>
  );
};

export default Marketprojectdetails;
