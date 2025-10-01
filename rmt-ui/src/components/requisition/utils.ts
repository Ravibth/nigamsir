import { SxProps } from "@mui/material";
import { IProjectMaster } from "../../common/interfaces/IProject";
import {
  IRequisitionMaster,
  IRequisitionParametersMaster,
} from "../../common/interfaces/IRequisition";
import { ISkillsMaster } from "../../common/interfaces/ISkillsMaster";
import { ERequisitionParameters } from "../system-suggestions/requisition-form-system-suggestions/suggestions-grid-view/suggestions-grid-view";
import { IDesignationMaster } from "../../common/interfaces/IDesignationMaster";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { IIndustryMaster } from "../../common/interfaces/IIndustryMaster";
import { IWCGTLocationMaster } from "../../common/interfaces/IWCGTLocationMaster";
import {
  EConfigurationConfigGroup,
  getDesignationList,
  GetExpertiesConfigurationByExpertiesNameAndConfigGroup,
  getLocationList,
  getSectorIndustryList,
} from "../../services/configuration-services/configuration.service";
import { getSkillMaster } from "../../services/skills/skill.service";
import { getAllCompetency } from "../../services/wcgt-master-services/wcgt-master-services";
import { GetNewDateWithNoonTimeZone } from "../../utils/date/dateHelper";
import { checkProjectAllocationStartDateWithCurrentDate } from "../common-allocation/utils";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";

export const ParameterDefaultValues = (checkboxOptions: ICheckboxOption[]) => {
  const defaultValues: any = {};
  checkboxOptions.forEach((item: ICheckboxOption) => {
    defaultValues[item.name] = 1;
  });
  return defaultValues;
};

export interface IGlobalOptionsForParameters {
  designation: IDesignationMaster[];
  competency: ICompetencyMaster[];
  skills: ISkillsMaster[];
  industry: IIndustryMaster[];
  location: IWCGTLocationMaster[];
  industrySubIndustryMaster: IIndustryMaster[];
}

export interface ICheckboxOption {
  name: string;
  label: string;
}

export enum SkillsGroupByType {
  Mandatory = "Expertise skills (These are mandatory for requisition)",
  NiceToHave = "Additional Optional Skills",
}

export interface IResourceWiseParameterOptions {
  skills: IRequisitionSkillOptions[];
  subIndustry: IRequisitionSubIndustry[]; //Check for master
}

export interface IRequisitionSkillOptions extends ISkillsMaster {
  type: string;
}

export enum EBaseRequisitionFormMainControlForm {
  designation = "designation",
  grade = "grade",
  description = "description",
  allDetails = "allDetails",
  businessUnit = "businessUnit",
  offerings = "offerings",
  solutions = "solutions",
  numberOfResources = "numberOfResources",
  allResourcesHaveSimilarDetails = "allResourcesHaveSimilarDetails",
  competency = "competency",
  skills = "skills",
  startDate = "startDate",
  endDate = "endDate",
  noOfHours = "noOfHours",
  isPerDayHourAllocation = "isPerDayHourAllocation",
  location = "location",
  industry = "industry",
  subIndustry = "subIndustry",
  SameClient = "Same_client",
}

export const RequisitionParametersPreferenceOrder = [
  ERequisitionParameters.competency,
  ERequisitionParameters.Skills,
  ERequisitionParameters.offerings,
  ERequisitionParameters.Industry,
  ERequisitionParameters.Location,
  ERequisitionParameters.solutions,
  ERequisitionParameters.Sub_Industry,
  ERequisitionParameters.Same_client,
];

export interface IRequisitionLocations {
  location: string;
}
export interface IRequisitionIndustry {
  industry: string;
  industryId: string;
}
export interface IRequisitionSubIndustry {
  subIndustry: string;
  subIndustryId: string;
}

export interface IRequisitionFormMainDetails {
  [EBaseRequisitionFormMainControlForm.designation]: IDesignationMaster;
  [EBaseRequisitionFormMainControlForm.description]: string;
  [EBaseRequisitionFormMainControlForm.businessUnit]: string;
  [EBaseRequisitionFormMainControlForm.offerings]: string;
  [EBaseRequisitionFormMainControlForm.solutions]: string;
  [EBaseRequisitionFormMainControlForm.numberOfResources]: number;
  [EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails]: boolean;
  [EBaseRequisitionFormMainControlForm.allDetails]: IRequisitionResourcesDetails[];
}

