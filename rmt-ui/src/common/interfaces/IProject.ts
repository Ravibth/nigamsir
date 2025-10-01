export interface IProjectMaster {
  id?: number;
  // projectCode?: string;
  // projectName?: string;
  jobCode?: string;
  jobId?: string;
  jobName?: string;
  pipelineCode?: string;
  pipelineName?: string;
  clientName?: string;
  clientGroup?: string;
  expertise?: string;
  sme?: string;
  startDate?: Date;
  endDate?: Date;
  createdDate?: Date;
  description?: string;
  projectAllocationStatus?: string;
  projectClosureState?: boolean;
  projectActivationStatus?: boolean;
  location?: string;
  pipelineStage?: string;
  projectType?: string;
  pipelineStatus?: string;
  chargableType?: string;
  revenueUnit?: string;
  industry?: string;
  subindustry?: string;
  budgetStatus?: string;
  projectFulFilledDemands?: number;
  marketClosed?: boolean;
  isActive?: boolean;
  justificationToAllocate?: string;
  createdBy?: string;
  modifiedBy?: string;
  isRollover?: boolean;
  rolloverDays?: number;
  bu?: string;
  offerings?: string;
  solutions?: string;
  createdAt?: Date;
  modifiedAt?: Date;
  projectDemands?: IProjectDemandMaster[];
  projectJobCodes?: IProjectJobCodesMaster[];
  projectRoles?: IProjectRolesMaster[];
  projectRolesView?: IProjectRolesMaster[]; // Modify this view
  projectRequisitionAllocations?: IProjectRequisitionAllocation;
  isPublishedToMarketPlace?: boolean;
}

export interface IProjectDemandMaster {
  id?: number;
  projectId?: number;
  project?: any;
  designation?: string;
  noOfResources?: number;
  isActive?: boolean;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt?: Date;
  projectDemandSkills?: any;
}
export interface IProjectJobCodesMaster {
  id?: number;
  projectId?: number;
  project?: any;
  // projectCode: string;
  pipelineCode: string;
  jobCode: string;
  jobName: string;
  isActive?: boolean;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt?: Date;
}
export interface IProjectRolesMaster {
  id?: number;
  projectId?: number;
  project?: any;
  user?: string;
  userName?: string;
  role?: string;
  description?: string;
  isActive?: boolean;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt?: Date;
  delegateEmail?: string;
}
export interface IProjectRequisitionAllocation {
  id?: number;
  projectId?: number;
  // projectCode?: string;
  pipelineCode?: string;
  jobCode?: string;
  requisitionCount: number;
  allocationCount: number;
  status?: string;
  createdBy?: string;
  modifiedBy?: string;
  createdAt?: Date;
  modifiedAt?: Date;
}

export interface ProjectRole {
  id: number;
  projectId: number;
  user: string;
  userName: string;
  role: string;
  applicationRole: string;
  description: string | null;
  delegateUserName: string | null;
  delegateEmail: string | null;
  isActive: boolean;
  createdBy: string | null;
  modifiedBy: string | null;
  createdAt: string | null;
  modifiedAt: string | null;
}
