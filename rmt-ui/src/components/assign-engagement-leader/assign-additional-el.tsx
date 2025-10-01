import {
  Autocomplete,
  Chip,
  Grid,
  TextField,
  Typography,
  debounce,
} from "@mui/material";
import React, { useContext, useEffect, useMemo, useState } from "react";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import * as constant from "./constant";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { GetAllUsersExcludingEmployeesWithRolesInProject } from "../../services/project-list-services/project-list-services";
import { EPipelineStatus } from "../../common/enums/EProject";

interface IEmployeeDetails {
  id: number | null;
  projectRoleId: number | null;
  name: string | null;
  emailId: string | null;
  businessUnit: string | null;
  designation: string | null;
  empCode: string | null;
  expertise: string | null;
  fname: string | null;
  lname: string | null;
  role_list: string[] | null;
  serviceLine: string | null;
  supercoach: string | null;
  skills: string[] | null;
  smeg: string | null;
}

const AssignAdditionalEl = (props: any) => {
  const { projectDetails, additionalElDBData, setadditionalElDBData } = props;

  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const [options, setOptions] = useState<IEmployeeDetails[]>([]);
  const [value, setValue] = useState<IEmployeeDetails[] | []>([]);
  const [inputValue, setInputValue] = useState("");
  const { projectUpdateEL, setProjectUpdateEL } = useContext(
    ProjectUpdateDetailsContext
  );
  const isSuspended =
    projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;

  // console.log("update additional el ", projectUpdateEL);
  // console.log("value", value);

  const CheckForUpdatedValues = (
    originalValue: any,
    newlySelected: IEmployeeDetails[]
  ) => {
    if (!originalValue) {
      return [
        {
          id: 0,
          user: newlySelected.map((e) => e.emailId).toLocaleString(),
          label: newlySelected.map((e) => e.name).toLocaleString(),
          role: constant.ADDITIONAL_EL_ROLE,
          isactive: true,
        },
      ];
    } else if (
      newlySelected
        ?.map((e) => e.emailId)
        .toLocaleString()
        .toLocaleLowerCase()
        .trim() === originalValue?.user?.toLocaleLowerCase().trim()
    ) {
      return [
        {
          id: originalValue.id,
          user: originalValue?.user,
          label: originalValue?.userName,
          role: constant.ADDITIONAL_EL_ROLE,
          isactive: true,
        },
      ];
    } else if (
      newlySelected
        ?.map((e) => e.emailId)
        .toLocaleString()
        .toLocaleLowerCase()
        .trim() !== originalValue?.user?.toLocaleLowerCase().trim()
    ) {
      return [
        {
          id: 0,
          user: newlySelected.map((e) => e.emailId).toLocaleString(),
          label: newlySelected.map((e) => e.name).toLocaleString(),
          role: constant.ADDITIONAL_EL_ROLE,
          isactive: true,
        },
        {
          id: originalValue.id,
          user: originalValue?.user,
          label: originalValue?.userName,
          role: constant.ADDITIONAL_EL_ROLE,
          isactive: false,
        },
      ];
    }
  };

  const SelectEventChangeHandler = (
    event: any,
    newValue: any,
    currentValue: any
  ) => {
    if (newValue) {
      let finalValue = CheckForUpdatedValues(additionalElDBData[0], newValue);
      setValue(newValue);
      setProjectUpdateEL(finalValue);
    }
  };

  const AdditionalELChangeEvent = (event: any, newValue: any) => {
    setInputValue(newValue);
  };

  const fetch = useMemo(
    () =>
      debounce(
        async (
          request: { input: string; pipelineCode: string; jobCode: string },
          callback: (results?: readonly any[]) => void
        ) => {
          const result: any = await GetEmployeesList(
            request.input,
            request.pipelineCode,
            request.jobCode
          );
          if (result && result.length > 0) {
            showOptions(result);
          }
          // console.log(result);
        },
        400
      ),
    []
  );

  const showOptions = (data: any[]) => {
    console.log(data);
    // setAllDelegateLeaderOptions([]);
    setOptions(data);
  };
  const GetEmployeesList = async (
    inputEmail: string,
    pipelineCode: string,
    jobCode: string
  ) => {
    try {
      var result = await GetAllUsersExcludingEmployeesWithRolesInProject(
        inputEmail,
        pipelineCode,
        jobCode
      );
      return result.data;
    } catch (ex) {
      console.log(ex);
    }
  };
  useEffect(() => {
    // console.log("162 line", inputValue, projectDetails.pipelineCode);
    if (inputValue && projectDetails.pipelineCode) {
      fetch(
        {
          input: inputValue,
          pipelineCode: projectDetails.pipelineCode,
          jobCode: projectDetails.jobCode,
        },
        (result: any[]) => {
          showOptions(result);
        }
      );
    }
  }, [inputValue, projectDetails.pipelineCode, fetch]);

  useEffect(() => {
    if (additionalElDBData && additionalElDBData.length > 0) {
      let dbData = additionalElDBData[0];
      // console.log("EL_DB_DATA ===>", dbData);
      const elViewData = {
        emailId: dbData.user,
        name: dbData.userName,
        projectRoleId: dbData.id,
      } as IEmployeeDetails;
      // console.log(elViewData);
      setValue([elViewData]);
    }
  }, [additionalElDBData]);
  // console.log(additionalElDBData);
  return (
    <div
    // style={{
    //   border: "1px solid #ebebeb",
    //   marginBottom: "30px",
    //   padding: "12px 16px",
    // }}
    >
      {/* <Typography
        variant="h6"
        mt={1}
        mb={1}
        component={"h6"}
        className="engagement-header"
      >
        Additional Engagement Leader
      </Typography> */}
      {isEmployee ? (
        <TextField value={"dat"} disabled={isEmployee || isSuspended} />
      ) : (
        <Autocomplete
          autoComplete
          disablePortal
          disableClearable
          disabled={isSuspended}
          multiple
          filterSelectedOptions
          noOptionsText="No Match Found"
          getOptionLabel={(option) =>
            typeof option === "string" ? option : option.name
          }
          includeInputInList
          filterOptions={(x) => x}
          sx={constant.AutocompleteSxProps}
          id="combo-box-demo"
          options={options}
          value={value}
          renderInput={(params) => (
            <TextField
              {...params}
              label="Type And Select"
              fullWidth
              //   variant="standard"
            />
          )}
          onChange={(e, newValue, currentValue) => {
            // console.log(e, newValue, currentValue);
            SelectEventChangeHandler(e, newValue, currentValue);
          }}
          onInputChange={(e, newValue) => {
            AdditionalELChangeEvent(e, newValue);
          }}
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
                        {option.name}
                      </span>
                      <br />
                      <span className="search-employee-skills">
                        {option.emailId}
                      </span>
                    </div>
                  </Grid>
                </Grid>
              </li>
            );
          }}
        />
      )}
    </div>
  );
};

export default AssignAdditionalEl;
