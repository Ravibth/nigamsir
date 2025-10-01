import {
  Autocomplete,
  Grid,
  TextField,
  Typography,
  debounce,
  fabClasses,
} from "@mui/material";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React, { useContext, useEffect, useMemo, useState } from "react";
import { GetAllUsersExcludingEmployeesWithRolesInProject } from "../../services/project-list-services/project-list-services";

import { GetAllEmployeesExcludingEmployeesWithRolesInTheProject } from "../../services/project-list-services/project-list-services";
import * as constant from "./constant";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { IEmployeeDetails } from "../project-details-layout/additionalElAgGrid";
import { getEmailId } from "../../global/utils";

const AssignAddDelegate = (props: any) => {
  const {
    projectDetails,
    additionalDelegateGridValue,
    gridRef,
    rowNode,
    rowIndex,
    gridUpdateTrigger,
    isDisabled,
  } = props;
  const [options, setOptions] = useState<readonly IEmployeeDetails[]>([]);
  const [value, setValue] = useState<IEmployeeDetails | null>(null);
  const [inputValue, setInputValue] = useState("");
  // const { setProjectUpdateDelegate, projectUpdateDelegate } = React.useContext(
  //   ProjectUpdateDetailsContext
  // );
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);

  const { projectUiRolesData, projectUpdateDelegate } = useContext(
    ProjectUpdateDetailsContext
  );

  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  const CheckForUpdatedValues = (
    originalValue: any,
    newlySelected: IEmployeeDetails
  ) => {
    if (originalValue?.userName == "") {
      return [
        {
          id: 0,
          user: newlySelected.emailId,
          label: newlySelected.name,
          role: constant.DELEGATE_ROLE,
          isactive: true,
        },
      ];
    } else if (
      newlySelected.emailId.toLocaleLowerCase().trim() ===
      originalValue?.user?.toLocaleLowerCase().trim()
    ) {
      return [
        {
          id: originalValue.id,
          user: originalValue?.user,
          label: originalValue?.userName,
          role: constant.DELEGATE_ROLE,
          isactive: true,
        },
      ];
    } else if (
      newlySelected.emailId.toLocaleLowerCase().trim() !==
      originalValue?.user.toLocaleLowerCase().trim()
    ) {
      return [
        {
          id: 0,
          user: newlySelected.emailId,
          label: newlySelected.name,
          role: constant.DELEGATE_ROLE,
          isactive: true,
        },
        {
          id: originalValue.id,
          user: originalValue?.user,
          label: originalValue?.userName,
          role: constant.DELEGATE_ROLE,
          isactive: false,
        },
      ];
    }
  };
  const GenerateData = (aggrid: any[], projUpdateDelegateData: any[]) => {
    const result: string[] = [];
    aggrid.map((data) => {
      if (data.additionalDelegateEmail) {
        result.push(data.additionalDelegateEmail);
      }
      if (data.additionalElEmail) {
        result.push(data.additionalElEmail);
      }
    });
    projUpdateDelegateData.map((data) => {
      if (data.user && data.isactive) {
        result.push(data.user);
      }
    });
    return result;
  };
  const SelectEventChangeHandler = (
    event: any,
    newValue: any,
    currentValue: any
  ) => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    if (newValue) {
      if (gridRef?.current?.api) {
        //WIP Change
        rowNode.updateData({
          ...rowNode.data,
          additionalDelegateEmail: newValue.emailId,
          additionalDelegateName: newValue.name,
        });
        setValue({
          ...rowNode.data,
          additionalDelegateEmail: newValue.emailId,
          additionalDelegateName: newValue.name,
        });
        gridUpdateTrigger();
      }
    } else {
      if (gridRef?.current?.api) {
        //WIP Change
        rowNode.updateData({
          ...rowNode.data,
          additionalDelegateEmail: "",
          additionalDelegateName: "",
        });
        setValue({
          ...rowNode.data,
          additionalDelegateEmail: "",
          additionalDelegateName: "",
        });
        gridUpdateTrigger();
      }
    }
  };
  const DLChangeEvent = (event: any, newValue: any) => {
    setInputValue(newValue);
  };
  const fetch = useMemo(
    () =>
      debounce(
        async (
          request: {
            input: string;
            pipelineCode: string;
            jobCode: string;
            currentRoles: any[];
            projUpdDel: any[];
          },
          callback: (results?: readonly any[]) => void
        ) => {
          const finalData = GenerateData(
            request.currentRoles,
            request.projUpdDel
          );
          const result: any = await GetEmployeesList(
            request.input,
            request.pipelineCode,
            request.jobCode,
            request.currentRoles,
            finalData
          );
          showOptions(result);
        },
        400
      ),
    []
  );
  const showOptions = (results?: any[]) => {
    if (results) {
      let newOptions: readonly IEmployeeDetails[] = [];
      if (value) {
        newOptions = [value];
      }

      if (results) {
        newOptions = [...newOptions, ...results];
      }
      setOptions([]);
      setOptions([...newOptions]);
    }
  };
  const GetEmployeesList = async (
    inputEmail: string,
    pipelineCode: string,
    jobCode: string,
    currentRoles: any[],
    usersNotToInclude: any[]
  ) => {
    try {
      var result = await GetAllUsersExcludingEmployeesWithRolesInProject(
        inputEmail,
        pipelineCode,
        jobCode,
        currentRoles,
        usersNotToInclude
      );
      return result.data;
    } catch (ex) {
      console.log(ex);
    }
  };

  const getAllRows = () => {
    let rowData = [];
    gridRef.current.api.forEachNode((node) => rowData.push(node.data));
    return rowData;
  };
  useEffect(() => {
    let active = true;
    if (
      inputValue === "" ||
      value?.additionalDelegateName?.toLowerCase().trim() ===
        inputValue?.toLowerCase()?.trim()
    ) {
      setOptions(value && value?.additionalDelegateEmail ? [value] : []);
      return;
    }
    if (inputValue.length <= 4) {
      return;
    }
    if (inputValue && projectDetails.pipelineCode) {
      fetch(
        {
          input: inputValue,
          pipelineCode: projectDetails.pipelineCode,
          jobCode: projectDetails.jobCode,
          currentRoles: getAllRows(),
          projUpdDel: projectUpdateDelegate,
        },
        (result: any[]) => {
          if (active) {
            let fetchedOptions = [];
            if (result) {
              fetchedOptions = [...fetchedOptions, ...result];
            }
            setOptions([...fetchedOptions]);
          }
        }
      );
    }
    return () => {
      active = false;
    };
  }, [value, inputValue, projectDetails.pipelineCode, fetch]);
  useEffect(() => {
    if (additionalDelegateGridValue && additionalDelegateGridValue.length > 0) {
      let dbData = additionalDelegateGridValue[0].data;
      const delegateViewData = {
        emailId: dbData.additionalDelegateEmail,
        name: dbData.additionalDelegateName,
        projectRoleId: dbData.projectRoleId,
        additionalElEmail: dbData.additionalElEmail,
        additionalElName: dbData.additionalElName,
        additionalDelegateEmail: dbData.additionalDelegateEmail,
        additionalDelegateName: dbData.additionalDelegateName,
      } as IEmployeeDetails;
      setValue(delegateViewData);
    }
  }, [additionalDelegateGridValue]);
  return (
    <div>
      {isEmployee ? (
        <TextField value={props?.params?.data?.delegateUserName} disabled />
      ) : (
        <Autocomplete
          autoComplete
          size="small"
          // filterSelectedOptions
          noOptionsText="No Match Found"
          getOptionLabel={(option) =>
            option
              ? typeof option === "string"
                ? option
                : option?.additionalDelegateName
                ? option?.additionalDelegateName
                : ""
              : ""
          }
          includeInputInList
          filterOptions={(x) => x}
          disabled={isDisabled ? isDisabled : false}
          id="combo-box-demo"
          options={options}
          value={value || value?.additionalDelegateName ? value : ""}
          renderInput={(params) => <TextField {...params} fullWidth />}
          onChange={(e, newValue, currentValue) => {
            SelectEventChangeHandler(e, newValue, currentValue);
          }}
          onInputChange={(e, newValue) => {
            DLChangeEvent(e, newValue);
          }}
          renderOption={(props, option: IEmployeeDetails) => {
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
                        {option.name === null ? "" : option?.name}
                        {" - "}
                        {option.emailId === null
                          ? ""
                          : getEmailId(option?.emailId)}
                      </span>
                      {/* <br />
                      <span className="search-employee-skills">
                        {option.emailId === null
                          ? ""
                          : getEmailId(option?.emailId)}
                      </span> */}
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
export default AssignAddDelegate;
