import {
  ReactCalendarItemRendererProps,
  ReactCalendarTimelineProps,
} from "react-calendar-timeline";
import { IUserDetailsContext } from "../../../contexts/userDetailsContext";
import { LoaderContextProps } from "../../../contexts/loaderContext";

export interface TimelineProps extends ReactCalendarTimelineProps {
  name?: string;
  currentView: string;

  clickToday?: boolean;
  isEmployee: boolean;
  handlePreviousorNextClick?: string;
  submittedFilterData: any;
  setPreviousorNextNone: () => {};
  handleFilterData: Function;
  getChargeType: Function;
  SetupFilterDefaultValue: Function;
  selectedDate: any;
  onItemClick: (itemId: any, isFlag: any) => {};
  itemRenderer?:
    | ((props: ReactCalendarItemRendererProps<any>) => React.ReactNode)
    | undefined;
  userDetails: IUserDetailsContext;
  getPaginationDataFunc: Function;
  setPaginationDataFunc: Function;
  searchQuery: any;
  onInitialPaginationData: Function;
  pageLimitConst: number;
  loaderContext: LoaderContextProps;
  searchRoles: string[];
  onMouseEnterHandler: Function;
  onMouseLeaveHandler: Function;
}

export interface IPaginationData {
  limit: number;
  currentPageNumber: number;
  nextPageNumber: number;
}
