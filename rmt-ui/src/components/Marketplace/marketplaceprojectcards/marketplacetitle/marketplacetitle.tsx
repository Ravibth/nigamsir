import React from "react";
import Marketmaintitle from "./marketplacemaintitle/marketmaintitle";
import Marketsubtitle from "./marketplacesubtitle/marketsubtitle";
import { Stack, Typography } from "@mui/material";
import TurnedInNotIcon from "@mui/icons-material/TurnedInNot";
import { titleIcons } from "./marketplacesubtitle/constant";
import moment from "moment";
import PeopleAltOutlinedIcon from "@mui/icons-material/PeopleAltOutlined";
import CalendarTodayOutlinedIcon from "@mui/icons-material/CalendarTodayOutlined";
import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined";
import WorkOutlineOutlinedIcon from "@mui/icons-material/WorkOutlineOutlined";

const Marketplacetitle = (props: any) => {
  const checkMaskingForProjectId = () => {
    var obj = JSON.parse(props.projectDetail.jsonData);
    if (obj.projectCode == true || obj.projectId == true) {
      return "xxxxxxxx";
    }
    return props.projectDetail.pipelineCode;
  };

  const marketplaceCardDateFormat = "DD-MM-YYYY";

  const data1 = [
    {
      label: "Project ID : ",
      value: checkMaskingForProjectId(),
      icon: <TurnedInNotIcon sx={titleIcons} />,
    },
    {
      label: "Client Group : ",
      value: props.projectDetail.clientGroup,
      icon: <PeopleAltOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "Client Name : ",
      value: props.projectDetail.clientName,
      icon: <PeopleAltOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "Start Date : ",
      value: moment(props.projectDetail.startDate).format(
        marketplaceCardDateFormat
      ),
      icon: <CalendarTodayOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "End Date : ",
      value: moment(props.projectDetail.endDate).format(
        marketplaceCardDateFormat
      ),
      icon: <CalendarTodayOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "",
      value: props.projectDetail.location,
      icon: <FmdGoodOutlinedIcon sx={titleIcons} />,
    },
    {
      label: "",
      value: props.projectDetail.chargableType,
      icon: <WorkOutlineOutlinedIcon sx={titleIcons} />,
    },
  ];

  return (
    <div>
      <Marketmaintitle
        detailCardViewInfo={props.detailCardViewInfo}
        projectDetail={props.projectDetail}
        GetAllRequisitionByProjectCodeInfo={
          props.GetAllRequisitionByProjectCodeInfo
        }
        getProjectList={props.getProjectList}
        // fetchDetailsConfig = {props.fetchDetailsConfig}
        likeButtonUpdateCallback={props.likeButtonUpdateCallback}
      />
      <Marketsubtitle projectDetail={props.projectDetail} data={data1} />
      {/* <Marketsubtitle projectDetail={props.projectDetail} data={data2} /> */}
    </div>
  );
};

export default Marketplacetitle;
