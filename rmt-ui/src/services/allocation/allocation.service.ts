import axios from "axios";
import { createQueryUrl } from "../utils";
import * as ServiceConstant from "../../global/service-constant";
import { IAllocation } from "../../components/update-allocation/entity/IAllocations";
import { IUsersTimelines } from "../../components/system-suggestions/availability-view/constants";
import {
  IAllUserAllocationEntries,
  INewJobCodeMoveEntries,
} from "../../components/common-allocation/interface";
import { IResourceAllocationDetailsMaster } from "../../common/interfaces/IAllocation";
import { IResourceAllocationDetails } from "../../components/common-allocation/Iresource-allocation-details-response";

const baseurl = process.env.REACT_APP_ALLOCATION;

export const getProjectsOfEmployeeByEmail = async (employeeEmail: any) => {
  try {
    return await axios.get(
      baseurl +
        `ResourceAllocation/GetProjectsByEmployeeEmail?email=${employeeEmail}`
    );
  } catch (error) {
    throw error;
  }
};

export const fetchSystemSuggestionsByReqId = async (
  requisitionId: string,
  limit: number,
  pagination: number,
  filterPayload: string[]
) => {
  try {
    const url = createQueryUrl(
      baseurl + "ResourceAllocation/GetSystemSuggestionsByRequisitionId",
      {
        requisitionId,
        limit,
        pagination,
        parameter_value_pairs: filterPayload,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const GetMultipleAllocationsByRequisitionIDs = async (
  ids: number[]
): Promise<IResourceAllocationDetailsMaster[]> => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.GetMultipleAllocationsByRequisitionIDs}`,
      { ids }
    );
    return (await axios.get(url))?.data;
  } catch (error) {
    throw error;
  }
};

export const getAllocationByEmail = async (email: string) => {
  try {
    return await axios.get(
      `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.getAllocationByEmail}${email}`
    );
  } catch (error) {
    throw error;
  }
};

export const isAllocationHoursAvilable = async (
  requisitionId: string,
  emailId: string[],
  allocation: IAllocation
) => {
  try {
    const payload: any = {
      RequisitionId: requisitionId,
      EmailId: emailId,
      StartDate: allocation?.confirmedAllocationStartDate
        ? new Date(allocation.confirmedAllocationStartDate)
        : new Date(),
      EndDate: allocation.confirmedAllocationEndDate
        ? new Date(allocation.confirmedAllocationEndDate)
        : new Date(),
      RequireWorkingHours: allocation.confirmedPerDayHours,
      isPerDayHourAllocation: allocation.isPerDayHourAllocation,
      PipelineCode: allocation?.pipelineCode,
      JobCode: allocation?.jobCode,
    };
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.getAvaiableHoursByEmailId}`,
      payload
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const GetUsersTimelineForMultipleDates = async (
  payload: ServiceConstant.GetUsersTimelinePayload[]
): Promise<IUsersTimelines[]> => {
  try {
    const url = `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.GetUsersTimelineForMultipleDates}`;
    var body = payload.map((item) => {
      return {
        emails: item.emails?.toString(),
        start_date: new Date(item.start_date).toISOString(),
        end_date: new Date(item.end_date).toISOString(),
      };
    });
    return (await axios.post(url, body)).data;
  } catch (error) {
    throw error;
  }
};

export const alocateNamedResource = async (payload: any) => {
  try {
    return await axios.post(
      baseurl + "NamedAllocation/SubmitResourceAllocateToProject",
      payload
    );
  } catch (err) {
    throw err;
  }
};

export const getAllocationByJobcode = async (payload: any) => {
  try {
    return await axios.post(
      baseurl + "SameTeamAllocation/GetAllocationByJobCode",
      payload
    );
  } catch (err) {
    throw err;
  }
};

export const GetActiveAllocationByPipeLineCode = async (
  pipelineCode: string,
  jobCode?: string,
  isAllocationDetailsFilterByUserRoles?: boolean
) => {
  try {
    const url = createQueryUrl(
      baseurl + `ResourceAllocation/GetActiveAllocationByPipeLineCode`,
      {
        pipelineCode: encodeURIComponent(pipelineCode),
        jobCode: encodeURIComponent(jobCode),
        isAllocationDetailsFilterByUserRoles:
          isAllocationDetailsFilterByUserRoles,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const ReleaseResourceByGuid = async (id: any) => {
  try {
    return await axios.post(
      baseurl + `ResourceAllocation/ReleaseResourceByGuid`,
      id
    );
  } catch (error) {
    throw error;
  }
};

export const CreateCommonAllocation = async (
  isDraft: boolean,
  payload: IAllUserAllocationEntries[]
) => {
  try {
    const url = createQueryUrl(
      baseurl + "ResourceAllocation/CommonResourceAllocation",
      {
        isDraft: isDraft,
      }
    );
    return await axios.post(url, payload);
  } catch (error) {
    throw error;
  }
};

export const MoveNewJobAllocation = async (
  isDraft: boolean,
  payload: INewJobCodeMoveEntries
) => {
  try {
    const url = createQueryUrl(
      baseurl + "ResourceAllocation/NewResourceAllocationMove",
      {
        isDraft: isDraft,
      }
    );
    return await axios.post(url, payload);
  } catch (error) {
    throw error;
  }
};

// export const GetSkillsByEmpEmailJobCode = async (
//   emailId: string,
//   pipelineCode: string,
//   jobCode: string
// ) => {
//   try {
//     const url = createQueryUrl(
//       baseurl + `ResourceAllocation/GetSkillByEmailJobCode `,
//       {
//         emailId: emailId,
//         pipelineCode: pipelineCode,
//         jobCode: jobCode,
//       }
//     );
//     return await axios.get(url);
//   } catch (error) {
//     throw error;
//   }
// };

export const getAllPipelineOrJobCodes = async (
  pipelineCode: string,
  jobCode: string
) => {
  try {
    const payload = {
      pipelineCode: pipelineCode,
      jobCode: jobCode,
    };

    const url = createQueryUrl(
      baseurl + `ResourceAllocation/GetAllAllocationByPipelineCode`,
      {
        pipelineCode: pipelineCode,
        jobCode: jobCode,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const UpdateResourceAllocations = async (
  payload: IResourceAllocationDetails[]
) => {
  try {
    const url = createQueryUrl(
      baseurl + "ResourceAllocation/UpdateResourceAllocations",
      {}
    );
    return await axios.post(url, payload);
  } catch (error) {
    throw error;
  }
};
