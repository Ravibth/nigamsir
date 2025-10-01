import React, { memo } from "react";
import { IAllUserAllocationEntries } from "../interface";
import BackDropModal from "../../../common/back-drop-modal/backDropModal";
import MainAllocationForm from "./main-allocation-form";
import { IProjectMaster } from "../../../common/interfaces/IProject";
import {
  EAllocateEmployeeFormDetailVariables,
  FormValuesForAllocationForm,
} from "./utils";
import { EAllocationType } from "../enum";

export interface ICommonAllocationMainProps {
  openAllocationModal: boolean;
  setOpenAllocationModal: (e: boolean) => {};
  selectedUserForAllocationModal: IAllUserAllocationEntries;
  projectInfo: IProjectMaster;
  updateAllocationForEmployee: (
    updatedEntry: IAllUserAllocationEntries
  ) => void;
  addNameAllocationUsersToTimeline: (
    newlyAddedUserAllocationEntries: IAllUserAllocationEntries[]
  ) => Promise<void>;
  baseStartEndDateToConsiderForDefaultAllocationEntry: {
    startDate: Date;
    endDate: Date;
    noOfHours: number;
    isPerDayHourAllocation: boolean;
  };
}
const CommonAllocationModalForm = (props: ICommonAllocationMainProps) => {
  const closeModel = () => {
    props.setOpenAllocationModal(false);
  };

  const submitAllocationForm = (
    formData: FormValuesForAllocationForm
  ): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      const updatedEntryInfo: IAllUserAllocationEntries = {
        ...props.selectedUserForAllocationModal,
        skills: formData[EAllocateEmployeeFormDetailVariables.Skills],
        description: formData[EAllocateEmployeeFormDetailVariables.Description],
        allocations: formData[EAllocateEmployeeFormDetailVariables.Allocations],
        isContinuousAllocation:
          formData[EAllocateEmployeeFormDetailVariables.ContinuousAllocation],
        totalEfforts:
          formData[EAllocateEmployeeFormDetailVariables.TotalEfforts],
        isInDraftMode: false,
        available: true,
      };

      if (
        props.selectedUserForAllocationModal.isInDraftMode &&
        props.selectedUserForAllocationModal.type ===
          EAllocationType.NAME_ALLOCATION
      ) {
        props.addNameAllocationUsersToTimeline([updatedEntryInfo]);
      } else {
        props.updateAllocationForEmployee(updatedEntryInfo);
      }
      closeModel();
      resolve(true);
    });
  };

  return props.openAllocationModal ? (
    <BackDropModal
      open={props.openAllocationModal}
      onclose={function (): {} {
        closeModel();
        return;
      }}
      style={{ overflowY: "auto", maxHeight: "85vh" }}
    >
      <MainAllocationForm
        {...props}
        closeModel={closeModel}
        submitAllocationForm={submitAllocationForm}
      />
    </BackDropModal>
  ) : (
    <></>
  );
};
export default memo(CommonAllocationModalForm);
