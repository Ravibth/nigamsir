import React, { useState } from "react";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import { Box, Button, Grid } from "@mui/material";
import * as constant from "./constant";
import Approvaltable from "./approvaltable/approvaltable";
import Notificationtable from "./notificationtable/notificationtable";
import ActionButton from "../actionButton/actionButton";
import Approvalsfunctions from "./approvalfucntions/approvalsfunctions";

function Approvaltabs() {
  const [selectedTab, setSelectedTab] = useState(0);

  const handleTabChange = (event: any, newValue: any) => {
    setSelectedTab(newValue);
  };

  return (
    <div style={{ flexDirection: "column" }}>
      <Grid container alignItems="baseline">
        <Grid item width={"50%"} pl={3}>
          <Box className={"tab-container"} sx={constant.TabLabelSXProps}>
            <Tabs value={selectedTab} onChange={handleTabChange} aria-label="basic tabs example">
              <Tab sx={constant.tabs} label="Approvals" />
              <Tab sx={constant.tabs} label="Notification" />
            </Tabs>
          </Box>
        </Grid>
      </Grid>
      <Approvalsfunctions/>
      {selectedTab === 0 && <Approvaltable />}
      {selectedTab === 1 && <Notificationtable />}
    </div>
  );
}

export default Approvaltabs;
