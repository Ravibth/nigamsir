import { Box, Link } from "@mui/material";
import React from "react";
import * as constant from "./constant";
import gtLogo from "../../common/images/SPMS.jpeg";

const GrantLogoComp = () => {
  return (
    <Link href="/" color="inherit">
      <Box
        component="img"
        sx={constant.LogoSxProps}
        alt="SPMS"
        src={gtLogo}
      />
    </Link>
  );
};

export default GrantLogoComp;
