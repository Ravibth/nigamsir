export interface IProjectViewProps {
  PipelineCode: string;
  JobCode: string;
  ProjectType: string;
  Client: string;
  Expertise: string;
  Sme: string;
  BudgetStatus: string;
  // StartDate: string;
  // EndDate: string;
  StartDate: Date;
  EndDate: Date;
  jobCode: any;
  setIsLoading: any;
  isLoading: any;
  closeHandler: Function;
  role?: string[];
}
