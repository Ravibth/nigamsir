import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { MsalProvider } from "@azure/msal-react";
import { myMSALObj } from "./auth/authService";
import { EventType } from "@azure/msal-browser";
import { jwtInterceptor } from "./auth/jwtInterceptor";
import { licenseKey } from "./common/assets/agGridLicense";
import { LicenseManager } from "ag-grid-enterprise";

LicenseManager.setLicenseKey(licenseKey);
myMSALObj.addEventCallback((event: any) => {
  if (event.eventType === EventType.LOGIN_SUCCESS) {
    myMSALObj.setActiveAccount(event.payload.account);
  }
});
jwtInterceptor();

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <MsalProvider instance={myMSALObj}>
    <App />
  </MsalProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
