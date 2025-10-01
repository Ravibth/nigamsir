import Grid from "@mui/material/Grid";
import React from "react";
import CircleIcon from "@mui/icons-material/Circle";
import { SxProps, Typography } from "@mui/material";
import { TooltipDetailsSxProps } from "./availability-view/constants";
import MarketPlaceIcon from "../../common/images/marketplace.png";

const SystemSuggestionCardLegends = () => {
  const VerticalAlignSxProps: SxProps = {
    display: "flex",
    alignItems: "center",
    color: "#4f2d7f",
  };

  return (
    <Typography sx={{ ...TooltipDetailsSxProps, maxWidth: "180px" }}>
      <Grid container spacing={2}>
        <Grid item xs={2} sx={VerticalAlignSxProps}>
          <CircleIcon
            sx={{
              color: "green",
              fontSize: "1rem",
            }}
          />
        </Grid>
        <Grid item xs={9} sx={VerticalAlignSxProps}>
          Available
        </Grid>
        <Grid item xs={2} sx={VerticalAlignSxProps}>
          <CircleIcon
            sx={{
              color: "red",
              fontSize: "1rem",
            }}
          />
        </Grid>
        <Grid item xs={9} sx={VerticalAlignSxProps}>
          Not Available
        </Grid>

        <Grid item xs={2} sx={VerticalAlignSxProps}>
          <img
            src={MarketPlaceIcon}
            alt="upload"
            style={{
              height: "22px",
              width: "22px",
            }}
          />
        </Grid>
        <Grid item xs={9} sx={VerticalAlignSxProps}>
          Showed Interest
        </Grid>
      </Grid>
    </Typography>
  );
};

export default SystemSuggestionCardLegends;
