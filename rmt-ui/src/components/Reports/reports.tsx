import { Box, Tab, Tabs, ToggleButton, ToggleButtonGroup } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import CustomReportModal from "./Modal/Modal";
import ColumnChart from "./Charts/Column-Chart/column-chart";
import { ISelectedChartData } from "./interface";
import ReportAndDashboardFilter from "./Filters/report-filter";
import dayjs from "dayjs";
import { IReportDashboardFilterControl } from "./Filters/uitls";
import { GetFilterDefaultValueOnTheBasisOfRole } from "./util";
import { TOGGLE_CONSTANTS } from "./constant";
import TabularChart from "./Charts/Tabular-Chart/tabular-chart-data";
import "./report.scss";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { getBUTreeMappingListByMID, getEmployeesSuperCoachOrCSCByMID } from "../../services/configuration-services/configuration.service";
import { IBUTreeMappingListByMID } from "../../common/interfaces/IBUTreeMappingListByMID";
import ReportCustomTabPanel from "./ReportTabs/report-custom-tab-panel";
import { RolesListMaster } from "../../common/enums/ERoles";
import BarCharts from "./Charts/Bar-Chart/scheduled_vs_variance/bar_chart";
import ReportDashboard from "./Reports/report-dashboard";
import { getAllCompetencyByMID } from "../../services/wcgt-master-services/wcgt-master-services";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { useSearchParams } from "react-router-dom";
import { Tooltip, Typography, Grid } from "@mui/material";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import { VerticalCenterAlignSxProps } from "../common-allocation/user-info-timeline-group/style";
import { IEmployeeModel } from "../../common/interfaces/IEmployeeModel";

