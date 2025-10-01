import { Tooltip } from "@mui/material";
import React from "react";
// import ReplayIcon from "@mui/icons-material/Replay";
import RestartAltIcon from "@mui/icons-material/RestartAlt";

const CustomHeader = (props: any) => {
  return (
    <div>
      <div style={{ cursor: "pointer" }}>
        <Tooltip title="Clear selected role" placement="left">
          <RestartAltIcon
            height={15}
            width={18}
            onClick={props.clearRoleNameSelcted}
          />
        </Tooltip>
      </div>
    </div>
  );
};

export default CustomHeader;
