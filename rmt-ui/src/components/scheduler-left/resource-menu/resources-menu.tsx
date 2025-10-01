/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  Box,
  IconButton,
  Menu,
  MenuItem,
  Modal,
  Typography,
} from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import Person2OutlinedIcon from "@mui/icons-material/Person2Outlined";
// import ProjectView from "../../project-view/project-view";
import ProjectView from "../../project-view/project-view";
import "./resourcestyles.css";
import * as constant from "./Constant";
import RemoveRedEyeIcon from "@mui/icons-material/RemoveRedEye";
import UploadFileIcon from "@mui/icons-material/UploadFile";
import PostAddIcon from "@mui/icons-material/PostAdd";
import { IProjectViewProps } from "../../project-view/IProjectViewProps";
import {
  GetAllJobCodesForPipelineCodeService,
  getBasicProjectDetailsRequestor,
  GetProjectByCode,
  getProjectCompleteDetails,
} from "../../../services/project-list-services/project-list-services";
import { useNavigate } from "react-router-dom";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import UpdateRoundedIcon from "@mui/icons-material/UpdateRounded";
import MoveUpIcon from "@mui/icons-material/MoveUp";
import { getDateInMomentFormat } from "../../../utils/date/dateHelper";
import moment from "moment";
import { IContextMenuItem, getContextMenuByRoles } from "../../appbar/Util";
import AllocationJustificationModal from "../../allocationJustificationModal/allocationJustificationModal";
import {
  GetAllRequisitionByProjectCode,
  IsReqistionExistsInProject,
} from "../../../services/allocation/getAllRequisitionByProjectCode";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import PeopleIcon from "@mui/icons-material/People";
import SegmentIcon from "@mui/icons-material/Segment";
import { ContextMenuInternalNames } from "./Constant";
import CommonAllocationWrapper from "../../common-allocation/commonAllocationWrapper";
import { EAllocationType } from "../../common-allocation/enum";
import AddIcon from "@mui/icons-material/Add";
import Movetonewjob from "../../move-to-new-job/movetonewjob";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import { TabsTitleEnum } from "../../requestor-view/constant";
import {
  IsProjectInActiveOrClosed,
  routeValueEncode,
  ToCheckMarketPlaceExpirationDate,
} from "../../../global/utils";
import { EPipelineStatus } from "../../../common/enums/EProject";
import { RolesListMaster } from "../../../common/enums/ERoles";

