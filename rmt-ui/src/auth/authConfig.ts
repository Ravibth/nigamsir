export const msalConfig: any = {
  auth: {
    clientId: process.env.REACT_APP_CLIENTID,
    authority: process.env.REACT_APP_AUTHORITY,
    redirectUri: process.env.REACT_APP_REDIRECT_URI,
    postLogoutRedirectUri: "/",
  },
  cache: {
    cacheLocation: "localStorage", // This configures where your cache will be stored
    storeAuthStateInCookies: false, // Set this to "true" if you are having issues on IE11 or Edge
  },
};
// Add scopes here for ID token to be used at Microsoft identity platform endpoints.
export const loginRequest = {
  scopes: ["User.Read"],
};
