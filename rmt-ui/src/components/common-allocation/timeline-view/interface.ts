import { IUserInfo } from "../../system-suggestions/availability-view/constants";
import { FormValuesForAllocationBreakup } from "../common-allocation-modal-form/utils";
import { EAllocationType } from "../enum";
import { IUserTimeline, IAllUserAllocationEntries } from "../interface";
import { IWeeklyBreakup } from "../../calendar/Utils";

export interface ITimelineViewProps {
  userTimelines: IUserTimeline;
  defaultTimeStart: Date | moment.Moment;
  defaultTimeEnd: Date | moment.Moment;
  timelineGroups: ITimelineGroup[];
  timelineItems: ITimelineItems[];
  onItemClick: (
    itemId: any,
    e: React.SyntheticEvent<Element, Event>,
    time: number
  ) => {};
  showCheckbox: boolean;
  onCheckBoxChange?: (e) => void;
  allUsersSelected: boolean;
}

export interface ITimelineGroup {
  id: number;
  title: any;
  height: number;
  user: IAllUserAllocationEntries;
  type: EAllocationType;
  display: boolean;
}

export interface ITimelineItems {
  id: number;
  title: any;
  start_time: Date;
  end_time: Date;
  group: number;
  innerHeight: number;
  outerHeight: number;
  condition?: string;
  itemProps?: {
    className?: string;
  };
  user?: IUserInfo;
  allocations: FormValuesForAllocationBreakup[];
  meta?: { weeklyBreakup: IWeeklyBreakup; weeklyTotal: number } | any;
  type: EAllocationType;
}
