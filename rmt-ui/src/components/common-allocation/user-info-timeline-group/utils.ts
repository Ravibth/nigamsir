import {
  EAllocationsBreakupVariables,
  FormValuesForAllocationBreakup,
} from "../common-allocation-modal-form/utils";
import { IUserSelectedAllocationContext } from "../context/users-selected-allocation";
import { IAllUserAllocationEntries } from "../interface";

const objectsAreEqual = (
  obj1: FormValuesForAllocationBreakup,
  obj2: FormValuesForAllocationBreakup
): boolean => {
  const resp =
    new Date(
      obj1[EAllocationsBreakupVariables.ConfirmedAllocationStartDate]
    ).getTime() ===
      new Date(
        obj2[EAllocationsBreakupVariables.ConfirmedAllocationStartDate]
      ).getTime() &&
    new Date(
      obj1[EAllocationsBreakupVariables.ConfirmedAllocationEndDate]
    ).getTime() ===
      new Date(
        obj2[EAllocationsBreakupVariables.ConfirmedAllocationEndDate]
      ).getTime() &&
    obj1[EAllocationsBreakupVariables.ConfirmedPerDayHours].toString() ===
      obj2[EAllocationsBreakupVariables.ConfirmedPerDayHours].toString() &&
    obj1[EAllocationsBreakupVariables.PerDayAllocation] ===
      obj2[EAllocationsBreakupVariables.PerDayAllocation];
  return resp;
};

export const checkIfPrevAllocIsSameAsNewAllocation = (
  userAllocationEntryInput: IAllUserAllocationEntries,
  usersSelectedAllocationContext: IUserSelectedAllocationContext
) => {
  if (
    userAllocationEntryInput.allocations.length !==
    userAllocationEntryInput.baseAllocations.length
  ) {
    return false;
  }
  let foundOddOneOut = false;
  for (let i = 0; i < userAllocationEntryInput.allocations.length; i++) {
    const userAllocationEntry =
      usersSelectedAllocationContext.allUserSelectionEntries.find(
        (user) => user.email === userAllocationEntryInput.email
      );
    if (
      userAllocationEntry?.allocations[i] &&
      userAllocationEntry?.allocations[i]
    ) {
      if (userAllocationEntry) {
        const isEqual = objectsAreEqual(
          userAllocationEntry?.allocations[i],
          userAllocationEntry?.baseAllocations[i]
        );
        if (!isEqual) {
          foundOddOneOut = true;
        }
      }
    } else {
      return false;
    }
  }
  return !foundOddOneOut;
};
