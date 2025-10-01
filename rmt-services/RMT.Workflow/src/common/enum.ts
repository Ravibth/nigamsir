export enum EMicroServicesNames {
  SERVICE_LAYER = "SERVICE_LAYER",
  CONFIGURATION = "CONFIGURATION",
  IDENTITY = "IDENTITY",
}

export const EMicroservicesCommand = {
  [EMicroServicesNames.SERVICE_LAYER]: {
    sendEmail: { cmd: "sendEmail" },
    health: { cmd: "health" },
  },
  [EMicroServicesNames.CONFIGURATION]: {
    getEmailTemplate: { cmd: "findEmailTemplate" },
  },
  [EMicroServicesNames.IDENTITY]: {
    getUserDetailsByEmail: { cmd: "getUserDetailsByEmail" },
  },
};

export enum RolesListMaster {
  Employee = "Employee",
  ResourceRequestor = "ResourceRequestor",
  Delegate = "Delegate",
  Admin = "Admin",
  CEOCOO = "CEOCOO",
  Reviewer = "Reviewer",
  Leaders = "Leaders",
  SystemAdmin = "SystemAdmin",
  AdditionalEl = "AdditionalEl",
  AdditionalDelegate = "AdditionalDelegate",
  EngagementLeader = "EngagementLeader",
}
