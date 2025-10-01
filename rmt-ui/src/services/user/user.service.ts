import axios from "axios";
import { createQueryUrl } from "../utils";
import { IUserModelMaster } from "../../common/interfaces/IUserModel";

const baseurl = process.env.REACT_APP_BASEAPIURL;

export const GetUsersInfoByEmails1 = async (
  emails: string[]
): Promise<IUserModelMaster[]> => {
  try {
    const url = createQueryUrl(`${baseurl}identity/user/GetUsersByEmails`, {
      emails: emails,
    });
    return (await axios.get(url)).data;
  } catch (error) {
    throw error;
  }
};

export const GetUsersInfoByEmails = async (
  emails: string[]
): Promise<IUserModelMaster[]> => {
  try {
    return (
      await axios.post(`${baseurl}identity/user/GetUsersByEmails`, emails)
    ).data;
  } catch (error) {
    throw error;
  }
};

export const DoesUserHaveAnyFutureOrOngoingAllocations = async (
  emails: string[]
): Promise<string[]> => {
  try {
    var url = createQueryUrl(
      `${baseurl}ResourceAllocation/DoesUserHaveAnyFutureOrOngoingAllocations`,
      {
        emails: emails,
      }
    );
    return (await axios.get(url)).data;
  } catch (error) {
    throw error;
  }
};

export interface RemoveUserRoleByEmailDto {
  email_id: string;
  roles: string[];
}

export const RemoveUserRoleByEmail = async (
  emails: RemoveUserRoleByEmailDto[]
) => {
  try {
    return await axios.put(
      `${baseurl}identity/user/removeUserRoleByEmail`,
      emails
    );
  } catch (error) {
    throw error;
  }
};
