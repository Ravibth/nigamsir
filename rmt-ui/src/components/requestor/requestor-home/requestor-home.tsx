import React, { useEffect, useState } from "react";
import RequestorProjectList from "../resource-requestor/project-list";

function RequestorHome(props: any) {
  return (
    <div>
      <RequestorProjectList {...props}></RequestorProjectList>
    </div>
  );
}

export default RequestorHome;
