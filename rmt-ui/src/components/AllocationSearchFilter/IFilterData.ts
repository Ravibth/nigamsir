export interface IFilterData {
  job: Array<string>;
  locations: Array<string>;
  startDate: string;
  endDate: string;
  email: Array<string>;
  BusinessUnit?: Array<string>;
  experties: Array<string>;
  smeg?: Array<string>;
}

// export interface IProjectJobCodes {
//   createdAt: string;
//   createdBy: string;
//   id: number;
//   isActive: boolean;
//   jobCode: string;
//   jobName: string;
//   modifiedAt: string;
//   modifiedBy: string;
//   pipelineCode: string;
//   projectCode: string;
//   projectId: number;
// }

export interface IProjectRoles {
  id: number;
  projectId: number;
  user: string;
  userName: string;
  role: string;
  isActive: boolean;
  description: string;
  createdAt: string;
  createdBy: string;
  modifiedAt: string;
  modifiedBy: string;
}
