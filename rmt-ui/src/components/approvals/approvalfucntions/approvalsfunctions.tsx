import { Grid, Paper } from "@mui/material";
import React from "react";
import Approvalsfilter from "./approvalsfilter";
import Approvalssort from "./approvalssort";

const Approvalsfunctions = () => {
  return (
    <div style={{ marginLeft: "25px" }}>
      <Grid container spacing={2}>
        <Grid item xs={6} sx={{ marginTop: "20px" }}>
          <Approvalsfilter />
        </Grid>
        <Grid item xs={6} sx={{ display: 'flex', justifyContent: 'flex-end' }}>
          <Approvalssort />
        </Grid>
      </Grid>
    </div>
  );
};

export default Approvalsfunctions;
