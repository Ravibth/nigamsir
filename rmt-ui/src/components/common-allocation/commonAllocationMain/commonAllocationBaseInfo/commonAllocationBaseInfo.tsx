import { Grid, Tooltip, Typography } from "@mui/material";
import ActionButton from "../../../actionButton/actionButton";
import {
  EAllocationType,
  EBaseCommonAllocationMainControlForm,
} from "../../enum";
import { LoaderContextProps } from "../../../../contexts/loaderContext";
import NameSearchComponent from "../../name-search/name-search";
import { IProjectMaster } from "../../../../common/interfaces/IProject";
import { IUserSelectedAllocationContext } from "../../context/users-selected-allocation";
import {
  IAllUserAllocationEntries,
  IBaseCommonAllocationFormDetails,
  IUserTimeline,
} from "../../interface";
import {
  Control,
  FieldErrors,
  UseFormTrigger,
  UseFormGetValues,
} from "react-hook-form";
import NavigateActionButton from "../../../actionButton/navigateActionButton";
import { CreateCommonAllocation } from "../../../../services/allocation/allocation.service";
import {
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../../contexts/snackbarContext";
import FilterWorking from "./filter-working/filter-working";
import {
  EReturnTypeForCheckingUserErrors,
  checkIfUserHasAnyErrors,
} from "../../commonAllocationWrapper";
import { useNavigate } from "react-router-dom";
import { TabsTitleEnum } from "../../../requestor-view/constant";
import { PipelineCodeNameHeadingSxPops } from "../../style";
import ConfirmationDialog from "../../../../common/confirmation-dialog/confirmation-dialog";
import { useState } from "react";
import { routeValueEncode } from "../../../../global/utils";
import InfoIcon from "@mui/icons-material/Info";
import AllocationJustificationModal from "../../../allocationJustificationModal/allocationJustificationModal";
import FilterAltIcon from "@mui/icons-material/FilterAlt";

enum EConfirmationDialogType {
  ALLOCATE = "ALLOCATE",
  DRAFT = "DRAFT",
}

export interface ICommonAllocationBaseInfoProps {
  control: Control<IBaseCommonAllocationFormDetails, any>;
  errors: FieldErrors<IBaseCommonAllocationFormDetails>;
  trigger: UseFormTrigger<IBaseCommonAllocationFormDetails>;
  getValues: UseFormGetValues<IBaseCommonAllocationFormDetails>;
  loaderContext: LoaderContextProps;
  projectInfo: IProjectMaster;
  baseType: EAllocationType;
  setOpenBackPromptDialog: React.Dispatch<React.SetStateAction<boolean>>;
  usersSelectedAllocationContext: IUserSelectedAllocationContext;
  updateAllUserAllocationsAccordingToBaseInfos: () => Promise<void>;
  setApplyDatesToAllAllocation: React.Dispatch<React.SetStateAction<boolean>>;
  setSelectedUserForAllocationModal: React.Dispatch<
    React.SetStateAction<IAllUserAllocationEntries>
  >;
  setOpenAllocationModal: React.Dispatch<React.SetStateAction<boolean>>;
  allUserAllocationEntries: IAllUserAllocationEntries[];
  snackbarContext: SnackbarContextProps;
  userTimelines: IUserTimeline;
  setUserTimelines: React.Dispatch<React.SetStateAction<IUserTimeline>>;
  updateUserEntriesOnJobCodeChangeSameTeam: (
    selectedJobCodeIfAny: string
  ) => void;
  listOfJobCodes: string[];
  setIsFormDirty: React.Dispatch<React.SetStateAction<boolean>>;
  refreshProjectInfo: () => void;
  isPageLoad? : boolean;
}

const CommonAllocationBaseInfo = (props: ICommonAllocationBaseInfoProps) => {
  const {
    getValues,
    loaderContext,
    usersSelectedAllocationContext,
    setSelectedUserForAllocationModal,
    setOpenAllocationModal,
    allUserAllocationEntries,
  } = props;
  const navigate = useNavigate();

  const navigateToAllocationsTab = () => {
    props.setIsFormDirty(false);
    setTimeout(() => {
      navigate(
        `/project-details/${routeValueEncode(
          props.projectInfo.pipelineCode
        )}/${routeValueEncode(props.projectInfo.jobCode)}?tab=${
          TabsTitleEnum.Allocations
        }`
      );
      if (
        props.baseType.toLowerCase() ===
        EAllocationType.UPDATE_ALLOCATION.toLowerCase() || props.isPageLoad
      ) {
        window.location.reload();
      }
    }, 100);
  };
  const [openConfirmation, setOpenConfirmation] = useState<string>("");
  const [isFilterApplied, setIsFilterApplied] = useState<boolean>(false);
  const [isJustificationModalOpen, setIsJustificationModalOpen] =
    useState<EConfirmationDialogType | null>(null);
  const saveAsDraftFunction = () => {
    props.loaderContext.open(true);
    Promise.all([submitAllocations(true)])
      .then(() => {
        props.loaderContext.open(false);
        props.snackbarContext.displaySnackbar(
          "Draft allocations submitted successfully",
          SnackbarSeverity.SUCCESS
        );
        navigateToAllocationsTab();
      })
      .catch((err) => props.loaderContext.open(false));
  };
  const allocateFunction = () => {
    props.loaderContext.open(true);
    Promise.all([submitAllocations(false)])
      .then(() => {
        props.loaderContext.open(false);
        props.snackbarContext.displaySnackbar(
          "Allocations submitted successfully",
          SnackbarSeverity.SUCCESS
        );
        navigateToAllocationsTab();
      })
      .catch((err) => props.loaderContext.open(false));
  };

  const submitAllocations = (isDraft: boolean) => {
    return new Promise((resolve, reject) => {
      CreateCommonAllocation(
        isDraft,
        usersSelectedAllocationContext.usersSelected
      )
        .then((resp) => resolve(resp))
        .catch((err) => {
          props.snackbarContext.displaySnackbar(
            "Error submitting allocations",
            SnackbarSeverity.ERROR
          );
          reject(err);
        });
    });
  };

  const checkIfAnySelectedUserHasErrors = (): boolean => {
    const anyErrors = [];
    usersSelectedAllocationContext.usersSelected.forEach((item) => {
      anyErrors.push(
        checkIfUserHasAnyErrors(
          usersSelectedAllocationContext,
          item.email,
          EReturnTypeForCheckingUserErrors.BooleanValue
        )
      );
    });
    if (anyErrors.includes(true)) {
      return true;
    } else {
      return false;
    }
  };

  const CheckForJustificationAndAllocation = (
    confirmationType: EConfirmationDialogType
  ) => {
    if (
      props?.projectInfo?.isPublishedToMarketPlace ||
      props?.projectInfo?.justificationToAllocate
    ) {
      //open normal form
      setOpenConfirmation(confirmationType);
    } else {
      //Open justification modal
      setIsJustificationModalOpen(confirmationType);
    }
  };
  const justificationSuccess = () => {
    setIsJustificationModalOpen(null);
    props.refreshProjectInfo();
    setOpenConfirmation(isJustificationModalOpen);
  };
  return (
    <>
      <ConfirmationDialog
        handleYesClick={(e) => {
          if (openConfirmation === EConfirmationDialogType.ALLOCATE) {
            allocateFunction();
          } else if (openConfirmation === EConfirmationDialogType.DRAFT) {
            saveAsDraftFunction();
          }
          setOpenConfirmation("");
        }}
        title={
          openConfirmation === EConfirmationDialogType.ALLOCATE
            ? "Confirm Allocations?"
            : openConfirmation === EConfirmationDialogType.DRAFT
            ? "Confirm Draft Allocations?"
            : ""
        }
        content={
          openConfirmation === EConfirmationDialogType.ALLOCATE
            ? "Are you sure you want to allocate selected users?"
            : openConfirmation === EConfirmationDialogType.DRAFT
            ? "Are you sure you want to allocate selected users in draft?"
            : ""
        }
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openConfirmation ? true : false}
        onConfirmationPopClose={(e) => {
          setOpenConfirmation("");
        }}
      />
      <Grid item xs={1.5} sm={1.5} md={1.5} lg={1}>
        <NavigateActionButton
          onClick={function (e: any): void {
            props.setOpenBackPromptDialog(true);
          }}
          label="Back"
        />
      </Grid>
      <Grid item xs={1.3} sm={1.3} md={1.3} lg={0.8} />
      <Grid
        item
        xs={5.8}
        sm={5.8}
        md={5.8}
        lg={7.8}
        sx={PipelineCodeNameHeadingSxPops}
      >
        {props.projectInfo?.jobName
          ? props.projectInfo?.jobName
          : props.projectInfo?.pipelineName
          ? props.projectInfo?.pipelineName
          : ""}
      </Grid>
      <Grid item xs={0.2} sm={0.2} md={0.2} lg={0.2}></Grid>
      <Grid item xs={0.2} sm={0.2} md={0.2} lg={0.2}>
        <Tooltip
          placement="right"
          title={
            <span>
              <div>
                Save - Allows you to save your allocation for re-work. Resources
                will not be blocked.
              </div>
              <div>
                Allocate - Completes the allocations process and resources is
                allocated.
              </div>
            </span>
          }
        >
          <InfoIcon className="infoIconStyle" />
        </Tooltip>
      </Grid>

      <Grid item xs={1.5} sm={1.5} md={1.5} lg={1}>
        <ActionButton
          label={"Save"}
          onClick={function (e: any): void {
            CheckForJustificationAndAllocation(EConfirmationDialogType.DRAFT);
          }}
          disabled={usersSelectedAllocationContext.usersSelected.length === 0}
          type={"button"}
        />
      </Grid>
      <Grid item xs={1.5} sm={1.5} md={1.5} lg={1}>
        <ActionButton
          label={"Allocate"}
          onClick={function (e: any): void {
            CheckForJustificationAndAllocation(
              EConfirmationDialogType.ALLOCATE
            );
          }}
          disabled={
            usersSelectedAllocationContext.usersSelected.length === 0 ||
            checkIfAnySelectedUserHasErrors()
          }
          type={"button"}
        />
      </Grid>
      <Grid item xs={0.4} sm={0.4} md={0.4} lg={0.5}>
        <FilterWorking
          allUserAllocationEntries={props.allUserAllocationEntries}
          projectInfo={props.projectInfo}
          userTimelines={props.userTimelines}
          setUserTimelines={function (
            value: React.SetStateAction<IUserTimeline>
          ): void {
            props.setUserTimelines(value);
          }}
          setIsFilterApplied={setIsFilterApplied}
          isFilterApplied={isFilterApplied}
        />
      </Grid>
      <Grid item xs={0.5} sm={0.5} md={0.5} lg={0.5}>
        {isFilterApplied && (
          <Typography component="span">
            <Tooltip title="Filters applied">
              <FilterAltIcon style={{ marginTop : "5px" }} fontSize="large" />
            </Tooltip>
          </Typography>
        )}
      </Grid>
      <Grid item xs={1.7} sm={1.7} md={1.7} lg={1.7} />
      <Grid item xs={6.9} sm={6.9} md={5.9} lg={5.8}></Grid>
      {isJustificationModalOpen && (
        <AllocationJustificationModal
          isModalOpen={true}
          closeJustificationModal={() => {
            setIsJustificationModalOpen(null);
          }}
          justificationSuccess={justificationSuccess}
          projectDetails={props.projectInfo}
        />
      )}
      <Grid item xs={2.5} sm={2.5} md={3.5} lg={3.5}>
        <NameSearchComponent
          loaderContext={loaderContext}
          listOfJobCodes={props.listOfJobCodes}
          setSelectedUserForAllocationModal={setSelectedUserForAllocationModal}
          projectInfo={props.projectInfo}
          setOpenAllocationModal={setOpenAllocationModal}
          allUserAllocationEntries={allUserAllocationEntries}
          baseType={props.baseType}
          baseStartEndDateToConsiderForDefaultAllocationEntry={{
            endDate: getValues(EBaseCommonAllocationMainControlForm.endDate),
            startDate: getValues(
              EBaseCommonAllocationMainControlForm.startDate
            ),
            noOfHours: getValues(
              EBaseCommonAllocationMainControlForm.noOfHours
            ),
            isPerDayHourAllocation: getValues(
              EBaseCommonAllocationMainControlForm.isPerDayHourAllocation
            ),
          }}
          updateUserEntriesOnJobCodeChangeSameTeam={
            props.updateUserEntriesOnJobCodeChangeSameTeam
          }
        />
      </Grid>
    </>
  );
};
export default CommonAllocationBaseInfo;
