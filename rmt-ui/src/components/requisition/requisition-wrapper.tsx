import React, { useContext, useEffect, useState } from "react";
import useBlockerCustom from "../../hooks/UnsavedChangesHook/useBlockerCustom";
import DialogBox from "../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import useBlockRefreshAndBack from "../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import {
  SnackbarContext,
  SnackbarSeverity,
} from "../../contexts/snackbarContext";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";
import { useFieldArray, useForm } from "react-hook-form";
import {
  EBaseRequisitionFormMainControlForm,
  fetchConfigDefaultWeightage,
  getAllCompetencyMaster,
  getAllDesignationMaster,
  getAllIndustryMaster,
  getAllLocationMaster,
  getAllSkillMaster,
  IGlobalOptionsForParameters,
  IRequisitionFormMainDetails,
  getSubmissionPayloadForNewRequisition,
  getFreshNewRequisitionResourceEntry,
  RequisitionSnackbarMessagesAndLabels,
  getExistingRequisitionResourceEntry,
  getSubmissionPayloadForUpdateRequisition,
  checkIfRequisitionIsSameAsBefore,
  ICheckboxOption,
  ParameterDefaultValues,
} from "./utils";
import { IProjectMaster } from "../../common/interfaces/IProject";
import { IRequisitionMaster } from "../../common/interfaces/IRequisition";
import RequisitionFormBreakups from "./requisition-form-breakups/requisition-form-breakups";
import RequisitionFormHeader from "./requisition-form-header/requisition-form-header";
import RequisitionFormBaseInfo from "./requisition-form-base-info/requisition-form-base-info";
import {
  SubmitRequisitionForProjectCode,
  UpdateRequisition,
} from "../../services/requisition/requisition";
import { IDesignationMaster } from "../../common/interfaces/IDesignationMaster";
import { Grid, Typography } from "@mui/material";
import { GT_DESIGN_PARAMETERS } from "../../global/constant";
import RequisitionFormSystemSuggestions from "../system-suggestions/requisition-form-system-suggestions/requisition-form-system-suggestions";
import uniqBy from "lodash/uniqBy";
import { ConfigBuOfferingKeyCreator } from "../../common/interfaces/IConfigurationMaster";
import CustomTabPanel from "../../common/tab-panel/custom-tab-panel";

interface IRequisitionWrapper {
  projectInfo: IProjectMaster | null;
  parameterOptions: ICheckboxOption[];
  requisitionDetails?: IRequisitionMaster | null;
  updatePermissions?: boolean;
  navigateToUpdateRequisitionOnSubmission: (
    pipelineCode: string,
    requisitionId: string,
    jobCode?: string
  ) => void;
}

