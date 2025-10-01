import { Box, Button, Chip, ListItem } from "@mui/material";
import React, { useEffect, useState } from "react";
import { IProjectSkills } from "./IProjectSkillsProps";
import * as constant from "./constant";
import CancelIcon from "@mui/icons-material/Cancel";
import CancelRoundedIcon from "@mui/icons-material/CancelRounded";
import AssignEngagementLeader from "../assign-engagement-leader/assign-engagement-leader";
import AssignSkills from "../assign-skills/assign-skills";
import "./project-skill.css";

const ProjectSkills = (props: any) => {
  const { skills, allSkills } = props;
  const [projectSkills, setProjectSkills] = useState(skills);
  useEffect(() => {
    setProjectSkills(skills);
  }, []);

  const handleDelete = (skill: any) => {
    console.log(skill);
    projectSkills.map((item: any) => {
      if (item?.label == skill?.label) {
        item.isactive = false;
      }
    });
    setProjectSkills(() => {
      return [...projectSkills];
    });
  };

  return (
    <Box mt={2}>
      {/* <div className="skills-header">Skills</div> */}
      <Box>
        <AssignSkills
          SkillsList={allSkills}
          {...props}
        ></AssignSkills>
        {/* {projectSkills
          .filter((item: any) => item.isactive == true)
          .map((skill: any, index: number) => {
            return (
              <Chip
                key={skill.id}
                sx={constant.ChipSxProps}
                onDelete={() => {
                  handleDelete(skill);
                }}
                label={skill.name}
                deleteIcon={<CancelIcon sx={constant.CloseIconSxProps} />}
              />
            );
          })} */}
        {/* <Button
          variant="outlined"
          size="small"
          sx={constant.AddSkillsSxProps}
        >
          Add Skills
        </Button> */}
      </Box>
    </Box>
  );
};

export default ProjectSkills;
