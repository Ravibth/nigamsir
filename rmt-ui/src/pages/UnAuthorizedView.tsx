import React from "react";
import { Grid } from "@mui/material";
const UnAuthorizedView = () => {
  return (
    <div>
      <Grid container>
        <Grid item xs={12} sm={12}>
          <div
            style={{
              textAlign: "center",
              width: "100%",
            }}
          >
            <div
              style={{
                textAlign: "center",
                width: "100%",
                fontSize: "20px",
                fontWeight: "bold",
                color: "red",
                paddingTop: "10px",
              }}
            >
              Unauthorized Access!
            </div>
            <div>You do not have access to view this page!</div>
          </div>
        </Grid>
      </Grid>
    </div>
  );
};

export default UnAuthorizedView;
