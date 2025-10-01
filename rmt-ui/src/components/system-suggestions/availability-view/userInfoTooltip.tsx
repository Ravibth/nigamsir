import React, { memo } from "react";
import { Grid, Typography, Tooltip } from "@mui/material";
import * as constants from "./constants";
import { getEmailId } from "../../../global/utils";

const UserInfoTooltip = (props: any) => {
  return (
    // <Card raised sx={{ backgroundColor: "black" }}>
    //   <CardContent>
    <Typography
      component="div"
      sx={{ ...constants.TooltipDetailsSxProps, maxWidth: "550px" }}
    >
      <Grid
        container
        spacing={2}
      >
        {constants.UserInfoTooltipCategories.map((column: any) => {
          let value = props.userInfo[column.toLowerCase()]
            ? props.userInfo[column.toLowerCase()]
            : props.userInfo[column];
          if (column.toLowerCase() === "email") {
            value = getEmailId(props.userInfo["email"]);
          }
          if (column.toLowerCase() === "name") {
            value = props.userInfo["empName"];
          }
          if (column === "Business Unit") {
            value =
              props.userInfo["business_unit"] ||
              props.userInfo["businessUnit"] ||
              "";
          }
          if (column.toLowerCase() === "supercoach") {
            value = props.userInfo["supercoach"]
              ? getEmailId(props.userInfo["supercoach"])
              : "";
          }
          return (
            <Grid
              item
              key={column}
              xs={12}
              // xs={12 / constants.UserInfoTooltipCategories.length}
            >
              <Grid
                container
                spacing={2}
              >
                <Grid
                  item
                  xs={3.8}
                >
                  <Typography
                    sx={constants.TextWrapSxProps}
                    component={"div"}
                  >
                    <Typography sx={constants.UserInfoTooltipColumnsSxProps}>
                      {column}
                    </Typography>
                  </Typography>
                </Grid>
                <Grid
                  item
                  xs={8.2}
                >
                  <Typography
                    sx={constants.TextWrapSxProps}
                    component={"div"}
                  >
                    <Typography sx={constants.UserInfoTooltipValuesSxProps}>
                      <Tooltip title={value}>{value}</Tooltip>
                    </Typography>
                  </Typography>
                </Grid>
              </Grid>
            </Grid>
          );
        })}
      </Grid>
    </Typography>
    //   </CardContent>
    // </Card>
  );
};
export default memo(UserInfoTooltip);
