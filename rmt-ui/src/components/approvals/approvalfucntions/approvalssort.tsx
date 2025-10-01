import {
  Autocomplete,
  Grid,
  IconButton,
  TextField,
  Typography,
} from "@mui/material";
import React from "react";
import ImportExportIcon from "@mui/icons-material/ImportExport";
import * as constant from "./constant";
import NorthIcon from "@mui/icons-material/North";
import SouthIcon from "@mui/icons-material/South";

const Approvalssort = () => {
  const flatProps = {
    options: top100Films.map((option) => option.title),
  };
  return (
    <div>
      <Grid container>
        <Grid className="gridOne" container alignItems="center">
          <Grid item>
            <Grid container alignItems="center" sx={{ marginTop: "18px" }}>
              <IconButton>
                <NorthIcon
                  sx={{
                    fontSize: 15,
                    marginRight: "-20px",
                    color: "#4f2d7f",
                    fontType: "bold",
                  }}
                />{" "}
              </IconButton>
              <IconButton>
                <SouthIcon
                  sx={{ fontSize: 15, color: "#4f2d7f", fontType: "bold" }}
                />
              </IconButton>
            </Grid>
          </Grid>
          <Grid item>
            <Typography sx={{ ...constant.label, marginLeft: "10px" }}>
              Sort By:
            </Typography>
          </Grid>
          <Grid className="gridOne" item>
            <Autocomplete
              {...flatProps}
              id="flat-demo"
              renderInput={(params) => (
                <TextField
                  style={{ width: "200px", marginTop: "20px" }}
                  {...params}
                  label=""
                  variant="standard"
                />
              )}
            />
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};
const top100Films = [
  { title: "Due Date" },
  { title: "Start date" },
  { title: "End date" },
  { title: "Designation" },
];

export default Approvalssort;
