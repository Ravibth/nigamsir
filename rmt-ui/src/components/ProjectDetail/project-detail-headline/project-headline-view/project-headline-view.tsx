import { Grid, IconButton, Switch, TextField, Tooltip } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import CloseIcon from "@mui/icons-material/Close";
import { ProjectDetailsHeadline } from "../Constant";
import { useState } from "react";
import ControllerCalendar from "../../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import { IsPermissionExistForProject } from "../../../../global/utils";
import { PERMISSION_TYPE } from "../../../../common/access-control-guard/access-control";
import { MODULE_NAME_ENUM } from "../../../../common/module-permission/module-permission";
import React from "react";
import { UserDetailsContext } from "../../../../contexts/userDetailsContext";
import SaveIcon from "@mui/icons-material/Save";
import moment from "moment";
import BriefCase from "../../../../common/images/briefcase.png";
import Confidential from "../../../../common/images/Confidential.png";
import Calendar from "../../../../common/images/calendar.png";
import Map from "../../../../common/images/map pin.png";
import Peoples from "../../../../common/images/users.png";
import CurrencyRupeeOutlinedIcon from "@mui/icons-material/CurrencyRupeeOutlined";
import { ProjectUpdateDetailsContext } from "../../../../contexts/projectDetailsContext";
import ControllerSwitch from "../../../controllers/controller-switch";

