import { Autocomplete, TextField, Typography } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import * as constant from "./constant";
import { useForm } from "react-hook-form";
import "./assign-engagement-leader.css";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import * as GC from "../../global/constant";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { GetAllUsersExcludingEmployeesWithRolesInProject } from "../../services/project-list-services/project-list-services";
import { EPipelineStatus } from "../../common/enums/EProject";

const AssignEngagementLeader = (props: any) => {
  const {
    EngagementLeadersList,
    ProjectEngagementLeaderProps,
    projectDetails,
  } = props;
  const isEmployee = React.useContext(UserDetailsContext).isEmployee;
  const [engagementLeader, setEngagementLeader] = useState([] as any);
  const [allEngagementLeaderOptions, setAllEngagementLeaderOptions] = useState(
    EngagementLeadersList.map((item: any) => ({
      ...item,
      isactive: !isEmployee, // Set isactive to false if isEmployee is true
    }))
  );
  const [selectEngagementLeader, setSelectEngagementLeader] = useState(
    [] as any
  );
  const [isRequireRender, setIsRequireRender] = useState(true);
  const isSuspended =
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const { setProjectUpdateEL } = useContext(ProjectUpdateDetailsContext);

  const submitFormHandler = (event: any) => {
    event.preventDefault();
  };

  const ELChangeHandler = (event: any, newValue: any) => {
    const data: any[] = [];
    newValue?.map((item: any) => {
      const ELData = selectEngagementLeader.find(
        (a: any) => a.label == item?.label
      );
      if (!ELData) {
        item.id = 0;
      } else {
        item.id = ELData.id;
      }
      data.push(item);
    });
    const EGLeader = selectEngagementLeader.concat(data);
    const uniqueValues = Array.from(new Set(EGLeader));
    uniqueValues.map((item: any) => {
      const skill = newValue.find((a: any) => a.label == item?.label);
      if (!skill) {
        item.isactive = false;
      }
    });
    setSelectEngagementLeader(uniqueValues);
    setProjectUpdateEL(uniqueValues);
    setEngagementLeader(uniqueValues);
    getAllEngagementLeaderOptions(EngagementLeadersList, uniqueValues);
    props.handleElChange(uniqueValues);
  };

  useEffect(() => {
    const pEngegementLeader =
      props?.ProjectEngagementLeaderProps?.EngagementLeadersList?.filter(
        (item: any) => item.modifiedBy !== GC.DEFAULT_SYSTEM_ACCOUNT_NAME
      );
    setEngagementLeader(pEngegementLeader);
    getAllEngagementLeaderOptions(EngagementLeadersList, pEngegementLeader);
    setSelectEngagementLeader(pEngegementLeader);
  }, []);

  const getAllEngagementLeaderOptions = (
    allOptions: any,
    selectedOptions: any
  ) => {
    const data: any[] = [];
    allOptions?.map((item: any) => {
      const el = selectedOptions.find(
        (a: any) => a?.label === item?.label && item?.isactive && a?.isactive
      );
      if (!el) {
        data.push({
          ...item,
          isactive: !isEmployee,
        });
      }
    });
    setAllEngagementLeaderOptions(data);
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
  const CustomAutocompleteInput = (params: any) => {
    return (
      <TextField
        {...params}
        placeholder="Type and Select"
        disabled={isEmployee || isSuspended}
        InputProps={{
          ...params.InputProps,
        }}
      />
    );
  };

  useEffect(() => {
    if (engagementLeader && projectDetails) {
      // console.log("selected values ", delegateInput, projectDetails);
      GetEmployeesList(
        engagementLeader,
        projectDetails.pipelineCode,
        projectDetails.jobCode
      )
        .then((data) => {
          // console.log(data);
          setAllEngagementLeaderOptions([]);
          setAllEngagementLeaderOptions(() => {
            return [...data];
          });
        })
        .catch((err) => {
          console.log(err);
        });
      // fetch(
      //   {
      //     input: delegateInput,
      //     pipelineCode: projectDetails.pipelineCode,
      //   },
      //   (result: any[]) => {
      //     showOptions(result);
      //   }
      // );
    }
  }, [engagementLeader, projectDetails]);

  useEffect(() => {
    if (isRequireRender) {
      const data: any[] = [];
      EngagementLeadersList?.map((item: any) => {
        const el = ProjectEngagementLeaderProps?.EngagementLeadersList.find(
          (a: any) => a.user == item.internalName
        );
        if (!el) {
          data.push(item);
        }
      });
      if (data?.length > 0) {
        setIsRequireRender(false);
      }
      const pEngegementLeader =
        props?.ProjectEngagementLeaderProps?.EngagementLeadersList?.filter(
          (item: any) => item.modifiedBy !== GC.DEFAULT_SYSTEM_ACCOUNT_NAME
        );
      setSelectEngagementLeader(pEngegementLeader);
      getAllEngagementLeaderOptions(EngagementLeadersList, pEngegementLeader);
    }
  }, [EngagementLeadersList]);

  return (
    <div
      style={{
        border: "1px solid #ebebeb",
        marginBottom: "30px",
        padding: "12px 16px",
      }}
    >
      <form onSubmit={submitFormHandler}>
        <Typography
          variant="h6"
          mt={1}
          mb={1}
          component={"h6"}
          className="engagement-header"
        >
          Secondary Engagement Leader
        </Typography>

        {isEmployee ? (
          <TextField
            disabled
            value={selectEngagementLeader
              .map((item: any) => item?.label)
              .join(", ")}
          />
        ) : (
          <Autocomplete
            disablePortal
            disableClearable
            multiple
            filterSelectedOptions
            sx={constant.AutocompleteSxProps}
            id="combo-box-demo"
            options={allEngagementLeaderOptions}
            onChange={ELChangeHandler}
            value={selectEngagementLeader.filter((item: any) => item.isactive)}
            renderInput={CustomAutocompleteInput}
            disabled={isSuspended}
          />
        )}
      </form>
    </div>
  );
};

export default AssignEngagementLeader;
