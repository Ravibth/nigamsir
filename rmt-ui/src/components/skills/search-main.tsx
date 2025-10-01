import { useState, useEffect, useContext } from "react";
import SkillSearch from "./skills-search/skills-search";
import EmployeeSkillSearch from "./employee-skill-search/employee-skill-search";
import ControllerSwitch from "../controllers/controller-switch";
import { useForm } from "react-hook-form";
import { Box, Button, Grid, Tooltip, Typography } from "@mui/material";
import {
  groupDisplayType,
  columnDefs,
  groupValueFormatter,
  // autoGroupColumnDef,
} from "./skills-search/constant";
import { RequisitionHeaderSxProps } from "../create-requisition-main/constant";
import SkillsSearchResult from "./skills-search/skills-search-result";
import SkillSearchFilterForm from "./skills-search/skill-search-filter";
import { ICompetencyMaster } from "../../common/interfaces/ICompetencyMaster";
import { currentBuLeaderMaster, currentCompetencyLeaderMaster, IReportDashboardFilterControl, loadBUTreeMappingData, loadCompetencyMappingData } from "../Reports/Filters/uitls";
import { GetFilterDefaultValueOnTheBasisOfRole } from "../Reports/util";
import { filterIconButton } from "../filter/project-filter/constant";
import MainFilter from "../../common/images/MainFilter.png";
import {
  UserDetailsContext,
  IUserDetailsContext,
} from "../../contexts/userDetailsContext";
import { RolesListMaster } from "../../common/enums/ERoles";
import { IBUTreeMappingListByMID } from "../../common/interfaces/IBUTreeMappingListByMID";
import { getBUTreeMappingListByMID, getEmployeesSuperCoachOrCSCByMID } from "../../services/configuration-services/configuration.service";
import { getAllCompetencyByMID } from "../../services/wcgt-master-services/wcgt-master-services";
//import { IEmployeeModel } from "../../common/interfaces/IEmployeeModel";
import FilterAltIcon from "@mui/icons-material/FilterAlt";
import { VerticalCenterAlignSxProps } from "../common-allocation/user-info-timeline-group/style";
import { routeToEmployeeProfile } from "../../global/utils";

export enum ESearchType {
  Skill = "Skill",
  Employee = "Employee",
}

