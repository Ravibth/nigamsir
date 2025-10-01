import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import {
  Box,
  Button,
  Checkbox,
  Grid,
  Modal,
  Tooltip,
  Typography,
} from "@mui/material";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import * as constant from "./Constant";
import React, { useContext, useEffect, useState } from "react";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import * as Utils from "./util";
import { ProjectUpdateDetailsContext } from "../../../contexts/projectDetailsContext";
import {
  SnackbarContext,
  SnackbarContextProps,
} from "../../../contexts/snackbarContext";
import ActionButton from "../../actionButton/actionButton";
import { IsReqistionExistsInProject } from "../../../services/allocation/getAllRequisitionByProjectCode";
import ConfirmationDialog from "../../../common/confirmation-dialog/confirmation-dialog";
import {
  getActiveFieldForMarketPlace,
  setIsRequisitionCreationAllowed,
} from "../../../services/project-list-services/project-list-services";
import {
  IsPermissionExistForProject,
  IsProjectInActiveOrClosed,
  routeValueEncode,
} from "../../../global/utils";
import moment from "moment";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import { addProjectToMarketPlace } from "../../../services/marketPlace/addProjectToMarketPlace";
import { createOrUpdatePublishedFieldForMarketPlace } from "../../../services/project-published-for-marketPlace/createOrUpdatePublishedFieldForMarketPlace";
import { getPublishedFieldByprojectCodeForMarketPlace } from "../../../services/project-published-for-marketPlace/getPublishedFieldByprojectCodeForMarketPlace";
import CheckBoxComponent from "../check-box-component/checkBoxComponent";
import { updatePublishedToMarketPlace } from "../../../services/project-published-for-marketPlace/update-published-to-marketPlace-field";
import "./style.css";
import BackActionButton from "../../actionButton/backactionButton";
import { MODULE_NAME_ENUM } from "../../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../../common/access-control-guard/access-control";
import { FontWeight600 } from "../../common-allocation/common-allocation-modal-form/style";
import { RolesListMaster } from "../../../common/enums/ERoles";
import { MoveMarketPlaceHeader } from "./Constant";
import NavigateActionButton from "../../actionButton/navigateActionButton";
import { EPipelineStatus } from "../../../common/enums/EProject";
import DialogBox from "../../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockerCustom from "../../../hooks/UnsavedChangesHook/useBlockerCustom";
import useBlockRefreshAndBack from "../../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import InfoIcon from "@mui/icons-material/Info";
import { ProjectChargeableType } from "../../project-types/constant";
import {
  IAddProjectToMarketPlace,
  ISetPublishedToMarketPlace,
} from "../../Marketplace/interfaces/marketplaceinterfaces";
import { GetApplicationLevelSettings } from "../../../services/configuration-services/configuration.service";
import { CONFIDENTIAL_APPLICATION_SETTING_KEY_FIELD } from "../../../global/constant";

const label = { inputProps: { "aria-label": "Checkbox demo" } };

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "50%",
  height: "auto",
  bgcolor: "background.paper",
  padding: "30px",
  borderRadius: "15px",
};

