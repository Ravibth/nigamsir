import React, { Fragment } from "react";
import { Outlet } from "react-router-dom";
import Appbarcomp from "../components/appbar/appbar";
const Layout = () => {
  return (
    <Fragment>
      <div style={{ height: "100%" }}>
        <div style={{ position: "sticky", top: "0", zIndex: "100" }}>
          <Appbarcomp />
        </div>
        <div
          style={{
            // marginTop: "15px",
            height: "92vh",
            overflow: "auto",
          }}
        >
          <Outlet />
        </div>
      </div>
    </Fragment>
  );
};

export default Layout;
