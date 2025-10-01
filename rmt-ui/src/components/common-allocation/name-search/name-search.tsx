import React, { useEffect, useState } from "react";
import Employeesearch from "../../AllocationSearchFilter/EmployeeSearch/employeesearch";
import { EAllocationType } from "../enum";
import { IAllUserAllocationEntries } from "../interface";
import { LoaderContextProps } from "../../../contexts/loaderContext";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import { EAllocationsBreakupVariables } from "../common-allocation-modal-form/utils";
import { calculateDaysBetweenTwoDates } from "../../../utils/date/dateHelper";
import { EProjectType } from "../../scheduler-left/resource-menu/Constant";

export interface INameSearchComponentProps {
  loaderContext: LoaderContextProps;
  projectInfo: IProjectMaster;
  setSelectedUserForAllocationModal: React.Dispatch<
    React.SetStateAction<IAllUserAllocationEntries>
  >;
  setOpenAllocationModal: React.Dispatch<React.SetStateAction<boolean>>;
  allUserAllocationEntries: IAllUserAllocationEntries[];
  baseType: EAllocationType;
  baseStartEndDateToConsiderForDefaultAllocationEntry: {
    startDate: Date;
    endDate: Date;
    noOfHours: number;
    isPerDayHourAllocation: boolean;
  };
  updateUserEntriesOnJobCodeChangeSameTeam: (
    selectedJobCodeIfAny: string
  ) => void;
  listOfJobCodes: string[];
}

export enum ESearchTypeOptions {
  Name = "Name",
  SameTeam = "Same Team",
}

export const SearchTypeOptions = [
  ESearchTypeOptions.Name,
  ESearchTypeOptions.SameTeam,
];

const NameSearchComponent = (props: INameSearchComponentProps) => {
  const [searchType, setSearchType] = useState<string>(
    ESearchTypeOptions.SameTeam
  );
  const [selectedJobCode, setSelectedJobCode] = useState<string>("");
  const [isSameTeamAllocationAllowed, setIsSameTeamAllocationAllowed] =
    useState<boolean>(false);
  const employeeSelected = (user: any) => {
    if (user) {
      const userAlreadyPresentInTimeline = props.allUserAllocationEntries.find(
        (entry) =>
          entry.email.toLowerCase().trim() === user.emailId.toLowerCase().trim()
      );
      if (userAlreadyPresentInTimeline) {
        addUserForAllocationForm([userAlreadyPresentInTimeline]);
      } else {
        var newlyAddedUserAllocationEntries: IAllUserAllocationEntries[] =
          getUserAllocationEntry([user]);
        addUserForAllocationForm(newlyAddedUserAllocationEntries);
      }
    }
  };

  const getUserAllocationEntry = (user: any[]): IAllUserAllocationEntries[] => {
    const tempAllocationEntry: IAllUserAllocationEntries[] = user.map(
      (userEntry) => {
        return {
          email: userEntry.emailId,
          userInfo: {
            empName: userEntry.name,
            email: userEntry.emailId,
            grade: userEntry.grade,
            designation: userEntry.designation,
            location: userEntry?.location,
            supercoach: userEntry?.Supercoach,
            businessUnit: userEntry?.BusinessUnit,
            match_score: "",
            competency: userEntry?.competency,
            competencyId: userEntry?.competencyId,
          },
          type: EAllocationType.NAME_ALLOCATION,
          skills: userEntry.skills ? userEntry.skills : [],
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
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .startDate,
              [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]:
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .endDate,
              [EAllocationsBreakupVariables.ConfirmedPerDayHours]:
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .noOfHours,
              [EAllocationsBreakupVariables.PerDayAllocation]:
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .isPerDayHourAllocation,
            },
          ],
          isContinuousAllocation: true,
          description: "",
          totalEfforts: props
            .baseStartEndDateToConsiderForDefaultAllocationEntry
            .isPerDayHourAllocation
            ? props.baseStartEndDateToConsiderForDefaultAllocationEntry
                .noOfHours *
              calculateDaysBetweenTwoDates(
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .startDate,
                props.baseStartEndDateToConsiderForDefaultAllocationEntry
                  .endDate
              )
            : props.baseStartEndDateToConsiderForDefaultAllocationEntry
                .noOfHours,
          isInDraftMode: true,
          showSkills: true,
          showDescription: true,
          projectInfo: props.projectInfo,
          isUpdateAllowed: true,
          isPreviouslyDraft: false,
        };
      }
    );
    return tempAllocationEntry;
  };

  const addUserForAllocationForm = async (
    newlyAddedUserAllocationEntries: IAllUserAllocationEntries[]
  ) => {
    props.setSelectedUserForAllocationModal(newlyAddedUserAllocationEntries[0]);
    props.setOpenAllocationModal(true);
  };

  const onChangeForDifferentTypeSelections = (e: any) => {
    if (!e || e === null) {
      resetBaseSearchType();
    } else {
      setSearchType(e);
    }
  };

  const resetBaseSearchType = () => {
    setSearchType(
      props.baseType === EAllocationType.SAME_TEAM_ALLOCATION
        ? ESearchTypeOptions.SameTeam
        : ESearchTypeOptions.Name
    );
  };

  useEffect(() => {
    resetBaseSearchType();
    if (
      props.projectInfo?.projectType &&
      props.projectInfo?.projectType.toLowerCase() ===
        EProjectType.RECURRING.toLowerCase()
    ) {
      setIsSameTeamAllocationAllowed(true);
    } else {
      setIsSameTeamAllocationAllowed(false);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.baseType, props.projectInfo]);

  return (
    <Employeesearch
      employeeSelected={employeeSelected}
      showDifferentTypeSelections={true}
      valueForDifferentTypeSelections={searchType}
      optionsForDifferentTypeSelections={
        isSameTeamAllocationAllowed
          ? SearchTypeOptions
          : [ESearchTypeOptions.Name]
      }
      optionsForSameTypeSearch={props.listOfJobCodes}
      onChangeForDifferentTypeSelections={function (e: any): void {
        onChangeForDifferentTypeSelections(e);
      }}
      onChangeForSameTeamJobCode={(e) => {
        setSelectedJobCode(e);
        props.updateUserEntriesOnJobCodeChangeSameTeam(e);
      }}
      selectedJobCode={selectedJobCode}
    />
  );
};
export default NameSearchComponent;
