export interface IGetAllRequisitionByProjectCodeResponse {
  pipelineCode: string;
  pipelineName?: string;
  jobCode?: string;
  jobName: string;
  requisitionDescription: string;
  isContinuousAllocation: boolean;
  startDate: Date;
  endDate: Date;
  totalHours: number;
  requisitionStatus: string;
  bu: string;
  buId: string;

  offerings?: string;
  solutions?: string;

  offeringsId?: string;
  solutionsId?: string;

  competency?: string;
  competencyId?: string;

  designation: string;
  description: string;
  createdDate: Date;
  modifiedDate: Date;
  createdBy: string;
  modifiedBy: string;
  isActive: boolean;
  score?: string;
  requisitionSkill: any[];

  id: string;
  requisitionDemand: string;
  clientName?: string;
  effortsPerDay: number;
  isPerDayHourAllocation: boolean;
  businessUnit: string;
  sMEG: string;
  createdAt: Date;
  modifiedAt: Date;
  pequisitionTypeId: number;
  hasPermissionToEdit?: boolean;
  hasPermissionToDelete?: boolean;
}

export interface IAddProjectToMarketPlace {
  pipelineCode: string;
  pipelineName: string;
  jobCode?: string;
  jobName: string;
  clientName: string;
  clientGroup: string;
  startDate: Date;
  endDate: Date;
  description: string;
  isPublishedToMarketPlace: string;
  createdBy: string;
  modifiedBy: string;
  isActive: string;
  jsonData: string;
  chargableType: string;
  location: string;
  businessUnit: string;
  Expertise: string; //Recheck

  buId: string;
  offerings: string;
  offeringsId: string;
  solutions: string;
  solutionsId: string;

  Smeg: string; //Recheck
  Sme: string; //Recheck
  RevenueUnit: string; //Recheck
  industry: string;
  subindustry: string;
  csp: string;
  proposedCsp: string;
  elForJob: string;
  elForPipeLine: string;
  jobManager: string;
  isInterested: boolean;
  marketPlaceExpirationDate: Date;
  ispipeLine: boolean;
}

export interface ISetPublishedToMarketPlace {
  isPublishedToMarketPlace: boolean;
  pipelineCode: string;
  jobCode?: string;
  marketPlaceExpirationDate?: Date;
}

export interface ICreateOrUpdatePublishedFieldForMarketPlace {
  fieldNameList: string[];
  isActiveList: boolean[];
  // projectCode: projectID,
  pipelineCode: string;
  jobCode?: string;
}

export interface IFieldForMarketPlace {
  id?: number;
  internalName: string;
  displayName: string;
  isActive: boolean;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt: Date;
}

export interface IMarketPlaceProjectDetailDTO {
  iD?: number;
  jobCode?: string;
  jobName?: string;
  pipelineCode?: string;
  pipelineName?: string;
  clientName?: string;
  clientGroup?: string;
  startDate?: Date;
  endDate?: Date;
  description?: string;
  isPublishedToMarketPlace?: boolean;
  marketPlacePublishDate?: Date;
  createdDate?: Date;
  createdBy?: string;
  modifiedDate?: Date;
  modifiedBy?: string;
  isActive?: boolean;
  jsonData?: string;
  chargableType?: string;
  location?: string;
  expertise?: string; //Recheck
  businessUnit?: string;
  buId?: string;
  offerings?: string;
  solutions?: string;
  offeringsId?: string;
  solutionsId?: string;
  smeg?: string; //Recheck
  sme?: string; //Recheck
  revenueUnit?: string; //Recheck
  industry?: string;
  subindustry?: string;
  csp?: string;
  proposedCsp?: string;
  elForJob?: string;
  elForPipeLine?: string;
  jobManager?: string;
  isInterested?: boolean;
  isAllocated?: boolean;
  marketPlaceExpirationDate?: Date;
  ispipeLine?: boolean;
}

export interface IMarketPlaceDataObject {
  marketplaceProjects: IMarketPlaceProjectDetailDTO[];
  interestedProjects: IMarketPlaceProjectDetailDTO[];
}

export interface IGetAllProjectDetailsForMarketPlace {
  limit: number;
  pagination: number;
  currentDateValue: Date;
  showLiked: boolean;
  emailId: string;
  buFiltervalue: string[];
  offeringsFiltervalue: string[];
  solutionsFiltervalue: string[];
  industryFiltervalue: string[];
  subIndustryFiltervalue: string[];
  locationFiltervalue: string[];
  isAllocatedToProject: boolean;
  startDateFiltervalue: Date;
  endDateFiltervalue: Date;
  selectedValueForSorting: string;
  orderBy: string;
}

