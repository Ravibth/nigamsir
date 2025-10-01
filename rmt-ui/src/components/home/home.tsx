import { useContext, useState } from "react";
import FunctionBar from "../function-bar/function-bar";
import RequestorHome from "../requestor/requestor-home/requestor-home";
import * as GC from "../../global/constant";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { IFilterOptions } from "../scheduler/react-time-calendar/util";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";

const config = process.env;

function Home(props: any) {
  const pageLimitConst: number = Number(
    process.env.REACT_APP_PROJECTLISTING_PAGESIZE
      ? process.env.REACT_APP_PROJECTLISTING_PAGESIZE
      : 25
  ); //projectlisting page size limit
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [currentView, setCurrentView] = useState("month");
  const [isClickToday, setIsClickToday] = useState(false);
  const [isClickPreviouorNext, setIsClickPreviouorNext] = useState("");
  const [submittedFilterData, setSubmittedFilterData] = useState<any>({});
  const [filterData, setFilterData] = useState<any>();
  const [searchQueryText, setSearchQueryText] = useState<any>();
  const [searchRoles, setSearchRoles] = useState<string[]>([]);
  const [filterDefaultValue, setFilterDefaultValue] = useState<any>({
    bu: [],
    experties: [],
    offering: [],
    solution: [],
    sme: [],
    revenueUnit: [],
    clientName: [],
    pipeline: [],
    job: [],
    jobName: [],
    role: [],
    status: [],
    marketPlaceType: [],
    projectType: [],
    industry: [],
    subIndustry: [],
  });
  const [selectedDate, setSelectedDate] = useState(new Date());
  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const handleSchedulerView = (view: any) => {
    setIsClickToday(false);
    setIsClickPreviouorNext(GC.NEXTPRE_CLICK.None);
    switch (view) {
      case GC.CALENDAR_VIEW_TYPE.TimeLineDay:
        setCurrentView(GC.CALENDAR_VIEW.Day);
        break;
      case GC.CALENDAR_VIEW_TYPE.TimeLineMonth:
        setCurrentView(GC.CALENDAR_VIEW.Month);
        break;
      case GC.CALENDAR_VIEW_TYPE.TimeLineQuater:
        setCurrentView(GC.CALENDAR_VIEW.Quater);
        break;
      case GC.CALENDAR_VIEW_TYPE.TimeLineHalfYear:
        setCurrentView(GC.CALENDAR_VIEW.Half);
        break;
      case GC.CALENDAR_VIEW_TYPE.TimeLineToday:
        setIsClickToday(true);
        break;
      default:
        setCurrentView(GC.CALENDAR_VIEW.Year);
        break;
    }
  };
  const handlePreviousClick = () => {
    setIsClickToday(false);
    setIsClickPreviouorNext(GC.NEXTPRE_CLICK.Pre);
  };
  const handleNextClick = () => {
    setIsClickToday(false);
    setIsClickPreviouorNext(GC.NEXTPRE_CLICK.Next);
  };
  const handleFilterData = (filterData: IFilterOptions) => {
    setFilterData((prevState: any) => {
      return { ...filterData };
    });
  };
  const selectedDataByFilter = (data: any) => {
    setSubmittedFilterData((prevState: any) => {
      return {
        ...data,
      };
    });
  };
  // console.log(searchRoles);
  const handleResetClick = () => {
    setSubmittedFilterData((prevData: any) => {
      return {
        // bu: [],
        experties: [],
        offering: [],
        solution: [],
        sme: [],
        revenueUnit: [],
        clientName: [],
        pipeline: [],
        job: [],
        role: [],
        status: [],
        projectType: [],
        projectCode: [],
        marketplace: undefined,
        marketPlaceType: undefined,
        industry: [],
        subIndustry: [],
      };
    });
    setFilterDefaultValue((prevState: any) => {
      return {
        //  bu: [],
        experties: [],
        offering: [],
        solution: [],
        sme: [],
        revenueUnit: [],
        clientName: [],
        pipeline: [],
        job: [],

        role: [],
        status: [],
        projectType: [],
        marketplace: undefined,
        marketPlaceType: undefined,
        industry: [],
        subIndustry: [],
      };
    });
  };
  const SetupFilterDefaultValue = (data: any) => {
    setFilterDefaultValue((prevData: any) => {
      return { ...data };
    });
  };
  const selectDateHandler = (date: Date) => {
    const currentDate = new Date();
    setSelectedDate(currentDate);
  };

  const searchQueryHandler = (data: any) => {
    loaderContext.open(true);
    setSearchQueryText(data);
  };

  return (
    <>
      <FunctionBar
        handleSchedulerView={handleSchedulerView}
        handlePreviousClick={handlePreviousClick}
        handleNextClick={handleNextClick}
        selectDateHandler={selectDateHandler}
        filterData={filterData}
        defaultValue={filterDefaultValue}
        selectedDataByFilter={selectedDataByFilter}
        handleResetClick={handleResetClick}
        searchQueryHandle={(e) => searchQueryHandler(e)}
        setSearchRoles={setSearchRoles}
        submittedFilterData={submittedFilterData}
      />

      <RequestorHome
        currentView={currentView}
        clickToday={isClickToday}
        handleFilterData={handleFilterData}
        submittedFilterData={submittedFilterData}
        handlePreviousorNextClick={isClickPreviouorNext}
        setPreviousorNextNone={() => {
          setIsClickPreviouorNext(GC.NEXTPRE_CLICK.None);
        }}
        selectedDate={selectedDate}
        SetupFilterDefaultValue={SetupFilterDefaultValue}
        isEmployee={isEmployee}
        searchQuery={searchQueryText}
        pageLimitConst={pageLimitConst}
        searchRoles={searchRoles}
      ></RequestorHome>
    </>
  );
}

export default Home;