export interface IRequisitionResourcesDetails {
  [EBaseRequisitionFormMainControlForm.startDate]: Date;
  [EBaseRequisitionFormMainControlForm.endDate]: Date;
  [EBaseRequisitionFormMainControlForm.noOfHours]: number;
  [EBaseRequisitionFormMainControlForm.isPerDayHourAllocation]: boolean;
  [EBaseRequisitionFormMainControlForm.competency]: ICompetencyMaster;
  [EBaseRequisitionFormMainControlForm.location]: IWCGTLocationMaster[];
  [EBaseRequisitionFormMainControlForm.skills]: IRequisitionSkillOptions[];
  [EBaseRequisitionFormMainControlForm.industry]: IIndustryMaster;
  [EBaseRequisitionFormMainControlForm.subIndustry]: IRequisitionSubIndustry;
  parameters: IRequisitionParametersMaster[];
}

export enum RequisitionSnackbarMessagesAndLabels {
  RequisitionCreatedSuccessfully = "Requisition created successfully.",
  RequisitionCreationError = "Error in creating new requisition!",
  RequisitionUpdateSuccessfully = "Requisition updated successfully",
  ErrorFetchingDetails = "Error fetching Details",
  RequisitionUpdateError = "Error in updating Requisition",
  UpdateRequisition = "Update Requisition",
  CreateRequisition = "Create Requisition",
  BulkUpload = "Bulk Upload",
  DownloadTemplate = "Download Template",
  StartDate = "Start Date",
  EndDate = "End Date",
  Location = "Location",
  Back = "BACK",
  SkillError = "Please select required Expertise skills  for the requisition.",
  ResourceDetailsError = "Some of the resource details are missing, please review the form.",
  Skills = "Skills",
  GoodToHave = "Good to have",
  ResourceEffortHoursError = "Please enter valid effort hours for the requisition.",
}

export const ResourceSpecificationHeader: SxProps = {
  fontWeight: "600 !important",
  fontSize: "x-large",
};

export const getExistingRequisitionResourceEntry = (
  optionsMaster: IGlobalOptionsForParameters,
  requisitionDetails: IRequisitionMaster
): IRequisitionResourcesDetails => {
  const matchedLocationItem = getMatchingLocation(
    optionsMaster.location,
    requisitionDetails.requisitionParameterValues
      ?.filter((a) => a?.parameter === ERequisitionParameters.Location)
      .map((a) => a.value)
  );
  const matchedIndustryItem = getMatchingIndustry(
    optionsMaster.industry,
    requisitionDetails.requisitionParameterValues
      ?.filter((a) => a?.parameter === ERequisitionParameters.Industry)
      .find((a) => a)?.value
  );

  const matchedSubIndustryItem = getMatchingSubIndustry(
    optionsMaster.industry,
    requisitionDetails.requisitionParameterValues
      ?.filter(
        (a) =>
          a?.parameter === ERequisitionParameters.Sub_Industry ||
          a?.parameter?.toLowerCase() === "subindustry"
      )
      .find((a) => a)?.value
  );

  const matchedCompetencyItem = getMatchingCompetency(
    optionsMaster.competency,
    requisitionDetails.competency
  );

  const newRequisitionResourceEntry: IRequisitionResourcesDetails = {
    parameters: requisitionDetails.requisitionParameters,
    [EBaseRequisitionFormMainControlForm.startDate]:
      requisitionDetails.startDate,
    [EBaseRequisitionFormMainControlForm.endDate]: GetNewDateWithNoonTimeZone(
      requisitionDetails.endDate
    ),
    [EBaseRequisitionFormMainControlForm.noOfHours]:
      requisitionDetails.effortsPerDay,
    [EBaseRequisitionFormMainControlForm.competency]: matchedCompetencyItem,
    [EBaseRequisitionFormMainControlForm.location]: matchedLocationItem,
    [EBaseRequisitionFormMainControlForm.skills]:
      requisitionDetails.requisitionSkill,
    [EBaseRequisitionFormMainControlForm.isPerDayHourAllocation]:
      requisitionDetails.isPerDayHourAllocation,
    [EBaseRequisitionFormMainControlForm.industry]: matchedIndustryItem,
    [EBaseRequisitionFormMainControlForm.subIndustry]: matchedSubIndustryItem,
  };
  return newRequisitionResourceEntry;
};

