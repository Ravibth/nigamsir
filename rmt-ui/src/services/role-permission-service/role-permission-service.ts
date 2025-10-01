import axios from "axios";
import { createQueryUrl } from "../utils";
import { errorMessage } from "../log-services/log-service";
import uniq from "lodash/uniq";

const baseurl = process.env.REACT_APP_IDENTITY;

export const getAllRoles = async () => {
  let response;
  await axios
    .get(baseurl + "identity/role")
    .then((resp) => {
      response = resp.data;
    })
    .catch((err) => {
      errorMessage("role-permission", "getAllRoles", err);
    });
  return response;
};

export async function getAllUsers() {
  let response;
  await axios
    .get(baseurl + "identity/user/all?")
    .then((resp) => {
      response = resp.data.rows;
    })
    .catch((err) => {
      errorMessage("role-permission", "getAllUsers", err);
    });
  return response;
}

export async function getUsersBasedOnRoleName(role: any) {
  let response;
  await axios
    .get(baseurl + "identity/user/all?roles=" + role)
    .then((resp) => {
      if (resp) {
        response = resp;
      }
    })
    .catch((err) => {
      errorMessage(
        "role-permission",
        "getModulePermissionsBasedOnRoleName",
        err
      );
    });
  return response;
}

export async function getModulePermissionsBasedOnRoleName(role: any) {
  let response;

  if (typeof role === "string") {
    role = role.trim();
  } else if (Array.isArray(role)) {
    role= uniq(role);
    role = role.filter((item) => item != null && item.trim() !== "").join(",");
  }

  if (role) {
    await axios
      .get(baseurl + "identity/modulePermission/role?roleName=" + role)
      .then((resp) => {
        if (resp) {
          response = resp;
        }
      })
      .catch((err) => {
        errorMessage(
          "role-permission",
          "getModulePermissionsBasedOnRoleName",
          err
        );
      });
  }

  return response;
}

export async function getAllModulePermissions() {
  let response;
  await axios
    .get(baseurl + "identity/modulePermission/permissionMappings")
    .then((resp) => {
      response = resp.data;
    })
    .catch((err) => {
      errorMessage("role-permission", "getAllModulePermissions", err);
    });
  return response;
}

export async function updateUserStatus(emailId: any, statusValue: any) {
  try {
    return await axios.put(
      baseurl + "identity/user/update?emailId=" + emailId,
      {
        status: statusValue,
      }
    );
  } catch (err) {
    errorMessage("role-permission", "updateUserStatus", err);
  }
}

export async function updateUserRoles(emailId: any, roles: any) {
  try {
    return await axios.put(baseurl + "identity/user/v2/" + emailId, {
      roles: roles,
    });
  } catch (err) {
    errorMessage("role-permission", "updateUserRoles", err);
  }
}

export async function updateUserRoleList(payload: any) {
  try {
    await axios.post(baseurl + "identity/user/v1/bulkAddUserRoles", payload);
  } catch (err) {
    throw err;
  }
}

export async function getUserFromWCCGT(emailId: string) {
  let response;
  const url = createQueryUrl(baseurl + "identity/user/GetEmpDetailsFromWCGT", {
    emailId: emailId,
  });
  await axios
    .get(url)
    .then((resp) => {
      response = resp.data;
    })
    .catch((err) => {
      errorMessage("role-permission", "getUserFromWCCGT", err);
    });
  return response;
}

export async function addNewUserWithRoles(newUserData: any) {
  try {
    return await axios.post(baseurl + "identity/user/v1", newUserData);
  } catch (err) {
    errorMessage("role-permission", "addNewUserWithRoles", err);
  }
}

export async function getUserByEmail(emailId: string) {
  let response;
  const url = baseurl + "identity/user/v4/" + emailId;
  await axios
    .get(url)
    .then((resp) => {
      response = resp.data;
    })
    .catch((err) => {
      console.error(err);
      throw err;
      return err;
    });
  return response;
}

// export async function getUserByEmail(emailId: string) {
//   try {
//     return axios.get(baseurl + "identity/user/v4/" + emailId);
//   } catch (err) {
//     throw err;
//   }
// }
