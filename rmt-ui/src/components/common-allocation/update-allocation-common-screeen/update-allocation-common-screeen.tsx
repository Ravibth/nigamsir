import { Typography } from "@mui/material";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import CommonAllocationWrapper from "../commonAllocationWrapper";
import { EAllocationType, ENavigatingFromToUpdateCommonScreen } from "../enum";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { useEffect, useState, useContext } from "react";
import { IResourceAllocationDetailsMaster } from "../../../common/interfaces/IAllocation";
import { GetMultipleAllocationsByRequisitionIDs } from "../../../services/allocation/allocation.service";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import { getMultipleTasksByByQuery } from "../../../services/allocation/workflow-service";
import { IWorkflowModelMaster } from "../../../common/interfaces/IWorkflowmodel";
import { OUTCOME } from "../../../global/constant";
import { GetProjectByCode } from "../../../services/project-list-services/project-list-services";

export interface IUpdateAllocationCommonScreenProps {
  isModalOpen: boolean;
  handleCloseModal: (itemId?: number) => void;
  projectInfo: IProjectMaster;
  requisitionIds: number[];
  pipelineCode: string;
  jobCode: string;
  navigatingFrom: ENavigatingFromToUpdateCommonScreen;
}

export interface IIsWorkflowRunning {
  isWorkflowRunning: boolean;
}
export interface IUpdateAllocationCommonScreenItem
  extends IResourceAllocationDetailsMaster,
    IIsWorkflowRunning {}

const UpdateAllocationCommonScreen = (
  props: IUpdateAllocationCommonScreenProps
) => {
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [openCommonScreen, setOpenCommonScreen] = useState<boolean | null>(
    null
  );
  const [allocationsToUpdate, setAllocationsToUpdate] = useState<
    IUpdateAllocationCommonScreenItem[]
  >([]);
  const [projectInfo, setProjectInfo] = useState<IProjectMaster>(null);

  useEffect(() => {
    if (props.requisitionIds && props.requisitionIds.length) {
      loaderContext.open(true);
      setOpenCommonScreen(false);
      Promise.all([
        fetchAndPopulateAllocationsInCommonScreen(props.requisitionIds),
        fetchProjectInfo(props.pipelineCode, props.jobCode),
      ]).then(() => {
        loaderContext.open(false);
        setOpenCommonScreen(true);
      });
    }
  }, []);

  const fetchProjectInfo = (pipelineCode: string, jobCode: string) => {
    return new Promise<boolean>((resolve, reject) => {
      if (props.projectInfo && props.projectInfo.justificationToAllocate) {
        setProjectInfo(props.projectInfo);
        resolve(true);
      } else {
        GetProjectByCode(pipelineCode, jobCode)
          .then((resp) => {
            setProjectInfo(resp.data);
            resolve(true);
          })
          .catch(() => {
            snackbarContext.displaySnackbar(
              "Error fetching project information",
              SnackbarSeverity.ERROR
            );
            resolve(true);
          });
      }
    });
  };
  const getMultipleTasksByByQueryService = (
    item_ids: string[]
  ): Promise<IWorkflowModelMaster[]> => {
    return new Promise<IWorkflowModelMaster[]>((resolve, reject) => {
      getMultipleTasksByByQuery(item_ids)
        .then((resp) => {
          resolve(resp);
        })
        .catch(() => {
          snackbarContext.displaySnackbar(
            "Error fetching workflow details for the allocations",
            SnackbarSeverity.ERROR
          );
        });
    });
  };

  const fetchAndPopulateAllocationsInCommonScreen = (ids: number[]) => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        const allocationsData =
          await GetMultipleAllocationsByRequisitionIDsService(ids);
        const runningWorkflows = await getMultipleTasksByByQueryService(
          allocationsData.map((allocations) => allocations.id)
        );
        const finalAllocationsData: IUpdateAllocationCommonScreenItem[] =
          allocationsData.map((allocation) => {
            return {
              ...allocation,
              isWorkflowRunning: runningWorkflows.find(
                (item) =>
                  item.item_id === allocation.id &&
                  item.outcome === OUTCOME.inprogress
              )
                ? true
                : false,
            };
          });
        setAllocationsToUpdate(finalAllocationsData);

        resolve(true);
      } catch (err) {
        resolve(true);
      }
    });
  };

  const GetMultipleAllocationsByRequisitionIDsService = (
    ids: number[]
  ): Promise<IResourceAllocationDetailsMaster[]> => {
    return new Promise<IResourceAllocationDetailsMaster[]>(
      (resolve, reject) => {
        GetMultipleAllocationsByRequisitionIDs(ids)
          .then((resp) => {
            // setAllocationsToUpdate(resp);
            resolve(resp);
          })
          .catch(() => {
            snackbarContext.displaySnackbar(
              "Error fetching  allocations",
              SnackbarSeverity.ERROR
            );
            resolve([]);
          });
      }
    );
  };

  const closeModal = () => {
    if (
      props.navigatingFrom ===
      ENavigatingFromToUpdateCommonScreen.PROJECT_LISTING
    ) {
      props.handleCloseModal(props.requisitionIds[0]);
      const event = new CustomEvent("named-closed-event", {
        detail: {
          projectInfo: projectInfo,
          pipelineCode: props?.pipelineCode,
          jobCode: props?.jobCode,
        },
      });
      document.dispatchEvent(event);
    } else {
      props.handleCloseModal();
    }
  };

  return (
    <>
      {openCommonScreen ? (
        <BackDropModal
          open={props.isModalOpen}
          onclose={closeModal}
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
                closeModal();
                return;
              }}
              baseType={EAllocationType.UPDATE_ALLOCATION}
              projectInfo={projectInfo}
              allocationsToUpdate={allocationsToUpdate}
            />
          </Typography>
        </BackDropModal>
      ) : (
        <></>
      )}
    </>
  );
};

export default UpdateAllocationCommonScreen;
