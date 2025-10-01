import React from "react";
import { UserDetailsContext } from "../../contexts/userDetailsContext";

export enum PERMISSION_TYPE {
  Create = "create",
  Read = "read",
  Update = "update",
  Delete = "delete",
}

interface AccessControlProps {
  moduleName: string;
  type: PERMISSION_TYPE;
  children: any;
}

const AccessControl = (props: AccessControlProps) => {
  const userDetails: any = React.useContext(UserDetailsContext);

  const access = userDetails?.modulePermissionsState[props.moduleName];
  return access && access[props.type] ? props.children : "";
};
export default AccessControl;