const MainTitle = (props: any) => {
  const {
    currentTabValue,
    projectDetails,
    setProjectDetails,
    getProjectDetails,
  } = props;

  const {
    control,
    formState: { isDirty },
  } = useForm({ mode: "onTouched" });

  const [isMoveToMarketSectionVisible, setIsMoveToMarketSectionVisible] =
    useState(false);
  const [isMoveToMarketPlaceButtonEnable, setIsMoveToMarketPlaceButtonEnable] =
    useState(false);
  const [isRequisitionCreationallowed, setIsRequisitionCreationallowed] =
    useState(false);
  const [isConfirmationPopOpen, setIsConfirmationPopOpen] = useState(false);
  const [activeFieldForMarketPlace, setActiveFieldForMarketPlace] = useState(
    [] as any
  );
  const [activeFieldMap1, setActiveFieldMap1] = useState({} as any);

  const [marketPlaceExpirationDate, setMarketPlaceExpirationDate] =
    useState(null);
  const [publishedFieldForMarketPlace, setPublishedFieldForMarketPlace] =
    useState([] as any);
  const [
    disableCheckBoxClosedRequisition,
    setDisableCheckBoxClosedRequisition,
  ] = useState(false);
  const [mpSubmitConfirmationBox, setMpSubmitConfirmationBox] = useState(false);
  const [warningPop, setWarningPop] = useState(false);
  const [isUpdateProjectDisabled, setIsUpdateProjectDisabled] = useState(null);
  const isSuspended =
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Suspended ||
    props?.projectDetails?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const userContext = React.useContext(UserDetailsContext);
  const isEmployee = userContext?.isEmployee;
  const updateDetailsContetext = useContext(ProjectUpdateDetailsContext);
  const { setProjectUpdateEL } = useContext(ProjectUpdateDetailsContext);
  const { setProjectUpdateDelegate } = useContext(ProjectUpdateDetailsContext);
  const { setProjectUpdateSkills } = useContext(ProjectUpdateDetailsContext);
  const { setProjectUpdateDemands } = useContext(ProjectUpdateDetailsContext);
  const [searchParams, setSearchParams] = useSearchParams();
  const tab = searchParams.get("tab");
  const [openModal, setOpenModal] = useState(false);
  const projectDetailsContext = React.useContext(ProjectUpdateDetailsContext);
  const qsTab = tab != undefined && tab != "0" ? parseInt(tab) : 0;
  const [rollOverState, setRollOverState] = useState({
    isRollOverApplicable: projectDetails.isRollover,
    showRollOverButton:
      currentTabValue == 0 && qsTab == 0 ? projectDetails.isRollover : false,

    rollOverPipelineCode: projectDetails.pipelineCode,
  });
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const [confidentialFields, setConfidentialFields] = useState([] as any);
  const navigate = useNavigate();
  const { pipelineCode, jobCode } = useParams();
  const handleCalendarClick = () => {
    navigate(
      `/calendar/${routeValueEncode(pipelineCode)}/${routeValueEncode(jobCode)}`
    );
  };

  useEffect(() => {
    setRollOverState({
      ...rollOverState,
      isRollOverApplicable: props.projectDetails.isRollover,
      showRollOverButton:
        currentTabValue == 0 && qsTab == 0 ? projectDetails.isRollover : false,
    });
  }, [props.projectDetails, currentTabValue]);

  useEffect(() => {
    isMoveToMarkePlace();
  }, [
    projectDetails,
    projectDetails.isPublishedToMarketPlace,
    projectDetails.endDate,
    projectDetails.isRequisitionCreationallowed,
    props?.refresh,
  ]);

  useEffect(() => {
    //show marketplace section
    setIsMoveToMarketSectionVisible(true);
  }, [
    isMoveToMarketPlaceButtonEnable,
    // isRequisitionCreationallowed,
    disableCheckBoxClosedRequisition,
  ]);

  const isMoveToMarkePlace = async () => {
    var allReq: any = await IsReqistionExistsInProject(pipelineCode, jobCode);
    let flag: boolean = await IsMoveToMarketPlaceButtonDisabled(allReq);
    setIsMoveToMarketPlaceButtonEnable(flag);
    setDisableCheckBoxClosedRequisition(
      (flag && projectDetails.isRequisitionCreationallowed) ||
        projectDetails.isPublishedToMarketPlace ||
        IsProjectInActiveOrClosed(projectDetails)
    );
    setIsRequisitionCreationallowed(
      projectDetails.isRequisitionCreationallowed == true
    );
  };

  const onUpdateDetilsHandleChange = async () => {
    const updateTransformedData: Utils.ITransformDataForAdditionalElAndDelegateRoles =
      Utils.transformDataForAdditionalElAndDelegateRoles(
        updateDetailsContetext.projectRoleAdditionalDBData,
        updateDetailsContetext.projectRoleAdditionalData,
        projectDetails
      );
    if (updateTransformedData.isError) {
      snackbarContext.displaySnackbar(
        updateTransformedData.error,
        "error",
        6000
      );
      return;
    }
    let preValue = isUpdateProjectDisabled;
    try {
      //disable the update button
      setIsUpdateProjectDisabled(true);
      const flag = await Utils.UpdateProjectDetails(
        projectDetails,
        updateDetailsContetext.projectUpdateEL,
        updateDetailsContetext.projectUpdateDelegate,
        updateDetailsContetext.projectUpdateSkills,
        updateDetailsContetext.projectUpdateDemands,
        updateDetailsContetext.projectUpdateDescription,
        updateDetailsContetext.projectUpdateEndDate,
        updateDetailsContetext.projectUpdateIsEndDateChanged,
        updateTransformedData.updateData,
        updateDetailsContetext.projectRoleAdditionalDBData,
        updateDetailsContetext.confidential
      );

      if (flag) {
        clearProjectUpdateContext();
        getProjectDetails();
        props.getFlagForUpdate(true);
      } else props.getFlagForUpdate(false); // to be check.

      //reset to previous value of update button
      setIsUpdateProjectDisabled(preValue);
      snackbarContext.displaySnackbar(
        "Project details updated successfully!",
        "success",
        6000
      );
      updateDetailsContetext.setProjectUpdateIsEndDateUpdated(true && updateDetailsContetext.projectUpdateIsEndDateChanged)
      projectDetailsContext.setIsSelectedDateInvalid(false);
    } catch (error) {
      setIsUpdateProjectDisabled(preValue);
      let msgToShow = "";
      if (
        error?.response?.data.indexOf("InvalidProjectEndDateException") > -1
      ) {
        projectDetailsContext.setIsSelectedDateInvalid(true);
        msgToShow =
          "There is a conflict with the existing allocation or requisitions. Request you to review these and first change the allocation / requisition end date to ensure it lies within the intended proposed end date.";
      } else {
        msgToShow = "Something went wrong! Please try after sometime.";
      }

      snackbarContext.displaySnackbar(msgToShow, "error", 6000);
    }
  };
  const clearProjectUpdateContext = () => {
    setProjectUpdateDemands([]);
    setProjectUpdateSkills([]);
    setProjectUpdateDelegate([]);
    setProjectUpdateEL([]);
  };

  const onRollOverClickEvent = () => {
    setRollOverState({
      ...rollOverState,
      isRollOverApplicable: false,
      showRollOverButton: false,
    });
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=3`
    );
  };

  const IsMoveToMarketPlaceButtonDisabled = (
    allRequsitionObj: any
  ): boolean => {
    let projectEndDate = moment(new Date(projectDetails?.endDate));
    let currentDate = moment(new Date());
    let dateDiff = moment.duration(projectEndDate.diff(currentDate)).asHours();
    const isExpiredProject = !(dateDiff >= 0);

    if (
      !projectDetails.isPublishedToMarketPlace &&
      !allRequsitionObj &&
      isExpiredProject == false
    ) {
      return false;
    }
    return true;
  };

  const SetIsRequisitionCreationAllowed = async () => {
    const val = await setIsRequisitionCreationAllowed(
      pipelineCode,
      jobCode,
      !(isRequisitionCreationallowed == true)
    );

    var _project = projectDetails;
    _project.isRequisitionCreationallowed = !(
      isRequisitionCreationallowed == true
    );
    setProjectDetails(_project);
    if (!val.isPublishedToMarketPlace) {
      setIsMoveToMarketPlaceButtonEnable(!isMoveToMarketPlaceButtonEnable);
    }
  };

  const OnClickMovetoMarketPlaceButton = async () => {
    await GetPublishedFieldByprojectCodeForMarketPlace(pipelineCode, jobCode);
    setIsConfirmationPopOpen(true);
  };

  const handleChangeRequisitionClosure = () => {
    projectDetailsContext.setIsProjectDetailsDirty(true);
    SetIsRequisitionCreationAllowed();
  };

  const handleMaskFieldChangeForCheckBox = (e: any) => {
    let _activeFieldMap = activeFieldMap1;
    _activeFieldMap[e?.target?.ariaLabel] = e?.target?.checked;
    setActiveFieldMap1(_activeFieldMap);
  };

  //save marketplace project to service
  const handlerContinueClick = async () => {
    let count = 0;
    let activeFieldWithMaskedValue: any = {};

    for (var val of publishedFieldForMarketPlace) {
      if (val.isActive) {
        count++;
      }
    }

    //get all role names for the market place publishing
    //projectRolesView change
    let csp = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.CSP.toLowerCase().trim()
    )?.userName;
    //projectRolesView change
    let proposedCsp = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.ProposedCSP.toLowerCase().trim()
    )?.userName;
    //projectRolesView change
    let elForJob = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.EngagementLeader.toLowerCase().trim()
    )?.userName;
    //projectRolesView change
    let elForPipeLine = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.ProposedEL.toLowerCase().trim()
    )?.userName;
    //projectRolesView change
    let jobManager = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.JobManager.toLowerCase().trim()
    )?.userName;
    let smegLeader = projectDetails.projectRolesView.find(
      (x) =>
        x.role?.toLowerCase().trim() ===
        RolesListMaster.SMEGLeader.toLowerCase().trim()
    )?.userName;

    let reviewer = "";
    if (
      projectDetails.chargableType?.toLowerCase().trim() ==
      ProjectChargeableType.Chargable.toLowerCase().trim()
    ) {
      reviewer = csp;
    } else {
      reviewer = smegLeader;
    }
    const payload: IAddProjectToMarketPlace = {
      pipelineCode: projectDetails.pipelineCode,
      pipelineName: projectDetails.pipelineName,
      jobCode: projectDetails.jobCode,
      jobName: projectDetails.jobName,
      clientName: projectDetails.clientName,
      clientGroup: projectDetails.clientGroup,
      startDate: projectDetails.startDate,
      endDate: projectDetails.endDate,
      description: projectDetails.description,
      isPublishedToMarketPlace: projectDetails.isPublishedToMarketPlace,
      createdBy: userContext.username,
      modifiedBy: userContext.username,
      isActive: projectDetails.isActive,
      jsonData: JSON.stringify(activeFieldWithMaskedValue),
      chargableType: projectDetails.chargableType,
      location: projectDetails.location,
      Expertise: projectDetails.expertise, //Recheck
      businessUnit: projectDetails.bu,

      buId: projectDetails.buId,
      offerings: projectDetails.offerings,
      offeringsId: projectDetails.offeringsId,
      solutions: projectDetails.solutions,
      solutionsId: projectDetails.solutionsId,

      Smeg: projectDetails.smeg, //Recheck
      Sme: projectDetails.sme, //Recheck
      RevenueUnit: projectDetails.revenueUnit, //Recheck
      industry: projectDetails.industry,
      subindustry: projectDetails.subindustry,
      csp: reviewer, //projectDetails.csp,
      proposedCsp: proposedCsp, //projectDetails.proposedCsp,
      elForJob: elForJob, //projectDetails.elForJob,
      elForPipeLine: elForPipeLine, //projectDetails.elForPipeLine,
      jobManager: jobManager,
      isInterested: projectDetails.isInterested,
      marketPlaceExpirationDate: marketPlaceExpirationDate,
      ispipeLine: projectDetails.jobCode == null,
    };

    var countActiveField = 0;
    let _activeFieldMap = activeFieldMap1;

    for (let key in _activeFieldMap) {
      if (_activeFieldMap[key]) {
        countActiveField++;
      }
    }
    if (countActiveField == 0) {
      for (let key in publishedFieldForMarketPlace) {
        if (publishedFieldForMarketPlace[key].isActive) {
          _activeFieldMap[publishedFieldForMarketPlace[key].internalName] =
            true;
        }
      }
    }

    payload["jsonData"] = JSON.stringify(_activeFieldMap);

    await AddProjectToMarketPlace(payload);
    await CreateOrUpdatePublishedFieldForMarketPlace(_activeFieldMap);
    setIsConfirmationPopOpen(false);
    setIsMoveToMarketPlaceButtonEnable(true);
    setDisableCheckBoxClosedRequisition(true);
    snackbarContext.displaySnackbar("Project moved to Marketplace", "success");
  };

  //Marketplace submit confirmation Yes click handler for final save
  const handlerConfirmClick = () => {
    handlerNoFieldMaskingValidator();
  };

  const AddProjectToMarketPlace = async (load: IAddProjectToMarketPlace) => {
    const val = await addProjectToMarketPlace(load);

    // after Adding project to marketPlace successfully
    // set isPublishedToMarketPlace field in project table to to true
    if (!val.err) {
      //chnaged with MP scheduler to send dta as collection

      var publishToMarketPlacePayload: ISetPublishedToMarketPlace[] = [
        {
          isPublishedToMarketPlace: true,
          pipelineCode: load.pipelineCode,
          jobCode: load.jobCode,
          marketPlaceExpirationDate: load.marketPlaceExpirationDate,
        },
      ];

      UpdatePublishedToMarketPlace(publishToMarketPlacePayload);
    } else {
      throw val.result;
    }
  };

  const CreateOrUpdatePublishedFieldForMarketPlace = async (load: any) => {
    const val = await createOrUpdatePublishedFieldForMarketPlace(
      pipelineCode,
      jobCode,
      load
    );
  };

  const GetPublishedFieldByprojectCodeForMarketPlace = async (
    _pipelineCode: string,
    _jobCode: string
  ) => {
    const _publishedField = await getPublishedFieldByprojectCodeForMarketPlace(
      _pipelineCode,
      _jobCode
    );
    setPublishedFieldForMarketPlace(_publishedField);

    const _ActiveFieldForMarketPlace = await getActiveFieldForMarketPlace();
    const _confidentialFields = await GetApplicationLevelSettings(CONFIDENTIAL_APPLICATION_SETTING_KEY_FIELD);
    const temp = _confidentialFields[`${CONFIDENTIAL_APPLICATION_SETTING_KEY_FIELD}`].split(',') || [];
    setConfidentialFields(_confidentialFields[`${CONFIDENTIAL_APPLICATION_SETTING_KEY_FIELD}`].split(',') || []);
    let _activeFieldMap = activeFieldMap1;
    for (let val in _ActiveFieldForMarketPlace) {
      _activeFieldMap[_ActiveFieldForMarketPlace[val].internalName] = false;
    }
    setActiveFieldForMarketPlace(_ActiveFieldForMarketPlace);

    for (let val in _publishedField) {
      if (_activeFieldMap[_publishedField[val].fieldName] != undefined) {
        _activeFieldMap[_publishedField[val].fieldName] =
          _publishedField[val].isActive;
      }
    }
    setActiveFieldMap1(_activeFieldMap);
  };

  const UpdatePublishedToMarketPlace = async (
    publishToMarketPlacePayload: ISetPublishedToMarketPlace[]
  ) => {
    const val = await updatePublishedToMarketPlace(publishToMarketPlacePayload);

    //sending only one project data  so using 0th index for publish flag
    if (val && val.length > 0 && val[0].isPublishedToMarketPlace) {
      var _projectDetail = projectDetails;
      _projectDetail.isPublishedToMarketPlace = true;
      setProjectDetails(_projectDetail);
    }
  };

  const IscheckBoxIsMarked = (internalName: any) => {
    for (var val of publishedFieldForMarketPlace) {
      if (internalName === val.fieldName) {
        return val.isActive;
      }
    }
    return false;
  };

  //MarketPlace Submit button function
  const handlerNoFieldMaskingValidator = () => {
    let count = 0;
    for (let key in activeFieldMap1) {
      if (activeFieldMap1[key]) {
        count++;
      }
    }
    if (count === 0) {
      setWarningPop(true);
    } else {
      handlerContinueClick();
      snackbarContext.displaySnackbar(
        "Project moved to Marketplace.",
        "success"
      );
    }
  };

  const backButtonClick = () => {
    navigate("/");
  };

  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  return (
    <div style={{ margin: "7px 0px" }}>
      <ConfirmationDialog
        title="Confirm!"
        content="Are you sure you want to proceed?"
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openModal}
        onConfirmationPopClose={() => {
          setOpenModal(false);
        }}
        handleYesClick={() => {
          onUpdateDetilsHandleChange();
          setOpenModal(false);
        }}
      ></ConfirmationDialog>
      <Grid item xs={12} className="project-details-btn-container">
        <NavigateActionButton
          className="backButton"
          onClick={backButtonClick}
          label="BACK"
        />

        <div className="buttonContainer">
          <div>
            {false &&
              !isEmployee &&
              currentTabValue === 0 &&
              rollOverState.showRollOverButton && (
                <ActionButton
                  label={"Navigate to Roll Over"}
                  type="submit"
                  disabled={false}
                  onClick={(e: any) => {
                    onRollOverClickEvent();
                  }}
                />
              )}
          </div>
          {isMoveToMarketSectionVisible && (
            <>
              <Grid>
                {!isEmployee &&
                  currentTabValue === 0 &&
                  IsPermissionExistForProject(
                    userContext.projectPermissionData?.permissions,
                    MODULE_NAME_ENUM.Project_Details,
                    PERMISSION_TYPE.Update,
                    userContext.role
                  ) && (
                    <div className="RequisitionClosure">
                      <Checkbox
                        {...label}
                        checked={isRequisitionCreationallowed != true}
                        disabled={disableCheckBoxClosedRequisition}
                        onChange={(e) => {
                          setIsRequisitionCreationallowed(
                            !(isRequisitionCreationallowed == true)
                          );
                          handleChangeRequisitionClosure();
                        }}
                      />
                      {"Close Requisitions"}
                      <Tooltip
                        sx={{ marginLeft: "5px", marginTop: "-5px" }}
                        className={"tool-requisition"}
                        title={
                          "Check box, to deactivate requisition and allocation process on the project.  This can be unchecked at any time to activate it."
                        }
                        placement="top"
                      >
                        <InfoIcon />
                      </Tooltip>
                    </div>
                  )}
              </Grid>
              <Grid>
                {!isEmployee &&
                  currentTabValue === 0 &&
                  IsPermissionExistForProject(
                    userContext.projectPermissionData?.permissions,
                    MODULE_NAME_ENUM.Project_Details,
                    PERMISSION_TYPE.Update,
                    userContext.role
                  ) && (
                    <>
                      <ActionButton
                        label={"Move To MarketPlace"}
                        type="submit"
                        disabled={isMoveToMarketPlaceButtonEnable}
                        onClick={OnClickMovetoMarketPlaceButton}
                      />
                    </>
                  )}
              </Grid>
            </>
          )}
          <Grid>
            {isEmployee && (
              <Button
                variant="outlined"
                onClick={handleCalendarClick}
                sx={constant.calendarButtonSxProps}
                startIcon={
                  <CalendarTodayIcon sx={constant.calendarIconSxProps} />
                }
              >
                Calendar View
              </Button>
            )}

            {!isEmployee &&
              currentTabValue === 0 &&
              updateDetailsContetext.isProjectDetailsDirty &&
              (IsPermissionExistForProject(
                userContext.projectPermissionData?.permissions,
                MODULE_NAME_ENUM.Project_Details,
                PERMISSION_TYPE.Update,
                userContext.role
              ) ||
                IsPermissionExistForProject(
                  userContext.projectPermissionData?.permissions,
                  MODULE_NAME_ENUM.Assign_Additional_Delegate,
                  PERMISSION_TYPE.Update,
                  userContext.role
                )) && (
                <>
                  <ActionButton
                    label={"Update Details"}
                    type="submit"
                    disabled={
                      isSuspended ||
                      isUpdateProjectDisabled ||
                      IsProjectInActiveOrClosed(projectDetails)
                    }
                    onClick={() => setOpenModal(true)}
                  />
                </>
              )}
          </Grid>
        </div>
      </Grid>
      <Grid
        container
        justifyContent="space-between"
        className="job-name-container"
      >
        <Grid item sx={{ paddingLeft: "18px" }}>
          <Typography sx={{ fontSize: "40px" }} variant="h6" component={"span"}>
            {props?.projectDetails?.jobCode &&
            props?.projectDetails?.jobCode.length > 0
              ? props?.projectDetails?.jobName
              : props?.projectDetails?.pipelineName}
          </Typography>
        </Grid>
      </Grid>
      <>
        {/* Confirmation popup box before final save to marketplace */}
        <ConfirmationDialog
          title="Confirmation"
          content="Please review the fields to be masked & date for project availability in Marketplace. Project once sent to Marketplace cannot be retrieved!"
          noBtnLabel="Back"
          yesBtnLabel="Confirm"
          open={mpSubmitConfirmationBox}
          onConfirmationPopClose={() => {
            setMpSubmitConfirmationBox(false);
          }}
          handleYesClick={() => {
            handlerConfirmClick();
            setMpSubmitConfirmationBox(false);
          }}
        ></ConfirmationDialog>
      </>
      <ConfirmationDialog
        title="Warning"
        content="No field is selected for masking"
        noBtnLabel="Back"
        yesBtnLabel="Continue"
        open={warningPop}
        onConfirmationPopClose={() => {
          setWarningPop(false);
        }}
        handleYesClick={() => {
          setMpSubmitConfirmationBox(false);
          handlerContinueClick();
          setWarningPop(false);
        }}
      ></ConfirmationDialog>
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Modal
        open={isConfirmationPopOpen}
        onClose={() => {
          setIsConfirmationPopOpen(false);
        }}
      >
        <Box sx={style}>
          <Typography
            className="allocation-title-header"
            sx={MoveMarketPlaceHeader}
          >
            Select the fields required to be masked in Marketplace
          </Typography>

          <Typography>
            <Grid container>
              <Grid item xs={8} sm={8}>
                <span
                  className="lbl-prjmp-date"
                  style={{ fontSize: "18px", fontWeight: 600 }}
                >
                  Project will be available in Marketplace until
                  <Typography component={"span"} sx={{ color: "red" }}>
                    *
                  </Typography>
                </span>
              </Grid>
              <Grid item xs={4} sm={4} className="market-calender-control">
                <ControllerCalendar
                  name={`confirmedAllocationStartDate`}
                  control={control}
                  required={true}
                  label={""}
                  error={""}
                  minDate={new Date()}
                  onChange={(date: any) => {
                    let date1 = moment(date).local().endOf("day");
                    setMarketPlaceExpirationDate(date1);
                  }}
                  defaultValue={"new world"}
                  maxDate={new Date(props.projectDetails.endDate)}
                />
              </Grid>
              <Grid item xs={12} sm={12}>
                <br></br>
                <hr />
              </Grid>
              <Grid item xs={12} sm={12}>
                <div style={FontWeight600}>
                  <br></br>
                  Please mask the fields below which are of confidential nature
                  :<br></br>
                  <br></br>
                </div>{" "}
              </Grid>
              <Grid item xs={12} sm={12} className="masking-field-container">
                {activeFieldForMarketPlace.map((val: any, index: any) => {
                  var temp = IscheckBoxIsMarked(val.internalName);
                  return (
                    <CheckBoxComponent
                      key={index}
                      checked={projectDetails.isConfidential && confidentialFields.map(f => f.toLowerCase()).includes(val.internalName.toLowerCase())? true: temp}
                      inputPros={{
                        inputProps: { "aria-label": val.internalName },
                      }}
                     disabled={projectDetails.isConfidential && confidentialFields.map(f => f.toLowerCase()).includes(val.internalName.toLowerCase())}
                     displayName={val.displayName}
                     handleChangeForCheckBox={handleMaskFieldChangeForCheckBox}
                     index={index}
                    />
                  );
                })}
              </Grid>
              <Grid
                item
                xs={12}
                sm={12}
                sx={{
                  display: "flex",
                  justifyContent: "center",
                  marginTop: "25px",
                }}
              >
                <Grid item xs={8} />
                <Grid item xs={2}>
                  <BackActionButton
                    label={"Cancel"}
                    onClick={() => {
                      setIsConfirmationPopOpen(false);
                    }}
                  />
                </Grid>
                &nbsp; &nbsp;
                <Grid item xs={2}>
                  <ActionButton
                    label={"Submit"}
                    type="submit"
                    disabled={marketPlaceExpirationDate == null ? true : false}
                    onClick={(e: any) => {
                      setMpSubmitConfirmationBox(true);
                    }}
                  />
                </Grid>
              </Grid>
            </Grid>
          </Typography>
        </Box>
      </Modal>
    </div>
  );
};

export default MainTitle;
