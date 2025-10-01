import { SxProps } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../../global/constant";
import { IAllocateFormSkills } from "../common-allocation-modal-form/utils";

export enum ECommonAllocationFilterControl {
  location = "location",
  skills = "skills",
  competency = "competency",
  businessUnit = "businessUnit",
}

export interface ICommonAllocationFilterControl {
  location: string[];
  skills: string[];
  competency: string[];
  businessUnit: string[];
}

export interface ICommonMarketPlaceFilterControl {
  buFiltervalue: string[];
  offeringsFiltervalue: string[];
  solutionsFiltervalue: string[];
  industryFiltervalue: string[];
  subIndustryFiltervalue: string[];
  startDateFiltervalue: Date;
  endDateFiltervalue: Date;
  locationFiltervalue: string[];
  isAllocatedFiltervalue: string[];
}

export const DefaultCommonMarketPlaceFillerControlValues: ICommonMarketPlaceFilterControl =
  {
    buFiltervalue: [],
    offeringsFiltervalue: [],
    solutionsFiltervalue: [],
    industryFiltervalue: [],
    subIndustryFiltervalue: [],
    startDateFiltervalue: null,
    endDateFiltervalue: null,
    locationFiltervalue: [],
    isAllocatedFiltervalue: [],
  };

export interface ICommonAllocationFilterOptions {
  location: string[];
  skills: IAllocateFormSkills[];
  competency: string[];
  businessUnit: string[];
}

export const DefaultCommonAllocationFillerControlValues: ICommonAllocationFilterControl =
  {
    location: [],
    skills: [],
    competency: [],
    businessUnit: [],
  };

export const filterIconButton: SxProps = {
  color: GT_DESIGN_PARAMETERS.GTTealColor,
  fontSize: "14px",
  textTransform: "initial",
  borderRadius: "40px",
  border: "2px solid",
  borderColor: GT_DESIGN_PARAMETERS.GTTealColor + " !important",
  "&:hover": {
    borderColor: GT_DESIGN_PARAMETERS.GTTealColor + " !important",
    border: "2px solid",
    backgroundColor: "#E9F7FB !important",
  },
};
export const DividerSxProps: SxProps = {
  borderBottomWidth: 2,
  margin: "10px",
};

export const FontStyle: SxProps = {
  fontFamily: GT_DESIGN_PARAMETERS.GtFontFamily,
};