export const getFreshNewRequisitionResourceEntry = (
  defaultParameterPriorityTemp: any,
  optionsMaster: IGlobalOptionsForParameters,
  projectInfo: IProjectMaster,
  parameterOptions: ICheckboxOption[]
): IRequisitionResourcesDetails => {
  const matchedLocationItem = getMatchingLocation(optionsMaster.location, [
    projectInfo?.location,
  ]);
  const matchedIndustryItem = getMatchingIndustry(
    optionsMaster.industry,
    projectInfo?.industry
  );

  const matchedSubIndustryItem = getMatchingSubIndustry(
    optionsMaster.industry,
    projectInfo?.subindustry
  );

  const newRequisitionResourceEntry: IRequisitionResourcesDetails = {
    parameters: GetRequisitionParameterFormData(
      parameterOptions,
      defaultParameterPriorityTemp
    ),
    [EBaseRequisitionFormMainControlForm.startDate]:
      checkProjectAllocationStartDateWithCurrentDate(projectInfo.startDate),
    [EBaseRequisitionFormMainControlForm.endDate]: GetNewDateWithNoonTimeZone(
      projectInfo.endDate
    ),
    [EBaseRequisitionFormMainControlForm.noOfHours]: 1,
    [EBaseRequisitionFormMainControlForm.competency]: null,
    [EBaseRequisitionFormMainControlForm.location]: matchedLocationItem,
    [EBaseRequisitionFormMainControlForm.skills]: [],
    [EBaseRequisitionFormMainControlForm.isPerDayHourAllocation]: false,
    [EBaseRequisitionFormMainControlForm.industry]: matchedIndustryItem,
    [EBaseRequisitionFormMainControlForm.subIndustry]: matchedSubIndustryItem,
  };
  return newRequisitionResourceEntry;
};

export const GetRequisitionParameterFormData = (
  parameterList: ICheckboxOption[],
  defaultParameterPriorityTemp: any
): IRequisitionParametersMaster[] => {
  if (defaultParameterPriorityTemp) {
    const finalParameters: IRequisitionParametersMaster[] = parameterList.map(
      (parameter) => {
        const defaultWeight = Object.keys(defaultParameterPriorityTemp).find(
          (item) => item.toLowerCase() === parameter.name.toLowerCase()
        );

        return {
          id: "",
          requisitionId: "",
          category: parameter.name,
          requisitionWeight: defaultWeight
            ? defaultParameterPriorityTemp[defaultWeight]
            : 0,
          isChecked: true,
        };
      }
    );
    return finalParameters;
  }
  return [];
};

export interface IParametersSelectionConfigs {
  category: ERequisitionParameters;
  multipleSelectionAllowed: boolean;
  isChangeAllowed: boolean;
  controlName: EBaseRequisitionFormMainControlForm;
  label: string;
  getOptionLabel: (e: any) => string;
  isOptionEqualToValue: (option: any, value: any) => boolean;
  xsSpace: number;
  maxSliderValue: number;
  isMandatory: boolean;
  groupBy: (option) => any;
  defaultValue: string | any[];
  sortBy: (option) => any;
  infoIconMessage: string;
  highlightDifferentValuesDifferently: boolean;
  propertyToHighlightDifferently: string;
  propertyValueToHighlightDifferently: string;
}

