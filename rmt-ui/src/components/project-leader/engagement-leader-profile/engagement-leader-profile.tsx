import { Divider, Grid, Typography } from "@mui/material";
import LeaderProfile from "../profile/profile";
import { IPELeadersProflieProps, IPELProfileProps } from "./IPELProfileProps";
import * as GC from "../../../global/constant";
import "./style.css";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";

const ELProfle = (props: IPELeadersProflieProps) => {
  const { engagementLeaders } = props;
  return (
    <Grid className="engagement-container" container sx={{ padding: "13px" }}>
      <Grid item className="enquiry-header">
        <PersonOutlineOutlinedIcon style={{ marginRight: "3px" }} />

        <Typography
          display="inline"
          component="div"
          className="EL"
          sx={{ whiteSpace: "nowrap", fontSize: "25px" }}
        >
          <span style={{ fontSize: "18px", fontWeight: "600" }}>
            Primary Engagement Leader :
          </span>
          {/* <Divider sx={{ width: "250px" }} /> */}
        </Typography>
      </Grid>

      {engagementLeaders.map((profile, index) => (
        <Grid key={index} item sx={{ paddingTop: "5px" }}>
          <LeaderProfile {...profile} index={index} />

        </Grid>
      ))}
    </Grid>
  );
};

export default ELProfle;
