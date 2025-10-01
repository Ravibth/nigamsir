import { SxProps } from "@mui/system";
import * as GlobalConstant from "../../global/constant";
import moment from "moment";
import {
  calculateDaysBetweenTwoDates,
  dateFilterParams,
  getDateInMomentFormat,
  GetNewDateWithNoonTimeZone,
} from "../../utils/date/dateHelper";
import { DEFAULT_ALLOCATION_HOUR } from "../../global/constant";

const dateFormatter = function (params: any) {
  if (params.value)
    return getDateInMomentFormat(new Date(params.value), "DD-MM-YYYY");
  else {
    return null;
  }
};

export enum BulkUploadGridHeader {
  StartDate = "Start Date*",
  EndDate = "End Date*",
  PerDay = "Per Day*",
  NoOfHours = "No of Hours*",
  JobDescription = "Task Description*",
  DesignationCode = "Designation Code*",
  NoOfResources = "No of Resources*",
  CompetencyId = "Competency ID*",
  CompetencyWeightage = "Competency Weightage",
  SkillId = "Skill ID *",
  SkillWeightage = "Additional Skill Weightage*",
  EmailId = "Email ID",
  EmpCode = "Employee ID*",
  OfferingsWeightage = "Offering Weightage",
  SolutionsWeightage = "Solution Weightage",
  LocationCode = "Location ID",
  LocationWeightage = "Location Weightage",
  ClientExperienceWeightage = "Client Experience Weightage",
  IndustryId = "Industry ID",
  IndustryWeightage = "Industry Weightage",
  SubIndustryId = "Sub-Industry ID",
  SubIndustryWeightage = "Sub Industry Weightage",
}

export const requisitionColumnDefs: any[] = [
  {
    headerName: BulkUploadGridHeader.StartDate,
    field: "startDate",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    cellDataType: "date",
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    valueFormatter: dateFormatter,
    tooltipValueGetter: dateFormatter,
  },
  {
    headerName: BulkUploadGridHeader.EndDate,
    field: "endDate",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    cellDataType: "date",
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    valueFormatter: dateFormatter,
    tooltipValueGetter: dateFormatter,
  },
  {
    headerName: BulkUploadGridHeader.PerDay,
    field: "perDay",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "perDay",
    cellDataType: "boolean",
  },
  {
    headerName: BulkUploadGridHeader.NoOfHours,
    field: "totalHours",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "totalHours",
    cellDataType: "number",
  },
  {
    headerName: BulkUploadGridHeader.JobDescription,
    field: "requisitionDescription",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "requisitionDescription",
    cellDataType: "text",
  },
  {
    headerName: BulkUploadGridHeader.DesignationCode,
    field: "designationId",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "designationId",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.NoOfResources,
    field: "numberOfResources",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "numberOfResources",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.CompetencyId,
    field: "competencyId",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    width: "500px",
  },
  {
    headerName: BulkUploadGridHeader.CompetencyWeightage,
    field: "competencyWeightage",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    width: "500px",
  },
  {
    headerName: BulkUploadGridHeader.SkillId,
    field: "skillId",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "skills",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.SkillWeightage,
    field: "skillWeightage",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "skillWeightage",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.OfferingsWeightage,
    field: "offeringsWeightage",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    width: "500px",
  },
  {
    headerName: BulkUploadGridHeader.SolutionsWeightage,
    field: "solutionsWeightage",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    width: "500px",
  },
  {
    headerName: BulkUploadGridHeader.LocationCode,
    field: "locationCode",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "locationCode",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.LocationWeightage,
    field: "locationWeightage",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "locationWeightage",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.IndustryId,
    field: "industryID",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "industryID",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.IndustryWeightage,
    field: "industryWeight",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "industryWeight",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.SubIndustryId,
    field: "subIndustryID",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "subIndustryID",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.SubIndustryWeightage,
    field: "subIndustryWeight",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "subIndustryWeight",
    cellDataType: "string",
  },
  {
    headerName: "Client Experience Weightage",
    field: "sameClientExperienceWeightage",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "sameClientExperienceWeightage",
    cellDataType: "string",
  },
  {
    headerName: "Status",
    field: "status",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    // tooltipField: "status",
    filter: true,
    filterParams: {
      suppressAndOrCondition: true,
    },
    tooltipValueGetter: (params: any) => {
      return params.data?.status ? "Success" : "Error";
    },
    valueGetter: (params: any) => {
      return params.data?.status ? "Success" : "Error";
    },
  },
  {
    headerName: "Comments",
    field: "comments",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    // tooltipField: "comments",
    width: "500px",
  },
];

