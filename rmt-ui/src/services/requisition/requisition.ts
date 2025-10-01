import axios from "axios";
import { createQueryUrl } from "../utils";
import * as ServiceConstant from "../../global/service-constant";

const baseurl = process.env.REACT_APP_ALLOCATION;

export const getAllRequisitionByProjectCode = async (
  pipelineCode: string,
  jobCode: string,
  IsRequsitionFilterByProjectRoles: boolean = false
) => {
  try {
    return await axios.get(
      baseurl +
        `Requisition/GetAllRequisitionByProjectCode?pipelineCode=${encodeURIComponent(
          pipelineCode
        )}&jobCode=${encodeURIComponent(
          jobCode
        )}&isRequsitionFilterByProjectRoles=${IsRequsitionFilterByProjectRoles}`
    );
  } catch (error) {
    throw error;
  }
};

export const DeleteRequisitionById = async (Requisition_id: any) => {
  try {
    return await axios.post(
      baseurl + `Requisition/DeleteRequisitionById`,
      Requisition_id
    );
  } catch (error) {
    throw error;
  }
};

export const getRequisitionDetailsByRequisitionId = async (
  requisitionId: string,
  isRequsitionFilterByProjectRoles: boolean = false
) => {
  try {
    const payload: any = {
      requisitionId: requisitionId,
      isRequsitionFilterByProjectRoles: isRequsitionFilterByProjectRoles,
    };
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.getRequisitionDetailsByRequisitionId}`,
      payload
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const SubmitRequisitionForProjectCode = async (payload: any) => {
  try {
    return await axios.post(
      `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.submitRequisitionForProjectCode}`,
      payload
    );
  } catch (error) {
    throw error;
  }
};

export const UpdateRequisition = async (payload: any): Promise<any> => {
  return new Promise((resolve, reject) => {
    axios
      .put(
        `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.updateRequisition}`,
        payload
      )
      .then((resp) => {
        resolve(resp);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

export const BulkUploadRequisition = (payload: any) => {
  return new Promise((resolve, reject) => {
    axios
      .post(
        `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.bulkUploadRequisition}`,
        payload
      )
      .then((resp) => {
        resolve(resp);
      })
      .catch((err) => {
        reject(err);
      });
  });
};

export const UploadWcgtValidation = (payload: any) => {
  return new Promise((resolve, reject) => {
    axios
      .post(
        `${baseurl}${ServiceConstant.ALLOCATION_SERVICES.uploadWcgtValidation}`,
        payload
      )
      .then((response) => {
        resolve(response);
      })
      .catch((err) => {
        reject(err);
      });
  });
};
