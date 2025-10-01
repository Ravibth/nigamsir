import React, { useEffect, useState } from "react";
import RolePermissionMain from "../components/role-permission-main/role-permission-main";
import { UserDetailsContext } from "../contexts/userDetailsContext";
import { IsPermissionExistForApp } from "../global/utils";
import { PERMISSION_TYPE } from "../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../common/module-permission/module-permission";
import UnAuthorizedView from "./UnAuthorizedView";

const RolePermission = () => {
  const userContext = React.useContext(UserDetailsContext);
  const [hasPermission, setHasPermission] = useState(false);

  useEffect(() => {
    var hasPermission = IsPermissionExistForApp(
      userContext.modulePermissionsState,
      MODULE_NAME_ENUM.Roles_and_Permissions,
      PERMISSION_TYPE.Read
    );
    setHasPermission(hasPermission);
  }, []);

  return (
    <>
      {hasPermission ? (
        <RolePermissionMain></RolePermissionMain>
      ) : (
        <UnAuthorizedView></UnAuthorizedView>
      )}
    </>
  );
};

export default RolePermission;
