import moment from "moment";
import { IResourceAllocationDetailsMaster } from "../../common/interfaces/IAllocation";
import { IProjectMaster } from "../../common/interfaces/IProject";
import { IRequisitionMaster } from "../../common/interfaces/IRequisition";
import { IUserModelMaster } from "../../common/interfaces/IUserModel";
import { GetUsersTimelinePayload } from "../../global/service-constant";
import {
  GetUsersTimelineForMultipleDates,
  getAllocationByJobcode,
} from "../../services/allocation/allocation.service";
import { GetUsersInfoByEmails } from "../../services/user/user.service";
import {
  GetPrevCurrentValidDate,
  GetNextCurrentValidDate,
  GetNewDateWithNoonTimeZone,
  MomentAddDays,
  MomentToJSDate,
  calculateDaysBetweenTwoDates,
  getDateInMomentFormat,
  getDateWithEndHours,
} from "../../utils/date/dateHelper";
import { IBulkUploadData } from "../bulk-upload-main/interfaces";
import {
  IUserTimelineInner,
  IUsersTimelines,
  getCustomItemPropsClass,
} from "../system-suggestions/availability-view/constants";
import { ISystemSuggestions } from "../system-suggestions/interfaces";
import {
  ECommonAllocationFilterControl,
  ICommonAllocationFilterControl,
} from "./common-allocation-filter/utils";
import {
  CheckUserAvailabilityForDatesAndEffortsUtils,
  EAllocationsBreakupVariables,
  FormValuesForAllocationBreakup,
  IAllocateFormSkills,
  IisAllocationHoursAvailableResponse,
} from "./common-allocation-modal-form/utils";
import { getUserAvailabilityInfoComponent } from "./commonAllocationWrapper";
import { EAllocationType, EBaseCommonAllocationMainControlForm } from "./enum";
import {
  IAllUserAllocationEntries,
  IBaseCommonAllocationFormDetails,
  ICommonAllocationMainProps,
  IUserTimeline,
} from "./interface";
import { ITimelineGroup } from "./timeline-view/interface";
import { ERequisitionTypeData } from "../../common/enums/ERequisitionEnums";
import { IUpdateAllocationCommonScreenItem } from "./update-allocation-common-screeen/update-allocation-common-screeen";
import { ITimelineDisplayType, ITimelineType } from "../calendar/Utils";

export enum EConstantsEnum {
  HoursAvailable = " hours available",
  HoursAllocated = " hours allocated",
}
export enum ETimelineType {
  LEAVE = ITimelineDisplayType.FULL_DAY_LEAVE,
  FULL_DAY_LEAVE = ITimelineType.FULL_DAY_LEAVE,
  FIRST_HALF_LEAVE = ITimelineType.FIRST_HALF_LEAVE,
  SECOND_HALF_LEAVE = ITimelineType.SECOND_HALF_LEAVE,
  ALLOCATION = ITimelineType.ALLOCATION,
  AVAILABLE = ITimelineType.AVAILABLE,
  HOLIDAY = ITimelineType.HOLIDAY,
  // ROLLOVERPROPOSED = "rolloverproposed",
  // ROLLOVERALLOCATION = "rolloverallocation",
}

