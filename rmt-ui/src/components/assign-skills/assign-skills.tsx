import {
  Autocomplete,
  Box,
  Button,
  TextField,
  Typography,
} from "@mui/material";

import React, { useContext, useEffect, useState } from "react";

import * as constant from "./constant";
import { useForm } from "react-hook-form";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";

const AssignSkills = (props: any) => {
  const { control } = useForm();
  const { allSkills, currentSkills } = props;
  const [projectSkills, setProjectSkills] = useState([] as any);
  const [allSkillsOptions, setAllSkillsOptions] = useState(allSkills);
  const [selectSkills, setSelectSkills] = useState(currentSkills);
  const [isRequireRender, setIsRequireRender] = useState(true);
  const { setProjectUpdateSkills } = useContext(ProjectUpdateDetailsContext);

  const submitFormHandler = (event: any) => {
    event.preventDefault();
    // console.log("Form Submitted");
  };
  const skillChangeHandler = (event: any, newValue: any) => {
    const data: any[] = [];
    // console.log(newValue);
    newValue?.map((item: any) => {
      const skill = projectSkills.find((a: any) => a?.label == item?.label);
      if (!skill) {
        // item.isactive = false;
        item.id = 0;
      } else {
        item.id = skill.id;
      }
      data.push(item);
    });
    const demandSkils = projectSkills.concat(data);
    // console.log("projectDetamds", demandSkils);
    const uniqueValues = Array.from(new Set(demandSkils));
    // console.log("projectDetamds-2", uniqueValues);
    uniqueValues.map((item: any) => {
      const skill = newValue.find((a: any) => a?.label == item?.label);
      if (!skill) {
        item.isactive = false;
      }
    });
    setProjectSkills(uniqueValues);
    setSelectSkills(newValue);
    setProjectUpdateSkills(data);
    props.handleSkillChange(uniqueValues);
  };

  useEffect(() => {
    getAllSkillsOptions();
  }, [selectSkills]);

  useEffect(() => {
    if (isRequireRender) {
      const data: any[] = [];
      allSkills?.map((item: any) => {
        const el = selectSkills.find((a: any) => a?.label == item?.label);
        if (!el) {
          data.push(item);
        }
      });
      if (data.length > 0) setIsRequireRender(false);
      // setAllSkillsOptions(data);
      // setProjectSkills(skills);
      // setSelectSkills(skills);
      setProjectSkills(currentSkills);
      setSelectSkills(currentSkills);
    }
  }, [allSkills]);

  const getAllSkillsOptions = () => {
    const data: any[] = [];
    allSkills?.map((item: any) => {
      const el = selectSkills.find((a: any) => a?.label == item?.label);
      if (!el) {
        data.push(item);
      }
    });
    setAllSkillsOptions(data);
  };
  return (
    <div
      style={{
        paddingBottom: "30px ",
      }}
    >
      <form onSubmit={submitFormHandler}>
        <Autocomplete
          defaultValue={[]}
          multiple
          filterSelectedOptions={true}
          sx={constant.AutocompleteSxProps}
          id="tags-standard"
          getOptionDisabled={(option: any) => option.disabled}
          getOptionLabel={(option: any) => option.label}
          value={selectSkills?.filter((item: any) => item?.isactive == true)}
          options={allSkillsOptions.filter((item: any) => item?.isactive)}
          onChange={skillChangeHandler}
          renderInput={(params) => (
            <TextField
              {...params}
              variant="standard"
              placeholder="Type and Select"
            />
          )}
        />
      </form>
    </div>
  );
};
export default AssignSkills;
