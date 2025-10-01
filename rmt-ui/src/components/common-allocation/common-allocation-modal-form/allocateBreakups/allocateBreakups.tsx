import React, { useContext, useEffect, useState } from "react";
import Grid from "@mui/material/Grid";
import {
  Control,
  FieldErrors,
  UseFieldArrayAppend,
  UseFieldArrayPrepend,
  UseFieldArrayRemove,
  UseFormClearErrors,
  UseFormGetValues,
  UseFormSetError,
  UseFormTrigger,
} from "react-hook-form";
import ControllerCalendar from "../../../controllerInputs/controlerCalendar";
import {
  EAllocateEmployeeFormDetailVariables,
  EAllocationsBreakupVariables,
  EAvailabilityErrorMessage,
  FormValuesForAllocationBreakup,
  FormValuesForAllocationForm,
  IMinMaxDataForAllocationEntry,
  IisAllocationHoursAvailableResponse,
} from "../utils";
import ControllerNumberTextField from "../../../controllerInputs/controllerNumbeTextfield";
import * as GC from "../../../../global/constant";
import { Tooltip } from "@mui/material";
import DeleteRoundedIcon from "@mui/icons-material/DeleteRounded";
import { DeleteIconSxProps } from "../style";
import { VerticalCenterAlignSxProps } from "../../../scheduler/react-time-calendar/constant";
import {
  IAllUserAllocationEntries,
  IBaseCommonAllocationFormDetails,
} from "../../interface";
import {
  AllocationContextProps,
  AllocationContext,
} from "../allocationContext/allocationContext";
import ControllerCheckbox from "../../../controllerInputs/controllerCheckbox";
import PriorityHighIcon from "@mui/icons-material/PriorityHigh";
import {
  GetNewDateWithNoonTimeZone,
  isValidDate,
} from "../../../../utils/date/dateHelper";
import moment from "moment";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../../contexts/loaderContext";
import InfoIcon from "@mui/icons-material/Info";
import { EAllocationType } from "../../enum";

export interface IAllocateBreakups {
  control: Control<FormValuesForAllocationForm>;
  selectedUserForAllocationModal: IAllUserAllocationEntries;
  index: number;
  errors: FieldErrors;
  isViewMode: boolean;
  getValues: UseFormGetValues<FormValuesForAllocationForm>;
  append: UseFieldArrayAppend<
    FormValuesForAllocationForm,
    EAllocateEmployeeFormDetailVariables.Allocations
  >;
  prepend: UseFieldArrayPrepend<
    FormValuesForAllocationForm,
    EAllocateEmployeeFormDetailVariables.Allocations
  >;
  remove: UseFieldArrayRemove;
  trigger: UseFormTrigger<FormValuesForAllocationForm>;
  validateAndGetTotalEffortCount: () => Promise<void>;
  setError: UseFormSetError<FormValuesForAllocationForm>;
  clearErrors: UseFormClearErrors<FormValuesForAllocationForm>;
  baseDataForAllocationRestrictions: IBaseCommonAllocationFormDetails;
  // validateTotalEffort: () => boolean;
  checkUserAvailabilityForDatesAndEfforts: (
    startDate: Date,
    endDate: Date,
    confirmedPerDayHours: number,
    isPerDayHourAllocation: boolean
  ) => Promise<IisAllocationHoursAvailableResponse[]>;
  isAllocationBreakupDirty: boolean;
  setIsAllocationBreakupDirty: React.Dispatch<React.SetStateAction<boolean>>;
}

