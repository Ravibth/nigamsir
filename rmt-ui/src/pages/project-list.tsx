import React, { Fragment, useContext, useEffect } from "react";
import Home from "../components/home/home";
import { UserDetailsContext } from "../contexts/userDetailsContext";
import { useNavigate } from "react-router-dom";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import { IsPermissionExistForApp, ShowUnAuthorizedView } from "../global/utils";

const ProjectListingPage = (props: any) => {
  const { isEmployee } = props;
  const setIsEmployee = useContext(UserDetailsContext)?.setIsEmployee;
  const navigate = useNavigate();
  const userContext = React.useContext(UserDetailsContext);

  useEffect(() => {
    if (setIsEmployee) {
      setIsEmployee(isEmployee);
    }
    //debugger;
    var hasPermission = IsPermissionExistForApp(
      userContext.modulePermissionsState,
      MODULE_NAME_ENUM.Project_Details,
      PERMISSION_TYPE.Read
    );
    if (hasPermission) {
      //Current User has permssion to access the page
    } else {
      //Show Unathorized access error for the page
      ShowUnAuthorizedView(navigate);
    }
  }, []);

  return (
    <Fragment>
      <Home />
    </Fragment>
  );
};

export default ProjectListingPage;
