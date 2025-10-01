import { Button } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import {
  filterIconButton,
  loadBUTreeMappingData,
  loadLocationMaster,
  designationMaster,
  currentBuLeaderMaster,
  loadCompetencyMappingData,
  currentCompetencyLeaderMaster,
} from "./uitls";
import ReportDashboardFilterForm from "./report-filter-form";
import MainFilter from "../../../common/images/MainFilter.png";
import ReportDashboardFilterFormEmployeeView from "./report-filter-form-employee-view";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import {
  UserDetailsContext,
  IUserDetailsContext,
} from "../../../contexts/userDetailsContext";
import { RolesListMaster } from "../../../common/enums/ERoles";

const ReportAndDashboardFilter = (props) => {
  const [buTreeMappingMaster, setBuTreeMappingMaster] = useState([]);
  const [locationMaster, setLocationMaster] = useState([]);
  const [designation, setDesignation] = useState([]);
  const [currenBuTreeMapping, setCurrenBuTreeMapping] = useState([]);
  const [competencyMapping, setCompetencyMapping] = useState<
    ICompetencyMaster[]
  >([]);
  const [currentCompetencyMapping, setCurrentCompetencyMapping] = useState<
    ICompetencyMaster[]
  >([]);
  const userContext: IUserDetailsContext = useContext(UserDetailsContext);

  useEffect(() => {
    Promise.all([
      loadBUTreeMappingData(),
      loadLocationMaster(),
      designationMaster(),
      loadCompetencyMappingData(),
    ])
      .then((result) => {
        const buMappingData = result[0];
        const locationData = result[1];
        const designationTemp = result[2];
        const competencyMasters = result[3];
        setBuTreeMappingMaster(buMappingData);
        setLocationMaster(locationData);
        setDesignation(designationTemp);
        setCompetencyMapping(competencyMasters);
      })
      .catch((error) => {});
  }, []);
  useEffect(() => {
    if (
      userContext.role.includes(RolesListMaster.CEOCOO) ||
      userContext.role.includes(RolesListMaster.SystemAdmin)
    ) {
      setCurrenBuTreeMapping(buTreeMappingMaster);
      setCurrentCompetencyMapping(competencyMapping);
    } else {
      const result = currentBuLeaderMaster(
        buTreeMappingMaster,
        props.userLeaderRoles
      );

      setCurrenBuTreeMapping(result);
      const competencyMappingResult = currentCompetencyLeaderMaster(
        competencyMapping,
        props.userLeaderRoles,
        props.competencyLeaderRoles,
        buTreeMappingMaster
      );
      setCurrentCompetencyMapping(competencyMappingResult);
    }
  }, [
    buTreeMappingMaster,
    props.userLeaderRoles,
    competencyMapping,
    props.competencyLeaderRoles,
  ]);
  return (
    <>
      <Button
        variant="outlined"
        sx={filterIconButton}
        onClick={() => {
          props.setOpenFilter(true);
        }}
      >
        {/* <FilterAltOutlinedIcon /> */}
        <img src={MainFilter} alt="upload" />
      </Button>
      {props.isFilterOpen && (
        <>
          {(props.toggleValue == "2" || props.toggleValue == 2) &&
          props.isEmployeeViewGraph ? (
            //Employee view filter
            <ReportDashboardFilterFormEmployeeView
              setOpenFilter={props.setOpenFilter}
              setisFilterApplied={props.setisFilterApplied}
              isFilterOpen={props.isFilterOpen}
              buTreeMappingMaster={buTreeMappingMaster}
              locationData={locationMaster}
              designation={designation}
              filterParameters={props.filterParameters}
              setFilterParameters={props.setFilterParameters}
              GetFilterDefaultValueOnTheBasisOfRole={
                props.GetFilterDefaultValueOnTheBasisOfRole
              }
              isEmployeeViewGraph={props.isEmployeeViewGraph}
            />
          ) : (
            //Leader view filter
            <ReportDashboardFilterForm
              setOpenFilter={props.setOpenFilter}
              setisFilterApplied={props.setisFilterApplied}
              isFilterOpen={props.isFilterOpen}
              buTreeMappingMaster={buTreeMappingMaster}
              //chk for such bu from competency which do not exist in master
              currentReportTab={props.toggleValue}
              locationData={locationMaster}
              designation={designation}
              filterParameters={props.filterParameters}
              setFilterParameters={props.setFilterParameters}
              GetFilterDefaultValueOnTheBasisOfRole={
                props.GetFilterDefaultValueOnTheBasisOfRole
              }
              currenBuTreeMapping={currenBuTreeMapping}
              currentCompetencyMapping={currentCompetencyMapping}
              isEmployeeViewGraph={props.isEmployeeViewGraph}
            />
          )}
        </>
      )}
    </>
  );
};

export default ReportAndDashboardFilter;