export const ParametersSelectionConfigs: IParametersSelectionConfigs[] = [
  {
    category: ERequisitionParameters.competency,
    isChangeAllowed: true,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.competency,
    label: "Competency",
    getOptionLabel: (option: any) => option?.competency as string,
    xsSpace: 2,
    maxSliderValue: 10,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: true,
    defaultValue: "",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option?.competency === value?.competency;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },
  {
    category: ERequisitionParameters.Skills,
    isChangeAllowed: true,
    multipleSelectionAllowed: true,
    controlName: EBaseRequisitionFormMainControlForm.skills,
    label: "Skills",
    getOptionLabel: (option: any) =>
      `${option?.skillName} (${option?.skillCode})`,
    xsSpace: 6,
    maxSliderValue: 9,
    groupBy: function (option: any) {
      return option.type;
    },
    isMandatory: true,
    defaultValue: [],
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option?.skillName === value?.skillName;
    },
    sortBy: function (option: any) {
      return option?.sort((a, b) =>
        `${a.skillName}` > `${b.skillName}` ? 1 : -1
      );
    },
    infoIconMessage:
      "Add Competency specific skills which are mandatory for Requisition & any additional skills which would be non-mandatory.",
    highlightDifferentValuesDifferently: true,
    propertyToHighlightDifferently: "type",
    propertyValueToHighlightDifferently: SkillsGroupByType.NiceToHave,
  },
  {
    category: ERequisitionParameters.offerings,
    isChangeAllowed: false,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.offerings,
    label: "Offerings",
    getOptionLabel: (e: any) => e as string,
    xsSpace: 2,
    maxSliderValue: 9,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: true,
    defaultValue: "",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option === value;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },
  {
    category: ERequisitionParameters.Industry,
    isChangeAllowed: true,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.industry,
    label: "Industry",
    getOptionLabel: (e: any) => e?.industry_name as string,
    xsSpace: 2,
    maxSliderValue: 10,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: false,
    defaultValue: "",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option?.industry_name === value?.industry_name;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },
  {
    category: ERequisitionParameters.Location,
    isChangeAllowed: true,
    multipleSelectionAllowed: true,
    controlName: EBaseRequisitionFormMainControlForm.location,
    label: "Location",
    getOptionLabel: (e: any) => {
      return e?.location_name as string;
    },
    xsSpace: 2,
    maxSliderValue: 10,
    groupBy: function (option: any) {
      return null;
    },
    isMandatory: false,
    defaultValue: [],
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option?.location_name === value?.location_name;
    },
    sortBy: function (option: any) {
      return option?.sort((a, b) =>
        `${a.location_name}` > `${b.location_name}` ? 1 : -1
      );
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },
  {
    category: ERequisitionParameters.solutions,
    isChangeAllowed: false,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.solutions,
    label: "Solutions",
    getOptionLabel: (e: any) => e as string,
    xsSpace: 2,
    maxSliderValue: 9,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: true,
    defaultValue: "",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option === value;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },

  {
    category: ERequisitionParameters.Sub_Industry,
    isChangeAllowed: true,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.subIndustry,
    label: "Sub Industry",
    getOptionLabel: (e: any) => e?.subIndustry as string,
    xsSpace: 2,
    maxSliderValue: 10,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: false,
    defaultValue: "",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option?.subIndustry === value?.subIndustry;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },

  {
    category: ERequisitionParameters.Same_client,
    isChangeAllowed: false,
    multipleSelectionAllowed: false,
    controlName: EBaseRequisitionFormMainControlForm.SameClient,
    label: "Same Client Experience",
    getOptionLabel: (e: any) => e as string,
    xsSpace: 2,
    maxSliderValue: 10,
    groupBy: function (option: any) {
      return option;
    },
    isMandatory: false,
    defaultValue: "Yes",
    isOptionEqualToValue: function (option: any, value: any): boolean {
      return option === value;
    },
    sortBy: function (option: any) {
      return 1;
    },
    infoIconMessage: "",
    highlightDifferentValuesDifferently: false,
    propertyToHighlightDifferently: "",
    propertyValueToHighlightDifferently: "",
  },
];

export const validateSkill = (finalSkills: IRequisitionSkillOptions[]) => {
  if (
    finalSkills &&
    finalSkills.length > 0 &&
    finalSkills.find((item) => item.type === SkillsGroupByType.Mandatory)
  ) {
    return true;
  } else {
    return false;
  }
};

export const getAllDesignationMaster = (): Promise<IDesignationMaster[]> => {
  return new Promise((resolve, reject) => {
    getDesignationList()
      .then((options: any) => {
        resolve(options.data);
      })
      .catch(() => {
        resolve([]);
      });
  });
};

export const getAllLocationMaster = (): Promise<IWCGTLocationMaster[]> => {
  return new Promise((resolve, reject) => {
    getLocationList()
      .then((options: any) => {
        resolve(options.data);
      })
      .catch(() => {
        resolve([]);
      });
  });
};

export const getAllIndustryMaster = (): Promise<any[]> => {
  return new Promise((resolve, reject) => {
    getSectorIndustryList()
      .then((options: any) => {
        resolve(options.data);
      })
      .catch(() => {
        resolve([]);
      });
  });
};

export const getAllSkillMaster = (): Promise<ISkillsMaster[]> => {
  return new Promise((resolve, reject) => {
    getSkillMaster()
      .then((options: any) => {
        const activeSkill = options?.data?.filter(
          (skill) => skill?.isEnable && skill.isActive
        );
        resolve(activeSkill);
      })
      .catch(() => {
        resolve([]);
      });
  });
};

export const getAllCompetencyMaster = (): Promise<ICompetencyMaster[]> => {
  return new Promise((resolve, reject) => {
    getAllCompetency()
      .then((options: any) => {
        const modifiedCompetencyMaster: ICompetencyMaster[] = options.filter(
          (item: ICompetencyMaster) => item.isActive
        );
        resolve(modifiedCompetencyMaster);
      })
      .catch(() => {
        resolve([]);
      });
  });
};

