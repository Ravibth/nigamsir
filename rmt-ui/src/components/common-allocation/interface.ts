import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { IProjectMaster } from "../../common/interfaces/IProject";
import { IRequisitionMaster } from "../../common/interfaces/IRequisition";
import { GetNewDateWithNoonTimeZone } from "../../utils/date/dateHelper";
import { IBulkUploadData } from "../bulk-upload-main/interfaces";
import { IUserInfo } from "../system-suggestions/availability-view/constants";
import { ISystemSuggestions } from "../system-suggestions/interfaces";
import {
  FormValuesForAllocationBreakup,
  IAllocateFormSkills,
} from "./common-allocation-modal-form/utils";
import { EAllocationType, EBaseCommonAllocationMainControlForm } from "./enum";
import { ITimelineGroup, ITimelineItems } from "./timeline-view/interface";
import { IUpdateAllocationCommonScreenItem } from "./update-allocation-common-screeen/update-allocation-common-screeen";

export interface ICommonAllocationMainProps {
  requisition?: IRequisitionMaster;
  projectInfo: IProjectMaster;
  suggestionsSelected?: ISystemSuggestions[];
  back: () => {};
  baseType: EAllocationType;
  bulkAllocationsUploaded?: IBulkUploadData[];
  allocationsToUpdate?: IUpdateAllocationCommonScreenItem[];
  baseUserEmailToSelect?: string[];
  refreshProjectInfo: () => void;
  isPageLoad?: boolean;
}
export interface ICommonAllocationWrapperProps {
  requisition?: IRequisitionMaster;
  projectInfo: IProjectMaster;
  suggestionsSelected?: ISystemSuggestions[];
  back: () => {};
  baseType: EAllocationType;
  bulkAllocationsUploaded?: IBulkUploadData[];
  allocationsToUpdate?: IUpdateAllocationCommonScreenItem[];
  baseUserEmailToSelect?: string[];
  isPageLoad?: boolean;
}

export interface IAllUserAllocationEntries {
  email: string;
  userInfo: IUserInfo;
  type: EAllocationType;
  skills: IAllocateFormSkills[];
  available: boolean;
  competency: ICompetencyMaster;
  meta: any;
  interested: boolean;
  allocations: FormValuesForAllocationBreakup[];
  //To be used for storing previous allocations in case of update allocation for performing validations that allocation has changed
  baseAllocations?: FormValuesForAllocationBreakup[];
  requisition?: IRequisitionMaster;
  requisitionId?: string;
  description?: string;
  showDescription?: boolean;
  isContinuousAllocation: boolean;
  totalEfforts: number;
  isInDraftMode: boolean;
  showSkills: boolean;
  isSkillUpdateAllowed: boolean;
  projectInfo: IProjectMaster;
  isUpdateAllowed: boolean;
  isPreviouslyDraft: boolean;
}

export interface INewJobCodeMoveEntries {
  allResourceAllocation: IAllUserAllocationEntries[];
  pipelineCode: string;
  pipelineName: string;
  jobCode: string;
  jobName: string;
  newPipelineCode: string;
  newPipelineName: string;
  newJobCode: string;
  newJobName: string;
}
export interface IUserTimeline {
  timelineGroups: ITimelineGroup[];
  timelineItems: ITimelineItems[];
}
export interface IBaseCommonAllocationFormDetails {
  [EBaseCommonAllocationMainControlForm.startDate]: Date;
  [EBaseCommonAllocationMainControlForm.endDate]: Date;
  [EBaseCommonAllocationMainControlForm.noOfHours]: number;
  [EBaseCommonAllocationMainControlForm.isRequisition]: boolean;
  [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]: boolean;
  [EBaseCommonAllocationMainControlForm.applyToAll]?: boolean;
}
export const BaseCommonAllocationFormDetailsValues: IBaseCommonAllocationFormDetails =
  {
    startDate: new Date(
      GetNewDateWithNoonTimeZone().setDate(new Date().getDate() - 90)
    ),
    endDate: new Date(
      GetNewDateWithNoonTimeZone().setDate(new Date().getDate() + 90)
    ),
    noOfHours: 0,
    isRequisition: false,
    isPerDayHourAllocation: true,
  };
