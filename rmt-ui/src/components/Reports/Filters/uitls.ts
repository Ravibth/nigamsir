import { SxProps } from "@mui/material";
import { IBUTreeMapping } from "../../../common/interfaces/IBuTreeMapping";
import { IWCGTLocationMaster } from "../../../common/interfaces/IWCGTLocationMaster";
import {
  getBU_Exp_SME_RUFromWcgt,
  getLocationMasterFromWCGT,
  getDesignationList,
  getAllCompetency,
} from "../../../services/wcgt-master-services/wcgt-master-services";
import { ICompetencyMaster } from "../../../common/interfaces/ICompetencyMaster";
import { IDesignations } from "../../../common/interfaces/IDesignations";

export enum EReportDashboardFilterControl {
  businessUnit = "businessUnit",
  offering = "offering",
  solution = "solution",
  competency = "competency",
  expertise = "expertise",
  smeg = "smeg",
  location = "location",
  start_date = "startDate",
  end_date = "endDate",
  emailId = "emailId",
  designation = "designation",
  grade = "grade",
}

export enum EXAxisGroupKey {
  businessUnit = "business_unit",
  expertise = "expertise",
  sme_group_name = "sme_group_name",
  competency = "competency",
}

export interface IReportDashboardFilterOptions {
  businessUnit: string[];
  expertise: string[];
  smeg: string[];
  location: string[];
  designation?: string[];
  grade?: [];
  // start_date?: Date | null;
}
export interface IReportDashboardFilterControl {
  [EReportDashboardFilterControl.businessUnit]: string[];
  [EReportDashboardFilterControl.expertise]: string[];
  [EReportDashboardFilterControl.smeg]: string[];
  [EReportDashboardFilterControl.location]: string[];
  [EReportDashboardFilterControl.start_date]?: Date | null; //should not be null
  [EReportDashboardFilterControl.end_date]?: Date | null; //should not be null
  [EReportDashboardFilterControl.emailId]?: string | null;
  [EReportDashboardFilterControl.designation]?: string[];
  [EReportDashboardFilterControl.grade]?: string[];
}

export const filterIconButton: SxProps = {
  color: "#4f2d7f",
  fontSize: "14px",
  textTransform: "initial",
  borderRadius: "40px",
  borderColor: "#B8B8B8",
};

export const DividerSxProps: SxProps = {
  borderBottomWidth: 2,
  margin: "10px",
};

export const loadBUTreeMappingData = () => {
  return new Promise<IBUTreeMapping[]>((resolve, reject) => {
    getBU_Exp_SME_RUFromWcgt()
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
  });
};
export const loadCompetencyMappingData = () => {
  return new Promise<ICompetencyMaster[]>((resolve, reject) => {
    getAllCompetency()
      .then((response) => {
        resolve(response);
      })
      .catch((err) => {
        reject(err);
      });
  });
};
export const loadLocationMaster = () => {
  return new Promise<IWCGTLocationMaster[]>((resolve, reject) => {
    getLocationMasterFromWCGT()
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

export const designationMaster = () => {
  return new Promise<IDesignations[]>((resolve, reject) => {
    getDesignationList()
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

export const currentBuLeaderMaster = (
  buTreeMapping: any[],
  currentUserLeaderRole: any
) => {
  let result = [];

  if (currentUserLeaderRole && currentUserLeaderRole.bu) {
    const currentUserBUs = Object.keys(currentUserLeaderRole.bu);
    const absentBu = currentUserBUs.filter(
      (bu) =>
        !result.some(
          (r) => r.bu_id?.toLowerCase().trim() === bu.toLowerCase().trim()
        )
    );
    const busToAdd = buTreeMapping.filter((mapping) =>
      absentBu.includes(mapping.bu_id)
    );
    result = [...result, ...busToAdd];
  }
  if (currentUserLeaderRole && currentUserLeaderRole.offerings) {
    const currentUserOfferings = Object.keys(currentUserLeaderRole.offerings);
    const absentOffering = currentUserOfferings.filter(
      (offering) =>
        !result.some(
          (r) =>
            r.offering_id?.toLowerCase().trim() ===
            offering.toLowerCase().trim()
        )
    );
    const offeringToAdd = buTreeMapping.filter((mapping) =>
      absentOffering.includes(mapping.offering_id)
    );
    result = [...result, ...offeringToAdd];
  }
  if (currentUserLeaderRole && currentUserLeaderRole.solutions) {
    const currentUserSolutions = Object.keys(currentUserLeaderRole.solutions);
    const absentSolutions = currentUserSolutions.filter(
      (solution) =>
        !result.some(
          (r) =>
            r.solution_id?.toLowerCase().trim() ===
            solution.toLowerCase().trim()
        )
    );
    const solutionToAdd = buTreeMapping.filter((mapping) =>
      absentSolutions.includes(mapping.solution_id)
    );

    result = [...result, ...solutionToAdd];
  }
  return result;
};

export const currentCompetencyLeaderMaster = (
  competencyMasterList: ICompetencyMaster[],
  currentUserLeaderRole: any,
  competencyLeaderRole: ICompetencyMaster[],
  buTreeMappingMaster: IBUTreeMapping[]
) => {
  let result: ICompetencyMaster[] = [];
  if (currentUserLeaderRole && currentUserLeaderRole.bu) {
    const currentUserBUs = Object.keys(currentUserLeaderRole.bu);
    const absentBu = currentUserBUs.filter(
      (bu) =>
        !result.some(
          (r) => r.buId.toLowerCase().trim() === bu.toLowerCase().trim()
        )
    );
    const buToAdd = competencyMasterList.filter((m) =>
      absentBu.includes(m.buId)
    );
    result = [...result, ...buToAdd];
  }
  if (competencyLeaderRole && competencyLeaderRole.length > 0) {
    const absentCompetencies = competencyLeaderRole.filter(
      (cl) =>
        !result.some(
          (r) =>
            r.competencyId.toLowerCase().trim() ===
            cl.competencyId.toLowerCase().trim()
        )
    );
    result = [...result, ...absentCompetencies];
  }
  return result;
};