export const fetchConfigDefaultWeightage = async (
  buOffering: string,
  defaultWeightageValuesBasicTemp: any
): Promise<any> => {
  const fetchedConfigs: any =
    await GetExpertiesConfigurationByExpertiesNameAndConfigGroup(
      buOffering,
      EConfigurationConfigGroup.RequisitionForm
    );

  const configs: any = {};
  fetchedConfigs.data.forEach((item: any) => {
    configs[item.configurationGroup.configKey] = parseInt(item.attributeValue);
  });
  const tempDefaultParametersPriority: any = {};
  if (defaultWeightageValuesBasicTemp) {
    Object.keys(defaultWeightageValuesBasicTemp).forEach((item: any) => {
      tempDefaultParametersPriority[item] =
        configs[ERequisitionParameters[item]] ||
        configs[ERequisitionParameters[item]] === 0
          ? configs[ERequisitionParameters[item]]
          : 1;
    });
  }

  return tempDefaultParametersPriority;
};

export const getMatchingLocation = (
  options: IWCGTLocationMaster[],
  valueToCheck: string[]
): IWCGTLocationMaster[] => {
  return options.filter((item) =>
    valueToCheck.some(
      (value) => value.toLowerCase() === item?.location_name?.toLowerCase()
    )
  );
};

export const getMatchingIndustry = (
  options: IIndustryMaster[],
  valueToCheck: string
): IIndustryMaster => {
  return options.find(
    (item) => item?.industry_name?.toLowerCase() === valueToCheck?.toLowerCase()
  );
};
export const getMatchingSubIndustry = (
  options: IIndustryMaster[],
  valueToCheck: string
): IRequisitionSubIndustry => {
  const industryItemFound = options.find(
    (item) =>
      item?.sub_industry_name?.toLowerCase() === valueToCheck?.toLowerCase()
  );
  if (industryItemFound) {
    return {
      subIndustry: industryItemFound.sub_industry_name,
      subIndustryId: industryItemFound.sub_industry_id,
    };
  }
};
export const getMatchingCompetency = (
  options: ICompetencyMaster[],
  valueToCheck: string
): ICompetencyMaster => {
  return options.find(
    (item) => item?.competency?.toLowerCase() === valueToCheck?.toLowerCase()
  );
};

export interface INewRequisitionSubmissionPayload {
  pipelineCode: string;
  jobCode: string;
  pipelineName: string;
  jobName: string;
  bU: string;
  designation: string;
  grade: string;
  designationId: string;
  numberOfResources: number;
  offerings: string;
  solutions: string;
  isAllResourcesSimilar: boolean;
  resourceEntities: any;
  description: string;
  clientName: string;
}

export interface IUpdateRequisitionSubmissionPayload {
  resourceEntities: {
    id: string;
    effort_Hrs: number;
    startDate: Date;
    endDate: Date;
    isPerDayHourAllocation: boolean;
    businessUnit: string;
    competency: string;
    competencyId: string;
    offerings: string;
    solutions: string;
    designation: string;
    description: string;
    grade: string;
    parameters: IParametersEntities[];
    skills: IRequisitionSkillOptions[];
    locations: IRequisitionParameterValueEntities[];
    industries: IRequisitionParameterValueEntities[];
    subIndustries: IRequisitionParameterValueEntities[];
  }[];
}

export interface IRequisitionEntityPayload {
  competency: string;
  competencyId: string;
  startDate: Date;
  endDate: Date;
  effort_Hrs: number;
  isPerDayHourAllocation: boolean;
  parameters: IParametersEntities[];
  skills: IRequisitionSkillOptions[];
  locations: IRequisitionParameterValueEntities[];
  industries: IRequisitionParameterValueEntities[];
  subIndustries: IRequisitionParameterValueEntities[];
}

export interface IRequisitionParameterValueEntities {
  label: string;
}
export interface IParametersEntities {
  name: string;
  value: number;
  isChecked: boolean;
}

export const finalParameterEntityByParameter = (
  allConditionsFulfilled: boolean,
  parameter: IRequisitionParametersMaster
) => {
  if (allConditionsFulfilled) {
    return {
      name: parameter.category,
      value: parameter.requisitionWeight,
      isChecked: parameter.requisitionWeight > 0 ? true : false,
    };
  } else {
    return {
      name: parameter.category,
      value: 0,
      isChecked: false,
    };
  }
};

