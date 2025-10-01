import React, { useEffect, useState } from "react";
import { RouterProvider } from "react-router-dom";
import router from "../../routes/route";

const RouterProviderComp = () => {
  return (
    <>
      <RouterProvider router={router}></RouterProvider>
    </>
  );
};
export default RouterProviderComp;
