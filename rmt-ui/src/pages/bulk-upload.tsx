import React, { Fragment, useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import BulkUploadMain from "../components/bulk-upload-main/bulk-upload-main";
import {
  GetProjectRoleAndPermission,
  ShowUnAuthorizedView,
} from "../global/utils";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../contexts/userDetailsContext";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../contexts/snackbarContext";
import { RolesListMaster } from "../common/enums/ERoles";
import { IProjectMaster } from "../common/interfaces/IProject";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import { GetProjectByCode } from "../services/project-list-services/project-list-services";

const BulkUpload = () => {
  const rolesWhoCanAccess = [
    RolesListMaster.Delegate,
    RolesListMaster.ResourceRequestor,
  ];

  const [projectDetails, setProjectDetails] = useState(null);
  const { pipelineCode, jobCode } = useParams();
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const navigate = useNavigate();
  useEffect(() => {
    //************ Get Project details */
    GetProjectByCode(pipelineCode, jobCode).then(async (response: any) => {
      // IsCreationAllowedForUser(response.data);
      setProjectDetails(response.data);
      var hasPermission = false;
      if (response.data) {
        hasPermission = await GetProjectRoleAndPermission(
          response.data,
          userDetailsContext,
          pipelineCode,
          jobCode,
          MODULE_NAME_ENUM.Requisition,
          PERMISSION_TYPE.Create
        );
      }
      if (hasPermission === true) {
        //Current User has permssion to access the page
      } else {
        //Show Unathorized access error for the page
        ShowUnAuthorizedView(navigate);
      }
    });
  }, []);
  const IsCreationAllowedForUser = (projectInfo: IProjectMaster) => {
    const permissions =
      userDetailsContext.modulePermissionsState[MODULE_NAME_ENUM.Requisition];

    //projectRolesView change
    const loggedInUserRoleInProject = projectInfo?.projectRolesView?.filter(
      (item: any) =>
        item.user.trim().toLowerCase() ===
          userDetailsContext.username.trim().toLowerCase() &&
        //Project Specific Role will bes used only Application Role Not Needed
        rolesWhoCanAccess.includes(item.role)
    );
    if (
      loggedInUserRoleInProject &&
      loggedInUserRoleInProject.length > 0 &&
      permissions?.create
    ) {
      //
    } else {
      navigate("/");
      snackbarContext.displaySnackbar(
        "Permissions not found",
        SnackbarSeverity.ERROR
      );
    }
  };
  return (
    <Fragment>
      <BulkUploadMain projectDetails={projectDetails} />
    </Fragment>
  );
};

export default BulkUpload;
