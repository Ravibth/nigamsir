import React, {useContext, useEffect, useState} from "react";
import {
  Grid, Tooltip,
} from "@mui/material";
import {currentBuLeaderMaster, currentCompetencyLeaderMaster, loadBUTreeMappingData, loadCompetencyMappingData} from "../util/util";
import {ICompetencyMaster} from "../../../common/interfaces/ICompetencyMaster";
import {IBUTreeMapping} from "../../../common/interfaces/IBuTreeMapping";
import ActionButton from "../../actionButton/actionButton";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import {AutocompleteSxProps} from "../../search/constant";
import CheckBoxDropdown from "../../dropdowns/CheckBoxDropdown";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { RolesListMaster } from "../../../common/enums/ERoles";
import uniq from "lodash/uniq";
import { GetUserRoleOptions, GetUserRoleOptionsPortfolio } from "../../../global/utils";
import { IRoleOption } from "../../function-bar/interface";
import { IEmployeeModel } from "../../../common/interfaces/IEmployeeModel";
import { getEmployeesSuperCoachOrCSCByMID } from "../../../services/configuration-services/configuration.service";
import { ROLE_VIEW_TYPE } from "../../Reports/Charts/Bar-Chart/scheduled_vs_variance/constant";
import { RoleDisplayNames,Role } from "../../../global/constant";

