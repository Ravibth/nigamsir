import { Box, Button, Modal } from "@mui/material";
import React, { useState, useEffect, useRef, useContext } from "react";
import {
  AssignRoleButtonSXProps,
  IAllRoleList,
  ModalDialogSXProps,
  rolePermission,
} from "./constant";
import "./role-permission-main.css";
import RoleName from "./role-name/role-name";
import ModulePermission from "./module-permission/module-permission";
import Users from "./users/users";
import UserRolesModalDetail from "./role-permission-modal/user-roles-modal-detail";
import {
  getAllRoles,
  getAllUsers,
  getAllModulePermissions,
  getUsersBasedOnRoleName,
  getModulePermissionsBasedOnRoleName,
  updateUserStatus,
  addNewUserWithRoles,
  updateUserRoles,
  getUserByEmail,
} from "../../services/role-permission-service/role-permission-service";
import SplitPaneVertical from "./splitPane";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import { SnackbarContext } from "../../contexts/snackbarContext";
import { LoaderContext } from "../../contexts/loaderContext";
import { AgGridReact } from "ag-grid-react";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import { RolesListMaster } from "../../common/enums/ERoles";

const RolePermissionMain = () => {
  const snackbarContext: any = useContext(SnackbarContext);
  const loaderContext: any = useContext(LoaderContext);
  const userDetailsContext: any = useContext(UserDetailsContext);
  const gridComponentRef = useRef<AgGridReact | null>(null);
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    // if (isDataCheckedDirty) {
    //   setIsDirty(true);
    // } else {
    //   setOpen(false);
    // }
    setOpen(false);
  };
  const [emailError, setEmailError] = useState(false);
  const [email, setEmail] = useState("");
  let roleSelected: any;
  const gridRefRoles = useRef();
  const gridRef: any = useRef();
  const gridRefModulePermission: any = useRef();
  let userRowDataStatic: any = [];
  const [roleRowData, setRoleRowData] = useState([]);
  const [userRowData, setUserRowData] = useState<Array<any>>([]);
  const [modulePermissionRowData, setModulePermissionRowData] = useState<any>(
    []
  );
  const [showModulePermission, setShowModulePermission] = useState(false);
  const [roleIdSelected, setRoleIdSelected] = useState<any>();
  const [emailIdselected, setEmailIdselected] = useState("");
  const [userdetails, setUserDetails] = useState({});
  const [isuserEmailFound, setIsUserEmailFound] = useState(false);
  const [emailSearchErrorMessage, setEmailSearchErrorMessage] = useState("");
  const [displayUserNotFoundOnSearch, setdisplayUserNotFoundOnSearch] =
    useState(false);
  const [rolesAssignedToUserSelcted, setRolesAssignedToUserSelected] = useState(
    []
  );
  const [roleRowDataForGrid, setRoleRowDataForGrid] = useState<any>([]);
  const [showUserRoles, setShowUserRoles] = useState<any>();
  const [finalrolesToUpdate, setFinalrolesToUpdate] = useState<any>();
  const [selectDropdownOpenedData, setSelectDropdownOpenedData] = useState<{
    email_id: string;
    opened: boolean;
  }>();
  const [selectedRoles, setSelectedRoles] = useState([]);
  const [roleAssigningPermissions, setRoleAssigningPermissions] = useState([]);
  const getRef = (refParam: any) => {};
  const [checkUserExist, setCheckUserExist] = useState<boolean>();
  const [isDirty, setIsDirty] = useState<boolean>(false);
  const [isDataCheckedDirty, setIsDataCheckedDirty] = useState<boolean>(false);

  // Get All Roles
  const getListIOfAllRoles = () => {
    return new Promise((resolve, reject) => {
      getAllRoles().then((resp) => {
        resolve(resp);
        setRoleRowDataForGrid(resp);
        userRowDataStatic = resp;
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar("Error Fetching Roles", "error", 6000);
    });
  };

  // Get All Users
  const getListIOfAllUsers = () => {
    return new Promise((resolve, reject) => {
      getAllUsers().then((resp) => {
        resolve(resp);
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar("Error Fetching Users", "error", 6000);
    });
  };

  // Get All Modules & Permissions
  const getListIOfAllModulesPermissions = () => {
    return new Promise((resolve, reject) => {
      getAllModulePermissions().then((resp) => {
        resolve(resp);
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar(
        "Error Fetching Modules Permissions",
        "error",
        6000
      );
    });
  };

  //Get user by email
  const getUserOnSearchByEmail = (emailId: string): Promise<any> => {
    return new Promise((resolve, reject) => {
      getUserByEmail(emailId).then((resp: any) => {
        resolve(resp);
        if (resp && resp.is_existing) {
          setCheckUserExist(resp.is_existing);
        }
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar(err.response.data.error, "error", 6000);
    });
  };

  const loggedinuserRle = userDetailsContext?.role.includes("Admin")
    ? "Admin"
    : userDetailsContext?.role.includes(RolesListMaster.SystemAdmin) //SystemAdmin Change System Admin
    ? RolesListMaster.SystemAdmin //SystemAdmin Change System Admin
    : null;
  const rolePermissionsAssigncheck = () => {
    let pers: any = [];
    const rolepermissions = rolePermission.find(
      (perm) => perm.role_name === loggedinuserRle
    );
    if (rolepermissions) {
      const permissions = rolepermissions.permissions ?? [];
      pers = roleRowData.map((item: any) => {
        const permission = permissions.find(
          (perm) => perm.Name.toLowerCase() === item.role_name.toLowerCase()
        );
        return {
          Name: item.role_name,
          Permissions: permission ? permission.Perm : [],
        };
      });
      setRoleAssigningPermissions(pers);
    }
  };

  //addUser
  const assignNewUserWithRoles = (newUserData: any) => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      addNewUserWithRoles(newUserData).then((resp) => {
        resolve(resp);
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "User Onboarded Successfully!",
          "success",
          6000
        );
      });
    }).catch((err) => {
      loaderContext.open(false);
      snackbarContext.displaySnackbar(
        "Error in Assigning New User",
        "error",
        6000
      );
    });
  };

  const updateUserWithRoles = (emailId: string, roles: any) => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      updateUserRoles(emailId, roles).then((resp: any) => {
        resolve(resp);
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "User role & permission update successful.",
          "success",
          6000
        );
      });
    }).catch((err) => {
      loaderContext.open(false);
      snackbarContext.displaySnackbar(
        "Error in Updating User Role",
        "error",
        6000
      );
    });
  };

  const checkandAssignNewEntry = (newUserData: any) => {
    if (checkUserExist === true) {
      Promise.all([
        updateUserWithRoles(newUserData.email_id, newUserData.roles),
      ]).then((value: any) => {
        let updatedUserRoleData = userRowData.map((e: any) => {
          if (e.email_id !== newUserData.email_id) {
            return e;
          } else {
            return {
              ...e,
              role_list: value[0].data.role_list,
            };
          }
        });
        setUserRowData(updatedUserRoleData);
      });
    } else {
      Promise.all([assignNewUserWithRoles(newUserData)]).then((value: any) => {
        setUserRowData((prevState: any) => {
          return [...prevState, value[0].data];
        });
      });
    }
  };

  const assignNewUserApi = async (emp_Email: string) => {
    loaderContext.open(true);
    getUserOnSearchByEmail(emp_Email)
      .then((newUserResp: any) => {
        if (newUserResp) {
          setRolesAssignedToUserSelected(newUserResp.role_list || []);
          setSelectedUserRolesGridData(
            newUserResp,
            newUserResp.role_list || []
          );
          setUserDetails(newUserResp);
          setIsUserEmailFound(true);
          setdisplayUserNotFoundOnSearch(false);
          loaderContext.open(false);
          return newUserResp;
        } else {
          loaderContext.open(false);
          setIsUserEmailFound(false);
          setdisplayUserNotFoundOnSearch(true);
        }
      })
      .catch((error) => {
        loaderContext.open(false);
        setIsUserEmailFound(false);
        setdisplayUserNotFoundOnSearch(true);
      });
  };

  // assign users using email
  const emailValidateSearch = async (emailid: string) => {
    setEmailIdselected(emailid);
    const response = await assignNewUserApi(emailid);
    return response;
  };

  // for update status
  const updateTableAfterEditFunctionality = async (resp: any) => {
    if (roleIdSelected) {
      const findIfUpdatedUserDataHasSelectedRole = resp.role_list.find(
        (role: any) => role.role === roleIdSelected
      );
      updateAndReplaceUser(
        userRowData,
        resp
        //todo: saif // findIfUpdatedUserDataHasSelectedRole === undefined
      );
    } else {
      updateAndReplaceUser(userRowData, resp);
    }
  };

  const updateStatusApi = () => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      const userFound = (userRowData || []).filter((user: any) => {
        return user.email_id === emailIdselected;
      });
      updateUserStatus(emailIdselected, !userFound[0].status)
        .then(async (resp) => {
          if (resp.data) {
            await updateTableAfterEditFunctionality(resp.data);
            resolve(resp.data);
          }
          loaderContext.open(false);
        })
        .catch((err) => {
          reject(err);
          loaderContext.open(false);
        });
    });
  };

  const updateUserRolesOnGrid = async (roles: any, emailId: string) => {
    let response: any;
    if (userdetails === undefined || Object.keys(userdetails).length === 0) {
      await updateUserRoles(emailId, roles)
        .then((resp) => {
          updateTableAfterEditFunctionality(resp.data);
          response = resp.data;
        })
        .catch((err) => {});
    } else {
      const newUserData: any = { ...userdetails };
      newUserData.roles = roles;
      await addNewUserWithRoles(newUserData)
        .then((resp) => {
          updateTableAfterEditFunctionality(resp.data);
          response = resp.data;
        })
        .catch((err) => {});
    }
    return response;
  };

  const afterStatusUpdate = () => {
    Promise.all([updateStatusApi()]).then((values) => {
      // setUserRowData(values); // todo:saif
    });
  };

  const updateAndReplaceUser = (allusers: any, updatedUser: any) => {
    const updatedUsers = userRowData.map((user) =>
      user.email_id === updatedUser.email_id ? updatedUser : user
    );
    setUserRowData([]);
    setUserRowData(updatedUsers);
  };

  //For Users and Module Permissions based on roles
  const selectIdApi = async () => {
    loaderContext.open(true);
    getModulePermissionsBasedOnRoleName(roleSelected).then(
      (moduleResponse: any) => {
        loaderContext.open(false);

        if (moduleResponse) {
          setModulePermissionRowData(moduleResponse.data);
        }
      }
    );
    return new Promise((resolve, reject) => {
      getUsersBasedOnRoleName(roleSelected).then((resp: any) => {
        loaderContext.open(false);

        if (resp) {
          setUserRowData(resp.data.rows);
          userRowDataStatic = resp.data.rows;
        }
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar(
        "Error in Fetching Users and Premissions based on role",
        "error",
        6000
      );
      loaderContext.open(false);
    });
  };

  const isAnyRoleSelected = async (rolename: any) => {
    if (roleIdSelected && rolename === roleIdSelected) {
      await getListIOfAllUsers();
      setRoleIdSelected("");
    } else {
      setShowModulePermission(true);
      setRoleIdSelected(rolename);
      roleSelected = rolename;
      await selectIdApi();
    }
  };

  const setSelectedUserRolesGridData = async (userDetails: any, roles: any) => {
    let allRoleListWithDisableFlag = roleRowDataForGrid.map((role: any) => {
      const assignedAndUnAssignedRoleList: IAllRoleList = {};
      const isAlreadyAssigned = roles.some(
        (element: any) => element.toLowerCase() === role.role_name.toLowerCase()
      );
      assignedAndUnAssignedRoleList.role_name = role.role_name;
      assignedAndUnAssignedRoleList.isAssigned = isAlreadyAssigned;

      return assignedAndUnAssignedRoleList;
    });
    const unlistedRoles = roles.filter(
      (assignedRole: any) =>
        !allRoleListWithDisableFlag.some(
          (role: any) => role.role_name === assignedRole.role
        )
    );

    unlistedRoles.forEach((role: any) => {
      const roleDetails: IAllRoleList = {
        role_name: role,
        isAssigned: true,
        isDisabled: false,
      };
      allRoleListWithDisableFlag.push(roleDetails);
    });
    setShowUserRoles(allRoleListWithDisableFlag);
    setFinalrolesToUpdate(allRoleListWithDisableFlag);
  };

  useEffect(() => {}, [showUserRoles]);
  const handleChangeRolesSelection = async (event: any, params: any) => {
    let findIfSelectedRoleOnPageIsDeSelectedFromUserRole: any;
    if (roleIdSelected) {
      const roleSelectedName = roleRowDataForGrid.filter((role: any) => {
        if (role.role_name === roleIdSelected) {
          return role.role_name;
        }
      });
      findIfSelectedRoleOnPageIsDeSelectedFromUserRole =
        event.target.value.includes(roleSelectedName[0].role_name);
    }

    if (event.target.value.length === 0) {
      snackbarContext.displaySnackbar("Select at-least 1 role!", "error", 6000);
    } else {
      loaderContext.open(true);
      await updateUserRolesOnGrid(event.target.value, params.data.email_id)
        .then((resp: any) => {
          if (!findIfSelectedRoleOnPageIsDeSelectedFromUserRole) {
            const newUserUpdatedData = userRowData.map((user) => {
              if (user.email_id !== params.data.email_id) {
                return user;
              } else {
                return resp;
              }
            });
            setUserRowData([]);
            setUserRowData((prevData: any) => {
              return [...newUserUpdatedData];
            });
            snackbarContext.displaySnackbar(
              "User role & permission update successful.",
              "success",
              6000
            );
            setSelectDropdownOpenedData({
              email_id: "",
              opened: false,
            });
          }
          loaderContext.open(false);
        })
        .catch((err: any) => {
          loaderContext.open(false);
          alert("Failed, please try again later");
        });
    }
  };

  //update roles
  const updateRolesOnClick = (rolesApplied: any) => {
    let tempSelectedRoles = showUserRoles;
    if (
      showUserRoles.find(
        (role: any) => role.role_name === rolesApplied.role_name
      )
    ) {
      tempSelectedRoles = (showUserRoles || []).filter((userRole: any) => {
        if (userRole.role_name === rolesApplied.role_name) {
          userRole.isAssigned = !userRole.isAssigned;
          return userRole;
        } else {
          return userRole;
        }
      });
    } else {
      tempSelectedRoles = [
        ...tempSelectedRoles,
        {
          role_name: rolesApplied.role_name,
          isAssigned: true,
          isDisabled: false,
        },
      ];
    }
    setShowUserRoles([]);
    setShowUserRoles(tempSelectedRoles);
  };

  const resetUsersData = () => {
    Promise.all([getListIOfAllUsers()]).then((values: any) => {
      setUserRowData(values[0]);
      setShowModulePermission(false);
    });
  };

  useEffect(() => {
    if (roleRowData && roleRowData.length > 0) {
      rolePermissionsAssigncheck();
    }
  }, [roleRowData]);
  const onLoadPageApiCalls = () => {
    loaderContext.open(true);
    Promise.all([
      getListIOfAllRoles(),
      getListIOfAllUsers(),
      getListIOfAllModulesPermissions(),
    ])
      .then((values: any) => {
        setRoleRowData(values[0]);
        setUserRowData(values[1]);
        setModulePermissionRowData(values[2]);
        loaderContext.open(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar("Error in fetching Data", "error");
        loaderContext.open(false);
      });
  };

  useEffect(() => {
    onLoadPageApiCalls();
  }, []);

  const isSystemAdminLogin = userDetailsContext?.role.includes(
    RolesListMaster.SystemAdmin
  ); //SystemAdmin Change System Admin
  // Route Block

  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);
  const handleClosePopup = () => {
    setIsDirty(false);
  };
  const handleOpenPopup = () => {
    setIsDirty(false);
    setOpen(true);
  };

  //----- Route Block -------//

  return (
    <div className={"rolePermissionMain"}>
      {isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleClosePopup}
          confirmNavigation={handleOpenPopup}
        />
      ) : null}
      <div className="headerMainContainer">
        <h2 className="user-management-heading">User Management</h2>
        {(userDetailsContext?.role.includes(RolesListMaster.SystemAdmin) ||
          userDetailsContext?.role.includes(RolesListMaster.Admin)) && ( //SystemAdmin Change System Admin
          <Button
            className="rmt-action-button"
            sx={AssignRoleButtonSXProps}
            onClick={handleOpen}
          >
            Assign Role
          </Button>
        )}
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={ModalDialogSXProps}>
            <UserRolesModalDetail
              roleRowData={roleRowData}
              emailError={emailError}
              setEmailError={setEmailError}
              email={email}
              setEmail={setEmail}
              setEmailSearchErrorMessage={setEmailSearchErrorMessage}
              isuserEmailFound={isuserEmailFound}
              userdetails={userdetails}
              setUserDetails={setUserDetails}
              handleClose={handleClose}
              emailValidateSearch={emailValidateSearch}
              displayUserNotFoundOnSearch={displayUserNotFoundOnSearch}
              setdisplayUserNotFoundOnSearch={setdisplayUserNotFoundOnSearch}
              updateRolesOnClick={updateRolesOnClick}
              userDetailsContext={userDetailsContext}
              checkandAssignNewEntry={checkandAssignNewEntry}
              showUserRoles={showUserRoles}
              roleAssigningPermissions={roleAssigningPermissions}
              isDirty={isDirty}
              setIsDirty={setIsDirty}
              setOpen={setOpen}
              setIsDataCheckedDirty={setIsDataCheckedDirty}
            />
          </Box>
        </Modal>
      </div>
      <div className="main-container-aggrids">
        <div className="splitter">
          <SplitPaneVertical>
            <div className="simulationDiv">
              <div
                className={
                  roleIdSelected !== undefined && showModulePermission
                    ? "ag-theme-alpine role-table role-table-half-height"
                    : "ag-theme-alpine role-table role-table-full-height"
                }
              >
                <RoleName
                  roleRowData={roleRowData}
                  gridRefRoles={gridRefRoles}
                  resetUsersData={resetUsersData}
                  isAnyRoleSelected={isAnyRoleSelected}
                  roleIdSelected={roleIdSelected}
                  setRoleIdSelected={setRoleIdSelected}
                />
              </div>
              <div>
                {roleIdSelected !== undefined && showModulePermission && (
                  <ModulePermission
                    modulePermissionRowData={modulePermissionRowData}
                    roleIdSelected={roleIdSelected}
                    gridRefModulePermission={gridRefModulePermission}
                  />
                )}
              </div>
            </div>
            <div className="statisticsDiv">
              <div className="user-table">
                <Users
                  getRef={getRef}
                  gridRef={gridRef}
                  roleRowData={roleRowData}
                  userRowData={userRowData}
                  gridComponentRef={gridComponentRef}
                  roleSelected={roleSelected}
                  setEmailIdselected={setEmailIdselected}
                  afterStatusUpdate={afterStatusUpdate}
                  updateAndReplaceUser={updateAndReplaceUser}
                  setSelectDropdownOpenedData={setSelectDropdownOpenedData}
                  selectDropdownOpenedData={selectDropdownOpenedData}
                  handleChangeRolesSelection={handleChangeRolesSelection}
                  setSelectedRoles={setSelectedRoles}
                  selectedRoles={selectedRoles}
                  roleAssigningPermissions={roleAssigningPermissions}
                  isSystemAdminLogin={isSystemAdminLogin}
                />
              </div>
            </div>
          </SplitPaneVertical>
        </div>
      </div>
    </div>
  );
};
export default RolePermissionMain;
