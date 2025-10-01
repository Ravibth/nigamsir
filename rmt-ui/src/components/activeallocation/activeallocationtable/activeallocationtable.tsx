import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import * as constant from "./constant";
import TableContainer from "@mui/material/TableContainer";
import Paper from "@mui/material/Paper";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { Grid, IconButton, Menu, MenuItem, TableCell, Typography } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { AllocateEmployeesState } from "../../../contexts/allocateEmployeesContext";
// import RollOverAvailabilityView from "../../rollOver/rollOverAvailabilityView";
import VisibilityIcon from "@mui/icons-material/Visibility";
import PostAddIcon from "@mui/icons-material/PostAdd";
import PersonRemoveSharpIcon from "@mui/icons-material/PersonRemoveSharp";
import ViewMoreAllocationDetail from "../active-allocation-modal/view-more-allocation-details-modal/view-more-allocation-detail";
import ActionButton from "../../actionButton/actionButton";
import Activeallocationfuctionbar from "../activeallocationfunctions/activeallocationfuctionbar";
import AllocationAggrid from "./allocationAggrid";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import {
  OUTCOME,
  WORKFLOW_ACTION_STATUS,
  WORKFLOW_TASK_STATUS,
  WORKFLOW_WITHDRAW_STATUS,
  WorkflowModule,
  WorkflowSubModule,
} from "../../../global/constant";
import { IGetTaskPayload, IWorkflowUpdateTask } from "../../workflow/constant";
import { bulkUpdateMyTask } from "../../../services/allocation/workflow-service";
import { LoaderContext } from "../../../contexts/loaderContext";
import { SnackbarContext } from "../../../contexts/snackbarContext";
import ReleaseResourceModal from "../active-allocation-modal/release-resource-modal/release-resource-modal";
import { getMyApprovalTasks } from "../../workflow/util";
import { ButtonGridSXProps } from "../activeallocationfunctions/constant";
import CommonAllocationWrapper from "../../common-allocation/commonAllocationWrapper";
import { EAllocationType } from "../../common-allocation/enum";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import { getContextMenuByRoles, IContextMenuItem, IsContextItemExist } from "../../appbar/Util";
import { RolesListMaster } from "../../../common/enums/ERoles";
import { ContextMenuInternalNames } from "../../scheduler-left/resource-menu/Constant";
import { IsProjectInActiveOrClosed, ToCheckMarketPlaceExpirationDate } from "../../../global/utils";
import { EPipelineStatus } from "../../../common/enums/EProject";
import { GetProjectByCode } from "../../../services/project-list-services/project-list-services";


