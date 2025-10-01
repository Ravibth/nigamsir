/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable @typescript-eslint/no-unused-vars */
import { Box } from "@mui/material";
import ProjectTitle from "../ProjectTitle/project-title";
import DescriptionLayout from "./projectdescriptionlayout";
import RequestorView from "../requestor-view/requestor-view";
import { useContext, useEffect, useState } from "react";
import * as constant from "./constant";
import * as Utils from "./utils";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import React from "react";
import { useParams } from "react-router";
import ProjectDetailHeadlines from "../ProjectDetail/project-detail-headline/project-detail-headlines";
import { IProjectDetailHeadlinesProps } from "../ProjectDetail/project-detail-headline/IProjectDetailHeadlinesProps";
import { useNavigate } from "react-router-dom";
import "./style.css";
import { EPipelineStatus } from "../../common/enums/EProject";

const ProjectDetailsLayout = (props: any) => {
  const [isUpdateButtonEnable, setIsUpdateButtonEnable] = useState(false);
  const [projectDetails, setProjectDetails] = useState({} as any);
  const navigate = useNavigate();
  const [tabValue, setTabValue] = useState<any>(0);
  const [elCurrentData, setElCurrentData] = useState([] as any[]);
  const [dlCurrentData, setDlCurrentData] = useState([] as any[]);
  const [delegateDBData, setDelegateDBData] = useState([] as any);
  const [additionalElDBData, setadditionalElDBData] = useState([] as any); //will contain both additional el and additionalDelegate Data
  const [skillCurrentData, setSkillCurrentData] = useState([] as any[]);
  const [isUpdateMade, setIsUpdateMade] = useState(false);
  const [designationData, setDesignationData] = useState([] as any[]);
  const [refresh, setRefresh] = useState(false);

  const isEmployee = useContext(UserDetailsContext).isEmployee;
  const { pipelineCode, jobCode } = useParams();
  const getProjectDetails = async () => {
    const projectDetailsItems = await Utils.getProjectDetails(
      pipelineCode ? pipelineCode.toString() : "",
      jobCode
    );
    setProjectDetails(projectDetailsItems);
    return projectDetailsItems;
  };
  const handleElChange = (data: any) => {
    setElCurrentData(data);
    setIsUpdateButtonEnable(true);
  };
  const handleDlChange = (data: any) => {
    setDlCurrentData(data);
    setIsUpdateButtonEnable(true);
  };
  const handleSkillChange = (data: any) => {
    setSkillCurrentData(data);
  };

  const handleCurrentDesignationChange = (data: any) => {
    setIsUpdateButtonEnable(true);
  };

  const headlinesDataProps: IProjectDetailHeadlinesProps = {
    headlinesData: Utils.getProjectDetailHeadlineEmployee(projectDetails),
  };

  useEffect(() => {
    getProjectDetails();
  }, [isUpdateMade]);

  useEffect(() => {
    getProjectDetails();
  }, []);
  useEffect(() => {
    if (projectDetails) {
      //projectRolesView change not required
      const delegatesFromDb = projectDetails?.projectRoles?.filter(
        (r) =>
          r.role.toLowerCase().trim() ===
          constant.ROLE_TYPES.DELEGATE_ROLE.toLowerCase().trim()
      );
      //projectRolesView change not required
      const additionalElFromDb = projectDetails?.projectRoles?.filter(
        (r) =>
          r.role.toLowerCase().trim() ===
          constant.ROLE_TYPES.ADDITIONAL_EL_ROLE.toLowerCase().trim()
      );
      //projectRolesView change not required
      setDelegateDBData(delegatesFromDb);
      setadditionalElDBData(additionalElFromDb);
    }
  }, [projectDetails]);

  const handleTabChange = (tabValue: any) => {
    setTabValue(tabValue);
  };

  const getFlagForUpdate = (flag: boolean) => {
    setIsUpdateMade((prevState) => !prevState);
  };

  const isDescriptionChanges = () => {
    setIsUpdateButtonEnable(true);
  };

  const refreshHeader = () => {
    setRefresh(!refresh);
  };

  return (
    <Box sx={{ pl: 2, pr: 2 }}>
      <ProjectTitle
        currentTabValue={tabValue}
        designationData={designationData}
        projectDetails={projectDetails}
        setProjectDetails={setProjectDetails}
        elCurrentData={elCurrentData}
        dlCurrentData={dlCurrentData}
        getFlagForUpdate={getFlagForUpdate}
        getProjectDetails={getProjectDetails}
        skillCurrentData={skillCurrentData}
        isUpdateButtonEnable={isUpdateButtonEnable}
        refresh={refresh}
      />
      {isEmployee && <DescriptionLayout projectDetails={projectDetails} />}
      {(projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
        projectDetails?.pipelineStatus === EPipelineStatus.Lost) && <></>}
      <ProjectDetailHeadlines
        {...headlinesDataProps}
        currentTabValue={tabValue}
      />
      {isEmployee && (
        <DescriptionLayout
          projectDetails={projectDetails}
          assignDelegateHandler={handleDlChange}
        />
      )}

      {!isEmployee && Object.keys(projectDetails).length > 0 && (
        <RequestorView
          handleTabChange={handleTabChange}
          projectDetails={projectDetails}
          handleElChange={handleElChange}
          assignDelegateHandler={handleDlChange}
          handleCurrentDesignationChange={handleCurrentDesignationChange}
          handleSkillChange={handleSkillChange}
          setDelegateDBData={setDelegateDBData}
          delegateDBData={delegateDBData}
          setadditionalElDBData={setadditionalElDBData} //WIP
          additionalElDBData={additionalElDBData} //WIP
          handleDescriptionChanges={isDescriptionChanges}
          refreshHeader={refreshHeader}
          set
          {...props}
        />
      )}
    </Box>
  );
};

export default ProjectDetailsLayout;
