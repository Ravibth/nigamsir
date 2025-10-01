import { RESPONSE_STATUS } from "../../global/constant";
import {
  getMyPendingApprovalTask,
  getMyPendingApprovalTaskByModule,
  getMyWithdrawalTask,
} from "../../services/allocation/workflow-service";
import * as constant from "./constant";

export const getMyAllPendingTask = async (emailId: string) => {
  return Promise.all([getMyTask(emailId), getWithdrawalTask(emailId)])
    .then(([myTaskResponse, myWithdrawalTask]) => {
      return [...myTaskResponse, ...myWithdrawalTask];
    })
    .catch((err) => {
      throw err;
    });
};

export const getMyTask = async (emailId: string) => {
  const payload: constant.IGetTaskPayload = {
    assigned_to: emailId,
    taskstatus: "pending",
    outcome: "inprogress",
  };
  return getMyPendingApprovalTask(payload).then((response: any) => {
    if (response.status === RESPONSE_STATUS.Success) {
      return response.data;
    } else {
      return [];
    }
  });
};

export const getWithdrawalTask = async (emailId: string) => {
  const payload: constant.IGetTaskPayload = {
    employeeEmail: emailId,
    taskstatus: "pending",
    outcome: "inprogress",
  };
  return getMyWithdrawalTask(payload).then((response: any) => {
    if (response.status === RESPONSE_STATUS.Success) {
      return response.data.map((i) => {
        return { isWithdrawl: true, ...i };
      });
    } else {
      return [];
    }
  });
};

export const getMyApprovalTasks = (payload: constant.IGetTaskPayload) => {
  try {
    return new Promise((resolve, reject) => {
      Promise.all([
        getPendingApproval(payload),
        getWithdrawalTask(payload.assigned_to),
      ])
        .then(([myPendingResponse, myWithdrawalTask]) => {
          resolve([...myPendingResponse, ...myWithdrawalTask]);
        })
        .catch((err) => {
          throw err;
        });
    });
  } catch (error) {
    throw error;
  }
};

export const getPendingApproval = (payload: constant.IGetTaskPayload) => {
  return getMyPendingApprovalTaskByModule(payload).then((response: any) => {
    if (response.status === RESPONSE_STATUS.Success) {
      return response.data;
    } else {
      return [];
    }
  });
};
