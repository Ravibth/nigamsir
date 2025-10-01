import Grid from "@mui/material/Grid";
import {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import Timeline, {
  TimelineHeaders,
  SidebarHeader,
  DateHeader,
} from "react-calendar-timeline";
import { TimelineGroup } from "../Type/type";
import {
  aggregatedEmployeeInformation,
  calculateNextDates,
  calculatePreviousDates,
  calculateStartAndEnd,
  generateTimelineData,
  getEmployeeItemBackgroundColor,
  getPortfolioFiltersOptions,
  getFriday,
  getMonday,
  getEmployeeProfileData,
  createWeeklyItem,
  getQuarterEndMonth,
  downloadEmployeesForPortfolioReport,
  convertToCSV,
} from "../util/util";
import { useForm } from "react-hook-form";
import EmployeeRow from "./employee-row";
import "./style.css";
import { v4 as uuidv4 } from "uuid";
import ProjectRow from "./project-row";
import {
  IEmployeeLeaveHolidayAndAvailablity,
  IEmployeeModel,
} from "../../../common/interfaces/IEmployeeModel";
import * as GC from "../../../global/constant";
import { PorjectPortfolio } from "../../../services/employee-portfolio/project-portfolio";
import { DateRangePickerSection } from "./DateRangePickerSection";
import { BuCompetencySelector } from "./BuCompetencySelector";
import { ViewControls } from "./ViewControls";
import moment from "moment";
import { IResourceAllocationMaster } from "../../../common/interfaces/IAllocation";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import PortfoliMainFilter from "./portfoli-main-filter";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import FileDownloadOutlinedIcon from '@mui/icons-material/Download';
import EmployeeItemRow from "./employee-item-row";
import { IPortfolioFiltersOptions } from "../../../common/interfaces/IPortfolioFiltersOptions";
import { getAllCompetencyByMID } from "../../../services/wcgt-master-services/wcgt-master-services";
import { IBUTreeMappingListByMID } from "../../../common/interfaces/IBUTreeMappingListByMID";
import {
  getBUTreeMappingListByMID,
  getEmployeesSuperCoachOrCSCByMID,
} from "../../../services/configuration-services/configuration.service";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import { PAGESIZE_FOR_EMPLOYEE_PORTFOLIO } from "../util/constant";
import { debounce } from "lodash";
import ProjectWeeklyItem from "./project-weekly-item";
import { ERROR_MSG } from "../../../global/constant";
import { differenceInDays, endOfQuarter, startOfQuarter } from "date-fns";
import { IconButton } from "@mui/material";
import { DownloadTemplateIcon } from "../../bulk-upload-main/bulk-upload-constant";
import ActionButton from "../../actionButton/actionButton";
import format from 'date-fns/format';
import { eachDayOfInterval, parseISO } from 'date-fns';

export const Timesheet = () => {
  const ctrlTimelineContainerRef = useRef<any>(null);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [employees, setEmployees] = useState<
    IEmployeeLeaveHolidayAndAvailablity[]
  >([]);
  const [projectsMap, setProjectsMap] = useState<
    Map<string, IResourceAllocationMaster[]>
  >(new Map());
  const userContext = useContext(UserDetailsContext);
  const [groups, setGroups] = useState<TimelineGroup[]>([]);
  const [items, setItems] = useState([]);
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);
  const [expandedEmployees, setExpandedEmployees] = useState<Set<string>>(
    new Set()
  );
  const [currentView, setCurrentView] = useState("month");
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [submittedFilterData, setSubmittedFilterData] = useState<any>({});
  const [searchQueryText, setSearchQueryText] = useState<any>();
  const [filiterOptions, setFiliterOptions] =
    useState<IPortfolioFiltersOptions>();
  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);
  const [pageNumber, setPageNumber] = useState(1);
  const weekEffortMap = new Map<
    string,
    { totalEffort: number; start: Date; end: Date }
  >();
  const [isLeftArrowDisabled, setIsLeftArrowDisabled] = useState(false);
  const [isRightArrowDisabled, setIsRightArrowDisabled] = useState(false);

  const [timelineData, setTimelineData] = useState<{
    start: any;
    end: any;
    visibleTimeStart: number;
    visibleTimeEnd: number;
  } | null>(null);

  const {
    control,
    getValues,
    setValue,
    handleSubmit,
    watch,
    setError,
    formState: { errors, isDirty },
  } = useForm();

  const [userLeaderRoles, setUserLeaderRoles] =
    useState<IBUTreeMappingListByMID | null>(null);



  const [searchRoles, setSearchRoles] = useState<string[]>([]);

  const toggleEmployee = async (employeeId: string, emailId: string) => {
    const newExpanded = new Set(expandedEmployees);
    newExpanded.has(employeeId)
      ? newExpanded.delete(employeeId)
      : newExpanded.add(employeeId);
    //if (!projectsMap.has(employeeId)) 
    {
      try {
        let startdateproject = new Date(getValues("startDate"))
        let enddateproject = new Date(getValues("endDate"))
        const data = await PorjectPortfolio(`${employeeId}__${emailId}`,startdateproject,enddateproject, submittedFilterData);
        setProjectsMap((prev) => new Map(prev.set(employeeId, data)));
      } catch (error) {
        console.error("Error fetching projects:", error);
      }
    }
    setExpandedEmployees(newExpanded);
  };

  useEffect(() => {
    const filteredEmployees = employees;

    if (filteredEmployees && filteredEmployees?.length > 0) {
      const innerItems: any[] = [];
      const innerGroup: any[] = [];
      const employeeInformation = aggregatedEmployeeInformation(
        filteredEmployees,
        currentView,
        {
          start: new Date(moment(getValues("startDate")).format("YYYY-MM-DD")),
          end: new Date(moment(getValues("endDate")).format("YYYY-MM-DD")),
        }
      );
      const employeeUniqInfo = filteredEmployees.map((x) => {
        return {
          email_id: x.email_id,
          email_id_uid: x.email_id_uid,
          employee_mid: x.employee_mid,
          name: x.name,
        };
      });
      const employeeSet = new Set(employeeUniqInfo.map((x) => x.employee_mid));
      Array.from(employeeSet)
        .map((x) => {
          const currentEmp = filteredEmployees.find(
            (l) => l.employee_mid === x
          );
          return {
            email_id: currentEmp.email_id,
            email_id_uid: currentEmp.email_id_uid,
            employee_mid: currentEmp.employee_mid,
            name: currentEmp.name,
            totalTimelineAllocationHrs: employeeInformation
              .get(currentEmp.employee_mid)
              ?.at(0)?.totalTimelineAllocationHrs,
            totalTimelineLeaveHrs: employeeInformation
              .get(currentEmp.employee_mid)
              ?.at(0)?.totalTimelineLeaveHrs,
            totalTimelineHolidayHrs: employeeInformation
              .get(currentEmp.employee_mid)
              ?.at(0)?.totalTimelineHolidayHrs,
            totalTimelineAvailablity: employeeInformation
              .get(currentEmp.employee_mid)
              ?.at(0)?.totalTimelineAvailablity,
          };
        })
        .forEach((employee) => {
          innerGroup.push({
            id: employee.employee_mid,
            title: (
              <div className="employee-title">
                <EmployeeRow
                  isExpanded={expandedEmployees.has(employee.employee_mid)}
                  onToggle={() =>
                    toggleEmployee(employee.employee_mid, employee.email_id)
                  }
                  employee={employee}
                />
              </div>
            ),
            height: 50,
            lineHeight: 20,
          });

          employeeInformation
            ?.get(employee?.employee_mid)
            ?.forEach((element) => {
              innerItems.push({
                type: "EMPLOYEE_ITEM",
                employee: element,
                filteredEmployees: filteredEmployees,
                id: uuidv4(),
                group: employee.employee_mid,
                innerHeight: 500,
                outerHeight: 500,
                start_time: element.start,
                end_time: element.end,
                itemProps: {
                  style: {
                    background: getEmployeeItemBackgroundColor(element),
                    color: "black",
                    borderRadius: "4px",
                    padding: "1px 4px",
                  },
                },
              });
            });

          if (expandedEmployees.has(employee.employee_mid)) {
            const employeeProjects = projectsMap.get(employee.employee_mid) || [];  //getting project list from allocation                        
            const groupedProjects = new Map();  //group the projects based on pipeline and job code for clubbing allocationdays data.            
              employeeProjects.forEach(project => {
                  const key = `${project.jobCode}_${project.pipelineCode}`;
                  
                  if (!groupedProjects.has(key)) {
                      groupedProjects.set(key, {  ...project,  resourceAllocationDays: [], calculatedEfforts: 0 });
                  }                
                  groupedProjects.get(key).resourceAllocationDays.push(...project.resourceAllocationDays);
              });

            // Calculate total efforts per project in date range
            groupedProjects.forEach((projectAlloc) => {
              projectAlloc.calculatedEfforts = projectAlloc.resourceAllocationDays?.reduce((sum, dayAlloc) => {
                const allocationDate = new Date(dayAlloc?.allocationDate);
                const allocationDateStr = format(new Date(dayAlloc?.allocationDate), GC.dateFormatyMd);
                const startDateStr = format(new Date(getValues("startDate")), GC.dateFormatyMd);
                const endDateStr = format(new Date(getValues("endDate")), GC.dateFormatyMd);

                if (allocationDateStr >= startDateStr && allocationDateStr <= endDateStr) {
                  const workEntry = filteredEmployees.find((emp) => emp.employee_mid === employee.employee_mid
                    &&
                    format(parseISO(emp.working_date.toString()), GC.dateFormatyMd) === format(allocationDate, GC.dateFormatyMd));

                  if (workEntry.leave_hrs == 8 || workEntry.holiday_hrs == 8) {
                    return sum;
                  }
                  if(workEntry.leave_hrs == 4)
                  {                          
                    if(dayAlloc?.efforts > 4){ 
                      let leaveHrs = dayAlloc?.efforts - 4;
                      sum -= leaveHrs;
                    }                       
                  }
                  return sum + dayAlloc?.efforts
                }
                return sum;
              }, 0) || 0;
            });

            // Create a map of unique projects with their efforts
            const uniqueProjectsMap = new Map();
            groupedProjects.forEach((project, key) => {                
                uniqueProjectsMap.set(key, {
                    ...project,
                    resourceAllocationDays: undefined, 
                    efforts: project.calculatedEfforts
                });
            });

            // Project listing on employee expansion
            uniqueProjectsMap.forEach((project) => {
              innerGroup.push({
                id: `${employee.employee_mid}-project-${project.id}`,
                title: <ProjectRow project={project} efforts={project.efforts} />,
                height: 50,
                lineHeight: 20,
              });
            });

            //Project time line
            groupedProjects.forEach((projectAllocation) => {
              if (projectAllocation.resourceAllocationDays?.length) {
                weekEffortMap.clear();
                const groupId = `${employee.employee_mid}-project-${projectAllocation.id}`;

                projectAllocation.resourceAllocationDays.forEach((dayAlloc) => {
                  const monday = getMonday(new Date(dayAlloc.allocationDate));
                  const friday = getFriday(monday);
                  const weekKey = monday.toISOString();

                  if (!weekEffortMap.has(weekKey)) {
                    weekEffortMap.set(weekKey, {
                      totalEffort: 0,
                      start: monday,
                      end: friday,
                    });
                  }
                  weekEffortMap.get(weekKey)!.totalEffort += dayAlloc.efforts;
                });

                weekEffortMap.forEach((week) => {
                  innerItems.push(
                    createWeeklyItem(
                      uuidv4(),
                      groupId,
                      week,
                      projectAllocation,
                      filteredEmployees,
                      employee.employee_mid
                    )
                  );
                });
              }
            });
          }
        });

      setGroups([...innerGroup]);
      setItems([...innerItems]);
    } else {
      setGroups([]);
      setItems([]);
    }
    return () => {
      setGroups([]);
      setItems([]);
    };
  }, [employees, expandedEmployees]);

  const handleSchedulerView = (view: any) => {
    switch (view) {
      case GC.CALENDAR_VIEW_TYPE.TimeLineMonth:
        {
          setCurrentView(GC.CALENDAR_VIEW.Month);
          setSelectedDate(getValues("startDate"));
          setTimelineData(
            generateTimelineData(
              GC.CALENDAR_VIEW.Month,
              getValues("startDate"),
              getValues("endDate")
            )
          );
        }
        break;
      case GC.CALENDAR_VIEW_TYPE.TimeLineQuater:
        {
          setCurrentView(GC.CALENDAR_VIEW.Quater);
          setSelectedDate(getValues("startDate"));
          setTimelineData(
            generateTimelineData(
              GC.CALENDAR_VIEW.Quater,
              getValues("startDate"),
              getValues("endDate")
            )
          );
        }
        break;
      default:
        setCurrentView(GC.CALENDAR_VIEW.Month);
        break;
    }
  };

  const handleNextClick = () => {
    if (startDate && endDate) {
      const { newSelectedDate, newStartDate, newEndDate } = calculateNextDates(
        currentView,
        selectedDate
      );
      setStartDate(newStartDate);
      setEndDate(newEndDate);
      setSelectedDate(newSelectedDate);
      setTimelineData(
        generateTimelineData(currentView, newStartDate, newEndDate)
      );
      fetchData(1);
    }
  };

  const handlePreviousClick = () => {
    if (startDate && endDate) {
      const { newSelectedDate, newStartDate, newEndDate } =
        calculatePreviousDates(currentView, selectedDate);
      setStartDate(newStartDate);
      setEndDate(newEndDate);
      setSelectedDate(newSelectedDate);
      setTimelineData(
        generateTimelineData(currentView, newStartDate, newEndDate)
      );
      fetchData(1);
    }
  };

  const handleCurrentDateClick = () => {
    setSelectedDate(getValues("startDate"));
    const timelineData = generateTimelineData(
      currentView,
      getValues("startDate"),
      getValues("startDate")
    );
    setTimelineData(timelineData);
    fetchData(1);
  };
 

  useEffect(() => {
    enabledOrDiabledArrow();
  }, [selectedDate]);

  const enabledOrDiabledArrow = () => {
    const _startDate = getValues("startDate");
    const _endDate = getValues("endDate");
    if (selectedDate && _startDate && _endDate) {
      if (currentView == GC.CALENDAR_VIEW.Quater) {
        const _startQuarterDate = startOfQuarter(_startDate);
        const _selectedQuarterStartDate = startOfQuarter(selectedDate);
        const _selectedQuarterEndDate = endOfQuarter(selectedDate);
        const _endQuarterDate = endOfQuarter(_endDate);

        setIsLeftArrowDisabled(_selectedQuarterStartDate.getMonth() == _startQuarterDate.getMonth() && _selectedQuarterStartDate.getFullYear() == _startQuarterDate.getFullYear());
        setIsRightArrowDisabled(_selectedQuarterEndDate.getMonth() == _endQuarterDate.getMonth() && _selectedQuarterEndDate.getFullYear() == _endQuarterDate.getFullYear());
      } else {
        setIsLeftArrowDisabled(
          selectedDate.getMonth() == _startDate.getMonth() &&
            selectedDate.getFullYear() == _startDate.getFullYear()
        );
        setIsRightArrowDisabled(
          selectedDate.getMonth() == _endDate.getMonth() &&
            selectedDate.getFullYear() == _endDate.getFullYear()
        );
      }
    }
  };

  const fetchData = async (
    pageIndex: number,
    isApplyFilter: boolean = false
  ) => {
    const buId = watch("selectedBUId");  
    const competencies = getValues("selectedCompetency");
    const _buid = buId?.length ? [buId] : [];    
    const formattedStartDate = moment(getValues("startDate")).format(
      "YYYY-MM-DD"
    );
    const formattedEndDate = moment(getValues("endDate")).format("YYYY-MM-DD");
    if (startDate > endDate) {
      setError("startDate", {
        type: "manual",
        message: "Select vaild date",
      });
    }
    loaderContext.open(true);
    try {
      const profileDataPromise = getEmployeeProfileData(
        formattedStartDate,
        formattedEndDate,
        submittedFilterData,
        _buid,
        competencies,
        pageIndex,
        PAGESIZE_FOR_EMPLOYEE_PORTFOLIO,
        searchRoles
      );

      let employeeData: any[] = [];

      if (!isApplyFilter) {
        const filterOptionsPromise = getPortfolioFiltersOptions(
          formattedStartDate,
          formattedEndDate,
          _buid,
          competencies,
          searchRoles
        );

        const [profileData, filterOptions] = await Promise.all([
          profileDataPromise,
          filterOptionsPromise,
        ]);

        employeeData = profileData;

        setFiliterOptions({
          supercoaches: filterOptions.supercoaches,
          cosupercoaches: filterOptions.cosupercoaches,
          employees: filterOptions.employees,
          clients: filterOptions.clients,
          designations: filterOptions.designations,
          locations: filterOptions.locations,
        });
      } else {
        employeeData = await profileDataPromise;
      }
      setEmployees((prev) =>
        pageIndex === 1 ? employeeData : [...prev, ...employeeData]
      );
    } catch (error) {
      console.error("Error fetching employees for portfolio", error);
    } finally {
      loaderContext.open(false);
    }
  };

  useEffect(() => {
    if (
      timelineData &&
      timelineData.visibleTimeStart &&
      timelineData.visibleTimeEnd
    ) {
      fetchData(1);
      enabledOrDiabledArrow();
    }
  }, [currentView]);

  async function handleSearch(data: any) {
    const { startDate, endDate } = data;
    setEmployees([]);
    setExpandedEmployees(new Set());
    await setTimelineData(
      generateTimelineData(currentView, startDate, endDate)
    );
    setPageNumber(1);
    fetchData(1);
    setSelectedDate(getValues("startDate"));
    enabledOrDiabledArrow();
  }

  const selectDateHandler = (date: Date) => {
    const currentDate = new Date();
    setSelectedDate(currentDate);
  };

  const selectedDataByFilter = (data: any) => {
    setExpandedEmployees(new Set());
    setSubmittedFilterData(data);
  };
  useEffect(() => {
    //setPageNumber(-1);
    setIsFilterApplied(!isFilterApplied);
  }, [submittedFilterData]);

  const handleResetClick = () => {
    setSubmittedFilterData((prevData: any) => {
      return {
        availability: 0,
        grade: [],
        designation: [],
        location: [],
        employeename: [],
        supercoach: [],
        cosupercoach: [],
        clientname: [],
        clientgroupname: [],
      };
    });
  };

  const searchQueryHandler = (data: any) => {
    loaderContext.open(true);
    setPageNumber((prev) => prev + 1);
    setSearchQueryText(data);
  };

  const userDetailsContext = useContext(UserDetailsContext);
  const [competencyLeaderRoles, setCompetencyLeaderRoles] = useState<
    ICompetencyMaster[]
  >([]);

  const loadLeadersInformationFromBUTreeMapping = () => {
    return new Promise((resolve, reject) => {
      getBUTreeMappingListByMID(userDetailsContext.employee_id)
        .then((response) => {
          resolve(response.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };
  const loadLeadersInformationFromCompetencyMapping = () => {
    return new Promise<ICompetencyMaster[]>((resolve, reject) => {
      getAllCompetencyByMID(userDetailsContext.employee_id)
        .then((response) => {
          resolve(response);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  useEffect(() => {
    if (userDetailsContext && userDetailsContext?.employee_id?.length > 0) {
      Promise.all([
        loadLeadersInformationFromBUTreeMapping(),
        loadLeadersInformationFromCompetencyMapping(),
      ])
        .then((response) => {
          const result: IBUTreeMappingListByMID =
            response[0] as IBUTreeMappingListByMID;
          setUserLeaderRoles(result);
          setCompetencyLeaderRoles(response[1]);
        })
        .catch((err) => {
          // throw err;
        });
    }
  }, [userDetailsContext]);

  const handleScroll = useCallback(
    debounce(() => {
      const element =
        ctrlTimelineContainerRef.current?.childNodes[0]?.childNodes[1];
      if (element) {
        const scrollPosition = element.scrollHeight - element.scrollTop;
        const scrollMargin = 5;
        // Example: Load more data when near bottom
        if (scrollPosition <= element.clientHeight + scrollMargin) {
           
          setPageNumber((prev) => prev + 1);
          
        }
      }
    }, 300),
    [timelineData]
  );

  useEffect(() => {    
     fetchData(pageNumber, true);     
  }, [pageNumber,isFilterApplied]);

  useEffect(() => {
    const container = ctrlTimelineContainerRef.current;
    if (!container) return;
    // Find the scrollable element (same as in your working code)
    const scrollElement = container.childNodes[0]?.childNodes[1];
    if (!scrollElement) return;
    // Add event listener
    scrollElement.addEventListener("scroll", handleScroll);
    // Cleanup function
    return () => {
      scrollElement.removeEventListener("scroll", handleScroll);
      handleScroll.cancel(); // Cancel any pending debounced calls
    };
  }, [handleScroll]); // Re-run when employees change

  const onStartDateChange = () => {
    const _startDate = getValues("startDate");
    const _endDate = getValues("endDate");
    const days = differenceInDays(new Date(_endDate), new Date(_startDate));
    if (_startDate > _endDate || days > GC.PORTFOLIO_MAX_DATERANGE_DAYS){
      setValue("endDate", null);
      setEndDate(null);
    }
    setSelectedDate(_startDate);
  };

  function onItemClick(item: any, e: any, time: any) {
    //throw new Error("Function not implemented.");
  }

  const downloadReport = async () =>{
    console.log("dowload");
    const buId = watch("selectedBUId");  
    const competencies = getValues("selectedCompetency");
    const _buid = buId?.length ? [buId] : [];    
    const formattedStartDate = moment(getValues("startDate")).format(
      "YYYY-MM-DD"
    );
    const formattedEndDate = moment(getValues("endDate")).format("YYYY-MM-DD");
    if (startDate > endDate) {
      setError("startDate", {
        type: "manual",
        message: "Select vaild date",
      });
    }
    loaderContext.open(true);
    
      const responseData = await downloadEmployeesForPortfolioReport(
        formattedStartDate,
        formattedEndDate,
        submittedFilterData,
        _buid,
        competencies,
        searchRoles,
        currentView
      );
    const csvData = convertToCSV(responseData, moment(getValues("startDate")).format("DD-MM-YYYY"));
    const blob = new Blob([csvData], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.setAttribute('hidden', '');
    a.setAttribute('href', url);
    a.setAttribute('download', 'portfolio_report.csv');
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);

    loaderContext.open(false);   
  }

  return (
    <>
      <Grid container spacing={2} alignItems="center" marginTop={"3px"}>
        <PortfoliMainFilter
          handleSchedulerView={handleSchedulerView}
          handlePreviousClick={handlePreviousClick}
          handleNextClick={handleNextClick}
          selectDateHandler={selectDateHandler}
          selectedDataByFilter={selectedDataByFilter}
          handleResetClick={handleResetClick}
          searchQueryHandle={(e) => searchQueryHandler(e)}
          control={control}
          filterOptions={filiterOptions}
          submittedFilterData={submittedFilterData}
        />
        <DateRangePickerSection
          control={control}
          errors={errors}
          setStartDate={setStartDate}
          setEndDate={setEndDate}
          startDate={startDate}
          onStartDateChange={onStartDateChange}
        />

        <BuCompetencySelector
          empMid={userContext.employee_id}
          errors={errors}
          control={control}
          competencyName="selectedCompetency"
          handleSearch={handleSubmit(handleSearch)}
          setValue={setValue}
          getValues={getValues}
          isLeader={
            [GC.Role.Leaders, GC.Role.CEOCOO, GC.Role.Admin,GC.Role.SystemAdmin].some((role) =>
              userContext.role.includes(role)
            )
              ? true
              : false
          }
          userLeaderRoles={userLeaderRoles}
          competencyLeaderRoles={competencyLeaderRoles}
          setSearchRoles={setSearchRoles}
        />

        {startDate && endDate && timelineData ? (
          <ViewControls
            control={control}
            handleCurrentDateClick={handleCurrentDateClick}
            handlePreviousClick={handlePreviousClick}
            handleNextClick={handleNextClick}
            handleSchedulerView={handleSchedulerView}
            startDate={getValues("startDate")}
            endDate={getValues("endDate")}
            selectedDate={selectedDate}
            isLeftArrowDisabled={isLeftArrowDisabled}
            isRightArrowDisabled={isRightArrowDisabled}
          />
        ) : (
          ""
        )}
      </Grid>

      {/* Timeline Section */}
      {startDate === null && endDate === null || !timelineData ? (
        <Grid className="emptygridtimesheet">Please select the date range and role to proceed.</Grid>
      ) : (
        <div>
          <div
            className={`emptygridtimesheet ${
              (!loaderContext.isOpen && items.length > 0) ||
              loaderContext.isOpen
                ? "displayhide"
                : ""
            }`}
          >
            {ERROR_MSG.timesheet_no_record_found}
          </div>
          <div className={`${items.length == 0 ? "displayhide" : ""}`}>
          <Grid className="download-button" ml={1} mt={3.5}>
            <div>
               <ActionButton 
              label={"Download"} 
              onClick={downloadReport}
              disabled={false}
              type={"button"}
              textTransform="none"
            />
            </div>
          </Grid>
           {/* <FileDownloadOutlinedIcon sx={DownloadTemplateIcon} /> */}
            <div
              id="timelineCalendartimesheet"
              className={`time-calender-main div-Timeline-container`}
              ref={ctrlTimelineContainerRef}
            >
              <Timeline
                groups={groups}
                items={items}
                itemRenderer={({ item, getItemProps }) => {
                  const baseStyle = {
                    ...item.itemProps?.style,
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    height: item.outerHeight ?? 50,
                  };

                  if (item.type === "EMPLOYEE_ITEM") {
                    return (
                      <div {...getItemProps({ style: baseStyle })}>
                        <EmployeeItemRow
                          employee={item.employee}
                          filteredEmployees={item.filteredEmployees}
                          currentView={currentView}
                        />
                      </div>
                    );
                  }

                  if (item.type === "PROJECT_ITEM") {
                    return (
                      <div {...getItemProps({ style: baseStyle })}>
                        <ProjectWeeklyItem
                          id={item.id}
                          groupId={item.group}
                          week={item.week}
                          projectAllocation={item.projectAllocation}
                          employee_mid={item.employee_mid}
                          filteredEmployees={item.filteredEmployees}
                        />
                      </div>
                    );
                  }

                  return null;
                }}
                visibleTimeStart={timelineData.visibleTimeStart}
                visibleTimeEnd={timelineData.visibleTimeEnd}
                stackItems
                sidebarWidth={310}
                buffer={1}
                onItemDoubleClick={(item: any, e: any, time: any) => {
                  onItemClick(item, e, time);
                }}
              >
                <TimelineHeaders className="sticky">
                  <SidebarHeader>
                    {({ getRootProps }) => (
                      <div className="time-calender-main" {...getRootProps()}>
                        <Grid
                          container
                          rowSpacing={1}
                          columnSpacing={{ xs: 1, sm: 2, md: 3 }}
                        >
                          <Grid item xs={12}></Grid>
                          <Grid item xs={12}></Grid>
                        </Grid>
                      </div>
                    )}
                  </SidebarHeader>
                  <DateHeader unit={"primaryHeader"} />
                  <DateHeader />
                </TimelineHeaders>
              </Timeline>
            </div>
            
          </div>

        </div>
      )}
    </>
  );
};
