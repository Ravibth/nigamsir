import * as React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { Autocomplete, Divider, Grid, Tooltip } from "@mui/material";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import * as constant from "./constant";
import Newallocation from "./newallocation/newallocation";
import Currentallocation from "./current-allocation/currentallocation";
import CloseIcon from "@mui/icons-material/Close";
import ActionButton from "../actionButton/actionButton";
import BackActionButton from "../actionButton/backactionButton";
import "./movetonewjob.css";
import {
  MoveToNewJobCode,
  getAllProjects,
  getAllProjectsByBUAndExpertiseService,
} from "../../services/project-list-services/project-list-services";
import {
  SnackbarContext,
  SnackbarSeverity,
} from "../../contexts/snackbarContext";
import { getDateInMomentFormat } from "../../utils/date/dateHelper";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import {
  CreateCommonAllocation,
  MoveNewJobAllocation,
  UpdateResourceAllocations,
  getAllPipelineOrJobCodes,
} from "../../services/allocation/allocation.service";
import { getUserAllocationByCodeMove } from "./util";
import { IResourceAllocationDetails } from "../common-allocation/Iresource-allocation-details-response";
import {
  IAllUserAllocationEntries,
  INewJobCodeMoveEntries,
} from "../common-allocation/interface";
import {
  jobCode,
  jobName,
} from "../activeallocation/AllocationFilter/constant";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 600,
  bgcolor: "background.paper",
  padding: "30px",
  borderRadius: "15px",
};

