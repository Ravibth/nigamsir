import axios from "axios";
import { PROJECT_SERVICES } from "../../global/service-constant";
import { createQueryUrl } from "../utils";
import { IProjectList } from "./IProjectList";
import { isEmpty, isNull } from "lodash";
import { isEmptyString } from "../../global/utils";

const baseurl = process.env.REACT_APP_PROJECT_MS;
// axios.get("http://localhost:7000/gateway/Project/GetProjectByCode", {
//   params: {
//     pipelineCode: "PC101",
//     jobCode: "PC%101"
//   }
// })
export const getAllProjectListByRequestorEmail = async (
  projectListRequest: IProjectList
) => {
  try {
    return await axios.post(
      baseurl + `Project/GetProjectListDataByUser`,
      projectListRequest
    );
  } catch (err) {
    throw err;
  }
};

export const getProjectCompleteDetails = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    let projectServiceUrl = `${baseurl}Project/GetProjectFullDetailsByPipelineCode?pipelineCode=${encodeURIComponent(
      pipelineCode
    )}`;
    if (!isEmptyString(jobCode)) {
      projectServiceUrl = `${projectServiceUrl}&jobCode=${encodeURIComponent(
        jobCode
      )}`;
    }
    return await axios.get(projectServiceUrl);
  } catch (err) {
    throw err;
  }
};

export const getAllProjectRolesByCodes = async (
  pipelineCode: string,
  jobCode: string,
  roles: []
) => {
  try {
    const payload: any = {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      roles,
    };

    return await axios
      .post(baseurl + "Project/GetAllProjectRolesByCodes", payload)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (err) {
    throw err;
  }
};

export const getBasicProjectDetailsRequestor = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    let _projectServiceUrl = `Project/GetBasiProjectDetailRequestorByPipelineCode?pipelineCode=${encodeURIComponent(
      pipelineCode
    )}`;
    if (!isEmptyString(jobCode)) {
      _projectServiceUrl = `Project/GetBasiProjectDetailRequestorByPipelineCode?pipelineCode=${encodeURIComponent(
        pipelineCode
      )}&jobCode=${encodeURIComponent(jobCode)}`;
    }
    const url = baseurl + _projectServiceUrl;

    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};

export const getProjectDetailsForEmployeeListingByPipelineCode = async (
  pipelineCodes: string[]
) => {
  try {
    return await axios.post(
      baseurl + `Project/GetEmployeeListingProjectDataByPipelineCodes/`,
      pipelineCodes
    );
  } catch (error) {
    throw error;
  }
};

export const UpdateProjectDetails = async (projectDetails: any) => {
  try {
    return await axios.post(
      baseurl + `project/UpdateProjectDetails`,
      projectDetails
    );
  } catch (error) {
    throw error;
  }
};

