import {
  EAllocationsBreakupVariables,
  IAllocateFormSkills,
} from "../common-allocation/common-allocation-modal-form/utils";
import { EAllocationTypeArray } from "../common-allocation/enum";
import { IAllUserAllocationEntries } from "../common-allocation/interface";

export const getUserAllocationByCodeMove = (
  allocationData: any[],
  projectDetails: any
): IAllUserAllocationEntries[] => {
  const tempAllocationEntry: IAllUserAllocationEntries[] = allocationData.map(
    (userEntry) => {
      const filteredEntry = EAllocationTypeArray.filter(
        (e) => e.id === userEntry?.requisition?.requisitionTypeId
      );
      const value = filteredEntry.length > 0 ? filteredEntry[0] : null;
      return {
        email: userEntry.empEmail,
        userInfo: {
          empName: userEntry.empName,
          email: userEntry.empEmail,
          designation: userEntry?.requisition?.designation,
          grade: userEntry?.requisition?.grade,
          location: userEntry?.requisition?.location,
          sme_group: userEntry?.requisition?.sme,
          expertise: userEntry?.requisition?.expertise,
          supercoach: userEntry?.Supercoach,
          businessUnit: userEntry?.requisition?.businessUnit,
          smeg: userEntry?.requisition?.smeg,
          competency: userEntry?.requisition?.competency,
          competencyId: userEntry?.requisition?.competencyId,
          match_score: "",
        },
        type: value?.name,
        skills: userEntry.skills.map((skill: IAllocateFormSkills) => ({
          skillName: skill.skillName,
          skillCode: skill.skillCode,
        })),
        competency: {
          competency: userEntry?.requisition?.competency,
          competencyId: userEntry?.requisition?.competencyId,
        },
        available: false,
        meta: userEntry,
        interested: false,
        isSkillUpdateAllowed: true,
        allocations: userEntry?.resourceAllocations?.map((alloc: any) => ({
          [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]:
            new Date(alloc.startDate) < new Date()
              ? new Date()
              : alloc.startDate,
          [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
            alloc.endDate,
          [EAllocationsBreakupVariables.ConfirmedPerDayHours]: alloc.efforts,
          [EAllocationsBreakupVariables.PerDayAllocation]:
            alloc.isPerDayAllocation,
        })),
        isContinuousAllocation: true,
        description: userEntry.description,
        totalEfforts: userEntry.totalEffort,
        isInDraftMode: true,
        showSkills: true,
        showDescription: true,
        projectInfo: projectDetails,
        isUpdateAllowed: true,
        isPreviouslyDraft: false,
      };
    }
  );
  return tempAllocationEntry;
};

export const getCurrentProjectName = (data: any) => {
  if (data.jobCode) {
    return data.jobName + " - " + data.jobCode;
  } else {
    return data.pipelineName + " - " + data.pipelineCode;
  }
};
