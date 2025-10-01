import React, { memo } from "react";
import { Grid, Typography } from "@mui/material";
import * as constants from "./constants";
import { sortCategories } from "../../../global/utils";
import { PreferenceOrderForScoreBreakup } from "../score-breakup-datagrid";

const ScoreBreakupTooltip = (props: any) => {
  return (
    <Typography sx={{ ...constants.TooltipDetailsSxProps, maxWidth: "400px" }}>
      <Grid container spacing={2}>
        {sortCategories(
          props.requisitionParameters.map((item) => item.category),
          PreferenceOrderForScoreBreakup
        ).map((column: any) => {
          return (
            <Grid
              item
              key={column}
              xs={12}
              // xs={12 / props.requisitionParameters.length}
            >
              <Grid container spacing={2}>
                <Grid item xs={7}>
                  <Typography
                    component={"span"}
                    sx={{
                      ...constants.TextWrapSxProps,
                      color: "black",
                      fontweight: "800",
                      fontSize: "19px",
                    }}
                  >
                    {constants.categoryLabels(column)}
                  </Typography>
                </Grid>
                <Grid item xs={5}>
                  <Typography component={"span"} sx={constants.TextWrapSxProps}>
                    {props.userInfo.score_breakup.find(
                      (a) => a.parameter === column
                    )?.value
                      ? props.userInfo.score_breakup.find(
                          (a) => a.parameter === column
                        )?.value
                      : 0}
                  </Typography>
                </Grid>
              </Grid>
            </Grid>
          );
        })}
      </Grid>
    </Typography>
  );
};
export default memo(ScoreBreakupTooltip);
