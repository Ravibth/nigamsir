import React, { useContext, useEffect, useState } from "react";
import {
  Button,
  FormControlLabel,
  FormLabel,
  Grid,
  Radio,
  RadioGroup,
  Tooltip,
} from "@mui/material";
import { Controller, useFieldArray, useForm } from "react-hook-form";
import {
  IAllUserAllocationEntries,
  IBaseCommonAllocationFormDetails,
} from "../interface";
import * as GlobalConstant from "../../../global/constant";
import ControllerTextField from "../../controllerInputs/controllerTextField";
import {
  CheckUserAvailabilityForDatesAndEffortsUtils,
  EAllocateEmployeeFormDetailVariables,
  EAllocationsBreakupVariables,
  FormValuesForAllocationForm,
  GetTotalEffort,
  IAllocateFormSkills,
  IisAllocationHoursAvailableResponse,
} from "./utils";
import AllocateBreakups from "./allocateBreakups/allocateBreakups";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import BackActionButton from "../../actionButton/backactionButton";
import ActionButton from "../../actionButton/actionButton";
import AddCircleOutlinedIcon from "@mui/icons-material/AddCircleOutlined";
import * as FormSxPropsStyle from "./style";
import RequisitionInfo from "./requisition-info/requisition-info";
import { AllocationContextState } from "./allocationContext/allocationContext";
import ControllerNumberTextField from "../../controllerInputs/controllerNumbeTextfield";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import { AllocationBreakupSxProps } from "./style";
import { getApprovedSkillByEmail } from "../../../services/skills/userSkills.service";
import _ from "lodash";
import useBlockRefreshAndBack from "../../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import useBlockerCustom from "../../../hooks/UnsavedChangesHook/useBlockerCustom";
import DialogBox from "../../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import moment from "moment";
import { getEmailId } from "../../../global/utils";
import ControllerAutoCompleteChipsSimple from "../../controllerInputs/controllerAutoCompleteChipsSimple";
import { GetNewDateWithNoonTimeZone } from "../../../utils/date/dateHelper";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import InfoIcon from "@mui/icons-material/Info";
// import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
// import { getAllCompetency } from "../../../services/wcgt-master-services/wcgt-master-services";
// import ControllerAutoCompleteTextFieldWithGetOptionsLabel from "../../controllerInputs/ControllerAutoCompleteTextFieldWithGetOptionsLabel";

export interface IMainAllocationFormProps {
  selectedUserForAllocationModal: IAllUserAllocationEntries;
  projectInfo: IProjectMaster;
  updateAllocationForEmployee: (
    updatedEntry: IAllUserAllocationEntries
  ) => void;
  closeModel: () => void;
  addNameAllocationUsersToTimeline: (
    newlyAddedUserAllocationEntries: IAllUserAllocationEntries[]
  ) => Promise<void>;
  baseStartEndDateToConsiderForDefaultAllocationEntry: {
    startDate: Date;
    endDate: Date;
    noOfHours: number;
    isPerDayHourAllocation: boolean;
  };
  submitAllocationForm: (
    formData: FormValuesForAllocationForm
  ) => Promise<boolean>;
}

