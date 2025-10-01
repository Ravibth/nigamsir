/* eslint-disable react-hooks/exhaustive-deps */
import React, { memo, useContext, useEffect, useState } from "react";
import {
  BaseCommonAllocationFormDetailsValues,
  IAllUserAllocationEntries,
  IBaseCommonAllocationFormDetails,
  ICommonAllocationMainProps,
  IUserTimeline,
} from "../interface";
import { EAllocationType, EBaseCommonAllocationMainControlForm } from "../enum";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarMessages,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import {
  CheckIfUsersAllocationIsUpdatedDifferentlyFromApplyToAll,
  GetAllAllocationsByJobCodesForSameTeam,
  GetSameTeamUserAllocationEntries,
  UpdateAllUserAllocationsAccordingToBaseInfosUtils,
  UpdateBaseCommonFormDetailsUtils,
  convertBulkAllocationDataToCommonScreenPayload,
  fetchUsersTimelineForMultipleDates,
  getSystemSuggestedUsersAllocationEntries,
  makeUserTimelineGroupsAndItems,
  UpdateUserTimelinesGroupAndItems,
  getAllocationsToUpdateUsersAllocationEntries,
  getPreSelectedUsersForNameAllocationEntries,
} from "../utils";
import TimelineView from "../timeline-view/timeline-view";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../../contexts/loaderContext";
import { Grid, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { AlignGridCenterSxProps } from "../style";
import CommonAllocationModalForm from "../common-allocation-modal-form/common-allocation-modal-form";
import "../timeline-view/style.css";
import {
  IUserSelectedAllocationContext,
  UserSelectedAllocationContext,
} from "../context/users-selected-allocation";
import {
  CheckUserAvailabilityForDatesAndEffortsUtils,
  EAllocationsBreakupVariables,
  FormValuesForAllocationBreakup,
} from "../common-allocation-modal-form/utils";
import CommonAllocationBaseInfo from "./commonAllocationBaseInfo/commonAllocationBaseInfo";
import ConfirmationDialog from "../../../common/confirmation-dialog/confirmation-dialog";
import DialogBox from "../../../hooks/UnsavedChangesHook/DialogBoxComponent/DialogBoxComponent";
import { GetAllJobCodesForPipelineCodeService } from "../../../services/project-list-services/project-list-services";
import { GetUsersTimelinePayload } from "../../../global/service-constant";
import { IResourceAllocationDetailsMaster } from "../../../common/interfaces/IAllocation";
import {
  getDateInMomentFormat,
  getDateWithEndHours,
  GetNewDateWithNoonTimeZone,
  MomentToJSDate,
} from "../../../utils/date/dateHelper";
import useBlockRefreshAndBack from "../../../hooks/UnsavedChangesHook/useBlockRefreshAndBack";
import useBlockerCustom from "../../../hooks/UnsavedChangesHook/useBlockerCustom";
import { checkIfPrevAllocIsSameAsNewAllocation } from "../user-info-timeline-group/utils";

enum EConfirmationDialogType {
  RemoveUserEntry = "RemoveUserEntry",
  ApplyBaseTimelineToAllUsers = "ApplyBaseTimelineToAllUsers",
}

const CommonAllocationMain = (props: ICommonAllocationMainProps) => {
  const {
    control,
    setValue,
    getValues,
    trigger,
    formState: { errors, isDirty },
  } = useForm<IBaseCommonAllocationFormDetails>({
    mode: "onTouched",
    defaultValues: {
      [EBaseCommonAllocationMainControlForm.startDate]:
        GetNewDateWithNoonTimeZone(),
      [EBaseCommonAllocationMainControlForm.endDate]:
        GetNewDateWithNoonTimeZone(),
      [EBaseCommonAllocationMainControlForm.noOfHours]: 1,
      [EBaseCommonAllocationMainControlForm.isRequisition]: false,
      [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]: false,
    },
  });
  const [isFormDirty, setIsFormDirty] = useState<boolean>(false);

  useEffect(() => {
    setIsFormDirty(isDirty);
  }, [isDirty]);

  useBlockRefreshAndBack(isFormDirty);
  let { blocker, handleCancel, handleConfirm } = useBlockerCustom(isFormDirty);

  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);
  const usersSelectedAllocationContext: IUserSelectedAllocationContext =
    useContext(UserSelectedAllocationContext);
  const [baseFormDetails, setBaseFormDetails] =
    useState<IBaseCommonAllocationFormDetails>(
      BaseCommonAllocationFormDetailsValues
    );
  const [baseFormDetailsForTimelineView, setBaseFormDetailsForTimelineView] =
    useState<IBaseCommonAllocationFormDetails>(
      BaseCommonAllocationFormDetailsValues
    );
  const [allUserAllocationEntries, setAllUserAllocationEntries] = useState<
    IAllUserAllocationEntries[]
  >([]);
  const [selectedUserForAllocationModal, setSelectedUserForAllocationModal] =
    useState<IAllUserAllocationEntries | null>(null);
  const [allUsersSelected, setAllUsersSelected] = useState<boolean>(false);
  const [userTimelines, setUserTimelines] = useState<IUserTimeline>({
    timelineGroups: [],
    timelineItems: [],
  });
  const [listOfJobCodes, setListOfJobCodes] = useState<string[]>([]);

  const [openAllocationModal, setOpenAllocationModal] =
    useState<boolean>(false);
  const [applyDatesToAllAllocation, setApplyDatesToAllAllocation] =
    useState<boolean>(false);
  const [openConfirmation, setOpenConfirmation] = useState<string>("");
  const [openBackPromptDialog, setOpenBackPromptDialog] =
    useState<boolean>(false);

  const addSystemSuggestedUsersToTimeline = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      const userAllocationEntries = getSystemSuggestedUsersAllocationEntries({
        requisition: props.requisition,
        suggestionsSelected: props.suggestionsSelected,
        projectInfo: props.projectInfo,
      });
      Promise.all([
        fetchAndAddUserTimelines(
          props.suggestionsSelected.map((suggestion) => suggestion.email),
          userAllocationEntries
        ),
      ])
        .then(() => {
          resolve(true);
        })
        .catch(() => {
          reject(false);
        });
    });
  };
  const addAllocationsToUpdateUsersToTimeline = (): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        const userAllocationEntries =
          await getAllocationsToUpdateUsersAllocationEntries(
            props.allocationsToUpdate,
            props.projectInfo
          );
        Promise.all([
          fetchAndAddUserTimelines(
            props.allocationsToUpdate.map(
              (allocation: IResourceAllocationDetailsMaster) =>
                allocation.empEmail
            ),
            userAllocationEntries
          ),
        ])
          .then(() => {
            resolve(true);
          })
          .catch(() => {
            reject(false);
          });
      } catch (err) {}
    });
  };

  const updateUserEntriesOnJobCodeChangeSameTeam = (
    selectedJobCodeIfAny: string
  ) => {
    if (selectedJobCodeIfAny) {
      const userEntriesOtherThanSameTeam = allUserAllocationEntries.filter(
        (item) => item.type !== EAllocationType.SAME_TEAM_ALLOCATION
      );
      loaderContext.open(true);
      setAllUserAllocationEntries((prev) => [...userEntriesOtherThanSameTeam]);
      Promise.all([
        addSameTeamUsersToTimeline(
          [selectedJobCodeIfAny],
          baseFormDetails,
          true,
          userEntriesOtherThanSameTeam
        ),
      ])
        .then((resp) => {
          loaderContext.open(false);
        })
        .catch((err) => {
          loaderContext.open(false);
        });
    }
  };

  const addSameTeamUsersToTimeline = (
    jobCodes: string[],
    tempBaseDetails: IBaseCommonAllocationFormDetails,
    useTemporaryAllocationUserEntries: boolean,
    entries: IAllUserAllocationEntries[]
  ): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        const allSameTeamUserAllocations =
          await GetAllAllocationsByJobCodesForSameTeam(jobCodes);

        const uniqueItems = {};
        // Use filter to get distinct items based on the 'empEmail' property
        const distinctItems = allSameTeamUserAllocations.filter((item) => {
          if (!uniqueItems[item.empEmail]) {
            uniqueItems[item.empEmail] = true;
            return true;
          }
          return false;
        });
        if (distinctItems.length > 0) {
          const userAllocationEntries = await GetSameTeamUserAllocationEntries(
            distinctItems,
            tempBaseDetails,
            props.projectInfo
          );
          await fetchAndAddUserTimelines(
            userAllocationEntries.map((user) => user.email),
            userAllocationEntries,
            useTemporaryAllocationUserEntries,
            entries
          );
        }
        resolve(true);
      } catch (err) {
        snackbarContext.displaySnackbar(
          "Error fetching users for same team",
          SnackbarSeverity.ERROR
        );
        reject(err);
      }
    });
  };

  const updateTimelineDatesOnUserAllocationFormAction = (
    finalEntries: IAllUserAllocationEntries[]
  ) => {
    const minMaxStartEndDatesForAllUsers = finalEntries.map((entry) =>
      getMinMaxDatesFromUserAllocationEntry(entry.allocations)
    );

    const finalMinMax = {
      min: new Date(
        Math.min.apply(
          null,
          minMaxStartEndDatesForAllUsers.map((item) => item.min)
        )
      ),
      max: new Date(
        Math.max.apply(
          null,
          minMaxStartEndDatesForAllUsers.map((item) => item.max)
        )
      ),
    };

    const tempTimelineDates: IBaseCommonAllocationFormDetails = {
      [EBaseCommonAllocationMainControlForm.startDate]: MomentToJSDate(
        getDateInMomentFormat(finalMinMax.min)
      ),
      [EBaseCommonAllocationMainControlForm.endDate]: getDateWithEndHours(
        finalMinMax.max
      ).toDate(),
      [EBaseCommonAllocationMainControlForm.noOfHours]: 1,
      [EBaseCommonAllocationMainControlForm.isRequisition]: false,
      [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]: true,
    };

    setBaseFormDetailsForTimelineView(tempTimelineDates);
  };

  const addPreSelectedUsersForNameAllocationEntries = (): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        if (
          props.baseUserEmailToSelect &&
          props.baseUserEmailToSelect.length > 0
        ) {
          const userAllocationEntries =
            await getPreSelectedUsersForNameAllocationEntries(
              props.baseUserEmailToSelect,
              props.projectInfo
            );
          setSelectedUserForAllocationModal(userAllocationEntries[0]);
          setOpenAllocationModal(true);
        }
        resolve(true);
      } catch (err) {
        snackbarContext.displaySnackbar(
          "Something went wrong!, Please try again later.",
          SnackbarSeverity.ERROR
        );
        resolve(true);
      }
    });
  };

  const addBulkUploadedAllocationsToTimeline = (): Promise<boolean> => {
    return new Promise<boolean>(async (resolve, reject) => {
      try {
        if (
          props.bulkAllocationsUploaded &&
          props.bulkAllocationsUploaded.length > 0
        ) {
          const userAllocationEntries =
            await convertBulkAllocationDataToCommonScreenPayload(
              props.bulkAllocationsUploaded,
              baseFormDetails,
              props.projectInfo
            );
          if (userAllocationEntries && userAllocationEntries.length > 0) {
            await fetchAndAddUserTimelines(
              userAllocationEntries.map((user) => user.email),
              userAllocationEntries
            );
          }
        }
        resolve(true);
      } catch (err) {
        snackbarContext.displaySnackbar(
          "Error fetching user timelines",
          SnackbarSeverity.ERROR
        );
        resolve(true);
      }
    });
  };

  const addNameAllocationUsersToTimeline = async (
    newlyAddedUserAllocationEntries: IAllUserAllocationEntries[]
  ) => {
    setIsFormDirty(true);
    loaderContext.open(true);
    RemoveSelectAllCheckBoxWhenNewUserIsAddedByNameAllocation();
    checkIfApplyToAllConditionIsUpdated(newlyAddedUserAllocationEntries);
    await Promise.all([
      fetchAndAddUserTimelines(
        newlyAddedUserAllocationEntries.map((userItem) => userItem.email),
        newlyAddedUserAllocationEntries
      ),
    ])
      .then(() => {
        loaderContext.open(false);
      })
      .catch(() => {
        loaderContext.open(false);
        snackbarContext.displaySnackbar(
          "Error updating availabilities",
          SnackbarSeverity.ERROR
        );
      });
  };

  const RemoveSelectAllCheckBoxWhenNewUserIsAddedByNameAllocation = () => {
    setAllUsersSelected(false);
  };

  const checkIfApplyToAllConditionIsUpdated = (
    allocationItems: IAllUserAllocationEntries[]
  ) => {
    if (props.baseType !== EAllocationType.SYSTEM_SUGGESTED_ALLOCATION) {
      const isAllocationValuesUpdated =
        CheckIfUsersAllocationIsUpdatedDifferentlyFromApplyToAll(
          allocationItems,
          baseFormDetails
        );
      if (isAllocationValuesUpdated) {
        setValue(EBaseCommonAllocationMainControlForm.applyToAll, false);
        setApplyDatesToAllAllocation(false);
      }
    }
  };

  const fetchAndAddUserTimelines = (
    newlyAddedUsersEmails: string[],
    newlyAddedUserAllocationEntries: IAllUserAllocationEntries[],
    useTemporaryAllocationUserEntries = false,
    entries?: IAllUserAllocationEntries[]
  ): Promise<boolean> => {
    let tempTimelines = userTimelines;
    let tempUserAllocationEntries: IAllUserAllocationEntries[] = [];
    if (useTemporaryAllocationUserEntries && entries) {
      tempUserAllocationEntries = [
        ...entries,
        ...newlyAddedUserAllocationEntries,
      ];

      var timelineGroups = userTimelines.timelineGroups.filter(
        (item) => item.type !== EAllocationType.SAME_TEAM_ALLOCATION
      );
      var timelineItems = userTimelines.timelineItems.filter(
        (item) => item.type !== EAllocationType.SAME_TEAM_ALLOCATION
      );
      tempTimelines = {
        timelineGroups: timelineGroups,
        timelineItems: timelineItems,
      };
    } else {
      tempUserAllocationEntries = [
        ...allUserAllocationEntries,
        ...newlyAddedUserAllocationEntries,
      ];
    }

    setAllUserAllocationEntries((prev) => tempUserAllocationEntries);
    updateTimelineDatesOnUserAllocationFormAction(tempUserAllocationEntries);

    const fetchTimelineBody: GetUsersTimelinePayload[] = [];
    newlyAddedUsersEmails.forEach((userEmail) => {
      const userEntry = tempUserAllocationEntries.find(
        (item) =>
          item.email.toLowerCase().trim() === userEmail.toLowerCase().trim()
      );
      if (userEntry && userEntry.allocations.length) {
        userEntry.allocations.forEach((userEntryItem) => {
          fetchTimelineBody.push({
            emails: [userEntry.email],
            start_date:
              userEntryItem[
                EAllocationsBreakupVariables.ConfirmedAllocationStartDate
              ],
            end_date:
              userEntryItem[
                EAllocationsBreakupVariables.ConfirmedAllocationEndDate
              ],
          });
        });
      } else {
        fetchTimelineBody.push({
          emails: [userEmail],
          start_date: GetNewDateWithNoonTimeZone(props.projectInfo.startDate),
          end_date: GetNewDateWithNoonTimeZone(props.projectInfo.endDate),
        });
      }
    });

    return new Promise<boolean>((resolve, reject) => {
      Promise.all([fetchUsersTimelineForMultipleDates(fetchTimelineBody)])
        .then((response) => {
          const tempTimeline = makeUserTimelineGroupsAndItems(
            newlyAddedUserAllocationEntries,
            response[0],
            tempTimelines
          );
          setUserTimelines(tempTimeline);
          resolve(true);
        })
        .catch((error) => {
          snackbarContext.displaySnackbar(
            SnackbarMessages.ErrorFetchingUserTimelines,
            SnackbarSeverity.ERROR,
            6000
          );
          reject(false);
        });
    });
  };

  const getMinMaxDatesFromUserAllocationEntry = (
    entry: FormValuesForAllocationBreakup[]
  ): { min: Date; max: Date } => {
    const startDates = entry.map(
      (obj) =>
        new Date(obj[EAllocationsBreakupVariables.ConfirmedAllocationStartDate])
    );
    const endDates = entry.map(
      (obj) =>
        new Date(obj[EAllocationsBreakupVariables.ConfirmedAllocationEndDate])
    );
    return {
      min: new Date(Math.min.apply(null, startDates)),
      max: new Date(Math.max.apply(null, endDates)),
    };
  };

  const updateUserTimelineOnAllocationUpdate = (
    userEntry: IAllUserAllocationEntries
  ): Promise<boolean> => {
    const fetchTimelineBody: GetUsersTimelinePayload[] =
      userEntry.allocations.map((item) => {
        return {
          emails: [userEntry.email],
          start_date:
            item[EAllocationsBreakupVariables.ConfirmedAllocationStartDate],
          end_date:
            item[EAllocationsBreakupVariables.ConfirmedAllocationEndDate],
        };
      });
    return new Promise<boolean>((resolve, reject) => {
      Promise.all([fetchUsersTimelineForMultipleDates(fetchTimelineBody)])
        .then((response) => {
          const tempUpdatedTimeline = UpdateUserTimelinesGroupAndItems(
            userEntry,
            response[0],
            userTimelines
          );
          setUserTimelines(tempUpdatedTimeline);
          resolve(true);
        })
        .catch((error) => {
          snackbarContext.displaySnackbar(
            "User entry updated, but unable to update user on screen timeline",
            SnackbarSeverity.ERROR
          );
          reject(false);
        });
    });
  };

  const updateBaseCommonFormDetails = (): IBaseCommonAllocationFormDetails => {
    let tempDetails: IBaseCommonAllocationFormDetails =
      UpdateBaseCommonFormDetailsUtils(props, baseFormDetails);
    setBaseFormDetails(tempDetails);
    if (
      props.baseType === EAllocationType.UPDATE_ALLOCATION &&
      props.allocationsToUpdate.length > 0 &&
      props.allocationsToUpdate[0] &&
      props.allocationsToUpdate[0].startDate &&
      props.allocationsToUpdate[0].endDate
    ) {
      let timelineDetails: IBaseCommonAllocationFormDetails = {
        [EBaseCommonAllocationMainControlForm.startDate]: MomentToJSDate(
          getDateInMomentFormat(props.allocationsToUpdate[0].startDate)
        ),
        [EBaseCommonAllocationMainControlForm.endDate]: getDateWithEndHours(
          props.allocationsToUpdate[0].endDate
        ).toDate(),
        [EBaseCommonAllocationMainControlForm.noOfHours]: 1,
        [EBaseCommonAllocationMainControlForm.isRequisition]: false,
        [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]: true,
      };
      setBaseFormDetailsForTimelineView(timelineDetails);
    } else {
      setBaseFormDetailsForTimelineView(tempDetails);
    }
    Object.keys(tempDetails).forEach((key: any) => {
      setValue(key, tempDetails[key]);
    });
    return tempDetails;
  };

  const openAllocationModalScreenForItemId = (itemId: number): void => {
    const selectedUserAllocationEntry =
      getUserAllAllocationEntriesByTimelineItemId(itemId);
    setSelectedUserForAllocationModal(selectedUserAllocationEntry);
    setOpenAllocationModal(true);
  };

  const getUserAllAllocationEntriesByTimelineItemId = (
    itemId: number
  ): IAllUserAllocationEntries => {
    const allocationEntries = userTimelines.timelineItems.find(
      (item) => item.id === itemId
    );
    return allUserAllocationEntries.find(
      (entry) =>
        entry.email.toLowerCase().trim() ===
        allocationEntries.user.email.toLowerCase().trim()
    );
  };

  const updateAllocationForEmployee = async (
    updatedEntry: IAllUserAllocationEntries
  ) => {
    checkIfApplyToAllConditionIsUpdated([updatedEntry]);
    const temporaryAllocationEntries = allUserAllocationEntries.map((entry) => {
      if (
        entry.email.toLowerCase().trim() ===
        updatedEntry.email.toLowerCase().trim()
      ) {
        return updatedEntry;
      } else {
        return entry;
      }
    });
    setAllUserAllocationEntries((prev) => temporaryAllocationEntries);
    updateTimelineDatesOnUserAllocationFormAction(temporaryAllocationEntries);
    loaderContext.open(true);
    await Promise.all([updateUserTimelineOnAllocationUpdate(updatedEntry)])
      .then(() => {
        loaderContext.open(false);
      })
      .catch((er) => {
        loaderContext.open(false);
      });
    return;
  };

  const refreshAllUserTimelines = (
    allPrevEntries: IAllUserAllocationEntries[]
  ) => {
    const fetchTimelineBody: GetUsersTimelinePayload[] = allPrevEntries.map(
      (entry) => {
        return {
          emails: [entry.email],
          start_date: getValues(EBaseCommonAllocationMainControlForm.startDate),
          end_date: getValues(EBaseCommonAllocationMainControlForm.endDate),
        };
      }
    );
    return new Promise<boolean>((resolve, reject) => {
      Promise.all([fetchUsersTimelineForMultipleDates(fetchTimelineBody)])
        .then((response) => {
          const tempTimeline = makeUserTimelineGroupsAndItems(
            allPrevEntries,
            response[0],
            {
              timelineGroups: [],
              timelineItems: [],
            }
          );
          setUserTimelines(tempTimeline);
          resolve(true);
        })
        .catch((error) => {
          snackbarContext.displaySnackbar(
            SnackbarMessages.ErrorFetchingUserTimelines,
            SnackbarSeverity.ERROR,
            6000
          );
          reject(false);
        });
    });
  };

  const updateAllUserAllocationsAccordingToBaseInfos = async () => {
    try {
      if (
        props.baseType !== EAllocationType.SYSTEM_SUGGESTED_ALLOCATION &&
        applyDatesToAllAllocation
      ) {
        loaderContext.open(true);
        const getAllUserAvailabilities =
          await CheckUserAvailabilityForDatesAndEffortsUtils(
            allUserAllocationEntries.map((item) => item.email),
            getValues(EBaseCommonAllocationMainControlForm.startDate),
            getValues(EBaseCommonAllocationMainControlForm.endDate),
            getValues(EBaseCommonAllocationMainControlForm.noOfHours),
            getValues(
              EBaseCommonAllocationMainControlForm.isPerDayHourAllocation
            ),
            null,
            props.projectInfo?.pipelineCode,
            props.projectInfo?.jobCode
          );

        const tempAllUserAllocationEntries =
          UpdateAllUserAllocationsAccordingToBaseInfosUtils(
            allUserAllocationEntries,
            getValues,
            getAllUserAvailabilities
          );
        await refreshAllUserTimelines(tempAllUserAllocationEntries);
        setAllUserAllocationEntries(tempAllUserAllocationEntries);
        loaderContext.open(false);
      }
    } catch (er) {
      loaderContext.open(false);
    }
  };

  const GetAllJobCodesForPipelineCode = (
    pipelineCode: string,
    jobCode: string
  ): Promise<string[]> => {
    return new Promise<string[]>((resolve, reject) => {
      GetAllJobCodesForPipelineCodeService(pipelineCode, jobCode, true)
        .then((resp) => {
          setListOfJobCodes(resp);
          resolve(resp);
        })
        .catch(() => {
          snackbarContext.displaySnackbar(
            "Error fetching job codes",
            SnackbarSeverity.ERROR
          );
          resolve([]);
        });
    });
  };

  useEffect(() => {
    try {
      if (props.projectInfo && allUserAllocationEntries.length === 0) {
        updateBaseCommonFormDetails();
        usersSelectedAllocationContext.setProjectInfo(props.projectInfo);

        if (
          props?.suggestionsSelected &&
          props?.requisition &&
          props.baseType === EAllocationType.SYSTEM_SUGGESTED_ALLOCATION
        ) {
          loaderContext.open(true);
          Promise.all([
            addSystemSuggestedUsersToTimeline(),
            GetAllJobCodesForPipelineCode(
              props.projectInfo.pipelineCode,
              props.projectInfo.jobCode
            ),
          ]).then((resp) => {
            loaderContext.open(false);
          });
        } else if (props.baseType === EAllocationType.UPDATE_ALLOCATION) {
          loaderContext.open(true);
          Promise.all([
            addAllocationsToUpdateUsersToTimeline(),
            GetAllJobCodesForPipelineCode(
              props.projectInfo.pipelineCode,
              props.projectInfo.jobCode
            ),
          ])
            .then((resp) => {
              loaderContext.open(false);
            })
            .catch((err) => {});
        } else if (
          props.baseType === EAllocationType.SAME_TEAM_ALLOCATION ||
          props.baseType === EAllocationType.NAME_ALLOCATION
        ) {
          loaderContext.open(true);
          Promise.all([
            GetAllJobCodesForPipelineCode(
              props.projectInfo.pipelineCode,
              props.projectInfo.jobCode
            ),
            addPreSelectedUsersForNameAllocationEntries(),
          ]).then((resp) => {
            loaderContext.open(false);
          });
        } else if (
          props.baseType === EAllocationType.BULK_ALLOCATION &&
          props.bulkAllocationsUploaded
        ) {
          loaderContext.open(true);
          Promise.all([
            addBulkUploadedAllocationsToTimeline(),
            GetAllJobCodesForPipelineCode(
              props.projectInfo.pipelineCode,
              props.projectInfo.jobCode
            ),
          ]).then((resp) => {
            loaderContext.open(false);
          });
        }
      }
    } catch (err) {
      loaderContext.open(false);
    }
  }, [
    props?.suggestionsSelected,
    props?.requisition,
    props.projectInfo,
    props.bulkAllocationsUploaded,
  ]);

  const updateCheckedUsersInCaseUserBecomesUnavailable = () => {
    const updatedSelectedUsers = allUserAllocationEntries.filter((userItem) => {
      const isAlreadySelected =
        usersSelectedAllocationContext.usersSelected.find(
          (user) =>
            user.email.toLowerCase().trim() ===
            userItem.email.trim().toLowerCase()
        );
      if (isAlreadySelected && userItem.available) {
        return userItem;
      } else {
        return false;
      }
    });
    usersSelectedAllocationContext.setUsersSelected(updatedSelectedUsers);
  };

  useEffect(() => {
    usersSelectedAllocationContext.setAllUserSelectionEntries(
      allUserAllocationEntries
    );
    updateCheckedUsersInCaseUserBecomesUnavailable();
  }, [allUserAllocationEntries]);

  useEffect(() => {
    const isEveryDisplayItemSelected = userTimelines.timelineGroups
      .filter((item) => item.display)
      .every((item) =>
        usersSelectedAllocationContext.usersSelected.find(
          (prevSelected) => prevSelected.email === item.user.email
        )
      );
    if (
      usersSelectedAllocationContext.usersSelected.length === 0 ||
      !isEveryDisplayItemSelected
    ) {
      setAllUsersSelected(false);
    } else if (isEveryDisplayItemSelected) {
      setAllUsersSelected(true);
    }
  }, [
    usersSelectedAllocationContext.usersSelected,
    userTimelines.timelineGroups,
  ]);

  useEffect(() => {
    // updateAllUserAllocationsAccordingToBaseInfos();
    if (applyDatesToAllAllocation) {
      setOpenConfirmation(EConfirmationDialogType.ApplyBaseTimelineToAllUsers);
    }
  }, [applyDatesToAllAllocation]);

  const removeUserFromTimeline = (email: string) => {
    loaderContext.open(true);
    const tempAllUserEntries = allUserAllocationEntries.filter(
      (item) => item.email !== email
    );
    setAllUserAllocationEntries(tempAllUserEntries);
    updateTimelineDatesOnUserAllocationFormAction(tempAllUserEntries);
    var timelineGroup = userTimelines.timelineGroups.filter(
      (item) => item.user.email !== email
    );
    var timelineItems = userTimelines.timelineItems.filter(
      (item) => item.user.email !== email
    );
    const tempTimeline = {
      timelineGroups: timelineGroup,
      timelineItems: timelineItems,
    };
    setUserTimelines(tempTimeline);

    const tempSelected = usersSelectedAllocationContext.usersSelected.filter(
      (item) => item.email !== email
    );
    usersSelectedAllocationContext.setUsersSelected(tempSelected);
    loaderContext.open(false);
  };

  useEffect(() => {
    if (usersSelectedAllocationContext.removeUserFromTimeline) {
      setOpenConfirmation(EConfirmationDialogType.RemoveUserEntry);
    }
  }, [usersSelectedAllocationContext.removeUserFromTimeline]);

  const onChangeAllUsersChecked = (e) => {
    const userDisplayOnScreen = userTimelines.timelineGroups.filter(
      (item) => item.display
    );
    if (e.target.checked) {
      const tempUserAllocationEntries = allUserAllocationEntries.filter(
        (item) =>
          userDisplayOnScreen.some(
            (displayedItm) => displayedItm.user.email === item.email
          ) ||
          usersSelectedAllocationContext.usersSelected.find(
            (prevSelected) => prevSelected.email === item.email
          )
      );
      const filterOutEntries = tempUserAllocationEntries.filter((item) => {
        if (item.type === EAllocationType.UPDATE_ALLOCATION) {
          return !checkIfPrevAllocIsSameAsNewAllocation(
            item,
            usersSelectedAllocationContext
          );
        } else {
          return true;
        }
      });

      usersSelectedAllocationContext.setUsersSelected(() => filterOutEntries);
    } else {
      const tempUserAllocationEntries = allUserAllocationEntries.filter(
        (item) =>
          !userDisplayOnScreen.some(
            (displayedItm) => displayedItm.user.email === item.email
          ) &&
          usersSelectedAllocationContext.usersSelected.find(
            (prevSelected) => prevSelected.email === item.email
          )
      );

      usersSelectedAllocationContext.setUsersSelected(
        tempUserAllocationEntries
      );
    }
  };

  const checkAndSetOpenBackPromptDialog = (check: boolean) => {
    if (check) {
      if (isDirty || allUserAllocationEntries.length > 0) {
        setOpenBackPromptDialog(check);
      } else {
        setOpenBackPromptDialog(false);
        props.back();
      }
    }
  };

  return (
    <Typography component="div" className="CommonAllocationMain">
      {blocker.state === "blocked" && isFormDirty ? (
        <DialogBox
          showDialog={isFormDirty}
          cancelNavigation={handleCancel}
          confirmNavigation={handleConfirm}
        />
      ) : null}
      <DialogBox
        showDialog={openBackPromptDialog}
        confirmNavigation={() => {
          setOpenBackPromptDialog(false);
          props.back();
        }}
        cancelNavigation={() => {
          setOpenBackPromptDialog(false);
        }}
      />
      <ConfirmationDialog
        handleYesClick={(e) => {
          if (openConfirmation === EConfirmationDialogType.RemoveUserEntry) {
            if (usersSelectedAllocationContext.removeUserFromTimeline) {
              removeUserFromTimeline(
                usersSelectedAllocationContext.removeUserFromTimeline
              );
              usersSelectedAllocationContext.setRemoveUserFromTimeline("");
            }
          } else if (
            openConfirmation ===
            EConfirmationDialogType.ApplyBaseTimelineToAllUsers
          ) {
            updateAllUserAllocationsAccordingToBaseInfos();
          }
          setOpenConfirmation("");
        }}
        title={
          openConfirmation === EConfirmationDialogType.RemoveUserEntry
            ? "Remove User from timeline?"
            : openConfirmation ===
              EConfirmationDialogType.ApplyBaseTimelineToAllUsers
            ? "Apply base dates to all users?"
            : ""
        }
        content={
          openConfirmation === EConfirmationDialogType.RemoveUserEntry
            ? "Are you sure you want to remove user from the timeline?"
            : openConfirmation ===
              EConfirmationDialogType.ApplyBaseTimelineToAllUsers
            ? "Are you sure you want to apply base dates to all users?"
            : ""
        }
        noBtnLabel="No"
        yesBtnLabel="Yes"
        open={openConfirmation ? true : false}
        onConfirmationPopClose={(e) => {
          setOpenConfirmation("");
          if (openConfirmation === EConfirmationDialogType.RemoveUserEntry) {
            usersSelectedAllocationContext.setRemoveUserFromTimeline("");
          } else if (
            openConfirmation ===
            EConfirmationDialogType.ApplyBaseTimelineToAllUsers
          ) {
            setApplyDatesToAllAllocation(false);
            setValue(EBaseCommonAllocationMainControlForm.applyToAll, false);
          }
        }}
      />
      <CommonAllocationModalForm
        openAllocationModal={openAllocationModal}
        setOpenAllocationModal={function (e: boolean): {} {
          setOpenAllocationModal(e);
          return;
        }}
        selectedUserForAllocationModal={selectedUserForAllocationModal}
        projectInfo={props.projectInfo}
        updateAllocationForEmployee={updateAllocationForEmployee}
        addNameAllocationUsersToTimeline={addNameAllocationUsersToTimeline}
        baseStartEndDateToConsiderForDefaultAllocationEntry={{
          endDate: getValues(EBaseCommonAllocationMainControlForm.endDate),
          startDate: getValues(EBaseCommonAllocationMainControlForm.startDate),
          noOfHours: getValues(EBaseCommonAllocationMainControlForm.noOfHours),
          isPerDayHourAllocation: getValues(
            EBaseCommonAllocationMainControlForm.isPerDayHourAllocation
          ),
        }}
      />
      <Grid container spacing={2} sx={AlignGridCenterSxProps}>
        <CommonAllocationBaseInfo
          control={control}
          listOfJobCodes={listOfJobCodes}
          errors={errors}
          trigger={trigger}
          getValues={getValues}
          loaderContext={loaderContext}
          projectInfo={props.projectInfo}
          baseType={props.baseType}
          setOpenBackPromptDialog={checkAndSetOpenBackPromptDialog}
          usersSelectedAllocationContext={usersSelectedAllocationContext}
          updateAllUserAllocationsAccordingToBaseInfos={
            updateAllUserAllocationsAccordingToBaseInfos
          }
          setApplyDatesToAllAllocation={setApplyDatesToAllAllocation}
          setSelectedUserForAllocationModal={setSelectedUserForAllocationModal}
          setOpenAllocationModal={setOpenAllocationModal}
          allUserAllocationEntries={allUserAllocationEntries}
          snackbarContext={snackbarContext}
          userTimelines={userTimelines}
          setUserTimelines={function (
            value: React.SetStateAction<IUserTimeline>
          ): void {
            setUserTimelines(value);
          }}
          updateUserEntriesOnJobCodeChangeSameTeam={
            updateUserEntriesOnJobCodeChangeSameTeam
          }
          setIsFormDirty={setIsFormDirty}
          refreshProjectInfo={function (): void {
            props.refreshProjectInfo();
          }}
          isPageLoad = { props.isPageLoad}
        />
        <Grid item xs={12} className="commonAllocation">
          {!loaderContext.isOpen && (
            <TimelineView
              defaultTimeStart={baseFormDetailsForTimelineView.startDate}
              defaultTimeEnd={baseFormDetailsForTimelineView.endDate}
              timelineGroups={userTimelines.timelineGroups.filter(
                (timeline) => timeline.display
              )}
              timelineItems={userTimelines.timelineItems}
              onItemClick={function (
                itemId: any,
                e: React.SyntheticEvent<Element, Event>,
                time: number
              ): {} {
                openAllocationModalScreenForItemId(itemId);
                return true;
              }}
              showCheckbox={true}
              userTimelines={userTimelines}
              onCheckBoxChange={(e) => onChangeAllUsersChecked(e)}
              allUsersSelected={allUsersSelected}
            />
          )}
        </Grid>
      </Grid>
    </Typography>
  );
};

export default memo(CommonAllocationMain);