export const BuCompetencySelector = (props) => {
  
  const userContext = useContext(UserDetailsContext);
  const [selectedBu, setSelectedBu] = useState("");
  const [buTreeMappingMaster, setBuTreeMappingMaster] = useState<IBUTreeMapping[]>([]);
  const [currenBuTreeMapping, setCurrenBuTreeMapping] = useState<IBUTreeMapping[]>([]);
  const [competencyMapping, setCompetencyMapping] = useState<ICompetencyMaster[]>([]);
  const [currentCompetencyMapping, setCurrentCompetencyMapping] = useState<ICompetencyMaster[]>([]);
  const [useRoleOptions,setUseRoleOptions] = useState<IRoleOption[]>([]);
  const [employeesSuperCoachOrCSC, setEmployeesSuperCoachOrCSC] = useState<IEmployeeModel[]>([]);
  const [localRole,setLocalRole] = useState<string[]>([]);
  const excludedRoles = [RolesListMaster.SuperCoach, RolesListMaster.CSC, RolesListMaster.Employee]
    .map(role => role.toLowerCase().trim());

  const getEmployeesSuperCoachOrCSC = () => {
    return new Promise<IEmployeeModel[]>((resolve, reject) => {
      getEmployeesSuperCoachOrCSCByMID(userContext.employee_id)
        .then((response) => {
          resolve(response.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  useEffect(() => {
    Promise.all([
      loadBUTreeMappingData(),
      loadCompetencyMappingData(),
      getEmployeesSuperCoachOrCSC()
    ])
      .then((result) => {
        const buMappingData = result[0];
        const competencyMasters = result[1];
        const  employeesSuperCoachOrCSCByMID: IEmployeeModel[] = result[2] as IEmployeeModel[];
        setBuTreeMappingMaster(buMappingData);
        setCompetencyMapping(competencyMasters);
        setEmployeesSuperCoachOrCSC(employeesSuperCoachOrCSCByMID);
      })
      .catch((error) => {});
  }, []);

  useEffect(() => {
    if (userContext.role.includes(RolesListMaster.CEOCOO) || userContext.role.includes(RolesListMaster.SystemAdmin) || userContext.role.includes(RolesListMaster.Admin)) 
    {
      setCurrenBuTreeMapping(buTreeMappingMaster);
      setCurrentCompetencyMapping(competencyMapping);
    }
    else
    {
      const currentBuLeaderMas = currentBuLeaderMaster(buTreeMappingMaster,props.userLeaderRoles);
      const currentCompetencyLeaderMas = currentCompetencyLeaderMaster(
        competencyMapping,
        props.userLeaderRoles,
        props.competencyLeaderRoles,
        buTreeMappingMaster
      );

      //Bu's which belongs to user through competency
      const BUsOfCompetencies = uniq(
        currentCompetencyLeaderMas.map((item) => {
          var _t1 = buTreeMappingMaster.filter(
            (x) => x.bu_id === item.buId
          );
          if (_t1 && _t1.length > 0) {
            return _t1[0].bu_id;
          } else return;
        })
      );

      //From master list get bu details with buid of BUsOfCompetencies
      const BUsOfCompetenciesList = buTreeMappingMaster.filter((item) =>
        BUsOfCompetencies.includes(item.bu_id)
      );

      //Combine leader of bu's and & competency bu's
      const combinedBuList = uniq(
        [...currentBuLeaderMas, ...BUsOfCompetenciesList],
        (item) => item.bu_id
      );

      setCurrenBuTreeMapping(combinedBuList);
      setCurrentCompetencyMapping(currentCompetencyLeaderMas);
    }
    //set Role dropdown
    const roleOptions = GetUserRoleOption();
    setUseRoleOptions(roleOptions);
  }, [
    buTreeMappingMaster,
    props.userLeaderRoles,
    competencyMapping,
    props.competencyLeaderRoles,
  ]);

  function GetUserRoleOption(){
    let roleOptions = GetUserRoleOptionsPortfolio(userContext);

    const userRoleNames = roleOptions.map(opt => opt.roleName);//Role names from roleOptions as strings

    const filteredViewTypes = ROLE_VIEW_TYPE.filter(group =>       
      group.key === Role.Employee ||  // Always include Employee
      group.role.some(role => userRoleNames.includes(role.toString()))
    );      

    // IRoleOption format:
    const transformedOptions = filteredViewTypes.map(group => ({
      roleDisplayName: group.key.toString(),
      roleName: group.key.toString()
    }));

    //check if user is super coach or csc of any employee
    const reportingAndSupercoach = checkReportingAndSupercoach(employeesSuperCoachOrCSC);

    if(reportingAndSupercoach.CoSuperCoach){
      const newRole: IRoleOption =
      {
        roleDisplayName: RoleDisplayNames.CSC,
        roleName: RolesListMaster.CSC.toLowerCase().trim()
      };
      transformedOptions.push(newRole);
    }

    return transformedOptions;
  }

  function checkReportingAndSupercoach(employees: IEmployeeModel[]): { CoSuperCoach: boolean; is_supercoach_mid: boolean } 
  {
    if (!employees || employees.length === 0) {
      return { CoSuperCoach: false, is_supercoach_mid: false };
    }

    let CoSuperCoach = false;
    let hasSupercoach = false;

    for (const employee of employees) {
      if (employee.reporting_partner_mid !== undefined && employee.reporting_partner_mid !== null && employee.reporting_partner_mid == userContext.employee_id) {
        CoSuperCoach = true;
      }
      if (employee.supercoach_mid !== undefined && employee.supercoach_mid !== null && employee.supercoach_mid == userContext.employee_id) {
        hasSupercoach = true;
      }
      if (CoSuperCoach && hasSupercoach) {
        break;
      }
    }
    return { CoSuperCoach: CoSuperCoach, is_supercoach_mid: hasSupercoach };
  }

  function setBu(selectedBuName) {
    const selectedBU = currenBuTreeMapping.find((bu) => bu.bu === selectedBuName);
    const selectedBuId = selectedBU?.bu_id ?? null;
    setSelectedBu(selectedBuId)
    props.setValue("selectedBU", selectedBuName);
    props.setValue("selectedBUId", selectedBuId);
    props.setValue(props.competencyName, []);
  }

  const hasExcludedRole = localRole?.some(
    role => excludedRoles.includes(role.toLowerCase().trim())
  );

  const handleWrapperSearch = () => {
    const formValues = props.getValues();
    const selectedCompetencies = formValues[props.competencyName] || [];
    
    if (selectedCompetencies.length === 0 && selectedBu) {
      const allCompetencies = currentCompetencyMapping.filter(
        option => option.buId === selectedBu
      );
      let competencyIds = allCompetencies.map(c => c.competencyId);
      props.setValue(props.competencyName, competencyIds);
    }
    
    setTimeout(() => {
      props.handleSearch();
    }, 100);
  };

  const handleRoleChange = (value) => {
    const selectedRoleNames = value ? [value.roleName] : [];
    setLocalRole(selectedRoleNames);
    props.setSearchRoles(selectedRoleNames);
    
    // Reset BU dropdown when role changes
    setSelectedBu("");
    props.setValue("selectedBU", null);
    props.setValue("selectedBUId", null);
    props.setValue(props.competencyName, []);
  };

  return (
    <Grid item xs={12} sm="auto" display="flex" gap={2} flexWrap="nowrap" alignItems="center" ml={-3}>
      <ControllerAutocompleteFilteredOptionsTextfield
        name={"role"}
        control={props.control}
        sx={{
          ...AutocompleteSxProps,
          width: 200, fontSize: '0.5rem',
          '& .MuiInputBase-root': { padding: '4px 8px' },
          '& .MuiAutocomplete-inputRoot': { padding: '2px 6px' },
          '& .MuiInputBase-input': {
            height: '25px',
          },
        }}
        options={useRoleOptions || []}
        getOptionLabel={(option: IRoleOption) => option?.roleDisplayName ?? ""}
        onChange={handleRoleChange}
        label="Role*"
        rules={{ required: true }}
        error={!!props.errors.selectedBU}
        FullWidth={true}
      />
      {props.isLeader && localRole?.length > 0 && !hasExcludedRole && (
        <>
          <Tooltip
            title={props.getValues("selectedBU") ? 
            `${currenBuTreeMapping.find(bu => bu.bu_id === props.getValues("selectedBUId"))?.bu}` : 
            "Please select a Business Unit"}
            placement="top"
            arrow
            enterDelay={500}>
            <div>
              <ControllerAutocompleteFilteredOptionsTextfield
                name={"selectedBU"}
                control={props.control}
                defaultValue={""}
                sx={{
                  ...AutocompleteSxProps,
                  width: 200, fontSize: '0.5rem',
                  '& .MuiInputBase-root': { padding: '4px 8px' },
                  '& .MuiAutocomplete-inputRoot': { padding: '2px 6px' },
                  '& .MuiInputBase-input': {
                    height: '25px',
                  },
                }}
                options={uniq(currenBuTreeMapping?.map((bu) => bu.bu))}
                onChange={(selectedBuName) => setBu(selectedBuName)}
                label="Business unit*"
                rules={{ required: true }}
                error={!!props.errors.selectedBU}
                FullWidth={true}
              />
            </div>
          </Tooltip>
          <CheckBoxDropdown
            name={props.competencyName}
            control={props.control}
            options={currentCompetencyMapping.filter(option => option.buId === selectedBu)}
            label="Competency"
            getOptionLabel={(option) => option.competency}
            isOptionEqualToValue={(opt, val) => opt.competencyId === val.competencyId}
            renderTags={(selected, getTagProps) =>
              selected.length ? (                                
                <span
                {...(selected.length === 1 ? getTagProps({ index: 0 }) : {})}
                style={{ whiteSpace: "nowrap", fontSize: "1rem", overflow: "hidden" }}
              >
                  {selected[0].competency}
                  {selected.length > 1 && ` +${selected.length - 1}`}
                </span>
              ) : null
            }
            sx={{
              ...AutocompleteSxProps,
              width: 340,
              fontSize: "0.5rem",
              '& .MuiInputBase-root': { padding: '4px 8px' },
              '& .MuiAutocomplete-inputRoot': { padding: '2px 6px' },
              '& .MuiInputBase-input': {
                height: '25px',
              },
            }}
          />
        </>
      )}

      <Grid className="filter-btn-container" ml="10px" mb="20px">
        <ActionButton
          label={"Search"}
          onClick={handleWrapperSearch}
          disabled={false}
          type={"button"}
          textTransform="none"
        />
      </Grid>
    </Grid>
  );
};
