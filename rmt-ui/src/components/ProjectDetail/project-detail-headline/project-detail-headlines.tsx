import { Box, Grid, Typography } from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import * as constant from "./Constant";
// import { IProjectDetailHeadlinesProps } from "./IProjectDetailHeadlinesProps";
import "./style.css";
import { Accordion, AccordionSummary, AccordionDetails } from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
// import { data } from "../../assign-resources/constant";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { useParams } from "react-router-dom";
import { GetProjectDetailsAsPerPipelineCodeAndUserRole } from "../../../services/project-list-services/project-list-services";
import {
  GetPropertyValueByTitle,
  // ProjectDetailsHeadline,
  ProjectHeadlinesView,
} from "./Constant";
import ProjectHeadlineView from "./project-headline-view/project-headline-view";
import { ProjectUpdateDetailsContext } from "../../../contexts/projectDetailsContext";
// import { ProjectChargeableType } from "../../project-types/constant";
import { DATE_TIME_INITIAL_VALUE } from "../../../global/constant";
import { fetchBudgetOverallData } from "../../ProjectTitle/utils";
import { getPipelineData } from "../../../services/wcgt-master-services/wcgt-master-services";
import _ from "lodash";
import { GetProjectHeadlineTitle } from "./util";
import moment from "moment";
import * as GC from "../../../global/constant";
import { IProjectMaster } from "../../../common/interfaces/IProject";

const ProjectDetailHeadlines = (props: any) => {
  const isEmployee = React.useContext(UserDetailsContext)?.isEmployee;
  const userDetails = React.useContext(UserDetailsContext);

  const [isAccordionExpanded, setIsAccordionExpanded] = useState(isEmployee);
  const [isLoading, setIsLoading] = useState(false);
  const [headlineData, setHeadlineData] = useState<ProjectHeadlinesView>();
  const { pipelineCode, jobCode } = useParams();
  const [projectData, setProjectData] = useState<IProjectMaster | null>(null);
  const { projectUpdateIsEndDateUpdated } =
    useContext(ProjectUpdateDetailsContext);

  const { setProjectUpdateEndDate, setProjectUpdateIsEndDateChanged } =
    useContext(ProjectUpdateDetailsContext);
  const [projectDetailViewTitles, setProjectDetailViewTitles] = useState([]);
  const FetchProjectDetails = (pipelineCode: string, jobCode: string) => {
    return new Promise((resolve, reject) => {
      GetProjectDetailsAsPerPipelineCodeAndUserRole(pipelineCode, jobCode)
        .then((response) => {
          setProjectData(response.data);
          resolve(response.data);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  useEffect(() => {
    if (headlineData && userDetails?.projectPermissionData?.projectRolesView) {
      const projectHeadlineTitleList = GetProjectHeadlineTitle(
        userDetails.role,
        userDetails.projectPermissionData.projectRolesView,
        headlineData.chargableType,
        headlineData.pipelineCode,
        headlineData.jobId,
        userDetails.buTreeMappingListByMID,
        projectData
      );
      setProjectDetailViewTitles(projectHeadlineTitleList);
    }
  }, [headlineData, userDetails]);
  useEffect(() => {
    if (jobCode != null && jobCode != "undefined") {
      setIsLoading(true);
      Promise.all([FetchProjectDetails(pipelineCode, jobCode)])
        .then((response) => {
          setIsLoading(false);
          const data: any = response[0];
          let projectHeadlineStatus = {
            ...data,
            jobId: jobCode,
          } as ProjectHeadlinesView;

          setHeadlineData(projectHeadlineStatus);
          Promise.all([
            fetchBudgetOverallData(
              [{ key: pipelineCode, value: jobCode }],
              moment(new Date()).format(GC.dateFormatYMD),
              moment(new Date()).format(GC.dateFormatYMD)
            ),
          ]).then((budgetResponse) => {
            if (budgetResponse && projectHeadlineStatus) {
              projectHeadlineStatus.budget = budgetResponse[0]?.data[0]
                ?.budgetedCost
                ? budgetResponse[0]?.data[0]?.budgetedCost
                : 0; //@ts-ignore
              setHeadlineData(projectHeadlineStatus);
            }
          });
          setProjectUpdateEndDate(projectHeadlineStatus.endDate);
        })
        .catch((error) => {
          setIsLoading(false);
          console.log(error);
        });
    } else if (pipelineCode != null) {
      setIsLoading(true);
      Promise.all([
        FetchProjectDetails(pipelineCode, jobCode),
        getPipelineData(),
      ])
        .then((response) => {
          setIsLoading(false);
          const data: any = response[0];
          let projectHeadlineStatus = {
            ...data,
            jobId: jobCode,
          } as ProjectHeadlinesView;
          const pipelineData = response[1];
          const selectedPipeline = _.find(pipelineData, {
            pipeline_code: pipelineCode,
          });
          const finalPropsedFee = selectedPipeline?.finalproposedfee
            ? selectedPipeline?.finalproposedfee
            : 0;
          const wonExpected = selectedPipeline?.won_expected_recovery
            ? selectedPipeline?.won_expected_recovery
            : 0;
          projectHeadlineStatus.budget =
            (finalPropsedFee / wonExpected) * 100 + "";
          setHeadlineData(projectHeadlineStatus);

          setProjectUpdateEndDate(projectHeadlineStatus.endDate);
        })
        .catch((error) => {
          setIsLoading(false);
          console.log(error);
        });
    }
  }, [pipelineCode, jobCode]);

  const [editedValues, setEditedValues] = useState({});

  const handleEdit = (index, editedValue) => {
    setEditedValues((prevValues) => ({ ...prevValues, [index]: editedValue }));
  };
  return (
    <div className="header-accordion-container">
      {/* {userDetails?.projectPermissionData?.permissions ? ( */}
      <Accordion
        className="projectDetailsAccordian"
        expanded={isAccordionExpanded}
        onChange={() => setIsAccordionExpanded(!isAccordionExpanded)}
      >
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          className="header-accordion-title"
        >
          <Typography className="projectDetailsAccordianTitle">
            Project Headers
          </Typography>{" "}
          <Typography className="projectDetailsAccordianTitle projectDetailsAccordianTitle-error-msg">
            {!isLoading &&
              !projectUpdateIsEndDateUpdated &&
              (headlineData?.endDate == null ||
                headlineData?.endDate?.toString() ===
                  DATE_TIME_INITIAL_VALUE) && (
                <span className="end-date-warning">{` *End Date missing! It is to be updated for performing tasks on the project.`}</span>
              )}
          </Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Box sx={constant.getProjectHeadlineSxProps()}>
            <Grid container spacing={0.8} columns={{ xs: 12 }}>
              {projectDetailViewTitles?.map((title, index) => {
                const value = GetPropertyValueByTitle(title, headlineData);
                const editedValue = editedValues[index] || value;
                return (
                  <ProjectHeadlineView
                    key={index}
                    title={title}
                    value={editedValue}
                    index={index}
                    headlineData={headlineData}
                    onEdit={handleEdit}
                    setProjectUpdateEndDate={setProjectUpdateEndDate}
                    setProjectUpdateIsEndDateChanged={
                      setProjectUpdateIsEndDateChanged
                    }
                    currentTabValue={props?.currentTabValue}
                  ></ProjectHeadlineView>
                );
              })}
            </Grid>
          </Box>
        </AccordionDetails>
      </Accordion>
      {/* ) : (
        <>
          <span style={{ display: "none" }}>Access denied!</span>
        </>
      )} */}
    </div>
  );
};

export default ProjectDetailHeadlines;
