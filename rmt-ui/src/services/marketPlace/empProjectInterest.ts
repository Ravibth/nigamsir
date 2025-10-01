import axios from "axios";

//todo: change local host url
const baseurl = process.env.REACT_APP_MARKETPLACE;

export const UpdateEmployeeProjectInterest = async (empInterest: any) => {
  try {
    return await axios
      .post(baseurl + `MarketPlace/SubmitEmpProjectInterest`, empInterest)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (error) {
    throw error;
  }
};

export const GetNotificationSubscription = async (
  user_emailId: string,
  module: string,
  subscription_role: string
) => {
  try {
    //Query Parameter => ?module=mm&subscription_role=ss&user_emailid=emailId&user_name=uu
    return await axios.get(
      baseurl +
        `Notification/GetNotificationSubscription?user_emailid=${encodeURIComponent(
          user_emailId
        )}&module=${encodeURIComponent(module)}
        &subscription_role=${encodeURIComponent(subscription_role)}`
    );
  } catch (err) {
    throw err;
  }
};

export const UpdateNotificationSubscription = async (payload: any) => {
  try {
    return await axios
      .post(baseurl + `Notification/SubscribeToNotification`, payload)
      .then((resp: any) => {
        return resp.data;
      });
  } catch (err) {
    throw err;
  }
};
