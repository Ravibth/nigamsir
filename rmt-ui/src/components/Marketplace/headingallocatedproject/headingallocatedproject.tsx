import React from "react";
import { Divider, Grid, Typography } from "@mui/material";
import * as constant from "./constant";
import Sortprojects from "../sortprojects/sortprojects";
import Filtermarketplaceproject from "../filtermarketplaceproject/filtermarketplaceproject";

const HeadingAllocatedProject = () => {
  return (
    <div style={{ paddingLeft: "40px" }}>
      <Grid
        container
        spacing={2}
        sx={{ paddingTop: "30px", paddingBottom: "20px" }}
      >
        <Grid item xs={6}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <Typography sx={constant.typographyHeading}>
                Recommended Projects For You
              </Typography>
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={6}>
          <Grid container spacing={2}>
            <Grid item xs={12} container justifyContent="flex-end">
              <Filtermarketplaceproject />
            </Grid>
            <Grid item xs={12} container justifyContent="flex-end">
              <Sortprojects />
            </Grid>
          </Grid>
        </Grid>

        {/* <Grid  item xs={6} container justifyContent="flex-end">
         <Sortprojects/>
        </Grid> */}
      </Grid>
      <Divider sx={{ backgroundColor: "rgba(0, 0, 0, 0.05)", width: "100%" }} />
    </div>
  );
};

export default HeadingAllocatedProject;
