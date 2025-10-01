import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";
import { jobCode } from "../../activeallocation/AllocationFilter/constant";

export const GetSxPropsForButton = (
  selectedView: string,
  selectedButton: string,
  view: string
) => {
  const sxProps: SxProps = {
    // backgroundColor: selectedView === view ? "#4f2d7f" : "",
    backgroundColor:
      selectedView === view ? GT_DESIGN_PARAMETERS.GtPrimaryColorPurple : "",
    // color: selectedButton === view ? "white" : "#4f2d7f",
    color:
      selectedButton === view
        ? "white"
        : GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    fontSize: "14px",
    // fontFamily: "GT Walsheim Pro",
    textTransform: "initial",
    borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
    borderRadius: view === "month" || view === "year" ? "30px" : "0px",
    "&:hover": {
      color: "#000",
      borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
      backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor,
    },
  };
  return sxProps;
};

export const GetSxPropsForButtonPortfolioBalancing = (
  selectedView: string,
  selectedButton: string,
  view: string
) => {
  const sxProps: SxProps = {
    // backgroundColor: selectedView === view ? "#4f2d7f" : "",
    backgroundColor:
      selectedView === view ? GT_DESIGN_PARAMETERS.GtPrimaryColorPurple : "",
    // color: selectedButton === view ? "white" : "#4f2d7f",
    color:
      selectedButton === view
        ? "white"
        : GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
    fontSize: "14px",
    // fontFamily: "GT Walsheim Pro",
    textTransform: "initial",
    borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
    borderRadius: view === "month" || view === "quater" ? "30px" : "0px",
    "&:hover": {
      color: "#000",
      borderColor: GT_DESIGN_PARAMETERS.GtColorPurple2,
      backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor,
    },
  };
  return sxProps;
};

export const PAGESIZE_FOR_EMPLOYEE_PORTFOLIO: number = 25;

export const TooltipDetailsSxPropsPortfolio: SxProps = {
  padding: "10px 10px 10px 10px",
  width: "100%",  
};

export const UserInfoTooltipColumnsSxPropsPortfolio: SxProps = {
  whiteSpace: "nowrap",
  textOverflow: "ellipsis",
  color: "black",
  fontWeight: "500",
  fontSize: "16px",
};

export const UserInfoTooltipValuesSxPropsPortfolio: SxProps = {
  color: "black",
  whiteSpace: "normal", // allows wrapping
  overflowWrap: "break-word", // ensures long words break
  textOverflow: "ellipsis", // optional, only works with nowrap
  overflow: "hidden", // hides overflow if needed
};

export const PortfolioReportHeader01 = {
  Name: "Name",
  Designation: "Designation",
  Grade: "Grade",
  Supercoach: "Super Coach",
  Cosupercoach: "Co-SuperCoach",
  Officelocation: "Office Location",
  Availablevsallocated: "AvailablevsAllocated",
  Clientgroup: "Client Group",
  Client: "Client",
  JobCode: "Job Code",
  JobName: "Job Name",
}

export const PortfolioReportHeader = [
  "Name",
  "Designation",
  "Grade",
  "Super Coach",
  "Co-SuperCoach",
  "Office Location",
  "AvailablevsAllocated",
  "Client Group",
  "Client",
  "Job Code",
  "Job Name"
];

export const PortfolioReportHeaderKey = [
  "name",
  "designation",
  "grade",
  "supercoach",
  "cosupercoach",
  "officelocation",
  "availablevsallocated",
  "clientgroup",
  "client",
  "jobcode",
  "jobname"
];