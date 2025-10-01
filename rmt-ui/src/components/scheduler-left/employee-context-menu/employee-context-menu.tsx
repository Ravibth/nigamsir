import React, { useContext, useEffect, useState } from "react";
import { Box, IconButton, Menu, MenuItem, Modal } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";
// import ProjectView from "../../project-view/project-view";
import "./employeecontextmenustyles.css";
import * as constant from "./Constant";
import RemoveRedEyeIcon from "@mui/icons-material/RemoveRedEye";
import UploadFileIcon from "@mui/icons-material/UploadFile";
import PostAddIcon from "@mui/icons-material/PostAdd";
import { IProjectViewProps } from "../../project-view/IProjectViewProps";
import {
  getBasicProjectDetailsRequestor,
  getProjectCompleteDetails,
} from "../../../services/project-list-services/project-list-services";
import { useNavigate } from "react-router-dom";
import {
  UserDetailsContext,
  IUserDetailsContext,
} from "../../../contexts/userDetailsContext";
import Movetonewjob from "../../move-to-new-job/movetonewjob";
import ResourceReleaseModal from "./resource-release-modal/resource-release-modal";
import { ReleaseResourceByGuid } from "../../../services/allocation/allocation.service";
import { SnackbarContext } from "../../../contexts/snackbarContext";
import {
  isReleaseResourceDisabled,
  isUpdateAllocationDisabled,
} from "../../activeallocation/utils";
import UpdateAllocationCommonScreen from "../../common-allocation/update-allocation-common-screeen/update-allocation-common-screeen";
import { ENavigatingFromToUpdateCommonScreen } from "../../common-allocation/enum";
import {
  IsPermissionExistForProject,
  IsProjectInActiveOrClosed,
} from "../../../global/utils";
import { MODULE_NAME_ENUM } from "../../../common/module-permission/module-permission";
import { PERMISSION_TYPE } from "../../../common/access-control-guard/access-control";

