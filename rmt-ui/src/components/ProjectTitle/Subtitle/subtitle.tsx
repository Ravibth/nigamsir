import React, { useContext, useEffect, useState } from "react";
import { Stack, Box, Divider, Typography } from "@mui/material";
import { ISubtitleProps } from "./interface/ISubtitleProps";
import "./subtitle.css";
import { UserDetailsContext } from "../../../contexts/userDetailsContext";
import { GetWCGTURLByPipelineCodeJobCodeAndUserProjectRole } from "./utils";

const Subtitle = (props: ISubtitleProps) => {
  const { subtitleProperties, jobPage, budgetStatus, projectDetails } = props;
  const [wcgtURL, setWCGTURL] = useState(null);
  const userDetails = useContext(UserDetailsContext);
  useEffect(() => {
    if (
      projectDetails &&
      userDetails &&
      userDetails?.projectPermissionData?.projectRoles
    ) {
      const result = GetWCGTURLByPipelineCodeJobCodeAndUserProjectRole(
        userDetails?.projectPermissionData?.projectRoles,
        projectDetails.chargableType,
        projectDetails.pipelineCode,
        projectDetails.jobCode
      );
      setWCGTURL(result);
    }
  }, [projectDetails, userDetails]);
  return (
    <div className="main-container-job-detail">
      <div>
        <Stack
          className="stack"
          direction="row"
          divider={
            <Divider
              sx={{ paddingLeft: "10px" }}
              orientation="vertical"
              flexItem
            />
          }
          spacing={2}
        >
          {subtitleProperties.map((properties: any, index: number) => {
            return (
              <Box key={index} className="titleHeader">
                {properties.title} - <span>{properties.value}</span>
              </Box>
            );
          })}
          {wcgtURL && (
            <Box className="box wcgtHeader">
              WCGT URL -{" "}
              <a className="wcgt-url" target="_blank" href={wcgtURL}>
                {wcgtURL}
              </a>
            </Box>
          )}
          {/* {jobPage && (
            <Box className="box">
              Budget Status -{" "}
           
                {budgetStatus}
          
            </Box>
          )} */}
        </Stack>
      </div>
    </div>
  );
};

export default Subtitle;
