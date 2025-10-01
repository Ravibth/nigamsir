import { SxProps } from "@mui/material";

export const filterIconButton: SxProps = () => {
  return {
    color: "#4f2d7f",
    fontSize: "14px",
    textTransform: "initial",
    borderRadius: "20px",
    borderColor: "#B8B8B8",
    height: "35px",
  };
};

export interface IFilterParameters {
  key: string;
  label: string;
  isAlwaysVisible?: boolean;
  isVisible: boolean;
  filterType: EFilterType;
  options?: string[];
  min?: number;
  max?: number;
}

export enum RadioButtonsFilterOptions {
  DEFAULT = "Default",
  BASE = "Base",
  PREFERRED = "Preferred",
}

export enum EFilterType {
  Textfield = "Textfield",
  Radio = "Radio",
  Slider = "Slider",
}

export enum ESystemSuggestionParameters {
  availability = "availability",
  marketplace = "marketplace",
  location = "Location",
  business_unit = "business_unit",
  industry = "Industry",
  sub_industry = "Sub_Industry",
  same_client = "Same_client",
  skills = "Skills",
  competency = "Competency",
  offerings = "Offerings",
  solutions = "Solutions",
}

export const DefaultFilterParameters: IFilterParameters[] = [
  {
    key: ESystemSuggestionParameters.availability,
    label: "Available",
    isAlwaysVisible: true,
    isVisible: true,
    filterType: EFilterType.Textfield,
    options: ["Yes", "No"],
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.marketplace,
    label: "Marketplace Interest",
    isVisible: true,
    isAlwaysVisible: true,
    filterType: EFilterType.Textfield,
    options: ["Yes", "No"],
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.location,
    label: "Location",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.business_unit,
    label: "Business Unit",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.industry,
    label: "Industry",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.sub_industry,
    label: "Sub Industry",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.same_client,
    label: "Experience Working With Same Client",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.skills,
    label: "Additional Skills",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.competency,
    label: "Competency",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.offerings,
    label: "Offerings",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
  {
    key: ESystemSuggestionParameters.solutions,
    label: "Solutions",
    isVisible: false,
    filterType: EFilterType.Slider,
    min: 0,
    max: 10,
  },
];