export const fetchUsersTimelineForMultipleDates = (
  payloadBody: GetUsersTimelinePayload[]
): Promise<IUsersTimelines[]> => {
  return new Promise<IUsersTimelines[]>((resolve, reject) => {
    GetUsersTimelineForMultipleDates(payloadBody)
      .then((resp) => {
        resolve(resp);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

const getTimelineDisplayText = (
  userTimelineBreakups: IUserTimelineInner
): string => {
  let finalText: string = " ";

  if (
    userTimelineBreakups?.timeline_type.toLowerCase() ===
      ETimelineType.AVAILABLE.toString().toLowerCase().trim() ||
    userTimelineBreakups?.timeline_type.toLowerCase() ===
      ETimelineType.ALLOCATION.toString().toLowerCase().trim() ||
    userTimelineBreakups?.timeline_type.toLowerCase() ===
      ETimelineType.FIRST_HALF_LEAVE.toString().toLowerCase().trim() ||
    userTimelineBreakups?.timeline_type.toLowerCase() ===
      ETimelineType.SECOND_HALF_LEAVE.toString().toLowerCase().trim()
  ) {
    finalText = `${
      userTimelineBreakups.timeline_display_text
        ? userTimelineBreakups.timeline_display_text?.trim()
        : ""
    } ${EConstantsEnum.HoursAvailable}`;
  } else if (
    userTimelineBreakups?.timeline_type.toLowerCase() ===
    ETimelineType.FULL_DAY_LEAVE.toString().toLowerCase().trim()
  ) {
    finalText = ETimelineType.LEAVE.toString();
  } else if (
    userTimelineBreakups?.timeline_type.toLowerCase() ===
    ETimelineType.LEAVE.toString().toLowerCase().trim()
  ) {
    finalText = ETimelineType.LEAVE.toString();
  } else if (
    userTimelineBreakups?.timeline_type.toLowerCase() ===
    ETimelineType.HOLIDAY.toString().toLowerCase().trim()
  ) {
    finalText = ETimelineType.HOLIDAY.toString();
  } else {
    finalText = userTimelineBreakups.timeline_display_text
      ? userTimelineBreakups.timeline_display_text
      : "";
  }
  return finalText?.toString();
};

export const makeUserTimelineGroupsAndItems = (
  users: IAllUserAllocationEntries[],
  newTimelinesFetched: IUsersTimelines[],
  prevAllTimelines: IUserTimeline
): IUserTimeline => {
  let groupId: number =
    prevAllTimelines.timelineGroups.length >= 1
      ? prevAllTimelines.timelineGroups[
          prevAllTimelines.timelineGroups.length - 1
        ].id
      : 0;
  let itemId: number =
    prevAllTimelines.timelineItems.length >= 1
      ? prevAllTimelines.timelineItems[
          prevAllTimelines.timelineItems.length - 1
        ].id
      : 0;

  const tempTimelineGroup = prevAllTimelines.timelineGroups;
  const tempTimelineItems = prevAllTimelines.timelineItems;

  users.forEach((userItem, index: number) => {
    tempTimelineGroup.push({
      id: ++groupId,
      title: getUserAvailabilityInfoComponent(userItem, index),
      height: 60,
      user: userItem,
      type: userItem.type,
      display: true,
    });
    const userTimeline = newTimelinesFetched.filter(
      (timelineItem) =>
        timelineItem.email.toLowerCase().trim() ===
        userItem.email.toLowerCase().trim()
    );
    if (userTimeline) {
      userTimeline.forEach((userTimelineItem) => {
        userTimelineItem.usersTimelines.forEach(
          (userTimelineBreakups: IUserTimelineInner) => {
            tempTimelineItems.push({
              id: ++itemId,
              title: getTimelineDisplayText(userTimelineBreakups),
              start_time: MomentToJSDate(
                getDateInMomentFormat(new Date(userTimelineBreakups.start))
              ),
              end_time: getDateWithEndHours(
                new Date(userTimelineBreakups.end)
              ).toDate(),
              group: groupId,
              innerHeight: 500,
              outerHeight: 500,
              condition: userTimelineBreakups.timeline_type,
              itemProps: {
                className: getCustomItemPropsClass(
                  userTimelineBreakups.timeline_type
                ),
              },
              user: userItem.userInfo,
              allocations: userItem.allocations,
              meta: {
                weeklyBreakup: userTimelineBreakups.weeklyBreakup,
                weeklyTotal: userTimelineBreakups.weeklyTotal,
              },
              type: userItem.type,
            });
          }
        );
      });
    }
  });
  const updatedTimelines: IUserTimeline = {
    timelineGroups: tempTimelineGroup,
    timelineItems: tempTimelineItems,
  };
  return updatedTimelines;
};

export const UpdateUserTimelinesGroupAndItems = (
  userEntry: IAllUserAllocationEntries,
  updatedTimeline: IUsersTimelines[],
  prevAllTimelines: IUserTimeline
): IUserTimeline => {
  const updatedItemTiles = prevAllTimelines.timelineItems.filter(
    (timelineItem) => timelineItem.user.email !== userEntry.email
  );
  const userGroup = prevAllTimelines.timelineGroups.find(
    (timelineGroup) => timelineGroup.user.email === userEntry.email
  );
  const userTimeline = updatedTimeline.filter(
    (timelineItem) =>
      timelineItem.email.toLowerCase().trim() ===
      userEntry.email.toLowerCase().trim()
  );
  let groupId: number = userGroup ? userGroup.id : 0;
  let itemId: number =
    updatedItemTiles.length >= 1
      ? updatedItemTiles[updatedItemTiles.length - 1].id
      : 0;
  if (userTimeline) {
    userTimeline.forEach((userTimelineItem) => {
      userTimelineItem.usersTimelines.forEach(
        (userTimelineBreakups: IUserTimelineInner) => {
          updatedItemTiles.push({
            id: ++itemId,
            title: getTimelineDisplayText(userTimelineBreakups),
            start_time: MomentToJSDate(
              getDateInMomentFormat(new Date(userTimelineBreakups.start))
            ),
            end_time: getDateWithEndHours(
              new Date(userTimelineBreakups.end)
            ).toDate(),
            group: groupId,
            innerHeight: 500,
            outerHeight: 500,
            condition: userTimelineBreakups.timeline_type,
            itemProps: {
              className: getCustomItemPropsClass(
                userTimelineBreakups.timeline_type
              ),
            },
            user: userEntry.userInfo,
            allocations: userEntry.allocations,
            meta: {
              weeklyBreakup: userTimelineBreakups.weeklyBreakup,
              weeklyTotal: userTimelineBreakups.weeklyTotal,
            },
            type: userEntry.type,
          });
        }
      );
    });
  }

  const updatedTimelines: IUserTimeline = {
    timelineGroups: prevAllTimelines.timelineGroups,
    timelineItems: updatedItemTiles,
  };
  return updatedTimelines;
};

export interface IGetSystemSuggestedUsersAllocationEntries {
  requisition?: IRequisitionMaster;
  suggestionsSelected?: ISystemSuggestions[];
  projectInfo: IProjectMaster;
}

export const getPreSelectedUsersForNameAllocationEntries = async (
  usersToAdd: string[],
  projectInfo: IProjectMaster
): Promise<IAllUserAllocationEntries[]> => {
  const usersInfoFetched = await GetAllUsersInfo(usersToAdd);
  const tempAllocationEntry: IAllUserAllocationEntries[] = usersInfoFetched.map(
    (userEntry) => {
      return {
        email: userEntry.email_id,
        userInfo: {
          empName: userEntry.name,
          email: userEntry.email_id,
          grade: userEntry.grade,
          designation: userEntry.designation,
          location: userEntry?.location,
          supercoach: userEntry?.supercoach_name,
          businessUnit: userEntry?.business_unit,
          competency: userEntry?.competency,
          competencyId: userEntry?.competencyId,
          match_score: "",
        },
        type: EAllocationType.NAME_ALLOCATION,
        skills: [],
        competency: {
          competency: userEntry?.competency,
          competencyId: userEntry?.competencyId,
        },
        available: false,
        meta: userEntry,
        interested: false,
        isSkillUpdateAllowed: true,
        allocations: [
          {
            [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
              checkProjectAllocationStartDateWithCurrentDate(
                GetNewDateWithNoonTimeZone(projectInfo?.startDate)
              ),
            [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
              GetNewDateWithNoonTimeZone(projectInfo?.endDate),
            [EAllocationsBreakupVariables.ConfirmedPerDayHours]: 1,
            [EAllocationsBreakupVariables.PerDayAllocation]: false,
          },
        ],
        isContinuousAllocation: true,
        description: "",
        totalEfforts: 1,
        isInDraftMode: true,
        showSkills: true,
        showDescription: true,
        projectInfo: projectInfo,
        isUpdateAllowed: true,
        isPreviouslyDraft: false,
      };
    }
  );
  return tempAllocationEntry;
};

export const getSystemSuggestedUsersAllocationEntries = (
  props: IGetSystemSuggestedUsersAllocationEntries
): IAllUserAllocationEntries[] => {
  const allocationsPrefilledData: FormValuesForAllocationBreakup[] = [
    {
      [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]: moment(
        props.requisition.startDate
      ).isSameOrAfter(moment(new Date()))
        ? props.requisition.startDate
        : new Date(),
      [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
        props.requisition.endDate,
      [EAllocationsBreakupVariables.ConfirmedPerDayHours]: props.requisition
        .isPerDayHourAllocation
        ? props.requisition.effortsPerDay
        : props.requisition.totalHours,
      [EAllocationsBreakupVariables.PerDayAllocation]:
        props.requisition.isPerDayHourAllocation,
    },
  ];

  const userAllocationEntries: IAllUserAllocationEntries[] =
    props?.suggestionsSelected.map(
      (userSuggestions: ISystemSuggestions, index: number) => {
        return {
          email: userSuggestions.email,
          userInfo: {
            ...userSuggestions,
            email: userSuggestions?.email,
            empName: userSuggestions?.empName,
            designation: userSuggestions?.designation,
            location: userSuggestions?.location,
            supercoach: userSuggestions?.supercoach,
            match_score: userSuggestions?.score,
            interested: userSuggestions?.interested,
            businessUnit: userSuggestions?.business_unit,
            grade: userSuggestions.grade,
            competency: userSuggestions?.competency,
            competencyId: userSuggestions?.competencyId,
          },
          available: userSuggestions.available,
          type: EAllocationType.SYSTEM_SUGGESTED_ALLOCATION,
          competency: {
            competency: userSuggestions.competency,
            competencyId: userSuggestions.competencyId,
          },
          skills: userSuggestions.skill.map((skillItem) => {
            return {
              skillName: skillItem.skillName,
              skillCode: skillItem.skillCode,
            };
          }),
          isSkillUpdateAllowed: false,
          meta: userSuggestions,
          interested: userSuggestions.interested,
          allocations: allocationsPrefilledData,
          requisition: props?.requisition,
          requisitionId: props?.requisition?.demands?.requisitions[index]?.id,
          isContinuousAllocation: true,
          description: props?.requisition?.description || "",
          totalEfforts: props.requisition.totalHours,
          isInDraftMode: true,
          showSkills: true,
          showDescription: true,
          projectInfo: props.projectInfo,
          isUpdateAllowed: true,
          isPreviouslyDraft: false,
        };
      }
    );
  return userAllocationEntries;
};

export const getAllocationsToUpdateUsersAllocationEntries = async (
  allocationsToUpdate: IUpdateAllocationCommonScreenItem[],
  projectInfo: IProjectMaster
): Promise<IAllUserAllocationEntries[]> => {
  try {
    let response: IAllUserAllocationEntries[] = [];

    const reducedAllocationsPerEntry = allocationsToUpdate.flatMap((item) =>
      item.resourceAllocations.map((allocation) => ({
        email: item.empEmail,
        requisitionId: item.requisitionId,
        startDate: allocation.startDate,
        endDate: allocation.endDate,
        efforts: allocation.efforts,
        isPerDayAllocation: allocation.isPerDayAllocation,
      }))
    );
    await Promise.all([
      GetAllUsersInfo(allocationsToUpdate.map((user) => user.empEmail)),
      ...reducedAllocationsPerEntry.map((user) =>
        CheckUserAvailabilityForDatesAndEffortsUtils(
          [user.email],
          GetNewDateWithNoonTimeZone(user.startDate),
          GetNewDateWithNoonTimeZone(user.endDate),
          user.efforts,
          user.isPerDayAllocation,
          user.requisitionId,
          projectInfo?.pipelineCode,
          projectInfo?.jobCode
        )
      ),
    ])
      .then((resp) => {
        const usersInfoFetched = resp[0];
        const getAllUserAvailabilities = resp.slice(1, resp.length);
        let finalGetAllUserAvailabilities: any = getAllUserAvailabilities
          .filter((user) => {
            return user[0] ? true : false;
          })
          .map((user) => user[0]);

        const userAllocationEntries: IAllUserAllocationEntries[] =
          allocationsToUpdate.map((allocationItem) => {
            const userEntry = usersInfoFetched.find(
              (user) =>
                user.email_id.toLowerCase().trim() ===
                allocationItem.empEmail.toLowerCase().trim()
            );
            let allocationsPrefilledData: FormValuesForAllocationBreakup[] = [];
            if (
              allocationItem?.consumedHours &&
              allocationItem?.consumedHours > 0
            ) {
              const currentDate = GetNewDateWithNoonTimeZone();
              const currentPrevValidDate = GetPrevCurrentValidDate();
              const currentNextValidDate = GetNextCurrentValidDate();
              // const resourceAllocItems = [];
              allocationItem.resourceAllocations.forEach((resItem) => {
                if (
                  moment(
                    GetNewDateWithNoonTimeZone(resItem.startDate)
                  ).isSameOrBefore(moment(currentDate)) &&
                  moment(
                    GetNewDateWithNoonTimeZone(resItem.endDate)
                  ).isSameOrAfter(moment(currentDate))
                ) {
                  if (
                    moment(GetNewDateWithNoonTimeZone(resItem.endDate)).isSame(
                      moment(currentDate)
                    )
                  ) {
                    allocationsPrefilledData.push({
                      [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                        GetNewDateWithNoonTimeZone(resItem.startDate),
                      [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                        GetNewDateWithNoonTimeZone(resItem.endDate),
                      [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                        resItem.efforts,
                      [EAllocationsBreakupVariables.PerDayAllocation]:
                        resItem.isPerDayAllocation,
                    });
                  } else {
                    var pastAllocItem = {
                      [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                        GetNewDateWithNoonTimeZone(resItem.startDate),
                      [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                        GetNewDateWithNoonTimeZone(currentPrevValidDate),
                      [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                        allocationItem?.consumedHours,
                      [EAllocationsBreakupVariables.PerDayAllocation]: false,
                    };
                    allocationsPrefilledData.push(pastAllocItem);
                    var nextAllocItem = {
                      [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                        GetNewDateWithNoonTimeZone(
                          moment(currentNextValidDate).isSame(
                            moment(GetNewDateWithNoonTimeZone())
                          )
                            ? GetNextCurrentValidDate(
                                MomentAddDays(GetNewDateWithNoonTimeZone(), 1)
                              )
                            : currentNextValidDate
                        ),
                      [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                        GetNewDateWithNoonTimeZone(resItem.endDate),
                      [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                        resItem.isPerDayAllocation
                          ? resItem.efforts
                          : resItem.efforts - allocationItem?.consumedHours,
                      [EAllocationsBreakupVariables.PerDayAllocation]:
                        resItem.isPerDayAllocation,
                    };
                    allocationsPrefilledData.push(nextAllocItem);
                  }
                } else {
                  allocationsPrefilledData.push({
                    [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                      GetNewDateWithNoonTimeZone(resItem.startDate),
                    [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                      GetNewDateWithNoonTimeZone(resItem.endDate),
                    [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                      resItem.efforts,
                    [EAllocationsBreakupVariables.PerDayAllocation]:
                      resItem.isPerDayAllocation,
                  });
                }
              });
            } else {
              allocationsPrefilledData = allocationItem.resourceAllocations.map(
                (resourceAllocationItem) => {
                  return {
                    [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                      GetNewDateWithNoonTimeZone(
                        resourceAllocationItem.startDate
                      ),
                    [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                      GetNewDateWithNoonTimeZone(
                        resourceAllocationItem.endDate
                      ),
                    [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                      resourceAllocationItem.efforts,
                    [EAllocationsBreakupVariables.PerDayAllocation]:
                      resourceAllocationItem.isPerDayAllocation,
                  };
                }
              );
            }
            const getAllUserAvailabilitiesEntry: IisAllocationHoursAvailableResponse =
              finalGetAllUserAvailabilities.find(
                (availabilityItem: IisAllocationHoursAvailableResponse) =>
                  availabilityItem.emailId.toLowerCase().trim() ===
                    allocationItem.empEmail.toLowerCase().trim() &&
                  (!availabilityItem.isHoursAvialable ||
                    availabilityItem.errorMsg)
              );

            return {
              email: allocationItem.empEmail,
              userInfo: {
                ...userEntry,
                email: userEntry?.email_id,
                empName: userEntry?.name,
                designation: userEntry?.designation,
                location: userEntry?.location,
                supercoach: userEntry?.supercoach_name,
                businessUnit: userEntry?.business_unit,
                grade: userEntry.grade,
                competency: userEntry?.competency,
                competencyId: userEntry?.competencyId,
              },
              available: getAllUserAvailabilitiesEntry ? false : true,
              type: EAllocationType.UPDATE_ALLOCATION,
              competency: {
                competency: allocationItem?.requisition?.competency,
                competencyId: allocationItem?.requisition?.competencyId,
              },
              skills: (allocationItem.skills || []).map((req) => {
                return {
                  skillName: req.skillName,
                  skillCode: req.skillCode,
                };
              }),
              meta: allocationItem,
              interested: false,
              allocations: allocationsPrefilledData,
              baseAllocations: allocationsPrefilledData,
              requisition: allocationItem.requisition,
              requisitionId: allocationItem.requisitionId,
              isContinuousAllocation:
                allocationsPrefilledData.length > 1 ? false : true,
              description: allocationItem?.description || "",
              totalEfforts: allocationItem.totalEffort,
              isInDraftMode: false,
              showSkills: true,
              isSkillUpdateAllowed:
                allocationItem.allocationStatus.toLowerCase().trim() ===
                  "draft" &&
                allocationItem.requisition?.requisitionType?.type !==
                  ERequisitionTypeData.CreateRequisition &&
                allocationItem.requisition?.requisitionType?.type !==
                  ERequisitionTypeData.BulkAllocation
                  ? true
                  : false,
              showDescription: true,
              projectInfo: projectInfo,
              isUpdateAllowed: !allocationItem.isWorkflowRunning,
              isPreviouslyDraft:
                allocationItem.allocationStatus.toLowerCase().trim() ===
                "draft",
            };
          });
        response = userAllocationEntries;
      })
      .catch((err) => {});
    return response;
  } catch (error) {
    throw error;
  }
};

export const GetSameTeamUserAllocationEntries = async (
  usersAllocationData: IResourceAllocationDetailsMaster[],
  baseFormDetails: IBaseCommonAllocationFormDetails,
  projectInfo: IProjectMaster
): Promise<IAllUserAllocationEntries[]> => {
  const [usersInfoFetched, getAllUserAvailabilities] = await Promise.allSettled(
    [
      GetAllUsersInfo(usersAllocationData.map((user) => user.empEmail)),
      CheckUserAvailabilityForDatesAndEffortsUtils(
        usersAllocationData.map((user) => user.empEmail),
        baseFormDetails.startDate,
        baseFormDetails.endDate,
        baseFormDetails.noOfHours,
        baseFormDetails.isPerDayHourAllocation,
        null,
        projectInfo?.pipelineCode,
        projectInfo?.jobCode
      ),
    ]
  );
  const allocationsPrefilledData: FormValuesForAllocationBreakup[] = [
    {
      [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
        baseFormDetails.startDate,
      [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
        baseFormDetails.endDate,
      [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
        baseFormDetails.noOfHours,
      [EAllocationsBreakupVariables.PerDayAllocation]:
        baseFormDetails.isPerDayHourAllocation,
    },
  ];
  if (
    usersInfoFetched.status === "fulfilled" &&
    getAllUserAvailabilities.status === "fulfilled"
  ) {
    const userAllocationEntries: IAllUserAllocationEntries[] =
      usersAllocationData.map((userAllocation) => {
        const userEntry = usersInfoFetched.value.find(
          (user) =>
            user.email_id.toLowerCase().trim() ===
            userAllocation.empEmail.toLowerCase().trim()
        );
        return {
          email: userAllocation.empEmail,
          userInfo: {
            ...userEntry,
            email: userEntry?.email_id,
            empName: userEntry?.name,
            designation: userEntry?.designation,
            location: userEntry?.location,
            supercoach: userEntry?.supercoach_name,
            businessUnit: userEntry?.business_unit,
            competency: userEntry?.competency,
            competencyId: userEntry?.competencyId,
          },
          available:
            getAllUserAvailabilities.value &&
            getAllUserAvailabilities.value.find(
              (userAvailability) => userAvailability?.isHoursAvialable
            )
              ? true
              : false,
          type: EAllocationType.SAME_TEAM_ALLOCATION,
          skills: (userAllocation.skills || []).map((req) => {
            return {
              skillName: req.skillName,
              skillCode: req.skillCode,
            };
          }),
          competency: {
            competency: userAllocation?.requisition?.competency,
            competencyId: userAllocation?.requisition?.competencyId,
          },
          isSkillUpdateAllowed: true,
          meta: userAllocation,
          interested: false,
          allocations: allocationsPrefilledData,
          isContinuousAllocation: true,
          description: userAllocation.description,
          totalEfforts: baseFormDetails.isPerDayHourAllocation
            ? calculateDaysBetweenTwoDates(
                baseFormDetails.startDate,
                baseFormDetails.endDate
              ) * baseFormDetails.noOfHours
            : baseFormDetails.noOfHours,
          isInDraftMode: false,
          showSkills: true,
          showDescription: true,
          projectInfo: projectInfo,
          isUpdateAllowed: true,
          isPreviouslyDraft: false,
        };
      });
    return userAllocationEntries;
  }
};

export const GetAllUsersInfo = (
  users: string[]
): Promise<IUserModelMaster[]> => {
  return new Promise<IUserModelMaster[]>((resolve, reject) => {
    GetUsersInfoByEmails(users)
      .then((usersInfo) => {
        resolve(usersInfo);
      })
      .catch((err) => reject(err));
  });
};

export const GetAllAllocationsByJobCodesForSameTeam = (
  jobCodes: string[]
): Promise<IResourceAllocationDetailsMaster[]> => {
  return new Promise((resolve, reject) => {
    getAllocationByJobcode(jobCodes)
      .then((allocation) => {
        resolve(allocation.data);
      })
      .catch((err) => reject(err));
  });
};

export const UpdateBaseCommonFormDetailsUtils = (
  props: ICommonAllocationMainProps,
  baseFormDetails: IBaseCommonAllocationFormDetails
): IBaseCommonAllocationFormDetails => {
  let tempDetails: IBaseCommonAllocationFormDetails = baseFormDetails;
  if (props?.suggestionsSelected && props?.requisition) {
    tempDetails = {
      [EBaseCommonAllocationMainControlForm.startDate]:
        checkProjectAllocationStartDateWithCurrentDate(
          GetNewDateWithNoonTimeZone(props.requisition.startDate)
        ),
      [EBaseCommonAllocationMainControlForm.endDate]:
        GetNewDateWithNoonTimeZone(props.requisition.endDate),
      [EBaseCommonAllocationMainControlForm.noOfHours]: props.requisition
        .isPerDayHourAllocation
        ? props.requisition.effortsPerDay
        : props.requisition.totalHours,
      [EBaseCommonAllocationMainControlForm.isRequisition]: true,
      [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]:
        props.requisition.isPerDayHourAllocation,
    };
  } else {
    tempDetails = {
      [EBaseCommonAllocationMainControlForm.startDate]:
        checkProjectAllocationStartDateWithCurrentDate(
          GetNewDateWithNoonTimeZone(props.projectInfo?.startDate)
        ),
      [EBaseCommonAllocationMainControlForm.endDate]:
        GetNewDateWithNoonTimeZone(props.projectInfo?.endDate),
      [EBaseCommonAllocationMainControlForm.noOfHours]: 1,
      [EBaseCommonAllocationMainControlForm.isRequisition]: false,
      [EBaseCommonAllocationMainControlForm.isPerDayHourAllocation]: true,
    };
  }
  return tempDetails;
};

export const UpdateAllUserAllocationsAccordingToBaseInfosUtils = (
  allUserAllocationEntries: IAllUserAllocationEntries[],
  getValues: any,
  userAvailabilities: IisAllocationHoursAvailableResponse[]
) => {
  let tempAllUserAllocationEntries = allUserAllocationEntries.map((item) => {
    return {
      ...item,
      allocations: [
        {
          [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
            getValues(EBaseCommonAllocationMainControlForm.startDate),
          [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]: getValues(
            EBaseCommonAllocationMainControlForm.endDate
          ),
          [EAllocationsBreakupVariables.ConfirmedPerDayHours]: getValues(
            EBaseCommonAllocationMainControlForm.noOfHours
          ),
          [EAllocationsBreakupVariables.PerDayAllocation]: getValues(
            EBaseCommonAllocationMainControlForm.isPerDayHourAllocation
          ),
        },
      ],
      totalEfforts: getValues(
        EBaseCommonAllocationMainControlForm.isPerDayHourAllocation
      )
        ? getValues(EBaseCommonAllocationMainControlForm.noOfHours) *
          calculateDaysBetweenTwoDates(
            getValues(EBaseCommonAllocationMainControlForm.startDate),
            getValues(EBaseCommonAllocationMainControlForm.endDate)
          )
        : getValues(EBaseCommonAllocationMainControlForm.noOfHours),
      available:
        userAvailabilities &&
        userAvailabilities.find(
          (userAvailability) => userAvailability?.isHoursAvialable
        )
          ? true
          : false,
    };
  });
  return tempAllUserAllocationEntries;
};
export const convertBulkAllocationDataToCommonScreenPayload = async (
  bulkAllocationsUploaded: IBulkUploadData[],
  baseFormDetails: IBaseCommonAllocationFormDetails,
  projectInfo: IProjectMaster
): Promise<IAllUserAllocationEntries[]> => {
  let response: IAllUserAllocationEntries[] = [];
  await Promise.all([
    GetAllUsersInfo(bulkAllocationsUploaded.map((user) => user.emailId)),
    ...bulkAllocationsUploaded.map((user) =>
      CheckUserAvailabilityForDatesAndEffortsUtils(
        [user.emailId],
        GetNewDateWithNoonTimeZone(user.startDate),
        GetNewDateWithNoonTimeZone(user.endDate),
        user.numberOfHours,
        user.perDay === "No" ? false : true,
        null,
        projectInfo?.pipelineCode,
        projectInfo?.jobCode
      )
    ),
  ])
    .then((resp) => {
      const usersInfoFetched = resp[0];
      const getAllUserAvailabilities = resp.slice(1, resp.length);
      let finalGetAllUserAvailabilities: any = getAllUserAvailabilities
        .filter((user) => {
          return user[0] ? true : false;
        })
        .map((user) => user[0]);
      const userAllocationEntries: IAllUserAllocationEntries[] =
        bulkAllocationsUploaded.map((userAllocation) => {
          const userEntry = usersInfoFetched.find(
            (user) =>
              user.email_id.toLowerCase().trim() ===
              userAllocation.emailId.toLowerCase().trim()
          );
          const getAllUserAvailabilitiesEntry: IisAllocationHoursAvailableResponse =
            finalGetAllUserAvailabilities.find(
              (availabilityItem: IisAllocationHoursAvailableResponse) =>
                availabilityItem.emailId.toLowerCase().trim() ===
                userAllocation.emailId.toLowerCase().trim()
            );
          return {
            email: userAllocation.emailId,
            userInfo: {
              ...userEntry,
              email: userEntry?.email_id,
              empName: userEntry?.name,
              designation: userEntry?.designation,
              location: userEntry?.location,
              supercoach: userEntry?.supercoach_name,
              businessUnit: userEntry?.business_unit,
              competency: userEntry?.competency,
              competencyId: userEntry?.competencyId,
            },
            available:
              getAllUserAvailabilitiesEntry &&
              getAllUserAvailabilitiesEntry.isHoursAvialable
                ? true
                : false,
            type: EAllocationType.BULK_ALLOCATION,
            skills: userAllocation.skillList,
            competency: {
              competency: userEntry?.competency,
              competencyId: userEntry?.competencyId,
            },
            meta: userAllocation,
            interested: false,
            allocations: [
              {
                [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
                  GetNewDateWithNoonTimeZone(userAllocation.startDate),
                [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                  GetNewDateWithNoonTimeZone(userAllocation.endDate),
                [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                  userAllocation.numberOfHours,
                [EAllocationsBreakupVariables.PerDayAllocation]:
                  userAllocation.perDay === "No" ? false : true,
              },
            ],
            isContinuousAllocation: true,
            description: userAllocation.requisitionDescription,
            totalEfforts:
              userAllocation.perDay === "No"
                ? userAllocation.numberOfHours
                : calculateDaysBetweenTwoDates(
                    userAllocation.startDate,
                    userAllocation.endDate
                  ) * userAllocation.numberOfHours,
            isInDraftMode: false,
            isSkillUpdateAllowed: true,
            showSkills: true,
            showDescription: true,
            projectInfo: projectInfo,
            isUpdateAllowed: true,
            isPreviouslyDraft: false,
          };
        });
      response = userAllocationEntries;
    })
    .catch((err) => {
      //
    });
  return response;
};

export const FilterUserTimelinesForSameTeam = (
  filteredValues: ICommonAllocationFilterControl,
  allUserAllocationEntries: IAllUserAllocationEntries[],
  userTimelines: IUserTimeline
): IUserTimeline => {
  const tempGroupFilteredData: ITimelineGroup[] =
    userTimelines.timelineGroups.map((item) => {
      let display = true;
      for (const filterKey in filteredValues) {
        const filterValue = filteredValues[filterKey];
        const allUserItemData = { ...item.user, ...item.user.userInfo };
        if (
          filterKey.toLowerCase() ===
          ECommonAllocationFilterControl.skills.toLowerCase()
        ) {
          if (
            !filterValue.every((filterItemValue) =>
              allUserItemData[filterKey].some(
                (userItem: IAllocateFormSkills) =>
                  userItem.skillCode === filterItemValue.skillCode &&
                  userItem.skillName === filterItemValue.skillName
              )
            )
          ) {
            display = false;
            break;
          }
        } else if (
          !filterValue ||
          (Array.isArray(filterValue) && filterValue.length === 0)
        ) {
          continue;
        } else if (Array.isArray(filterValue)) {
          // AND condition for array values

          // If the values are array
          if (
            allUserItemData[filterKey] &&
            Array.isArray(allUserItemData[filterKey])
          ) {
            if (
              !filterValue.every((filterItemValue) =>
                allUserItemData[filterKey].some(
                  (userItem) => userItem === filterItemValue
                )
              )
            ) {
              display = false;
              break;
            }
          } else if (
            allUserItemData[filterKey] &&
            !filterValue.includes(allUserItemData[filterKey])
          ) {
            display = false;
            break;
          }
        } else {
          // OR condition for string values
          if (
            !allUserItemData[filterKey]
              .toLowerCase()
              .includes(filterValue.toLowerCase())
          ) {
            display = false;
            break;
          }
        }
      }
      return { ...item, display: display };
    });
  const updatedTimelines: IUserTimeline = {
    timelineGroups: tempGroupFilteredData,
    timelineItems: userTimelines.timelineItems,
  };
  return updatedTimelines;
};
export const CheckIfUsersAllocationIsUpdatedDifferentlyFromApplyToAll = (
  allocationItems: IAllUserAllocationEntries[],
  baseFormDetails: IBaseCommonAllocationFormDetails
): boolean => {
  const isApplyToAllConditionChanged = allocationItems.find((item) => {
    if (
      item.allocations.length !== 1 ||
      item.allocations[0][
        EAllocationsBreakupVariables.ConfirmedAllocationStartDate
      ] !== baseFormDetails.startDate ||
      item.allocations[0][
        EAllocationsBreakupVariables.ConfirmedAllocationEndDate
      ] !== baseFormDetails.endDate ||
      item.allocations[0][EAllocationsBreakupVariables.ConfirmedPerDayHours] !==
        baseFormDetails.noOfHours ||
      item.allocations[0][EAllocationsBreakupVariables.PerDayAllocation] !==
        baseFormDetails.isPerDayHourAllocation
    ) {
      return item;
    } else {
      return false;
    }
  });

  if (isApplyToAllConditionChanged) {
    return true;
  } else {
    return false;
  }
};

export const checkProjectAllocationStartDateWithCurrentDate = (
  projectStartDate: Date
): Date => {
  return moment(GetNewDateWithNoonTimeZone(projectStartDate)).isBefore(
    GetNewDateWithNoonTimeZone()
  )
    ? GetNewDateWithNoonTimeZone()
    : projectStartDate;
};
