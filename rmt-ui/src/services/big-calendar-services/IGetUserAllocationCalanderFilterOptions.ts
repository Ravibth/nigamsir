export interface IGetUserAllocationCalander {
  startDate: string;
  endDate: string;
  filterParameters: IGetUserAllocationCalanderFilterOptions;
}

export interface IGetUserAllocationCalanderFilterOptions {
  pipelineCodes?: string[];
}
