import { Stack } from "@mui/material";
import React from "react";
import Header from "../header/Header";
import NotificationComp from "../notification/notification";
import HelpComp from "../help/help";
import LoggedInComp from "../logged-info/loggedIn";

const MenuComp = () => {
  return (
    <Stack direction="row" spacing={0}>
      <Header />
      <NotificationComp />
      <HelpComp />
      <LoggedInComp />
    </Stack>
  );
};

export default MenuComp;