export default function Activeallocationtable(props: any) {
  const [anchorEl, setAnchorEl] = useState(null);
  const userDetailsContext: any = useContext(UserDetailsContext);
  const [rollOverState, setRollOverState] = useState({
    isRollOverApplicable: false,
    showRollOverButton: false,
    showRollOverPanel: false,
    rollOverPipelineCode: "",
    noOfRollOverDays: 0,
  });
  const [isPublishedToMarketPlace, setIsPublishedToMarketPlace] =
    useState(false);
  const [isViewMorePopOpen, setIsViewMorePopOpen] = useState(false);
  const handleCloseViewDetailModal = () => setIsViewMorePopOpen(false);
  const [selectedRowData, setSelectedRowData] = useState(null);
  const [myPendingTasks, setMyPendingTasks] = useState<Array<any>>([]);
  const [isGridRead, setIsGridRead] = useState(true);
  const [isShowActionButton, SetIsShowActionButton] = useState(false);
  const [isProjectMoveToMarketPlace, SetIsProjectMoveToMarketPlace] = useState(false);
  const [selectedTasks, setSelectedTasks] = useState<Array<any>>([]);
  const loaderContext: any = useContext(LoaderContext);
  const snackbarContext: any = useContext(SnackbarContext);
  const [open, setOpen] = useState(false);
  const [selectedAllocationGuid, setSelectedAllocationGuid] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const handleCloseModal = () => setOpen(false);
  const { role } = React.useContext(UserDetailsContext);
  const userContext = React.useContext(UserDetailsContext);
  const [contextMenuItems, setContextMenuItems] = useState<IContextMenuItem[]>([]);
  const [isReqCreationAllowed, setIsReqCreationAllowed] = useState(true);
  
  const isSuspended =     props?.project?.pipelineStatus === EPipelineStatus.Suspended ||    props?.project?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const handleOpen = (e: string) => {
    setOpen(true);
    setSelectedAllocationGuid(e);
  };

  const handleClick = (event: any) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  useEffect(() => {
    loaderContext.open(true);
    setRollOverState({
      ...rollOverState,
      isRollOverApplicable: props.projectDetails?.isRollover,
      showRollOverButton: props.projectDetails?.isRollover,
    });
    getMyPendingTask();
    getCurrentProjectRole();
    getProjctByJobCode();    
     checkProjectMoveToMarketPlace();
  }, []);

  const getProjctByJobCode = async ()=>{
    await GetProjectByCode(props?.projectDetails?.pipelineCode, props?.projectDetails?.jobCode).then((response: any) =>{
      setIsReqCreationAllowed(response.data.isRequisitionCreationallowed);
      setIsPublishedToMarketPlace(response.data.isPublishedToMarketPlace);
     
    });
  }

  const checkProjectMoveToMarketPlace = async () => {
    let flag = await ToCheckMarketPlaceExpirationDate(props?.projectDetails);
    if (props.projectDetails?.isPublishedToMarketPlace ||
        !props.projectDetails?.isRequisitionCreationallowed) {
          setIsPublishedToMarketPlace (flag);
        }
    else {
      setIsPublishedToMarketPlace(
      props.projectDetails?.isPublishedToMarketPlace ||
        !props.projectDetails?.isRequisitionCreationallowed
    );
    }
  }; 

  const getCurrentProjectRole = async () => {
    let payload: any[] = [];
    //*********** Get project role ************/
    payload = props?.projectDetails?.projectRolesView ?.
    filter((a) => a.user?.toLowerCase()?.trim() === userContext.username?.toLowerCase()?.trim())
    .map((item) => item.role);
    //*********** Get ApplicationRole ************/
    let appRole = props?.projectDetails?.projectRolesView
        ?.filter(
          (a) =>
            a.user?.toLowerCase()?.trim() ===
            userContext.username?.toLowerCase()?.trim()
        )
        .map((item) => item.applicationRole);
      payload.push(...appRole);

    let adminRoles = userContext?.role?.filter(
      (a) =>
        a?.toLowerCase() === RolesListMaster.Admin.toLowerCase() ||
        a?.toLowerCase() === RolesListMaster.CEOCOO.toLowerCase() ||
        a?.toLowerCase() === RolesListMaster.SystemAdmin.toLowerCase()
    );
    if (payload.length === 0 && adminRoles && adminRoles.length > 0) {
      payload.push(...adminRoles);
    }
    let leadersRoles = userContext?.role?.filter(
      (a) => a?.toLowerCase() == RolesListMaster.Leaders.toLowerCase()
    );
    if (payload.length == 0 && leadersRoles && leadersRoles.length > 0) {
      payload.push(...leadersRoles);
    }
    await getContextMenuByRoles(payload)
    .then((resp: any) => {
      setContextMenuItems(resp.data);     
    })
    .catch((err: any) => {
      // console.log(err);
    });
    

  }

  const getMyPendingTask = () => {
    loaderContext.open(true);
    setIsGridRead(false);
    const payload: IGetTaskPayload = {
      outcome: OUTCOME.inprogress,
      module: WorkflowModule.EMPLOYEE_ALLOCATION,
      sub_module: WorkflowSubModule.EMPLOYEE_ALLOCATION,
      assigned_to: userDetailsContext.username,
      workflow_task_status: WORKFLOW_TASK_STATUS.pending,
    };
    return getMyApprovalTasks(payload)
      .then((response: any) => {
        setMyPendingTasks(response);
        return response;
      })
      .then(() => {
        setIsGridRead(true);
        loaderContext.open(false);
      })
      .catch(() => {
        loaderContext.open(false);
        setIsGridRead(true);
        snackbarContext.displaySnackbar(
          "Error in Fetching Workflow Status",
          "error",
          6000
        );
      });
  };

  const onRollOverClickEvent = () => {
    //const noOfRollOverDays = 20;
    setRollOverState({
      ...rollOverState,
      rollOverPipelineCode: props.projectDetails.pipelineCode, // "PC101",
      showRollOverButton: false,
      showRollOverPanel: true,
      noOfRollOverDays: props.projectDetails.rolloverDays, // props.projectDetails.rolloverDays,
    });
  };

  const setShowAvailability = (obj: any) => {
    setRollOverState({
      ...rollOverState,
      showRollOverButton: obj.showRollOverButton,
      showRollOverPanel: obj.showRollOverPanel,
    });
  };

  const openViewMoreDetailsModal = (Data: any) => {
    setSelectedRowData(Data);
    setIsViewMorePopOpen(true);
  };

  const actionButtonShowOrHide = (e: any) => {
    if (e) {
      SetIsShowActionButton(e.showActionButton);
      setSelectedTasks(e.selectedTasks);
    }
  };
  const WorkflowTaskUpdate = async (actionType: string, comments: string) => {
    loaderContext.open(true);
    await bulkUpdateMyTask(selectedTasks, actionType, comments).then(
      async () => {
        setIsGridRead(false);
        SetIsShowActionButton(false);
        getMyPendingTask();
      }
    );
    await props.fetchAllApi();
    loaderContext.open(false);
  };

  const terminateAllocation = (e, guid) => {
    const _task = myPendingTasks.filter(
      (data) =>
        data.workflow.item_id === guid &&
        data.title ===
          "Employee Allocation Task For Resource Requestor After Employee Rejection For Termination"
    );
    if (_task.length > 0) {
      const _selectedtask: IWorkflowUpdateTask = {
        workflow_id: _task[0].workflow_id,
        item_id: guid,
        workflow_task_id: _task[0].id,
        assigned_to: userDetailsContext.username,
        status: "",
        remarks: "",
        workflow_task_title: _task[0].title,
      };
      bulkUpdateMyTask(
        [_selectedtask],
        WORKFLOW_ACTION_STATUS.Approved,
        ""
      ).then(() => {
        setIsGridRead(false);
        SetIsShowActionButton(false);
        getMyPendingTask();
        props.fetchAllApi();
      });
    }
  };
  const withdrawAllocation = (e, guid) => {
    const _task = myPendingTasks.filter(
      (a: any) =>
        a.workflow.item_id == guid &&
        WORKFLOW_WITHDRAW_STATUS.includes(a.workflow.status)
    );
    if (_task.length > 0) {
      const _selectedtask: IWorkflowUpdateTask = {
        workflow_id: _task[0].workflow_id,
        item_id: guid,
        workflow_task_id: _task[0].id,
        assigned_to: userDetailsContext.username,
        status: "",
        remarks: "EMPLOYEE_WITHDRAWS_ALLOCATION_REJECTION",
        workflow_task_title: _task[0].title,
      };
      bulkUpdateMyTask(
        [_selectedtask],
        WORKFLOW_ACTION_STATUS.Approved,
        "EMPLOYEE_WITHDRAWS_ALLOCATION_REJECTION"
      ).then(() => {
        setIsGridRead(false);
        SetIsShowActionButton(false);
        getMyPendingTask();
        props.fetchAllApi();
      });
    }
  };

  const handleCloseCommonAllocationModal = () => {
    setIsModalOpen(false);
    // Create the event
    const event = new CustomEvent("named-closed-event", {
      detail: {
        projectInfo: props?.project,
        pipelineCode: props?.project?.pipelineCode,
        jobCode: props?.project?.jobCode,
      },
    });
    // Dispatch/Trigger/Fire the event
    document.dispatchEvent(event);
  };
  return (
    <>
      <ViewMoreAllocationDetail
        isViewMorePopOpen={isViewMorePopOpen}
        handleCloseViewDetailModal={handleCloseViewDetailModal}
        alloctionsList={props.allocationList}
        selectedRowData={selectedRowData}
      />

      <Grid container spacing={2} sx={constant.BtnGridContainerSXProps}>
      
        <Grid item xs={1.5}>
          {rollOverState.showRollOverButton && (
            <ActionButton
              label={"Roll Over the Project"}
              type="submit"
              disabled={false}
              // disabled={isPublishedToMarketPlace}//as discussed with ankita
              onClick={(e: any) => {
                onRollOverClickEvent();
              }}
            />
          )}
        </Grid>
        <Grid item xs={6} sm={6} md={6}></Grid>

        {isShowActionButton && (
          <Grid item xs={2} sm={2} md={2}>
            <Activeallocationfuctionbar
              {...props}
              submittedFilterData={props.submittedFilterData}
              handleResetClick={props.handleResetClick}
              handleStartDateChange={props.handleStartDateChange}
              handleEndDateChange={props.handleEndDateChange}
              selectedDataByFilter={props.selectedDataByFilter}
              selectedTasks={selectedTasks}
              ApprovedBtnClick={() => {
                WorkflowTaskUpdate(WORKFLOW_ACTION_STATUS.Approved, "");
              }}
              rejectBtnClick={(e: string) => {
                WorkflowTaskUpdate(WORKFLOW_ACTION_STATUS.Rejected, e);
              }}
              WithdrawBtnClick={(e: string) => {
                WorkflowTaskUpdate(WORKFLOW_ACTION_STATUS.Approved, e);
              }}
            />
          </Grid>
        )}
        {
        IsContextItemExist(contextMenuItems, ContextMenuInternalNames.AllocateEmployee) && !isModalOpen &&
          <Grid item xs={2} sm={2} md={2} sx={ButtonGridSXProps} className="action-button">           
              <ActionButton
                label={"Allocate Employee"}
                type="submit"
                disabled={
                  !isReqCreationAllowed ||
                  isPublishedToMarketPlace ||
                  isSuspended ||
                  IsProjectInActiveOrClosed(props.projectDetails)                                   
                }
                onClick={(e: any) => {
                  setIsModalOpen(true);
                }}
              />
          </Grid>        
      }
        
      </Grid>
      
      {!rollOverState.showRollOverPanel && (
        <>
          <Grid container sx={{ paddingTop: "10px" }}>
            <TableContainer component={Paper} sx={{ display: "none" }}>
              <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableBody>
                  {props.allocationList.map((row: any, index: number) => (
                    <TableCell sx={constant.DataRows} key={index}>
                      <IconButton onClick={handleClick}>
                        <MoreVertIcon />
                      </IconButton>
                      <Menu
                        anchorEl={anchorEl}
                        open={Boolean(anchorEl)}
                        onClose={handleClose}
                        PaperProps={{
                          style: {
                            transform: "translateX(-50%)",
                            boxShadow: "none",
                            border: "1px solid rgba(216,216,216,255)",
                            backgroundColor: "rgba(238,238,238,255)",
                          },
                        }}
                      >
                        <MenuItem onClick={handleClose} data-row={row}>
                          <span
                            onClick={(e: any) => {
                              openViewMoreDetailsModal(row);
                            }}
                          >
                            <IconButton>
                              <VisibilityIcon
                                fontSize="small"
                                sx={constant.IconSxProps}
                              />
                            </IconButton>
                            View More Details
                          </span>
                        </MenuItem>
                        <MenuItem
                          disabled={isPublishedToMarketPlace}
                          onClick={handleClose}
                        >
                          <IconButton>
                            <PostAddIcon
                              fontSize="small"
                              sx={constant.IconSxProps}
                            />
                          </IconButton>
                          Update Allocation
                        </MenuItem>
                        <MenuItem
                          onClick={handleClose}
                          disabled={isPublishedToMarketPlace}
                        >
                          <IconButton>
                            <PersonRemoveSharpIcon
                              fontSize="small"
                              sx={constant.IconSxProps}
                            />
                          </IconButton>
                          Release Employee
                        </MenuItem>
                      </Menu>
                    </TableCell>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
          <>
            <ReleaseResourceModal
              open={open}
              handleOpen={handleOpen}
              handleCloseModal={handleCloseModal}
              releaseAllocationResource={props.releaseAllocationResource}
              selectedAllocationGuid={selectedAllocationGuid}
              setAllocationList={props.setAllocationList}
              allocationList={props.allocationList}
            />
          </>
          <>
            {isGridRead && !isModalOpen && (
              <div className="allocation-main">
                <AllocationAggrid
                  allocationList={props.allocationList}
                  myPendingTasks={myPendingTasks}
                  handleClick={handleClick}
                  actionButtonShowOrHide={(e: any) => actionButtonShowOrHide(e)}
                  userEmailId={userDetailsContext.username}
                  // projectCode={props.projectDetails.projectCode}
                  pipelineCode={props.projectDetails.pipelineCode}
                  jobCode={props.projectDetails.jobCode}
                  handleOpen={handleOpen}
                  withdrawAllocation={withdrawAllocation}
                  terminateAllocation={terminateAllocation}
                  defaultSelectedNodes={props.defaultSelectedNodes}
                  setDefaultSelectedNodes={props.setDefaultSelectedNodes}
                  projectDetails={props.projectDetails}
                />
              </div>
            )}
             {isModalOpen ? (
              <BackDropModal
                open={isModalOpen}
                onclose={handleCloseCommonAllocationModal}
                restrictOnClose={true}
                style={{
                  width: "95%",
                  height: "90vh",
                  borderRadius: "15px",
                }}
              >
                <Typography component="div">
                  <CommonAllocationWrapper
                    back={function (): {} {
                      handleCloseCommonAllocationModal();
                      return;
                    }}
                    baseType={EAllocationType.NAME_ALLOCATION}
                    projectInfo={props.projectDetails}
                    isPageLoad = {true}
                  />
                </Typography>
              </BackDropModal>
            ) : (
              <></>
            )}            
          </>
        </>
      )}
    </>
  );
}
