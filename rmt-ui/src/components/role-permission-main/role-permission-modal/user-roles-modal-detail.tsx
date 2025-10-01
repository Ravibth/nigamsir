import {
  Box,
  Button,
  Card,
  CardContent,
  Checkbox,
  InputAdornment,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import validator from "validator";
import SearchSharpIcon from "@mui/icons-material/SearchSharp";
import _ from "lodash";
import useBlockRefreshAndBack from "../../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import { getEmailId } from "../../../global/utils";

const UserRolesModalDetail = (props: any) => {
  useBlockRefreshAndBack(props.isDirty);
  const validateEmail = (e: any) => {
    const emailEntered = e.target.value;
    if (validator.isEmail(emailEntered)) {
      props.setEmailError(false);
    } else {
      props.setEmailSearchErrorMessage("Please enter a valid email id");
      props.setEmailError(true);
    }
    if (emailEntered === "") {
      props.setEmailError(false);
    }
    props.setEmail(emailEntered);
  };

  const submitEmail = async () => {
    if (validator.isEmail(props.email) && props.email !== "") {
      props.setEmailError(false);
      await props.emailValidateSearch(props.email);
      props.setEmail("");
    } else {
      props.setEmailError(true);
    }
  };

  const submitUserRoles = async () => {
    const payload = {
      email_id: props.userdetails.email_id,
      roles: _.uniq(
        props.showUserRoles
          .filter((user: any) => user.isAssigned)
          .map((user: any) => user.role_name)
      ),
      name: props.userdetails.name,
      status: props.userdetails.status,
    };
    props.checkandAssignNewEntry(payload);
    props.handleClose();
    props.setUserDetails({});
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

  return (
    <div>
      <Typography
        className="assign-modal-header header-style"
        id="modal-modal-title"
        variant="h6"
        component="h1"
      >
        Assign Role
        <span>
          <Tooltip title={"Close"}>
            <CloseIcon
              onClick={() => {
                props.setUserDetails({});
                props.setdisplayUserNotFoundOnSearch(false);
                props.handleClose();
              }}
            />
          </Tooltip>
        </span>
      </Typography>
      <Typography component={"div"} id="modal-modal-description" sx={{ mt: 2 }}>
        <div className="col-7 assign-user-input-field">
          <form
            onSubmit={(e) => {
              e.preventDefault();
              submitEmail();
            }}
          >
            <TextField
              className="font-style"
              error={props.emailError}
              value={props.email}
              required
              autoComplete="off"
              id="outlined-error-helper-text"
              label="Search user with email id"
              onChange={(e) => {
                validateEmail(e);
              }}
              InputProps={{
                endAdornment: (
                  <InputAdornment className="search-icon" position="end">
                    <Tooltip title="Search">
                      <button className="search-icon-btn" type="submit">
                        <SearchSharpIcon />
                      </button>
                    </Tooltip>
                  </InputAdornment>
                ),
              }}
            />
          </form>
        </div>
        <div className="row">
          <span className="user-not-found-error">
            {(props.displayUserNotFoundOnSearch ||
              (props.userdetails?.is_existing == false &&
                !(props.userdetails?.email_id?.length > 0))) && (
              <span style={{ paddingLeft: "2px" }}>
                We couldn’t find an employee with that username.
              </span>
            )}
          </span>
        </div>
      </Typography>
      <div className="two-tables-display">
        {props.isuserEmailFound && props.userdetails?.email_id?.length > 0 && (
          <>
            <span className="user-details-table">
              <Box className="user-box-style">
                <Card variant="outlined" className="user-details-table-card">
                  <CardContent>
                    <Typography component="div" className="header-name">
                      <div className="row ">
                        <div className="col-4">User Details</div>
                      </div>
                    </Typography>
                    <Typography component="div" className="padding-18">
                      <div className="row user-box-item">
                        <div className="col-3 cell-align">Email ID:</div>
                        <Tooltip title={getEmailId(props.userdetails.email_id)}>
                          <div className="col-9 email-id-underline email-id-overflow">
                            {getEmailId(props.userdetails.email_id)}
                          </div>
                        </Tooltip>
                      </div>
                    </Typography>
                    <Typography component="div" className="padding-18">
                      <div className="row user-box-item">
                        <div className="col-3 cell-align">Name:</div>
                        <div className="col-9">{props.userdetails.name}</div>
                      </div>
                    </Typography>
                    <Typography component="div" className="padding-18">
                      <div className="row user-box-item">
                        <div className="col-3 cell-align">Designation:</div>
                        <div className="col-9">
                          {props.userdetails.designation}
                        </div>
                      </div>
                    </Typography>
                    <Typography
                      component="div"
                      // gutterBottom
                      className="padding-18"
                    >
                      <div className="row help-text">
                        <span>HELP</span>
                        <ul>
                          <li>
                            The indicated disabled roles are not valid to be
                            assigned to the selected user.
                          </li>
                        </ul>
                      </div>
                    </Typography>
                  </CardContent>
                </Card>
              </Box>
            </span>
            <span className="assign-roles-table">
              <Box className="role-box-style">
                <Card
                  variant="outlined"
                  className={
                    props.roleRowData.length > 7
                      ? "assign-roles-table-card overflow-card"
                      : "assign-roles-table-card"
                  }
                >
                  <CardContent>
                    <Typography component="div" className="header-name">
                      <div className="row">
                        <div className="col-7">Roles Assignment</div>
                      </div>
                    </Typography>
                    <>
                      {props.roleRowData.map((row: any) => {
                        return (
                          <Typography component="div" key={row.display}>
                            <div className="roles-check-main ">
                              <div className="col-3">
                                <Checkbox
                                  className="checkbox-asssign-role-table purple"
                                  defaultChecked={props.userdetails.role_list.includes(
                                    row.role_name
                                  )}
                                  disabled={getTfDisabledPermissions(
                                    row.role_name,
                                    props.userdetails.role_list.includes(
                                      row.role_name
                                    )
                                  )}
                                  onChange={(e) => {
                                    props.setIsDataCheckedDirty(true);
                                    props.updateRolesOnClick(
                                      row,
                                      props.userdetails.role_list
                                    );
                                  }}
                                />
                              </div>
                              <div>{row.display}</div>
                            </div>
                          </Typography>
                        );
                      })}
                    </>
                  </CardContent>
                </Card>
              </Box>
            </span>
          </>
        )}
      </div>

      <div className="row button-on-add-update-user-popup">
        <div className="col-md-3">
          <div className="submitButton submit-button-on-add-update-user-popup">
            {props.isuserEmailFound &&
              props.userdetails?.email_id?.length > 0 && (
                <Button className="rmt-action-button" onClick={submitUserRoles}>
                  Save
                </Button>
              )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserRolesModalDetail;
