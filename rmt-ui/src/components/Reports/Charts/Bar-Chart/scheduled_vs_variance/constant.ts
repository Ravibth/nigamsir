import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../../../../global/constant";
import { RolesListMaster } from "../../../../../common/enums/ERoles";

export const Headings: SxProps = {
  color: "grey",
  alignItems: "center",
  fontWeight: "bold",
  fontSize: "16px",
  fontFamily: "GT Walsheim Pro ",
};

export const DataRows: SxProps = {
  alignItems: "center",
  color: "black",
  fontWeight: "bold",
  fontSize: "17px",
  fontFamily: "GT Walsheim Pro ",
};

export const SkillsChip: SxProps = {
  borderRadius: "10px", // Adjust the border radius as needed
  backgroundColor: "#ebeaf3", // Change the background color as needed
  border: "1px solid #4f2d7f",
  color: "#4f2d7f",
  fontWeight: "bold",
  margin: "4px",
};

export const PaperStyle: SxProps = {
  transform: "translateX(-50%)",
  boxShadow: "none",
  border: "1px solid rgba(216, 216, 216, 255)",
  backgroundColor: "rgba(238, 238, 238, 255)",
};

export const IconSxProps: SxProps = {
  mr: 0.5,
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
  ":disabled": { color: "#c5c2c2 !important" },
};

export const BtnGridContainerSXProps: SxProps = {
  display: "flex",
  justifyContent: "space-between",
};

export const defaultColDef = {
  lockVisible: true,
  resizable: true,
};

export const dateFormat = "YYYY-MM-DD";

export enum CHARTTYPE  {
  EmployeeView = "EmployeeView",
  SupercoachView= "SupercoachView",
  CoachSuperCoachView= "CoachSuperCoachView",
  LeaderView = "LeaderView",
}

export const CHART_VIEW_TYPE = [
  {
    "key": CHARTTYPE.EmployeeView,
    "title": "Employee View",
    "role": [RolesListMaster.Employee],
    "defaultSelect": true
  },
  {
    "key": CHARTTYPE.SupercoachView,
    "title": "Supercoach View",
    "role": [RolesListMaster.SuperCoach],
    "defaultSelect": false 
  },
  {
    "key": CHARTTYPE.CoachSuperCoachView,
    "title": "Co Supercoach View",
    "role": [RolesListMaster.CSC],
    "defaultSelect": false
  },
  {
    "key": CHARTTYPE.LeaderView,
    "title": "Leader View",
    "role": [RolesListMaster.Leaders, RolesListMaster.SystemAdmin, RolesListMaster.CEOCOO],
    "defaultSelect": false
  }  
]


export const ROLE_VIEW_TYPE = [
  {
    "key": GlobalConstant.Role.Employee,    
    "role": [],    
  },
  {
    "key": GlobalConstant.Role.Leaders,    
    "role": [RolesListMaster.Leaders, RolesListMaster.SystemAdmin, RolesListMaster.CEOCOO, RolesListMaster.Admin],    
  },
    {
    "key": GlobalConstant.Role.SuperCoach,    
    "role": [RolesListMaster.SuperCoach],    
  }    
]