export const allocationColumnDefs: any[] = [
  {
    headerName: BulkUploadGridHeader.StartDate,
    field: "startDate",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    cellDataType: "date",
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    valueFormatter: dateFormatter,
    tooltipValueGetter: dateFormatter,
  },
  {
    headerName: BulkUploadGridHeader.EndDate,
    field: "endDate",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    cellDataType: "date",
    filter: "agDateColumnFilter",
    filterParams: dateFilterParams,
    valueFormatter: dateFormatter,
    tooltipValueGetter: dateFormatter,
  },
  {
    headerName: BulkUploadGridHeader.PerDay,
    field: "perDay",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "perDay",
    cellDataType: "boolean",
  },
  {
    headerName: BulkUploadGridHeader.NoOfHours,
    field: "totalHours",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "totalHours",
    cellDataType: "number",
  },
  {
    headerName: BulkUploadGridHeader.JobDescription,
    field: "requisitionDescription",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "requisitionDescription",
    cellDataType: "text",
  },
  {
    headerName: BulkUploadGridHeader.SkillId,
    field: "skillId",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "skills",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.EmailId,
    field: "emailId",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "emailId",
    cellDataType: "string",
  },
  {
    headerName: BulkUploadGridHeader.EmpCode,
    field: "empCode",
    filter: "agTextColumnFilter",
    filterParams: {
      suppressAndOrCondition: true,
    },
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    tooltipField: "empCode",
    cellDataType: "number",
  },

  {
    headerName: "Status",
    field: "status",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    // tooltipField: "status",
    filter: true,
    filterParams: {
      suppressAndOrCondition: true,
    },
    tooltipValueGetter: (params: any) => {
      return params.data?.status ? "Success" : "Error";
    },
    valueGetter: (params: any) => {
      return params.data?.status ? "Success" : "Error";
    },
  },
  {
    headerName: "Comments",
    field: "comments",
    sortable: true,
    unSortIcon: true,
    menuTabs: ["filterMenuTab"],
    // tooltipField: "comments",
    width: "500px",
  },
];

export const defaultHeaders = ["STATUS", "COMMENTS"];
export const defaultColDef = {
  lockVisible: true,
  resizable: true,
};

export const onGridReady = (params: any) => {
  params.columnApi.autoSizeAllColumns();
  const gridColumnApi = params.columnApi;
  const allColumnIds: string[] = [];
  if (gridColumnApi !== undefined) {
    gridColumnApi.getColumns().forEach((column: any) => {
      allColumnIds.push(column.getId());
    });
    gridColumnApi.autoSizeColumns(allColumnIds);
    gridColumnApi.sizeColumnsToFit();
  }
};

export const DownloadTemplateIcon: SxProps = {
  color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColorPurple,
  fontSize: "20px",
};

//todo change path from config
const templatePathUiUrlRequisition =
  process.env.REACT_APP_REQUISITION_TEMPLATE_URL_WITH_FILE_NAME;
export const XLSX_FILE_URL_REQUISITION_ = templatePathUiUrlRequisition;

const templatePathUiUrlAllocation =
  process.env.REACT_APP_ALLOCATION_TEMPLATE_URL_WITH_FILE_NAME;
export const XLSX_FILE_URL_ALLOCATION = templatePathUiUrlAllocation;

export enum DatatypeConst {
  Number = "number",
  String = "string",
}