export const getParametersWithWeightage = (
  resourceItemDetails: IRequisitionResourcesDetails
): IParametersEntities[] => {
  const finalParameters: IParametersEntities[] = [];
  resourceItemDetails.parameters.forEach((parameter) => {
    let isParameterConditionSatisfied: boolean = false;

    switch (parameter.category) {
      case ERequisitionParameters.competency:
        isParameterConditionSatisfied = resourceItemDetails[
          EBaseRequisitionFormMainControlForm.competency
        ]
          ? true
          : false;
        break;
      case ERequisitionParameters.Skills:
        isParameterConditionSatisfied =
          resourceItemDetails[EBaseRequisitionFormMainControlForm.skills]
            .length > 0
            ? true
            : false;
        break;

      case ERequisitionParameters.Industry:
        isParameterConditionSatisfied = resourceItemDetails[
          EBaseRequisitionFormMainControlForm.industry
        ]
          ? true
          : false;
        break;
      case ERequisitionParameters.Sub_Industry:
        isParameterConditionSatisfied = resourceItemDetails[
          EBaseRequisitionFormMainControlForm.subIndustry
        ]
          ? true
          : false;
        break;
      case ERequisitionParameters.Location:
        isParameterConditionSatisfied =
          resourceItemDetails[EBaseRequisitionFormMainControlForm.location]
            .length > 0
            ? true
            : false;
        break;
      case ERequisitionParameters.offerings:
        isParameterConditionSatisfied = true;
        break;
      case ERequisitionParameters.solutions:
        isParameterConditionSatisfied = true;
        break;
      case ERequisitionParameters.Same_client:
        isParameterConditionSatisfied = true;
        break;
      default:
        break;
    }

    if (isParameterConditionSatisfied) {
      finalParameters.push(finalParameterEntityByParameter(true, parameter));
    } else {
      finalParameters.push(finalParameterEntityByParameter(false, parameter));
    }
  });
  return finalParameters;
};

export const getResourceEntitiesPayload = (
  formData: IRequisitionFormMainDetails
): IRequisitionEntityPayload[] => {
  const resourceEntitiesToMap =
    formData[
      EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
    ] && formData[EBaseRequisitionFormMainControlForm.allDetails].length > 0
      ? [formData[EBaseRequisitionFormMainControlForm.allDetails][0]]
      : formData[EBaseRequisitionFormMainControlForm.allDetails];

  return resourceEntitiesToMap.map((item) => {
    const locations: IRequisitionParameterValueEntities[] = item[
      EBaseRequisitionFormMainControlForm.location
    ].map((locationItem) => {
      return {
        label: locationItem.location_name,
      };
    });

    return {
      competency:
        item[EBaseRequisitionFormMainControlForm.competency]?.competency,
      competencyId:
        item[EBaseRequisitionFormMainControlForm.competency]?.competencyId,
      startDate: item[EBaseRequisitionFormMainControlForm.startDate],
      endDate: item[EBaseRequisitionFormMainControlForm.endDate],
      effort_Hrs: item[EBaseRequisitionFormMainControlForm.noOfHours],
      isPerDayHourAllocation:
        item[EBaseRequisitionFormMainControlForm.isPerDayHourAllocation],
      parameters: getParametersWithWeightage(item),
      skills: item[EBaseRequisitionFormMainControlForm.skills],
      locations: locations,
      industries: item[EBaseRequisitionFormMainControlForm.industry]
        ? [
            {
              label:
                item[EBaseRequisitionFormMainControlForm.industry]
                  .industry_name,
            },
          ]
        : [],
      subIndustries: item[EBaseRequisitionFormMainControlForm.subIndustry]
        ? [
            {
              label:
                item[EBaseRequisitionFormMainControlForm.subIndustry]
                  .subIndustry,
            },
          ]
        : [],
    };
  });
};

export const getSubmissionPayloadForNewRequisition = (
  formData: IRequisitionFormMainDetails,
  projectInfo: IProjectMaster
): INewRequisitionSubmissionPayload => {
  return {
    pipelineCode: projectInfo?.pipelineCode,
    jobCode: projectInfo?.jobCode,
    pipelineName: projectInfo?.pipelineName,
    jobName: projectInfo?.jobName,
    bU: projectInfo?.bu,
    designation:
      formData[EBaseRequisitionFormMainControlForm.designation]
        .designation_name,
    grade: formData[EBaseRequisitionFormMainControlForm.designation].grade,
    designationId:
      formData[EBaseRequisitionFormMainControlForm.designation].designation_id,
    numberOfResources:
      formData[EBaseRequisitionFormMainControlForm.numberOfResources],
    offerings: projectInfo?.offerings,
    solutions: projectInfo?.solutions,
    isAllResourcesSimilar:
      formData[
        EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
      ],
    resourceEntities: getResourceEntitiesPayload(formData),
    description: formData[EBaseRequisitionFormMainControlForm.description],
    clientName: projectInfo?.clientName,
  };
};

