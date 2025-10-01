import {
  TimelineGroupBase,
  TimelineItemBase,
} from "react-calendar-timeline";

export interface TimelineGroupExt extends TimelineGroupBase {
  pipelineCode?: string;
  jobCode?: string;
  RowType: string;
}

export interface TimelineItemBaseExt extends TimelineItemBase<any> {
  isEmployeeItem?: boolean;
  requisitionId?: number;
  pipelineCode?: string;
  jobCode?: string;
  innerHeight?: number;
  outerHeight?: number;
  RowType: string;
}

export interface TimelineSate {
  groups: TimelineGroupExt[];
  projectsData: any;
  startDate: any;
  endDate: any;
  currentView: any;
  items: TimelineItemBaseExt[];
  projectsListData: any;
  itemsListData: any;
  submittedFilterData: any;
  isEmployee: boolean;
  projectListDataEmployee: any;
  projectChargeType: string;
  currentProjectData: any;
  isAllocationEmployeeShow: boolean;
  itemId?: number;
  pipelineCode?: string;
  jobCode?: string;
  currentRecordPipelineCode?: string;
  currentRecordJobCode?: string;
  isCalendarRefresh?: boolean;
  serviceResponseData: [any];
}
