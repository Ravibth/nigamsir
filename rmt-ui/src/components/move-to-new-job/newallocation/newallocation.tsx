import { Autocomplete, Grid, TextField, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import * as constant from "../constant";
import { NumberFilterModel } from "ag-grid-community";
import { getCurrentProjectName } from "../util";

const Newallocation = (props: any) => {
  const {
    projectData,
    allProjectData,
    setSelectedOption,
    selectedOption,
    setIsDataDirty,
  } = props;
  const [value, setValue] = useState([]);

  const projectOptionsList = () => {
    var jsondata = [];
    allProjectData.map((e) => {
      jsondata.push({
        projectId: e.id,
        id: e.pipelineCode + " - " + e.jobCode,
        label: getCurrentProjectName(e),
      });
    });
    setValue(jsondata);
  };

  useEffect(() => {
    projectOptionsList();
  }, [allProjectData]);

  const handleOptionChange = (event: any, newValue: any) => {
    setSelectedOption(newValue);
    props.onSelectedProject(projectData);
  };

  //console.log("selected option", selectedOption);
  return (
    <div>
      <Grid item xs={12}>
        <Typography sx={constant.Text}>Move to</Typography>
        <Typography sx={constant.Title}>Name - Code</Typography>
        <Autocomplete
          autoComplete
          id="combo-box-demo"
          sx={constant.AutocompleteSxProps}
          options={value}
          value={
            Object.entries(selectedOption).length > 0 ? selectedOption : null
          }
          onChange={handleOptionChange}
          renderInput={(params: any) => (
            <TextField {...params} label={"Type and Select"} />
          )}
        />
      </Grid>
    </div>
  );
};

export default Newallocation;
