import * as service from "../../services/allocation/allocation.service";
import { IAllocation, IAllocationDetails } from "./entity/IAllocations";
import * as GC from "../../global/constant";
import * as GUtil from "../../global/utils";
import { getTaskDetailsByItemGuid } from "../../services/allocation/workflow-service";

export const calculateValues = (customAllocations: IAllocation[]) => {
  let weekdaysCountfinal = 0;
  let totalEffortsfinal = 0;
  const oneDay = 24 * 60 * 60 * 1000;
  customAllocations.forEach((alloc: IAllocation) => {
    const tt = alloc.confirmedAllocationStartDate;
    if (
      alloc.confirmedAllocationStartDate &&
      alloc.confirmedAllocationEndDate
    ) {
      const start: any = new Date(alloc.confirmedAllocationStartDate);
      const end: any = new Date(alloc.confirmedAllocationEndDate?.toString());
      const totalDays = Math.round(Math.abs((end - start) / oneDay)) + 1;
      let weekdaysCount = 0;
      let currentDate = new Date(alloc.confirmedAllocationStartDate);
      for (let i = 0; i < totalDays; i++) {
        const dayOfWeek = currentDate.getDay();
        if (dayOfWeek !== 0 && dayOfWeek !== 6) {
          weekdaysCount++;
        }
        currentDate.setTime(currentDate.getTime() + oneDay);
      }
      weekdaysCountfinal += weekdaysCount;
      if (alloc.totalEfforts)
        totalEffortsfinal += weekdaysCount * alloc.totalEfforts;
    }
  });
  return {
    totalEffortsfinal: totalEffortsfinal,
    weekdaysCountfinal: weekdaysCountfinal,
  };
};

// Not in use
// export const getAllocationDetailsByService = (id: string) => {
//   return new Promise((resolve, rejects) => {
//     service
//       .getAllocationById(id)
//       .then((resp: any) => {
//         return resolve(resp.data);
//       })
//       .catch((ex) => {
//         throw ex;
//       });
//   });
// };


export const getAllocationDetails = async (
  resoureAllocations: IAllocationDetails[]
): Promise<IAllocation[]> => {
  const allocation: IAllocation[] = [];
  let idx = 0;
  (await resoureAllocations) &&
    resoureAllocations.forEach((data) => {
      allocation.push({
        confirmedAllocationStartDate: data.confirmedAllocationStartDate,
        confirmedAllocationEndDate: data.confirmedAllocationEndDate,
        totalWorkingDays: data.totalWorkingDays,
        confirmedPerDayHours: data.confirmedPerDayHours,
        totalEfforts: data.totalEfforts,
        isactive: data.isActive,
        id: data.id,
        index: idx,
        isPerDayHourAllocation: data.isPerDayHourAllocation,
      });
      idx++;
    });
  return allocation;
};

export const getTotalEffort = (
  resoureAllocations: IAllocation[],
  currentAllocation: IAllocation
) => {
  let totalefforts: number = 0;
  resoureAllocations?.length > 0 &&
    resoureAllocations
      .filter(
        (a: IAllocation) => a.isactive && a.index !== currentAllocation.index
      )
      .forEach((item: IAllocation) => {
        if (
          item.isPerDayHourAllocation &&
          item.confirmedPerDayHours &&
          item.totalWorkingDays
        ) {
          totalefforts +=
            item.totalWorkingDays *
            parseInt(item.confirmedPerDayHours?.toString());
        } else if (item.confirmedPerDayHours) {
          totalefforts += parseInt(item.confirmedPerDayHours?.toString());
        }
      });
  return totalefforts;
};

export const isVaildDate = (
  resoureAllocations: IAllocation[],
  index: number
) => {
  let _validDate: boolean = true;
  const currentAllocation: IAllocation = resoureAllocations[index];
  resoureAllocations.forEach((item: IAllocation, idx: number) => {
    if (idx !== index) {
      if (
        currentAllocation.confirmedAllocationStartDate &&
        currentAllocation.confirmedAllocationEndDate &&
        item.confirmedAllocationStartDate &&
        item.confirmedAllocationEndDate
      ) {
        if (
          new Date(currentAllocation.confirmedAllocationStartDate) >=
            new Date(item.confirmedAllocationStartDate) &&
          new Date(currentAllocation.confirmedAllocationEndDate) <=
            new Date(item.confirmedAllocationEndDate)
        ) {
          _validDate = false;
          return _validDate;
        }
      }
    }
  });
  return _validDate;
};

export const isEnableAllocateBtn = () => {
  return true;
};
export const isWorkflowRunning = (guid: any): Promise<boolean> => {
  return new Promise((resolve, rejects) => {
    getTaskDetailsByItemGuid(guid)
      .then((resp: any) => {
        if (resp.status === 200) {
          if (
            resp.data.filter((a: any) => a.outcome === GC.OUTCOME.inprogress)
              ?.length > 0
          ) {
            return resolve(true);
          }
          return resolve(false);
        }
        return resolve(false);
      })
      .catch((ex) => {
        return rejects(ex);
      });
  });
};
