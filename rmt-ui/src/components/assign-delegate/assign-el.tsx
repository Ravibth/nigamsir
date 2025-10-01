import {
  Autocomplete,
  Grid,
  TextField,
  Typography,
  debounce,
} from "@mui/material";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React, { useContext, useEffect, useMemo, useState } from "react";
import { GetAllUsersExcludingEmployeesWithRolesInProject } from "../../services/project-list-services/project-list-services";
import * as constant from "./constant";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { IEmployeeDetails } from "../project-details-layout/additionalElAgGrid";
import { getEmailId } from "../../global/utils";

const AssignAddEl = (props: any) => {
  const {
    projectDetails,
    additionalElGridValue,
    gridRowData,
    gridRef,
    rowNode,
    isDisabled,
    rowIndex,
    gridUpdateTrigger,
  } = props;
  const [options, setOptions] = useState<readonly IEmployeeDetails[]>([]);
  const [value, setValue] = useState<IEmployeeDetails | null>(null);
  const [inputValue, setInputValue] = useState("");

  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);

  const { projectUiRolesData, projectUpdateDelegate } = useContext(
    ProjectUpdateDetailsContext
  );
  // useEffect(() => {
  //   console.log(gridRef);
  // }, [gridRef]);
  // useEffect(() => {
  //   if(additionalElDBData )
  // }, [additionalElDBData]);

  // const CheckForUpdatedValues = (
  //   originalValue: IEmployeeDetails,
  //   newlySelected: IEmployeeDetails
  // ) => {
  //   if (originalValue?.userName == "") {
  //     return [
  //       {
  //         id: 0,
  //         user: newlySelected.emailId,
  //         label: newlySelected.name,
  //         role: constant.ADDITIONAL_EL_ROLE,
  //         delegateEmail: "",
  //         delegateUserName: "",
  //         isactive: true,
  //       },
  //     ];
  //   } else if (
  //     newlySelected.emailId.toLocaleLowerCase().trim() ===
  //     originalValue?.user?.toLocaleLowerCase().trim()
  //   ) {
  //     return [
  //       {
  //         id: originalValue.id,
  //         user: originalValue?.user,
  //         label: originalValue?.userName,
  //         role: constant.ADDITIONAL_EL_ROLE,
  //         delegateEmail: "",
  //         delegateUserName: "",
  //         isactive: true,
  //       },
  //     ];
  //   } else if (
  //     newlySelected.emailId.toLocaleLowerCase().trim() !==
  //     originalValue?.user?.toLocaleLowerCase().trim()
  //   ) {
  //     return [
  //       {
  //         id: 0,
  //         user: newlySelected.emailId,
  //         label: newlySelected.name,
  //         role: constant.ADDITIONAL_EL_ROLE,
  //         delegateEmail: "",
  //         delegateUserName: "",
  //         isactive: true,
  //       },
  //       {
  //         id: originalValue.id,
  //         user: originalValue?.user,
  //         label: originalValue?.userName,
  //         role: constant.ADDITIONAL_EL_ROLE,
  //         delegateEmail: "",
  //         delegateUserName: "",
  //         isactive: false,
  //       },
  //     ];
  //   }
  // };
  const changeGridRowData = (
    rowData: IEmployeeDetails[],
    newValue: IEmployeeDetails,
    oldValue: IEmployeeDetails
  ) => {
    const indexValue = rowData.findIndex(
      (data) => data.additionalElEmail === oldValue.additionalElEmail
    );
    // const finalRowData = rowData.map((data) => {
    //   if (
    //     data.additionalElEmail.toLowerCase().trim() ===
    //     oldValue.additionalElEmail.toLowerCase().trim()
    //   ) {
    //     return newD
    //   }
    // });

    if (indexValue > -1) {
      rowData[indexValue] = newValue;
    }
    return rowData;
  };

  const SelectEventChangeHandler = (
    event: any,
    newValue: any,
    currentValue: any
  ) => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    if (newValue) {
      // let newRowData = [...gridRowData];
      // newRowData[rowIndex] = {
      //   ...gridRowData[rowIndex],
      //   additionalElEmail: newValue.emailId,
      //   additionalElName: newValue.name,
      // } as IEmployeeDetails;
      // console.log("changed row data ===> ");
      // console.log("===>", newRowData);
      if (gridRef?.current?.api) {
        // const rowNode = gridRef?.current?.api?.getDisplayedRowAtIndex(rowIndex);
        rowNode.updateData({
          ...rowNode.data,
          additionalElEmail: newValue.emailId,
          additionalElName: newValue.name,
        });
        // setOptions(newValue ? [newValue, ...options] : options);

        setValue({
          ...rowNode.data,
          additionalElEmail: newValue.emailId,
          additionalElName: newValue.name,
        });
        // setOptions([
        //   {
        //     ...rowNode.data,
        //     additionalElEmail: newValue.emailId,
        //     additionalElName: newValue.name,
        //   },
        // ]);
        gridUpdateTrigger();
      }
      // setRowData((prevState) => {
      //   let pevStateData = [...prevState];
      //   pevStateData[rowIndex] = {
      //     ...gridRowData[rowIndex],
      //     additionalElEmail: newValue.emailId,
      //     additionalElName: newValue.name,
      //   } as IEmployeeDetails;
      //   return pevStateData;
      // });
      // setValue(newValue);
    } else {
      if (gridRef?.current?.api) {
        // const rowNode = gridRef?.current?.api?.getDisplayedRowAtIndex(rowIndex);
        rowNode.updateData({
          ...rowNode.data,
          additionalElEmail: "",
          additionalElName: "",
        });
        // setOptions(newValue ? [newValue, ...options] : options);
        setValue({
          ...rowNode.data,
          additionalElEmail: "",
          additionalElName: "",
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
            uiRoles: any[];
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
  const showOptions = (results?: any[]) => {
    // if (results) {
    //   let newOptions: IEmployeeDetails[] = [];
    //   // if (value) {
    //   //   newOptions = [value];
    //   // }

    //   if (results) {
    //     newOptions = [...newOptions, ...results];
    //   }
    //   setOptions([]);
    //   setOptions((prevState) => [...newOptions]);
    // }
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

    // console.log(data);
    // // setAllDelegateLeaderOptions([]);
    // setOptions(data);
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
      value?.additionalElName?.toLowerCase().trim() ===
        inputValue?.toLowerCase()?.trim()
    ) {
      setOptions(value && value?.additionalElEmail ? [value] : []);
      return undefined;
    }
    if (inputValue.length <= 4) {
      return;
    }
    // if (inputValue && projectDetails.pipelineCode) {
    fetch(
      {
        input: inputValue,
        pipelineCode: projectDetails.pipelineCode,
        jobCode: projectDetails.jobCode,
        currentRoles: getAllRows(),
        uiRoles: projectUiRolesData,
        projUpdDel: projectUpdateDelegate,
      },
      (result: any[]) => {
        if (active) {
          let fetchedOptions = [];
          if (value) {
            fetchedOptions = [value];
          }
          if (result) {
            fetchedOptions = [...fetchedOptions, ...result];
          }
          setOptions([...fetchedOptions]);
        }
        // showOptions((revOptions) => [...fetchedOptions]);
      }
    );
    // }
    return () => {
      active = false;
    };
  }, [value, inputValue, projectDetails.pipelineCode, fetch]);
  // useEffect(() => {
  //   if (additionalElDBData && additionalElDBData.length > 0) {
  //     let dbData = additionalElDBData[0];
  //     console.log("DBDATA ===>", dbData);
  //     const delegateViewData = {
  //       emailId: dbData.user,
  //       name: dbData.userName,
  //       projectRoleId: dbData.id,
  //     } as IEmployeeDetails;
  //     setValue(delegateViewData);
  //   }
  // }, [additionalElDBData, additionalElGridValue]);
  useEffect(() => {
    if (additionalElGridValue && additionalElGridValue.length > 0) {
      let dbData = additionalElGridValue[0].data;
      const delegateViewData = {
        emailId: dbData.additionalElEmail,
        name: dbData.additionalElName,
        projectRoleId: dbData.projectRoleId,
        additionalElEmail: dbData.additionalElEmail,
        additionalElName: dbData.additionalElName,
        additionalDelegateEmail: dbData.additionalDelegateEmail,
        additionalDelegateName: dbData.additionalDelegateName,
      } as IEmployeeDetails;
      setValue(delegateViewData);
      setOptions([delegateViewData]);
    }
  }, [additionalElGridValue]);

  // useEffect(() => {
  //   console.log(gridRowData);
  //   if (gridRowData && gridRowData.length > 0) {
  //     console.log(gridRowData);
  //     const elViewData = {
  //       emailId: gridRowData[rowIndex].additionalElEmail,
  //       name: gridRowData[rowIndex].additionalElName,
  //       projectRoleId: gridRowData[rowIndex].projectRoleId,
  //       additionalElEmail: gridRowData[rowIndex].additionalElEmail,
  //       additionalElName: gridRowData[rowIndex].additionalElName,
  //       additionalDelegateEmail: gridRowData[rowIndex].additionalDelegateEmail,
  //       additionalDelegateName: gridRowData[rowIndex].additionalDelegateName,
  //     } as IEmployeeDetails;
  //     setValue(elViewData);
  //   }
  // }, [gridRowData]);

  return (
    <div>
      {isEmployee ? (
        <TextField value={props.params?.data?.userName} disabled />
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
                : option?.additionalElName
                ? option?.additionalElName
                : ""
              : ""
          }
          includeInputInList
          filterOptions={(x) => x}
          id="combo-box-demo"
          options={options}
          disabled={isDisabled ? isDisabled : false}
          value={value || value?.additionalElName ? value : ""}
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
                      {/* <br /> */}
                      {/* <span className="search-employee-skills">
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
export default AssignAddEl;
