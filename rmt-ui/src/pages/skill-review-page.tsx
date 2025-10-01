import { Typography } from "@mui/material";
import { RequisitionHeaderSxProps } from "../components/create-requisition-main/constant";
import SkillReviewWrapper from "../components/skill-review/skill-review-wrapper";

const SkillReviewPage = () => {
  return (
    <>
      <Typography
        component={"div"}
        className="skill-master-heading requisition-header-title"
      >
        <Typography component={"span"} sx={RequisitionHeaderSxProps}>
          Skills Review
        </Typography>
      </Typography>
      <SkillReviewWrapper />
    </>
  );
};
export default SkillReviewPage;
