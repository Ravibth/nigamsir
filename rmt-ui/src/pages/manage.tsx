import React, { useEffect, useState } from "react";
import ManageLayout from "../components/manage/manage-layout";
import { IsPermissionExistForApp } from "../global/utils";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import { UserDetailsContext } from "../contexts/userDetailsContext";
import UnAuthorizedView from "./UnAuthorizedView";

const ManagePage = () => {
  const userContext = React.useContext(UserDetailsContext);
  const [hasPermission, setHasPermission] = useState(false);

  useEffect(() => {
    var hasPermission1 = IsPermissionExistForApp(
      userContext.modulePermissionsState,
      MODULE_NAME_ENUM.Profile,
      PERMISSION_TYPE.Read
    );
    var hasPermission2 = IsPermissionExistForApp(
      userContext.modulePermissionsState,
      MODULE_NAME_ENUM.My_Preferences,
      PERMISSION_TYPE.Read
    );
    if (!hasPermission1 && !hasPermission2) {
      //Current User has permssion to access the page
      setHasPermission(false);
    } else {
      setHasPermission(true);
    }
  }, []);

  return (
    <>
      {hasPermission ? (
        <ManageLayout></ManageLayout>
      ) : (
        <UnAuthorizedView></UnAuthorizedView>
      )}
    </>
  );
};

export default ManagePage;
