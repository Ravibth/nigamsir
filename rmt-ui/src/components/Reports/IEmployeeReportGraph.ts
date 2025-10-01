export interface IEmployeeReportGraph {
  netCapacityCurrent: number;
  netCapacityPrevious: number;
  //totalAllocatedCurrent: number;
  //totalActualPrevious: number;
  //allocationPercentCurrent: number;
  //actualPercentPrevious: number;
  //chargeableAllocatedCurrent: number;
  //chargeableActualPrevious: number;
  // nonChargeableAllocatedCurrent: number;
  //nonChargeableActualPrevious: number;

  //netCapacity: number;
  totalActual: number;
  totalAllocated: number;
  chargeableAllocated: number;
  nonChargeableAllocated: number;
  chargeableActual: number;
  nonChargeableActual: number;
  chargiablityAllocatedPercent: number;
  chargeablityActualPercent: number;
  allocationPercent: number;
  actualPercent: number;
}