const ProjectHeadlineView = (props: any) => {
  const {
    title,
    value,
    index,
    onEdit,
    setProjectUpdateEndDate,
    setProjectUpdateIsEndDateChanged,
    headlineData,
  } = props;
  const [editMode, setEditMode] = useState(false);
  const [editedValue, setEditedValue] = useState(value);
  const [datePreviousValue, setDatePreviousValue] = useState(value);
  const userContext = React.useContext(UserDetailsContext);
  const {
    control,
    watch,
    setValue,
    register,
    handleSubmit,
    trigger,
    getValues,
    formState: { errors, isDirty },
  } = useForm({ mode: "onTouched" });
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);

  const handleEditClick = () => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    setEditMode(true);
    setEditedValue(moment(editedValue).toDate());
    setDatePreviousValue(moment(editedValue).toDate());
    onEdit(index);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEditedValue(e);
    projectDetailsContext.setIsSelectedDateInvalid(false);
  };

   const isConfidentialChange = (e) => {
    setEditedValue(e);
    projectDetailsContext.setConfidential(e);
  };

  const getMinAllowedDate = () => {
    const minDate =
      currentDate > moment(headlineData["startDate"]).startOf("day").toDate()
        ? currentDate
        : moment(headlineData["startDate"]).startOf("day").toDate();
    return minDate;
  };

  const handleSaveClick = (_title: string) => {
    const minDate = getMinAllowedDate();
    if (title == ProjectDetailsHeadline.END_DATE && editedValue >= minDate) {
      setEditMode(false);
      onEdit(index, editedValue);
      const finalDate = moment(editedValue).format("YYYY-MM-DD");
      setProjectUpdateIsEndDateChanged(true);
      setProjectUpdateEndDate(finalDate);
      setDatePreviousValue(moment(editedValue).toDate());
      projectDetailsContext.setIsProjectDetailsDirty(true);
    } 
    else if (title === ProjectDetailsHeadline.CONFIDENTIAL) {
     setEditMode(false);
     onEdit(index, editedValue);
     projectDetailsContext.setIsProjectDetailsDirty(true);
    }
    else {
      console.log("Invalid Date");
    }
  };

  const handleCancelClick = () => {
    setEditMode(false);
    setEditedValue(datePreviousValue);
  };

  const currentDate = moment().startOf("day").toDate();

  const iconMap: { [key: string]: React.ReactNode } = {
    [ProjectDetailsHeadline.START_DATE]: (
      <img
        src={Calendar}
        alt="Calendar"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.END_DATE]: (
      <img
        src={Calendar}
        alt="Calendar"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.ALLOCATION_STATUS]: (
      <img
        src={BriefCase}
        alt="briefcase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.ASSIGNMENT_INCHARGE]: (
      <img
        src={BriefCase}
        alt="briefcase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.BUDGET]: (
      <img
        src={BriefCase}
        alt="briefcase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.BUSINESS_UNITS]: (
      <img
        src={BriefCase}
        alt="briefcase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.CLIENT_GROUP]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.CLIENT_NAME]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.CSL]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.CSP]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.DELIVERY_LOCATION]: (
      <img src={Map} alt="Map" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.EL]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.EO]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.OFFERINGS]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.SOLUTIONS]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.FINDING_PARTNER_1]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.FINDING_PARTNER_2]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.GT_REFFERENCE_COUNTRY]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.INDUSTRY]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.JOB_ID]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.JOB_LOCATION]: (
      <img src={Map} alt="Map" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.JOB_MANAGER]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.LEAD_GENERATOR]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.SMEG_LEADER]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.SUB_INDUSTRY]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.LEGAL_ENTITY]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.PIPELINE_CODE]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PIPELINE_ID]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PROJECT_ACTIVATION_AND_CLOSURE_STATUS]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PROJECT_CATEGORY]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PROJECT_STATUS]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PROJECT_TYPE]: (
      <img
        src={BriefCase}
        alt="BriefCase"
        className="project-header-icons-style"
      />
    ),
    [ProjectDetailsHeadline.PROPOSED_CSP]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.PROPOSED_EL]: (
      <img src={Peoples} alt="Peoples" className="project-header-icons-style" />
    ),
    [ProjectDetailsHeadline.CONFIDENTIAL]: (
      <img
        src={Confidential}
        alt="Confidential"
        className="project-header-icons-style"
      />
    ),
  };
  return (
    <Grid
      item
      mt={1}
      mb={1}
      xs={4}
      key={index}
      className="main-headers-container"
    >
    <Grid xs={4} className="projectDetailsHeadlineTitle">
      {iconMap[title]}
      {title + (title?.length > 0 ? " : " : "")}

      {([ProjectDetailsHeadline.END_DATE, ProjectDetailsHeadline.CONFIDENTIAL].includes(title)) &&
        IsPermissionExistForProject(
          userContext.projectPermissionData?.permissions,
          MODULE_NAME_ENUM.Project_Details,
          PERMISSION_TYPE.Update,
          userContext.role
        ) &&
        props?.currentTabValue == "0" && (
          <IconButton
            className="editIcon-btn"
            onClick={editMode ? () => {} : handleEditClick}
          >
            {editMode ? (
              <>
                <Tooltip title="Save" placement="top">
                  <SaveIcon onClick={(e)=>handleSaveClick(title)} />
                </Tooltip>
                <Tooltip title="Close Edit Mode" placement="top">
                  <CloseIcon onClick={handleCancelClick} />
                </Tooltip>
              </>
            ) : (
              <Tooltip
                title={`Edit ${title === ProjectDetailsHeadline.END_DATE ? "Date" : "Confidential"}`}
                placement="top"
              >
                {title === ProjectDetailsHeadline.END_DATE &&
                projectDetailsContext.isSelectedDateInvalid ? (
                  <EditIcon sx={{ border: 2, borderColor: "rgb(211, 47, 47)" }} />
                ) : (
                  <EditIcon />
                )}
              </Tooltip>
            )}
          </IconButton>
        )}
    </Grid>

      {editMode ? (
          title === ProjectDetailsHeadline.END_DATE ? (
            <div className="end-date-controller">
              <ControllerCalendar
                name="endDate"
                control={control}
                defaultValue={value ? new Date(value) : new Date(currentDate)}
                minDate={getMinAllowedDate()}
                maxDate={new Date(props?.projectDetails?.endDate)}
                onChange={handleInputChange}
                value={editedValue}
                isSelectedDateInvalid={projectDetailsContext.isSelectedDateInvalid}
              />
            </div>
          ) : title === ProjectDetailsHeadline.CONFIDENTIAL ? (
            <div className="end-date-controller">
                <ControllerSwitch
                name="isConfidential"
                control={control}
                defaultValue={value}
                color="primary"
                onChange={(e)=>{isConfidentialChange(e)}}
              />
            </div>
          ) : null
        ) : (
          <div className="projectDetailsHeadlineData">
            {title === ProjectDetailsHeadline.END_DATE
              ? datePreviousValue != null
                ? moment(datePreviousValue).format("DD-MM-YYYY")
                : ""
              : title === ProjectDetailsHeadline.CONFIDENTIAL
              ? editedValue
                ? "Yes"
                : "No"
              : datePreviousValue}
          </div>
        )}
    </Grid>
  );
};
export default ProjectHeadlineView;