const AllocateBreakups = (props: IAllocateBreakups) => {
  const { control, index, errors, isViewMode, getValues } = props;
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const allocateContext: AllocationContextProps = useContext(AllocationContext);
  const [isValidate, setIsValidate] = useState(true);
  const [baseMinMaxData, setMinMaxData] =
    useState<IMinMaxDataForAllocationEntry>({
      startDate: {
        min: GetNewDateWithNoonTimeZone(),
        max: GetNewDateWithNoonTimeZone(),
      },
      endDate: {
        min: GetNewDateWithNoonTimeZone(),
        max: GetNewDateWithNoonTimeZone(),
      },
      efforts: {
        min: 1,
        max: 8,
      },
    });
  const [customErrorMessage, setCustomErrorMessage] = useState<string | any>(
    ""
  );
  const [isUpdateDisabledForField, setIsUpdateDisabledForField] =
    useState<boolean>(false);
  const getControlName = (type: string) => {
    return `${EAllocateEmployeeFormDetailVariables.Allocations}.${index}.${type}`;
  };
  useEffect(() => {
    allocationValuesChanged();
    checkIfDateCanBeChangedForUpdateAllocation();
  }, []);

  const setHelperErrorMessage = (message: string) => {
    if (!message) {
      return "";
    } else {
      setCustomErrorMessage(message);
    }
  };

  const getMinMaxDateForSpecificAllocationEntry = (
    allocationEntries: FormValuesForAllocationBreakup[],
    index: number
  ): IMinMaxDataForAllocationEntry => {
    let tempValues = baseMinMaxData;
    if (index === 0) {
      tempValues = {
        startDate: {
          min: props.baseDataForAllocationRestrictions.startDate,
          max: props.baseDataForAllocationRestrictions.endDate,
        },
        endDate: {
          min: allocationEntries[index][
            EAllocationsBreakupVariables.ConfirmedAllocationStartDate
          ],
          max: props.baseDataForAllocationRestrictions.endDate,
        },
        efforts: {
          min: GC.DEFAULT_ALLOCATION_HOUR.min,
          max: allocationEntries[index][
            EAllocationsBreakupVariables.PerDayAllocation
          ]
            ? GC.DEFAULT_ALLOCATION_HOUR.max
            : undefined,
        },
      };
    } else {
      const allocationEntryOfPrevField = allocationEntries[index - 1];
      let prevDate = GetNewDateWithNoonTimeZone(
        allocationEntryOfPrevField[
          EAllocationsBreakupVariables.ConfirmedAllocationEndDate
        ]
      );
      tempValues = {
        startDate: {
          min: GetNewDateWithNoonTimeZone(
            prevDate.setDate(prevDate.getDate() + 1)
          ),
          max: props.baseDataForAllocationRestrictions.endDate,
        },
        endDate: {
          min: allocationEntries[index][
            EAllocationsBreakupVariables.ConfirmedAllocationStartDate
          ],
          max: props.baseDataForAllocationRestrictions.endDate,
        },
        efforts: {
          min: GC.DEFAULT_ALLOCATION_HOUR.min,
          max: allocationEntries[index][
            EAllocationsBreakupVariables.PerDayAllocation
          ]
            ? GC.DEFAULT_ALLOCATION_HOUR.max
            : undefined,
        },
      };
    }
    setMinMaxData((prev) => tempValues);
    return tempValues;
  };

  const allocationValuesChanged = async (isDeletedEntry: boolean = false) => {
    loaderContext.open(true);
    await props.validateAndGetTotalEffortCount();
    props.trigger();

    if (!isDeletedEntry) {
      const baseData = getMinMaxDateForSpecificAllocationEntry(
        getValues(EAllocateEmployeeFormDetailVariables.Allocations),
        index
      );
      await isValidateEfforts(baseData);
    }
    allocateContext.isUpdated(!allocateContext.updated);
    loaderContext.open(false);
  };

  const checkForAvailability = async (): Promise<string> => {
    const currentData = props.getValues(
      EAllocateEmployeeFormDetailVariables.Allocations
    )[index];
    const allocationUnitChanged = {
      startDate:
        currentData[EAllocationsBreakupVariables.ConfirmedAllocationStartDate],
      endDate:
        currentData[EAllocationsBreakupVariables.ConfirmedAllocationEndDate],
      confirmedPerDayHours:
        currentData[EAllocationsBreakupVariables.ConfirmedPerDayHours],
      isPerDayHourAllocation:
        currentData[EAllocationsBreakupVariables.PerDayAllocation],
    };
    const errorIfAny = await props.checkUserAvailabilityForDatesAndEfforts(
      allocationUnitChanged.startDate,
      allocationUnitChanged.endDate,
      allocationUnitChanged.confirmedPerDayHours,
      allocationUnitChanged.isPerDayHourAllocation
    );
    return errorIfAny.length > 0 &&
      (!errorIfAny[0].isHoursAvialable || errorIfAny[0].errorMsg)
      ? errorIfAny[0].errorMsg || EAvailabilityErrorMessage.NotAvailable
      : "";
  };

  useEffect(() => {
    getMinMaxDateForSpecificAllocationEntry(
      getValues(EAllocateEmployeeFormDetailVariables.Allocations),
      index
    );
  }, [allocateContext.updated]);

  const getAnyErrorForPerDay = () => {
    try {
      return errors[EAllocateEmployeeFormDetailVariables.Allocations][index][
        EAllocationsBreakupVariables.ConfirmedPerDayHours
      ];
    } catch (err) {
      return false;
    }
  };

  const getAnyErrorForEndDate = () => {
    try {
      return errors[EAllocateEmployeeFormDetailVariables.Allocations][index][
        EAllocationsBreakupVariables.ConfirmedAllocationEndDate
      ];
    } catch (err) {
      return false;
    }
  };

  const getAnyErrorForStartDate = () => {
    try {
      return errors[EAllocateEmployeeFormDetailVariables.Allocations][index][
        EAllocationsBreakupVariables.ConfirmedAllocationStartDate
      ];
    } catch (err) {
      return false;
    }
  };

  const isValidateEfforts = async (baseData: IMinMaxDataForAllocationEntry) => {
    loaderContext.open(true);
    const value = props.getValues(
      getControlName(EAllocationsBreakupVariables.ConfirmedPerDayHours) as any
    );
    const isEffortInRangeOfTotalEfforts = baseData?.efforts?.max
      ? value >= baseData.efforts.min && value <= baseData?.efforts?.max
      : value >= baseData.efforts.min;
    if (!isEffortInRangeOfTotalEfforts) {
      setCustomErrorMessage("");
      setIsValidate(false);
      props.setError(`Allocations.${index}.confirmedPerDayHours`, {
        type: "validate",
      });
    } else {
      const availabilityError = await checkForAvailability();
      if (availabilityError) {
        setHelperErrorMessage(availabilityError);
        setIsValidate(false);
        props.setError(`Allocations.${index}.confirmedPerDayHours`, {
          type: "validate",
        });
      } else {
        setCustomErrorMessage("");
        setIsValidate(true);
        props.clearErrors(`Allocations.${index}.confirmedPerDayHours`);
      }
    }
    setTimeout(() => {
      props.trigger(`Allocations.${index}.confirmedPerDayHours`);
      loaderContext.open(false);
    }, 30);
  };

  const checkIfDateCanBeChangedForUpdateAllocation = (): void => {
    const startDate = props.getValues(
      getControlName(
        EAllocationsBreakupVariables.ConfirmedAllocationStartDate
      ) as any
    );

    const endDate = props.getValues(
      getControlName(
        EAllocationsBreakupVariables.ConfirmedAllocationEndDate
      ) as any
    );

    if (
      props.selectedUserForAllocationModal.type ===
      EAllocationType.UPDATE_ALLOCATION
    ) {
      if (
        moment(GetNewDateWithNoonTimeZone(startDate)).isBefore(
          moment(GetNewDateWithNoonTimeZone())
        ) &&
        moment(GetNewDateWithNoonTimeZone(endDate)).isBefore(
          moment(GetNewDateWithNoonTimeZone())
        )
      ) {
        setIsUpdateDisabledForField(true);
        return;
      }
    }
    setIsUpdateDisabledForField(false);
  };

  return (
    <Grid item xs={12} key={index}>
      <Grid container spacing={2}>
        <Grid item xs={3}>
          <ControllerCalendar
            name={getControlName(
              EAllocationsBreakupVariables.ConfirmedAllocationStartDate
            )}
            shouldDisableWeekends={true}
            control={control}
            required={true}
            label={"Start Date"}
            defaultValue={""}
            error={
              getAnyErrorForStartDate() && props.isAllocationBreakupDirty
                ? true
                : false
            }
            disabled={isViewMode || isUpdateDisabledForField}
            onChange={(date: any) => {
              props.setIsAllocationBreakupDirty(true);
              if (isValidDate(date)) {
                allocationValuesChanged();
              }
            }}
            minDate={new Date(baseMinMaxData.startDate.min)}
            maxDate={new Date(baseMinMaxData.startDate.max)}
            ignoreTimeZone={true}
            validate={async (value) => {
              const changedDate = GetNewDateWithNoonTimeZone(value);
              const today = GetNewDateWithNoonTimeZone();
              if (!isUpdateDisabledForField && moment(changedDate).isBefore(moment(today), "day")) {
                return false;
              }
              return (
                moment(changedDate).isSameOrAfter(
                  new Date(baseMinMaxData.startDate.min)
                ) &&
                moment(changedDate).isSameOrBefore(
                  new Date(baseMinMaxData.startDate.max)
                ) 
                // &&
                // changedDate.getDay() !== 0 &&
                // changedDate.getDay() !== 6
              );
            }}
          />
        </Grid>
        <Grid item xs={3}>
          <ControllerCalendar
            name={getControlName(
              EAllocationsBreakupVariables.ConfirmedAllocationEndDate
            )}
            control={control}
            required={true}
            shouldDisableWeekends={true}
            label={"End Date"}
            defaultValue={""}
            error={
              getAnyErrorForEndDate() && props.isAllocationBreakupDirty
                ? true
                : false
            }
            disabled={isViewMode || isUpdateDisabledForField}
            onChange={(date: any) => {
              props.setIsAllocationBreakupDirty(true);
              if (isValidDate(date)) {
                allocationValuesChanged();
              }
            }}
            minDate={new Date(baseMinMaxData.endDate.min)}
            maxDate={new Date(baseMinMaxData.endDate.max)}
            ignoreTimeZone={true}
            validate={async (value) => {
              const changedDate = GetNewDateWithNoonTimeZone(value); 
              const today = GetNewDateWithNoonTimeZone();
              if (!isUpdateDisabledForField && moment(changedDate).isBefore(moment(today), "day")) {
                return false;
              }          
              return (
                moment(changedDate).isSameOrAfter(
                  new Date(baseMinMaxData.startDate.min)
                ) &&
                moment(changedDate).isSameOrBefore(
                  new Date(baseMinMaxData.startDate.max)
                ) &&
                changedDate.getDay() !== 0 &&
                changedDate.getDay() !== 6
              );
            }}
          />
        </Grid>
        <Grid item xs={3} sx={{ display: "flex" }}>
          <ControllerNumberTextField
            name={getControlName(
              EAllocationsBreakupVariables.ConfirmedPerDayHours
            )}
            control={control}
            required={true}
            label={"Efforts(hr)"}
            defaultValue={""}
            min={baseMinMaxData.efforts.min}
            max={baseMinMaxData.efforts.max}
            error={
              getAnyErrorForPerDay() && props.isAllocationBreakupDirty
                ? true
                : false
            }
            disabled={isViewMode || isUpdateDisabledForField}
            validate={(e) => {
              return isValidate;
            }}
            onChange={(e: any) => {
              props.setIsAllocationBreakupDirty(true);
              allocationValuesChanged();
            }}
          />
          {customErrorMessage && props.isAllocationBreakupDirty && (
            <Tooltip title={customErrorMessage} placement="top">
              <PriorityHighIcon fontSize="small" color="error" />
            </Tooltip>
          )}
        </Grid>
        <Grid item xs={2.1} sx={VerticalCenterAlignSxProps}>
          <ControllerCheckbox
            name={
              getControlName(
                EAllocationsBreakupVariables.PerDayAllocation
              ) as any
            }
            defaultValue={true}
            control={control}
            disabled={isViewMode || isUpdateDisabledForField}
            label="Hours/Day"
            onChange={(e) => {
              props.setIsAllocationBreakupDirty(true);
              allocationValuesChanged();
            }}
          />
          {index === 0 && (
            <Tooltip
              placement="right"
              title={
                "Effort(hr) indicates hours required from the resource in the defined period i.e. *8 entered would be 8 hrs in the entire period. By checking Hours/Day, it defines the number of hours a resource is required for each day in the defined period. For E.g. now 8 hrs would mean 8 hrs per day. "
              }
            >
              <InfoIcon className="infoIconStyle" />
            </Tooltip>
          )}
        </Grid>
        <Grid item xs={0.8}>
          {getValues(EAllocateEmployeeFormDetailVariables.Allocations).length >
            1 &&
            !isViewMode &&
            !isUpdateDisabledForField && (
              <Tooltip title="Remove">
                <DeleteRoundedIcon
                  sx={DeleteIconSxProps}
                  fontSize="small"
                  onClick={() => {
                    props.remove(index);
                    props.setIsAllocationBreakupDirty(true);
                    allocationValuesChanged(true);
                    props.trigger();
                  }}
                />
              </Tooltip>
            )}
        </Grid>
      </Grid>
    </Grid>
  );
};
export default AllocateBreakups;
