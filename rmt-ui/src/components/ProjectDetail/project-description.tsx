import { Box, Button, IconButton, TextField, Typography } from "@mui/material";
import React, { useContext, useState, useEffect } from "react";
import EditIcon from "@mui/icons-material/Edit";
import SaveIcon from "@mui/icons-material/Save";
import "./style.css";
import { ProjectUpdateDetailsContext } from "../../contexts/projectDetailsContext";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import {
  IsPermissionExistForProject,
  IsProjectInActiveOrClosed,
} from "../../global/utils";
import { MODULE_NAME_ENUM } from "../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../common/access-control-guard/access-control";

const ProjectDescription = (props: any) => {
  const { content } = props;
  const [isEditing, setIsEditing] = useState(false);
  const isEmployee = React.useContext(UserDetailsContext)?.isEmployee;
  const [projectDescription, setProjectDescription] = useState(content);
  const [currentProjectDescription, setCurrentProjectDescription] =
    useState<string>();
  const userContext = React.useContext(UserDetailsContext);
  const { setProjectUpdateDescription } = useContext(
    ProjectUpdateDetailsContext
  );
  const [preProjectDescription, setPreProjectDescription] = useState(content);
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);

  const handleEditClick = () => {
    setPreProjectDescription(projectDescription);
    setIsEditing(true);
  };

  const handleTextChange = (event: any) => {
    setCurrentProjectDescription(event.target.value);
  };
  const handleSaveClick = () => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    setProjectUpdateDescription(currentProjectDescription);
    setProjectDescription(currentProjectDescription);
    // setCurrentProjectDescription(projectDescription);
    setIsEditing(false);
  };
  useEffect(() => {
    setProjectDescription(content);
    setCurrentProjectDescription(content);
  }, [content]);

  const handleCancelClick = () => {
    setIsEditing(false);
    setCurrentProjectDescription(projectDescription);
  };

  return (
    <div
    // style={{ marginLeft: "-8px" }}
    >
      <Box component="div" className="descriptionHeader">
        Description
        {!isEmployee &&
          IsPermissionExistForProject(
            userContext.projectPermissionData?.permissions,
            MODULE_NAME_ENUM.Project_Details,
            PERMISSION_TYPE.Update,
            userContext.role
          ) && (
            <IconButton
              onClick={handleEditClick}
              disabled={IsProjectInActiveOrClosed(props.projectDetails)}
              className="editButton"
            >
              <EditIcon />
            </IconButton>
          )}
      </Box>
      <div>
        {isEditing ? (
          <div className="desc-edit-container">
            <TextField
              fullWidth
              multiline
              value={currentProjectDescription}
              onChange={handleTextChange}
            />
            <Button
              className="btnStyle rmt-action-button"
              onClick={handleSaveClick}
              startIcon={<SaveIcon />}
            >
              Save
            </Button>
            <Button className="cancelbtnStyle" onClick={handleCancelClick}>
              Cancel
            </Button>
          </div>
        ) : (
          <div>
            <span>{currentProjectDescription}</span>
          </div>
        )}
      </div>
    </div>
  );
};

export default ProjectDescription;
