import Grid from "@mui/material/Grid";
import React from "react";
import { SxProps, Typography } from "@mui/material";
import { TooltipDetailsSxProps } from "../marketplacetabs/constant";
import AccountTreeOutlinedIcon from "@mui/icons-material/AccountTreeOutlined";
import WorkOutlineOutlinedIcon from "@mui/icons-material/WorkOutlineOutlined";

const MarketPlaceLegends = () => {
  const VerticalAlignSxProps: SxProps = {
    display: "flex",
    alignItems: "center",
    color: "white",
  };

  return (
    <Typography sx={{ ...TooltipDetailsSxProps, maxWidth: "180px" }}>
      <Grid container spacing={2}>
        <Grid item xs={2} sx={VerticalAlignSxProps}>
          <AccountTreeOutlinedIcon
            sx={{
              color: "red",
              fontSize: "1rem",
            }}
          />
        </Grid>
        <Grid item xs={9} sx={VerticalAlignSxProps}>
          Pipeline
        </Grid>
        <Grid item xs={2} sx={VerticalAlignSxProps}>
          <WorkOutlineOutlinedIcon
            sx={{
              color: "green",
              fontSize: "1rem",
            }}
          />
        </Grid>
        <Grid item xs={9} sx={VerticalAlignSxProps}>
          Job
        </Grid>
      </Grid>
    </Typography>
  );
};

export default MarketPlaceLegends;
