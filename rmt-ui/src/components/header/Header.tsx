import React, { useState } from "react";
import { AppBar, Button, Tab, Tabs, Toolbar, useMediaQuery, useTheme } from "@mui/material";
 
 
const PAGES = ["Projects", "Employee", "Reports", "Manage", "Marketplace"];

const Header = () => {
  const [value, setValue] = useState();
  const theme = useTheme();
  const isMatch = useMediaQuery(theme.breakpoints.down("md"));

  return (
    <React.Fragment>
      <Toolbar sx={{ background: "#4f2d7f" }}>
        {isMatch ? (
          <>
            {/* <DrawerComp /> */}
          </>
        ) : (
          <>
            <Tabs
              sx={{ color: "white" }}
              textColor="inherit"
              value={value}
              onChange={(e, value) => setValue(value)}
              indicatorColor="secondary"
            >
              {PAGES.map((page, index) => (
                <Tab key={index} label={page} />
              ))}
            </Tabs>
          </>
        )}
      </Toolbar>
    </React.Fragment>
  );
};

export default Header;