const EmployeeContextMenu = (props: any) => {
  console.log(props);
  const userContext: IUserDetailsContext = useContext(UserDetailsContext);
  const { pipelineCode, jobCode, employeeInfo } = props;
  const isEmployee = React.useContext(UserDetailsContext)?.isEmployee;
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [openMoveJobCodeModal, setOpenMoveJobCodeModal] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>();
  const [openView, setOpenView] = useState(false);
  const [projectBasicDetail, setBasicProjectDetail] = useState<any>();
  const [isUpdateAllocationOpen, setIsUpdateAllocationOpen] = useState(false);
  const [requestionsId, setRequestionsId] = useState(0);
  const [openRelease, setOpenRelease] = useState(false);
  const snackbarContext: any = useContext(SnackbarContext);
  const [projectCompleteInfo, setCompleteProjectInfo] = useState<any>();

  const navigate = useNavigate();
  const handleCloseReleaseModal = () => setOpenRelease(false);

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const openViewHandler = () => {
    setOpenView(true);
  };

  const closeViewHandler = () => {
    setOpenView(false);
  };

  const handleOpenRelease = (e: string) => {
    setOpenRelease(true);
    handleMenuClose();
  };

  let ProjectViewProps: IProjectViewProps = {} as IProjectViewProps;

  const open = Boolean(anchorEl);

  const handleMenuClose = () => {
    setAnchorEl(null);
  };
  const handleIconClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
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
        .catch((ex) => {
          console.log(ex);
        });
    });
  };

  const releaseAllocationResource = (Id: string) => {
    return new Promise((resolve, reject) => {
      ReleaseResourceByGuid(Id)
        .then(() => {
          snackbarContext.displaySnackbar(
            "Resource released successfully",
            "success"
          );
          const event = new CustomEvent("named-closed-event", {
            detail: {
              projectInfo: props?.project,
              pipelineCode: pipelineCode,
              jobCode: jobCode,
            },
          });
          document.dispatchEvent(event);
        })
        .catch((err) => {
          reject(err);
        });
    });
  };

  // const handleViewClick = (
  //   event: any,
  //   pipelineCode: string,
  //   jobCode: string
  // ) => {
  //   props.contextMenuClickHandler(pipelineCode, jobCode);
  //   event.preventDefault();
  //   console.log(event.currentTarget.dataset);
  //   Promise.all([
  //     GetBasicProjectDetailRequestorByPipelineCode(pipelineCode, jobCode),
  //   ]).then((values: any) => {
  //     console.log(values[0]);
  //     setBasicProjectDetail({
  //       PipelineCode: pipelineCode,
  //       StartDate: new Date(values[0].startDate).toLocaleDateString("en-US", {
  //         year: "numeric",
  //         month: "long",
  //         day: "numeric",
  //       }),
  //       EndDate: new Date(values[0].endDate).toLocaleDateString("en-US", {
  //         year: "numeric",
  //         month: "long",
  //         day: "numeric",
  //       }),
  //       Expertise: values[0].expertise,
  //       ProjectType: values[0].chargableType,
  //       Sme: values[0].sme,
  //       Client: values[0].clientName,
  //       BudgetStatus: values[0].budgetStatus,
  //       JobCode: values[0].jobCode,
  //     });
  //   });
  //   // console.log(event.target.innerText);
  //   openViewHandler();
  //   handleMenuClose();
  // };
  const handleRequsitionClick = (event: any) => {
    event.preventDefault();
    handleMenuClose();
  };
  const handleUploadRequsition = (event: any) => {
    props.contextMenuClickHandler(pipelineCode, jobCode);
    event.preventDefault();
    handleMenuClose();
  };

  const openCreateRequisitionHandler = (event: any, projectId: any) => {
    event.preventDefault();
    navigate("/create-requisition", {
      state: {
        project_name: projectId,
      },
    });
  };
  useEffect(() => {
    getProjectCompleteDetails(
      props.project?.pipelineCode,
      props.project?.jobCode
    ).then((resp: any) => {
      setCompleteProjectInfo(resp.data);
    });
  }, [props.project?.pipelineCode, props.project?.jobCode]);

  const openUpdateAllocation = (event: any, employeeInfo: any) => {
    // props.contextMenuClickHandler(pipelineCode, jobCode);
    event.preventDefault();
    //console.log("EmployeeInfo", employeeInfo);
    setRequestionsId(employeeInfo.requisitionId);
    setIsUpdateAllocationOpen(true);
  };

  const getContextMenuItem = () => {
    return (
      <div>
        {/* <MenuItem
          data-projectid={pipelineCode + ";#" + jobCode}
          onClick={(e) => {
            handleViewClick(e, pipelineCode, jobCode);
          }}
        >
          <RemoveRedEyeIcon fontSize="small" sx={constant.MenuIconSxProps} />
          Show Calendar View
        </MenuItem> */}
        {!isEmployee && (
          <MenuItem
            data-projectid={employeeInfo}
            disabled={
              IsProjectInActiveOrClosed(projectCompleteInfo) ||
              !IsPermissionExistForProject(
                userContext.projectPermissionData?.permissions,
                MODULE_NAME_ENUM.Allocation,
                PERMISSION_TYPE.Create,
                userContext.role
              )
            }
            onClick={(e) => {
              openUpdateAllocation(e, employeeInfo);
              handleMenuClose();
            }}
          >
            <PostAddIcon fontSize="small" sx={constant.MenuIconSxProps} />
            Update Allocation
          </MenuItem>
        )}

        {!isEmployee && (
          <MenuItem
            data-projectid={pipelineCode + ";#" + jobCode}
            disabled={
              IsProjectInActiveOrClosed(projectCompleteInfo) ||
              !IsPermissionExistForProject(
                userContext.projectPermissionData?.permissions,
                MODULE_NAME_ENUM.Allocation,
                PERMISSION_TYPE.Create,
                userContext.role
              ) ||
              isReleaseResourceDisabled(
                new Date(employeeInfo?.endDate),
                employeeInfo?.allocationStatus,
                employeeInfo?.isUpdated
              )
            }
            onClick={() => {
              handleOpenRelease(employeeInfo.guid);
            }}
          >
            <UploadFileIcon fontSize="small" sx={constant.MenuIconSxProps} />
            Release Employee
          </MenuItem>
        )}
        {/* {!isEmployee && (
          <MenuItem
            data-projectid={projectId}
            onClick={() => {
              props.contextMenuClickHandler(projectId);
              setOpenMoveJobCodeModal(true);
              handleMenuClose();
            }}
          >
            <PersonAddAltIcon fontSize="small" sx={constant.MenuIconSxProps} />
            Move to new Job code
          </MenuItem>
        )} */}
      </div>
    );
  };

  return (
    <Box>
      <>
        <ResourceReleaseModal
          open={openRelease}
          handleOpen={handleOpenRelease}
          handleCloseModal={handleCloseReleaseModal}
          releaseResource={employeeInfo.guid}
          releaseAllocationResource={releaseAllocationResource}
          pipelineCode={pipelineCode}
          jobCode={jobCode}
        />
      </>
      <Modal
        open={openMoveJobCodeModal}
        onClose={() => setOpenMoveJobCodeModal(false)}
      >
        <Box sx={constant.ModalBoxSxProps}>
          <Movetonewjob />
        </Box>
      </Modal>
      <IconButton
        id="basic-button"
        aria-controls={open ? "basic-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        onClick={handleIconClick}
        size="small"
        defaultValue={"ProjectId"}
      >
        <MoreVertIcon fontSize="small"></MoreVertIcon>
      </IconButton>
      <Menu
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleMenuClose}
        MenuListProps={{ "aria-labelledby": "basic-button" }}
      >
        {getContextMenuItem()}
      </Menu>
      {isUpdateAllocationOpen && (
        <UpdateAllocationCommonScreen
          isModalOpen={true}
          handleCloseModal={(itemId: number) => {
            setIsUpdateAllocationOpen(false);
          }}
          projectInfo={null}
          requisitionIds={[requestionsId]}
          pipelineCode={props?.project?.pipelineCode}
          jobCode={props?.project?.jobCode}
          navigatingFrom={ENavigatingFromToUpdateCommonScreen.PROJECT_LISTING}
        />
      )}
    </Box>
  );
};

export default EmployeeContextMenu;
