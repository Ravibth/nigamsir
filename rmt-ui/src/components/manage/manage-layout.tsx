import React from "react";
import { Grid } from "@mui/material";
import Manage from "./manage";

const ManageLayout = () => {
  return (
    <Grid container spacing={2} sx={{p:2}}>
      <Grid item xs={12}>
        <Manage />;
      </Grid>
    </Grid>
  );
};

export default ManageLayout;
