import { Box, Grid } from "@mui/material";
import React from "react";
import ResourceMenu from "../resource-menu/resources-menu";
import * as Constant from "./Contant";
import { IResourcesMenuProps } from "../resource-menu/IResourcesMenuProps";

const ResourceHeaderTemplate = (props: any) => {
  const { isProject, resourceData } = props;
  const colorData = resourceData.Color;
  const ResourcesMenuData: IResourcesMenuProps = {
    projectId: resourceData.Id,
    project: resourceData,
    contextMenuClickHandler: () => {
      // console.log("contextMenuClickHandler");
    },
  };
  return (
    <>
      {isProject && (
        <div className="resourceHeaderCustom">
          <Box sx={{ width: "100%" }}>
            <Grid container>
              <Grid item xs={3} /*sx={{ backgroundColor: "yellow" }}*/>
                <Box sx={Constant.getSchedulerColor(colorData)}></Box>
              </Grid>
              <Grid item xs={7} sx={{ backgroundColor: "pink" }}>
                <Box>{resourceData.ProjectName} </Box>
              </Grid>
              <Grid item xs={2} /*sx={{ backgroundColor: "lightgreen" }}*/>
                <div>
                  <ResourceMenu
                    {...ResourcesMenuData}
                    pipelineCode={resourceData.pipelineCode}
                    jobCode={resourceData.jobCode}
                  />
                </div>
              </Grid>
            </Grid>
          </Box>
        </div>
      )}
      {!isProject && <div>{resourceData.Name}</div>}
    </>
  );
};

export default ResourceHeaderTemplate;