export default function Movetonewjob(props: any) {
  const { setOpenAssignNewCodeModal, projectData } = props;
  const snackbarContext: any = React.useContext(SnackbarContext);
  const [allProjectData, setAllProjectData] = useState([]);
  const [open, setOpen] = React.useState(false);
  const [allocationData, setAllocationData] = useState([]);
  const [selectedProject, setSelectedProject] = useState([]);
  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpenAssignNewCodeModal(false);
  };
  const [selectedOption, setSelectedOption] = useState({} as any);
  const {
    formState: { errors, isDirty },
    watch,
  } = useForm({ mode: "onTouched" });
  const loaderContext: LoaderContextProps = React.useContext(LoaderContext);

  const getAllProjectByBUandExpertise = () => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      getAllProjectsByBUAndExpertiseService(
        props?.projectData?.bu,
        props?.projectData?.expertise,
        props?.projectData?.endDate,
        props?.projectData?.offerings
      ).then((resp) => {
        const projectList = resp;
        resolve(projectList);
        setAllProjectData([projectList]);
        loaderContext.open(false);
      });
    }).catch((err) => {
      snackbarContext.displaySnackbar("Error in fetching data", "error", 6000);
      loaderContext.open(false);
    });
  };

  const onLoadPageApiCalls = () => {
    Promise.all([getAllProjectByBUandExpertise()])
      .then((values: any) => {
        setAllProjectData(values[0]);
      })
      .then(() => {
        const allocationData = getAllRequistionsAndAllocation(
          props?.projectData?.pipelineCode,
          props?.projectData?.jobCode
        );
      });
  };

  const getAllRequistionsAndAllocation = (
    pipelineCode: string,
    jobCode: string
  ) => {
    return new Promise((resolve, rejects) => {
      getAllPipelineOrJobCodes(pipelineCode, jobCode).then((resp) => {
        const data: IResourceAllocationDetails[] = resp.data;
        setAllocationData(resp?.data);
        resolve(resp);
      });
    });
  };

  useEffect(() => {
    onLoadPageApiCalls();
  }, []);

  const newJC = (selectedOption?.id?.split(" - ")[1] || "").trim();
  const newPC = (selectedOption?.id?.split(" - ")[0] || "").trim();
  const projectId = selectedOption?.projectId;
  console.log(newJC);

  const payloadData = {
    pipelineCode: projectData?.pipelineCode,
    jobCode: projectData?.jobCode,
    newJobCode: newJC,
    newPipelineCode: newPC,
  };

  const moveToNewJC = async (codeDetail: any) => {
    const selectedProject = allProjectData.find((a) => a.id === projectId);
    payloadData.newJobCode = selectedProject.jobCode;
    payloadData.newPipelineCode = selectedProject.pipelineCode;
    const allocationDataEntity = getUserAllocationByCodeMove(
      allocationData,
      selectedProject
    );

    loaderContext.open(true);
    await Promise.all([
      createNewAllocation(allocationDataEntity),
      updatePreviousAllocations(allocationData),
    ])
      .then(async (values: any) => {
        await MoveJobNotification(allocationDataEntity, selectedProject);
        snackbarContext.displaySnackbar(
          "Moved to new job successfully",
          "success"
        );
        loaderContext.open(false);
        const event = new CustomEvent("named-closed-event", {
          detail: {
            projectInfo: props?.project,
            pipelineCode: props?.project?.pipelineCode,
            jobCode: props?.project?.jobCode,
          },
        });
        // Dispatch/Trigger/Fire the event
        document.dispatchEvent(event);
        setOpenAssignNewCodeModal(false);
      })
      .then(() => {
        const allocationData = getAllRequistionsAndAllocation(
          props?.projectData?.pipelineCode,
          props?.projectData?.jobCode
        );
      });
    loaderContext.open(false);
  };

  const MoveJobNotification = (
    allocationDataEntity: IAllUserAllocationEntries[],
    selectProject: any
  ) => {
    const _payload: INewJobCodeMoveEntries = {
      allResourceAllocation: allocationDataEntity,
      pipelineCode: payloadData.pipelineCode,
      pipelineName: props?.projectData?.pipelineName,
      jobCode: payloadData.jobCode,
      jobName: props?.projectData?.jobName,
      newPipelineCode: selectProject.pipelineCode,
      newPipelineName: selectProject.pipelineName,
      newJobCode: selectProject.jobCode,
      newJobName: selectProject.JobName,
    };
    return new Promise((resolve, reject) => {
      MoveNewJobAllocation(false, _payload)
        .then((resp) => resolve(resp))
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error move to new job allocations",
            SnackbarSeverity.ERROR
          );
          reject(err);
        });
    });
  };

  const createNewAllocation = (
    allocationDataEntity: IAllUserAllocationEntries[]
  ) => {
    return new Promise((resolve, reject) => {
      CreateCommonAllocation(false, allocationDataEntity)
        .then((resp) => resolve(resp))
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error move to new job allocations",
            SnackbarSeverity.ERROR
          );
          reject(err);
        });
    });
  };

  const updatePreviousAllocations = (
    preAllocationDataEntity: IResourceAllocationDetails[]
  ) => {
    return new Promise((resolve, reject) => {
      UpdateResourceAllocations(preAllocationDataEntity)
        .then((resp) => resolve(resp))
        .catch((err) => {
          snackbarContext.displaySnackbar(
            "Error move to new job allocations",
            SnackbarSeverity.ERROR
          );
          reject(err);
        });
    });
  };

  // Route Block
  const [isDataDirty, setIsDataDirty] = useState<boolean>(false);

  useBlockRefreshAndBack(isDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isDirty);

  const handleNewProjectNewPipeline = (project) => {
    setSelectedProject(project);
  };
  return (
    <div>
      {blocker.state === "blocked" && isDirty ? (
        <DialogBox
          showDialog={isDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Box sx={style}>
        <Grid container spacing={2}>
          <Grid item xs={6}>
            <Typography className="title-header">Move to New Code</Typography>
          </Grid>
          <Grid
            item
            xs={6}
            sx={{ display: "flex", justifyContent: "flex-end" }}
          >
            <Tooltip title={"close"}>
              <CloseIcon onClick={handleClose} />
            </Tooltip>
          </Grid>
        </Grid>
        <form>
          <Divider sx={constant.Divide} />
          <Grid item xs={12} container className="code-container">
            <Currentallocation projectData={projectData} />
          </Grid>
          <Divider sx={constant.Divide} />
          <Grid container className="code-container">
            <Newallocation
              projectData={projectData}
              allProjectData={allProjectData.filter(
                (a) => a.id != projectData.id && !a.isPublishedToMarketPlace
              )}
              // allProjectData={allProjectData}
              setSelectedOption={setSelectedOption}
              selectedOption={selectedOption}
              setIsDataDirty={setIsDataDirty}
              onSelectedProject={handleNewProjectNewPipeline}
            />
          </Grid>
          <Grid container spacing={1} className="btn-main-container">
            <Grid item xs={2.5}>
              <BackActionButton
                label={"Cancel"}
                onClick={() => {
                  handleClose();
                }}
              />
            </Grid>
            <Grid item xs={2.5}>
              <ActionButton
                label={"Move"}
                onClick={() => {
                  moveToNewJC(payloadData);
                }}
                type={"button"}
                disabled={!(selectedOption?.label?.length > 0)}
              />
            </Grid>
          </Grid>
        </form>
      </Box>
    </div>
  );
}
