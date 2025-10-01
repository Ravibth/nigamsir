import { useForm } from "react-hook-form";
import { useContext, useEffect, useState } from "react";
import { LoaderContext } from "../../../contexts/loaderContext";
import { SnackbarContext } from "../../../contexts/snackbarContext";
import * as util from "./util";
import "./style.css";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import { AutocompleteSxProps } from "../../search/constant";
export default function SkillSearch(props: any) {
  const { control,reset,setValue  } = useForm({ mode: "onTouched" });

  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [skills, setSkills] = useState([]);
  const [skillData, setSkillData] = useState([]);
  const [resetKey, setResetKey] = useState(0);

  useEffect(() => {
    getAllSkillMaster();
  }, []);

  useEffect(() => {
    if (props.isReset || props.isFilterApplied) {
      // Clear the form value
      setValue("items", []);
      reset({ items: [] });

      props.setEmployeeSkillResults([]);
      
      // Increment resetKey to force re-render
      setResetKey(prev => prev + 1);
            
      if (props.onResetComplete) {
        setTimeout(() => props.onResetComplete(), 100);
      }
    }
  }, [props.isReset, reset, setValue,props.isFilterApplied]);

  useEffect(() => {    
    if (props.filterParameters.businessUnit && props.filterParameters.businessUnit.length > 0) {
      if (props.filterParameters.competency && props.filterParameters.competency.length > 0) {        
        let skillFilteredData = skillData.filter(x => x.skill_Mapping && x.skill_Mapping.some(mapping => props.filterParameters.competency.includes(mapping.competency)))
        const skillsOptions = util.getSkillsIDropdownOptions(skillFilteredData);
        setSkills(skillsOptions);
        props.setEmployeeSkillResults(skillsOptions);
      }
      else {
        //Add code if Skills required when only BU filter applied
      }
    }
    else {
      getAllSkillMaster();
    }
  }, [props.filterParameters, props.isFilterApplied])


  const getAllSkillMaster = async () => {
    try {
      loaderContext.open(true);
      const skillsData = await util.fetchAllSkill();
      setSkillData(skillsData);
      const skillsOptions = util.getSkillsIDropdownOptions(skillsData);
      setSkills(skillsOptions);
      loaderContext.open(false);
    } catch (e) {
      loaderContext.open(false);
      snackbarContext.displaySnackbar("Something went Wrong.", "error");
    }
  };

  const onSearchTextChange = async (e: any) => {
    const skills = e.map((a) => a?.labelId);
    const searchResults = await util.getSearchSkills(skills);
    props.setEmployeeSkillResults(searchResults);
  };

  return (
    <ControllerAutocompleteFilteredOptionsTextfield
      resetKey={resetKey}
      name="items"
      placeholder={"Search Skill"}
      multiple={true}
      control={control}
      filterSelectedOptions={true}
      sx={AutocompleteSxProps}
      defaultValue={[]}
      options={skills}
      onChange={(e: any) => {
        onSearchTextChange(e);
      }}
    />
  );
}
