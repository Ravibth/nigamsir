import { Container } from "@mui/material";
import React, { useEffect } from "react";
import Marketplacetabs from "./marketplacetabs/marketplacetabs";
import { MarketPlaceState } from "../../contexts/marketPlaceContext";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import {
  IsPermissionExistForApp,
  ShowUnAuthorizedView,
} from "../../global/utils";
import { MODULE_NAME_ENUM } from "../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../common/access-control-guard/access-control";
import { useNavigate } from "react-router-dom";
import "./style.css";
import { RolesListMaster } from "../../common/enums/ERoles";

const Marketplace = () => {
  const userContext = React.useContext(UserDetailsContext);
  const navigate = useNavigate();
  //Show market place page to users having employee role
  var isEmployee =
    userContext.role?.filter(
      (role) => role?.toLowerCase() == RolesListMaster.Employee.toLowerCase()
    )?.length > 0;

  useEffect(() => {
    var hasPermission = IsPermissionExistForApp(
      userContext.modulePermissionsState,
      MODULE_NAME_ENUM.Marketplace,
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
    <>
      {isEmployee ? (
        <MarketPlaceState>
          <Marketplacetabs />
        </MarketPlaceState>
      ) : (
        <div style={{ width: "100%", textAlign: "center" }}>
          {" "}
          <br />
          <br />
          You do not have permission to view this page!
        </div>
      )}
    </>
  );
};

export default Marketplace;