export const getSubmissionPayloadForUpdateRequisition = (
  formData: IRequisitionFormMainDetails,
  projectInfo: IProjectMaster,
  requisitionId: string
): IUpdateRequisitionSubmissionPayload => {
  if (formData[EBaseRequisitionFormMainControlForm.allDetails].length > 0) {
    const resourceItem =
      formData[EBaseRequisitionFormMainControlForm.allDetails][0];
    const locations: IRequisitionParameterValueEntities[] = resourceItem[
      EBaseRequisitionFormMainControlForm.location
    ].map((locationItem) => {
      return {
        label: locationItem.location_name,
      };
    });
    return {
      resourceEntities: [
        {
          id: requisitionId,
          businessUnit: projectInfo?.bu,
          designation:
            formData[EBaseRequisitionFormMainControlForm.designation]
              .designation_name,
          grade:
            formData[EBaseRequisitionFormMainControlForm.designation].grade,
          offerings: projectInfo?.offerings,
          solutions: projectInfo?.solutions,
          description:
            formData[EBaseRequisitionFormMainControlForm.description],
          effort_Hrs:
            resourceItem[EBaseRequisitionFormMainControlForm.noOfHours],
          startDate:
            resourceItem[EBaseRequisitionFormMainControlForm.startDate],
          endDate: resourceItem[EBaseRequisitionFormMainControlForm.endDate],
          isPerDayHourAllocation:
            resourceItem[
              EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
            ],
          competency:
            resourceItem[EBaseRequisitionFormMainControlForm.competency]
              ?.competency,
          competencyId:
            resourceItem[EBaseRequisitionFormMainControlForm.competency]
              .competencyId,
          // status: "",
          parameters: getParametersWithWeightage(resourceItem),
          skills: resourceItem[EBaseRequisitionFormMainControlForm.skills],
          locations: locations,
          industries: resourceItem[EBaseRequisitionFormMainControlForm.industry]
            ? [
                {
                  label:
                    resourceItem[EBaseRequisitionFormMainControlForm.industry]
                      .industry_name,
                },
              ]
            : [],
          subIndustries: resourceItem[
            EBaseRequisitionFormMainControlForm.subIndustry
          ]
            ? [
                {
                  label:
                    resourceItem[
                      EBaseRequisitionFormMainControlForm.subIndustry
                    ].subIndustry,
                },
              ]
            : [],
        },
      ],
    };
  } else {
    return null;
  }
};

const compareDates = (date1: Date, date2: Date) =>
  new Date(date1).getTime() === new Date(date2).getTime();

export const checkIfRequisitionIsSameAsBefore = (
  requisitionInitialEntry: IRequisitionFormMainDetails,
  updatedRequisitionEntry: IRequisitionFormMainDetails
): boolean => {
  const resp =
    requisitionInitialEntry[EBaseRequisitionFormMainControlForm.designation]
      .designation_name ===
      updatedRequisitionEntry[EBaseRequisitionFormMainControlForm.designation]
        .designation_name &&
    requisitionInitialEntry[
      EBaseRequisitionFormMainControlForm.description
    ]?.trim() ===
      updatedRequisitionEntry[
        EBaseRequisitionFormMainControlForm.description
      ]?.trim() &&
    requisitionInitialEntry[
      EBaseRequisitionFormMainControlForm.businessUnit
    ] ===
      updatedRequisitionEntry[
        EBaseRequisitionFormMainControlForm.businessUnit
      ] &&
    requisitionInitialEntry[EBaseRequisitionFormMainControlForm.offerings] ===
      updatedRequisitionEntry[EBaseRequisitionFormMainControlForm.offerings] &&
    requisitionInitialEntry[EBaseRequisitionFormMainControlForm.solutions] ===
      updatedRequisitionEntry[EBaseRequisitionFormMainControlForm.solutions] &&
    requisitionInitialEntry[EBaseRequisitionFormMainControlForm.allDetails]
      .length ===
      updatedRequisitionEntry[EBaseRequisitionFormMainControlForm.allDetails]
        .length &&
    requisitionInitialEntry[
      EBaseRequisitionFormMainControlForm.allDetails
    ].every((initialDetails) =>
      updatedRequisitionEntry[
        EBaseRequisitionFormMainControlForm.allDetails
      ].some((updatedDetails) =>
        checkIfObjectsAreEqualForRequisition(initialDetails, updatedDetails)
      )
    );
  return resp;
};

