import { IAllocateFormSkills } from "../common-allocation/common-allocation-modal-form/utils";

export interface IBulkUploadData {
  pipelineCode: string;
  jobCode: string;
  // projectCode: string;
  projectName: string;
  projectStartDate: Date;
  projectEndDate: Date;
  requisitionDescription: string;
  designationId: string;
  designation: string;
  startDate: Date;
  endDate: Date;
  emailId: string;
  empCode: number;
  locationCode: string;
  smegId: string;
  subIndustryWeight: string;
  smegWeightage: string;
  subIndustryID: string;
  sameClientExperienceWeightage: string;
  locationWeightage: string;
  skillWeightage: string;
  parameters: [
    {
      name: string;
      priority: string;
      value: number;
    }
  ];
  numberOfResources: string;
  numberOfHours: number;
  perDay: string;
  skills: string[];
  skillList: IAllocateFormSkills[];
  totalHours: number;
  RequisitionStatus: string;
  pipelineName: string;
  empName: string;
  status: boolean;
  comments: string;
  competency: string;
  competencyId: string;
}
