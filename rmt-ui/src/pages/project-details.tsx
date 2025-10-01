import React, { Fragment, useEffect } from "react";
import ProjectDetailsLayout from "../components/project-details-layout/project-details-layout";
import { UserDetailsContext } from "../contexts/userDetailsContext";
import { ProjectUpdateDetailsState } from "../contexts/projectDetailsContext";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import {
  ShowUnAuthorizedView,
  checkUserPermissionsForProject,
} from "../global/utils";

const ProjectDetailPage = (props: any) => {
  // useContext(UserDetailsContext).setIsEmployee(props.isEmployee);
  const { state } = useLocation();
  const userContext = React.useContext(UserDetailsContext);
  const navigate = useNavigate();

  const { pipelineCode, jobCode } = useParams();

  useEffect(() => {
    checkUserPermissionsForProject(
      pipelineCode,
      jobCode,
      userContext,
      MODULE_NAME_ENUM.Project_Details,
      PERMISSION_TYPE.Read
    ).then((hasPermission: any) => {
      if (hasPermission) {
        //Current User has permission to access the page
      } else {
        //Show Unauthorized access error for the page
        ShowUnAuthorizedView(navigate);
      }
    });
  }, []);

  return (
    <Fragment>
      <ProjectUpdateDetailsState>
        <ProjectDetailsLayout navigationState={state} {...props} />
      </ProjectUpdateDetailsState>
    </Fragment>
  );
};

export default ProjectDetailPage;
