export enum RolesListMaster {
  Employee = "Employee",
  ResourceRequestor = "ResourceRequestor",
  Resource_Requestor = "Resource Requestor",
  Delegate = "Delegate",
  Admin = "Admin",
  CEOCOO = "CEOCOO",
  Reviewer = "Reviewer",
  Leaders = "Leaders",
  SystemAdmin = "SystemAdmin",
  AdditionalEl = "AdditionalEl",
  AdditionalDelegate = "AdditionalDelegate",
  EngagementLeader = "EngagementLeader",
  LeadGenerator = "LeadGenerator",
  JobManager = "JobManager",
  SMEGLeader = "SMEGLeader",
  ProposedCSP = "ProposedCSP",
  ProposedEL = "ProposedEL",
  FindingPartner = "FindingPartner",
  EO = "EO",
  CSL = "CSL",
  AssignmentIncharge = "AssignmentIncharge",
  CSP = "CSP",
  SuperCoach = "SuperCoach",
  CSC = "CSC",
  SkillSuperCoach = "SkillSuperCoach"
}
export enum EProjectRequisitionAllocationStatus {
  PENDING = "Pending",
  Completed = "Completed",
  ToBeStarted = "To be started",
}

export const ResourceRequestorsList: RolesListMaster[] = [
  RolesListMaster.EngagementLeader,
  RolesListMaster.EO,
  RolesListMaster.JobManager,
  RolesListMaster.ProposedEL,
  RolesListMaster.ResourceRequestor,
  RolesListMaster.Resource_Requestor,
];

export const ReviewersList: RolesListMaster[] = [
  RolesListMaster.CSP,
  RolesListMaster.SMEGLeader,
  RolesListMaster.ProposedCSP,
];
