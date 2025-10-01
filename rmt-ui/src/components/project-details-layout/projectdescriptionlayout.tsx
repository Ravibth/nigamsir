import { Box, Grid } from "@mui/material";
import ProjectLeadingMembers from "../project-leader/project-leader";
import * as Utils from "./utils";
import ProjectDescription from "../ProjectDetail/project-description";
import { IAssignEngagementLeaderProps } from "../assign-engagement-leader/IAssignEngagementLeaderProps";
import { IAssignDelegateLeaderProps } from "../assign-delegate/IAssignDelegate";
import Stack from "@mui/material/Stack";
import Alert from "@mui/material/Alert";
import React, { useEffect } from "react";
import { IEQProfileProps } from "../project-leader/enquiry-owner-profile/IEQProfileProps";
import ELProfle from "../project-leader/engagement-leader-profile/engagement-leader-profile";
import { IPELeadersProflieProps } from "../project-leader/engagement-leader-profile/IPELProfileProps";
import * as gc from "../../global/constant";
import AssignDelegate2 from "../assign-delegate/assign-delegate-leader-2";
import AdditionalElAgGrid from "./additionalElAgGrid";
import "./style.css";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";

const DescriptionLayout = (props: any) => {
  const {
    projectDetails,
    setDelegateDBData,
    delegateDBData,
    additionalElDBData, //WIP
    setadditionalElDBData, //WIP
  } = props;
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);

  useEffect(() => {
    projectDetailsContext.setIsProjectDetailsDirty(false);
  }, []);

  useBlockRefreshAndBack(projectDetailsContext.isProjectDetailsDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(
    projectDetailsContext.isProjectDetailsDirty
  );

  const RRProps: IPELeadersProflieProps = {
    engagementLeaders: Utils.getProjectResourceRequestors(projectDetails),
  };

  const ProjectEngagementLeaderProps: IAssignEngagementLeaderProps = {
    EngagementLeadersList: Utils.getProjectEngagementorDLLeaders(
      projectDetails,
      gc.ROLE_TYPE.engagementLeader
    ),
  };
  const ProjectDelegateProps: IAssignDelegateLeaderProps = {
    DelegateList: Utils.getProjectEngagementorDLLeaders(
      projectDetails,
      gc.ROLE_TYPE.delegateUser
    ),
  };
  const ProjectEnquireOwnerProps: IEQProfileProps = {
    eqProfile: Utils.getProjectEnquireOwnerLeaders(projectDetails),
  };

  useEffect(() => {
    getDesignation();
  }, []);

  const getDesignation = async () => {};

  return (
    <div>
      {blocker.state === "blocked" &&
      projectDetailsContext.isProjectDetailsDirty ? (
        <DialogBox
          showDialog={projectDetailsContext.isProjectDetailsDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Grid container spacing={{ xs: 2, sm: 2, md: 3 }}>
        <Grid item xs={12} sm={6} md={7}>
          <ProjectLeadingMembers
            {...ProjectEnquireOwnerProps}
            {...ProjectDelegateProps}
            {...ProjectEngagementLeaderProps}
            {...projectDetails}
          />
          <>
            {props.projectDetails.isRollover == true && (
              <>
                <Stack sx={{ width: "99%" }} spacing={1}>
                  <Alert severity="warning">{`This project has been rolled over. Please navigate to Active Allocation tab to roll forward the allocations!`}</Alert>
                </Stack>
              </>
            )}
          </>
          <Box className="description-container">
            <ProjectDescription
              content={projectDetails.description}
              projectDetails={projectDetails}
              handleDescriptionChanges={props.handleDescriptionChanges}
            ></ProjectDescription>
          </Box>
        </Grid>

        <Grid item xs={12} sm={6} md={5}>
          <Box></Box>
          <Box className="primary-roles-container">
            <ELProfle {...RRProps} />
            <AssignDelegate2
              projectDetails={projectDetails}
              setDelegateDBData={setDelegateDBData}
              delegateDBData={delegateDBData}
            />
          </Box>
          <Box className="secondary-roles-container">
            <AdditionalElAgGrid
              projectDetails={projectDetails}
              setDelegateDBData={setDelegateDBData}
              delegateDBData={delegateDBData}
              additionalElDBData={additionalElDBData}
              setAdditionalElDbData={setadditionalElDBData}
            />
          </Box>
        </Grid>
      </Grid>
    </div>
  );
};

export default DescriptionLayout;
