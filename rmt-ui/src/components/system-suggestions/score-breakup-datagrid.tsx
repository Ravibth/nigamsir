import React, { memo } from "react";
import "./style.css";
import Grid from "@mui/material/Grid";
import { Tooltip, SxProps } from "@mui/material";
import { categoryCardSxProps, categoryValueCardSxProps } from "./constants";
import { IScoreBreakup, ISystemSuggestions } from "./interfaces";
import FmdGoodOutlinedIcon from "@mui/icons-material/FmdGoodOutlined";
import WorkOutlineOutlinedIcon from "@mui/icons-material/WorkOutlineOutlined";
import PieChartOutlineOutlinedIcon from "@mui/icons-material/PieChartOutlineOutlined";
import PeopleAltOutlinedIcon from "@mui/icons-material/PeopleAltOutlined";
import CodeOutlinedIcon from "@mui/icons-material/CodeOutlined";
import { sortCategories } from "../../global/utils";
import CompetencyIcon from "../../common/images/Competency.png";

export const PreferenceOrderForScoreBreakup = [
  "competency",
  "offerings",
  "solutions",
  "Industry",
  "Sub_Industry",
];

interface IScoreBreakupDataGridProps {
  sx: SxProps;
  data: ISystemSuggestions;
  categories: string[];
}
const ScoreBreakupDataGrid = (props: IScoreBreakupDataGridProps) => {
  const categoryRowRender = (category: string) => {
    let matching_value = "";
    let label = "";
    let score = 0;
    let icon = undefined;

    const scoreItem: IScoreBreakup = (
      Array.isArray(props.data?.score_breakup) ? props.data?.score_breakup : []
    )?.find(
      (item: IScoreBreakup) =>
        item?.parameter?.toLowerCase() === category?.toLowerCase()
    );
    score = scoreItem?.value ? scoreItem?.value : 0;
    matching_value = (scoreItem?.matching_value || "")
      .split(",")
      .join(",")
      .toUpperCase();
    switch (category.split("_").join(" ").toLowerCase().trim()) {
      case "same client":
        icon = <PeopleAltOutlinedIcon />;
        label = "Same Client Exp.";
        matching_value = score > 0 ? "Yes" : "No";
        break;
      case "solutions":
        label = "Solutions";
        icon = <WorkOutlineOutlinedIcon />;
        break;
      case "location":
        label = "Location";
        icon = <FmdGoodOutlinedIcon />;
        break;
      case "sub industry":
        label = "Sub Industry";
        icon = <PieChartOutlineOutlinedIcon />;
        break;
      case "industry":
        label = "Industry";
        icon = <PieChartOutlineOutlinedIcon />;
        break;
      case "revenue unit":
        label = "Revenue unit";
        icon = <WorkOutlineOutlinedIcon />;
        break;
      case "business unit":
        label = "Business unit";
        icon = <WorkOutlineOutlinedIcon />;
        break;
      case "skills":
        label = "Skills";
        icon = <CodeOutlinedIcon />;
        break;
      case "offerings":
        label = "Offerings";
        icon = <WorkOutlineOutlinedIcon />;
        break;
      case "skill":
        label = "Skill";
        icon = <CodeOutlinedIcon />;
        break;
      case "competency":
        label = "Competency";
        icon = (
          <img
            src={CompetencyIcon}
            alt="competency"
            style={{
              height: "22px",
              width: "22px",
            }}
          />
        );
        break;
      default:
        label = category;
        break;
    }
    return (
      <>
        <Grid item xs={1}>
          {icon}
        </Grid>
        <Grid item xs={4} sx={categoryCardSxProps}>
          {label}
        </Grid>

        <Grid item xs={5} sx={categoryValueCardSxProps}>
          <div className="system-card-content-values-display">
            <Tooltip
              title={<span style={{ fontSize: "14px" }}>{matching_value}</span>}
            >
              <span>{matching_value}</span>
            </Tooltip>
          </div>
        </Grid>
        <Grid item xs={1.5}>
          <span className="scoreCategoryValue">{score}</span>
        </Grid>
      </>
    );
  };

  return (
    <Grid container spacing={1}>
      {sortCategories(props.categories, PreferenceOrderForScoreBreakup).map(
        (category: string) => {
          return (
            <Grid item xs={12} key={category} sx={{ marginBottom: "1px" }}>
              <Grid container spacing={2}>
                {categoryRowRender(category)}
              </Grid>
            </Grid>
          );
        }
      )}
    </Grid>
  );
};
export default memo(ScoreBreakupDataGrid);
