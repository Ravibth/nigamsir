import { useState, useEffect, useMemo } from "react";
import Box from "@mui/material/Box";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import SearchIcon from "@mui/icons-material/Search";
import { debounce } from "@mui/material/utils";
import Typography from "@mui/material/Typography";
import Autocomplete from "@mui/material/Autocomplete";
import Grid from "@mui/material/Grid";
import { AnyARecord } from "dns";
import { GetResourceNameService } from "../../../services/allocation/get-resource-name.service";
import "./employeeSearch.css";
import * as constant from "./constant";
import * as _ from "lodash";

interface IEmployeeDetails {
  empName: string;
  // skills: string[];
  email: string;
  designation: string;
  BusinessUnit: string;
  expertise: string;
  smeg: string;
}
export default function EmployeesearchTeam(props: any) {
  const [options, setOptions] = useState<readonly IEmployeeDetails[]>([]);
  const [value, setValue] = useState<any>([]);
  const [inputValue, setInputValue] = useState("");
  const [selectedResource, setSelectedResource] = useState<string>("");

  useEffect(() => {
    // console.log(props.resourceList);
    const resources = props.resourceList;
    if (resources?.length && options.length == 0) {
      setOptions(resources);
    }
  }, [props]);

  const showOptions = (results?: readonly IEmployeeDetails[]) => {
    if (results) {
      let newOptions: readonly IEmployeeDetails[] = [];
      if (value) {
        newOptions = value;
      }

      if (results) {
        newOptions = [...newOptions, ...results];
      }
      setOptions(newOptions);
    }
  };

  const findSelectedUsers = (selectedUser: string) => {
    //  const users = options.map((option, i) => option.empName.includes(selectedUser)? option : '').filter(String)
    const users = _.filter(options, function (option) {
      return option.empName.includes(selectedUser);
    });
    //  console.log(users);
    props.setFilteredResource(users);
    //  props.employeeSelected(user);
    // showOptions(user as any);
  };

  return (
    <Box sx={constant.BoxSxProps}>
      <FormControl variant="standard">
        <Box sx={constant.BoxSecondSxProps}>
          <SearchIcon sx={constant.SearchIconSxProps} />

          <Autocomplete
            id="searchEmployee"
            sx={{ width: 600 }}
            getOptionLabel={(option) =>
              typeof option === "string" ? option : option.empName
            }
            // filterOptions={(x) => x}
            options={options}
            autoComplete
            multiple
            includeInputInList
            filterSelectedOptions
            value={value}
            noOptionsText="No Match Found."
            onChange={(event: any, newValue: IEmployeeDetails[] | null) => {
              // setOptions(newValue ? [newValue, ...options] : options);
              setValue(newValue);
              props.setFilteredResource(newValue);
              // findSelectedUsers(newValue);
            }}
            // onInputChange={(event, newInputValue) => {
            //   if(newInputValue.length > 2)
            //   {
            //     setInputValue(newInputValue);
            //   //  findSelectedUsers(newInputValue);
            //   }
            // }}
            renderInput={(params) => (
              <TextField
                {...params}
                label="Search Employee"
                fullWidth
                variant="standard"
              />
            )}
            renderOption={(props, option) => {
              return (
                <li {...props}>
                  <Grid container alignItems="center">
                    <Grid
                      item
                      sx={{
                        width: "calc(100% - 44px)",
                        wordWrap: "break-word",
                      }}
                    >
                      <div className="search-employee-option">
                        <span className="search-employee-name">
                          {" "}
                          {option?.empName}{" "}
                        </span>
                        <span className="search-employee-skills">
                          {" "}
                          {option.email}{" "}
                        </span>
                      </div>

                      <Typography variant="body2" color="text.secondary">
                        {option.designation},{" "}
                        {option.BusinessUnit
                          ? option.BusinessUnit + " -> "
                          : ""}
                        {option.expertise ? option.expertise + " -> " : ""}
                        {option.smeg ? option.smeg : ""}
                      </Typography>
                      {/* <Typography variant="body2" color="text.secondary">
                     {option.skills.toString()} 
                      </Typography> */}
                    </Grid>
                  </Grid>
                </li>
              );
            }}
          />
        </Box>
      </FormControl>
    </Box>
  );
}