const ResourceMenu = (props: any) => {
  const { pipelineCode, jobCode } = props;
  const isSuspended =
    props?.project?.pipelineStatus === EPipelineStatus.Suspended ||
    props?.project?.pipelineStatus === EPipelineStatus.Lost
      ? true
      : false;
  const userContext = React.useContext(UserDetailsContext);
  const isEmployee = React.useContext(UserDetailsContext)?.isEmployee;
  // const showRollover = props?.isRollover ? true : false;
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>();
  const [openView, setOpenView] = useState(false);
  const [isMenuVisible, setIsMenuVisible] = useState(false);
  const [projectBasicDetail, setBasicProjectDetail] = useState<any>();
  const [projectCompleteDetail, setCompleteProjectDetail] = useState<any>();
  // const [showForm, setShowForm] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [allocationType, setAllocationType] = useState<string>("");
  const [disabledContextMenu, setDisabledContextMenu] = useState(false);
  const [isJustificationModalOpen, setIsJustificationModalOpen] =
    useState(false);
  const [contextMenuItems, setContextMenuItems] = useState<IContextMenuItem[]>(
    []
  );
  const [isPublishedToMarketPlace, setIsPublishedToMarketPlace] =
    useState(false);
  const [isRecurringProject, setIsRecurringProject] = useState(false);

  const [isReqCreationAllowed, setIsReqCreationAllowed] = useState(true);
  const [role, setRole] = useState<any>();

  const [showMoveToMarketPlaceButton, setShowMoveToMarketPlaceButton] =
    useState(false);
  const [openAssignNewCodeModal, setOpenAssignNewCodeModal] = useState(false);

  const [isLoading, setIsLoading] = useState(false);
  const [isJobCodeAvailableForSameTeam, setIsJobCodeAvailableForSameTeam] =
    useState(false);
  useEffect(() => {
    if (props.project.defaultExpanded) {
      GetAllJobCodesForPipelineCode(
        props.project?.pipelineCode,
        props.project?.jobCode
      );
      getProjectCompleteDetails(
        props.project?.pipelineCode,
        props.project?.jobCode
      ).then((resp: any) => {
        setCompleteProjectDetail(resp.data);
      });
    }
  }, [props.project]);

  useEffect(() => {   
      GetAllJobCodesForPipelineCode(
        props.project?.pipelineCode,
        props.project?.jobCode
      );       
  }, [props.project.pipelineCode]);


  const handleAllocateEmployeeClick = (
    pipelineCode: string,
    jobCode: string
  ) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    handleMenuClose();
    Promise.all([
      GetBasicProjectDetailRequestorByPipelineCode(pipelineCode, jobCode),
    ]).then(async (values: any) => {
      setBasicProjectDetail({
        PipelineCode: pipelineCode,
        StartDate: values[0].startDate,
        EndDate: values[0].endDate,
        Expertise: values[0].expertise,
        ProjectType: values[0].chargableType,
        Sme: values[0].sme,
        Client: values[0].clientName,
        BudgetStatus: values[0].budgetStatus,
        jobCode: values[0].jobCode,
      });
      const publishToMarketPlaceDate =
        values[1]?.data?.isPublishedToMarketPlace;
      const justificationToAllocate = values[1]?.data?.justificationToAllocate;
      await getProjectCompleteDetails(
        props.project?.pipelineCode,
        props.project?.jobCode
      ).then((resp: any) => {
        setCompleteProjectDetail(resp.data);
      });
      setIsModalOpen(true);
      // if (publishToMarketPlaceDate || justificationToAllocate) {
      //   setIsModalOpen(true);
      // } else {
      //   setIsJustificationModalOpen(true);
      // }
    });
  };

  useEffect(() => {
    const flag = moment(
      getDateInMomentFormat(props?.project?.endDate)
    ).isBefore(moment(getDateInMomentFormat(new Date())));
    setDisabledContextMenu(flag);
  }, [props?.project]);

  const handleCloseModal = () => {
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

  const navigate = useNavigate();

  const openViewHandler = () => {
    setOpenView(true);
  };

  const closeViewHandler = () => {
    setOpenView(false);
  };

  let ProjectViewProps: IProjectViewProps = {} as IProjectViewProps;

  const open = Boolean(anchorEl);

  const handleMenuClose = () => {
    setAnchorEl(null);
    setIsMenuVisible(false);
  };
  const handleIconClick = async (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    setAnchorEl(event.currentTarget);
    let payload: any[] = role; //["Admin"]
    let isPublished: boolean = false;
    //Get the project roles only on vlick of project
    await GetProjectByCode(pipelineCode, jobCode).then(async (response: any) => {
      setIsPublishedToMarketPlace(response.data.isPublishedToMarketPlace);
      if (
        response.data?.projectType?.toLowerCase() ===
        constant.EProjectType.RECURRING.toLowerCase()
      ) {
        setIsRecurringProject(true);
      } else {
        setIsRecurringProject(false);
      }
      if (response.data.isPublishedToMarketPlace) {
          const flag = await ToCheckMarketPlaceExpirationDate(response.data);
          setIsPublishedToMarketPlace(flag);
      }

      isPublished = response.data.isPublishedToMarketPlace === true;
      setIsReqCreationAllowed(response.data.isRequisitionCreationallowed);
      //projectRolesView change
      payload = props?.project?.projectRolesView
        ?.filter(
          (a) =>
            a.user?.toLowerCase()?.trim() ===
            userContext.username?.toLowerCase()?.trim()
        )
        .map((item) => item.role);
      let appRole = props?.project?.projectRolesView
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
      setRole(payload);
    });

    await isMoveToMarketPlaceButtonDisabled(pipelineCode, jobCode, isPublished);
    await getContextMenuByRoles(payload)
      .then((resp: any) => {
        setContextMenuItems(resp.data);
        setIsMenuVisible(true);
      })
      .catch((err: any) => {
        // console.log(err);
      });
  };
  const GetBasicProjectDetailRequestorByPipelineCode = (
    pipelineCode: string,
    jobCode: string
  ) => {
    return new Promise((resolve, reject) => {
      getBasicProjectDetailsRequestor(pipelineCode, jobCode)
        .then((resp) => {
          resolve(resp.data);
        })
        .catch((ex) => {});
    });
  };

  const handleViewClick = (
    event: any,
    pipelineCode: string,
    jobCode: string
  ) => {
    props.contextMenuClickHandler(
      props.project.pipelineCode,
      props.project.jobCode
    );
    event.preventDefault();

    Promise.all([
      GetBasicProjectDetailRequestorByPipelineCode(
        props.project.pipelineCode,
        props.project.jobCode
      ),
    ]).then((values: any) => {
      setBasicProjectDetail({
        PipelineCode: pipelineCode,
        StartDate: new Date(values[0].startDate).toLocaleDateString("en-US", {
          year: "numeric",
          month: "long",
          day: "numeric",
        }),
        EndDate: new Date(values[0].endDate).toLocaleDateString("en-US", {
          year: "numeric",
          month: "long",
          day: "numeric",
        }),
        Expertise: values[0].expertise,
        ProjectType: values[0].chargableType,
        Sme: values[0].sme,
        Client: values[0].clientName,
        BudgetStatus: values[0].budgetStatus,
        // ProjectCode: values[0].projectCode,
        // jobCode: values[0].projectJobCodes.map((i: any) => {
        //   if (i.isActive) {
        //     return i.jobCode;
        //   }
        // })[0],
        // ProjectCode: values[0].projectCode,
        jobCode: values[0].jobCode,
      });
      openViewHandler();
    });
    handleMenuClose();
  };

  const openCreateRequisitionHandler = (
    event: any,
    pipelineCode: string,
    jobCode: string
  ) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    event.preventDefault();
    navigate(
      `/create-requisition/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}`
    );
  };

  const openBulkUploadHandler = (
    event: any,
    pipelineCode: string,
    jobCode: string
  ) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    event.preventDefault();
    navigate(
      `/bulk-upload/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}`
    );
  };

  const IsContextItemExist = (itemInternalName: string): boolean => {
    const itemIndex = contextMenuItems.findIndex(
      (el: IContextMenuItem) => el.internalName === itemInternalName
    );

    return itemIndex > -1 ? true : false;
  };
  const handleRollforwardClick = (pipelineCode: string, jobCode: string) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=3`
    );
  };
  const GetAllJobCodesForPipelineCode = (
    pipelineCode: string,
    jobCode: string
  ): Promise<string[]> => {
    return new Promise<string[]>((resolve, reject) => {
      GetAllJobCodesForPipelineCodeService(pipelineCode, jobCode, true)
        .then((resp) => {
          // setListOfJobCodes(resp);
          // console.log(resp);
          if (resp.length > 0) {
            setIsJobCodeAvailableForSameTeam(true);
          }
          resolve(resp);
        })
        .catch(() => {
          // snackbarContext.displaySnackbar(
          //   "Error fetching job codes",
          //   SnackbarSeverity.ERROR
          // );
          resolve([]);
        });
    });
  };
  const handleMoveToMarketplaceClick = (
    pipelineCode: string,
    jobCode: string
  ) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=${TabsTitleEnum.DetailView}`
    );
  };
  const isMoveToMarketPlaceButtonDisabled = async (
    pipelineCode: string,
    jobCode: string,
    isPublished: boolean
  ) => {
    var disableFlag: boolean = true;
    //to do WIP  check req length for market place
    const val = await IsReqistionExistsInProject(pipelineCode, jobCode);
    // .then((resultData: any) => {
    //   console.log(resultData);
    // })
    // .catch((err) => {});
    disableFlag =
      disabledContextMenu ||
      isSuspended ||
      // !isReqCreationAllowed ||
      isPublished ||
      (!isPublished && !val);
    // !projectDetails.isPublishedToMarketPlace &&
    //   allRequisitionForProject.length !== 0 &&
    //   projectDetails.isRequisitionCreationallowed
    setShowMoveToMarketPlaceButton(disableFlag);
  };

  const isAssignNewCodeButtonDisabled = (isPublished: boolean) => {
    var disableFlag: boolean = true;
    disableFlag = disabledContextMenu || isSuspended || isPublished;
    return disableFlag;
  };

  const handleCalenderViewClick = (pipelineCode: string, jobCode: string) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=${TabsTitleEnum.CalenderView}`
    );
  };

  const handleAllocationsClick = (pipelineCode: string, jobCode: string) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=${TabsTitleEnum.Allocations}`
    );
  };
  const handleRequisitionsClick = (pipelineCode: string, jobCode: string) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    navigate(
      `/project-details/${routeValueEncode(pipelineCode)}/${routeValueEncode(
        jobCode
      )}?tab=${TabsTitleEnum.Requisitions}`
    );
  };
  const closeJustificationModal = () => {
    setIsJustificationModalOpen(false);
  };
  const handleAssignNewCodeClick = (pipelineCode: string, jobCode: string) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    setOpenAssignNewCodeModal(true);
    handleMenuClose();
  };

  return (
    <>
      <Box>
        <Modal open={openView} onClose={closeViewHandler}>
          <Box sx={constant.projectDetailModal}>
            <ProjectView
              closeHandler={closeViewHandler}
              isLoading={isLoading}
              setIsLoading={setIsLoading}
              role={role}
              {...projectBasicDetail}
            />
          </Box>
        </Modal>
        <IconButton
          id="basic-button"
          aria-controls={open ? "basic-menu" : undefined}
          aria-haspopup="true"
          aria-expanded={open ? "true" : undefined}
          onClick={handleIconClick}
          size="small"
          defaultValue={"pipelineCode"}
        >
          <MoreVertIcon fontSize="small"></MoreVertIcon>
        </IconButton>
        {isMenuVisible && contextMenuItems.length > 0 && (
          <Menu
            id="basic-menu"
            anchorEl={anchorEl}
            open={open}
            onClose={handleMenuClose}
            MenuListProps={{ "aria-labelledby": "basic-button" }}
          >
            {IsContextItemExist(
              constant.ContextMenuInternalNames.ViewDetails
            ) && (
              <MenuItem
                sx={constant.MenuOptions}
                data-pipelineCode={pipelineCode}
                data-jobCode={jobCode}
                onClick={(e) => {
                  handleViewClick(e, pipelineCode, jobCode);
                }}
              >
                <RemoveRedEyeIcon
                  fontSize="small"
                  sx={constant.MenuIconSxProps}
                />
                {ContextMenuInternalNames.ViewDetails}
              </MenuItem>
            )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.CalenderView
            ) && (
              <MenuItem
                sx={constant.MenuOptions}
                data-pipelineCode={pipelineCode}
                data-jobCode={jobCode}
                onClick={(e) => {
                  handleCalenderViewClick(pipelineCode, jobCode);
                }}
              >
                <CalendarMonthIcon
                  fontSize="small"
                  sx={constant.MenuIconSxProps}
                />
                {ContextMenuInternalNames.CalenderView}
              </MenuItem>
            )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.Allocations
            ) &&
              !isEmployee && (
                <MenuItem
                  data-pipelineCode={pipelineCode}
                  data-jobCode={jobCode}
                  sx={constant.MenuOptions}
                  onClick={(e) => {
                    handleAllocationsClick(pipelineCode, jobCode);
                  }}
                >
                  <PeopleIcon fontSize="small" sx={constant.MenuIconSxProps} />
                  {ContextMenuInternalNames.Allocations}
                </MenuItem>
              )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.Requisitions
            ) &&
              !isEmployee && (
                <MenuItem
                  data-pipelineCode={pipelineCode}
                  data-jobCode={jobCode}
                  sx={constant.MenuOptions}
                  onClick={(e) => {
                    handleRequisitionsClick(pipelineCode, jobCode);
                  }}
                >
                  <SegmentIcon fontSize="small" sx={constant.MenuIconSxProps} />
                  {ContextMenuInternalNames.Requisitions}
                </MenuItem>
              )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.CreateRequisition
            ) &&
              !isEmployee && (
                <MenuItem
                  sx={constant.MenuOptions}
                  data-pipelineCode={pipelineCode}
                  data-jobCode={jobCode}
                  onClick={(e) => {
                    openCreateRequisitionHandler(e, pipelineCode, jobCode);
                  }}
                  disabled={
                    disabledContextMenu ||
                    !isReqCreationAllowed ||
                    isSuspended ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                >
                  <PostAddIcon fontSize="small" sx={constant.MenuIconSxProps} />
                  {ContextMenuInternalNames.CreateRequisition}
                </MenuItem>
              )}
            {IsContextItemExist(constant.ContextMenuInternalNames.BulkUpload) &&
              !isEmployee && (
                <MenuItem
                  data-pipelineCode={pipelineCode}
                  data-jobCode={jobCode}
                  sx={constant.MenuOptions}
                  disabled={
                    disabledContextMenu ||
                    !isReqCreationAllowed ||
                    isSuspended ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                  onClick={(e) => {
                    openBulkUploadHandler(e, pipelineCode, jobCode);
                  }}
                >
                  <UploadFileIcon
                    fontSize="small"
                    sx={constant.MenuIconSxProps}
                  />
                  {ContextMenuInternalNames.BulkUpload}
                </MenuItem>
              )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.AllocateEmployee
            ) &&
              !isEmployee && (
                <MenuItem
                  sx={constant.MenuOptions}
                  onClick={(e) => {
                    setAllocationType("named");
                    handleAllocateEmployeeClick(pipelineCode, jobCode);
                  }}
                  disabled={
                    disabledContextMenu ||
                    isPublishedToMarketPlace ||
                    isSuspended ||
                    !isReqCreationAllowed ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                >
                  <Person2OutlinedIcon
                    fontSize="small"
                    sx={constant.MenuIconSxProps}
                  />
                  {ContextMenuInternalNames.AllocateEmployee}
                </MenuItem>
              )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.AllocateSameTeam
            ) &&
              !isEmployee && (
                <MenuItem
                  sx={constant.MenuOptions}
                  disabled={
                    disabledContextMenu ||
                    isPublishedToMarketPlace ||
                    isSuspended ||
                    !isReqCreationAllowed ||
                    !isRecurringProject ||
                    !isJobCodeAvailableForSameTeam ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                  onClick={(e) => {
                    setAllocationType("sameteam");
                    handleAllocateEmployeeClick(pipelineCode, jobCode);
                  }}
                >
                  <Person2OutlinedIcon
                    fontSize="small"
                    sx={constant.MenuIconSxProps}
                  />
                  {ContextMenuInternalNames.AllocateSameTeam}
                </MenuItem>
              )}

            {IsContextItemExist(
              constant.ContextMenuInternalNames.MoveToMarketplace
            ) &&
              !isEmployee && (
                <MenuItem
                  sx={constant.MenuOptions}
                  disabled={
                    showMoveToMarketPlaceButton ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                  onClick={(e) => {
                    handleMoveToMarketplaceClick(pipelineCode, jobCode);
                  }}
                >
                  {" "}
                  <MoveUpIcon fontSize="small" sx={constant.MenuIconSxProps} />
                  {ContextMenuInternalNames.MoveToMarketplace}
                </MenuItem>
              )}
            {IsContextItemExist(
              constant.ContextMenuInternalNames.AssignNewCode
            ) &&
              !isEmployee && (
                <MenuItem
                  data-pipelineCode={pipelineCode}
                  data-jobCode={jobCode}
                  sx={constant.MenuOptions}
                  disabled={
                    isAssignNewCodeButtonDisabled(isPublishedToMarketPlace) ||
                    IsProjectInActiveOrClosed(props.project)
                  }
                  onClick={(e) => {
                    handleAssignNewCodeClick(pipelineCode, jobCode);
                  }}
                >
                  <AddIcon fontSize="small" sx={constant.MenuIconSxProps} />
                  {ContextMenuInternalNames.AssignNewCode}
                </MenuItem>
              )}
          </Menu>
        )}

        <BackDropModal
          open={isModalOpen}
          onclose={handleCloseModal}
          restrictOnClose={true}
          style={{
            width: "95%",
            height: "90vh",
            borderRadius: "15px",
          }}
        >
          <Typography component="div" sx={{ marginTop: "20px" }}>
            <CommonAllocationWrapper
              back={function (): {} {
                handleCloseModal();
                return;
              }}
              baseType={
                allocationType === "named"
                  ? EAllocationType.NAME_ALLOCATION
                  : EAllocationType.SAME_TEAM_ALLOCATION
              }
              projectInfo={projectCompleteDetail}
            />
          </Typography>
        </BackDropModal>

        <Modal
          open={openAssignNewCodeModal}
          onClose={() => setOpenAssignNewCodeModal(false)}
        >
          <Movetonewjob
            setOpenAssignNewCodeModal={setOpenAssignNewCodeModal}
            projectData={props.project}
          />
        </Modal>
      </Box>
      {isJustificationModalOpen ? (
        <AllocationJustificationModal
          isModalOpen={true}
          closeJustificationModal={closeJustificationModal}
          projectDetails={projectCompleteDetail}
        />
      ) : (
        ""
      )}
    </>
  );
};

export default ResourceMenu;
