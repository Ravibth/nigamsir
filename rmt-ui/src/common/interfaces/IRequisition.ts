export interface IRequisitionMaster {
  id: string;
  requisitionDemand: string;
  pipelineCode: string;
  competency: string;
  competencyId: string;
  offerings: string;
  solutions: string;
  jobCode: string;
  pipelineName: string;
  jobName: string;
  clientName: string;
  startDate: Date;
  endDate: Date;
  effortsPerDay: number;
  totalHours: number;
  requisitionStatus: string;
  expertise?: string;
  designation: string;
  grade?: string;
  description: string;
  isPerDayHourAllocation: boolean;
  businessUnit: string;
  smeg?: string;
  isActive: boolean;
  createdBy: string;
  modifiedBy: string;
  createdAt: Date;
  modifiedAt: Date;
  requisitionTypeId: number;
  requisitionType: {
    id?: number;
    type?: string;
  };
  demands: IRequisitionDemandMaster;
  requisitionParameters: IRequisitionParametersMaster[];
  requisitionSkill: IRequisitionSkillMaster[];
  requisitionParameterValues: IRequisitionParameterValues[];
}

export interface IRequisitionParametersMaster {
  id: string;
  requisitionId: string;
  category: string;
  requisitionWeight: number;
  isChecked: boolean;
  Requisition?: IRequisitionMaster;
}
export interface IRequisitionParameterValues {
  id: string;
  requisitionId: string;
  parameter: string;
  value: string;
  requisition: IRequisitionMaster;
}

export interface IRequisitionSkillMaster {
  id: string;
  requisitionId: string;
  skillName: string;
  skillCode: string;
  type: string;
  requisition: IRequisitionMaster;
}

export interface IRequisitionDemandMaster {
  id: string;
  totalDemands: number;
  pendingDemands: number;
  allResourcesHaveSameDetails: boolean;
  //requisitions stores the pending requisition ids
  requisitions: IRequisitionMaster[];
}
