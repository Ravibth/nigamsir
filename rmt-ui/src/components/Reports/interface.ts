import { IReportDashboardFilterControl } from './Filters/uitls';

export interface ISelectedChartData {
    rowName: string;
    chartName: string;
}

export interface IReportAndDashboardFilter {
    isFilterOpen: boolean;
    toggleValue: any;
    setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
    filterParameters: IReportDashboardFilterControl;
    setFilterParameters: React.Dispatch<React.SetStateAction<IReportDashboardFilterControl>>;
    GetFilterDefaultValueOnTheBasisOfRole: (userRole?: string) => IReportDashboardFilterControl;
    isEmployeeViewGraph: boolean;
}