export const requisitionHandleValidation = (
  header: string,
  impObj: any,
  errors: any[]
) => {
  switch (header.toLowerCase().trim()) {
    case BulkUploadGridHeader.StartDate.toLowerCase().trim():
      const dateVal = getDateInMomentFormat(impObj[header], "DD-MM-YYYY");
      if (dateVal === undefined || dateVal.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (dateVal === "Invalid date") {
        errors.push(`${header} must be of "DD-MM-YYYY" format`);
      }
      break;
    case BulkUploadGridHeader.EndDate.toLowerCase().trim():
      const dateVal1 = getDateInMomentFormat(impObj[header], "DD-MM-YYYY");
      if (dateVal1 === undefined || dateVal1.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (dateVal1 === "Invalid date") {
        errors.push(`${header} must be of "DD-MM-YYYY" format`);
      }
      break;
    case BulkUploadGridHeader.PerDay:
      const dataValue = impObj[header];
      if (dataValue === undefined || dataValue.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (!(dataValue === "Yes" || dataValue === "No")) {
        errors.push(`${header} can either be Yes or No`);
      }
      break;
    case BulkUploadGridHeader.NoOfHours.toLowerCase().trim():
      const PerDayPresent = impObj[BulkUploadGridHeader.PerDay];
      const EndDateCheck = moment(impObj[BulkUploadGridHeader.EndDate])
        .add(1, "days")
        .toDate();
      const NoOfHours = impObj[header];
      if (NoOfHours === undefined || NoOfHours.length === 0) {
        errors.push(`${header} cannot be Empty`);
      } else if (
        typeof NoOfHours !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (
        PerDayPresent === "Yes" &&
        (NoOfHours < DEFAULT_ALLOCATION_HOUR.min ||
          NoOfHours > DEFAULT_ALLOCATION_HOUR.max)
      ) {
        errors.push(
          `${header} must be between ${DEFAULT_ALLOCATION_HOUR.min} and ${DEFAULT_ALLOCATION_HOUR.max} when PerDay is Yes`
        );
      } else if (
        PerDayPresent === "No" &&
        NoOfHours < DEFAULT_ALLOCATION_HOUR.min
      ) {
        errors.push(
          `${header} must be equal & greater than ${DEFAULT_ALLOCATION_HOUR.min}`
        );
      } else if (PerDayPresent === "No" && EndDateCheck > new Date()) {
        const start_date = moment(impObj[BulkUploadGridHeader.StartDate])
          .add(1, "days")
          .toDate();
        const end_date = moment(impObj[BulkUploadGridHeader.EndDate])
          .add(1, "days")
          .toDate();
        const effort = parseInt(NoOfHours);
        if (start_date && end_date) {
          if (effortValidate(start_date, end_date, PerDayPresent, effort)) {
            errors.push(
              `${header} within the selected duration exceeds 8 hours for a day.Entered duration extends over the weekend.`
            );
          }
        }
      }
      break;
    case BulkUploadGridHeader.SkillId.toLowerCase().trim():
      const Skill = impObj[header];
      if (Skill === undefined || Skill.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (typeof Skill !== DatatypeConst.String.toLowerCase().trim()) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.DesignationCode.toLowerCase().trim():
      const DesignationCode = impObj[header];
      if (DesignationCode === undefined || DesignationCode.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof DesignationCode !== DatatypeConst.String.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.NoOfResources.toLowerCase().trim():
      const NoOfResources = impObj[header];
      if (NoOfResources === undefined || NoOfResources.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof NoOfResources !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      }
      break;
    case BulkUploadGridHeader.CompetencyWeightage.toLowerCase().trim():
      const CompetencyWeightage = impObj[header];
      const competencyPresent =
        impObj[BulkUploadGridHeader.CompetencyId]?.length > 0;
      if (
        competencyPresent &&
        (CompetencyWeightage === undefined || CompetencyWeightage.length === 0)
      ) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof CompetencyWeightage !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (CompetencyWeightage < 1 || CompetencyWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;
    case BulkUploadGridHeader.OfferingsWeightage.toLowerCase().trim():
      const OfferingsWeightage = impObj[header];
      if (OfferingsWeightage === undefined || OfferingsWeightage.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof OfferingsWeightage !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (OfferingsWeightage < 1 || OfferingsWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;
    case BulkUploadGridHeader.SolutionsWeightage.toLowerCase().trim():
      const SolutionsWeightage = impObj[header];
      if (SolutionsWeightage === undefined || SolutionsWeightage.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof SolutionsWeightage !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (SolutionsWeightage < 1 || SolutionsWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;
    case BulkUploadGridHeader.CompetencyId.toLowerCase().trim():
      const CompetencyId = impObj[header];
      if (CompetencyId === undefined || CompetencyId.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof CompetencyId !== DatatypeConst.String.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.LocationCode:
      const LocationCode = impObj[header];
      if (
        LocationCode?.length > 0 &&
        typeof LocationCode !== DatatypeConst.String.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.LocationWeightage.toLowerCase().trim():
      const LocationWeightage = impObj[header];
      const locationCodePresent =
        impObj[BulkUploadGridHeader.LocationCode]?.length > 0;
      if (
        locationCodePresent &&
        (LocationWeightage === undefined || LocationWeightage.length === 0)
      ) {
        errors.push(`${header} can not be Empty`);
      } else if (
        LocationWeightage?.length > 0 &&
        typeof LocationWeightage !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (LocationWeightage < 1 || LocationWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;
    case BulkUploadGridHeader.ClientExperienceWeightage.toLowerCase().trim():
      const ClientExperienceWeightage = impObj[header];
      if (
        ClientExperienceWeightage?.length > 0 &&
        typeof ClientExperienceWeightage !==
          DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (
        ClientExperienceWeightage < 1 ||
        ClientExperienceWeightage > 10
      ) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;

    case BulkUploadGridHeader.IndustryId.toLowerCase().trim():
      const IndustryId = impObj[header];
      if (
        IndustryId?.length > 0 &&
        typeof IndustryId !== DatatypeConst.String.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of String type`);
      }
      break;
    case BulkUploadGridHeader.IndustryWeightage.toLowerCase().trim():
      const IndustryWeightage = impObj[header];
      const IndustryPresent =
        impObj[BulkUploadGridHeader.IndustryId]?.length > 0;

      if (
        IndustryPresent &&
        (IndustryWeightage === undefined || IndustryWeightage.length === 0)
      ) {
        errors.push(`${header} can not be Empty`);
      } else if (
        IndustryWeightage?.length > 0 &&
        typeof IndustryWeightage !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (IndustryWeightage < 1 || IndustryWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;

    case BulkUploadGridHeader.SubIndustryId.toLowerCase().trim():
      const SubIndustryId = impObj[header];
      if (
        SubIndustryId?.length > 0 &&
        typeof SubIndustryId !== DatatypeConst.String.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of String type`);
      }
      break;
    case BulkUploadGridHeader.SubIndustryWeightage.toLowerCase().trim():
      const SubIndustryWeightage = impObj[header];
      const subIndustryPresent =
        impObj[BulkUploadGridHeader.SubIndustryId]?.length > 0;

      if (
        subIndustryPresent &&
        (SubIndustryWeightage === undefined ||
          SubIndustryWeightage.length === 0)
      ) {
        errors.push(`${header} can not be Empty`);
      } else if (
        SubIndustryWeightage?.length > 0 &&
        typeof SubIndustryWeightage !==
          DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      } else if (SubIndustryWeightage < 1 || SubIndustryWeightage > 10) {
        errors.push(`${header} must be a number between 1 and 10`);
      }
      break;
    case BulkUploadGridHeader.JobDescription.toLowerCase().trim():
      const jobDescription = impObj[header];
      if (jobDescription === undefined || jobDescription.length === 0) {
        errors.push(`${header} can not be empty`);
      }
      break;
    case BulkUploadGridHeader.EmailId.toLowerCase().trim():
      const email = impObj[header];
      if (email?.length > 0) {
        errors.push(
          "Invalid data uploaded. Pls refer to sample data for uploading correct data for Requisition/ Allocations"
        );
      }
      break;
    case BulkUploadGridHeader.EmpCode.toLowerCase().trim():
      const empCode = impObj[header];
      if (empCode?.length > 0) {
        errors.push(
          "Invalid data uploaded. Pls refer to sample data for uploading correct data for Requisition/ Allocations"
        );
      }
      break;
    default:
  }
};

function isValidEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

export const allocationHandleValidation = (
  header: string,
  impObj: any,
  errors: any[]
) => {
  switch (header.toLowerCase().trim()) {
    case BulkUploadGridHeader.StartDate.toLowerCase().trim():
      const dateVal = getDateInMomentFormat(impObj[header], "DD-MM-YYYY");
      if (dateVal === undefined || dateVal.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (dateVal && dateVal === "Invalid date") {
        errors.push(`${header} must be of "DD-MM-YYYY" format`);
      }
      //  else if (
      //   dateVal &&
      //   dateVal.constructor.toString().indexOf("Date") === -1
      // ) {
      //   errors.push(`${header} must be of Date type`);
      // }
      break;
    case BulkUploadGridHeader.EndDate.toLowerCase().trim():
      const dateVal1 = getDateInMomentFormat(impObj[header], "DD-MM-YYYY");
      if (dateVal1 === undefined || dateVal1.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (dateVal1 === "Invalid date") {
        errors.push(`${header} must be of "DD-MM-YYYY" format`);
      } else if (impObj[header].constructor.toString().indexOf("Date") === -1) {
        errors.push(`${header} must be of Date type`);
      }
      break;
    case BulkUploadGridHeader.PerDay.toLowerCase().trim():
      const dataValue = impObj[header];
      if (dataValue === undefined || dataValue.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (!(dataValue === "Yes" || dataValue === "No")) {
        errors.push(`${header} can either be Yes or No`);
      }
      break;
    case BulkUploadGridHeader.NoOfHours.toLowerCase().trim():
      const NoOfHours = impObj[header];
      if (NoOfHours === undefined || NoOfHours.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (
        typeof NoOfHours !== DatatypeConst.Number.toLowerCase().trim()
      ) {
        errors.push(`${header} must be of number type`);
      }
      break;
    case BulkUploadGridHeader.SkillId.toLowerCase().trim():
      const Skill = impObj[header];
      if (Skill === undefined || Skill.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (typeof Skill !== DatatypeConst.String.toLowerCase().trim()) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.EmailId.toLowerCase().trim():
      const email = impObj[header];
      if (email === undefined || email.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (!isValidEmail(email)) {
        errors.push(`${header} must be a valid email address`);
      }
      break;
    case BulkUploadGridHeader.EmpCode.toLowerCase().trim():
      const empcode = impObj[header];
      if (empcode === undefined || empcode.length === 0) {
        errors.push(`${header} can not be Empty`);
      } else if (typeof empcode !== DatatypeConst.String.toLowerCase().trim()) {
        errors.push(`${header} must be of string type`);
      }
      break;
    case BulkUploadGridHeader.JobDescription.toLowerCase().trim():
      const jobDescription = impObj[header];
      if (jobDescription === undefined || jobDescription.length === 0) {
        errors.push(`${header} can not be empty`);
      }
      break;
    default:
  }
};

export const preAllocationHandleValidation = (header: string, impObj: any) => {
  switch (header.toLowerCase().trim()) {
    case BulkUploadGridHeader.DesignationCode.toLowerCase().trim():
      const DesignationCode = impObj[header];
      if (DesignationCode !== undefined && DesignationCode.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.NoOfResources.toLowerCase().trim():
      const NoOfResources = impObj[header];
      if (NoOfResources !== undefined && NoOfResources.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.CompetencyId.toLowerCase().trim():
      const CompetencyId = impObj[header];
      if (CompetencyId !== undefined && CompetencyId.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.CompetencyWeightage.toLowerCase().trim():
      const CompetencyWeightage = impObj[header];
      if (CompetencyWeightage !== undefined && CompetencyWeightage.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.SolutionsWeightage.toLowerCase().trim():
      const SolutionsWeightage = impObj[header];
      if (SolutionsWeightage !== undefined && SolutionsWeightage.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.OfferingsWeightage.toLowerCase().trim():
      const OfferingsWeightage = impObj[header];
      if (OfferingsWeightage !== undefined && OfferingsWeightage.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.LocationCode.toLowerCase().trim():
      const LocationCode = impObj[header];
      if (LocationCode !== undefined && LocationCode.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.LocationWeightage.toLowerCase().trim():
      const LocationWeightage = impObj[header];
      if (LocationWeightage !== undefined && LocationWeightage.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.ClientExperienceWeightage.toLowerCase().trim():
      const ClientExperienceWeightage = impObj[header];
      if (
        ClientExperienceWeightage !== undefined &&
        ClientExperienceWeightage.length > 0
      ) {
        return true;
      }
      break;
    case BulkUploadGridHeader.SubIndustryId.toLowerCase().trim():
      const SubIndustryId = impObj[header];
      if (SubIndustryId !== undefined && SubIndustryId.length > 0) {
        return true;
      }
      break;
    case BulkUploadGridHeader.SubIndustryWeightage.toLowerCase().trim():
      const SubIndustryWeightage = impObj[header];
      if (
        SubIndustryWeightage !== undefined &&
        SubIndustryWeightage.length > 0
      ) {
        return true;
      }
      break;
    default:
      return false;
  }
  return false;
};

export const gridOptions = {
  suppressCellSelection: true,
};

export enum TypeOfBulkUpload {
  ALLOCATION = "allocation",
  REQUISITION = "requisition",
}

const effortValidate = (
  start_date: Date | string,
  end_date: Date | string,
  PerDayPresent: string,
  effort: number
) => {
  // Convert start_date and end_date to Date objects if they are strings
  const startDate =
    typeof start_date === "string" ? new Date(start_date) : start_date;
  const endDate = typeof end_date === "string" ? new Date(end_date) : end_date;

  const totalDays = calculateDaysBetweenTwoDates(
    GetNewDateWithNoonTimeZone(startDate),
    GetNewDateWithNoonTimeZone(endDate)
  );
  const minNoOfHours = DEFAULT_ALLOCATION_HOUR.min;
  const maxNoOfHours = DEFAULT_ALLOCATION_HOUR.max * totalDays;

  // Check if PerDay is "Yes" and effort is within the allowed range
  if (PerDayPresent === "Yes") {
    return !(effort >= minNoOfHours && effort <= maxNoOfHours);
  }

  // Check if PerDay is "No" and effort exceeds the maximum allowed
  return effort > maxNoOfHours;
};
