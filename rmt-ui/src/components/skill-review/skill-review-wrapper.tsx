import { Grid, Typography } from "@mui/material";
import ActionButton from "../actionButton/actionButton";
import SkillReviewGrid from "./skill-review-grid";
import { useContext, useEffect, useState, useRef } from "react";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../contexts/snackbarContext";
import {
  ISkillReviewGridData,
  ModifyWorkflowModelMasterToSkillReviewGridData,
} from "./utils";
import { getMySkillTasksAssigned } from "../../services/skills/userSkills.service";
import { bulkUpdateMyTask } from "../../services/allocation/workflow-service";
import { IWorkflowUpdateTask } from "../workflow/constant";
import { WORKFLOW_ACTION_STATUS } from "../../global/constant";
import ConfirmationRejectApproveSkillModal from "./modal/confirmationRejectApproveSkillModal";
import BackDropModal from "../../common/back-drop-modal/backDropModal";
import {
  getSearchSkillsService,
  getSkillsByCodeOrName,
} from "../../services/skills/skill.service";
import { ISkillsMaster } from "../../common/interfaces/ISkillsMaster";

const SkillReviewWrapper = () => {
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const [itemToApproveReject, setItemToApproveReject] =
    useState<ISkillReviewGridData | null>();
  const [
    openRejectApproveConfirmationModal,
    setOpenRejectApproveConfirmationModal,
  ] = useState<WORKFLOW_ACTION_STATUS | null>(null);
  const [initialGridRowsData, setInitialGridRowsData] = useState<
    ISkillReviewGridData[]
  >([]);
  const [gridRowsData, setGridRowsData] = useState<ISkillReviewGridData[]>([]);
  const [selectedNodes, setSelectedNodes] = useState<ISkillReviewGridData[]>(
    []
  );

  const mySkillReviewGridRef: any = useRef();

  const GetMySkillTasksAssigned = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      getMySkillTasksAssigned()
        .then((resp) => {
          const response = ModifyWorkflowModelMasterToSkillReviewGridData(resp);
          setGridRowsData(response);
          setInitialGridRowsData(response);
          resolve(true);
        })
        .catch((e) => {
          reject(e);
        });
    });
  };

  const autoSelectSkillSelectedNodes = (
    selectedItems: ISkillReviewGridData[]
  ) => {
    if (mySkillReviewGridRef?.current?.api) {
      mySkillReviewGridRef.current.api.forEachNode((node: any) => {
        if (
          selectedItems.find(
            (item) =>
              item.skillCode === node?.data?.skillCode &&
              item.skillName === node?.data?.skillName
          )
        ) {
          node.setSelected(true);
        } else {
          node.setSelected(false);
        }
      });
    }
  };

  const approveRejectSkills = (
    status: WORKFLOW_ACTION_STATUS,
    comments: string,
    itemsToUpdate: ISkillReviewGridData[]
  ): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      if (openRejectApproveConfirmationModal && itemsToUpdate.length > 0) {
        const payload: IWorkflowUpdateTask[] = itemsToUpdate.map((item) => {
          return {
            workflow_task_id: item.meta.task_list[0].id,
            status: "",
            remarks: "",
          };
        });
        bulkUpdateMyTask(payload, status, comments)
          .then(async (resp) => {
            const actionPerformed = openRejectApproveConfirmationModal;
            setOpenRejectApproveConfirmationModal(null);
            setItemToApproveReject(null);
            await GetMySkillTasksAssigned();
            snackbarContext.displaySnackbar(
              `Skill ${actionPerformed} successfully`,
              SnackbarSeverity.SUCCESS
            );
            resolve(true);
          })
          .catch((err) => {
            const actionPerformed =
              openRejectApproveConfirmationModal ===
              WORKFLOW_ACTION_STATUS.Approved
                ? "approving"
                : openRejectApproveConfirmationModal ===
                  WORKFLOW_ACTION_STATUS.Rejected
                ? "rejecting"
                : "";
            snackbarContext.displaySnackbar(
              `Error ${actionPerformed} skill`,
              SnackbarSeverity.ERROR
            );
            reject(true);
          });
      } else {
        resolve(true);
      }
    });
  };

  useEffect(() => {
    loaderContext.open(true);
    Promise.all([GetMySkillTasksAssigned()])
      .then((res) => {
        loaderContext.open(false);
      })
      .catch(() => {
        loaderContext.open(false);
      });
  }, []);
  useEffect(() => {
    const skillCodeSet = new Set(gridRowsData.map((x) => x.skillCode));
    // console.log(Array.from(skillCodeSet));
    getSkillsByCodeOrName(Array.from(skillCodeSet))
      .then((resp) => {
        let finalResp = resp as ISkillsMaster[];
        let gridRowChangedInfo = gridRowsData.map((x) => {
          x.skillMaster = finalResp.find((t) => t.skillCode === x.skillCode);
          return { ...x };
        });
        // console.log(gridRowChangedInfo);

        setGridRowsData(gridRowChangedInfo);
      })
      .catch((err) => {
        console.log(err);
        throw err;
      });
  }, [initialGridRowsData]);

  const approveRejectConfirmHandle = (comment: string) => {
    loaderContext.open(true);
    const skillsBeingRejected = itemToApproveReject
      ? [itemToApproveReject]
      : selectedNodes;
    Promise.all([
      approveRejectSkills(
        openRejectApproveConfirmationModal,
        comment,
        skillsBeingRejected
      ),
    ])
      .then(() => {
        loaderContext.open(false);
        updateSelectedNodesAfterApproveReject(skillsBeingRejected);
      })
      .catch(() => {
        loaderContext.open(false);
      });
  };

  const updateSelectedNodesAfterApproveReject = (
    updatedSkills: ISkillReviewGridData[]
  ) => {
    if (mySkillReviewGridRef?.current && mySkillReviewGridRef?.current?.api) {
      const selectedItems: ISkillReviewGridData[] =
        mySkillReviewGridRef?.current?.api?.getSelectedRows();
      const finalSelectedItems = selectedItems.filter((item) => {
        const found = updatedSkills.find(
          (skillUpdated) =>
            skillUpdated.skillCode === item.skillCode &&
            skillUpdated.skillName === item.skillName
        );
        if (found) {
          return false;
        } else {
          return true;
        }
      });
      setSelectedNodes(finalSelectedItems);

      setTimeout(() => {
        autoSelectSkillSelectedNodes(finalSelectedItems);
      }, 50);
    }
  };

  return (
    <Typography component={"div"} sx={{ p: 2 }}>
      <Grid container spacing={2} sx={{ marginBottom: 2 }}>
        <Grid item xs={10} sm={10} md={9} lg={9} />
        <Grid item xs={1} sm={1} md={1.5} lg={1.5}>
          <ActionButton
            label={"Bulk Approve"}
            onClick={function (e: any): void {
              setOpenRejectApproveConfirmationModal(
                WORKFLOW_ACTION_STATUS.Approved
              );
            }}
            disabled={selectedNodes.length === 0}
            type={"button"}
          />
        </Grid>
        <Grid item xs={1} sm={1} md={1.5} lg={1.5}>
          <ActionButton
            label={"Bulk Reject"}
            onClick={function (e: any): void {
              setOpenRejectApproveConfirmationModal(
                WORKFLOW_ACTION_STATUS.Rejected
              );
            }}
            disabled={selectedNodes.length === 0}
            type={"button"}
          />
        </Grid>
      </Grid>
      <SkillReviewGrid
        gridRowsData={gridRowsData}
        setGridRowsData={setGridRowsData}
        loaderContext={loaderContext}
        snackbarContext={snackbarContext}
        mySkillReviewGridRef={mySkillReviewGridRef}
        selectedNodes={selectedNodes}
        setSelectedNodes={setSelectedNodes}
        setItemToApproveReject={setItemToApproveReject}
        setOpenRejectApproveConfirmationModal={
          setOpenRejectApproveConfirmationModal
        }
      />
      <BackDropModal
        open={openRejectApproveConfirmationModal ? true : false}
        onclose={() => {
          setOpenRejectApproveConfirmationModal(null);
        }}
        style={{ width: "25%" }}
      >
        <ConfirmationRejectApproveSkillModal
          open={openRejectApproveConfirmationModal ? true : false}
          type={openRejectApproveConfirmationModal}
          handleCloseModal={() => {
            setOpenRejectApproveConfirmationModal(null);
          }}
          onConfirmClick={(comment: string) => {
            approveRejectConfirmHandle(comment);
          }}
        />
      </BackDropModal>
    </Typography>
  );
};
export default SkillReviewWrapper;
