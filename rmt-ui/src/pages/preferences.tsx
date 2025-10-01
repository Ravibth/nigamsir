import React, { useEffect, useState } from "react";
import { UserDetailsContext } from "../contexts/userDetailsContext";
import { IsPermissionExistForApp } from "../global/utils";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import ManageLayout from "../components/manage/manage-layout";
import UnAuthorizedView from "./UnAuthorizedView";
import { LoaderContext, LoaderContextProps } from "../contexts/loaderContext";

const Preferences = () => {
  const userContext = React.useContext(UserDetailsContext);
  const [isLoaderOpen, setIsLoaderOpen] = useState(true);
  const [hasPermission, setHasPermission] = useState(false);
  useEffect(() => {
    // var hasPermission1 = IsPermissionExistForApp(
    //   userContext.modulePermissionsState,
    //   MODULE_NAME_ENUM.Profile,
    //   PERMISSION_TYPE.Read
    // );
    if (userContext && userContext.modulePermissionsState) {
      var hasPermission2 = IsPermissionExistForApp(
        userContext.modulePermissionsState,
        MODULE_NAME_ENUM.My_Preferences,
        PERMISSION_TYPE.Read
      );
      if (!hasPermission2) {
        //Current User has permission to access the page
        setHasPermission(false);
      } else {
        setHasPermission(true);
      }
      setIsLoaderOpen(false);
    } else {
      setIsLoaderOpen(true);
    }
  }, [userContext]);

  return (
    <>
      {userContext.modulePermissionsState && !isLoaderOpen ? (
        hasPermission ? (
          <ManageLayout></ManageLayout>
        ) : (
          <UnAuthorizedView></UnAuthorizedView>
        )
      ) : (
        <></>
      )}
    </>
  );
};

export default Preferences;
