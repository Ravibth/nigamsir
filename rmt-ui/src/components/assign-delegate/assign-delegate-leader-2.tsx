import {
  Autocomplete,
  Grid,
  TextField,
  Tooltip,
  Typography,
  debounce,
} from "@mui/material";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React, { useEffect, useMemo, useState } from "react";
import { GetAllEmployeesExcludingEmployeesWithRolesInTheProject } from "../../services/project-list-services/project-list-services";
import * as constant from "./constant";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { hasPermissionForChange } from "./util";
import InfoIcon from "@mui/icons-material/Info";
import "./assign-delegate-leader.css";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import { EPipelineStatus } from "../../common/enums/EProject";
import { getEmailId, IsProjectInActiveOrClosed } from "../../global/utils";
import { RolesListMaster } from "../../common/enums/ERoles";

interface IEmployeeDetails {
  id?: number | null;
  projectRoleId?: number | null;
  name?: string | null;
  emailId?: string | null;
  businessUnit?: string | null;
  designation?: string | null;
  empCode?: string | null;
  expertise?: string | null;
  fname?: string | null;
  lname?: string | null;
  role_list?: string[] | null;
  serviceLine?: string | null;
  supercoach?: string | null;
  skills?: string[] | null;
  smeg?: string | null;
}
const AssignDelegate2 = (props: any) => {
  const { projectDetails, setDelegateDBData, delegateDBData } = props;
  const [options, setOptions] = useState<IEmployeeDetails[]>([]);
  const [value, setValue] = useState<IEmployeeDetails | null>(null);
  const [inputValue, setInputValue] = useState("");
  const {
    setProjectUpdateDelegate,
    projectUpdateDelegate,
    projectRoleAdditionalData,
  } = React.useContext(ProjectUpdateDetailsContext);

  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  const userContext = React.useContext(UserDetailsContext);
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);
  const isSuspended =
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const CheckForUpdatedValues = (
    originalValue: any,
    newlySelected: IEmployeeDetails
  ) => {
    if (!originalValue) {
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
      originalValue?.user?.toLocaleLowerCase().trim()
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
  const SelectEventChangeHandler = (
    event: any,
    newValue: any,
    currentValue: any
  ) => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    if (newValue) {
      let finalValue = CheckForUpdatedValues(delegateDBData[0], newValue);
      setValue(newValue);
      setProjectUpdateDelegate(finalValue);
    } else {
      setValue({
        name: "",
        emailId: "",
        id: 0,
      } as IEmployeeDetails);
      setProjectUpdateDelegate([
        {
          id: delegateDBData[0]?.id,
          user: delegateDBData[0]?.user,
          label: delegateDBData[0]?.userName,
          role: constant.DELEGATE_ROLE,
          isactive: false,
        },
      ]);
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
            delegateUiRoles: any[];
            agGridRoles: any[];
          },
          callback: (results?: readonly any[]) => void
        ) => {
          const usersNotToInclude = GenerateDataToBeRemoved(
            request.agGridRoles
          );
          const result: any = await GetEmployeesList(
            request.input,
            request.pipelineCode,
            request.jobCode,
            usersNotToInclude
          );
          showOptions(result);
        },
        400
      ),
    []
  );
  const GenerateDataToBeRemoved = (additionalElGridData: any[]): string[] => {
    const result: string[] = [];
    additionalElGridData.map((data) => {
      if (
        data &&
        data.additionalDelegateEmail &&
        data.additionalDelegateEmail.length > 0
      ) {
        result.push(data.additionalDelegateEmail);
      }
      if (data && data.additionalElEmail && data.additionalElEmail.length > 0) {
        result.push(data.additionalElEmail);
      }
    });
    return result;
  };
  const showOptions = (results: any[]) => {
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
    usersNotToInclude: string[]
  ) => {
    try {
      var result = await GetAllEmployeesExcludingEmployeesWithRolesInTheProject(
        inputEmail,
        pipelineCode,
        jobCode,
        usersNotToInclude
      );
      return result.data;
    } catch (ex) {
      console.log(ex);
    }
  };

  useEffect(() => {
    let active = true;
    if (
      inputValue === "" ||
      value?.name?.toLowerCase().trim() === inputValue.toLowerCase().trim()
    ) {
      setOptions(value && value?.emailId ? [value] : []);
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
          delegateUiRoles: projectUpdateDelegate,
          agGridRoles: projectRoleAdditionalData,
        },
        (result: any[]) => {
          showOptions(result);
        }
      );
    }
    return () => {
      active = false;
    };
  }, [value, inputValue, projectDetails.pipelineCode, fetch]);

  useEffect(() => {
    if (delegateDBData && delegateDBData.length > 0) {
      let dbData = delegateDBData[0];
      const delegateViewData = {
        emailId: dbData.user,
        name: dbData.userName,
        projectRoleId: dbData.id,
      } as IEmployeeDetails;
      setValue(delegateViewData);
      setOptions([delegateViewData]);
      setProjectUpdateDelegate([
        {
          id: delegateDBData[0]?.id,
          user: delegateDBData[0]?.user,
          label: delegateDBData[0]?.userName,
          role: constant.DELEGATE_ROLE,
          isactive: true,
        },
      ]);
    }
  }, [delegateDBData]);

  const checkDelegateDisability = () => {
    if (IsProjectInActiveOrClosed(projectDetails)) {
      return true;
    }
    if (isSuspended) {
      return true;
    }
    const hasPermission =
      hasPermissionForChange(
        userContext?.projectPermissionData?.projectRoles
      ) || userContext.role.includes(RolesListMaster.SystemAdmin);
    return !hasPermission;
  };

  return (
    <div
      className="delegate-main-container"
      style={{
        padding: "8px 16px",
      }}
    >
      <Typography
        variant="h6"
        mt={1}
        mb={1}
        component={"h6"}
        className="delegate-header"
        sx={constant.DelegateHeader}
      >
        <PersonOutlineOutlinedIcon style={{ marginRight: "3px" }} />

        {"Delegate"}
        <Tooltip
          sx={{ marginLeft: "5px" }}
          className={"tool-requisition"}
          title={
            "Any changes to the assignment might have an impact on allocations of the project."
          }
          placement="right"
        >
          <InfoIcon />
        </Tooltip>
      </Typography>
      <div className="delegate-control">
        {isEmployee ? (
          <TextField value={"dat"} disabled />
        ) : (
          <Autocomplete
            autoComplete
            noOptionsText="No Match Found"
            getOptionLabel={(option) =>
              typeof option === "string" ? option : option.name
            }
            disabled={checkDelegateDisability()}
            includeInputInList
            filterOptions={(x) => x}
            sx={constant.AutocompleteSxProps}
            id="combo-box-demo"
            options={options}
            value={value}
            renderInput={(params) => (
              <TextField {...params} placeholder="Type And Select" fullWidth />
            )}
            onChange={(e, newValue, currentValue) => {
              SelectEventChangeHandler(e, newValue, currentValue);
            }}
            onInputChange={(e, newValue) => {
              DLChangeEvent(e, newValue);
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
                          {" - "}
                          {getEmailId(option.emailId)}
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
    </div>
  );
};
export default AssignDelegate2;
