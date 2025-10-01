import { Box, Divider, Typography } from "@mui/material";
import React from "react";
import "./project-supervisors.css";
import { IProjectSupervisorsProps } from "./IProjectSupervisorsProps";

const ProjectSupervisors = (props: IProjectSupervisorsProps) => {
  const { supervisorDetails } = props;

  return (
    <div className="detailsContainer">
      {supervisorDetails.map((input: any, index) => {
        return (
          <Box key={index}>
            {index !== 0 && <Divider />}
            <Typography
              mt={1}
              mb={1}
              variant="overline"
              display="block"
              gutterBottom
              className="supervisorDetailsHeader"
            >
              {input.InputLable}
            </Typography>
            <Typography mt={1} mb={1} variant="body1">
              {input.Value}
            </Typography>
          </Box>
        );
      })}
    </div>
  );
};

export default ProjectSupervisors;
