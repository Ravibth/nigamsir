import {IProjectMaster,ProjectRole} from "../../../common/interfaces/IProject";
import {Box, Typography, Popover,Grid, Modal } from "@mui/material";
import {IResourceAllocationDetailsMaster, IResourceAllocationMaster} from "../../../common/interfaces/IAllocation";
import React, { useEffect, useState } from "react";
import { getAllProjectRolesByCodes, getBasicProjectDetailsRequestor, GetProjectByCode, getProjectCompleteDetails } from "../../../services/project-list-services/project-list-services";
import { GT_DESIGN_PARAMETERS, HR_ABBR, Role } from "../../../global/constant";
import "./style.css"; 
import { UserInfoTooltipColumnsSxPropsPortfolio,TooltipDetailsSxPropsPortfolio,UserInfoTooltipValuesSxPropsPortfolio } from "../util/constant";
import { WorkOutlineRounded } from "@mui/icons-material";
import ProjectView from "../../project-view/project-view";
import { projectDetailModal } from "../../scheduler-left/resource-menu/Constant";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { RolesListMaster } from "../../../common/enums/ERoles";

interface ProjectRowProps {
  project: IResourceAllocationDetailsMaster;
  efforts:number;
}

const ProjectRow: React.FC<ProjectRowProps> = ({project,efforts}) => {
  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
  const [projectCompleteInfo, setCompleteProjectInfo] = useState<IProjectMaster | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const userContext = React.useContext(UserDetailsContext);

  const open = Boolean(anchorEl);

  const handlePopoverOpen = async (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
    if (!projectCompleteInfo && !isLoading) {
      setIsLoading(true);
      try {
        const resp = await getProjectCompleteDetails(
          project?.pipelineCode,
          project?.jobCode
        );     
        if(!resp) return;   
        setCompleteProjectInfo(resp?.data);
      } catch (error) {
        console.error("Error fetching project details:", error);
      } finally {
        setIsLoading(false);
      }
    }
  };

  const handlePopoverClose = () => setAnchorEl(null);

  const el = projectCompleteInfo?.projectRoles?.find(x => x.role === Role.EngagementLeader)?.userName || "N/A";
  const csp = projectCompleteInfo?.projectRoles?.find(x => x.role === Role.CSP)?.userName || "N/A";


  const [projectBasicDetail, setBasicProjectDetail] = useState<any>();
  const [openView, setOpenView] = useState(false);
  const [role, setRole] = useState<any>();


  const handleProjectClick = async (
    event: any,
  ) => {
    event.preventDefault();    
    let payload: any[] = role;
    GetAllProjectRolesByCodes(project?.pipelineCode, project?.jobCode)
      .then((values: ProjectRole[]) => {
        payload = values?.filter(
          (a) =>
            a.user?.toLowerCase()?.trim() ===
            userContext.username?.toLowerCase()?.trim()
        )
          .map((item) => item.role);
        let appRole = values?.filter(
          (a) =>
            a.user?.toLowerCase()?.trim() ===
            userContext.username?.toLowerCase()?.trim()
        )
          .map((item) => item.applicationRole);
        payload.push(...appRole);

        let adminRoles = userContext?.role?.filter(
          (a) =>
            a?.toLowerCase() === RolesListMaster.Admin.toLowerCase() ||
            a?.toLowerCase() === RolesListMaster.CEOCOO.toLowerCase() ||
            a?.toLowerCase() === RolesListMaster.SystemAdmin.toLowerCase()
        );
        if (payload.length === 0 && adminRoles && adminRoles.length > 0) {
          payload.push(...adminRoles);
        }
        let leadersRoles = userContext?.role?.filter(
          (a) => a?.toLowerCase() == RolesListMaster.Leaders.toLowerCase()
        );
        if (payload.length == 0 && leadersRoles && leadersRoles.length > 0) {
          payload.push(...leadersRoles);
        }
        setRole(payload);
      })
      .catch(error => {
        console.error('Error:', error);
      });

    Promise.all([
      GetBasicProjectDetailRequestorByPipelineCode(
        project?.pipelineCode,
        project?.jobCode
      ),
    ]).then((values: any) => {
      setBasicProjectDetail({
        PipelineCode: project?.pipelineCode,
        StartDate: new Date(values[0].startDate).toLocaleDateString("en-US", {
          year: "numeric",
          month: "long",
          day: "numeric",
        }),
        EndDate: new Date(values[0].endDate).toLocaleDateString("en-US", {
          year: "numeric",
          month: "long",
          day: "numeric",
        }),
        Expertise: values[0].expertise,
        ProjectType: values[0].chargableType,
        Sme: values[0].sme,
        Client: values[0].clientName,
        BudgetStatus: values[0].budgetStatus,
        jobCode: values[0].jobCode,
      });
      openViewHandler();
    })
  }
  const openViewHandler = () => {
    setOpenView(true);
  };

  const closeViewHandler = () => {    
    setOpenView(false);
    console.log(openView)
  };

  return (
    <div className="project-row">      
      <Modal open={openView} onClose={closeViewHandler}>
        <Box sx={projectDetailModal}>
          <ProjectView
            closeHandler={closeViewHandler}
            isLoading={isLoading}
            setIsLoading={setIsLoading}
            role={role}
            {...projectBasicDetail}
          />
        </Box>
      </Modal>
      <div className="project-content">
        <WorkOutlineRounded className="project-icon" sx={{ color: GT_DESIGN_PARAMETERS.GTTealColor }} />
        <div className="project-text-container" onClick={handleProjectClick}>
          <Typography
          variant="subtitle1"          
          color="textPrimary"          
          className="project-title"
          aria-owns={open ? "mouse-over-popover" : undefined}
          aria-haspopup="true"
          onMouseEnter={handlePopoverOpen}
          onMouseLeave={handlePopoverClose}
          >

            {project.jobName.length > 32 ? `${project.jobName.substring(0, 32)}...` : project.jobName}
          </Typography>
          <div className="project-title-hrs">              
              {efforts} {HR_ABBR}
          </div>
          
        <Typography sx={{ ...TooltipDetailsSxPropsPortfolio, maxWidth: "550px" }} >    
          <Popover
            id="mouse-over-popover"
            sx={{ pointerEvents: "none" }}
            open={open}
            anchorEl={anchorEl}
            anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
            transformOrigin={{ vertical: "top", horizontal: "left" }}
            onClose={handlePopoverClose}
            disableRestoreFocus
          >
            <Box className="projectrow-popover-box" sx={{ ...TooltipDetailsSxPropsPortfolio, maxWidth: "500px", minWidth: "200px" }}>
              <Grid container spacing={2}>
                {isLoading ? (
                  <Grid item xs={12}>
                    <Typography variant="body2">Loading...</Typography>
                  </Grid>
                ) : (
                  [
                    { label: 'Project Name', value: project?.jobName },
                    { label: 'Client', value: projectCompleteInfo?.clientName },
                    { label: 'Client Group', value: projectCompleteInfo?.clientGroup },
                    { label: 'EL', value: el },
                    { label: 'CSP', value: csp }
                  ].map((item, index) => (
                    <Grid item xs={12} key={index}>
                      <Grid container spacing={2}>
                        <Grid item xs={3}>
                          <Typography sx={UserInfoTooltipColumnsSxPropsPortfolio}>
                            {item.label}
                          </Typography>
                        </Grid>
                        <Grid item xs={9}>
                          <Typography sx={UserInfoTooltipValuesSxPropsPortfolio}>
                            {item.value || "N/A"}
                          </Typography>
                        </Grid>
                      </Grid>
                    </Grid>
                  ))
                )}
              </Grid>
            </Box>
          </Popover>
        </Typography>
        </div>
      </div>
    </div>
  );
};

const GetBasicProjectDetailRequestorByPipelineCode = (
  pipelineCode: string,
  jobCode: string
) => {
  return new Promise((resolve, reject) => {
    getBasicProjectDetailsRequestor(pipelineCode, jobCode)
      .then((resp) => {
        resolve(resp.data);
      })
      .catch((ex) => { });
  });
};

const GetAllProjectRolesByCodes = (
  pipelineCode: string,
  jobCode: string
) => {
  return new Promise((resolve, reject) => {
    getAllProjectRolesByCodes(pipelineCode, jobCode, [])
      .then((resp) => {
        resolve(resp);
      })
      .catch((ex) => { });
  });
};


export default ProjectRow;