const RequisitionWrapper = (props: IRequisitionWrapper) => {
  const [isFormDirty, setIsFormDirty] = useState<boolean>(false);
  const [selectedTabIndex, setSelectedTabIndex] = useState<number>(0);
  const snackbarContext: any = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const [requisitionIdSubmitted, setRequisitionIdSubmitted] =
    useState<string>("");
  const [defaultParametersPriority, setDefaultParametersPriority] =
    useState(null);
  const {
    control,
    watch,
    setValue,
    handleSubmit,
    trigger,
    getValues,
    reset,
    formState: { errors, isDirty },
  } = useForm({ mode: "onTouched" });
  const [allRequisitionEntities, setAllRequisitionEntities] =
    useState<IRequisitionFormMainDetails | null>();
  const [isReadOnlyModeOn, setIsReadOnlyModeOn] = useState<boolean>(false);
  const [isUpdateModeOn, setIsUpdateModeOn] = useState<boolean>(false);
  const [optionsMaster, setOptionsMaster] =
    useState<IGlobalOptionsForParameters>({
      designation: [],
      competency: [],
      skills: [],
      location: [],
      industry: [],
      industrySubIndustryMaster: [],
    });

  const { fields, append, remove } = useFieldArray({
    name: EBaseRequisitionFormMainControlForm.allDetails,
    control,
    rules: {
      maxLength: 10,
    },
  });
  watch();

  useEffect(() => {
    setIsFormDirty(isDirty);
  }, [isDirty]);

  useBlockRefreshAndBack(isFormDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isFormDirty);
  const populateInitialStateData = async (
    optionsMasterTemp: IGlobalOptionsForParameters
  ) => {
    if (props.requisitionDetails) {
      // Update use case
      setIsReadOnlyModeOn(true);
      setIsUpdateModeOn(true);
      setRequisitionIdSubmitted(props?.requisitionDetails?.id);
      populateDataForExistingRequisitionForm(
        props.requisitionDetails,
        optionsMasterTemp
      );
    } else {
      // New Requisition Form
      const defaultWeightageValuesBasicTemp = ParameterDefaultValues(
        props.parameterOptions
      );
      setDefaultParametersPriority(defaultWeightageValuesBasicTemp);
      try {
        const defaultParameterPriorityTemp = await fetchConfigDefaultWeightage(
          ConfigBuOfferingKeyCreator(
            props.projectInfo.bu,
            props.projectInfo.offerings
          ),
          defaultWeightageValuesBasicTemp
        );
        setDefaultParametersPriority(defaultParameterPriorityTemp);
        populateDataForNewForm(defaultParameterPriorityTemp, optionsMasterTemp);
      } catch (err) {
        snackbarContext.displaySnackbar(
          RequisitionSnackbarMessagesAndLabels.ErrorFetchingDetails,
          SnackbarSeverity.ERROR
        );
      }
    }
  };

  const populateDataForExistingRequisitionForm = (
    requisitionDetails: IRequisitionMaster,
    optionsMasterTemp: IGlobalOptionsForParameters
  ) => {
    const existingResourceEntry = getExistingRequisitionResourceEntry(
      optionsMasterTemp,
      requisitionDetails
    );
    const designation: IDesignationMaster = optionsMasterTemp.designation.find(
      (designationItem) =>
        designationItem?.designation_name?.toLowerCase() ===
        requisitionDetails?.designation?.toLowerCase()
    );

    const finalData: IRequisitionFormMainDetails = {
      [EBaseRequisitionFormMainControlForm.allDetails]: [existingResourceEntry],
      [EBaseRequisitionFormMainControlForm.designation]: designation,
      [EBaseRequisitionFormMainControlForm.description]:
        requisitionDetails?.description,
      [EBaseRequisitionFormMainControlForm.businessUnit]:
        requisitionDetails?.businessUnit,
      [EBaseRequisitionFormMainControlForm.offerings]:
        requisitionDetails?.offerings,
      [EBaseRequisitionFormMainControlForm.solutions]:
        requisitionDetails?.solutions,
      [EBaseRequisitionFormMainControlForm.numberOfResources]: 1,
      [EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails]:
        true,
    };
    setValue(
      EBaseRequisitionFormMainControlForm.businessUnit,
      finalData[EBaseRequisitionFormMainControlForm.businessUnit]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.offerings,
      finalData[EBaseRequisitionFormMainControlForm.offerings]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.solutions,
      finalData[EBaseRequisitionFormMainControlForm.solutions]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.description,
      finalData[EBaseRequisitionFormMainControlForm.description]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.designation,
      finalData[EBaseRequisitionFormMainControlForm.designation]
    );

    append(existingResourceEntry);
    setAllRequisitionEntities(finalData);
  };

  const populateDataForNewForm = (
    defaultParameterPriorityTemp: any,
    optionsMasterTemp: IGlobalOptionsForParameters
  ) => {
    const freshResourceEntry = getFreshNewRequisitionResourceEntry(
      defaultParameterPriorityTemp,
      optionsMasterTemp,
      props.projectInfo,
      props.parameterOptions
    );
    const finalNewData: IRequisitionFormMainDetails = {
      [EBaseRequisitionFormMainControlForm.allDetails]: [freshResourceEntry],
      [EBaseRequisitionFormMainControlForm.designation]: null,
      // [EBaseRequisitionFormMainControlForm.grade]: "",
      [EBaseRequisitionFormMainControlForm.description]: "",
      [EBaseRequisitionFormMainControlForm.businessUnit]: props?.projectInfo.bu,
      [EBaseRequisitionFormMainControlForm.offerings]:
        props.projectInfo?.offerings,
      [EBaseRequisitionFormMainControlForm.solutions]:
        props.projectInfo?.solutions,
      [EBaseRequisitionFormMainControlForm.numberOfResources]: 1,
      [EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails]:
        true,
    };
    setValue(
      EBaseRequisitionFormMainControlForm.businessUnit,
      finalNewData[EBaseRequisitionFormMainControlForm.businessUnit]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.offerings,
      finalNewData[EBaseRequisitionFormMainControlForm.offerings]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.solutions,
      finalNewData[EBaseRequisitionFormMainControlForm.solutions]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.numberOfResources,
      finalNewData[EBaseRequisitionFormMainControlForm.numberOfResources]
    );
    setValue(
      EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails,
      finalNewData[
        EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
      ]
    );
    append(freshResourceEntry);
    setAllRequisitionEntities(finalNewData);
  };
  useEffect(() => {
    if (props?.projectInfo) {
      loaderContext.open(true);
      fetchAllMasterDataOptions().then((resp) => {
        populateInitialStateData(resp);
        loaderContext.open(false);
      });
    }
  }, [props?.projectInfo]);

  const fetchAllMasterDataOptions =
    async (): Promise<IGlobalOptionsForParameters> => {
      return new Promise((resolve, reject) => {
        Promise.all([
          getAllDesignationMaster(),
          getAllCompetencyMaster(),
          getAllSkillMaster(),
          getAllLocationMaster(),
          getAllIndustryMaster(),
        ])
          .then((masterData) => {
            const masterOptionsData = {
              designation: uniqBy(masterData[0], "designation_name"),
              competency: uniqBy(masterData[1], "competency"),
              skills: masterData[2],
              location: uniqBy(masterData[3], "location_name"),
              industry: uniqBy(masterData[4], "industry_name"),
              industrySubIndustryMaster: masterData[4],
            };
            setOptionsMaster(masterOptionsData);
            resolve(masterOptionsData);
          })
          .catch((error) => {
            snackbarContext.displaySnackbar(
              RequisitionSnackbarMessagesAndLabels.ErrorFetchingDetails,
              SnackbarSeverity.ERROR
            );
            resolve(optionsMaster);
          });
      });
    };

  const numberOfNewEntriesToAdd = (entriesToAdd: number) => {
    for (let index = 0; index < entriesToAdd; index++) {
      append(
        getFreshNewRequisitionResourceEntry(
          defaultParametersPriority,
          optionsMaster,
          props.projectInfo,
          props.parameterOptions
        )
      );
    }
  };

  const numberOfEntriesToRemoveFromEnd = (entriesToRemove: number) => {
    let indexToRemove = fields.length - 1;
    for (let index = 0; index < entriesToRemove; index++) {
      remove(indexToRemove);
      trigger(EBaseRequisitionFormMainControlForm.allDetails);
      indexToRemove--;
    }
  };

  const appendRemoveResourcesFields = () => {
    const numberOfResources = getValues(
      EBaseRequisitionFormMainControlForm.numberOfResources
    );
    if (
      allRequisitionEntities &&
      numberOfResources <= 10 &&
      numberOfResources > 0
    ) {
      const allResourcesHaveSimilarDetails = getValues(
        EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
      );

      const alreadyAddedRequisitionResources = fields.length;

      if (
        numberOfResources === 1 &&
        allResourcesHaveSimilarDetails &&
        alreadyAddedRequisitionResources === 0
      ) {
        numberOfNewEntriesToAdd(1);
      } else if (
        allResourcesHaveSimilarDetails &&
        alreadyAddedRequisitionResources > 1
      ) {
        setSelectedTabIndex(0);
        numberOfEntriesToRemoveFromEnd(alreadyAddedRequisitionResources - 1);
      } else {
        if (
          alreadyAddedRequisitionResources < numberOfResources &&
          !allResourcesHaveSimilarDetails
        ) {
          numberOfNewEntriesToAdd(
            numberOfResources - alreadyAddedRequisitionResources
          );
        } else if (
          alreadyAddedRequisitionResources > numberOfResources &&
          !allResourcesHaveSimilarDetails
        ) {
          numberOfEntriesToRemoveFromEnd(
            alreadyAddedRequisitionResources - numberOfResources
          );
        }
      }
    }
  };

  // ***************** Code for different types of resources if toggle is off ********
  // const handleChange = (event: React.SyntheticEvent, newValue: number) => {
  //   setSelectedTabIndex(newValue);
  // };

  // const getTabLabel = (index: number) => {
  //   const allResourcesHaveSimilarDetails = getValues(
  //     EBaseRequisitionFormMainControlForm.allResourcesHaveSimilarDetails
  //   );
  //   if (allResourcesHaveSimilarDetails) {
  //     return "Resource";
  //   } else {
  //     return `Resource ${index + 1}`;
  //   }
  // };

  // const deleteResourceItem = (indexToRemove: number) => {
  //   remove(indexToRemove);

  //   const numberOfResources = getValues(
  //     EBaseRequisitionFormMainControlForm.numberOfResources
  //   );
  //   if (numberOfResources === 1) {
  //     setSelectedTabIndex(0);
  //   }

  //   setValue(
  //     EBaseRequisitionFormMainControlForm.numberOfResources,
  //     numberOfResources - 1
  //   );
  // };

  const onSubmit = async (values) => {
    loaderContext.open(true);
    if (isUpdateModeOn) {
      await submitUpdateRequisition(values);
    } else {
      await submitNewRequisition(values);
    }

    loaderContext.open(false);
  };

  const submitUpdateRequisition = async (values) => {
    try {
      const requisitionSameAsBefore = checkIfRequisitionIsSameAsBefore(
        allRequisitionEntities,
        values
      );
      if (!requisitionSameAsBefore) {
        const payload = getSubmissionPayloadForUpdateRequisition(
          values,
          props.projectInfo,
          requisitionIdSubmitted
        );
        const response = await UpdateRequisition(payload);
        snackbarContext.displaySnackbar(
          RequisitionSnackbarMessagesAndLabels.RequisitionUpdateSuccessfully,
          SnackbarSeverity.SUCCESS
        );
        setRequisitionIdSubmitted(response?.data[0]?.id);
        setAllRequisitionEntities(values);
        setIsUpdateModeOn(true);
        setIsReadOnlyModeOn(true);
        return response;
      }
      setIsUpdateModeOn(true);
      setIsReadOnlyModeOn(true);
    } catch (error) {
      snackbarContext.displaySnackbar(
        RequisitionSnackbarMessagesAndLabels.RequisitionUpdateError,
        SnackbarSeverity.ERROR
      );
    }
  };

  const submitNewRequisition = async (values) => {
    try {
      const payload = getSubmissionPayloadForNewRequisition(
        values,
        props.projectInfo
      );

      const response = await SubmitRequisitionForProjectCode(payload);
      snackbarContext.displaySnackbar(
        RequisitionSnackbarMessagesAndLabels.RequisitionCreatedSuccessfully,
        SnackbarSeverity.SUCCESS
      );
      setIsUpdateModeOn(true);
      setIsReadOnlyModeOn(true);
      setRequisitionIdSubmitted(response?.data?.id);

      props.navigateToUpdateRequisitionOnSubmission(
        props.projectInfo.pipelineCode,
        response?.data?.id,
        props.projectInfo?.jobCode
      );

      return response;
    } catch (error) {
      snackbarContext.displaySnackbar(
        RequisitionSnackbarMessagesAndLabels.RequisitionCreationError,
        SnackbarSeverity.ERROR
      );
    }
  };

  return (
    <>
      {blocker.state === "blocked" && isFormDirty ? (
        <DialogBox
          showDialog={isFormDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <form onSubmit={handleSubmit(onSubmit)}>
        <RequisitionFormHeader
          projectInfo={props.projectInfo}
          isReadOnlyModeOn={isReadOnlyModeOn}
          setIsReadOnlyModeOn={setIsReadOnlyModeOn}
          updatePermissions={props?.updatePermissions}
          isUpdateModeOn={isUpdateModeOn}
        />
        <Typography
          component="div"
          sx={{
            backgroundColor: GT_DESIGN_PARAMETERS.GtLightPurpleColor2,
            ml: 3,
            mr: 3,
            pt: 3,
            pb: 3,
          }}
        >
          <RequisitionFormBaseInfo
            control={control}
            getValues={getValues}
            setValue={setValue}
            isReadOnlyModeOn={isReadOnlyModeOn}
            errors={errors}
            isUpdateModeOn={isUpdateModeOn}
            optionsMaster={optionsMaster}
            appendRemoveResourcesFields={appendRemoveResourcesFields}
          />
        </Typography>

        {/* {!isUpdateModeOn && (
        <Grid item xs={12} sx={RequisitionDetailsContainer}>
          <Box>
            <Box
              className={"tab-container"}
              sx={{
                borderBottom: 1,
                borderColor: "divider",
              }}
            >
              <Tabs
                variant="scrollable"
                value={selectedTabIndex}
                onChange={handleChange}
                scrollButtons="auto"
              >
                {fields.map((tab, index) => (
                  <Tab
                    key={tab.id}
                    label={
                      <div className="tab-label">
                        <h2>{getTabLabel(index)}</h2>
                        {fields.length > 1 && (
                          <IconButton
                            onClick={() => {
                              deleteResourceItem(index);
                            }}
                          >
                            <CloseIcon />
                          </IconButton>
                        )}
                      </div>
                    }
                  />
                ))}
              </Tabs>
            </Box>
          </Box>
        </Grid>
      )} */}
        {fields.map((field, index) => {
          return (
            index === selectedTabIndex && (
              <CustomTabPanel
                key={field.id}
                value={selectedTabIndex}
                index={selectedTabIndex}
              >
                <RequisitionFormBreakups
                  key={field.id}
                  field={field}
                  index={index}
                  isReadOnlyModeOn={isReadOnlyModeOn}
                  control={control}
                  getValues={getValues}
                  setValue={setValue}
                  errors={errors}
                  isUpdateModeOn={isUpdateModeOn}
                  projectInfo={props.projectInfo}
                  trigger={trigger}
                  optionsMaster={optionsMaster}
                  reset={reset}
                />
              </CustomTabPanel>
            )
          );
        })}
      </form>
      {!props?.projectInfo?.isPublishedToMarketPlace &&
        isUpdateModeOn &&
        isReadOnlyModeOn && (
          <Grid container spacing={2} sx={{ padding: 4 }}>
            <Grid item xs={12}>
              <RequisitionFormSystemSuggestions
                requisitionId={requisitionIdSubmitted}
                projectInfo={props.projectInfo}
              />
            </Grid>
          </Grid>
        )}
    </>
  );
};
export default React.memo(RequisitionWrapper);
