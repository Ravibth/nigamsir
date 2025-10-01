import axios from "axios";
import { createQueryUrl } from "../utils";
import * as ServiceConstant from "../../global/service-constant";
import {
  IGetTaskPayload,
  IWorkflowUpdateTask,
} from "../../components/workflow/constant";
import { OUTCOME } from "../../global/constant";
import { IWorkflowModelMaster } from "../../common/interfaces/IWorkflowmodel";

const baseurl = process.env.REACT_APP_BASEAPIURL;
export const getMyPendingApprovalTask = async (payload: IGetTaskPayload) => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.getWorkflowTasks}`,
      payload
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getMyWithdrawalTask = async (payload: IGetTaskPayload) => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.getEmployeewithdrawalWorkflowTasks}`,
      payload
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getMyPendingApprovalTaskByModule = async (
  payload: IGetTaskPayload
) => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.getWorkflowTasksDetailsByEmailId}`,
      {
        employeeEmail: payload.assigned_to,
        outcome: payload.outcome,
        module: payload.module,
        sub_module: payload.sub_module,
        workflow_task_status: payload.workflow_task_status,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const updateMyTask = async (
  selectedWorkflowItems: IWorkflowUpdateTask[],
  actionType: string,
  comments: string
) => {
  try {
    return Promise.all(
      selectedWorkflowItems.map((item: IWorkflowUpdateTask) => {
        item.status = actionType;
        item.remarks = comments;
        return updateTaskProcess(item);
      })
    )
      .then((res) => {
        return true;
      })
      .catch((err) => {
        return false;
      });
  } catch (error) {
    throw error;
  }
};

const updateTaskProcess = (payload: IWorkflowUpdateTask) => {
  return new Promise(async (resolve, reject) => {
    try {
      const url = `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.updateWorkflow}`;
      let result = await axios.post(url, payload);
      resolve(result);
    } catch (err) {
      reject(err);
    }
  });
};

export const bulkUpdateMyTask = async (
  selectedWorkflowItems: IWorkflowUpdateTask[],
  actionType: string,
  comments: string
): Promise<any> => {
  selectedWorkflowItems.map((item: IWorkflowUpdateTask) => {
    item.status = actionType;
    item.remarks = comments;
  });
  return new Promise(async (resolve, reject) => {
    try {
      const result = await bulkupdateTaskProcess(selectedWorkflowItems);
      resolve(result);
    } catch (err) {
      reject(err);
    }
  });
};

const bulkupdateTaskProcess = (payload: IWorkflowUpdateTask[]) => {
  return new Promise(async (resolve, reject) => {
    try {
      const url = `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.bulkupdateWorkflow}`;
      let result = await axios.post(url, payload);
      resolve(result);
    } catch (err) {
      reject(err);
    }
  });
};

export const getTaskDetailsByItemGuid = async (allocation_guid: string) => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.getWorkflowByItemGuid}`,
      {
        item_id: allocation_guid,
        outcome: OUTCOME.inprogress,
      }
    );
    return await axios.get(url);
  } catch (error) {
    throw error;
  }
};

export const getMultipleTasksByByQuery = async (
  item_id: string[]
): Promise<IWorkflowModelMaster[]> => {
  try {
    const url = createQueryUrl(
      `${baseurl}${ServiceConstant.CONFIGURATION_WORKFLOW.getWorkflowByItemGuid}`,
      {
        item_id: item_id,
        outcome: OUTCOME.inprogress,
      }
    );
    return (await axios.get(url)).data;
  } catch (error) {
    throw error;
  }
};