const SearchMain = () => {
  const { control } = useForm({ mode: "onTouched" });
  const [searchType, setSearchType] = useState<ESearchType>(ESearchType.Skill);
  const [employeeSkillResults, setEmployeeSkillResults] = useState([]);
  const [buTreeMappingMaster, setBuTreeMappingMaster] = useState([]);
  const [currenBuTreeMapping, setCurrenBuTreeMapping] = useState([]);
  const [competencyMapping, setCompetencyMapping] = useState<ICompetencyMaster[]>([]);
  const [currentCompetencyMapping, setCurrentCompetencyMapping] = useState<ICompetencyMaster[]>([]);
  const [isFilterApplied, setisFilterApplied] = useState(false);
  const [filterParameters, setFilterParameters] = useState<IReportDashboardFilterControl>(GetFilterDefaultValueOnTheBasisOfRole());
  const [isFilterOpen, setIsFilterOpen] = useState<boolean>(false);
  const userContext: IUserDetailsContext = useContext(UserDetailsContext);
  const [userLeaderRoles, setUserLeaderRoles] = useState<IBUTreeMappingListByMID | null>(null);
  const [competencyLeaderRoles, setCompetencyLeaderRoles] = useState<ICompetencyMaster[]>([]);
  const userDetailsContext = useContext(UserDetailsContext);
  const [isReset,setIsReset] = useState(false);
  const columnDefsGrid = [
    {
      headerName: "Designation",
      valueFormatter: groupValueFormatter,
      field: "designation",
      flex: 1.1,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      menuTabs: ["filterMenuTab"],
      tooltipField: "designation",
      rowGroup: true,
      hide: true,
      sortable: true,
      unSortIcon: true,
    },
    {
      headerName: "Employee Name",
      field: "name",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      hide: true,
      rowGroup: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "name",
      flex: 1,
      cellRenderer: (params: any) => {
      if (!params.node.group) {
        return params.value;
      }
      return (
        <span
        style={{ cursor: "pointer", color: "blue" }}
        onClick={(e) => {
          e.stopPropagation();
          const allRows = params.node.allLeafChildren.map(
            (child: any) => child.data
          );
           routeToEmployeeProfile(`/employee-profile/${allRows?.[0]?.email}`);
        }}
      >
        {params.value}
      </span>
      );
    },
    },
    {
      headerName: "Skill Name",
      field: "skillName",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "skillName",
      flex: 1,
    },
    {
      headerName: "Proficiency",
      field: "proficiency",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "proficiency",
      flex: 1,
    },
    {
      headerName: "Business Unit",
      field: "business_unit",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "business_unit",
      flex: 1,
    },
    {
      headerName: "Competency",
      field: "competency",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "proficiency",
      flex: 1,
    },
    {
      headerName: "Location",
      field: "location",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "location",
      flex: 1,
    },
  ];

  const loadLeadersInformationFromBUTreeMapping = () => {
    return new Promise((resolve, reject) => {
      getBUTreeMappingListByMID(userContext.employee_id)
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
      getAllCompetencyByMID(userContext.employee_id)
        .then((response) => {
          resolve(response);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  useEffect(() => {
    if (userContext && userContext?.employee_id?.length > 0) {
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

  useEffect(() => {
    setisFilterApplied(false);
    setEmployeeSkillResults([]);
  }, [searchType]);

  useEffect(() => {
    Promise.all([
      loadBUTreeMappingData(),
      loadCompetencyMappingData(),
    ])
      .then((result) => {
        const buMappingData = result[0];
        const competencyMasters = result[1];
        setBuTreeMappingMaster(buMappingData);
        setCompetencyMapping(competencyMasters);
      })
      .catch((error) => { });
  }, []);

  useEffect(() => {
    if (
      userContext.role.includes(RolesListMaster.CEOCOO) ||
      userContext.role.includes(RolesListMaster.SystemAdmin)
    ) {
      setCurrenBuTreeMapping(buTreeMappingMaster);
      setCurrentCompetencyMapping(competencyMapping);
    }
    else {
      const result = currentBuLeaderMaster(
        buTreeMappingMaster,
        userLeaderRoles
      );

      setCurrenBuTreeMapping(result);
      const competencyMappingResult = currentCompetencyLeaderMaster(
        competencyMapping,
        userLeaderRoles,
        competencyLeaderRoles,
        buTreeMappingMaster
      );
      setCurrentCompetencyMapping(competencyMappingResult);
    }
  }, [
    userLeaderRoles,
    buTreeMappingMaster,
    competencyMapping,
  ]);

  function reset(){
    setEmployeeSkillResults([]);    
    setIsReset(true);
  }

  return (
    <>
      <Typography
        component={"div"}
        className="skill-master-heading requisition-header-title"
      >
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          Skill Search
        </Typography>
      </Typography>


      <SkillSearchFilterForm
        setOpenFilter={setIsFilterOpen}
        setisFilterApplied={setisFilterApplied}
        isFilterOpen={isFilterOpen}
        buTreeMappingMaster={buTreeMappingMaster}
        //chk for such bu from competency which do not exist in master        
        filterParameters={filterParameters}
        setFilterParameters={setFilterParameters}
        GetFilterDefaultValueOnTheBasisOfRole={
          GetFilterDefaultValueOnTheBasisOfRole
        }
        currenBuTreeMapping={currenBuTreeMapping}
        currentCompetencyMapping={currentCompetencyMapping}
        reset = {reset}
      />

      <Grid container spacing={2} sx={{ p: 1 }} alignItems="center">
        <Grid item xs={4}>
          <Typography component="span">{ESearchType.Skill}</Typography>
          <Typography component="span" sx={{ marginLeft: 1, marginRight: 1 }}>
            <ControllerSwitch
              name="searchType"
              control={control}
              onChange={(e) => {
                setSearchType(e ? ESearchType.Employee : ESearchType.Skill);
              }}
            />
          </Typography>
          <Typography component="span">{ESearchType.Employee}</Typography>
        </Grid>

        <Grid item xs={8} /> {/* keeps layout balanced */}
        
        <Grid item xs={4}>
          {searchType === ESearchType.Skill ? (
            <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
              <Box sx={{ flex: 1 }}>
                <SkillSearch
                  setEmployeeSkillResults={setEmployeeSkillResults}
                  filterParameters={filterParameters}
                  isFilterApplied={isFilterApplied}
                  isReset={isReset}
                />
              </Box>

              <Button
                variant="outlined"
                sx={filterIconButton}
                onClick={() => setIsFilterOpen(true)}
                style={{marginBottom:"10px"}}
              >
                <img src={MainFilter} alt="upload" />
              </Button>

              {isFilterApplied && (
                <Tooltip title="Filters applied">
                  <FilterAltIcon fontSize="large" />
                </Tooltip>
              )}
            </Box>
          ) : (
            <EmployeeSkillSearch setEmployeeSkillResults={setEmployeeSkillResults} />
          )}
        </Grid>


        <Grid item xs={12}>
          <SkillsSearchResult
            groupDisplayType={groupDisplayType}
            rowsData={employeeSkillResults}
            columnDefs={columnDefsGrid}
          />
        </Grid>
      </Grid>
    </>
  );
};
export default SearchMain;
