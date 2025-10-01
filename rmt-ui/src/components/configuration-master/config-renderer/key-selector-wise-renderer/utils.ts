import { ETextFieldRendererControlTypes } from "../../../../common/interfaces/IConfigurationMaster";

export enum BuWiseFormControls {
  BusinessUnit = "businessUnitFormControl",
  Offerings = "offeringsFormControl",
}

export enum GridColumnDefsControl {
  Offering = "offering",
}

export const getAgGridFilterType = (controlType: string) => {
  switch (controlType) {
    case ETextFieldRendererControlTypes.INTEGER:
      return "";
    default:
      return "agTextColumnFilter";
  }
};
