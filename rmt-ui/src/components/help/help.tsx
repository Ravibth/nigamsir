import { Badge, IconButton, Toolbar } from "@mui/material";
import React from "react";
import HelpOutlineIcon from "@mui/icons-material/HelpOutline";

const HelpMenu = () => {
  return (
    <React.Fragment>
      <IconButton size="large" aria-label="show 17 new notifications" color="inherit">
        <Badge color="error">
          <HelpOutlineIcon />
        </Badge>
      </IconButton>
    </React.Fragment>
  );
};

export default HelpMenu;
