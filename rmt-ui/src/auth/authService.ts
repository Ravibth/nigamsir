import {
  InteractionRequiredAuthError,
  PublicClientApplication,
} from "@azure/msal-browser";
import { loginRequest, msalConfig } from "./authConfig";
export const myMSALObj = new PublicClientApplication(msalConfig);

let token: any;
// export async function signIn() {
//   try {
//     await myMSALObj.ssoSilent(loginRequest);
//   } catch (err) {
//     if (err instanceof InteractionRequiredAuthError) {
//       await myMSALObj.loginRedirect(loginRequest).catch((error: any) => {
//         // TODO:for error
//       });
//     } else {
//       // TODO:for error
//     }
//   }
// }

export async function signIn() {
  try {
    await myMSALObj.initialize();
    await myMSALObj.ssoSilent(loginRequest);
  } catch (err) {
    console.log("Something went wrong in login!", err);
    await myMSALObj.loginRedirect(loginRequest).catch((error: any) => {
      console.log("login redirect error", error);
    });

    // if (err instanceof InteractionRequiredAuthError) {
    //   await myMSALObj.loginRedirect(loginRequest).catch((error: any) => {
    //     console.log("login redirect error", error);
    //   });
    // } else {
    // }
  }
}

export function signout() {
  // todo : Commented to avoid signout user
  try {
    myMSALObj.logoutRedirect();
  } catch (error) {
    //
  }
}

const isExpired = async () => {
  try {
    if (token) {
      const current_time = new Date().getTime() / 1000 + 600;

      if (current_time > token.idTokenClaims.exp) {
        /* expired */
        await myMSALObj.initialize();
        await myMSALObj.ssoSilent(loginRequest);
      }
    }
  } catch (err) {
    signIn();
  }
};

export async function getToken() {
  await myMSALObj.initialize();
  await isExpired();
  const accounts = myMSALObj.getAllAccounts();
  if (accounts.length > 0) {
    const request = {
      scopes: ["User.Read"],
      account: accounts[0],
    };
    const accessToken = await myMSALObj
      .acquireTokenSilent(request)
      .then((response) => {
        token = response;
        return response.idToken;
      })
      .catch(async (error) => {
        console.log("getToken error", error);
        await signIn();
        return "";
      });

    return accessToken;
  }
  return "";
}

export async function getAccessToken() {
  const accounts = myMSALObj.getAllAccounts();
  if (accounts.length > 0) {
    const request = {
      scopes: ["User.Read"],
      account: accounts[0],
    };
    const accessToken = await myMSALObj
      .acquireTokenSilent(request)
      .then((response) => {
        return response.accessToken;
      })
      .catch((error) => {
        return null;
      });

    return accessToken;
  }
  return null;
}
//-------> NEW ADDED <----------------
export async function getLoggedInUserInfo() {
  try {
    const accounts = myMSALObj.getAllAccounts();
    if (accounts.length > 0) {
      return accounts[0];
    }
    return null;
  } catch (err) {
    return null;
  }
}
