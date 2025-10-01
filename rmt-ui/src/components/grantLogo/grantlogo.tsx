import { Box, Link } from "@mui/material";
import React from "react";
import * as constant from "./constant";
import gtLogo from "../../common/images/GTLOGO.png";

const GrantLogoComp = () => {
  return (
    <Link href="/" color="inherit">
      <Box
        component="img"
        sx={constant.LogoSxProps}
        alt="OptiWise"
        src={gtLogo}
      />
    </Link>
  );
};

export default GrantLogoComp;