const checkIfObjectsAreEqualForRequisition = (
  requisitionAllDetailsInitial: IRequisitionResourcesDetails,
  updatedRequisitionAllDetails: IRequisitionResourcesDetails
): boolean => {
  return (
    compareDates(
      requisitionAllDetailsInitial[
        EBaseRequisitionFormMainControlForm.startDate
      ],
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.startDate
      ]
    ) &&
    compareDates(
      requisitionAllDetailsInitial[EBaseRequisitionFormMainControlForm.endDate],
      updatedRequisitionAllDetails[EBaseRequisitionFormMainControlForm.endDate]
    ) &&
    requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.noOfHours
    ] ===
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.noOfHours
      ] &&
    requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
    ] ===
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.isPerDayHourAllocation
      ] &&
    requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.competency
    ].competency
      ?.toLowerCase()
      .trim() ===
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.competency
      ].competency
        ?.toLowerCase()
        .trim() &&
    requisitionAllDetailsInitial[EBaseRequisitionFormMainControlForm.location]
      .length ===
      updatedRequisitionAllDetails[EBaseRequisitionFormMainControlForm.location]
        .length &&
    requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.location
    ].every((initialLocation) =>
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.location
      ].some(
        (updatedLocation) =>
          initialLocation.location_name === updatedLocation.location_name
      )
    ) &&
    requisitionAllDetailsInitial[EBaseRequisitionFormMainControlForm.skills]
      .length ===
      updatedRequisitionAllDetails[EBaseRequisitionFormMainControlForm.skills]
        .length &&
    requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.skills
    ].every((initialSkills) =>
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.skills
      ].some(
        (updatedSkills) => initialSkills.skillName === updatedSkills.skillName
      )
    ) &&
    ((requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.industry
    ] &&
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.industry
      ] &&
      requisitionAllDetailsInitial[EBaseRequisitionFormMainControlForm.industry]
        ?.industry_name ===
        updatedRequisitionAllDetails[
          EBaseRequisitionFormMainControlForm.industry
        ]?.industry_name) ||
      (!requisitionAllDetailsInitial[
        EBaseRequisitionFormMainControlForm.industry
      ] &&
        !updatedRequisitionAllDetails[
          EBaseRequisitionFormMainControlForm.industry
        ])) &&
    ((requisitionAllDetailsInitial[
      EBaseRequisitionFormMainControlForm.subIndustry
    ] &&
      updatedRequisitionAllDetails[
        EBaseRequisitionFormMainControlForm.subIndustry
      ] &&
      requisitionAllDetailsInitial[
        EBaseRequisitionFormMainControlForm.subIndustry
      ]?.subIndustry ===
        updatedRequisitionAllDetails[
          EBaseRequisitionFormMainControlForm.subIndustry
        ]?.subIndustry) ||
      (!requisitionAllDetailsInitial[
        EBaseRequisitionFormMainControlForm.subIndustry
      ] &&
        !updatedRequisitionAllDetails[
          EBaseRequisitionFormMainControlForm.subIndustry
        ])) &&
    requisitionAllDetailsInitial.parameters.every((initialParameter) =>
      updatedRequisitionAllDetails.parameters.some(
        (updatedParameter) =>
          initialParameter.category === updatedParameter.category &&
          initialParameter.requisitionWeight ===
            updatedParameter.requisitionWeight &&
          initialParameter.isChecked === updatedParameter.isChecked
      )
    )
  );
};

export const DownloadTemplate: SxProps = {
  display: "flex",
  cursor: "pointer",
};

export const marginRight5: SxProps = {
  marginRight: "5px",
};
export const ProjectDetailContainerSxProps: SxProps = {
  padding: "10px 23px",
  display: "flex",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  alignItems: "center",
};

export const projectSXProps: SxProps = {
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
};
export const RequisitionButtons: SxProps = {
  display: "flex",
  justifyContent: "end",
};

export const RequisitionHeaderContainerSxProps: SxProps = {
  bgcolor: "#f6f2fc",
  padding: "10px 23px",
};
export const RequisitionHeaderSxProps: SxProps = {
  // color: "#5A5A5A",
  fontSize: "20px",
  // marginLeft: "20px",
  fontWeight: "550",
  color: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
};
export const SaveSxProps: SxProps = {
  backgroundColor: GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  ml: 1,
  "&:hover": {
    backgroundColor: GT_DESIGN_PARAMETERS.OnHoverGtPrimaryColorPurple,
  },
  borderRadius: "10px",
};

export const ParameterTypographyContinerMt1: SxProps = {
  width: "100",
  mt: 1,
};

export const DescriptionControlSxProps: SxProps = {
  // mt: 1,
  backgroundColor: "#F2F5FF !important",
};
export const AutocompleteControlSxProps: SxProps = {
  backgroundColor: "#F2F5FF !important",
};