export const GetProjectByCode = async (
  pipelineCode: string,
  jobCode?: string
) => {
  try {
    if (!jobCode || jobCode === undefined || jobCode === "undefined") {
      jobCode = "";
    }
    const pipelineCode_enc = encodeURIComponent(pipelineCode);
    const jobCode_enc = encodeURIComponent(jobCode);
    const baseUrl = baseurl + `${PROJECT_SERVICES.getProjectByCode}`;
    const url = `${baseUrl}?pipelineCode=${pipelineCode_enc}&jobCode=${jobCode_enc}`;
    console.log(jobCode, jobCode_enc);
    console.log(url);
    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};

export const setIsRequisitionCreationAllowed = async (
  pipelineCode: any,
  jobCode: any,
  isRequisitionCreationAllowed: any
) => {
  try {
    const payload: any = {
      isRequisitionCreationallowed: isRequisitionCreationAllowed,
      pipelineCode: pipelineCode,
      jobCode: jobCode,
    };
    //todo: change local host url
    return await axios
      .post(baseurl + "Project/SetIsRequisitionCreationallowed", payload)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const getActiveFieldForMarketPlace = async () => {
  try {
    //todo: change local host url
    return await axios
      .get(baseurl + "Project/GetActiveFieldForMarketPlace")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};
//TODO:- SERVICES OF PROJECT
export const GetAllEmployeesExcludingEmployeesWithRolesInTheProject = async (
  inputEmail: string,
  pipelineCode: string,
  jobCode: string,
  usersNotToInclude: string[] = []
) => {
  try {
    const body = {
      inputEmail: inputEmail,
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      usersNotToInclude: usersNotToInclude,
    };
    return await axios.post(
      baseurl + "Project/GetAllEmployeesExcludingEmployessWithRoles",
      body
    );
  } catch (err) {
    throw err;
  }
};
export const GetProjectDetailsAsPerPipelineCodeAndUserRole = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    // GetProjectDetailsAsPerPipelineCodeAndUserRole
    let url = `${baseurl}Project/GetProjectDetailsAsPerPipelineCodeAndUserRole`;
    // ?PipelineCode=${encodeURIComponent(
    //   pipelineCode
    // )}`;
    if (isEmpty(jobCode)) {
      return await axios.get(url, {
        params: {
          PipelineCode: pipelineCode,
        },
      });
      // url = `${url}&JobCode=${encodeURIComponent(jobCode)}`;
    } else {
      return await axios.get(url, {
        params: {
          PipelineCode: pipelineCode,
          JobCode: jobCode,
        },
      });
    }
    return await axios.get(url);
  } catch (err) {
    throw err;
  }
};

export const AddJustificationText = async (
  justificationText: string,
  pipelineCode: string,
  jobCode?: string
) => {
  try {
    const payload = {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      justificationText: justificationText,
    };
    return await axios.post(baseurl + `Project/AddJustification`, payload);
  } catch (error) {
    throw error;
  }
};

export const getAllProjects = async () => {
  try {
    return await axios
      .get(baseurl + "Project/getAllProjects")
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const getAllProjectsByBUAndExpertiseService = async (
  bu: string,
  expertise: string,
  endDate: Date,
  offering: string
) => {
  try {
    var url = createQueryUrl(
      baseurl + "Project/GetAllProjectByBUandExpertise",
      {
        bu: encodeURIComponent(bu),
        expertise: encodeURIComponent(expertise),
        endDate: new Date(endDate),
        offerings: encodeURIComponent(offering),
      }
    );
    return await axios.get(url).then((resp: any) => {
      return resp.data;
    });
  } catch (error) {
    throw error;
  }
};

export const GetAllJobCodesForPipelineCodeService = async (
  pipelineCode: string,
  jobCode: string,
  SameTeamAllocation: boolean = false
): Promise<string[]> => {
  try {
    var url = createQueryUrl(
      baseurl + "Project/GetAllJobCodesForPipelineCode",
      {
        pipelineCode: pipelineCode,
        jobCode: jobCode,
        SameTeamAllocation: SameTeamAllocation,
      }
    );
    return await axios.get(url).then((resp: any) => {
      return resp.data;
    });
  } catch (error) {
    throw error;
  }
};

export const GetAllUsersExcludingEmployeesWithRolesInProject = async (
  inputEmail: string,
  pipelineCode: string,
  jobCode: string,
  currentUserRoles: any[] = [],
  usersNotToInclude: any[] = []
) => {
  try {
    const data = {
      inputEmail: inputEmail,
      pipelineCode: pipelineCode,
      jobCode: jobCode,
      currentUserRoles: currentUserRoles,
      usersNotToInclude: usersNotToInclude,
    };
    return await axios.post(
      baseurl + `Project/GetAllUsersExcludingEmployessWithRoles`,
      data
    );
  } catch (err) {
    throw err;
  }
};

export const RemoveProjectUser = async (userDetail: any) => {
  try {
    return await axios.post(
      baseurl + `Project/RemoveProjectUserWithRole`,
      userDetail
    );
  } catch (error) {
    throw error;
  }
};

export const MoveToNewJobCode = async (codeDetails: any) => {
  try {
    return await axios.post(
      baseurl + `Project/ChangeCodeForProject`,
      codeDetails
    );
  } catch (error) {
    throw error;
  }
};
