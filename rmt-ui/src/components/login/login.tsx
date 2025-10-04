import { Button } from "@mui/material";
import React, { useEffect } from "react";
import { signIn } from "../../auth/authService";
import "./login.css";

const Login = () => {
  const openMsalPopup = async () => {
    await signIn();
  };

  //RMT 33 > If user is not authenticated then redirect to login page without showing any page
  useEffect(() => {
    openMsalPopup();
  }, []);

  return (
    <div className="login-main">
      <div style={{ fontSize: "30px" }}>Loading...</div>
      <div style={{ display: "none" }}>
        <h2>Welcome to SPMS</h2>
        <div className="login-btn-main">
          <Button
            className="login-btn rmt-login-button"
            onClick={openMsalPopup}
          >
            Log in
          </Button>
        </div>
      </div>
    </div>
  );
};

export default Login;
