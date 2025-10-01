import {
  Autocomplete,
  Box,
  Card,
  CardContent,
  Grid,
  TextField,
  Typography,
} from "@mui/material";
import React from "react";
import * as constant from "../constant";
import { getCurrentProjectName } from "../util";

const Currentallocation = (props: any) => {
  const { projectData } = props;

  return (
    <div>
      <Grid
        item
        xs={12}
      >
        <Typography sx={constant.Text}>Current</Typography>
        <Grid
          item
          xs={12}
        >
          <Typography sx={constant.Title}>Name - Code</Typography>
        </Grid>
        <Grid
          item
          xs={12}
        >
          <Card sx={constant.Card}>
            <CardContent className="content-main">
              <Typography sx={constant.cardContent}>
                {getCurrentProjectName(projectData)}
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </div>
  );
};

export default Currentallocation;
