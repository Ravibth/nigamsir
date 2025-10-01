import { Typography } from "@mui/material";
import MySkills from "../components/my-skills/my-skills";
import { MySkillsGridState } from "../components/my-skills/my-skills-grid/mySkillsGridContext/mySkillsGridContext";
import { RequisitionHeaderSxProps } from "../components/create-requisition-main/constant";

const MySkillsMainRoute = () => {
  return (
    <MySkillsGridState>
      <Typography
        component={"div"}
        className="skill-master-heading requisition-header-title"
      >
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          My Skills
        </Typography>
      </Typography>
      <MySkills />
    </MySkillsGridState>
  );
};

export default MySkillsMainRoute;