const MainAllocationForm = (props: IMainAllocationFormProps) => {
  const {
    control,
    watch,
    trigger,
    setValue,
    setError,
    handleSubmit,
    clearErrors,
    getValues,
    formState: { errors, isDirty },
  } = useForm<FormValuesForAllocationForm>({
    mode: "onTouched",
    defaultValues: {
      [EAllocateEmployeeFormDetailVariables.Description]: "",
      [EAllocateEmployeeFormDetailVariables.ContinuousAllocation]: false,
      [EAllocateEmployeeFormDetailVariables.TotalEfforts]: 8,
      [EAllocateEmployeeFormDetailVariables.Allocations]: [],
    },
  });
  const { fields, append, prepend, remove } = useFieldArray({
    name: EAllocateEmployeeFormDetailVariables.Allocations,
    control,
    rules: {
      maxLength: GlobalConstant.ALLOCATION_BREAKUP_MAX_LENGTH,
    },
  });
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const [skillsMaster, setSkillsMaster] = useState<IAllocateFormSkills[]>([]);
  // const [finalSkillsByCompetency, setFinalSkillsByCompetency] = useState<
  //   IAllocateFormSkills[]
  // >([]);
  // const [competencyMaster, setCompetencyMaster] = useState<ICompetencyMaster[]>(
  //   []
  // );
  const [isWorkflowRunning, setIsWorkflowRunning] = useState<boolean>(false);
  const [isFormDirty, setIsFormDirty] = useState<boolean>(false);
  const [isAllocationBreakupDirty, setIsAllocationBreakupDirty] =
    useState<boolean>(false);

  useEffect(() => {
    setIsFormDirty(isDirty);
  }, [isDirty]);
  const [isViewMode, setIsViewMode] = useState<boolean>(false);
  const [isSavePressed, setIsSavePressed] = useState<boolean>(false);
  useBlockRefreshAndBack(isFormDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isFormDirty);

  // useEffect(() => {
  //   if (
  //     skillsMaster &&
  //     skillsMaster.length &&
  //     competencyMaster &&
  //     competencyMaster.length
  //   ) {
  //     getSkillsOptionBasedOnCompetency();
  //   }
  // }, [
  //   skillsMaster,
  //   competencyMaster,
  //   getValues(EAllocateEmployeeFormDetailVariables.Competency),
  // ]);

  // const getSkillsOptionBasedOnCompetency = () => {
  //   const sortedSkillOptions = [...skillsMaster].sort((a, b) =>
  //     a.skillName?.localeCompare(b.skillName)
  //   );
  //   const selectedCompetency = getValues(
  //     EAllocateEmployeeFormDetailVariables.Competency
  //   );
  //   if (selectedCompetency) {
  //     const finalOptions = sortedSkillOptions.filter((skillItem) => {
  //       return skillItem?.skill.skill_Mapping.find(
  //         (item) =>
  //           item.competency?.toLowerCase() ===
  //           selectedCompetency?.competency?.toLowerCase()
  //       );
  //     });
  //     setFinalSkillsByCompetency(finalOptions);
  //   } else {
  //     setFinalSkillsByCompetency([]);
  //   }
  // };

  const [
    baseDataForAllocationRestrictions,
    setBaseDataForAllocationRestrictions,
  ] = useState<IBaseCommonAllocationFormDetails>({
    startDate: undefined,
    endDate: undefined,
    noOfHours: 0,
    isRequisition: false,
    isPerDayHourAllocation: true,
  });

  const getMinMaxDatesForAllocation = () => {
    let tempBaseData: IBaseCommonAllocationFormDetails = {
      startDate:
        props.baseStartEndDateToConsiderForDefaultAllocationEntry.startDate,
      endDate:
        props.baseStartEndDateToConsiderForDefaultAllocationEntry.endDate,
      noOfHours: 0,
      isRequisition: false,
      isPerDayHourAllocation: true,
    };

    if (props.selectedUserForAllocationModal.requisition) {
      tempBaseData.noOfHours =
        props.selectedUserForAllocationModal.requisition.totalHours;
      tempBaseData.isRequisition = true;
      tempBaseData.isPerDayHourAllocation =
        props.selectedUserForAllocationModal.requisition.isPerDayHourAllocation;
    } else {
      tempBaseData.noOfHours =
        props.baseStartEndDateToConsiderForDefaultAllocationEntry.noOfHours;
      tempBaseData.isRequisition = false;
      tempBaseData.isPerDayHourAllocation =
        props.baseStartEndDateToConsiderForDefaultAllocationEntry.isPerDayHourAllocation;
    }
    setBaseDataForAllocationRestrictions(tempBaseData);
  };

  const autoPopulateSkills = () => {
    const email = props?.selectedUserForAllocationModal?.email;

    return new Promise<boolean>((resolve, reject) => {
      getApprovedSkills([email])
        .then(async (userApprovedSkillsResponse: IAllocateFormSkills[]) => {
          if (userApprovedSkillsResponse) {
            let savedSkills: IAllocateFormSkills[] = [];
            props.selectedUserForAllocationModal.skills.forEach((element) => {
              savedSkills.push(element);
            });
            const selectedSkills: IAllocateFormSkills[] = [];
            savedSkills.forEach((element) => {
              const findIfSkillIsMatched = userApprovedSkillsResponse.find(
                (respItem) =>
                  respItem.skillName?.trim()?.toLowerCase() ===
                    element.skillName?.trim()?.toLowerCase() &&
                  respItem.skillCode?.trim()?.toLowerCase() ===
                    element.skillCode?.trim()?.toLowerCase()
              );

              if (findIfSkillIsMatched) {
                selectedSkills.push({
                  skillName: findIfSkillIsMatched.skillName,
                  skillCode: findIfSkillIsMatched.skillCode,
                  skill: findIfSkillIsMatched.skill,
                });
              }
            });
            setValue(
              EAllocateEmployeeFormDetailVariables.Skills,
              selectedSkills
            );
          }
          resolve(true);
        })
        .catch((E) => {
          snackbarContext.displaySnackbar("Something went Wrong.", "error");
          resolve(true);
        });
    });
  };

  // const getCompetencyMasterFromWCGt = (): Promise<boolean> => {
  //   return new Promise<boolean>((resolve, reject) => {
  //     getAllCompetency()
  //       .then((resp) => {
  //         setCompetencyMaster(resp);
  //         resolve(true);
  //       })
  //       .catch((err) => {
  //         snackbarContext.displaySnackbar("Something went Wrong.", "error");
  //         resolve(true);
  //       });
  //   });
  // };

  const autoPopulateData = async () => {
    const baseValues: FormValuesForAllocationForm = {
      [EAllocateEmployeeFormDetailVariables.Description]:
        props.selectedUserForAllocationModal.description,
      [EAllocateEmployeeFormDetailVariables.ContinuousAllocation]:
        props.selectedUserForAllocationModal.isContinuousAllocation,
      [EAllocateEmployeeFormDetailVariables.TotalEfforts]:
        props.selectedUserForAllocationModal.totalEfforts,
      [EAllocateEmployeeFormDetailVariables.Allocations]:
        props.selectedUserForAllocationModal.allocations,
      [EAllocateEmployeeFormDetailVariables.Skills]:
        props.selectedUserForAllocationModal.skills,
      [EAllocateEmployeeFormDetailVariables.Competency]:
        props.selectedUserForAllocationModal.competency,
    };
    await Promise.all([
      // getCompetencyMasterFromWCGt(),
      autoPopulateSkills(),
    ]);
    // setValue(
    //   EAllocateEmployeeFormDetailVariables.Competency,
    //   baseValues[EAllocateEmployeeFormDetailVariables.Competency]
    // );
    // await autoPopulateSkills();
    setValue(
      EAllocateEmployeeFormDetailVariables.Description,
      baseValues[EAllocateEmployeeFormDetailVariables.Description]
    );
    setValue(
      EAllocateEmployeeFormDetailVariables.ContinuousAllocation,
      baseValues[EAllocateEmployeeFormDetailVariables.ContinuousAllocation]
    );
    setValue(
      EAllocateEmployeeFormDetailVariables.TotalEfforts,
      baseValues[EAllocateEmployeeFormDetailVariables.TotalEfforts]
    );

    if (
      baseValues[EAllocateEmployeeFormDetailVariables.Allocations].length > 0
    ) {
      baseValues[EAllocateEmployeeFormDetailVariables.Allocations].forEach(
        (itemAllocation) => {
          append({
            [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
              new Date(
                itemAllocation[
                  EAllocationsBreakupVariables.ConfirmedAllocationStartDate
                ]
              ),
            [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]: new Date(
              itemAllocation[
                EAllocationsBreakupVariables.ConfirmedAllocationEndDate
              ]
            ),
            [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
              itemAllocation[EAllocationsBreakupVariables.ConfirmedPerDayHours],
            [EAllocationsBreakupVariables.PerDayAllocation]:
              itemAllocation[EAllocationsBreakupVariables.PerDayAllocation],
          });
        }
      );
    } else {
      append({
        [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]: props
          ?.selectedUserForAllocationModal?.requisition
          ? props?.selectedUserForAllocationModal?.requisition.startDate
          : props.baseStartEndDateToConsiderForDefaultAllocationEntry.startDate,
        [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]: props
          ?.selectedUserForAllocationModal?.requisition
          ? props?.selectedUserForAllocationModal?.requisition.endDate
          : props.baseStartEndDateToConsiderForDefaultAllocationEntry.endDate,
        [EAllocationsBreakupVariables.ConfirmedPerDayHours]: props
          ?.selectedUserForAllocationModal?.requisition
          ? props.selectedUserForAllocationModal.requisition.effortsPerDay
          : parseInt(
              props.baseStartEndDateToConsiderForDefaultAllocationEntry.noOfHours.toString()
            ),
        [EAllocationsBreakupVariables.PerDayAllocation]:
          props.baseStartEndDateToConsiderForDefaultAllocationEntry
            .isPerDayHourAllocation,
      });
    }
  };

  useEffect(() => {
    getMinMaxDatesForAllocation();
    autoPopulateData();
    const email = props?.selectedUserForAllocationModal?.email;
    getApprovedSkills([email]);
    if (!props.selectedUserForAllocationModal.isUpdateAllowed) {
      setIsWorkflowRunning(true);
      setIsViewMode(true);
      
    }
  }, []);

  const getApprovedSkills = async (
    email: Array<string>
  ): Promise<IAllocateFormSkills[]> => {
    return new Promise<IAllocateFormSkills[]>((resolve, reject) => {
      getApprovedSkillByEmail(email)
        .then((response: any) => {
          if (response) {
            const skillArray: IAllocateFormSkills[] = response.data?.map(
              (skill) => ({
                skillName: skill.skillName,
                skillCode: skill.skillCode,
                skill: skill.skill,
              })
            );
            const seenCodes = {}; // To track seen skillCodes
            const uniqueSkills = [];

            skillArray.forEach((skill) => {
              if (!seenCodes[skill.skillCode]) {
                seenCodes[skill.skillCode] = true;
                uniqueSkills.push(skill);
              }
            });
            setSkillsMaster(uniqueSkills);
            resolve(uniqueSkills);
          } else {
            resolve([]);
          }
        })
        .catch((e) => {
          snackbarContext.displaySnackbar("Something went Wrong.", "error");
        });
    });
  };

  const submitAllocationForm = (formData: FormValuesForAllocationForm) => {
    try {
      setIsFormDirty(false);
      props.submitAllocationForm(formData);
    } catch (err) {
      //
    }
  };

  const closeAllocationModelWithoutSubmit = () => {
    props.closeModel();
  };

  const appendEmptyAllocationEntry = () => {
    const previousAllocationEntry = getValues(
      EAllocateEmployeeFormDetailVariables.Allocations
    );
    if (previousAllocationEntry.length > 0) {
      const lastAddedEntry =
        previousAllocationEntry[previousAllocationEntry.length - 1];
      let startDate = null;
      if (
        lastAddedEntry[EAllocationsBreakupVariables.ConfirmedAllocationEndDate]
      ) {
        startDate = moment(
          lastAddedEntry[
            EAllocationsBreakupVariables.ConfirmedAllocationEndDate
          ]
        ).add(1, "day");
      }
      append(
        {
          [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
            startDate || ("" as any),
          [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
            props.baseStartEndDateToConsiderForDefaultAllocationEntry.endDate ||
            ("" as any),
          [EAllocationsBreakupVariables.ConfirmedPerDayHours]: 0,
          [EAllocationsBreakupVariables.PerDayAllocation]: false,
        },
        { shouldFocus: false }
      );
    }
  };

  const validateAndGetTotalEffortCount = async () => {
    const totalEffortCount = GetTotalEffort(
      getValues(EAllocateEmployeeFormDetailVariables.Allocations)
    );
    setValue(
      EAllocateEmployeeFormDetailVariables.TotalEfforts,
      totalEffortCount
    );
    trigger();
    return;
  };

  const changeContinuousAllocation = () => {
    if (getValues(EAllocateEmployeeFormDetailVariables.ContinuousAllocation)) {
      const allocations = getValues(
        EAllocateEmployeeFormDetailVariables.Allocations
      );
      remove();
      append(allocations[0]);
      validateAndGetTotalEffortCount();
    }
  };

  const checkUserAvailabilityForDatesAndEfforts = (
    startDate: Date,
    endDate: Date,
    confirmedPerDayHours: number,
    isPerDayHourAllocation: boolean
  ): Promise<IisAllocationHoursAvailableResponse[]> => {
    loaderContext.open(true);
    return new Promise((resolve, reject) => {
      CheckUserAvailabilityForDatesAndEffortsUtils(
        [props.selectedUserForAllocationModal.email],
        startDate,
        endDate,
        confirmedPerDayHours,
        isPerDayHourAllocation,
        props.selectedUserForAllocationModal?.requisitionId,
        props.projectInfo.pipelineCode,
        props.projectInfo?.jobCode
      )
        .then((resp) => {
          loaderContext.open(false);
          resolve(resp);
        })
        .catch((err) => {
          loaderContext.open(false);
          snackbarContext.displaySnackbar(
            "Error checking availability",
            SnackbarSeverity.ERROR
          );
        });
    });
  };

  const checkIfAddMoreAllocationBreakupButtonIsDisabled = () => {
    let isDisabled = isViewMode;
    const formData = getValues();
    if (
      formData[EAllocateEmployeeFormDetailVariables.Allocations] &&
      formData[EAllocateEmployeeFormDetailVariables.Allocations].length
    ) {
      const lastAllocationEntry =
        formData[EAllocateEmployeeFormDetailVariables.Allocations][
          formData[EAllocateEmployeeFormDetailVariables.Allocations].length - 1
        ];
      if (
        moment(
          GetNewDateWithNoonTimeZone(
            lastAllocationEntry[
              EAllocationsBreakupVariables.ConfirmedAllocationEndDate
            ]
          )
        ).isSameOrAfter(
          moment(
            GetNewDateWithNoonTimeZone(
              props.baseStartEndDateToConsiderForDefaultAllocationEntry.endDate
            )
          )
        )
      ) {
        isDisabled = true;
      }
    }
    return isDisabled;
  };

  return (
    <form onSubmit={handleSubmit(submitAllocationForm)}>
      {blocker.state === "blocked" && isFormDirty ? (
        <DialogBox
          showDialog={isFormDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <Grid container spacing={2}>
        <Grid
          item
          xs={12}
          sx={{
            ...FormSxPropsStyle.FontWeight600,
            ...FormSxPropsStyle.FontSizeMedium,
          }}
        >
          Allocate Employee:
          {" " + props.selectedUserForAllocationModal.userInfo.empName}
          {" ( " +
            getEmailId(props.selectedUserForAllocationModal.userInfo.email) +
            " )"}
        </Grid>
        <Grid item xs={12}>
          <ul className="form-error-container">
            {isWorkflowRunning && (
              <li className="error_msg">
                {GlobalConstant.message.error.workflow_running_msg}
              </li>
            )}
            {/* {isRequisitionDateExpire && (
              <li className="error_msg">
                {GlobalConstant.message.error.requisition_date_expire_msg}
              </li>
            )} */}
          </ul>
        </Grid>
        <Grid item xs={12}>
          {props?.selectedUserForAllocationModal?.requisition && (
            <RequisitionInfo
              requisition={props?.selectedUserForAllocationModal?.requisition}
            />
          )}
        </Grid>
        {props.selectedUserForAllocationModal.showDescription && (
          <React.Fragment>
            <Grid item xs={11.5}>
              <ControllerTextField
                name={EAllocateEmployeeFormDetailVariables.Description}
                control={control}
                defaultValue=""
                required={true}
                label={"Task Description"}
                error={
                  errors[EAllocateEmployeeFormDetailVariables.Description] &&
                  isSavePressed
                    ? true
                    : false
                }
                multiline={true}
                fullWidth={true}
                onChange={(e) => {}}
                disabled={isViewMode}
              />
            </Grid>
            <Grid item xs={0.5}>
              <span>
                <Tooltip
                  placement="right"
                  title={
                    "Description of tasks the resource is expected to perform on the job"
                  }
                >
                  <InfoIcon className="infoIconStyle" />
                </Tooltip>
              </span>
            </Grid>
          </React.Fragment>
        )}
        {props.selectedUserForAllocationModal.showSkills && (
          
          <Grid item xs={11.5}>
            <ControllerAutoCompleteChipsSimple
              name={EAllocateEmployeeFormDetailVariables.Skills}
              control={control}
              defaultValue={[]}
              required={true}
              multiple={true}
              filterSelectedOptions={true}
              disabled={
                isViewMode ||
                !props.selectedUserForAllocationModal?.isSkillUpdateAllowed
              }
              error={
                errors[EAllocateEmployeeFormDetailVariables.Skills] &&
                isSavePressed
                  ? true
                  : false
              }
              label={EAllocateEmployeeFormDetailVariables.Skills}
              onChange={(e) => {}}
              options={skillsMaster || []}
             
              getOptionLabel={(option) =>
                option ? option?.skillName : "Skills"
              }
              isOptionEqualToValue={(option, value) => {
                return (
                  option.skillName === value.skillName &&
                  option.skillCode === value.skillCode
                );
              }}
              sortBy={(data) => {
                return data?.sort((a, b) =>
                  `${a.skillName}` > `${b.skillName}` ? 1 : -1
                );
              }}
            />
          </Grid>
          // </React.Fragment>
        )}
        <Grid item xs={3}>
          <FormLabel component="legend">Continuous Allocation</FormLabel>
          <Controller
            name={EAllocateEmployeeFormDetailVariables.ContinuousAllocation}
            control={control}
            render={({ field, fieldState }) => (
              <RadioGroup
                row
                aria-label={
                  EAllocateEmployeeFormDetailVariables.ContinuousAllocation
                }
                {...field}
                onChange={(e) => {
                  setValue(
                    EAllocateEmployeeFormDetailVariables.ContinuousAllocation,
                    e.target.value === "Yes" ? true : false
                  );
                  trigger(
                    EAllocateEmployeeFormDetailVariables.ContinuousAllocation
                  );
                  changeContinuousAllocation();
                }}
              >
                <FormControlLabel
                  value={"Yes"}
                  control={<Radio />}
                  label="Yes"
                  disabled={isViewMode}
                  checked={getValues(
                    EAllocateEmployeeFormDetailVariables.ContinuousAllocation
                  )}
                />
                <FormControlLabel
                  value={"No"}
                  control={<Radio />}
                  label="No"
                  disabled={isViewMode}
                  checked={
                    getValues(
                      EAllocateEmployeeFormDetailVariables.ContinuousAllocation
                    ) === false
                  }
                />
              </RadioGroup>
            )}
          />
        </Grid>
        <Grid item xs={3.5}>
          <ControllerNumberTextField
            name={EAllocateEmployeeFormDetailVariables.TotalEfforts}
            control={control}
            defaultValue={0}
            label={"Total indicative effort hours"}
            error={
              errors[EAllocateEmployeeFormDetailVariables.TotalEfforts]
                ? true
                : false
            }
            disabled={true}
            onChange={(e) => {}}
            min={GlobalConstant.DEFAULT_ALLOCATION_HOUR.min}
          />
        </Grid>
      </Grid>
      <Grid container spacing={2} sx={AllocationBreakupSxProps}>
        <AllocationContextState>
          {fields.map((field, index) => {
            return (
              <AllocateBreakups
                key={field.id}
                index={index}
                control={control}
                errors={errors}
                isViewMode={isViewMode}
                getValues={getValues}
                append={append}
                prepend={prepend}
                remove={remove}
                trigger={trigger}
                validateAndGetTotalEffortCount={validateAndGetTotalEffortCount}
                baseDataForAllocationRestrictions={
                  baseDataForAllocationRestrictions
                }
                checkUserAvailabilityForDatesAndEfforts={
                  checkUserAvailabilityForDatesAndEfforts
                }
                setError={setError}
                selectedUserForAllocationModal={
                  props.selectedUserForAllocationModal
                }
                clearErrors={clearErrors}
                isAllocationBreakupDirty={isAllocationBreakupDirty}
                setIsAllocationBreakupDirty={setIsAllocationBreakupDirty}
                //projectInfo={props.projectInfo}
               // isUpdateAllowed= {props.selectedUserForAllocationModal.isUpdateAllowed}
              />
            );
          })}
        </AllocationContextState>
      </Grid>
      <Grid container spacing={2}>
        <Grid item xs={3}>
          {!getValues(
            EAllocateEmployeeFormDetailVariables.ContinuousAllocation
          ) && (
            <>
              <Button
                variant="text"
                disabled={checkIfAddMoreAllocationBreakupButtonIsDisabled()}
                className="btn labelColor"
                onClick={() => appendEmptyAllocationEntry()}
              >
                <AddCircleOutlinedIcon
                  fontSize="small"
                  sx={GlobalConstant.MenuIconSxProps}
                />
                Add more
              </Button>
              <span>
                <Tooltip
                  placement="right"
                  title={
                    "Allocation end date should be within the project timelines"
                  }
                >
                  <InfoIcon className="infoIconStyle" />
                </Tooltip>
              </span>
            </>
          )}
        </Grid>
        <Grid item xs={9} />
        <Grid item xs={3} />

        <Grid item xs={3}>
          <BackActionButton
            label={"Cancel"}
            onClick={function (e: any): void {
              closeAllocationModelWithoutSubmit();
            }}
          />
        </Grid>
        <Grid item xs={3}>
          <ActionButton
            label={"Ok"}
            onClick={function (e: any): void {
              setIsSavePressed(true);
              setIsAllocationBreakupDirty(true);
              // handleSubmit(submitAllocationForm);
            }}
            disabled={false}
            type={"submit"}
          />
        </Grid>
        <Grid item xs={3} />
      </Grid>
    </form>
  );
};
export default MainAllocationForm;
