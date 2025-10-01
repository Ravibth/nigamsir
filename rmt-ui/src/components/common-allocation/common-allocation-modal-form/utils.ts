import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import { ISkillsMaster } from "../../../common/interfaces/ISkillsMaster";
import { isAllocationHoursAvilable } from "../../../services/allocation/allocation.service";
import { calculateDaysBetweenTwoDates } from "../../../utils/date/dateHelper";
import { IAllocation } from "../../update-allocation/entity/IAllocations";

export enum EAvailabilityErrorMessage {
  NotAvailable = "User is not available in above days.",
}

export enum EAllocateEmployeeFormDetailVariables {
  Description = "description",
  ContinuousAllocation = "continuousAllocation",
  TotalEfforts = "totalEfforts",
  Allocations = "Allocations",
  Skills = "Skills",
  Competency = "Competency",
}

export const AllocateEmployeeFormDetailVariables = [
  EAllocateEmployeeFormDetailVariables.Description,
  EAllocateEmployeeFormDetailVariables.ContinuousAllocation,
  EAllocateEmployeeFormDetailVariables.TotalEfforts,
  EAllocateEmployeeFormDetailVariables.Allocations,
  EAllocateEmployeeFormDetailVariables.Skills,
  EAllocateEmployeeFormDetailVariables.Competency,
];

export enum EAllocationsBreakupVariables {
  ConfirmedAllocationStartDate = "confirmedAllocationStartDate",
  ConfirmedAllocationEndDate = "confirmedAllocationEndDate",
  ConfirmedPerDayHours = "confirmedPerDayHours",
  PerDayAllocation = "perDayAllocation",
}

export const AllocationsBreakupVariables = [
  EAllocationsBreakupVariables.ConfirmedAllocationStartDate,
  EAllocationsBreakupVariables.ConfirmedAllocationEndDate,
  EAllocationsBreakupVariables.ConfirmedPerDayHours,
  EAllocationsBreakupVariables.PerDayAllocation,
];

export interface FormValuesForAllocationBreakup {
  [EAllocationsBreakupVariables.ConfirmedAllocationStartDate]: Date;
  [EAllocationsBreakupVariables.ConfirmedAllocationEndDate]: Date;
  [EAllocationsBreakupVariables.ConfirmedPerDayHours]: number;
  [EAllocationsBreakupVariables.PerDayAllocation]: boolean;
}

export interface FormValuesForAllocationForm {
  [EAllocateEmployeeFormDetailVariables.Description]: string;
  [EAllocateEmployeeFormDetailVariables.ContinuousAllocation]: boolean;
  [EAllocateEmployeeFormDetailVariables.TotalEfforts]: number;
  [EAllocateEmployeeFormDetailVariables.Allocations]: FormValuesForAllocationBreakup[];
  [EAllocateEmployeeFormDetailVariables.Skills]: IAllocateFormSkills[];
  [EAllocateEmployeeFormDetailVariables.Competency]: ICompetencyMaster;
}

export const GetTotalEffort = (
  allocations: FormValuesForAllocationBreakup[]
): number => {
  let count: number = 0;
  allocations.forEach((allocation) => {
    const startDate =
      allocation[EAllocationsBreakupVariables.ConfirmedAllocationStartDate];
    const endDate =
      allocation[EAllocationsBreakupVariables.ConfirmedAllocationEndDate];
    const perDayAllocation =
      allocation[EAllocationsBreakupVariables.PerDayAllocation];
    let ConfirmedPerDayHours =
      allocation[EAllocationsBreakupVariables.ConfirmedPerDayHours];
    if (startDate && endDate) {
      let totalWorkingDays = calculateDaysBetweenTwoDates(startDate, endDate);
      if (totalWorkingDays > 0 && perDayAllocation) {
        count +=
          parseInt(totalWorkingDays.toString()) *
          parseInt(ConfirmedPerDayHours.toString());
      } else if (totalWorkingDays > 0) {
        count += parseInt(ConfirmedPerDayHours.toString());
      }
    }
  });
  return count;
};

export interface IMinMaxDataForAllocationEntry {
  startDate: {
    min: Date;
    max: Date;
  };
  endDate: {
    min: Date;
    max: Date;
  };
  efforts: {
    min: number;
    max: number;
  };
}
export interface IisAllocationHoursAvailableResponse {
  emailId?: string;
  endDate?: Date;
  errorMsg?: string;
  isHoursAvialable?: boolean;
  isPerDayHourAllocation?: boolean;
  requireWorkingHours?: number;
  requisitionId?: number;
  startDate?: Date;
  totalAvaibleHours?: number;
  totalWorkingDays?: number;
  totalWorkingHours?: any;
}

export const CheckUserAvailabilityForDatesAndEffortsUtils = (
  email: string[],
  startDate: Date,
  endDate: Date,
  confirmedPerDayHours: number,
  isPerDayHourAllocation: boolean,
  requisitionId?: string,
  pipelineCode?: string,
  jobCode?: string
): Promise<IisAllocationHoursAvailableResponse[]> => {
  const allocation: IAllocation = {
    confirmedAllocationStartDate: startDate,
    confirmedAllocationEndDate: endDate,
    confirmedPerDayHours: confirmedPerDayHours,
    isPerDayHourAllocation: isPerDayHourAllocation,
    pipelineCode: pipelineCode,
    jobCode: jobCode,
  };
  return new Promise<IisAllocationHoursAvailableResponse[]>(
    (resolve, reject) => {
      isAllocationHoursAvilable(
        requisitionId?.toString() || "",
        email,
        allocation
      )
        .then((response) => {
          resolve(response.data);
        })
        .catch((err) => {
          reject(err);
        });
    }
  );
};

export interface IAllocateFormSkills {
  skillName: string;
  skillCode: string;
  skill?: ISkillsMaster;
}

export const GetSkillMaster = (): string[] => {
  return [
    "Dotnet",
    "Azure",
    "Aws",
    "Google cloud",
    "Python",
    "C#",
    "Entity Framework",
    "SQL",
    "Html",
    "CSS",
  ];
};
