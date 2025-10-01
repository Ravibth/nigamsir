import React, { useContext, useMemo, useState } from "react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import AgGridComponent from "../../aggrid-component/aggrid-component";
import {
  Box,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  ListItemText,
  MenuItem,
  Modal,
  Select,
  styled,
  Switch,
  SwitchProps,
  Tooltip,
} from "@mui/material";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import ApplyConfirmationModal from "../role-permission-modal/confirm-status-change-modal";
import { ApplyConfirmationModalSXProps } from "../constant";
import { RolesListMaster } from "../../../common/enums/ERoles";
import { DoesUserHaveAnyFutureOrOngoingAllocations } from "../../../services/user/user.service";
import {
  SnackbarContextProps,
  SnackbarContext,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import {
  LoaderContextProps,
  LoaderContext,
} from "../../../contexts/loaderContext";
import { routeToEmployeeProfile } from "../../../global/utils";

const Users = (props: any) => {
  const userDetailsContext: any = useContext(UserDetailsContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [isUserDeactivated, setisUserDeactivated] = useState(false);
  const [deactivatedUserEmail, setDeactivatedUserEmail] = useState("");
  const [open, setOpen] = useState(false);
  const handleClose = () => setOpen(false);

  const changeStatusOfUser = async (params: any) => {
    const isActive = params.value === "Active" || params.value === true;
    if (isActive) {
      loaderContext.open(true);
      const canUserBeDisabled = await Promise.all([
        checkIfUserCanBeDisabled(params.data.email_id),
      ]);
      loaderContext.open(false);
      if (canUserBeDisabled[0]) {
        setisUserDeactivated(true);
      } else {
        snackbarContext.displaySnackbar(
          "There are conflicting allocations, please release the allocation for the user prior to deactivating the employee.",
          SnackbarSeverity.ERROR
        );
        return;
      }
    } else {
      setisUserDeactivated(false);
    }
    setDeactivatedUserEmail(params.data.email_id);
    props.setEmailIdselected(params.data.email_id);
    setOpen(true);
  };

  const checkIfUserCanBeDisabled = (email: string): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      DoesUserHaveAnyFutureOrOngoingAllocations([email])
        .then((resp) => {
          if (resp.some((m) => m.toLowerCase() === email.toLowerCase())) {
            resolve(false);
          } else {
            resolve(true);
          }
        })
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error checking user's validity for disabling",
            SnackbarSeverity.ERROR
          );
          resolve(false);
        });
    });
  };

  const getTfDisabledPermissions = (role: string, checked: boolean) => {
    const selectedRolePermission = props.roleAssigningPermissions.find(
      (item: any) => item.Name === role
    )?.Permissions;
    if (selectedRolePermission && selectedRolePermission.length === 0) {
      return true;
    } else if (selectedRolePermission && selectedRolePermission.length === 2) {
      return false;
    } else if (selectedRolePermission && selectedRolePermission.length === 1) {
      if (
        (checked && selectedRolePermission.includes("Assign")) ||
        (!checked && selectedRolePermission.includes("Unassign"))
      ) {
        return true;
      }
      return false;
    }
    return false;
  };

  const validateRole = (
    roleList: any,
    toBeValidate: string,
    checkSuperAdmin = false
  ): boolean => {
    if (!checkSuperAdmin) {
      return roleList.some(
        (item: any) => item.role.toLowerCase() === toBeValidate.toLowerCase()
      );
    } else {
      const isSystemAdmin = userDetailsContext.role.some(
        (item: any) =>
          item?.role?.toLowerCase() ===
          RolesListMaster.SystemAdmin.toLowerCase().trim() //SystemAdmin Changes system admin
      );
      if (isSystemAdmin) return !isSystemAdmin;
      const Role_List = roleList?.some(
        (item: any) => item.role?.toLowerCase() === toBeValidate?.toLowerCase()
      );
      return Role_List;
    }
  };
  const MenuProps: any = {
    classes: { paper: "user-table-roles-dropdown" },
    PaperProps: {
      style: {
        width: 300,
      },
    },
    anchorOrigin: {
      vertical: "bottom",
      horizontal: "left",
    },
    transformOrigin: {
      vertical: "top",
      horizontal: "left",
    },
    getContentAnchorEl: null,
  };

  const RenderRolesDisplay = (type: string) => {
    switch (type) {
      case RolesListMaster.AdditionalDelegate:
        return "Additional Delegate";
      case RolesListMaster.Delegate:
        return "Delegate";
      case RolesListMaster.AdditionalEl:
        return "Additional EL";
      case RolesListMaster.Admin:
        return "Admin";
      case RolesListMaster.CEOCOO:
        return "CEO-COO";
      case RolesListMaster.Employee:
        return "Employee";
      case RolesListMaster.Reviewer:
        return "Reviewer";
      case RolesListMaster.Leaders:
        return "Leader";
      case RolesListMaster.SystemAdmin: //SystemAdmin Changes SystemAdmin
        return "System Admin";
      case RolesListMaster.ResourceRequestor:
        return "Resource Requestor";
      case RolesListMaster.SuperCoach:
        return "Super Coach";
      default:
        return "";
    }
  };

  const IOSSwitch = styled((props: SwitchProps) => (
    <Switch
      focusVisibleClassName=".Mui-focusVisible"
      disableRipple
      {...props}
    />
  ))(({ theme }) => ({
    width: 42,
    height: 26,
    padding: 0,
    "& .MuiSwitch-switchBase": {
      padding: 0,
      margin: 2,
      transitionDuration: "300ms",
      "&.Mui-checked": {
        transform: "translateX(16px)",
        color: "#fff",
        "& + .MuiSwitch-track": {
          backgroundColor:
            theme.palette.mode === "dark" ? "#2ECA45" : "#00a7b5 ",
          opacity: 1,
          border: 0,
        },
        "&.Mui-disabled + .MuiSwitch-track": {
          opacity: 0.5,
        },
      },
      "&.Mui-focusVisible .MuiSwitch-thumb": {
        color: "#33cf4d",
        border: "6px solid #fff",
      },
      "&.Mui-disabled .MuiSwitch-thumb": {
        color:
          theme.palette.mode === "light"
            ? theme.palette.grey[100]
            : theme.palette.grey[600],
      },
      "&.Mui-disabled + .MuiSwitch-track": {
        opacity: theme.palette.mode === "light" ? 0.5 : 0.3,
      },
    },
    "& .MuiSwitch-thumb": {
      boxSizing: "border-box",
      width: 22,
      height: 22,
    },
    "& .MuiSwitch-track": {
      borderRadius: 26 / 2,
      backgroundColor: theme.palette.mode === "light" ? "#787878" : "#39393D",
      opacity: 1,
      transition: theme.transitions.create(["background-color"], {
        duration: 500,
      }),
    },
  }));

  const columnDefs: any = useMemo(
    () => [
      {
        headerName: "Name",
        field: "name",
        flex: 1.1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "name",
        cellRenderer: (params) => {
        return (
          <span
            style={{ cursor: "pointer", color: "blue", textDecoration: "underline" }}
            title={params.value}
            onClick={() => {
              routeToEmployeeProfile(`/employee-profile/${params.data?.email_id}`);
            }}
          >
            {params.value}
          </span>
        );
      },
      },
      {
        headerName: "Email",
        field: "uemail_id",
        flex: 1.3,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        tooltipField: "uemail_id",
        menuTabs: ["filterMenuTab"],
      },
      {
        headerName: "Designation",
        field: "designation",
        flex: 1,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipField: "designation",
      },
      {
        headerName: "Roles",
        field: "roles",
        flex: 1.3,
        filter: "agTextColumnFilter",
        filterParams: {
          suppressAndOrCondition: true,
        },
        sortable: true,
        unSortIcon: true,
        menuTabs: ["filterMenuTab"],
        tooltipValueGetter: (par) => {
          const valueFOrmatted = par?.data?.roles
            ?.split(",")
            .map((item) => RenderRolesDisplay(item))
            .join(",");
          return valueFOrmatted;
        },
        valueGetter: (par) => {
          const valueFOrmatted = par?.data?.roles
            ?.split(",")
            .map((item) => RenderRolesDisplay(item))
            .join(",");
          return valueFOrmatted;
        },
        cellRenderer: function (params: any) {
          return (
            <div>
              <FormControl sx={{ m: 1, width: 300 }}>
                <Select
                  sx={{
                    fontSize: "14px",
                  }}
                  labelId="demo-multiple-name-label"
                  id="demo-multiple-name"
                  multiple
                  value={params?.data?.role_list.map(
                    (roles: any) => roles.role
                  )}
                  renderValue={(selected) =>
                    selected.map((item) => RenderRolesDisplay(item)).join(",")
                  }
                  onChange={(event: any) =>
                    props.handleChangeRolesSelection(event, params)
                  }
                  onOpen={(e) => {
                    props.setSelectDropdownOpenedData({
                      email_id: params.data?.email_id,
                      opened: true,
                    });
                  }}
                  open={
                    props.selectDropdownOpenedData &&
                    props.selectDropdownOpenedData.opened === true &&
                    props.selectDropdownOpenedData.email_id ===
                      params.data.email_id
                  }
                  onClose={(e: any) => {
                    props.setSelectDropdownOpenedData({
                      email_id: "",
                      opened: false,
                    });
                  }}
                  MenuProps={MenuProps}
                  disabled={
                    params.data?.status === "Inactive" ||
                    params.data?.status === false ||
                    userDetailsContext?.username.toLowerCase().trim() ===
                      params.data?.email_id.toLowerCase().trim() ||
                    (!props.isSystemAdminLogin &&
                      validateRole(params.data?.role_list, "admin", true))
                  }
                >
                  {props.roleRowData.map((roles: any) => {
                    return (
                      <MenuItem
                        key={roles.role_name}
                        value={roles.role_name}
                        disabled={getTfDisabledPermissions(
                          roles?.role_name,
                          params.data?.role_list
                            .map((roles: any) => roles.role)
                            .indexOf(roles.role_name) > -1
                        )}
                      >
                        <Checkbox
                          defaultChecked={
                            params.data?.role_list
                              .map((roles: any) => roles.role)
                              .indexOf(roles.role_name) > -1
                          }
                          disabled={getTfDisabledPermissions(
                            roles?.role_name,
                            params?.data?.role_list
                              .map((roles: any) => roles.role)
                              .indexOf(roles.role_name) > -1
                          )}
                        />
                        <ListItemText primary={roles.display} />
                      </MenuItem>
                    );
                  })}
                </Select>
              </FormControl>
            </div>
          );
        },
      },
      {
        headerName: "Active",
        field: "status",
        filter: true,
        sortable: true,
        sort: "asc",
        menuTabs: ["filterMenuTab"],
        filterParams: {
          suppressAndOrCondition: true,
          refreshValuesOnOpen: true,
        },
        unSortIcon: true,
        flex: 0.5,
        valueGetter: (params: any) => {
          return params.data?.status ? "Active" : "Inactive";
        },
        cellRenderer: function (params: any) {
          return (
            <FormGroup sx={{ marginTop: "6px", marginLeft: "15px" }}>
              <FormControlLabel
                disabled={
                  userDetailsContext?.username.toLowerCase().trim() ===
                    params.data?.email_id.toLowerCase().trim() ||
                  (!props.isSystemAdminLogin &&
                    validateRole(params.data?.role_list, "admin", true))
                }
                control={
                  <Tooltip
                    title={
                      params.value === "Active" || params.value === true
                        ? "Deactivate"
                        : "Activate"
                    }
                    placement="right"
                  >
                    <IOSSwitch
                      checked={params.value === "Active"}
                      onChange={() => changeStatusOfUser(params)}
                    />
                  </Tooltip>
                }
                label=""
              />
            </FormGroup>
          );
        },
      },
    ],
    [
      props.roleRowData,
      props.userRowData,
      props.selectDropdownOpenedData,
      userDetailsContext,
      props.isSystemAdmin,
    ]
  );

  const defaultColDef = {
    lockVisible: true,
    cellStyle: function (params: any) {
      if (params.data?.status === "Inactive" || params.data?.status === false) {
        return { fontStyle: "italic", color: "gray" };
      }
      if (
        userDetailsContext?.username.toLowerCase().trim() ===
        params?.data?.email_id.toLowerCase().trim()
      ) {
        return { color: "#9c9c9c", backgroundColor: "#E5E4E2" };
      }
      if (
        !props.isSystemAdminLogin &&
        validateRole(params.data?.role_list, "admin", true)
      ) {
        return { color: "#9c9c9c" };
      }
    },
  };

  const gridOptions = {
    suppressCellSelection: true,
  };

  const onGridReady = (params: any) => {
    params.columnApi.autoSizeAllColumns();
    const gridColumnApi = params.columnApi;
    const allColumnIds: string[] = [];
    if (gridColumnApi !== undefined) {
      gridColumnApi.getColumns().forEach((column: any) => {
        allColumnIds.push(column.getId());
      });
      gridColumnApi.autoSizeColumns(allColumnIds);
      gridColumnApi.sizeColumnsToFit();
    }
  };

  return (
    <>
      <div>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={ApplyConfirmationModalSXProps}>
            <ApplyConfirmationModal
              isUserDeactivated={isUserDeactivated}
              afterStatusUpdate={props.afterStatusUpdate}
              handleClose={handleClose}
              deactivatedUserEmail={deactivatedUserEmail}
            />
          </Box>
        </Modal>
      </div>
      <div className="ag-theme-alpine user-ag-grid" style={{ height: "100%" }}>
        <AgGridComponent
          ref={props.gridRef}
          rowData={props.userRowData}
          columnDefs={columnDefs}
          defaultColDef={defaultColDef}
          gridComponentRef={props.gridComponentRef}
          tooltipShowDelay={0}
          tooltipHideDelay={2000}
          isPageination={true}
          pageSize={18}
          suppressCsvExport={true}
          suppressContextMenu={true}
          suppressExcelExport={true}
          isFilterVisible={true}
          hideExport={true}
          gridOptions={gridOptions}
          suppressCellFocus={true}
          onGridReady={onGridReady}
        ></AgGridComponent>
      </div>
    </>
  );
};

export default Users;