const Reports = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const tabIndex = searchParams?.get("tab");
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [selectedChartData, setSelectedChartData] =
    useState<ISelectedChartData>(null);
  const [isFilterOpen, setIsFilterOpen] = useState<boolean>(false);
  const [toggleValue, setToggleValue] = useState<string>(TOGGLE_CONSTANTS.TIME);
  const [filterParameters, setFilterParameters] =
    useState<IReportDashboardFilterControl>(
      GetFilterDefaultValueOnTheBasisOfRole()
    );
  const [rowData, setRowData] = useState<any[]>([]);
  const [isFilterApplied, setisFilterApplied] = useState(false);
  const [rowTabularData, setRowTabularData] = useState<any[]>([]);
  const [colDef, setColDef] = useState<any[]>([]);
  const [value, setValue] = useState(0);
  const [userLeaderRoles, setUserLeaderRoles] =
    useState<IBUTreeMappingListByMID | null>(null);
  const [competencyLeaderRoles, setCompetencyLeaderRoles] = useState<
    ICompetencyMaster[]
  >([]);
  const [isLeaderRole, setIsLeaderRole] = useState(false);
  const [isOffSlnLeaderRole, setIsOffSlnLeaderRole] = useState(false);
  const [isCeoCooRole, setIsCeoCooRole] = useState(false);
  const [isEmployeeOnlyRole, setIesEmployeeOnlyRole] = useState(false);
  const userDetailsContext = useContext(UserDetailsContext);
  const [isUserSystemAdmin, setUserIsSystemAdmin] = useState(false);
  const [isUserSuperCoach, setIsUserSuperCoach] = useState(false);
  const [isUserCSC, setIsUserCSC] = useState(false);
  const [isEmployeeViewGraph, setIsEmployeeViewGraph] = useState(true);
  const [currentUserRoleView, setCurrentUserRoleView] = useState<any[]>([]);
  const [employeesSuperCoachOrCSC, setEmployeesSuperCoachOrCSC] = useState<IEmployeeModel[]>([]);
  const [schedularViewChartType,setSchedularViewChartType] = useState("");

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

  const getEmployeesSuperCoachOrCSC = () => {
    return new Promise<IEmployeeModel[]>((resolve, reject) => {
      getEmployeesSuperCoachOrCSCByMID(userDetailsContext.employee_id)
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
  const updateIsLeaderAndEmployyeeRole = (
    leaderRoles: IBUTreeMappingListByMID,
    competencyLeaderRole: ICompetencyMaster[],
    employeesSuperCoach: IEmployeeModel[]
  ) => {
    const isSystemAdmin = userDetailsContext.role.includes(
      RolesListMaster.SystemAdmin
    );

    const isLeader =
      userDetailsContext.role.includes(RolesListMaster.Leaders) &&
      leaderRoles &&
      (Object.keys(leaderRoles?.bu)?.length > 0 ||
        competencyLeaderRole?.length > 0);

    const _isOffSlnLeader =
      userDetailsContext.role.includes(RolesListMaster.Leaders) &&
      leaderRoles &&
      (Object.keys(leaderRoles?.offerings)?.length > 0 ||
        Object.keys(leaderRoles?.offerings)?.length > 0);
    const isEmp = userDetailsContext.role.includes(RolesListMaster.Employee); //&&

    const isCeoCoo = userDetailsContext.role.find(
      (m) =>
        m.toLowerCase().trim() === RolesListMaster.CEOCOO.toLowerCase().trim()
    );
    const superCoach = userDetailsContext.role.find(
      (m) =>
        m.toLowerCase().trim() === RolesListMaster.SuperCoach.toLowerCase().trim()
    );
    
    setIsCeoCooRole(isCeoCoo ? true : false);
    setIsLeaderRole(isLeader);
    setIsOffSlnLeaderRole(_isOffSlnLeader);
    setIesEmployeeOnlyRole(isEmp);
    setUserIsSystemAdmin(isSystemAdmin);

     const _currentRoleView = [];
     if (isCeoCoo){
      _currentRoleView.push(RolesListMaster.CEOCOO);
     }
     if (isSystemAdmin){
      _currentRoleView.push(RolesListMaster.SystemAdmin);
     }
     if (isLeader){
      _currentRoleView.push(RolesListMaster.Leaders);
     }    
    if (superCoach){
      _currentRoleView.push(RolesListMaster.SuperCoach);
      
    }
    if (employeesSuperCoach.length > 0) {
      const _isSuperCoach = employeesSuperCoach.filter(a=>   a?.supercoach_mid && userDetailsContext?.employee_id &&  a?.supercoach_mid.toLowerCase() == userDetailsContext?.employee_id?.toLowerCase())?.length > 0;
      const _isCSC = employeesSuperCoach.filter(a=> a?.reporting_partner_mid && userDetailsContext?.employee_id &&  a?.reporting_partner_mid?.toLowerCase() == userDetailsContext?.employee_id?.toLowerCase())?.length > 0;
      setIsUserSuperCoach(_isSuperCoach);
      setIsUserCSC(_isCSC);
      if ((superCoach || _isSuperCoach) && !_currentRoleView.includes(RolesListMaster.SuperCoach)){        
        _currentRoleView.push(RolesListMaster.SuperCoach);
        
      }
      if (_isCSC){
        _currentRoleView.push(RolesListMaster.CSC);
      }
    }
    
     _currentRoleView.push(RolesListMaster.Employee);
     setCurrentUserRoleView(_currentRoleView)
  };

  useEffect(() => {
    if (userDetailsContext && userDetailsContext?.employee_id?.length > 0) {
      Promise.all([
        loadLeadersInformationFromBUTreeMapping(),
        loadLeadersInformationFromCompetencyMapping(),
        getEmployeesSuperCoachOrCSC()
      ])
        .then((response) => {
          const result: IBUTreeMappingListByMID =
            response[0] as IBUTreeMappingListByMID;
          const resultSuperCoach: IEmployeeModel[] = response[2] as IEmployeeModel[];
          setUserLeaderRoles(result);
          setCompetencyLeaderRoles(response[1]);
          setEmployeesSuperCoachOrCSC(resultSuperCoach);
          updateIsLeaderAndEmployyeeRole(result, response[1],resultSuperCoach );
          setValue(Number(tabIndex));
        })
        .catch((err) => {
          // throw err;
        });
    }
  }, [userDetailsContext]);

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setFilterParameters(GetFilterDefaultValueOnTheBasisOfRole());
    setValue(newValue);
    setisFilterApplied(false);
  };

  const toggleChangeHandler = (
    event: React.MouseEvent<HTMLElement>,
    newAlignment: string
  ) => {
    setToggleValue(newAlignment);
  };
  const DATETIME_FORMAT = "YYYY-MM-DD";

  return (
    <div>
      <div className="main-toggle-div">
        <div>
          {value !== 0 &&
          (value !== 2 || (value === 2 && !isEmployeeViewGraph)) ? (
            <Grid container spacing={2}>
              <Grid item>
                <ReportAndDashboardFilter
                  toggleValue={value}
                  setisFilterApplied={setisFilterApplied}
                  isEmployeeViewGraph={isEmployeeViewGraph}
                  isFilterOpen={isFilterOpen}
                  setOpenFilter={setIsFilterOpen}
                  filterParameters={filterParameters}
                  setFilterParameters={setFilterParameters}
                  GetFilterDefaultValueOnTheBasisOfRole={
                    GetFilterDefaultValueOnTheBasisOfRole
                  }
                  competencyLeaderRoles={competencyLeaderRoles}
                  userLeaderRoles={userLeaderRoles}
                />
              </Grid>
              <Grid item>
                {isFilterApplied && (
                  <Typography component="span" sx={VerticalCenterAlignSxProps}>
                    <Tooltip title="Filters applied">
                      <FilterAltIcon fontSize="large" />
                    </Tooltip>
                  </Typography>
                )}
              </Grid>
            </Grid>
          ) : (
            ""
          )}
        </div>

        <div>
          {value !== 0 ? (
            <ToggleButtonGroup
              value={toggleValue}
              exclusive
              onChange={toggleChangeHandler}
              aria-label="text alignment"
            >
              <ToggleButton
                value={TOGGLE_CONSTANTS.COST}
                aria-label="left aligned"
              >
                Cost(INR)
              </ToggleButton>
              <ToggleButton value={TOGGLE_CONSTANTS.TIME} aria-label="centered">
                Time(Hours)
              </ToggleButton>
            </ToggleButtonGroup>
          ) : (
            <></>
          )}
        </div>
      </div>
      <div>
        <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
          <Tabs
            value={value}
            onChange={handleTabChange}
            aria-label="basic tabs example"
          >
            <Tab
              sx={isEmployeeOnlyRole ? {} : { display: "none" }}
              label="Statistics Report"
            />
            <Tab
              sx={
                isLeaderRole || isCeoCooRole || isUserSystemAdmin
                  ? {}
                  : { display: "none" }
              }
              label="Capacity Utilization Chart"
            />
            <Tab
              sx={
                isLeaderRole ||
                isOffSlnLeaderRole ||
                isEmployeeOnlyRole ||
                isUserSystemAdmin
                  ? {}
                  : { display: "none" }
              }
              label="Scheduled Vs Variance Chart"
            />
          </Tabs>
        </Box>
        {(isEmployeeOnlyRole || isUserSystemAdmin) && (
          <ReportCustomTabPanel
            hidden={!isEmployeeOnlyRole && !isUserSystemAdmin}
            value={value}
            index={0}
          >
            {/* Summary Statistics chart report  */}
            <ReportDashboard
              chartType={toggleValue}
              isFilterApplied={isFilterApplied}
              setisFilterApplied={setisFilterApplied}
              filterParameters={filterParameters}
              setFilterParameters={setFilterParameters}
            ></ReportDashboard>
          </ReportCustomTabPanel>
        )}
        {(isLeaderRole || isCeoCooRole || isUserSystemAdmin || isUserSuperCoach || isUserCSC) && (
          <ReportCustomTabPanel hidden={false} value={value} index={1}>
            {/* Capacity Utilization chart report  */}
            <ColumnChart
              isFilterApplied={isFilterApplied}
              setisFilterApplied={setisFilterApplied}
              selectedChartData={selectedChartData}
              setSelectedChartData={setSelectedChartData}
              toggleValue={toggleValue}
              setIsOpen={setIsOpen}
              chartTitle={"Capacity Utilization"}
              startDate={dayjs(new Date()).format(DATETIME_FORMAT)}
              endDate={dayjs(new Date()).format(DATETIME_FORMAT)}
              filterParameters={filterParameters}
              setFilterParameters={setFilterParameters}
              userLeaderRoles={userLeaderRoles}
              competencyLeaderRoles={competencyLeaderRoles}
              setRowData={setRowData}
              setColDef={setColDef}
              setRowTabularData={setRowTabularData}
              setIsEmployeeViewGraph={setIsEmployeeViewGraph}
              schedularViewChartType = {schedularViewChartType}
            />
            {/* AG grid chart report  */}
            <TabularChart
              rowData={rowTabularData}
              isFilterApplied={isFilterApplied}
              setisFilterApplied={setisFilterApplied}
              setRowData={setRowTabularData}
              colDef={colDef}
              setColDef={setColDef}
            />
          </ReportCustomTabPanel>
        )}
        {(isLeaderRole ||
          isOffSlnLeaderRole ||
          isEmployeeOnlyRole ||
          isUserSystemAdmin) && (
          <ReportCustomTabPanel
            hidden={!isLeaderRole && !isOffSlnLeaderRole && !isEmployeeOnlyRole}
            value={value}
            index={2}
          >
            {/* Schedule vs Variance chart report  */}
            <BarCharts
              chartType={toggleValue}
              isFilterApplied={isFilterApplied}
              setisFilterApplied={setisFilterApplied}
              filterParameters={filterParameters}
              setFilterParameters={setFilterParameters}
              setIsEmployeeViewGraph={setIsEmployeeViewGraph}
              isEmployeeViewGraph={isEmployeeViewGraph}
              currentUserRoleView={currentUserRoleView}
              setSchedularViewChartType= {(viewType: string)=>{setSchedularViewChartType(viewType)}}
              
            />
          </ReportCustomTabPanel>
        )}
      </div>

      {/* Chart model popup control */}
      <CustomReportModal
        isOpen={isOpen}
        setIsOpen={setIsOpen}
        selectedChartData={selectedChartData}
        setRowData={setRowData}
        rowData={rowData}
        colDef={colDef}
        setColDef={setColDef}
      />
    </div>
  );
};

export default Reports;
