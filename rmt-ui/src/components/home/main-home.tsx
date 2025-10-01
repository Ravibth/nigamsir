import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
  useIsAuthenticated,
} from "@azure/msal-react";
import React, { useEffect, useState } from "react";
import { ErrorResponse, useLocation } from "react-router-dom";
import Login from "../login/login";
import { getLoggedInUserInfo, signout } from "../../auth/authService";
import { getUserByEmail } from "../../services/role-permission-service/role-permission-service";
import { AccountInfo } from "@azure/msal-browser";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import Loader from "../loader/loader";
import { getTheListOfModulePermissionsMappingToUser } from "../../common/module-permission/module-permission";
import UnauthorizedUserPromptModal from "../unauthorized-user-prompt-modal/unauthorized-user-prompt-modal";
import { Chip, Modal } from "@mui/material";
import RoutesConfig from "../../routes/routeConfig";
import BlockIcon from "@mui/icons-material/Block";
import LogoutIcon from "@mui/icons-material/Logout";
import { getBUTreeMappingListByMID } from "../../services/configuration-services/configuration.service";
import { ERRORCODE, ErrorType, ServiceErrorResponse } from "./constant";

const MainHome = () => {
  const currentLocationModule = useLocation();
  const [loader, setLoader] = useState<boolean>(false);
  //user is authenticated but check permission on the portal
  const [unAuthorizedUser, setUnAuthorizedUser] = useState<boolean>(null);
  const [errRespones, setErrRespones] = useState<ServiceErrorResponse>(null);
  const isAuthenticatedUser = useIsAuthenticated();
  const userDetails = React.useContext(UserDetailsContext);
  const [openInvalidModal, setOpenInvalidModal] = useState(false);
  const [open, setOpen] = useState(true);
  const handleClose = () => setOpen(false);

  const authenticateAndFetchUserInfo = async () => {
    const currentAccountInformation = await getLoggedInUserInfo();
    setLoader(true);
  let errorResponse: ServiceErrorResponse =  {  
      errorType: ErrorType.unknown,
      msg: "",
    };
    Promise.all([fetchUserInfo(currentAccountInformation)])
      .then((resp) => {
        setLoader(false);
        setErrRespones({  
            errorType: ErrorType.unknown,
            msg: "",
          })
      })
      .catch((err: ServiceErrorResponse) => {
        setLoader(false);
       // if (err.ErrorType == ErrorType.Unauthorized ) {
          setUnAuthorizedUser(true);
          setErrRespones({  
            errorType: err.errorType,
            msg: err?.msg,
            label: err?.label
          });
       // }
      });
  };

  useEffect(() => {
    if (isAuthenticatedUser) {
      authenticateAndFetchUserInfo();
    }
  }, [isAuthenticatedUser]);

  const fetchUserInfo = async (
    currentAccountInformation: AccountInfo | null
  ) => {
    let errorResponse: ServiceErrorResponse =  {  
              errorType: ErrorType.unknown,
              msg: "",
              label: ""
            };   
    if (currentAccountInformation !== null) {
      return new Promise((resolve, reject) => {
        getUserByEmail(currentAccountInformation.username)
          .then(async (resp: any) => {
            if (resp) {
              if (resp.status === false) {
                setTimeout(() => {
                  signout();
                }, 6000);
              } else {
                userDetails.setName(resp.name);
                userDetails.setUserName(resp.email_id);
                userDetails.setRole(resp.role_list);
                userDetails.setEmployeeId(resp.employee_id);
                userDetails.setCompetency(resp.competency);
                userDetails.setCompetencyId(resp.competencyId);

                userDetails.setIsEmployee(false);
                userDetails.setDesignation(resp.designation);
                userDetails.setserviceLine(resp.service_line);

                await Promise.all([
                  getBUTreeMappingListByMID(resp.employee_id),
                  getTheListOfModulePermissionsMappingToUser(),
                ]).then((promiseResponse) => {
                  if (
                    promiseResponse &&
                    promiseResponse[0] &&
                    promiseResponse[0]?.data
                  ) {
                    userDetails.setBuTreeMappingListByMID(
                      promiseResponse[0].data
                    );
                  }
                  if (promiseResponse && promiseResponse[1]) {
                    userDetails.dispatchModulePermissions(promiseResponse[1]);
                  }
                });                
                resolve("");
              }
            } else {                       
              errorResponse.errorType = ErrorType.Unauthorized;
              errorResponse.msg = "You do not have required permission to access this portal! Please contact the Site Administrator";
              errorResponse.label = "Access Denied";
              reject(errorResponse);                  
            }
          })
          .catch((err) => {     
                
            if (err?.status && ERRORCODE.indexOf(err?.status) > -1) {
              errorResponse.errorType = err.status;
              errorResponse.msg = err.message;
              errorResponse.label = err?.response?.statusText;
              reject(errorResponse);
              return;
            }            
            else {
              errorResponse.errorType = ErrorType.Unauthorized;
              errorResponse.msg = "You do not have required permission to access this portal! Please contact the Site Administrator";
              errorResponse.label = "Access Denied";
              reject(errorResponse);
               reject(`{ErrorType: ${ErrorType.Unauthorized},Msg : UnauthenticatedUser, label : Access Denied!}`);               
            }
          });
      });
    }
  };

  useEffect(() => {
    validationFired(
      currentLocationModule.pathname,
      userDetails.name,
      userDetails.navList
    );
  }, [currentLocationModule.pathname, userDetails.name, userDetails.navList]);

  const validationFired = (path: any, name: any, navList: any) => {
    if (name && navList.length > 0) {
      if (
        currentLocationModule.pathname.length === 1 ||
        navList.find(
          (item) =>
            currentLocationModule.pathname.length > 1 &&
            currentLocationModule.pathname.indexOf(item.path) != -1
        )
      ) {
        setOpenInvalidModal(false);
      } else {
        setOpenInvalidModal(true);
      }
    }
  };

  const logoutClick = async () => {
    setTimeout(() => {
      signout();
    }, 500);
  };

  return (
    <>
      <UnauthenticatedTemplate>
        <Login />
      </UnauthenticatedTemplate>
      <AuthenticatedTemplate>
        {userDetails.username &&
        userDetails.modulePermissionsState != "" &&
        !loader ? (
          <>
            {openInvalidModal === true ? (
              <Modal open={open} onClose={handleClose}>
                <UnauthorizedUserPromptModal handleClose={handleClose} />
              </Modal>
            ) : (
              <RoutesConfig />
            )}
          </>
        ) : (
          <>{loader && <Loader />}</>
        )}
        {unAuthorizedUser == true && (
          <>
            <div className="unauthorized-main">
              <div
                style={{ fontSize: "30px", width: "100%", textAlign: "center" }}
              >
                <Chip
                  style={{ fontSize: "large" }}
                  icon={<BlockIcon fontSize="large" />}
                  label= {errRespones.label ? errRespones.label : "Access Denied!"}
                />
                <br></br>
                {errRespones.msg ? errRespones.msg : "You do not have required permission to access this portal! Please contact the Site Administrator"}
              </div>
              <div style={{marginTop:"10px"}}>
                <Chip
                  title="Click here to logout and switch account."
                  style={{ fontSize: "small" }}
                  icon={<LogoutIcon fontSize="small" />}
                  label="Switch account"
                  onClick={logoutClick}
                />
              </div>
            </div>
          </>
        )}
      </AuthenticatedTemplate>
    </>
  );
};

export default MainHome;
